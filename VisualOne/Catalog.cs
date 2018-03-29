using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PPT = Microsoft.Office.Interop.PowerPoint;
using MSO = Microsoft.Office.Core;

namespace VisualOne
{
    public class CatalogClass
    {
        public List<BluePrint> m_bluePrints = new List<BluePrint>();

        // private string m_sourceRoot = @"\\ppt-svc\Features\AutoLayout\DSNR\Blueprints\Active";
        private string m_sourceRoot = @"c:\users\dzhang\desktop\Active";
        private string m_outputRoot = @"c:\Rendered\Active\";
  
        private void ReadPerformanceSummary(Dictionary<string, TempSeenKeptRecord> performanceValues)
        {
            StreamReader summaryReader = File.OpenText(m_outputRoot + "7day.txt");
            while (!summaryReader.EndOfStream)
            {
                string bpLine = summaryReader.ReadLine();
                string[] segments = bpLine.Split('\t');
                if (segments.Count() != 3)
                    continue;
                string guid = segments[0];
                string seen = segments[1];
                string kept = segments[2];
                try
                {
                    UInt32 seenCount = UInt32.Parse(seen);
                    UInt32 keptCount = UInt32.Parse(kept);
                    if (keptCount > seenCount)
                        throw new ArgumentOutOfRangeException();
                    TempSeenKeptRecord record = new TempSeenKeptRecord();
                    record.seen = seenCount;
                    record.kept = keptCount;
                    performanceValues.Add(guid, record);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void RasterizeOriginals()
        {
            var pptApp = new PPT.Application();
            string directory = m_sourceRoot;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            List<string> failedFiles = new List<string>();
            List<string> successFiles = new List<string>();
            List<string> failedFinalFiles = new List<string>();

            foreach (string currentFile in pptxFiles)
            {
                try
                {
                    RasterizeOneOriginal(pptApp, currentFile, directory, m_outputRoot);
                    successFiles.Add(currentFile);
                }
                catch (Exception e)
                {
                    failedFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                    if ((uint)e.HResult == 0x0800706ba)
                        pptApp = new PPT.Application();
                }
            }

            foreach (string currentFile in failedFiles)
            {
                try
                {
                    RasterizeOneOriginal(pptApp, currentFile, directory, m_outputRoot);
                    successFiles.Add(currentFile);
                }
                catch (Exception e)
                {
                    failedFinalFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                }
            }
        }
 
        public void CreateCatalog()
        {
            string directory = m_sourceRoot;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            Dictionary<string, TempSeenKeptRecord> performanceValues = new Dictionary<string, TempSeenKeptRecord>();
            ReadPerformanceSummary(performanceValues);

            m_bluePrints.Clear();
            foreach (string currentFile in pptxFiles)
            {
                string fileName = currentFile.Substring(directory.Length + 1);
                string[] segments = fileName.Split('\\');
                string layout = segments[0];
                string type = segments[1];
                string crop = segments[2];
                string aspect = segments[3];
                string path = segments[4];

                string outputPath = m_outputRoot + layout + "_" + type + "_" + crop + "_" + aspect + "_" + path + "\\";
                if (!Directory.Exists(outputPath))
                    continue;
                string[] pngFiles = Directory.GetFiles(outputPath, "*.png", SearchOption.TopDirectoryOnly);
                StreamReader catalogReader = File.OpenText(outputPath + "Catalog.txt");
                while (!catalogReader.EndOfStream)
                {
                    string bpLine = catalogReader.ReadLine();
                    string[] properties = bpLine.Split('\t');
                    if (properties[0].StartsWith("ID="))
                    {
                        string bluePrintGuid = properties[0].Substring(3);
                        if (pngFiles.Contains(outputPath + bluePrintGuid + ".png"))
                        {
                            BluePrint bp = new BluePrint();
                            bp.source = path;
                            bp.layout = layout;
                            bp.type = type;
                            bp.aspectRaio = aspect;
                            bp.cropNonCrop = crop;
                            bp.guid = bluePrintGuid;
                            bp.otherProperties = properties;
                            bp.path = outputPath + bluePrintGuid + ".png";
                            foreach (string property in properties)
                            {
                                if (property.StartsWith("Variant="))
                                    bp.variant = property.Substring(8);
                            }
                            if (performanceValues.ContainsKey(bluePrintGuid))
                            {
                                bp.kept = performanceValues[bluePrintGuid].kept;
                                bp.seen = performanceValues[bluePrintGuid].seen;
                                if (bp.seen > 0)
                                    bp.keptRate = (double)bp.kept * 800 / bp.seen;
                            }
                            m_bluePrints.Add(bp);
                        }
                    }
                }
                catalogReader.Close();
            }
        }

        private void RasterizeOneOriginal(PPT.Application app, string currentFile, string directory, string outputRoot)
        {
            string fileName = currentFile.Substring(directory.Length + 1);
            string[] segments = fileName.Split('\\');
            string layout = segments[0];
            string type = segments[1];
            string crop = segments[2];
            string aspect = segments[3];
            string path = segments[4];
            if (path.StartsWith("~"))
                return;

            PPT.Presentation pres = app.Presentations.Open(currentFile, MSO.MsoTriState.msoTrue);
            string outputPath = outputRoot + layout + "_" + type + "_" + crop + "_" + aspect + "_" + path + "\\";
            Directory.CreateDirectory(outputPath);
            try
            {
                PPT.Slide sld = pres.Slides[1];
                var newSlide = pres.Slides.AddSlide(1, sld.CustomLayout);
                newSlide.Export(outputPath + "original" + ".png", "png");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                pres.Close();
            }
        }

        private void RasterizeOne(PPT.Application app, string currentFile, string directory, string outputRoot)
        {
            string fileName = currentFile.Substring(directory.Length + 1);
            string[] segments = fileName.Split('\\');
            string layout = segments[0];
            string type = segments[1];
            string crop = segments[2];
            string aspect = segments[3];
            string path = segments[4];
            if (path.StartsWith("~"))
                return;

            PPT.Presentation pres = app.Presentations.Open(currentFile, MSO.MsoTriState.msoTrue);
            string outputPath = outputRoot + layout + "_" + type + "_" + crop + "_" + aspect + "_" + path + "\\";
            Directory.CreateDirectory(outputPath);
            StreamWriter catalogWriter = File.CreateText(outputPath + "Catalog.txt");
            try
            {
                foreach (PPT.Slide sld in pres.Slides)
                {
                    foreach (PPT.Shape sp in sld.NotesPage.Shapes)
                    {
                        if (sp.HasTextFrame == MSO.MsoTriState.msoTrue)
                        {
                            string notesText = sp.TextFrame2.TextRange.Text;
                            if (notesText.StartsWith("ID="))
                            {
                                notesText = notesText.Replace('\r', '\n');      // Clean up
                                string[] properties = notesText.Split('\n');
                                string bluePrintGuid = properties[0].Substring(3);
                                sld.Export(outputPath + bluePrintGuid + ".png", "png");
                                string bpLine = notesText.Replace('\n', '\t');
                                catalogWriter.WriteLine(bpLine);
                            }
                        }
                    }
                }
                catalogWriter.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                catalogWriter.Close();
                pres.Close();
            }
        }

        public void RasterizeDirectory()
        {
            var pptApp = new PPT.Application();
            string directory = m_sourceRoot;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            List<string> failedFiles = new List<string>();
            List<string> successFiles = new List<string>();
            List<string> failedFinalFiles = new List<string>();

            foreach (string currentFile in pptxFiles)
            {
                try
                {
                    RasterizeOne(pptApp, currentFile, directory, m_outputRoot);
                    successFiles.Add(currentFile);
                }
                catch (Exception e)
                {
                    failedFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                    if ((uint)e.HResult == 0x0800706ba)
                        pptApp = new PPT.Application();
                }
            }

            foreach (string currentFile in failedFiles)
            {
                try
                {
                    RasterizeOne(pptApp, currentFile, directory, m_outputRoot);
                    successFiles.Add(currentFile);
                }
                catch (Exception e)
                {
                    failedFinalFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
