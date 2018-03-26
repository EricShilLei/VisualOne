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
            this.searchPanel = new System.Windows.Forms.Panel();
            this.sortStatement = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.variantFilter = new System.Windows.Forms.TextBox();
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
            this.blueprintPreviewPictureBox.Location = new System.Drawing.Point(0, 0);
            this.blueprintPreviewPictureBox.Name = "blueprintPreviewPictureBox";
            this.blueprintPreviewPictureBox.Size = new System.Drawing.Size(1560, 1400);
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
            this.leftRightSplitterContainer.Panel2.Controls.Add(this.blueprintPreviewPictureBox);
            this.leftRightSplitterContainer.Size = new System.Drawing.Size(2344, 1400);
            this.leftRightSplitterContainer.SplitterDistance = 780;
            this.leftRightSplitterContainer.TabIndex = 3;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.searchPanel.Controls.Add(this.sortStatement);
            this.searchPanel.Controls.Add(this.button1);
            this.searchPanel.Controls.Add(this.label4);
            this.searchPanel.Controls.Add(this.variantFilter);
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
            // sortStatement
            // 
            this.sortStatement.Location = new System.Drawing.Point(357, 119);
            this.sortStatement.Name = "sortStatement";
            this.sortStatement.Size = new System.Drawing.Size(1335, 31);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(764, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 27);
            this.label4.TabIndex = 15;
            this.label4.Text = "variant";
            // 
            // variantFilter
            // 
            this.variantFilter.Location = new System.Drawing.Point(849, 14);
            this.variantFilter.Name = "variantFilter";
            this.variantFilter.Size = new System.Drawing.Size(341, 31);
            this.variantFilter.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(764, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 27);
            this.label5.TabIndex = 13;
            this.label5.Text = "source";
            // 
            // sourceFilter
            // 
            this.sourceFilter.Location = new System.Drawing.Point(849, 64);
            this.sourceFilter.Name = "sourceFilter";
            this.sourceFilter.Size = new System.Drawing.Size(341, 31);
            this.sourceFilter.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(1289, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 27);
            this.label3.TabIndex = 9;
            this.label3.Text = "crop";
            // 
            // cropFilter
            // 
            this.cropFilter.Location = new System.Drawing.Point(1351, 10);
            this.cropFilter.Name = "cropFilter";
            this.cropFilter.Size = new System.Drawing.Size(341, 31);
            this.cropFilter.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(279, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 27);
            this.label2.TabIndex = 7;
            this.label2.Text = "layout";
            // 
            // layoutFilter
            // 
            this.layoutFilter.Location = new System.Drawing.Point(357, 64);
            this.layoutFilter.Name = "layoutFilter";
            this.layoutFilter.Size = new System.Drawing.Size(341, 31);
            this.layoutFilter.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(296, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "type";
            // 
            // typeFilter
            // 
            this.typeFilter.Location = new System.Drawing.Point(357, 13);
            this.typeFilter.Name = "typeFilter";
            this.typeFilter.Size = new System.Drawing.Size(341, 31);
            this.typeFilter.TabIndex = 4;
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(31, 13);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(205, 51);
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
            this.queryBox.Size = new System.Drawing.Size(1335, 31);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox variantFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sourceFilter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox sortStatement;
    }
}