namespace MushROMs.SNESEditor.PaletteEditor
{
    partial class PaletteForm
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
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.paletteStatus = new MushROMs.SNESControls.PaletteEditor.PaletteStatus();
            this.paletteControl = new MushROMs.SNESControls.PaletteEditor.PaletteControl();
            this.hsbPalette = new MushROMs.Controls.EditorHScrollBar();
            this.vsbPalette = new MushROMs.Controls.EditorVScrollBar();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 279);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(407, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 0;
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(76, 17);
            this.tssMain.Text = "Editor Ready!";
            // 
            // paletteStatus
            // 
            this.paletteStatus.Location = new System.Drawing.Point(275, 0);
            this.paletteStatus.Margin = new System.Windows.Forms.Padding(0);
            this.paletteStatus.Name = "paletteStatus";
            this.paletteStatus.ShowAddressScrolling = true;
            this.paletteStatus.Size = new System.Drawing.Size(132, 132);
            this.paletteStatus.TabIndex = 3;
            this.paletteStatus.ZoomScaleChanged += new System.EventHandler(this.PaletteOptions_ZoomScaleChanged);
            this.paletteStatus.NextByte += new System.EventHandler(this.PaletteOptions_NextByteClick);
            this.paletteStatus.LastByte += new System.EventHandler(this.PaletteOptions_LastByteClick);
            // 
            // paletteControl
            // 
            this.paletteControl.BackColor = System.Drawing.Color.Magenta;
            this.paletteControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteControl.EditorHScrollBar = this.hsbPalette;
            this.paletteControl.EditorVScrollBar = this.vsbPalette;
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
            this.paletteControl.TileMouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PaletteControl_TileMouseDoubleClick);
            // 
            // hsbPalette
            // 
            this.hsbPalette.EditorControl = this.paletteControl;
            this.hsbPalette.Location = new System.Drawing.Point(0, 258);
            this.hsbPalette.Name = "hsbPalette";
            this.hsbPalette.Size = new System.Drawing.Size(258, 17);
            this.hsbPalette.TabIndex = 1;
            // 
            // vsbPalette
            // 
            this.vsbPalette.EditorControl = this.paletteControl;
            this.vsbPalette.Location = new System.Drawing.Point(258, 0);
            this.vsbPalette.Name = "vsbPalette";
            this.vsbPalette.Size = new System.Drawing.Size(17, 258);
            this.vsbPalette.TabIndex = 2;
            // 
            // PaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 301);
            this.Controls.Add(this.paletteStatus);
            this.Controls.Add(this.paletteControl);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.vsbPalette);
            this.Controls.Add(this.hsbPalette);
            this.Name = "PaletteForm";
            this.Load += new System.EventHandler(this.PaletteForm_Load);
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private Controls.EditorHScrollBar hsbPalette;
        private SNESControls.PaletteEditor.PaletteControl paletteControl;
        private Controls.EditorVScrollBar vsbPalette;
        private SNESControls.PaletteEditor.PaletteStatus paletteStatus;
    }
}