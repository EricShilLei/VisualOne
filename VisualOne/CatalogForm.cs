using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharedLib;

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

        private BluePrintDataTable m_bluePrintDataTable;
        private BindingSource m_bluePrintBindingSource;
        public CatalogClass Catalog;

        public CatalogForm(CatalogClass catalog)
        {
            InitializeComponent();
            if (catalog == null)
            {
                var folderBrowserResult = this.sourceFolderBrowserDialog.ShowDialog(this);
                string sourceRoot = folderBrowserResult == DialogResult.OK ? this.sourceFolderBrowserDialog.SelectedPath : null;
                this.sourceFolderBrowserDialog.Description = "Pick the rendered thumbnails folder";
                folderBrowserResult = this.sourceFolderBrowserDialog.ShowDialog(this);
                string outputRoot = folderBrowserResult == DialogResult.OK ? this.sourceFolderBrowserDialog.SelectedPath : null;
                Catalog = new CatalogClass(sourceRoot, outputRoot);
            }
            else
            {
                Catalog = catalog;
            }
            Catalog.ReadFlatCatalog();

            this.sourceRootTextbox.Text = Catalog.SourceRoot;
            this.renderedRootTextbox.Text = Catalog.RenderedRoot;

            m_bluePrintDataTable = new BluePrintDataTable();
            m_bluePrintDataTable.AddBlueprints(Catalog.BluePrints);
            this.CatalogGridView.Rows.Clear();
            m_bluePrintBindingSource = new BindingSource();
            m_bluePrintBindingSource.DataSource = m_bluePrintDataTable;
            this.CatalogGridView.DataSource = m_bluePrintBindingSource;
            this.CatalogGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.CatalogGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CatalogGridView_CellMouseDoubleClick);
            this.CatalogGridView.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.CatalogGridView_RowStateChanged);

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
                BluePrintDataRow blueprintDataRow = (BluePrintDataRow)((DataRowView)row.DataBoundItem).Row;
                if (blueprintDataRow.GetBlueprint().Guid == selectedGuid)
                    return row.Index;
            }
            return -1;
        }

        private void CatalogGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (!e.Row.Selected)
                return;
            BluePrintDataRow blueprintRow = (BluePrintDataRow)((DataRowView)e.Row.DataBoundItem).Row;
            string imagePath = blueprintRow.GetBlueprint().PngPath;
            // TODO: runtime render the thumbnail if it is missing
            if (File.Exists(imagePath))
                this.blueprintPreviewPictureBox.Image = System.Drawing.Image.FromFile(imagePath);
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            FilterBy();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.typeFilter.Text = "";
            this.layoutFilter.Text = "";
            this.cropFilter.Text = "";
            this.sourceFilter.Text = "";
            this.keptGreaterThanValue.Text = "";
            this.keptLessThanValue.Text = "";
            this.seenGreatThanValue.Text = "";
            this.seenLessThanValue.Text = "";
            this.keptRateGreaterThanValue.Text = "";
            this.keptRateLessThanValue.Text = "";
            this.queryBox.Text = "";

            m_bluePrintBindingSource.Filter = "";
        }

        private void FilterBy()
        {
            StringBuilder filterString = new StringBuilder();
            filterString.AppendFormat("Seen >= {0} and Seen < {1}", this.seenGreaterThan, this.seenLessThan);
            filterString.AppendFormat(" and Kept >= {0} and Kept < {1}", this.keptGreaterThan, this.keptLessThan);
            filterString.AppendFormat(" and KeptRate >= {0} and KeptRate < {1}", this.keptRateLowbound, this.keptRateUpbound);
            if (this.hasTypeFilter && !this.typeFilter.Text.Contains('\''))
                filterString.AppendFormat(" and Type = '{0}'", this.typeFilter.Text);
            if (this.hasLayoutFilter && !this.layoutFilter.Text.Contains('\''))
                filterString.AppendFormat(" and Layout = '{0}'", this.layoutFilter.Text);
            if (this.hasCropFilter && !this.cropFilter.Text.Contains('\''))
                filterString.AppendFormat(" and Crop = '{0}'", this.cropFilter.Text);

            m_bluePrintBindingSource.Filter = filterString.ToString();
            this.inFilterBluePrintsCount.Text = this.CatalogGridView.Rows.Count.ToString();
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
            BluePrintDataRow blueprintDataRow = (BluePrintDataRow)((DataRowView)this.CatalogGridView.CurrentRow.DataBoundItem).Row;
            BlueprintOperations.OpenBP(blueprintDataRow.GetBlueprint());
        }

        private void ExamineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BluePrintDataRow blueprintDataRow = (BluePrintDataRow)((DataRowView)this.CatalogGridView.CurrentRow.DataBoundItem).Row;
            BlueprintOperations.OpenBP(blueprintDataRow.GetBlueprint());
        }

        private void ApplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BluePrintDataRow blueprintDataRow = (BluePrintDataRow)((DataRowView)this.CatalogGridView.CurrentRow.DataBoundItem).Row;
            BlueprintOperations.DuplicateBPToActivePresentation(blueprintDataRow.GetBlueprint());
        }

        private void CatalogGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BluePrintDataRow blueprintDataRow = (BluePrintDataRow)((DataRowView)this.CatalogGridView.CurrentRow.DataBoundItem).Row;
            BlueprintOperations.OpenBP(blueprintDataRow.GetBlueprint());
        }

        private void PopulateFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BluePrintDataRow blueprintDataRow = (BluePrintDataRow)((DataRowView)this.CatalogGridView.CurrentRow.DataBoundItem).Row;

            var bp = blueprintDataRow.GetBlueprint();
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
