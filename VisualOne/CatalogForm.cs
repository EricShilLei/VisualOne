using Equin.ApplicationFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisualOne
{
    public partial class CatalogForm : Form
    {
        private BindingListView<BluePrint> m_bluePrintsView;
        public CatalogClass Catalog;

        public CatalogForm(CatalogClass catalog)
        {
            InitializeComponent();
            if (catalog == null)
            {
                Catalog = new CatalogClass();
                Catalog.CreateCatalog();
            }
            else
                Catalog = catalog;
            var source = new BindingSource();
            m_bluePrintsView = new BindingListView<BluePrint>(Catalog.m_bluePrints);
            source.DataSource = m_bluePrintsView;
            this.CatalogGridView.AutoGenerateColumns = true;
            this.CatalogGridView.DataSource = source;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var control = (Button)sender;
            int rowIndex = SearchWithGuid(this.queryBox.Text);
            if (rowIndex >= 0)
            {
                this.CatalogGridView.CurrentCell = null;
                this.CatalogGridView.Rows[rowIndex].Selected = true;
                this.CatalogGridView.FirstDisplayedScrollingRowIndex = rowIndex;
                this.CatalogGridView.Update();
            }
        }

        private int SearchWithGuid(string text)
        {
            if (text == "")
                return -1;
            foreach (DataGridViewRow row in this.CatalogGridView.Rows)
            {
                ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)row.DataBoundItem;
                if (bpWrapper.Object.guid.StartsWith(text))
                    return row.Index;
            }
            return -1;
        }

        private void CatalogGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Selected)
            {
                var control = (DataGridView)sender;
                ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)e.Row.DataBoundItem;
                string imagePath = bpWrapper.Object.path;
                this.blueprintPreviewPictureBox.Image = System.Drawing.Image.FromFile(imagePath);
            }
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            FilterBy();
        }

        private void FilterBy()
        {
            this.CatalogGridView.CurrentCell = null;
            m_bluePrintsView.ApplyFilter(delegate (BluePrint bp) 
            {
                return (this.typeFilter.Text == "" || bp.type == this.typeFilter.Text) &&
                       (this.layoutFilter.Text == "" || bp.layout == this.layoutFilter.Text) &&
                       (this.cropFilter.Text == "" || bp.cropNonCrop == this.cropFilter.Text) &&
                       (this.sourceFilter.Text == "" || bp.source == this.sourceFilter.Text) &&
                       (this.variantFilter.Text == "" || bp.variant == this.variantFilter.Text)
                ;
            });
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            this.CatalogGridView.CurrentCell = null;
            if(this.sortStatement.Text != "")
                m_bluePrintsView.ApplySort(this.sortStatement.Text);
        }
    }
}
