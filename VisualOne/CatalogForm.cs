﻿using Equin.ApplicationFramework;

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
                this.sourceFolderBrowserDialog.ShowDialog(this);
                string sourceRoot = this.sourceFolderBrowserDialog.SelectedPath;
                this.sourceFolderBrowserDialog.Description = "Pick the rendered thumbnails folder";
                this.sourceFolderBrowserDialog.ShowDialog(this);
                string outputRoot = this.sourceFolderBrowserDialog.SelectedPath;
                Catalog = new CatalogClass(sourceRoot, outputRoot);
                Catalog.CreateCatalog();

            }
            else
                Catalog = catalog;
            this.sourceRootTextbox.Text = Catalog.SourceRoot;
            this.renderedRootTextbox.Text = Catalog.RenderedRoot;

            var source = new BindingSource();
            m_bluePrintsView = new BindingListView<BluePrint>(Catalog.m_bluePrints);
            source.DataSource = m_bluePrintsView;
            this.CatalogGridView.AutoGenerateColumns = true;
            this.CatalogGridView.DataSource = source;
            this.totalBluePrintsCount.Text = this.CatalogGridView.Rows.Count.ToString();
            this.inFilterBluePrintsCount.Text = this.CatalogGridView.Rows.Count.ToString();
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
                int lastSlashIndex = imagePath.LastIndexOf('\\');
                string originalPath = imagePath.Remove(lastSlashIndex + 1) + "original.png";
                try
                {
                    this.originalPictureBox.Image = System.Drawing.Image.FromFile(originalPath);
                }
                catch( Exception ex)
                {
                    this.originalPictureBox.Image = null;
                    Console.WriteLine(ex.Message);
                }
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
                return bp.seen >= this.seenGreaterThan &&
                       bp.kept >= this.keptGreaterThan &&
                       bp.seen < this.seenLessThan &&
                       bp.kept < this.keptLessThan &&
                       bp.keptRate < this.keptRateUpbound &&
                       bp.keptRate >= this.keptRateLowbound &&
                       (!this.hasTypeFilter || bp.type == this.typeFilter.Text) &&
                       (!this.hasLayoutFilter || bp.layout == this.layoutFilter.Text) &&
                       (!this.hasCropFilter || bp.cropNonCrop == this.cropFilter.Text) &&
                       (!this.hasSourceFilter || bp.source == this.sourceFilter.Text);
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

        private void DuplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            var thmxSelectorControl = new ThemeSelectorForm(Catalog.m_sourcesFor01_1Photo, Catalog.RenderedRoot);
            thmxSelectorControl.ShowDialog(this);
            Catalog.DuplicateBPTo(bpWrapper.Object, thmxSelectorControl.SelectedTheme);
        }

        private void ExamineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectView<BluePrint> bpWrapper = (ObjectView<BluePrint>)this.CatalogGridView.CurrentRow.DataBoundItem;
            Catalog.OpenBP(bpWrapper.Object);
        }
    }
}
