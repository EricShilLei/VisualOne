namespace VisualOne
{
    partial class CatalogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CatalogGridView = new System.Windows.Forms.DataGridView();
            this.blueprintPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.leftRightSplitterContainer = new System.Windows.Forms.SplitContainer();
            this.previewTabControl = new System.Windows.Forms.TabControl();
            this.bluePrintTab = new System.Windows.Forms.TabPage();
            this.originalLayoutPage = new System.Windows.Forms.TabPage();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.keptLessThanValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.seenGreatThanValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.inFilterBluePrintsCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.totalBluePrintsCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.keptGreaterThanValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.keptRateLessThanValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.keptRateGreaterThanValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.seenLessThanValue = new System.Windows.Forms.TextBox();
            this.sortStatement = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.sourceFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cropFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.layoutFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.typeFilter = new System.Windows.Forms.TextBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.queryBox = new System.Windows.Forms.TextBox();
            this.topBottomSplitterContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.CatalogGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueprintPreviewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitterContainer)).BeginInit();
            this.leftRightSplitterContainer.Panel1.SuspendLayout();
            this.leftRightSplitterContainer.Panel2.SuspendLayout();
            this.leftRightSplitterContainer.SuspendLayout();
            this.previewTabControl.SuspendLayout();
            this.bluePrintTab.SuspendLayout();
            this.originalLayoutPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topBottomSplitterContainer)).BeginInit();
            this.topBottomSplitterContainer.Panel1.SuspendLayout();
            this.topBottomSplitterContainer.Panel2.SuspendLayout();
            this.topBottomSplitterContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // CatalogGridView
            // 
            this.CatalogGridView.AllowUserToAddRows = false;
            this.CatalogGridView.AllowUserToDeleteRows = false;
            this.CatalogGridView.AllowUserToResizeRows = false;
            this.CatalogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CatalogGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatalogGridView.Location = new System.Drawing.Point(0, 0);
            this.CatalogGridView.MultiSelect = false;
            this.CatalogGridView.Name = "CatalogGridView";
            this.CatalogGridView.ReadOnly = true;
            this.CatalogGridView.RowHeadersVisible = false;
            this.CatalogGridView.RowTemplate.Height = 33;
            this.CatalogGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CatalogGridView.Size = new System.Drawing.Size(780, 1400);
            this.CatalogGridView.TabIndex = 0;
            this.CatalogGridView.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.CatalogGridView_RowStateChanged);
            // 
            // blueprintPreviewPictureBox
            // 
            this.blueprintPreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintPreviewPictureBox.Location = new System.Drawing.Point(3, 3);
            this.blueprintPreviewPictureBox.Name = "blueprintPreviewPictureBox";
            this.blueprintPreviewPictureBox.Size = new System.Drawing.Size(1538, 1347);
            this.blueprintPreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.blueprintPreviewPictureBox.TabIndex = 2;
            this.blueprintPreviewPictureBox.TabStop = false;
            // 
            // leftRightSplitterContainer
            // 
            this.leftRightSplitterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftRightSplitterContainer.Location = new System.Drawing.Point(0, 0);
            this.leftRightSplitterContainer.Name = "leftRightSplitterContainer";
            // 
            // leftRightSplitterContainer.Panel1
            // 
            this.leftRightSplitterContainer.Panel1.Controls.Add(this.CatalogGridView);
            // 
            // leftRightSplitterContainer.Panel2
            // 
            this.leftRightSplitterContainer.Panel2.Controls.Add(this.previewTabControl);
            this.leftRightSplitterContainer.Size = new System.Drawing.Size(2344, 1400);
            this.leftRightSplitterContainer.SplitterDistance = 780;
            this.leftRightSplitterContainer.TabIndex = 3;
            // 
            // previewTabControl
            // 
            this.previewTabControl.Controls.Add(this.bluePrintTab);
            this.previewTabControl.Controls.Add(this.originalLayoutPage);
            this.previewTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewTabControl.Location = new System.Drawing.Point(0, 0);
            this.previewTabControl.Name = "previewTabControl";
            this.previewTabControl.SelectedIndex = 0;
            this.previewTabControl.Size = new System.Drawing.Size(1560, 1400);
            this.previewTabControl.TabIndex = 3;
            // 
            // bluePrintTab
            // 
            this.bluePrintTab.Controls.Add(this.blueprintPreviewPictureBox);
            this.bluePrintTab.Location = new System.Drawing.Point(8, 39);
            this.bluePrintTab.Name = "bluePrintTab";
            this.bluePrintTab.Padding = new System.Windows.Forms.Padding(3);
            this.bluePrintTab.Size = new System.Drawing.Size(1544, 1353);
            this.bluePrintTab.TabIndex = 0;
            this.bluePrintTab.Text = "blue print";
            this.bluePrintTab.UseVisualStyleBackColor = true;
            // 
            // originalLayoutPage
            // 
            this.originalLayoutPage.Controls.Add(this.originalPictureBox);
            this.originalLayoutPage.Location = new System.Drawing.Point(8, 39);
            this.originalLayoutPage.Name = "originalLayoutPage";
            this.originalLayoutPage.Padding = new System.Windows.Forms.Padding(3);
            this.originalLayoutPage.Size = new System.Drawing.Size(1544, 1353);
            this.originalLayoutPage.TabIndex = 1;
            this.originalLayoutPage.Text = "original";
            this.originalLayoutPage.UseVisualStyleBackColor = true;
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalPictureBox.Location = new System.Drawing.Point(3, 3);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(1538, 1347);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalPictureBox.TabIndex = 3;
            this.originalPictureBox.TabStop = false;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.searchPanel.Controls.Add(this.label12);
            this.searchPanel.Controls.Add(this.keptLessThanValue);
            this.searchPanel.Controls.Add(this.label11);
            this.searchPanel.Controls.Add(this.seenGreatThanValue);
            this.searchPanel.Controls.Add(this.label9);
            this.searchPanel.Controls.Add(this.inFilterBluePrintsCount);
            this.searchPanel.Controls.Add(this.label10);
            this.searchPanel.Controls.Add(this.totalBluePrintsCount);
            this.searchPanel.Controls.Add(this.label4);
            this.searchPanel.Controls.Add(this.keptGreaterThanValue);
            this.searchPanel.Controls.Add(this.label8);
            this.searchPanel.Controls.Add(this.keptRateLessThanValue);
            this.searchPanel.Controls.Add(this.label7);
            this.searchPanel.Controls.Add(this.keptRateGreaterThanValue);
            this.searchPanel.Controls.Add(this.label6);
            this.searchPanel.Controls.Add(this.seenLessThanValue);
            this.searchPanel.Controls.Add(this.sortStatement);
            this.searchPanel.Controls.Add(this.button1);
            this.searchPanel.Controls.Add(this.label5);
            this.searchPanel.Controls.Add(this.sourceFilter);
            this.searchPanel.Controls.Add(this.label3);
            this.searchPanel.Controls.Add(this.cropFilter);
            this.searchPanel.Controls.Add(this.label2);
            this.searchPanel.Controls.Add(this.layoutFilter);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Controls.Add(this.typeFilter);
            this.searchPanel.Controls.Add(this.filterButton);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.queryBox);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(2344, 260);
            this.searchPanel.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(813, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 25);
            this.label12.TabIndex = 33;
            this.label12.Text = "kept <";
            // 
            // keptLessThanValue
            // 
            this.keptLessThanValue.Location = new System.Drawing.Point(889, 63);
            this.keptLessThanValue.Name = "keptLessThanValue";
            this.keptLessThanValue.Size = new System.Drawing.Size(160, 31);
            this.keptLessThanValue.TabIndex = 32;
            this.keptLessThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptLessThanValue.Validated += new System.EventHandler(this.KeptLessThanValue_Validated);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1060, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 25);
            this.label11.TabIndex = 31;
            this.label11.Text = "seen >=";
            // 
            // seenGreatThanValue
            // 
            this.seenGreatThanValue.Location = new System.Drawing.Point(1153, 22);
            this.seenGreatThanValue.Name = "seenGreatThanValue";
            this.seenGreatThanValue.Size = new System.Drawing.Size(160, 31);
            this.seenGreatThanValue.TabIndex = 30;
            this.seenGreatThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.seenGreatThanValue.Validated += new System.EventHandler(this.SeenGreaterThanValue_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1744, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 25);
            this.label9.TabIndex = 29;
            this.label9.Text = "in filter:";
            // 
            // inFilterBluePrintsCount
            // 
            this.inFilterBluePrintsCount.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.inFilterBluePrintsCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inFilterBluePrintsCount.Enabled = false;
            this.inFilterBluePrintsCount.Location = new System.Drawing.Point(1844, 63);
            this.inFilterBluePrintsCount.Name = "inFilterBluePrintsCount";
            this.inFilterBluePrintsCount.Size = new System.Drawing.Size(200, 24);
            this.inFilterBluePrintsCount.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1771, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 25);
            this.label10.TabIndex = 27;
            this.label10.Text = "total:";
            // 
            // totalBluePrintsCount
            // 
            this.totalBluePrintsCount.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.totalBluePrintsCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalBluePrintsCount.Enabled = false;
            this.totalBluePrintsCount.Location = new System.Drawing.Point(1844, 20);
            this.totalBluePrintsCount.Name = "totalBluePrintsCount";
            this.totalBluePrintsCount.Size = new System.Drawing.Size(200, 24);
            this.totalBluePrintsCount.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(802, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "kept >=";
            // 
            // keptGreaterThanValue
            // 
            this.keptGreaterThanValue.Location = new System.Drawing.Point(889, 20);
            this.keptGreaterThanValue.Name = "keptGreaterThanValue";
            this.keptGreaterThanValue.Size = new System.Drawing.Size(160, 31);
            this.keptGreaterThanValue.TabIndex = 24;
            this.keptGreaterThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptGreaterThanValue.Validated += new System.EventHandler(this.KeptGreaterThanValue_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1336, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 25);
            this.label8.TabIndex = 23;
            this.label8.Text = "keptRate <";
            // 
            // keptRateLessThanValue
            // 
            this.keptRateLessThanValue.Location = new System.Drawing.Point(1457, 63);
            this.keptRateLessThanValue.Name = "keptRateLessThanValue";
            this.keptRateLessThanValue.Size = new System.Drawing.Size(160, 31);
            this.keptRateLessThanValue.TabIndex = 22;
            this.keptRateLessThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptRateLessThanValue.Validated += new System.EventHandler(this.KeptRateLessThanValue_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1325, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "keptRate >=";
            // 
            // keptRateGreaterThanValue
            // 
            this.keptRateGreaterThanValue.Location = new System.Drawing.Point(1457, 20);
            this.keptRateGreaterThanValue.Name = "keptRateGreaterThanValue";
            this.keptRateGreaterThanValue.Size = new System.Drawing.Size(160, 31);
            this.keptRateGreaterThanValue.TabIndex = 20;
            this.keptRateGreaterThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptRateGreaterThanValue.Validated += new System.EventHandler(this.KeptRateGreateThanValue_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1071, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 25);
            this.label6.TabIndex = 19;
            this.label6.Text = "seen <";
            // 
            // seenLessThanValue
            // 
            this.seenLessThanValue.Location = new System.Drawing.Point(1153, 63);
            this.seenLessThanValue.Name = "seenLessThanValue";
            this.seenLessThanValue.Size = new System.Drawing.Size(160, 31);
            this.seenLessThanValue.TabIndex = 18;
            this.seenLessThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.seenLessThanValue.Validated += new System.EventHandler(this.SeenLessThanValue_Validated);
            // 
            // sortStatement
            // 
            this.sortStatement.Location = new System.Drawing.Point(357, 119);
            this.sortStatement.Name = "sortStatement";
            this.sortStatement.Size = new System.Drawing.Size(1260, 31);
            this.sortStatement.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 51);
            this.button1.TabIndex = 16;
            this.button1.Text = "Sort";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(539, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "source";
            // 
            // sourceFilter
            // 
            this.sourceFilter.Location = new System.Drawing.Point(622, 63);
            this.sourceFilter.Name = "sourceFilter";
            this.sourceFilter.Size = new System.Drawing.Size(160, 31);
            this.sourceFilter.TabIndex = 12;
            this.sourceFilter.Validated += new System.EventHandler(this.SourceFilter_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(562, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "crop";
            // 
            // cropFilter
            // 
            this.cropFilter.Location = new System.Drawing.Point(622, 20);
            this.cropFilter.Name = "cropFilter";
            this.cropFilter.Size = new System.Drawing.Size(160, 31);
            this.cropFilter.TabIndex = 8;
            this.cropFilter.Validated += new System.EventHandler(this.CropFilter_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "layout";
            // 
            // layoutFilter
            // 
            this.layoutFilter.Location = new System.Drawing.Point(357, 63);
            this.layoutFilter.Name = "layoutFilter";
            this.layoutFilter.Size = new System.Drawing.Size(160, 31);
            this.layoutFilter.TabIndex = 6;
            this.layoutFilter.Validated += new System.EventHandler(this.LayoutFilter_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "type";
            // 
            // typeFilter
            // 
            this.typeFilter.Location = new System.Drawing.Point(357, 20);
            this.typeFilter.Name = "typeFilter";
            this.typeFilter.Size = new System.Drawing.Size(160, 31);
            this.typeFilter.TabIndex = 4;
            this.typeFilter.Validated += new System.EventHandler(this.TypeFilter_Validated);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(31, 25);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(205, 55);
            this.filterButton.TabIndex = 2;
            this.filterButton.Text = "Filter";
            this.filterButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(31, 166);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(205, 51);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search by guid";
            this.searchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // queryBox
            // 
            this.queryBox.Location = new System.Drawing.Point(357, 176);
            this.queryBox.Name = "queryBox";
            this.queryBox.Size = new System.Drawing.Size(1260, 31);
            this.queryBox.TabIndex = 0;
            // 
            // topBottomSplitterContainer
            // 
            this.topBottomSplitterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topBottomSplitterContainer.Location = new System.Drawing.Point(0, 0);
            this.topBottomSplitterContainer.Name = "topBottomSplitterContainer";
            this.topBottomSplitterContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topBottomSplitterContainer.Panel1
            // 
            this.topBottomSplitterContainer.Panel1.Controls.Add(this.searchPanel);
            // 
            // topBottomSplitterContainer.Panel2
            // 
            this.topBottomSplitterContainer.Panel2.Controls.Add(this.leftRightSplitterContainer);
            this.topBottomSplitterContainer.Size = new System.Drawing.Size(2344, 1664);
            this.topBottomSplitterContainer.SplitterDistance = 260;
            this.topBottomSplitterContainer.TabIndex = 5;
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2344, 1664);
            this.Controls.Add(this.topBottomSplitterContainer);
            this.Name = "CatalogForm";
            this.Text = "CatalogForm";
            ((System.ComponentModel.ISupportInitialize)(this.CatalogGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueprintPreviewPictureBox)).EndInit();
            this.leftRightSplitterContainer.Panel1.ResumeLayout(false);
            this.leftRightSplitterContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitterContainer)).EndInit();
            this.leftRightSplitterContainer.ResumeLayout(false);
            this.previewTabControl.ResumeLayout(false);
            this.bluePrintTab.ResumeLayout(false);
            this.originalLayoutPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.topBottomSplitterContainer.Panel1.ResumeLayout(false);
            this.topBottomSplitterContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topBottomSplitterContainer)).EndInit();
            this.topBottomSplitterContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CatalogGridView;
        private System.Windows.Forms.PictureBox blueprintPreviewPictureBox;
        private System.Windows.Forms.SplitContainer leftRightSplitterContainer;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox queryBox;
        private System.Windows.Forms.SplitContainer topBottomSplitterContainer;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.TextBox typeFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cropFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox layoutFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sourceFilter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox sortStatement;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox keptRateGreaterThanValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox seenLessThanValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox keptRateLessThanValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox keptGreaterThanValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox inFilterBluePrintsCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox totalBluePrintsCount;
        private System.Windows.Forms.TabControl previewTabControl;
        private System.Windows.Forms.TabPage bluePrintTab;
        private System.Windows.Forms.TabPage originalLayoutPage;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox keptLessThanValue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox seenGreatThanValue;
    }
}