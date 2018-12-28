using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeOrMove
{
    class ResizeOrMove
    {
        static Guid BPGuid(string currentFile)
        {
            int lastSlash = currentFile.LastIndexOf('\\');
            string fileName = currentFile.Substring(lastSlash + 1);
            if (fileName == "original.png")
                return Guid.Empty;
            int lastDot = fileName.LastIndexOf('.');
            Guid bpGuid;
            if (Guid.TryParse(fileName.Substring(0, lastDot), out bpGuid))
                return bpGuid;
            else
                return Guid.Empty;
        }

        static void Move(string sourceRoot, string outputDir)
        {
            string directory = sourceRoot;
            var pngFiles = Directory.EnumerateFiles(directory, "*.png", SearchOption.AllDirectories);
            Parallel.ForEach(pngFiles, (currentFile) =>
                {
                    Guid bpGuid = BPGuid(currentFile);
                    if (bpGuid != Guid.Empty)
                    {
                        string outputFile = outputDir + "\\" + bpGuid + ".png";
                        File.Copy(currentFile, outputFile, true);
                    }
                }
            );
        }

        static void MoveOriginal(string oldRenderedRoot, string outputDir)
        {
            string directory = oldRenderedRoot;
            var pngFiles = Directory.EnumerateFiles(directory, "original.png", SearchOption.AllDirectories);
            Parallel.ForEach(pngFiles, (currentFile) =>
            {
                int lastSlash = currentFile.LastIndexOf('\\');
                string original = currentFile.Substring(0, lastSlash);
                lastSlash = original.LastIndexOf('\\');
                string name = original.Substring(lastSlash + 1);
                string outputFile = outputDir + "\\" + name + ".png";
                File.Copy(currentFile, outputFile, true);
            }
            );
        }

        static void ResizeRendering(string sourceRoot, string outputDir, bool fGrayscale, bool fPng, bool fMove)
        {
            string directory = sourceRoot;
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
                Guid bpGuid = BPGuid(currentFile);
                if (bpGuid == Guid.Empty)
                    continue;
                string outputFileName = bpGuid + (fPng ? "png" : "jpg");
                string _32x18_FilePath = outputDir + "\\32x18\\" + outputFileName;
                string _128x72_FilePath = outputDir + "\\128x72\\" + outputFileName;
                if (File.Exists(_32x18_FilePath) && File.Exists(_128x72_FilePath))
                    continue;
                sourceImage = System.Drawing.Image.FromFile(currentFile);
                Rectangle _32x12_destRect = new Rectangle(0, 0, 32, 18);
                Rectangle _128x72_destRect = new Rectangle(0, 0, 128, 72);

                using (var g = System.Drawing.Graphics.FromImage(_32x18_Image))
                {
                    if (fGrayscale)
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

        static void Main(string[] args)
        {
            MoveOriginal(args[0], args[1]);
        }
    }
}
