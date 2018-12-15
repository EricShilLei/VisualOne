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

namespace VisualOne
{
    public partial class MainForm : Form
    {
        CatalogClass m_catalog = new CatalogClass();
        bool m_fLocalCatalog = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void CreateCatalogOnceWithProgressBar()
        {
            if (this.CreateCatalogProgressBar.Value > 0)
                return;
            m_catalog.CreateCatalog(this.CreateCatalogProgressBar);
        }

        private void LocalCatalogButton_Click(object sender, EventArgs e)
        {
            var folderBrowserResult = this.sourceFolderBrowserDialog.ShowDialog(this);
            string sourceRoot = folderBrowserResult == DialogResult.OK ? this.sourceFolderBrowserDialog.SelectedPath : null;
            this.sourceFolderBrowserDialog.Description = "Pick the rendered thumbnails folder";
            folderBrowserResult = this.sourceFolderBrowserDialog.ShowDialog(this);
            string outputRoot = folderBrowserResult == DialogResult.OK ? this.sourceFolderBrowserDialog.SelectedPath : null;
            m_catalog = new CatalogClass(sourceRoot, outputRoot);
            m_fLocalCatalog = true;
            this.CreateCatalogProgressBar.Value = 0;
            CreateCatalogOnceWithProgressBar();
            var catalogForm = new CatalogForm(m_catalog);
            catalogForm.ShowDialog(this);
        }

        private void RasterizeButton_Click(object sender, EventArgs e)
        {
            m_catalog.RasterizeDirectory();
        }

        private void OnlineCatalogButton_Click(object sender, EventArgs e)
        {
            if (m_fLocalCatalog)
            {
                m_catalog = new CatalogClass();
                m_fLocalCatalog = false;
                this.CreateCatalogProgressBar.Value = 0;
            }
            CreateCatalogOnceWithProgressBar();
            var catalogForm = new CatalogForm(m_catalog);
            catalogForm.ShowDialog(this);
        }

        private void UpdateCatalogButton_Click(object sender, EventArgs e)
        {
            m_catalog.UpdateCatalog();
        }

        private void ResizeRenderingButton_Click(object sender, EventArgs e)
        {
            m_catalog = new CatalogClass(@"c:\users\dzhang\desktop\Active", @"c:\Rendered\Active");
            m_catalog.ResizeRendering(@"c:\rendered", false, false);
        }

        private void ResizeGrayscaleButton_Click(object sender, EventArgs e)
        {
            m_catalog = new CatalogClass(@"c:\users\dzhang\desktop\Active", @"c:\Rendered\Active");
            m_catalog.ResizeRendering(@"c:\rendered\grayscale", true, false);
        }

        private void ResizeRenderingAsPng_Click(object sender, EventArgs e)
        {
            m_catalog = new CatalogClass(@"c:\users\dzhang\desktop\Active", @"c:\Rendered\Active");
            m_catalog.ResizeRendering(@"c:\rendered\png", false, true);
        }
    }
}
