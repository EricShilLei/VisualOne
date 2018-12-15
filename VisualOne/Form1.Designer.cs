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
            this.OnlineCatalogButton = new System.Windows.Forms.Button();
            this.UpdateBlueprintsButton = new System.Windows.Forms.Button();
            this.CreateCatalogProgressBar = new System.Windows.Forms.ProgressBar();
            this.LocalCatalogButton = new System.Windows.Forms.Button();
            this.sourceFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ResizeRenderingButton = new System.Windows.Forms.Button();
            this.ResizeGrayscaleButton = new System.Windows.Forms.Button();
            this.ResizeRenderingAsPng = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rasterizeButton
            // 
            this.rasterizeButton.Enabled = false;
            this.rasterizeButton.Location = new System.Drawing.Point(417, 29);
            this.rasterizeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rasterizeButton.Name = "rasterizeButton";
            this.rasterizeButton.Size = new System.Drawing.Size(363, 86);
            this.rasterizeButton.TabIndex = 0;
            this.rasterizeButton.Text = "Rasterize BluePrints";
            this.rasterizeButton.UseVisualStyleBackColor = true;
            this.rasterizeButton.Click += new System.EventHandler(this.RasterizeButton_Click);
            // 
            // OnlineCatalogButton
            // 
            this.OnlineCatalogButton.Location = new System.Drawing.Point(31, 138);
            this.OnlineCatalogButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OnlineCatalogButton.Name = "OnlineCatalogButton";
            this.OnlineCatalogButton.Size = new System.Drawing.Size(363, 86);
            this.OnlineCatalogButton.TabIndex = 2;
            this.OnlineCatalogButton.Text = "View Online Catalog";
            this.OnlineCatalogButton.UseVisualStyleBackColor = true;
            this.OnlineCatalogButton.Click += new System.EventHandler(this.OnlineCatalogButton_Click);
            // 
            // UpdateBlueprintsButton
            // 
            this.UpdateBlueprintsButton.Enabled = false;
            this.UpdateBlueprintsButton.Location = new System.Drawing.Point(31, 29);
            this.UpdateBlueprintsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateBlueprintsButton.Name = "UpdateBlueprintsButton";
            this.UpdateBlueprintsButton.Size = new System.Drawing.Size(363, 86);
            this.UpdateBlueprintsButton.TabIndex = 4;
            this.UpdateBlueprintsButton.Text = "Update Blueprints";
            this.UpdateBlueprintsButton.UseVisualStyleBackColor = true;
            this.UpdateBlueprintsButton.Click += new System.EventHandler(this.UpdateCatalogButton_Click);
            // 
            // CreateCatalogProgressBar
            // 
            this.CreateCatalogProgressBar.Location = new System.Drawing.Point(30, 245);
            this.CreateCatalogProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CreateCatalogProgressBar.Name = "CreateCatalogProgressBar";
            this.CreateCatalogProgressBar.Size = new System.Drawing.Size(750, 62);
            this.CreateCatalogProgressBar.TabIndex = 5;
            // 
            // LocalCatalogButton
            // 
            this.LocalCatalogButton.Location = new System.Drawing.Point(417, 138);
            this.LocalCatalogButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LocalCatalogButton.Name = "LocalCatalogButton";
            this.LocalCatalogButton.Size = new System.Drawing.Size(363, 86);
            this.LocalCatalogButton.TabIndex = 1;
            this.LocalCatalogButton.Text = "View Local Catalog";
            this.LocalCatalogButton.UseVisualStyleBackColor = true;
            this.LocalCatalogButton.Click += new System.EventHandler(this.LocalCatalogButton_Click);
            // 
            // sourceFolderBrowserDialog
            // 
            this.sourceFolderBrowserDialog.Description = "Pick the folder that contains the blueprints";
            this.sourceFolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // ResizeRenderingButton
            // 
            this.ResizeRenderingButton.Location = new System.Drawing.Point(807, 29);
            this.ResizeRenderingButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ResizeRenderingButton.Name = "ResizeRenderingButton";
            this.ResizeRenderingButton.Size = new System.Drawing.Size(363, 86);
            this.ResizeRenderingButton.TabIndex = 6;
            this.ResizeRenderingButton.Text = "Resize Rendering";
            this.ResizeRenderingButton.UseVisualStyleBackColor = true;
            this.ResizeRenderingButton.Click += new System.EventHandler(this.ResizeRenderingButton_Click);
            // 
            // ResizeGrayscaleButton
            // 
            this.ResizeGrayscaleButton.Location = new System.Drawing.Point(807, 138);
            this.ResizeGrayscaleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ResizeGrayscaleButton.Name = "ResizeGrayscaleButton";
            this.ResizeGrayscaleButton.Size = new System.Drawing.Size(363, 86);
            this.ResizeGrayscaleButton.TabIndex = 7;
            this.ResizeGrayscaleButton.Text = "Resize Rendering Grayscale";
            this.ResizeGrayscaleButton.UseVisualStyleBackColor = true;
            this.ResizeGrayscaleButton.Click += new System.EventHandler(this.ResizeGrayscaleButton_Click);
            // 
            // ResizeRenderingAsPng
            // 
            this.ResizeRenderingAsPng.Location = new System.Drawing.Point(807, 245);
            this.ResizeRenderingAsPng.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ResizeRenderingAsPng.Name = "ResizeRenderingAsPng";
            this.ResizeRenderingAsPng.Size = new System.Drawing.Size(363, 86);
            this.ResizeRenderingAsPng.TabIndex = 8;
            this.ResizeRenderingAsPng.Text = "Resize Rendering Png";
            this.ResizeRenderingAsPng.UseVisualStyleBackColor = true;
            this.ResizeRenderingAsPng.Click += new System.EventHandler(this.ResizeRenderingAsPng_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 330);
            this.Controls.Add(this.ResizeRenderingAsPng);
            this.Controls.Add(this.ResizeGrayscaleButton);
            this.Controls.Add(this.ResizeRenderingButton);
            this.Controls.Add(this.CreateCatalogProgressBar);
            this.Controls.Add(this.UpdateBlueprintsButton);
            this.Controls.Add(this.OnlineCatalogButton);
            this.Controls.Add(this.LocalCatalogButton);
            this.Controls.Add(this.rasterizeButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button rasterizeButton;
        private System.Windows.Forms.Button OnlineCatalogButton;
        private System.Windows.Forms.Button UpdateBlueprintsButton;
        private System.Windows.Forms.ProgressBar CreateCatalogProgressBar;
        private System.Windows.Forms.Button LocalCatalogButton;
        private System.Windows.Forms.FolderBrowserDialog sourceFolderBrowserDialog;
        private System.Windows.Forms.Button ResizeRenderingButton;
        private System.Windows.Forms.Button ResizeGrayscaleButton;
        private System.Windows.Forms.Button ResizeRenderingAsPng;
    }
}

