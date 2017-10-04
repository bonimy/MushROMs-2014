namespace MushROMs.SNESControls.PaletteEditor
{
    partial class RGBForm
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
            this.gbxGrayscale = new System.Windows.Forms.GroupBox();
            this.lblBlue = new System.Windows.Forms.Label();
            this.ltbBlue = new MushROMs.Controls.LinkedTrackBar();
            this.ntbBlue = new MushROMs.Controls.IntegerTextBox();
            this.lblGreen = new System.Windows.Forms.Label();
            this.ltbGreen = new MushROMs.Controls.LinkedTrackBar();
            this.ntbGreen = new MushROMs.Controls.IntegerTextBox();
            this.lblRed = new System.Windows.Forms.Label();
            this.ltbRed = new MushROMs.Controls.LinkedTrackBar();
            this.ntbRed = new MushROMs.Controls.IntegerTextBox();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbxGrayscale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbRed)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxGrayscale
            // 
            this.gbxGrayscale.Controls.Add(this.lblBlue);
            this.gbxGrayscale.Controls.Add(this.ltbBlue);
            this.gbxGrayscale.Controls.Add(this.ntbBlue);
            this.gbxGrayscale.Controls.Add(this.lblGreen);
            this.gbxGrayscale.Controls.Add(this.ltbGreen);
            this.gbxGrayscale.Controls.Add(this.ntbGreen);
            this.gbxGrayscale.Controls.Add(this.lblRed);
            this.gbxGrayscale.Controls.Add(this.ltbRed);
            this.gbxGrayscale.Controls.Add(this.ntbRed);
            this.gbxGrayscale.Location = new System.Drawing.Point(12, 12);
            this.gbxGrayscale.Name = "gbxGrayscale";
            this.gbxGrayscale.Size = new System.Drawing.Size(329, 211);
            this.gbxGrayscale.TabIndex = 0;
            this.gbxGrayscale.TabStop = false;
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(6, 144);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(31, 13);
            this.lblBlue.TabIndex = 0;
            this.lblBlue.Text = "Blue:";
            // 
            // ltbBlue
            // 
            this.ltbBlue.Location = new System.Drawing.Point(6, 160);
            this.ltbBlue.Maximum = 255;
            this.ltbBlue.Name = "ltbBlue";
            this.ltbBlue.IntegerComponent = this.ntbBlue;
            this.ltbBlue.Size = new System.Drawing.Size(271, 45);
            this.ltbBlue.TabIndex = 4;
            this.ltbBlue.TickFrequency = 16;
            this.ltbBlue.Value = 255;
            this.ltbBlue.ValueChanged += new System.EventHandler(this.RGB_ValueChanged);
            // 
            // ntbBlue
            // 
            this.ntbBlue.AllowNegative = true;
            this.ntbBlue.Location = new System.Drawing.Point(283, 160);
            this.ntbBlue.MaxLength = 4;
            this.ntbBlue.Name = "ntbBlue";
            this.ntbBlue.Size = new System.Drawing.Size(40, 20);
            this.ntbBlue.TabIndex = 5;
            this.ntbBlue.Text = "255";
            this.ntbBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbBlue.Value = 255;
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(6, 80);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(39, 13);
            this.lblGreen.TabIndex = 0;
            this.lblGreen.Text = "Green:";
            // 
            // ltbGreen
            // 
            this.ltbGreen.LargeChange = 16;
            this.ltbGreen.Location = new System.Drawing.Point(6, 96);
            this.ltbGreen.Maximum = 255;
            this.ltbGreen.Name = "ltbGreen";
            this.ltbGreen.IntegerComponent = this.ntbGreen;
            this.ltbGreen.Size = new System.Drawing.Size(271, 45);
            this.ltbGreen.TabIndex = 2;
            this.ltbGreen.TickFrequency = 16;
            this.ltbGreen.Value = 255;
            this.ltbGreen.ValueChanged += new System.EventHandler(this.RGB_ValueChanged);
            // 
            // ntbGreen
            // 
            this.ntbGreen.AllowNegative = true;
            this.ntbGreen.Location = new System.Drawing.Point(283, 96);
            this.ntbGreen.MaxLength = 4;
            this.ntbGreen.Name = "ntbGreen";
            this.ntbGreen.Size = new System.Drawing.Size(40, 20);
            this.ntbGreen.TabIndex = 3;
            this.ntbGreen.Text = "255";
            this.ntbGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbGreen.Value = 255;
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(6, 16);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(30, 13);
            this.lblRed.TabIndex = 0;
            this.lblRed.Text = "Red:";
            // 
            // ltbRed
            // 
            this.ltbRed.LargeChange = 16;
            this.ltbRed.Location = new System.Drawing.Point(6, 32);
            this.ltbRed.Maximum = 255;
            this.ltbRed.Name = "ltbRed";
            this.ltbRed.IntegerComponent = this.ntbRed;
            this.ltbRed.Size = new System.Drawing.Size(271, 45);
            this.ltbRed.TabIndex = 0;
            this.ltbRed.TickFrequency = 16;
            this.ltbRed.Value = 255;
            this.ltbRed.ValueChanged += new System.EventHandler(this.RGB_ValueChanged);
            // 
            // ntbRed
            // 
            this.ntbRed.AllowNegative = true;
            this.ntbRed.Location = new System.Drawing.Point(283, 32);
            this.ntbRed.MaxLength = 4;
            this.ntbRed.Name = "ntbRed";
            this.ntbRed.Size = new System.Drawing.Size(40, 20);
            this.ntbRed.TabIndex = 1;
            this.ntbRed.Text = "255";
            this.ntbRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbRed.Value = 255;
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(12, 233);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(64, 17);
            this.chkPreview.TabIndex = 1;
            this.chkPreview.Text = "Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(185, 229);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // RGBForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(353, 264);
            this.Controls.Add(this.gbxGrayscale);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RGBForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "RGBForm";
            this.Shown += new System.EventHandler(this.RGBForm_Shown);
            this.gbxGrayscale.ResumeLayout(false);
            this.gbxGrayscale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbRed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox gbxGrayscale;
        protected System.Windows.Forms.CheckBox chkPreview;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnOK;
        protected Controls.LinkedTrackBar ltbGreen;
        protected Controls.IntegerTextBox ntbGreen;
        protected Controls.LinkedTrackBar ltbRed;
        protected Controls.IntegerTextBox ntbRed;
        protected Controls.LinkedTrackBar ltbBlue;
        protected Controls.IntegerTextBox ntbBlue;
        protected System.Windows.Forms.Label lblGreen;
        protected System.Windows.Forms.Label lblRed;
        protected System.Windows.Forms.Label lblBlue;
    }
}