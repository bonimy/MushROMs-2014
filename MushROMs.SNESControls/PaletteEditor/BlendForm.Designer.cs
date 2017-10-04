namespace MushROMs.SNESControls.PaletteEditor
{
    partial class BlendForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbLighten = new System.Windows.Forms.RadioButton();
            this.rdbDarken = new System.Windows.Forms.RadioButton();
            this.rdbDifference = new System.Windows.Forms.RadioButton();
            this.rdbLinearLight = new System.Windows.Forms.RadioButton();
            this.rdbVividLight = new System.Windows.Forms.RadioButton();
            this.rdbLinearBurn = new System.Windows.Forms.RadioButton();
            this.rdbColorBurn = new System.Windows.Forms.RadioButton();
            this.rdbLinearDodge = new System.Windows.Forms.RadioButton();
            this.rdbColorDodge = new System.Windows.Forms.RadioButton();
            this.rdbSoftLight = new System.Windows.Forms.RadioButton();
            this.rdbHardLight = new System.Windows.Forms.RadioButton();
            this.rdbOverlay = new System.Windows.Forms.RadioButton();
            this.rdbScreen = new System.Windows.Forms.RadioButton();
            this.rdbMultiply = new System.Windows.Forms.RadioButton();
            this.gbxGrayscale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbBlue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbLighten);
            this.groupBox1.Controls.Add(this.rdbDarken);
            this.groupBox1.Controls.Add(this.rdbDifference);
            this.groupBox1.Controls.Add(this.rdbLinearLight);
            this.groupBox1.Controls.Add(this.rdbVividLight);
            this.groupBox1.Controls.Add(this.rdbLinearBurn);
            this.groupBox1.Controls.Add(this.rdbColorBurn);
            this.groupBox1.Controls.Add(this.rdbLinearDodge);
            this.groupBox1.Controls.Add(this.rdbColorDodge);
            this.groupBox1.Controls.Add(this.rdbSoftLight);
            this.groupBox1.Controls.Add(this.rdbHardLight);
            this.groupBox1.Controls.Add(this.rdbOverlay);
            this.groupBox1.Controls.Add(this.rdbScreen);
            this.groupBox1.Controls.Add(this.rdbMultiply);
            this.groupBox1.Location = new System.Drawing.Point(347, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 180);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modes";
            // 
            // rdbLighten
            // 
            this.rdbLighten.AutoSize = true;
            this.rdbLighten.Location = new System.Drawing.Point(106, 157);
            this.rdbLighten.Name = "rdbLighten";
            this.rdbLighten.Size = new System.Drawing.Size(60, 17);
            this.rdbLighten.TabIndex = 13;
            this.rdbLighten.Text = "Lighten";
            this.rdbLighten.UseVisualStyleBackColor = true;
            this.rdbLighten.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbDarken
            // 
            this.rdbDarken.AutoSize = true;
            this.rdbDarken.Location = new System.Drawing.Point(106, 134);
            this.rdbDarken.Name = "rdbDarken";
            this.rdbDarken.Size = new System.Drawing.Size(60, 17);
            this.rdbDarken.TabIndex = 12;
            this.rdbDarken.Text = "Darken";
            this.rdbDarken.UseVisualStyleBackColor = true;
            this.rdbDarken.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbDifference
            // 
            this.rdbDifference.AutoSize = true;
            this.rdbDifference.Location = new System.Drawing.Point(106, 111);
            this.rdbDifference.Name = "rdbDifference";
            this.rdbDifference.Size = new System.Drawing.Size(74, 17);
            this.rdbDifference.TabIndex = 11;
            this.rdbDifference.Text = "Difference";
            this.rdbDifference.UseVisualStyleBackColor = true;
            this.rdbDifference.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbLinearLight
            // 
            this.rdbLinearLight.AutoSize = true;
            this.rdbLinearLight.Location = new System.Drawing.Point(106, 88);
            this.rdbLinearLight.Name = "rdbLinearLight";
            this.rdbLinearLight.Size = new System.Drawing.Size(80, 17);
            this.rdbLinearLight.TabIndex = 10;
            this.rdbLinearLight.Text = "Linear Light";
            this.rdbLinearLight.UseVisualStyleBackColor = true;
            this.rdbLinearLight.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbVividLight
            // 
            this.rdbVividLight.AutoSize = true;
            this.rdbVividLight.Location = new System.Drawing.Point(106, 65);
            this.rdbVividLight.Name = "rdbVividLight";
            this.rdbVividLight.Size = new System.Drawing.Size(74, 17);
            this.rdbVividLight.TabIndex = 9;
            this.rdbVividLight.Text = "Vivid Light";
            this.rdbVividLight.UseVisualStyleBackColor = true;
            this.rdbVividLight.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbLinearBurn
            // 
            this.rdbLinearBurn.AutoSize = true;
            this.rdbLinearBurn.Location = new System.Drawing.Point(106, 42);
            this.rdbLinearBurn.Name = "rdbLinearBurn";
            this.rdbLinearBurn.Size = new System.Drawing.Size(79, 17);
            this.rdbLinearBurn.TabIndex = 8;
            this.rdbLinearBurn.Text = "Linear Burn";
            this.rdbLinearBurn.UseVisualStyleBackColor = true;
            this.rdbLinearBurn.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbColorBurn
            // 
            this.rdbColorBurn.AutoSize = true;
            this.rdbColorBurn.Location = new System.Drawing.Point(106, 19);
            this.rdbColorBurn.Name = "rdbColorBurn";
            this.rdbColorBurn.Size = new System.Drawing.Size(74, 17);
            this.rdbColorBurn.TabIndex = 7;
            this.rdbColorBurn.Text = "Color Burn";
            this.rdbColorBurn.UseVisualStyleBackColor = true;
            this.rdbColorBurn.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbLinearDodge
            // 
            this.rdbLinearDodge.AutoSize = true;
            this.rdbLinearDodge.Location = new System.Drawing.Point(6, 157);
            this.rdbLinearDodge.Name = "rdbLinearDodge";
            this.rdbLinearDodge.Size = new System.Drawing.Size(89, 17);
            this.rdbLinearDodge.TabIndex = 6;
            this.rdbLinearDodge.Text = "Linear Dodge";
            this.rdbLinearDodge.UseVisualStyleBackColor = true;
            this.rdbLinearDodge.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbColorDodge
            // 
            this.rdbColorDodge.AutoSize = true;
            this.rdbColorDodge.Location = new System.Drawing.Point(6, 134);
            this.rdbColorDodge.Name = "rdbColorDodge";
            this.rdbColorDodge.Size = new System.Drawing.Size(84, 17);
            this.rdbColorDodge.TabIndex = 5;
            this.rdbColorDodge.Text = "Color Dodge";
            this.rdbColorDodge.UseVisualStyleBackColor = true;
            this.rdbColorDodge.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbSoftLight
            // 
            this.rdbSoftLight.AutoSize = true;
            this.rdbSoftLight.Location = new System.Drawing.Point(6, 111);
            this.rdbSoftLight.Name = "rdbSoftLight";
            this.rdbSoftLight.Size = new System.Drawing.Size(70, 17);
            this.rdbSoftLight.TabIndex = 4;
            this.rdbSoftLight.Text = "Soft Light";
            this.rdbSoftLight.UseVisualStyleBackColor = true;
            this.rdbSoftLight.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbHardLight
            // 
            this.rdbHardLight.AutoSize = true;
            this.rdbHardLight.Location = new System.Drawing.Point(6, 88);
            this.rdbHardLight.Name = "rdbHardLight";
            this.rdbHardLight.Size = new System.Drawing.Size(74, 17);
            this.rdbHardLight.TabIndex = 3;
            this.rdbHardLight.Text = "Hard Light";
            this.rdbHardLight.UseVisualStyleBackColor = true;
            this.rdbHardLight.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbOverlay
            // 
            this.rdbOverlay.AutoSize = true;
            this.rdbOverlay.Location = new System.Drawing.Point(6, 65);
            this.rdbOverlay.Name = "rdbOverlay";
            this.rdbOverlay.Size = new System.Drawing.Size(61, 17);
            this.rdbOverlay.TabIndex = 2;
            this.rdbOverlay.Text = "Overlay";
            this.rdbOverlay.UseVisualStyleBackColor = true;
            this.rdbOverlay.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbScreen
            // 
            this.rdbScreen.AutoSize = true;
            this.rdbScreen.Location = new System.Drawing.Point(6, 42);
            this.rdbScreen.Name = "rdbScreen";
            this.rdbScreen.Size = new System.Drawing.Size(59, 17);
            this.rdbScreen.TabIndex = 1;
            this.rdbScreen.Text = "Screen";
            this.rdbScreen.UseVisualStyleBackColor = true;
            this.rdbScreen.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // rdbMultiply
            // 
            this.rdbMultiply.AutoSize = true;
            this.rdbMultiply.Checked = true;
            this.rdbMultiply.Location = new System.Drawing.Point(6, 19);
            this.rdbMultiply.Name = "rdbMultiply";
            this.rdbMultiply.Size = new System.Drawing.Size(60, 17);
            this.rdbMultiply.TabIndex = 0;
            this.rdbMultiply.TabStop = true;
            this.rdbMultiply.Text = "Multiply";
            this.rdbMultiply.UseVisualStyleBackColor = true;
            this.rdbMultiply.CheckedChanged += new System.EventHandler(this.BlendModes_CheckedChanged);
            // 
            // BlendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 262);
            this.Controls.Add(this.groupBox1);
            this.Name = "BlendForm";
            this.Text = "Blend Palette";
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.chkPreview, 0);
            this.Controls.SetChildIndex(this.gbxGrayscale, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.gbxGrayscale.ResumeLayout(false);
            this.gbxGrayscale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbBlue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbMultiply;
        private System.Windows.Forms.RadioButton rdbLinearLight;
        private System.Windows.Forms.RadioButton rdbVividLight;
        private System.Windows.Forms.RadioButton rdbLinearBurn;
        private System.Windows.Forms.RadioButton rdbColorBurn;
        private System.Windows.Forms.RadioButton rdbLinearDodge;
        private System.Windows.Forms.RadioButton rdbColorDodge;
        private System.Windows.Forms.RadioButton rdbSoftLight;
        private System.Windows.Forms.RadioButton rdbHardLight;
        private System.Windows.Forms.RadioButton rdbOverlay;
        private System.Windows.Forms.RadioButton rdbScreen;
        private System.Windows.Forms.RadioButton rdbLighten;
        private System.Windows.Forms.RadioButton rdbDarken;
        private System.Windows.Forms.RadioButton rdbDifference;

    }
}