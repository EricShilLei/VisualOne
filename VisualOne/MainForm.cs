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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_catalog.CreateCatalog();
        }

        private void CatalogButton_Click(object sender, EventArgs e)
        {
            m_catalog.CreateCatalog();
        }

        private void RasterizeButton_Click(object sender, EventArgs e)
        {
            m_catalog.RasterizeDirectory();
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            var catalogForm = new CatalogForm(m_catalog);
            catalogForm.Show(this);
        }
    }
}
