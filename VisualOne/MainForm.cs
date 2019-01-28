using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualOne
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnViewAll_Click(object sender, EventArgs e)
        {
            var catalogForm = new CatalogForm(new CatalogClass(m_txtTargetFolder.Text, m_txtThumbnailFolder.Text));
            catalogForm.Show();
        }

        private void BtnGenerateCatalog_Click(object sender, EventArgs e)
        {
        }
    }
}
