using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PPT = Microsoft.Office.Interop.PowerPoint;
using MSO = Microsoft.Office.Core;

namespace VisualOne
{
    public partial class MainForm : Form
    {
        public List<BluePrint> m_bluePrints = new List<BluePrint>();
        //        private string root = @"\\ppt-svc\Features\AutoLayout\DSNR\Blueprints\";
        private string root = @"c:\users\dzhang\desktop\";
        private string source = "Active";
        private string outputRoot = @"c:\Rendered\Active\";

        public MainForm()
        {
            InitializeComponent();
        }

        private void CreateCatalog(string root, string source, string outputRoot)
        {
            string directory = root + source;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);

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

                string outputPath = outputRoot + layout + "_" + type + "_" + crop + "_" + aspect + "_" + path + "\\";
                if (!Directory.Exists(outputPath))
                    continue;
                string[] pngFiles = Directory.GetFiles(outputPath, "*.png", SearchOption.TopDirectoryOnly);
                StreamReader catalogReader = File.OpenText(outputPath + "Catalog.txt");
                while (!catalogReader.EndOfStream)
                {
                    string bpLine = catalogReader.ReadLine();
                    string[] properties = bpLine.Split('\t');
                    if(properties[0].StartsWith("ID="))
                    {
                        string bluePrintGuid = properties[0].Substring(3);
                        if(pngFiles.Contains(outputPath + bluePrintGuid + ".png"))
                        {
                            BluePrint bp = new BluePrint();
                            bp.source = currentFile;
                            bp.layout = layout;
                            bp.type = type;
                            bp.aspectRaio = aspect;
                            bp.cropNonCrop = crop;
                            bp.guid = bluePrintGuid;
                            bp.otherProperties = properties;
                            bp.path = outputPath + bluePrintGuid + ".png";
                            m_bluePrints.Add(bp);
                        }
                    }
                }
                catalogReader.Close();
            }
        }

        private void RasterizeOne( PPT.Application app, string currentFile, string directory, string outputRoot)
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

        private void RasterizeDirectory(string root, string source, string outputRoot)
        {
            var pptApp = new PPT.Application();
            string directory = root + source;
            var pptxFiles = Directory.EnumerateFiles(directory, "*.pptx", SearchOption.AllDirectories);
            List<string> failedFiles = new List<string>();
            List<string> successFiles = new List<string>();
            List<string> failedFinalFiles = new List<string>();

            foreach (string currentFile in pptxFiles)
            {
                try
                {
                    RasterizeOne(pptApp, currentFile, directory, outputRoot);
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
                    RasterizeOne(pptApp, currentFile, directory, outputRoot);
                    successFiles.Add(currentFile);
                }
                catch (Exception e)
                {
                    failedFinalFiles.Add(currentFile);
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void CatalogButton_Click(object sender, EventArgs e)
        {
            CreateCatalog(root, source, outputRoot);
        }

        private void RasterizeButton_Click(object sender, EventArgs e)
        {
            RasterizeDirectory(root, source, outputRoot);
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            var catalogForm = new CatalogForm(m_bluePrints);
            catalogForm.Show(this);
        }
    }
}
