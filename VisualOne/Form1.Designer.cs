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
            this.rasterizeButton = new System.Windows.Forms.Button();
            this.catalogButton = new System.Windows.Forms.Button();
            this.previewButton = new System.Windows.Forms.Button();
            this.UpdateBlueprintsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rasterizeButton
            // 
            this.rasterizeButton.Enabled = false;
            this.rasterizeButton.Location = new System.Drawing.Point(566, 259);
            this.rasterizeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rasterizeButton.Name = "rasterizeButton";
            this.rasterizeButton.Size = new System.Drawing.Size(484, 108);
            this.rasterizeButton.TabIndex = 0;
            this.rasterizeButton.Text = "Rasterize BluePrints";
            this.rasterizeButton.UseVisualStyleBackColor = true;
            this.rasterizeButton.Click += new System.EventHandler(this.RasterizeButton_Click);
            // 
            // catalogButton
            // 
            this.catalogButton.Enabled = false;
            this.catalogButton.Location = new System.Drawing.Point(566, 419);
            this.catalogButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.catalogButton.Name = "catalogButton";
            this.catalogButton.Size = new System.Drawing.Size(484, 108);
            this.catalogButton.TabIndex = 1;
            this.catalogButton.Text = "Create Catalog";
            this.catalogButton.UseVisualStyleBackColor = true;
            this.catalogButton.Click += new System.EventHandler(this.CatalogButton_Click);
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(566, 575);
            this.previewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(484, 108);
            this.previewButton.TabIndex = 2;
            this.previewButton.Text = "View Catalog";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // UpdateBlueprintsButton
            // 
            this.UpdateBlueprintsButton.Location = new System.Drawing.Point(566, 98);
            this.UpdateBlueprintsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpdateBlueprintsButton.Name = "UpdateBlueprintsButton";
            this.UpdateBlueprintsButton.Size = new System.Drawing.Size(484, 108);
            this.UpdateBlueprintsButton.TabIndex = 4;
            this.UpdateBlueprintsButton.Text = "Update Blueprints";
            this.UpdateBlueprintsButton.UseVisualStyleBackColor = true;
            this.UpdateBlueprintsButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1686, 845);
            this.Controls.Add(this.UpdateBlueprintsButton);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.catalogButton);
            this.Controls.Add(this.rasterizeButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button rasterizeButton;
        private System.Windows.Forms.Button catalogButton;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Button UpdateBlueprintsButton;
    }
}

