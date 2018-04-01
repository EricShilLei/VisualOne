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
    public partial class ThemeSelectorForm : Form
    {
        public string SelectedTheme { get; set; }
        private string m_renderedRoot;

        public ThemeSelectorForm(List<string> themes, string renderedRoot)
        {
            InitializeComponent();
            this.m_renderedRoot = renderedRoot;
            this.ThemeListbox.DataSource = themes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThemeListbox_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedTheme = (string)this.ThemeListbox.SelectedValue;
            CatalogForm parent = (CatalogForm)this.Parent;
            string imagePath = this.m_renderedRoot + "01_TITLE+TITLE_CONENT_1_PHOTO_CROP_16x9_" + SelectedTheme + "\\original.png";
            this.PreviewPictureBox.Image = System.Drawing.Image.FromFile(imagePath);
        }
    }
}
