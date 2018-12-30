using Equin.ApplicationFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisualOne
{
    public partial class CatalogForm : Form
    {
        private UInt32 keptRateUpbound = 500;   // Large enough
        private UInt32 keptRateLowbound = 0;
        private UInt32 seenGreaterThan = 0;
        private UInt32 keptGreaterThan = 0;
        private UInt32 seenLessThan = UInt32.MaxValue;
        private UInt32 keptLessThan = UInt32.MaxValue;
        private bool hasTypeFilter = false;
        private bool hasSourceFilter = false;
        private bool hasCropFilter = false;
        private bool hasLayoutFilter = false;

        private BindingListView<BluePrint> m_bluePrintsView;
        public CatalogClass Catalog;

        public CatalogForm(CatalogClass catalog)
        {
            InitializeComponent();
            if (catalog == null)
            {
                var folderBrowserResult = this.sourceFolderBrowserDialog.ShowDialog(this);
                string sourceRoot = folderBrowserResult == DialogResult.OK ?  this.sourceFolderBrowserDialog.SelectedPath : null;
                this.sourceFolderBrowserDialog.Description = "Pick the rendered thumbnails folder";
                folderBrowserResult = this.sourceFolderBrowserDialog.ShowDialog(this);
                string outputRoot = folderBrowserResult == DialogResult.OK ? this.sourceFolderBrowserDialog.SelectedPath : null;
                Catalog = new CatalogClass(sourceRoot, outputRoot);
                Catalog.ReadFlatCatalog();

            }
            else
                Catalog = catalog;
            this.sourceRootTextbox.Text = Catalog.SourceRoot;
            this.renderedRootTextbox.Text = Catalog.RenderedRoot;

            var source = new BindingSource();
            m_bluePrintsView = new BindingListView<BluePrint>(Catalog.BluePrints);
            source.DataSource = m_bluePrintsView;
            this.CatalogGridView.AutoGenerateColumns = true;
            this.CatalogGridView.DataSource = source;
            this.totalBluePrintsCount.Text = this.CatalogGridView.Rows.Count.ToString();
            this.inFilterBluePrintsCount.Text = this.CatalogGridView.Rows.Count.ToString();
            this.toolStripStatusLabelSource.Text = Catalog.SourceRoot;
            this.toolStripStatusLabelRendered.Text = Catalog.RenderedRoot;
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
            if (!Guid.TryParse(text, out Guid selectedGuid))
                return -1;
            if (text == "")
                return -1;
            foreach (DataGridViewRow row in this.CatalogGridView.Rows)
            {
                ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)row.DataBoundItem;
                if (bpWrapper.Object.Guid == selectedGuid)
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
                string imagePath = bpWrapper.Object.PngPath;
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
            this.m_bluePrintsView.ApplyFilter(delegate (BluePrint bp) 
            {
                return bp.Seen >= this.seenGreaterThan &&
                       bp.Kept >= this.keptGreaterThan &&
                       bp.Seen < this.seenLessThan &&
                       bp.Kept < this.keptLessThan &&
                       bp.KeptRate < this.keptRateUpbound &&
                       bp.KeptRate >= this.keptRateLowbound &&
                       (!this.hasTypeFilter || bp.Type == this.typeFilter.Text) &&
                       (!this.hasLayoutFilter || bp.Layout == this.layoutFilter.Text) &&
                       (!this.hasCropFilter || bp.CropNonCrop == this.cropFilter.Text) &&
                       (!this.hasSourceFilter || bp.Source == this.sourceFilter.Text);
            });
            this.inFilterBluePrintsCount.Text = this.m_bluePrintsView.Count.ToString();
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            this.CatalogGridView.CurrentCell = null;
            if(this.sortStatement.Text != "")
                m_bluePrintsView.ApplySort(this.sortStatement.Text);
        }

        private void IntegerTextboxValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                e.Cancel = false;
            else
                e.Cancel = !UInt32.TryParse(box.Text, out UInt32 i);
        }

        private void KeptRateLessThanValue_Validated(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                this.keptRateUpbound = 500;
            else
                this.keptRateUpbound = UInt32.Parse(box.Text);
        }

        private void KeptRateGreateThanValue_Validated(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                this.keptRateLowbound = 0;
            else
                this.keptRateLowbound = UInt32.Parse(box.Text);
        }

        private void SeenGreaterThanValue_Validated(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                this.seenGreaterThan = 0;
            else
                this.seenGreaterThan = UInt32.Parse(box.Text);
        }

        private void KeptGreaterThanValue_Validated(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                this.keptGreaterThan = 0;
            else
                this.keptGreaterThan = UInt32.Parse(box.Text);
        }

        private void TypeFilter_Validated(object sender, EventArgs e)
        {
            this.hasTypeFilter = ((TextBox)sender).Text != "";
        }

        private void LayoutFilter_Validated(object sender, EventArgs e)
        {
            this.hasLayoutFilter = ((TextBox)sender).Text != "";
        }

        private void SourceFilter_Validated(object sender, EventArgs e)
        {
            this.hasSourceFilter = ((TextBox)sender).Text != "";
        }

        private void CropFilter_Validated(object sender, EventArgs e)
        {
            this.hasCropFilter = ((TextBox)sender).Text != "";
        }

        private void SeenLessThanValue_Validated(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                this.seenLessThan = UInt32.MaxValue;
            else
                this.seenLessThan = UInt32.Parse(box.Text);
        }

        private void KeptLessThanValue_Validated(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "")
                this.keptLessThan = UInt32.MaxValue;
            else
                this.keptLessThan = UInt32.Parse(box.Text);
        }

        private void BlueprintPreviewPictureBox_DoubleClick(object sender, EventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            Catalog.OpenBP(bpWrapper.Object);
        }

        private void ExamineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            Catalog.OpenBP(bpWrapper.Object);
        }

        private void ApplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            Catalog.DuplicateBPToActivePresentation(bpWrapper.Object);
        }

        private void CatalogGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            Catalog.OpenBP(bpWrapper.Object);
        }

        private void PopulateFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            var bp = bpWrapper.Object;
            this.typeFilter.Text = bp.Type;
            this.hasTypeFilter = true;
            this.sourceFilter.Text = bp.Source;
            this.hasSourceFilter = true;
            this.cropFilter.Text = bp.CropNonCrop;
            this.hasCropFilter = true;
            this.layoutFilter.Text = bp.Layout;
            this.hasLayoutFilter = true;
        }
    }
}
