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
            this.CatalogGridView.Size = new System.Drawing.Size(542, 889);
            this.CatalogGridView.TabIndex = 0;
            this.CatalogGridView.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.CatalogGridView_RowStateChanged);
            // 
            // blueprintPreviewPictureBox
            // 
            this.blueprintPreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintPreviewPictureBox.Location = new System.Drawing.Point(0, 0);
            this.blueprintPreviewPictureBox.Name = "blueprintPreviewPictureBox";
            this.blueprintPreviewPictureBox.Size = new System.Drawing.Size(1082, 889);
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
            this.leftRightSplitterContainer.Size = new System.Drawing.Size(1628, 889);
            this.leftRightSplitterContainer.SplitterDistance = 542;
            this.leftRightSplitterContainer.TabIndex = 3;
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.queryBox);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1628, 75);
            this.searchPanel.TabIndex = 4;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(31, 12);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(148, 51);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // queryBox
            // 
            this.queryBox.Location = new System.Drawing.Point(212, 22);
            this.queryBox.Name = "queryBox";
            this.queryBox.Size = new System.Drawing.Size(1376, 31);
            this.queryBox.TabIndex = 0;
            // 
            // topBottomSplitterContainer
            // 
            this.topBottomSplitterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topBottomSplitterContainer.IsSplitterFixed = true;
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
            this.topBottomSplitterContainer.Size = new System.Drawing.Size(1628, 968);
            this.topBottomSplitterContainer.SplitterDistance = 75;
            this.topBottomSplitterContainer.TabIndex = 5;
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1628, 968);
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
    }
}