using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PPT = Microsoft.Office.Interop.PowerPoint;
using MSO = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

namespace VisualOne
{
    public class CatalogClass
    {
        public List<BluePrint> m_bluePrints = new List<BluePrint>();
        public List<string> m_sourcesFor01_1Photo = new List<string>();


        // private string m_sourceRoot = @"\\ppt-svc\Features\AutoLayout\DSNR\Blueprints\Active";
        // private string m_outputRoot = @"\\ppt-svc\Features\AutoLayout\CatalogTool\data\04-01\";
#pragma warning disable IDE0044 // Add readonly modifier
        private string m_sourceRoot = @"c:\users\dzhang\desktop\Active";
        private string m_outputRoot = @"c:\Rendered\Active\";
#pragma warning restore IDE0044 // Add readonly modifier

        public string SourceRoot { get { return m_sourceRoot; } }
        public string RenderedRoot { get { return m_outputRoot; } }

        public CatalogClass(string sourceRoot, string outputRoot)
        {
            if( sourceRoot != null)
                this.m_sourceRoot = sourceRoot;
            if( outputRoot != null)
            {
                this.m_outputRoot = outputRoot;
                if (!outputRoot.EndsWith("\\"))
                    this.m_outputRoot += "\\";
            }
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
                    TempSeenKeptRecord record = new TempSeenKeptRecord
                    {
                        seen = seenCount,
                        kept = keptCount
                    };
                    performanceValues.Add(guid, record);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        internal void ResizeRendering(string resizedOutputDir, bool fGrayscale, bool fPng)
        {
            string directory = this.RenderedRoot;
            var pngFiles = Directory.EnumerateFiles(directory, "*.png", SearchOption.AllDirectories);
            var _32x18_Image = new System.Drawing.Bitmap(32, 18);
            var _128x72_Image = new System.Drawing.Bitmap(128, 72);
            System.Drawing.Image sourceImage;
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                         new float[] {.3f, .3f, .3f, 0, 0},
                         new float[] {.59f, .59f, .59f, 0, 0},
                         new float[] {.11f, .11f, .11f, 0, 0},
                         new float[] {0, 0, 0, 1, 0},
                         new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            foreach (string currentFile in pngFiles)
            {
                int lastSlash = currentFile.LastIndexOf('\\');
                string fileName = currentFile.Substring(lastSlash + 1);
                if (fileName == "original.png")
                    continue;
                string outputFileName = fileName;
                if (!fPng)
                    outputFileName = outputFileName.Replace("png", "jpg");
                string _32x18_FilePath = resizedOutputDir + "\\32x18\\" + outputFileName;
                string _128x72_FilePath = resizedOutputDir + "\\128x72\\" + outputFileName;
                if(File.Exists(_32x18_FilePath) && File.Exists(_128x72_FilePath))
                    continue;
                sourceImage = System.Drawing.Image.FromFile(currentFile);
                Rectangle _32x12_destRect = new Rectangle(0, 0, 32, 18);
                Rectangle _128x72_destRect = new Rectangle(0, 0, 128, 72);

                using (var g = System.Drawing.Graphics.FromImage(_32x18_Image))
                {
                    if(fGrayscale)
                        g.DrawImage(sourceImage, _32x12_destRect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, attributes);
                    else
                        g.DrawImage(sourceImage, 0, 0, 32, 18);
                }
                _32x18_Image.Save(_32x18_FilePath, fPng ? ImageFormat.Png : ImageFormat.Jpeg);
                using (var g = System.Drawing.Graphics.FromImage(_128x72_Image))
                {
                    if (fGrayscale)
                        g.DrawImage(sourceImage, _128x72_destRect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, attributes);
                    else
                        g.DrawImage(sourceImage, 0, 0, 128, 72);
                }
                _128x72_Image.Save(_128x72_FilePath, fPng ? ImageFormat.Png : ImageFormat.Jpeg);
                sourceImage.Dispose();
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

        internal void DuplicateBPToCurrentPresentation(BluePrint bp)
        {
            var pptApp = new PPT.Application();
            string sourcePath = m_sourceRoot + "\\" + bp.layout + "\\" + bp.type + "\\" + bp.cropNonCrop + "\\" + bp.aspectRaio + "\\" + bp.source;
            PPT.Presentation targetPres = pptApp.Presentations[1];
            PPT.Presentation sourcePres = pptApp.Presentations.Open(sourcePath,
                                                                    MSO.MsoTriState.msoTrue,
                                                                    MSO.MsoTriState.msoFalse,
                                                                    MSO.MsoTriState.msoFalse);
            float sourceSlideHeight = sourcePres.PageSetup.SlideHeight;
            float sourceSlideWidth = sourcePres.PageSetup.SlideWidth;
            float targetSlideCenterY = targetPres.PageSetup.SlideHeight / 2;
            float targetSlideCenterX = targetPres.PageSetup.SlideWidth / 2;

            float vResizeRatio = (sourceSlideWidth / sourceSlideHeight)/ (targetSlideCenterX / targetSlideCenterY);

            // Find the exact blueprint slide
            foreach (PPT.Slide sld in sourcePres.Slides)
            {
                // Paste over to target presentation
                sld.Select();
                sld.Copy();
                var newSlides = targetPres.Slides.Paste();
                PPT.Slide newSlide = newSlides[1];
                foreach (PPT.Shape shape in newSlide.Shapes)
                {
                    float centerX = shape.Left + shape.Width / 2;
                    float centerY = shape.Top + shape.Height / 2;
                    float newY = (centerY - targetSlideCenterY) * vResizeRatio + targetSlideCenterY;
                    float newHeight = shape.Height;
                    if (shape.Type == MSO.MsoShapeType.msoAutoShape && shape.AutoShapeType == MSO.MsoAutoShapeType.msoShapeRectangle)
                        newHeight = shape.Height * vResizeRatio;
                    else if (shape.Type == MSO.MsoShapeType.msoTextBox)
                        newHeight = shape.Height * vResizeRatio;
                    else if (shape.Type == MSO.MsoShapeType.msoGroup)
                        newHeight = shape.Height * vResizeRatio;
//                    else if (shape.Type == MSO.MsoShapeType.msoPicture)
//                        newHeight = shape.Height * vResizeRatio;
                    else if (shape.Type == MSO.MsoShapeType.msoPlaceholder)
                        newHeight = shape.Height * vResizeRatio;
                    shape.Top = newY - newHeight / 2;
                    shape.Height = newHeight;
                }
                // Change GUID of the target slide
                ChangeGuid(newSlide);
            }
            sourcePres.Close();
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

        public void CreateCatalog(ProgressBar progressBar)
        {
            string directory = m_sourceRoot;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            Dictionary<string, TempSeenKeptRecord> performanceValues = new Dictionary<string, TempSeenKeptRecord>();
            ReadPerformanceSummary(performanceValues);
            int pptxCount = 0;
            if (progressBar != null)
                progressBar.Maximum = pptxFiles.Count();
            m_bluePrints.Clear();
            foreach (string currentFile in pptxFiles)
            {
                pptxCount++;
                if (progressBar != null)
                    progressBar.Value = pptxCount;
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
                            BluePrint bp = new BluePrint
                            {
                                source = path,
                                layout = layout,
                                type = type,
                                aspectRaio = aspect,
                                cropNonCrop = crop,
                                guid = bluePrintGuid,
                                otherProperties = properties,
                                path = outputPath + bluePrintGuid + ".png"
                            };
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
