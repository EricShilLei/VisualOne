using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SharedLib;
using PPT = Microsoft.Office.Interop.PowerPoint;
using MSO = Microsoft.Office.Core;

namespace VisualOne
{
    class BlueprintOperations
    {
        private static void ChangeGuid(PPT.Slide newSlide)
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

        public static void OpenBP(BluePrint bp)
        {
            try
            {
                var pptApp = new PPT.Application();
                PPT.Presentation pres = pptApp.Presentations.Open(bp.FlattendPptxPath, MSO.MsoTriState.msoTrue);

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
                                if (bluePrintGuid == bp.Guid.ToString())
                                {
                                    sld.Select();
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Log.TraceTag(Log.Level.Error, "Exception when open file {0}, Exception:{1}", bp.FlattendPptxPath, e.ToString());
            }
        }

        internal static void DuplicateBPToActivePresentation(BluePrint bp)
        {
            var pptApp = new PPT.Application();
            if (pptApp.Presentations.Count < 1)
                return;     // TODO: Alert need a target to apply
            PPT.Presentation targetPres = pptApp.Presentations[1];
            PPT.Presentation sourcePres = pptApp.Presentations.Open(bp.FlattendPptxPath,
                                                                    MSO.MsoTriState.msoTrue,
                                                                    MSO.MsoTriState.msoFalse,
                                                                    MSO.MsoTriState.msoFalse);
            float sourceSlideHeight = sourcePres.PageSetup.SlideHeight;
            float sourceSlideWidth = sourcePres.PageSetup.SlideWidth;
            float targetSlideCenterY = targetPres.PageSetup.SlideHeight / 2;
            float targetSlideCenterX = targetPres.PageSetup.SlideWidth / 2;

            float vResizeRatio = (sourceSlideWidth / sourceSlideHeight) / (targetSlideCenterX / targetSlideCenterY);

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

        public static void RasterizeOriginals(string blueprintFolder, string thumbnailFolder)
        {
            var pptApp = new PPT.Application();
            string directory = blueprintFolder;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            List<string> failedFiles = new List<string>();
            List<string> successFiles = new List<string>();
            List<string> failedFinalFiles = new List<string>();

            foreach (string currentFile in pptxFiles)
            {
                try
                {
                    RasterizeOneOriginal(pptApp, currentFile, directory, thumbnailFolder);
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
                    RasterizeOneOriginal(pptApp, currentFile, directory, thumbnailFolder);
                    successFiles.Add(currentFile);
                }
                catch (Exception e)
                {
                    failedFinalFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void RasterizeOneOriginal(PPT.Application app, string currentFile, string directory, string outputRoot)
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
    }
}
