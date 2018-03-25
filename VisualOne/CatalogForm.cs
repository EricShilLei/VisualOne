using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisualOne
{
    public partial class CatalogForm : Form
    {
        private List<BluePrint> m_bluePrints;
        public CatalogForm( List<BluePrint> bluePrints)
        {
            m_bluePrints = bluePrints;
            InitializeComponent();
            var source = new BindingSource();
            source.DataSource = m_bluePrints.ToList();
            this.CatalogGridView.AutoGenerateColumns = true;
            this.CatalogGridView.DataSource = source;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var control = (Button)sender;
            var catalogForm = (CatalogForm)control.Parent.Parent.Parent.Parent;
            int rowIndex = SearchWithGuid(catalogForm.queryBox.Text);
            if (rowIndex >= 0)
            {
                catalogForm.CatalogGridView.Rows[rowIndex].Selected = true;
                catalogForm.CatalogGridView.FirstDisplayedScrollingRowIndex = rowIndex;
                catalogForm.CatalogGridView.Update();
            }
        }

        private int SearchWithGuid(string text)
        {
            foreach (DataGridViewRow row in this.CatalogGridView.Rows)
            {
                BluePrint bp = (BluePrint)row.DataBoundItem;
                if (bp.guid == text)
                    return row.Index;
            }
            return -1;
        }

        private void CatalogGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Selected)
            {
                var control = (DataGridView)sender;
                BluePrint bp = (BluePrint)e.Row.DataBoundItem;
                string imagePath = bp.path;
                var catalogForm = (CatalogForm)control.Parent.Parent.Parent.Parent.Parent;
                catalogForm.blueprintPreviewPictureBox.Image = System.Drawing.Image.FromFile(imagePath);
            }
        }
    }
}
