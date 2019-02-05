using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib
{
    public class CatalogClass
    {
        public List<BluePrint> BluePrints { get; set; } = new List<BluePrint>();

#pragma warning disable IDE0044 // Add readonly modifier
        private string m_sourceRoot = @"c:\sample\";
        private string m_outputRoot = @"c:\flat.png\";
#pragma warning restore IDE0044 // Add readonly modifier

        public string SourceRoot { get { return m_sourceRoot; } }
        public string RenderedRoot { get { return m_outputRoot; } }

        public static readonly string c_catalogFileName = "_Catalog.txt";
        public static readonly string c_7dayPerfFileName = "_7day.txt";

        public CatalogClass(string sourceRoot, string outputRoot)
        {
            if (sourceRoot != null)
            {
                this.m_sourceRoot = sourceRoot;
                if (!sourceRoot.EndsWith("\\"))
                    this.m_sourceRoot += "\\";
            }
            if ( outputRoot != null)
            {
                this.m_outputRoot = outputRoot;
                if (!outputRoot.EndsWith("\\"))
                    this.m_outputRoot += "\\";
            }
        }

        public void ReadFlatCatalog()
        {
            this.BluePrints = ReadBlueprints(SourceRoot, RenderedRoot);
        }

        public static List<BluePrint> ReadBlueprints(string blueprintFolder, string thumbnailFolder)
        {
            List<BluePrint> blueprints = new List<BluePrint>();
            Dictionary<Guid, string> guidsWithPngs = ReadThumbnailGuidsWithPngs(thumbnailFolder);
            Dictionary<Guid, TempSeenKeptRecord> performanceValues = ReadPerformanceSummary(blueprintFolder);
            HashSet<Guid> bpIDSets = new HashSet<Guid>();

            using (StreamReader catalogReader = File.OpenText(Path.Combine(blueprintFolder, c_catalogFileName)))
            {
                while (!catalogReader.EndOfStream)
                {
                    string bpLine = catalogReader.ReadLine();
                    if (bpLine == string.Empty || bpLine.Trim().Length == 0 || bpLine.StartsWith("#")) // '#' is the beginning of comments
                        continue;

                    // The format is: BlueprintID,FileName,Layout,Type,Crop,Aspect,CloneBPID,OriginalFile
                    string[] segments = bpLine.Split(',');
                    if (segments == null || segments.Length != 11)
                    {
                        Log.TraceTag(Log.Level.Error, "Wrong file format in Catalog.txt: {0}", bpLine);
                        continue;
                    }
                    string guidStr = segments[0];
                    string filename = segments[1];
                    string slideIndex = segments[2];
                    string themeName = segments[3];
                    string variantName = segments[4];
                    string layout = segments[5];
                    string type = segments[6];
                    string crop = segments[7];
                    string aspect = segments[8];
                    string cloneBPID = segments[9];
                    string originalFile = segments[10];

                    Guid bluePrintGuid;
                    if (!Guid.TryParse(guidStr, out bluePrintGuid))
                    {
                        Log.TraceTag(Log.Level.Error, "Failed to parse blueprint GUID {0}", guidStr);
                        continue;
                    }
                    if (bpIDSets.Contains(bluePrintGuid))
                    {
                        Log.TraceTag(Log.Level.Error, "Duplicated BlueprintID found:", guidStr);
                        continue;
                    }

                    BluePrint bp = new BluePrint
                    {
                        Guid = bluePrintGuid,
                        Source = filename,
                        FlattendPptxPath = Path.Combine(blueprintFolder, filename),
                        Theme = themeName,
                        Variant = variantName,
                        OriginalPath = originalFile,
                        Layout = layout,
                        Type = type,
                        AspectRaio = aspect,
                        CropNonCrop = crop,
                        CloneBPID = cloneBPID,
                        SlideIndex = uint.Parse(slideIndex),
                        PngPath = guidsWithPngs.ContainsKey(bluePrintGuid) ? guidsWithPngs[bluePrintGuid] : "",
                    };
                    if (!guidsWithPngs.ContainsKey(bluePrintGuid))
                        Console.WriteLine("No png found for " + bluePrintGuid);

                    if (performanceValues.ContainsKey(bluePrintGuid))
                    {
                        bp.Kept = performanceValues[bluePrintGuid].kept;
                        bp.Seen = performanceValues[bluePrintGuid].seen;
                        if (bp.Seen > 0)
                            bp.KeptRate = (double)bp.Kept * 800 / bp.Seen;
                    }

                    blueprints.Add(bp);
                    bpIDSets.Add(bluePrintGuid);
                }
                catalogReader.Close();
            }

            return blueprints;
        }

        public static bool SaveBlueprints(List<BluePrint> bpList, string outputDir)
        {
            bpList.Sort();
            using (StreamWriter catalogWriter = File.CreateText(Path.Combine(outputDir, c_catalogFileName)))
            {
                catalogWriter.WriteLine("#BlueprintID,FileName,SlideIndex,Theme,Variant,Layout,Type,Crop,Aspect,CloneBPID,OriginalFile");
                foreach (var bp in bpList)
                {
                    catalogWriter.WriteLine($"{bp.Guid},{bp.Source},{bp.SlideIndex},{bp.Theme},{bp.Variant},{bp.Layout},{bp.Type},{bp.CropNonCrop},{bp.AspectRaio},{bp.CloneBPID},{bp.OriginalPath}");
                }
                catalogWriter.Flush();
            }
            return true;
        }

        private static Dictionary<Guid, string> ReadThumbnailGuidsWithPngs(string thumbnailFolder)
        {
            Dictionary<Guid, string> thumbnails = new Dictionary<Guid, string>();
            string[] pngFiles = Directory.GetFiles(thumbnailFolder, "*.png", SearchOption.AllDirectories);

            foreach (var path in pngFiles)
            {
                string fileNameNoExtention = Path.GetFileNameWithoutExtension(path);
                if (Guid.TryParse(fileNameNoExtention, out Guid bpGuid))
                    thumbnails[bpGuid] = path;
            }

            return thumbnails;
        }

        private static Dictionary<Guid, TempSeenKeptRecord> ReadPerformanceSummary(string rootFolder)
        {
            Dictionary<Guid, TempSeenKeptRecord> performanceValues = new Dictionary<Guid, TempSeenKeptRecord>();
            string perfFileName = Path.Combine(rootFolder, c_7dayPerfFileName);
            if (!File.Exists(perfFileName))
                return performanceValues;

            StreamReader summaryReader = File.OpenText(perfFileName);
            while (!summaryReader.EndOfStream)
            {
                string bpLine = summaryReader.ReadLine();
                string[] segments = bpLine.Split('\t');
                if (segments.Length != 3)
                    continue;
                string guidStr = segments[0];
                string seen = segments[1];
                string kept = segments[2];
                try
                {
                    UInt32 seenCount = UInt32.Parse(seen);
                    UInt32 keptCount = UInt32.Parse(kept);
                    if (keptCount > seenCount)
                    {
                        Log.TraceTag(Log.Level.Error, "keptcount is larger than seencount, which is wrong:{0}", bpLine);
                        throw new ArgumentOutOfRangeException();
                    }
                    TempSeenKeptRecord record = new TempSeenKeptRecord
                    {
                        seen = seenCount,
                        kept = keptCount
                    };
                    Guid guid = Guid.Parse(guidStr);
                    performanceValues.Add(guid, record);
                }
                catch (Exception e)
                {
                    Log.TraceTag(Log.Level.Error, "Exception when read the performance data:{0}", e.ToString());
                }
            }

            return performanceValues;
        }
    }
}
