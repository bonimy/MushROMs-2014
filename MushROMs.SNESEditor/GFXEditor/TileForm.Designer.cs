namespace MushROMs.SNESEditor.GFXEditor
{
    partial class TileForm
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
            this.hsbTileEditor = new MushROMs.Controls.EditorHScrollBar();
            this.gfxTileControl = new MushROMs.SNESControls.GFXEditor.GFXTileControl();
            this.vsbTileEditor = new MushROMs.Controls.EditorVScrollBar();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.gfxTileStatus = new MushROMs.SNESEditor.GFXEditor.GFXTileStatus();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // hsbTileEditor
            // 
            this.hsbTileEditor.EditorControl = this.gfxTileControl;
            this.hsbTileEditor.ScrollEnd = MushROMs.Editors.ScrollEnd.Full;
            this.hsbTileEditor.Location = new System.Drawing.Point(0, 258);
            this.hsbTileEditor.Name = "hsbTileEditor";
            this.hsbTileEditor.Size = new System.Drawing.Size(258, 17);
            this.hsbTileEditor.TabIndex = 1;
            // 
            // gfxTileControl
            // 
            this.gfxTileControl.BackColor = System.Drawing.Color.Magenta;
            this.gfxTileControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gfxTileControl.EditorHScrollBar = this.hsbTileEditor;
            this.gfxTileControl.EditorVScrollBar = this.vsbTileEditor;
            this.gfxTileControl.Location = new System.Drawing.Point(0, 0);
            this.gfxTileControl.Margin = new System.Windows.Forms.Padding(0);
            this.gfxTileControl.Name = "gfxTileControl";
            this.gfxTileControl.OverrideInputKeys = new System.Windows.Forms.Keys[] {
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
            this.gfxTileControl.TabIndex = 0;
            this.gfxTileControl.UserImageLocation = new System.Drawing.Point(0, 0);
            this.gfxTileControl.UserImageSize = new System.Drawing.Size(150, 150);
            // 
            // vsbTileEditor
            // 
            this.vsbTileEditor.EditorControl = this.gfxTileControl;
            this.vsbTileEditor.Location = new System.Drawing.Point(258, 0);
            this.vsbTileEditor.Name = "vsbTileEditor";
            this.vsbTileEditor.Size = new System.Drawing.Size(17, 258);
            this.vsbTileEditor.TabIndex = 2;
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 422);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(466, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 0;
            this.stsMain.Text = "...";
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(73, 17);
            this.tssMain.Text = "Editor Ready";
            // 
            // gfxTileStatus
            // 
            this.gfxTileStatus.Location = new System.Drawing.Point(278, 0);
            this.gfxTileStatus.Name = "gfxTileStatus";
            this.gfxTileStatus.Size = new System.Drawing.Size(82, 82);
            this.gfxTileStatus.TabIndex = 3;
            this.gfxTileStatus.ZoomScaleChanged += new System.EventHandler(this.gfxTileStatus_ZoomScaleChanged);
            // 
            // TileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 444);
            this.Controls.Add(this.gfxTileStatus);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.gfxTileControl);
            this.Controls.Add(this.vsbTileEditor);
            this.Controls.Add(this.hsbTileEditor);
            this.Name = "TileForm";
            this.ShowIcon = false;
            this.Text = "Tile Editor";
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.EditorHScrollBar hsbTileEditor;
        private Controls.EditorVScrollBar vsbTileEditor;
        private SNESControls.GFXEditor.GFXTileControl gfxTileControl;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private GFXTileStatus gfxTileStatus;
    }
}