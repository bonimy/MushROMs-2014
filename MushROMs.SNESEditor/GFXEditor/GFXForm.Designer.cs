namespace MushROMs.SNESEditor.GFXEditor
{
    partial class GFXForm
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
            this.gfxControl = new MushROMs.SNESControls.GFXEditor.GFXControl();
            this.hsbGFX = new MushROMs.Controls.EditorHScrollBar();
            this.vsbGFX = new MushROMs.Controls.EditorVScrollBar();
            this.gfxStatus = new MushROMs.SNESEditor.GFXEditor.GFXStatus();
            this.gfx1 = new MushROMs.SNES.GFX();
            this.editor1 = new MushROMs.Editors.Editor();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 156);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(415, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 5;
            this.stsMain.Text = "...";
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(73, 17);
            this.tssMain.Text = "Editor Ready";
            // 
            // gfxControl
            // 
            this.gfxControl.BackColor = System.Drawing.Color.Magenta;
            this.gfxControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gfxControl.EditorHScrollBar = this.hsbGFX;
            this.gfxControl.EditorVScrollBar = this.vsbGFX;
            this.gfxControl.Location = new System.Drawing.Point(0, 0);
            this.gfxControl.Margin = new System.Windows.Forms.Padding(0);
            this.gfxControl.Name = "gfxControl";
            this.gfxControl.OverrideInputKeys = new System.Windows.Forms.Keys[] {
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
            this.gfxControl.TabIndex = 0;
            this.gfxControl.UserImageLocation = new System.Drawing.Point(0, 0);
            this.gfxControl.UserImageSize = new System.Drawing.Size(150, 150);
            // 
            // hsbGFX
            // 
            this.hsbGFX.EditorControl = this.gfxControl;
            this.hsbGFX.Location = new System.Drawing.Point(0, 130);
            this.hsbGFX.Name = "hsbGFX";
            this.hsbGFX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.hsbGFX.Size = new System.Drawing.Size(258, 17);
            this.hsbGFX.TabIndex = 7;
            // 
            // vsbGFX
            // 
            this.vsbGFX.EditorControl = this.gfxControl;
            this.vsbGFX.Location = new System.Drawing.Point(258, 0);
            this.vsbGFX.Name = "vsbGFX";
            this.vsbGFX.ScrollEnd = MushROMs.Editors.ScrollEnd.Full;
            this.vsbGFX.Size = new System.Drawing.Size(17, 130);
            this.vsbGFX.TabIndex = 8;
            // 
            // gfxStatus
            // 
            this.gfxStatus.GraphicsFormat = MushROMs.LunarCompress.GraphicsFormats.None;
            this.gfxStatus.Location = new System.Drawing.Point(278, 0);
            this.gfxStatus.Name = "gfxStatus";
            this.gfxStatus.ShowAddressScrolling = true;
            this.gfxStatus.Size = new System.Drawing.Size(134, 103);
            this.gfxStatus.TabIndex = 9;
            this.gfxStatus.GraphicsFormatChanged += new System.EventHandler(this.GFXStatus_GraphicsFormatChanged);
            this.gfxStatus.ZoomScaleChanged += new System.EventHandler(this.GFXStatus_ZoomScaleChanged);
            this.gfxStatus.NextByte += new System.EventHandler(this.GFXStatus_NextByte);
            this.gfxStatus.LastByte += new System.EventHandler(this.GFXStatus_LastByte);
            // 
            // gfx1
            // 
            this.gfx1.ArrangeFormat = MushROMs.SNES.ArrangeFormats.Horizontal;
            this.gfx1.FileFormat = MushROMs.SNES.GFXFileFormats.CHR;
            this.gfx1.GraphicsFormat = MushROMs.LunarCompress.GraphicsFormats.SNES_4BPP;
            this.gfx1.TileSize = new System.Drawing.Size(8, 8);
            this.gfx1.ViewSize = new System.Drawing.Size(16, 8);
            this.gfx1.ZoomSize = new System.Drawing.Size(1, 1);
            // 
            // editor1
            // 
            this.editor1.TileSize = new System.Drawing.Size(8, 8);
            this.editor1.ViewSize = new System.Drawing.Size(16, 8);
            this.editor1.ZoomSize = new System.Drawing.Size(1, 1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GFXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 178);
            this.Controls.Add(this.gfxControl);
            this.Controls.Add(this.gfxStatus);
            this.Controls.Add(this.vsbGFX);
            this.Controls.Add(this.hsbGFX);
            this.Controls.Add(this.stsMain);
            this.Name = "GFXForm";
            this.Text = "GFXForm";
            this.Load += new System.EventHandler(this.GFXForm_Load);
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private MushROMs.Controls.EditorHScrollBar hsbGFX;
        private MushROMs.Controls.EditorVScrollBar vsbGFX;
        private GFXStatus gfxStatus;
        private SNESControls.GFXEditor.GFXControl gfxControl;
        private SNES.GFX gfx1;
        private Editors.Editor editor1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}