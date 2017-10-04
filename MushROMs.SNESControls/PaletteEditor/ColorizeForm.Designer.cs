namespace MushROMs.SNESControls.PaletteEditor
{
    partial class ColorizeForm
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
            this.btnReset = new System.Windows.Forms.Button();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.chkColorize = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxColorize = new System.Windows.Forms.GroupBox();
            this.lblEffectiveness = new System.Windows.Forms.Label();
            this.ltbEffectiveness = new MushROMs.Controls.LinkedTrackBar();
            this.ntbEffectiveness = new MushROMs.Controls.IntegerTextBox();
            this.lblLightness = new System.Windows.Forms.Label();
            this.ltbLightness = new MushROMs.Controls.LinkedTrackBar();
            this.ntbLightness = new MushROMs.Controls.IntegerTextBox();
            this.lblSaturation = new System.Windows.Forms.Label();
            this.ltbSaturation = new MushROMs.Controls.LinkedTrackBar();
            this.ntbSaturation = new MushROMs.Controls.IntegerTextBox();
            this.lblHue = new System.Windows.Forms.Label();
            this.ltbHue = new MushROMs.Controls.LinkedTrackBar();
            this.ntbHue = new MushROMs.Controls.IntegerTextBox();
            this.gbxColorize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbEffectiveness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbLightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbHue)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(81, 293);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(12, 320);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(64, 17);
            this.chkPreview.TabIndex = 2;
            this.chkPreview.Text = "Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.Preview_CheckedChanged);
            // 
            // chkColorize
            // 
            this.chkColorize.AutoSize = true;
            this.chkColorize.Location = new System.Drawing.Point(12, 297);
            this.chkColorize.Name = "chkColorize";
            this.chkColorize.Size = new System.Drawing.Size(63, 17);
            this.chkColorize.TabIndex = 1;
            this.chkColorize.Text = "Colorize";
            this.chkColorize.UseVisualStyleBackColor = true;
            this.chkColorize.CheckedChanged += new System.EventHandler(this.Colorize_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(185, 293);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 293);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbxColorize
            // 
            this.gbxColorize.Controls.Add(this.lblEffectiveness);
            this.gbxColorize.Controls.Add(this.ltbEffectiveness);
            this.gbxColorize.Controls.Add(this.ntbEffectiveness);
            this.gbxColorize.Controls.Add(this.lblLightness);
            this.gbxColorize.Controls.Add(this.ltbLightness);
            this.gbxColorize.Controls.Add(this.ntbLightness);
            this.gbxColorize.Controls.Add(this.lblSaturation);
            this.gbxColorize.Controls.Add(this.ltbSaturation);
            this.gbxColorize.Controls.Add(this.ntbSaturation);
            this.gbxColorize.Controls.Add(this.lblHue);
            this.gbxColorize.Controls.Add(this.ltbHue);
            this.gbxColorize.Controls.Add(this.ntbHue);
            this.gbxColorize.Location = new System.Drawing.Point(12, 12);
            this.gbxColorize.Name = "gbxColorize";
            this.gbxColorize.Size = new System.Drawing.Size(329, 275);
            this.gbxColorize.TabIndex = 0;
            this.gbxColorize.TabStop = false;
            // 
            // lblEffectiveness
            // 
            this.lblEffectiveness.AutoSize = true;
            this.lblEffectiveness.Enabled = false;
            this.lblEffectiveness.Location = new System.Drawing.Point(6, 208);
            this.lblEffectiveness.Name = "lblEffectiveness";
            this.lblEffectiveness.Size = new System.Drawing.Size(74, 13);
            this.lblEffectiveness.TabIndex = 0;
            this.lblEffectiveness.Text = "Effectiveness:";
            // 
            // ltbEffectiveness
            // 
            this.ltbEffectiveness.Enabled = false;
            this.ltbEffectiveness.Location = new System.Drawing.Point(6, 224);
            this.ltbEffectiveness.Maximum = 100;
            this.ltbEffectiveness.Name = "ltbEffectiveness";
            this.ltbEffectiveness.IntegerComponent = this.ntbEffectiveness;
            this.ltbEffectiveness.Size = new System.Drawing.Size(271, 45);
            this.ltbEffectiveness.TabIndex = 6;
            this.ltbEffectiveness.TickFrequency = 5;
            this.ltbEffectiveness.Value = 100;
            this.ltbEffectiveness.ValueChanged += new System.EventHandler(this.HSLE_ValueChanged);
            // 
            // ntbEffectiveness
            // 
            this.ntbEffectiveness.Enabled = false;
            this.ntbEffectiveness.Location = new System.Drawing.Point(283, 228);
            this.ntbEffectiveness.MaxLength = 4;
            this.ntbEffectiveness.Name = "ntbEffectiveness";
            this.ntbEffectiveness.Size = new System.Drawing.Size(40, 20);
            this.ntbEffectiveness.TabIndex = 7;
            this.ntbEffectiveness.Text = "100";
            this.ntbEffectiveness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbEffectiveness.Value = 100;
            // 
            // lblLightness
            // 
            this.lblLightness.AutoSize = true;
            this.lblLightness.Location = new System.Drawing.Point(6, 144);
            this.lblLightness.Name = "lblLightness";
            this.lblLightness.Size = new System.Drawing.Size(55, 13);
            this.lblLightness.TabIndex = 0;
            this.lblLightness.Text = "Lightness:";
            // 
            // ltbLightness
            // 
            this.ltbLightness.LargeChange = 10;
            this.ltbLightness.Location = new System.Drawing.Point(6, 160);
            this.ltbLightness.Maximum = 100;
            this.ltbLightness.Minimum = -100;
            this.ltbLightness.Name = "ltbLightness";
            this.ltbLightness.IntegerComponent = this.ntbLightness;
            this.ltbLightness.Size = new System.Drawing.Size(271, 45);
            this.ltbLightness.TabIndex = 4;
            this.ltbLightness.TickFrequency = 10;
            this.ltbLightness.ValueChanged += new System.EventHandler(this.HSLE_ValueChanged);
            // 
            // ntbLightness
            // 
            this.ntbLightness.AllowNegative = true;
            this.ntbLightness.Location = new System.Drawing.Point(283, 164);
            this.ntbLightness.MaxLength = 4;
            this.ntbLightness.Name = "ntbLightness";
            this.ntbLightness.Size = new System.Drawing.Size(40, 20);
            this.ntbLightness.TabIndex = 5;
            this.ntbLightness.Text = "0";
            this.ntbLightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSaturation
            // 
            this.lblSaturation.AutoSize = true;
            this.lblSaturation.Location = new System.Drawing.Point(6, 80);
            this.lblSaturation.Name = "lblSaturation";
            this.lblSaturation.Size = new System.Drawing.Size(58, 13);
            this.lblSaturation.TabIndex = 0;
            this.lblSaturation.Text = "Saturation:";
            // 
            // ltbSaturation
            // 
            this.ltbSaturation.LargeChange = 10;
            this.ltbSaturation.Location = new System.Drawing.Point(6, 96);
            this.ltbSaturation.Maximum = 100;
            this.ltbSaturation.Minimum = -100;
            this.ltbSaturation.Name = "ltbSaturation";
            this.ltbSaturation.IntegerComponent = this.ntbSaturation;
            this.ltbSaturation.Size = new System.Drawing.Size(271, 45);
            this.ltbSaturation.TabIndex = 2;
            this.ltbSaturation.TickFrequency = 10;
            this.ltbSaturation.ValueChanged += new System.EventHandler(this.HSLE_ValueChanged);
            // 
            // ntbSaturation
            // 
            this.ntbSaturation.AllowNegative = true;
            this.ntbSaturation.Location = new System.Drawing.Point(283, 100);
            this.ntbSaturation.MaxLength = 4;
            this.ntbSaturation.Name = "ntbSaturation";
            this.ntbSaturation.Size = new System.Drawing.Size(40, 20);
            this.ntbSaturation.TabIndex = 3;
            this.ntbSaturation.Text = "0";
            this.ntbSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHue
            // 
            this.lblHue.AutoSize = true;
            this.lblHue.Location = new System.Drawing.Point(6, 16);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(30, 13);
            this.lblHue.TabIndex = 0;
            this.lblHue.Text = "Hue:";
            // 
            // ltbHue
            // 
            this.ltbHue.LargeChange = 18;
            this.ltbHue.Location = new System.Drawing.Point(6, 32);
            this.ltbHue.Maximum = 180;
            this.ltbHue.Minimum = -180;
            this.ltbHue.Name = "ltbHue";
            this.ltbHue.IntegerComponent = this.ntbHue;
            this.ltbHue.Size = new System.Drawing.Size(271, 45);
            this.ltbHue.TabIndex = 0;
            this.ltbHue.TickFrequency = 18;
            this.ltbHue.ValueChanged += new System.EventHandler(this.HSLE_ValueChanged);
            // 
            // ntbHue
            // 
            this.ntbHue.AllowNegative = true;
            this.ntbHue.Location = new System.Drawing.Point(283, 36);
            this.ntbHue.MaxLength = 4;
            this.ntbHue.Name = "ntbHue";
            this.ntbHue.Size = new System.Drawing.Size(40, 20);
            this.ntbHue.TabIndex = 1;
            this.ntbHue.Text = "0";
            this.ntbHue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ColorizeForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(353, 346);
            this.Controls.Add(this.gbxColorize);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.chkColorize);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Colorize Palette";
            this.Shown += new System.EventHandler(this.ColorizeForm_Shown);
            this.gbxColorize.ResumeLayout(false);
            this.gbxColorize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbEffectiveness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbLightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbHue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LinkedTrackBar ltbHue;
        private Controls.IntegerTextBox ntbHue;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.CheckBox chkColorize;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbxColorize;
        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.Label lblEffectiveness;
        private Controls.LinkedTrackBar ltbEffectiveness;
        private Controls.IntegerTextBox ntbEffectiveness;
        private System.Windows.Forms.Label lblLightness;
        private Controls.LinkedTrackBar ltbLightness;
        private Controls.IntegerTextBox ntbLightness;
        private System.Windows.Forms.Label lblSaturation;
        private Controls.LinkedTrackBar ltbSaturation;
        private Controls.IntegerTextBox ntbSaturation;
    }
}