namespace MushROMs.SNESEditor.GFXEditor
{
    partial class GFXTileStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxZoom = new System.Windows.Forms.GroupBox();
            this.cbxZoom = new System.Windows.Forms.ComboBox();
            this.gbxZoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxZoom
            // 
            this.gbxZoom.Controls.Add(this.cbxZoom);
            this.gbxZoom.Location = new System.Drawing.Point(3, 3);
            this.gbxZoom.Name = "gbxZoom";
            this.gbxZoom.Size = new System.Drawing.Size(56, 44);
            this.gbxZoom.TabIndex = 9;
            this.gbxZoom.TabStop = false;
            this.gbxZoom.Text = "Zoom";
            // 
            // cbxZoom
            // 
            this.cbxZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxZoom.FormattingEnabled = true;
            this.cbxZoom.ItemHeight = 13;
            this.cbxZoom.Items.AddRange(new object[] {
            "8x",
            "16x",
            "24x",
            "32x"});
            this.cbxZoom.Location = new System.Drawing.Point(6, 16);
            this.cbxZoom.Name = "cbxZoom";
            this.cbxZoom.Size = new System.Drawing.Size(44, 21);
            this.cbxZoom.TabIndex = 0;
            // 
            // GFXTileStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxZoom);
            this.Name = "GFXTileStatus";
            this.Size = new System.Drawing.Size(82, 82);
            this.gbxZoom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxZoom;
        private System.Windows.Forms.ComboBox cbxZoom;
    }
}
