using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PPT = Microsoft.Office.Interop.PowerPoint;
using MSO = Microsoft.Office.Core;

namespace BatchRasterizer
{
    class BatchRasterizer
    {
        static private void RasterizeOne(PPT.Application app, string currentFile, string directory, string outputDir)
        {
            string path = currentFile.Substring(directory.Length + 1);
            if (path.StartsWith("~"))
                return;

            PPT.Presentation pres = app.Presentations.Open(currentFile, MSO.MsoTriState.msoTrue);
            try
            {
                foreach (PPT.Slide sld in pres.Slides)
                {
                    sld.Export(outputDir + "/" + path + ".png", "png");
                }

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

        static private void RasterizeDirectory( string pptxRoot, string outputDir)
        {
            var pptApp = new PPT.Application();
            string directory = pptxRoot;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.TopDirectoryOnly);
            List<string> failedFiles = new List<string>();
            List<string> successFiles = new List<string>();
            List<string> failedFinalFiles = new List<string>();
            StreamWriter catalogWriter = File.CreateText(outputDir + "Failed.txt");

            const int batchSize = 5;
            int indexInBatch = 0;
            foreach (string currentFile in pptxFiles)
            {
                bool fSaved = false;
                try
                {
                    RasterizeOne(pptApp, currentFile, directory, outputDir);
                    successFiles.Add(currentFile);
                    fSaved = true;
                    indexInBatch++;
                    if( indexInBatch == batchSize)
                    {
                        indexInBatch = 0;
                        pptApp.Quit();
                        pptApp = null;
                        pptApp = new PPT.Application();
                    }
                }
                catch (Exception e)
                {
                    if(!fSaved)
                        failedFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                    if ((uint)e.HResult == 0x0800706ba)
                    {
                        pptApp = new PPT.Application();
                        indexInBatch = 0;
                    }
                }
            }

            indexInBatch = 0;
            foreach (string currentFile in failedFiles)
            {
                try
                {
                    RasterizeOne(pptApp, currentFile, directory, outputDir);
                    successFiles.Add(currentFile);
                    indexInBatch++;
                    if (indexInBatch == batchSize)
                    {
                        indexInBatch = 0;
                        pptApp.Quit();
                        Thread.Sleep(1000);
                        pptApp = new PPT.Application();
                    }
                }
                catch (Exception e)
                {
                    failedFinalFiles.Add(currentFile);
                    catalogWriter.WriteLine(currentFile);
                    catalogWriter.WriteLine(e.Message);
                }
            }
            catalogWriter.Flush();
        }

        static void Main(string[] args)
        {
            RasterizeDirectory(args[0], args[1]);
        }
    }
}
