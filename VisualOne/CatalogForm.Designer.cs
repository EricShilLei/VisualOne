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
            this.components = new System.ComponentModel.Container();
            this.CatalogGridView = new System.Windows.Forms.DataGridView();
            this.catalogGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populateFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueprintPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.leftRightSplitterContainer = new System.Windows.Forms.SplitContainer();
            this.previewTabControl = new System.Windows.Forms.TabControl();
            this.bluePrintTab = new System.Windows.Forms.TabPage();
            this.originalLayoutPage = new System.Windows.Forms.TabPage();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.renderedRootTextbox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.sourceRootTextbox = new System.Windows.Forms.TextBox();
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
            this.sourceFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRendered = new System.Windows.Forms.ToolStripStatusLabel();
            this.resetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CatalogGridView)).BeginInit();
            this.catalogGridContextMenuStrip.SuspendLayout();
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
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CatalogGridView
            // 
            this.CatalogGridView.AllowUserToAddRows = false;
            this.CatalogGridView.AllowUserToDeleteRows = false;
            this.CatalogGridView.AllowUserToResizeRows = false;
            this.CatalogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CatalogGridView.ContextMenuStrip = this.catalogGridContextMenuStrip;
            this.CatalogGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CatalogGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.CatalogGridView.Location = new System.Drawing.Point(0, 0);
            this.CatalogGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 20);
            this.CatalogGridView.MultiSelect = false;
            this.CatalogGridView.Name = "CatalogGridView";
            this.CatalogGridView.ReadOnly = true;
            this.CatalogGridView.RowHeadersVisible = false;
            this.CatalogGridView.RowTemplate.Height = 33;
            this.CatalogGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CatalogGridView.Size = new System.Drawing.Size(683, 1121);
            this.CatalogGridView.TabIndex = 0;
            // 
            // catalogGridContextMenuStrip
            // 
            this.catalogGridContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.catalogGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.applyToolStripMenuItem,
            this.populateFiltersToolStripMenuItem});
            this.catalogGridContextMenuStrip.Name = "catalogGridContextMenuStrip";
            this.catalogGridContextMenuStrip.Size = new System.Drawing.Size(205, 94);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(204, 30);
            this.editToolStripMenuItem.Text = "Examine";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.ExamineToolStripMenuItem_Click);
            // 
            // applyToolStripMenuItem
            // 
            this.applyToolStripMenuItem.Name = "applyToolStripMenuItem";
            this.applyToolStripMenuItem.Size = new System.Drawing.Size(204, 30);
            this.applyToolStripMenuItem.Text = "Apply";
            this.applyToolStripMenuItem.Click += new System.EventHandler(this.ApplyToolStripMenuItem_Click);
            // 
            // populateFiltersToolStripMenuItem
            // 
            this.populateFiltersToolStripMenuItem.Name = "populateFiltersToolStripMenuItem";
            this.populateFiltersToolStripMenuItem.Size = new System.Drawing.Size(204, 30);
            this.populateFiltersToolStripMenuItem.Text = "Populate Filters";
            this.populateFiltersToolStripMenuItem.Click += new System.EventHandler(this.PopulateFiltersToolStripMenuItem_Click);
            // 
            // blueprintPreviewPictureBox
            // 
            this.blueprintPreviewPictureBox.BackColor = System.Drawing.Color.LightGray;
            this.blueprintPreviewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.blueprintPreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintPreviewPictureBox.Location = new System.Drawing.Point(2, 2);
            this.blueprintPreviewPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.blueprintPreviewPictureBox.Name = "blueprintPreviewPictureBox";
            this.blueprintPreviewPictureBox.Size = new System.Drawing.Size(1357, 1084);
            this.blueprintPreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.blueprintPreviewPictureBox.TabIndex = 2;
            this.blueprintPreviewPictureBox.TabStop = false;
            this.blueprintPreviewPictureBox.DoubleClick += new System.EventHandler(this.BlueprintPreviewPictureBox_DoubleClick);
            // 
            // leftRightSplitterContainer
            // 
            this.leftRightSplitterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftRightSplitterContainer.Location = new System.Drawing.Point(0, 0);
            this.leftRightSplitterContainer.Margin = new System.Windows.Forms.Padding(2);
            this.leftRightSplitterContainer.Name = "leftRightSplitterContainer";
            // 
            // leftRightSplitterContainer.Panel1
            // 
            this.leftRightSplitterContainer.Panel1.Controls.Add(this.CatalogGridView);
            // 
            // leftRightSplitterContainer.Panel2
            // 
            this.leftRightSplitterContainer.Panel2.Controls.Add(this.previewTabControl);
            this.leftRightSplitterContainer.Size = new System.Drawing.Size(2055, 1121);
            this.leftRightSplitterContainer.SplitterDistance = 683;
            this.leftRightSplitterContainer.SplitterWidth = 3;
            this.leftRightSplitterContainer.TabIndex = 3;
            // 
            // previewTabControl
            // 
            this.previewTabControl.Controls.Add(this.bluePrintTab);
            this.previewTabControl.Controls.Add(this.originalLayoutPage);
            this.previewTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewTabControl.Location = new System.Drawing.Point(0, 0);
            this.previewTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.previewTabControl.Name = "previewTabControl";
            this.previewTabControl.SelectedIndex = 0;
            this.previewTabControl.Size = new System.Drawing.Size(1369, 1121);
            this.previewTabControl.TabIndex = 3;
            // 
            // bluePrintTab
            // 
            this.bluePrintTab.Controls.Add(this.blueprintPreviewPictureBox);
            this.bluePrintTab.Location = new System.Drawing.Point(4, 29);
            this.bluePrintTab.Margin = new System.Windows.Forms.Padding(2);
            this.bluePrintTab.Name = "bluePrintTab";
            this.bluePrintTab.Padding = new System.Windows.Forms.Padding(2);
            this.bluePrintTab.Size = new System.Drawing.Size(1361, 1088);
            this.bluePrintTab.TabIndex = 0;
            this.bluePrintTab.Text = "blue print";
            this.bluePrintTab.UseVisualStyleBackColor = true;
            // 
            // originalLayoutPage
            // 
            this.originalLayoutPage.Controls.Add(this.originalPictureBox);
            this.originalLayoutPage.Location = new System.Drawing.Point(4, 29);
            this.originalLayoutPage.Margin = new System.Windows.Forms.Padding(2);
            this.originalLayoutPage.Name = "originalLayoutPage";
            this.originalLayoutPage.Padding = new System.Windows.Forms.Padding(2);
            this.originalLayoutPage.Size = new System.Drawing.Size(1361, 1088);
            this.originalLayoutPage.TabIndex = 1;
            this.originalLayoutPage.Text = "original";
            this.originalLayoutPage.UseVisualStyleBackColor = true;
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.BackColor = System.Drawing.Color.LightGray;
            this.originalPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.originalPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalPictureBox.Location = new System.Drawing.Point(2, 2);
            this.originalPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(1357, 1084);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalPictureBox.TabIndex = 3;
            this.originalPictureBox.TabStop = false;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.searchPanel.Controls.Add(this.label13);
            this.searchPanel.Controls.Add(this.renderedRootTextbox);
            this.searchPanel.Controls.Add(this.label14);
            this.searchPanel.Controls.Add(this.sourceRootTextbox);
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
            this.searchPanel.Controls.Add(this.label5);
            this.searchPanel.Controls.Add(this.sourceFilter);
            this.searchPanel.Controls.Add(this.label3);
            this.searchPanel.Controls.Add(this.cropFilter);
            this.searchPanel.Controls.Add(this.label2);
            this.searchPanel.Controls.Add(this.layoutFilter);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Controls.Add(this.typeFilter);
            this.searchPanel.Controls.Add(this.resetButton);
            this.searchPanel.Controls.Add(this.filterButton);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.queryBox);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(2);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(2055, 207);
            this.searchPanel.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1301, 52);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 20);
            this.label13.TabIndex = 37;
            this.label13.Text = "rendered";
            // 
            // renderedRootTextbox
            // 
            this.renderedRootTextbox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.renderedRootTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.renderedRootTextbox.Enabled = false;
            this.renderedRootTextbox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.renderedRootTextbox.Location = new System.Drawing.Point(1387, 53);
            this.renderedRootTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.renderedRootTextbox.Name = "renderedRootTextbox";
            this.renderedRootTextbox.Size = new System.Drawing.Size(442, 19);
            this.renderedRootTextbox.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1318, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 20);
            this.label14.TabIndex = 35;
            this.label14.Text = "source";
            // 
            // sourceRootTextbox
            // 
            this.sourceRootTextbox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.sourceRootTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sourceRootTextbox.Enabled = false;
            this.sourceRootTextbox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.sourceRootTextbox.Location = new System.Drawing.Point(1387, 18);
            this.sourceRootTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.sourceRootTextbox.Name = "sourceRootTextbox";
            this.sourceRootTextbox.Size = new System.Drawing.Size(442, 19);
            this.sourceRootTextbox.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(610, 53);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 20);
            this.label12.TabIndex = 33;
            this.label12.Text = "kept <";
            // 
            // keptLessThanValue
            // 
            this.keptLessThanValue.Location = new System.Drawing.Point(667, 50);
            this.keptLessThanValue.Margin = new System.Windows.Forms.Padding(2);
            this.keptLessThanValue.Name = "keptLessThanValue";
            this.keptLessThanValue.Size = new System.Drawing.Size(121, 26);
            this.keptLessThanValue.TabIndex = 32;
            this.keptLessThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptLessThanValue.Validated += new System.EventHandler(this.KeptLessThanValue_Validated);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(795, 21);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 20);
            this.label11.TabIndex = 31;
            this.label11.Text = "seen >=";
            // 
            // seenGreatThanValue
            // 
            this.seenGreatThanValue.Location = new System.Drawing.Point(865, 18);
            this.seenGreatThanValue.Margin = new System.Windows.Forms.Padding(2);
            this.seenGreatThanValue.Name = "seenGreatThanValue";
            this.seenGreatThanValue.Size = new System.Drawing.Size(121, 26);
            this.seenGreatThanValue.TabIndex = 30;
            this.seenGreatThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.seenGreatThanValue.Validated += new System.EventHandler(this.SeenGreaterThanValue_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1312, 133);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 29;
            this.label9.Text = "in filter:";
            // 
            // inFilterBluePrintsCount
            // 
            this.inFilterBluePrintsCount.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.inFilterBluePrintsCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inFilterBluePrintsCount.Enabled = false;
            this.inFilterBluePrintsCount.ForeColor = System.Drawing.SystemColors.Highlight;
            this.inFilterBluePrintsCount.Location = new System.Drawing.Point(1387, 133);
            this.inFilterBluePrintsCount.Margin = new System.Windows.Forms.Padding(2);
            this.inFilterBluePrintsCount.Name = "inFilterBluePrintsCount";
            this.inFilterBluePrintsCount.Size = new System.Drawing.Size(150, 19);
            this.inFilterBluePrintsCount.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1348, 98);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 20);
            this.label10.TabIndex = 27;
            this.label10.Text = "all:";
            // 
            // totalBluePrintsCount
            // 
            this.totalBluePrintsCount.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.totalBluePrintsCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalBluePrintsCount.Enabled = false;
            this.totalBluePrintsCount.ForeColor = System.Drawing.SystemColors.Highlight;
            this.totalBluePrintsCount.Location = new System.Drawing.Point(1387, 98);
            this.totalBluePrintsCount.Margin = new System.Windows.Forms.Padding(2);
            this.totalBluePrintsCount.Name = "totalBluePrintsCount";
            this.totalBluePrintsCount.Size = new System.Drawing.Size(150, 19);
            this.totalBluePrintsCount.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(602, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "kept >=";
            // 
            // keptGreaterThanValue
            // 
            this.keptGreaterThanValue.Location = new System.Drawing.Point(667, 16);
            this.keptGreaterThanValue.Margin = new System.Windows.Forms.Padding(2);
            this.keptGreaterThanValue.Name = "keptGreaterThanValue";
            this.keptGreaterThanValue.Size = new System.Drawing.Size(121, 26);
            this.keptGreaterThanValue.TabIndex = 24;
            this.keptGreaterThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptGreaterThanValue.Validated += new System.EventHandler(this.KeptGreaterThanValue_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1002, 54);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "keptRate <";
            // 
            // keptRateLessThanValue
            // 
            this.keptRateLessThanValue.Location = new System.Drawing.Point(1093, 50);
            this.keptRateLessThanValue.Margin = new System.Windows.Forms.Padding(2);
            this.keptRateLessThanValue.Name = "keptRateLessThanValue";
            this.keptRateLessThanValue.Size = new System.Drawing.Size(121, 26);
            this.keptRateLessThanValue.TabIndex = 22;
            this.keptRateLessThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptRateLessThanValue.Validated += new System.EventHandler(this.KeptRateLessThanValue_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(994, 18);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "keptRate >=";
            // 
            // keptRateGreaterThanValue
            // 
            this.keptRateGreaterThanValue.Location = new System.Drawing.Point(1093, 16);
            this.keptRateGreaterThanValue.Margin = new System.Windows.Forms.Padding(2);
            this.keptRateGreaterThanValue.Name = "keptRateGreaterThanValue";
            this.keptRateGreaterThanValue.Size = new System.Drawing.Size(121, 26);
            this.keptRateGreaterThanValue.TabIndex = 20;
            this.keptRateGreaterThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.keptRateGreaterThanValue.Validated += new System.EventHandler(this.KeptRateGreateThanValue_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(803, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "seen <";
            // 
            // seenLessThanValue
            // 
            this.seenLessThanValue.Location = new System.Drawing.Point(865, 50);
            this.seenLessThanValue.Margin = new System.Windows.Forms.Padding(2);
            this.seenLessThanValue.Name = "seenLessThanValue";
            this.seenLessThanValue.Size = new System.Drawing.Size(121, 26);
            this.seenLessThanValue.TabIndex = 18;
            this.seenLessThanValue.Validating += new System.ComponentModel.CancelEventHandler(this.IntegerTextboxValue_Validating);
            this.seenLessThanValue.Validated += new System.EventHandler(this.SeenLessThanValue_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(404, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "source";
            // 
            // sourceFilter
            // 
            this.sourceFilter.Location = new System.Drawing.Point(466, 50);
            this.sourceFilter.Margin = new System.Windows.Forms.Padding(2);
            this.sourceFilter.Name = "sourceFilter";
            this.sourceFilter.Size = new System.Drawing.Size(121, 26);
            this.sourceFilter.TabIndex = 12;
            this.sourceFilter.Validated += new System.EventHandler(this.SourceFilter_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "crop";
            // 
            // cropFilter
            // 
            this.cropFilter.Location = new System.Drawing.Point(466, 16);
            this.cropFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cropFilter.Name = "cropFilter";
            this.cropFilter.Size = new System.Drawing.Size(121, 26);
            this.cropFilter.TabIndex = 8;
            this.cropFilter.Validated += new System.EventHandler(this.CropFilter_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "layout";
            // 
            // layoutFilter
            // 
            this.layoutFilter.Location = new System.Drawing.Point(268, 50);
            this.layoutFilter.Margin = new System.Windows.Forms.Padding(2);
            this.layoutFilter.Name = "layoutFilter";
            this.layoutFilter.Size = new System.Drawing.Size(121, 26);
            this.layoutFilter.TabIndex = 6;
            this.layoutFilter.Validated += new System.EventHandler(this.LayoutFilter_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "type";
            // 
            // typeFilter
            // 
            this.typeFilter.Location = new System.Drawing.Point(268, 16);
            this.typeFilter.Margin = new System.Windows.Forms.Padding(2);
            this.typeFilter.Name = "typeFilter";
            this.typeFilter.Size = new System.Drawing.Size(121, 26);
            this.typeFilter.TabIndex = 4;
            this.typeFilter.Validated += new System.EventHandler(this.TypeFilter_Validated);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(23, 20);
            this.filterButton.Margin = new System.Windows.Forms.Padding(2);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(154, 44);
            this.filterButton.TabIndex = 2;
            this.filterButton.Text = "Filter";
            this.filterButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(23, 112);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(154, 41);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search by guid";
            this.searchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // queryBox
            // 
            this.queryBox.Location = new System.Drawing.Point(268, 120);
            this.queryBox.Margin = new System.Windows.Forms.Padding(2);
            this.queryBox.Name = "queryBox";
            this.queryBox.Size = new System.Drawing.Size(946, 26);
            this.queryBox.TabIndex = 0;
            // 
            // topBottomSplitterContainer
            // 
            this.topBottomSplitterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topBottomSplitterContainer.Location = new System.Drawing.Point(0, 0);
            this.topBottomSplitterContainer.Margin = new System.Windows.Forms.Padding(2);
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
            this.topBottomSplitterContainer.Size = new System.Drawing.Size(2055, 1331);
            this.topBottomSplitterContainer.SplitterDistance = 207;
            this.topBottomSplitterContainer.SplitterWidth = 3;
            this.topBottomSplitterContainer.TabIndex = 5;
            // 
            // sourceFolderBrowserDialog
            // 
            this.sourceFolderBrowserDialog.Description = "Pick the folder that contains the blueprints";
            this.sourceFolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelSource,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabelRendered});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1301);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(2055, 30);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 25);
            this.toolStripStatusLabel1.Text = "Source:";
            // 
            // toolStripStatusLabelSource
            // 
            this.toolStripStatusLabelSource.Name = "toolStripStatusLabelSource";
            this.toolStripStatusLabelSource.Size = new System.Drawing.Size(179, 25);
            this.toolStripStatusLabelSource.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(146, 25);
            this.toolStripStatusLabel3.Text = "           Rendered:";
            // 
            // toolStripStatusLabelRendered
            // 
            this.toolStripStatusLabelRendered.Name = "toolStripStatusLabelRendered";
            this.toolStripStatusLabelRendered.Size = new System.Drawing.Size(179, 25);
            this.toolStripStatusLabelRendered.Text = "toolStripStatusLabel4";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(23, 66);
            this.resetButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(154, 44);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2055, 1331);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.topBottomSplitterContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CatalogForm";
            this.Text = "CatalogForm";
            ((System.ComponentModel.ISupportInitialize)(this.CatalogGridView)).EndInit();
            this.catalogGridContextMenuStrip.ResumeLayout(false);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.FolderBrowserDialog sourceFolderBrowserDialog;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox renderedRootTextbox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox sourceRootTextbox;
        private System.Windows.Forms.ContextMenuStrip catalogGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSource;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRendered;
        private System.Windows.Forms.ToolStripMenuItem applyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem populateFiltersToolStripMenuItem;
        private System.Windows.Forms.Button resetButton;
    }
}