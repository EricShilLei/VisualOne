namespace VisualOne
{
    partial class ThemeSelectorForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.ThemeListbox = new System.Windows.Forms.ListBox();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 916);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(286, 72);
            this.button1.TabIndex = 0;
            this.button1.Text = "Okay";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ThemeListbox
            // 
            this.ThemeListbox.FormattingEnabled = true;
            this.ThemeListbox.ItemHeight = 25;
            this.ThemeListbox.Location = new System.Drawing.Point(35, 36);
            this.ThemeListbox.Name = "ThemeListbox";
            this.ThemeListbox.Size = new System.Drawing.Size(286, 854);
            this.ThemeListbox.TabIndex = 2;
            this.ThemeListbox.SelectedValueChanged += new System.EventHandler(this.ThemeListbox_SelectedValueChanged);
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PreviewPictureBox.Location = new System.Drawing.Point(367, 36);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(1258, 952);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 3;
            this.PreviewPictureBox.TabStop = false;
            // 
            // ThemeSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 1028);
            this.Controls.Add(this.PreviewPictureBox);
            this.Controls.Add(this.ThemeListbox);
            this.Controls.Add(this.button1);
            this.Name = "ThemeSelectorForm";
            this.Text = "ThemeSelectorForm";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox ThemeListbox;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
    }
}