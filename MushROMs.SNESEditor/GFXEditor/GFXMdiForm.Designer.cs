namespace MushROMs.SNESEditor.GFXEditor
{
    partial class GFXMdiForm
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
            this.mnuMain = new MushROMs.SNESEditor.GFXEditor.GFXMenuStrip();
            this.tlsMain = new MushROMs.SNESEditor.GFXEditor.GFXToolStrip();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 540);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(984, 22);
            this.stsMain.TabIndex = 4;
            this.stsMain.Text = "...";
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(73, 17);
            this.tssMain.Text = "Editor ready!";
            // 
            // mnuMain
            // 
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(984, 24);
            this.mnuMain.TabIndex = 5;
            this.mnuMain.Text = "...";
            // 
            // tlsMain
            // 
            this.tlsMain.Location = new System.Drawing.Point(0, 24);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(984, 25);
            this.tlsMain.TabIndex = 6;
            this.tlsMain.Text = "...";
            // 
            // GFXMdiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.tlsMain);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.mnuMain);
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::MushROMs.SNESEditor.Properties.Settings.Default, "DefaultWindowState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "GFXMdiForm";
            this.Text = "GFXMdiForm";
            this.WindowState = global::MushROMs.SNESEditor.Properties.Settings.Default.DefaultWindowState;
            this.Controls.SetChildIndex(this.mnuMain, 0);
            this.Controls.SetChildIndex(this.stsMain, 0);
            this.Controls.SetChildIndex(this.tlsMain, 0);
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private GFXMenuStrip mnuMain;
        private GFXToolStrip tlsMain;
    }
}