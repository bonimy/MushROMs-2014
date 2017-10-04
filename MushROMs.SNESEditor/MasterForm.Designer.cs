namespace MushROMs.SNESEditor
{
    partial class MasterForm
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsmEditors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaletteEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGFXEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEditors});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(190, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "...";
            // 
            // tsmEditors
            // 
            this.tsmEditors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPaletteEditor,
            this.tsmGFXEditor});
            this.tsmEditors.Name = "tsmEditors";
            this.tsmEditors.Size = new System.Drawing.Size(55, 20);
            this.tsmEditors.Text = "E&ditors";
            // 
            // tsmPaletteEditor
            // 
            this.tsmPaletteEditor.CheckOnClick = true;
            this.tsmPaletteEditor.Name = "tsmPaletteEditor";
            this.tsmPaletteEditor.Size = new System.Drawing.Size(144, 22);
            this.tsmPaletteEditor.Text = "&Palette Editor";
            this.tsmPaletteEditor.CheckedChanged += new System.EventHandler(this.tsmPaletteEditor_CheckedChanged);
            // 
            // tsmGFXEditor
            // 
            this.tsmGFXEditor.CheckOnClick = true;
            this.tsmGFXEditor.Name = "tsmGFXEditor";
            this.tsmGFXEditor.Size = new System.Drawing.Size(144, 22);
            this.tsmGFXEditor.Text = "&GFX Editor";
            this.tsmGFXEditor.CheckedChanged += new System.EventHandler(this.tsmGFXEditor_CheckedChanged);
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 24);
            this.Controls.Add(this.mnuMain);
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::MushROMs.SNESEditor.Properties.Settings.Default, "DefaultWindowState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MushROMs";
            this.TopMost = true;
            this.WindowState = global::MushROMs.SNESEditor.Properties.Settings.Default.DefaultWindowState;
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmEditors;
        private System.Windows.Forms.ToolStripMenuItem tsmPaletteEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmGFXEditor;
    }
}