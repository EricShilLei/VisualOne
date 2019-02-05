namespace VisualOne
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.m_btnSelectFolder = new System.Windows.Forms.Button();
            this.m_txtTargetFolder = new System.Windows.Forms.TextBox();
            this.btnGenerateCatalog = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.m_txtThumbnailFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local Changes:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(31, 46);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(671, 364);
            this.listBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(773, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Validate Local Changes";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(773, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(221, 47);
            this.button2.TabIndex = 3;
            this.button2.Text = "Check In";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnViewAll
            // 
            this.btnViewAll.Location = new System.Drawing.Point(31, 443);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(223, 57);
            this.btnViewAll.TabIndex = 4;
            this.btnViewAll.Text = "View All Blueprints";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.BtnViewAll_Click);
            // 
            // m_btnSelectFolder
            // 
            this.m_btnSelectFolder.Location = new System.Drawing.Point(591, 454);
            this.m_btnSelectFolder.Name = "m_btnSelectFolder";
            this.m_btnSelectFolder.Size = new System.Drawing.Size(115, 34);
            this.m_btnSelectFolder.TabIndex = 5;
            this.m_btnSelectFolder.Text = "BP Folder:";
            this.m_btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // m_txtTargetFolder
            // 
            this.m_txtTargetFolder.Location = new System.Drawing.Point(712, 458);
            this.m_txtTargetFolder.Name = "m_txtTargetFolder";
            this.m_txtTargetFolder.Size = new System.Drawing.Size(282, 26);
            this.m_txtTargetFolder.TabIndex = 6;
            this.m_txtTargetFolder.Text = "D:\\Git\\VisualOne\\TestFiles\\NewActive";
            // 
            // btnGenerateCatalog
            // 
            this.btnGenerateCatalog.Location = new System.Drawing.Point(297, 443);
            this.btnGenerateCatalog.Name = "btnGenerateCatalog";
            this.btnGenerateCatalog.Size = new System.Drawing.Size(121, 57);
            this.btnGenerateCatalog.TabIndex = 7;
            this.btnGenerateCatalog.Text = "ReGenerate Catalog";
            this.btnGenerateCatalog.UseVisualStyleBackColor = true;
            this.btnGenerateCatalog.Click += new System.EventHandler(this.BtnGenerateCatalog_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(591, 505);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 33);
            this.button4.TabIndex = 8;
            this.button4.Text = "Thumbnail:";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // m_txtThumbnailFolder
            // 
            this.m_txtThumbnailFolder.Location = new System.Drawing.Point(712, 508);
            this.m_txtThumbnailFolder.Name = "m_txtThumbnailFolder";
            this.m_txtThumbnailFolder.Size = new System.Drawing.Size(281, 26);
            this.m_txtThumbnailFolder.TabIndex = 9;
            this.m_txtThumbnailFolder.Text = "D:\\Git\\VisualOne\\TestFiles\\Thumbnails";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 582);
            this.Controls.Add(this.m_txtThumbnailFolder);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnGenerateCatalog);
            this.Controls.Add(this.m_txtTargetFolder);
            this.Controls.Add(this.m_btnSelectFolder);
            this.Controls.Add(this.btnViewAll);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button m_btnSelectFolder;
        private System.Windows.Forms.TextBox m_txtTargetFolder;
        private System.Windows.Forms.Button btnGenerateCatalog;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox m_txtThumbnailFolder;
    }
}