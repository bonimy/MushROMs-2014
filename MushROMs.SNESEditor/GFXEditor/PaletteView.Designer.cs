namespace MushROMs.SNESEditor.GFXEditor
{
    partial class PaletteView
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
            this.paletteControl = new MushROMs.SNESControls.PaletteEditor.PaletteControl();
            this.SuspendLayout();
            // 
            // paletteControl
            // 
            this.paletteControl.BackColor = System.Drawing.Color.Magenta;
            this.paletteControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteControl.EditorHScrollBar = null;
            this.paletteControl.EditorVScrollBar = null;
            this.paletteControl.Location = new System.Drawing.Point(0, 0);
            this.paletteControl.Margin = new System.Windows.Forms.Padding(0);
            this.paletteControl.Name = "paletteControl";
            this.paletteControl.OverrideInputKeys = new System.Windows.Forms.Keys[] {
        System.Windows.Forms.Keys.Up,
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up))),
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up))),
        ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up))),
        System.Windows.Forms.Keys.Left,
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left))),
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left))),
        ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left))),
        System.Windows.Forms.Keys.Down,
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down))),
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down))),
        ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down))),
        System.Windows.Forms.Keys.Right,
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right))),
        ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right))),
        ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))};
            this.paletteControl.TabIndex = 0;
            this.paletteControl.UserImageLocation = new System.Drawing.Point(0, 0);
            this.paletteControl.UserImageSize = new System.Drawing.Size(150, 150);
            this.paletteControl.WritePixels += new System.EventHandler(this.paletteControl_WritePixels);
            // 
            // PaletteView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 258);
            this.Controls.Add(this.paletteControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteView";
            this.Text = "Palette";
            this.ResumeLayout(false);

        }

        #endregion

        private SNESControls.PaletteEditor.PaletteControl paletteControl;
    }
}