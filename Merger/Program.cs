using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;

namespace Merger
{
    class Program
    {
        // Usage:
        // Merger.exe D:\SD\BP\Clone\ D:\SD\BP\NewPublish\
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Wrong parameters. Usage:");
                Console.WriteLine("   Merger.exe [inputFolder] [outputFolder]");
                return;
            }

            MergeFiles(args[1], args[2]);

            Console.ReadKey();
        }

        static private void MergeFiles(string inputFolder, string outputFolder)
        {
            StreamReader catalogReader = File.OpenText(Path.Combine(inputFolder, "_Catalog.txt"));
            while (!catalogReader.EndOfStream)
            {
                string bpLine = catalogReader.ReadLine();
                if (bpLine == string.Empty || bpLine.Trim().Length == 0 || bpLine.StartsWith("#")) // '#' is the beginning of comments
                    continue;

                // The format is: BlueprintID,FileName,Layout,Type,Crop,Aspect,CloneBPID,OriginalFile
                string[] segments = bpLine.Split(',');
                if (segments == null || segments.Length != 9)
                {
                    Console.WriteLine("Wrong file format in Catalog.txt: {0}", bpLine);
                    continue;
                }
                string guidStr = segments[0];
                Guid bluePrintGuid;
                if (!Guid.TryParse(guidStr, out bluePrintGuid))
                {
                    Console.WriteLine("Failed to parse blueprint GUID {0}", guidStr);
                    continue;
                }
                string filename = segments[1];
                string slideIndex = segments[2];
                string layout = segments[3];
                string type = segments[4];
                string crop = segments[5];
                string aspect = segments[6];
                string cloneBPID = segments[7];
                string originalFile = segments[8];
                BluePrint bp = new BluePrint
                {
                    Guid = bluePrintGuid,
                    Source = filename,
                    //FlattendPptxPath = SourceRoot + filename,
                    OriginalPath = originalFile,
                    Layout = layout,
                    Type = type,
                    AspectRaio = aspect,
                    CropNonCrop = crop,
                    CloneBPID = cloneBPID,
                };
                // this.BluePrints.Add(bp);
            }
            catalogReader.Close();
        }
    }
}
