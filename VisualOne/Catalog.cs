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
        public List<string> m_sourcesFor01_1Photo = new List<string>();

        private string m_sourceRoot = @"\\ppt-svc\Features\AutoLayout\DSNR\Blueprints\Active";
        private string m_outputRoot = @"\\ppt-svc\Features\AutoLayout\CatalogTool\data\04-01\";
        // private string m_sourceRoot = @"c:\users\dzhang\desktop\Active";
        // private string m_outputRoot = @"c:\Rendered\Active\";

        public string SourceRoot { get { return m_sourceRoot; } }
        public string RenderedRoot { get { return m_outputRoot; } }

        public CatalogClass(string sourceRoot, string outputRoot)
        {
            this.m_sourceRoot = sourceRoot;
            this.m_outputRoot = outputRoot;
            if (!outputRoot.EndsWith("\\"))
                this.m_outputRoot += "\\";
        }

        public CatalogClass()
        {
        }

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

        private void ChangeGuid(PPT.Slide newSlide)
        {
            foreach (PPT.Shape sp in newSlide.NotesPage.Shapes)
            {
                if (sp.HasTextFrame == MSO.MsoTriState.msoTrue)
                {
                    string notesText = sp.TextFrame2.TextRange.Text;
                    if (notesText.StartsWith("ID="))
                    {
                        notesText = notesText.Replace('\r', '\n');      // Clean up
                        string[] properties = notesText.Split('\n');
                        string bluePrintGuid = properties[0].Substring(3);
                        string newGuid = System.Guid.NewGuid().ToString();
                        notesText.Replace(properties[0], "ID=" + newGuid);
                        sp.TextFrame2.TextRange.Replace(properties[0], "ID=" + newGuid);
                        return;
                    }
                }
            }
        }

        public void DuplicateBPTo(BluePrint bp, string targetSource)
        {
            var pptApp = new PPT.Application();
            string sourcePath = m_sourceRoot + "\\" + bp.layout + "\\" + bp.type + "\\" + bp.cropNonCrop + "\\" + bp.aspectRaio + "\\" + bp.source;
            PPT.Presentation sourcePres = pptApp.Presentations.Open(sourcePath, MSO.MsoTriState.msoTrue);
            string targetPptxPath = m_sourceRoot + "\\01_TITLE+TITLE_CONENT\\1_PHOTO\\CROP\\16x9\\" + targetSource;
            PPT.Presentation targetPres = pptApp.Presentations.Open(targetPptxPath, MSO.MsoTriState.msoTrue);

            // Find the exact blueprint slide
            foreach (PPT.Slide sld in sourcePres.Slides)
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
                            if (bluePrintGuid == bp.guid)
                            {
                                // Paste over to target presentation
                                sld.Select();
                                sld.Copy();
                                var newSlides = targetPres.Slides.Paste();
                                PPT.Slide newSlide = newSlides[1];
                                // Change GUID of the target slide
                                ChangeGuid(newSlide);
                            }
                        }
                    }
                }
            }
        }

        public void OpenBP(BluePrint bp)
        {
            var pptApp = new PPT.Application();
            string pptxPath = m_sourceRoot + "\\" + bp.layout + "\\" + bp.type + "\\" + bp.cropNonCrop + "\\" + bp.aspectRaio + "\\" + bp.source;
            PPT.Presentation pres = pptApp.Presentations.Open(pptxPath, MSO.MsoTriState.msoTrue);

            // Find the exact blueprint slide
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
                            if(bluePrintGuid == bp.guid)
                            {
                                sld.Select();
                            }
                        }
                    }
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

        public void UpdateCatalog()
        {
            var pptApp = new PPT.Application();
            string updatlogFile = @"\updatelog.txt";
            StreamReader updatelogReader = File.OpenText(m_sourceRoot + updatlogFile);
            List<string> failedFiles = new List<string>();
            List<string> successFiles = new List<string>();
            List<string> failedFinalFiles = new List<string>();

            while (!updatelogReader.EndOfStream)
            {
                string currentFile = updatelogReader.ReadLine();
                try
                {
                    RasterizeOne(pptApp, m_sourceRoot + "\\" + currentFile, m_sourceRoot, m_outputRoot);
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
                    RasterizeOne(pptApp, currentFile, m_sourceRoot, m_outputRoot);
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
                if (path.StartsWith("~"))
                    continue;
                if (layout == "01_TITLE+TITLE_CONENT" && type == "1_PHOTO" && crop == "CROP" && aspect == "16x9")
                    m_sourcesFor01_1Photo.Add(path);

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

                var newSlide = pres.Slides.AddSlide(1, pres.Slides[1].CustomLayout);
                newSlide.Export(outputPath + "original" + ".png", "png");

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
