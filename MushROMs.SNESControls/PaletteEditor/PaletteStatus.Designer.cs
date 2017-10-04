namespace MushROMs.SNESControls.PaletteEditor
{
    partial class PaletteStatus
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
            this.gbxSelectedColor = new System.Windows.Forms.GroupBox();
            this.lblPcValueText = new System.Windows.Forms.Label();
            this.lblPcValue = new System.Windows.Forms.Label();
            this.lblSnesValueText = new System.Windows.Forms.Label();
            this.lblSnesValue = new System.Windows.Forms.Label();
            this.lblGreenValue = new System.Windows.Forms.Label();
            this.lblRedValue = new System.Windows.Forms.Label();
            this.lblBlueValue = new System.Windows.Forms.Label();
            this.gbxZoom = new System.Windows.Forms.GroupBox();
            this.cbxZoom = new System.Windows.Forms.ComboBox();
            this.gbxROMViewing = new System.Windows.Forms.GroupBox();
            this.btnNextByte = new System.Windows.Forms.Button();
            this.btnLastByte = new System.Windows.Forms.Button();
            this.gbxSelectedColor.SuspendLayout();
            this.gbxZoom.SuspendLayout();
            this.gbxROMViewing.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSelectedColor
            // 
            this.gbxSelectedColor.Controls.Add(this.lblPcValueText);
            this.gbxSelectedColor.Controls.Add(this.lblPcValue);
            this.gbxSelectedColor.Controls.Add(this.lblSnesValueText);
            this.gbxSelectedColor.Controls.Add(this.lblSnesValue);
            this.gbxSelectedColor.Controls.Add(this.lblGreenValue);
            this.gbxSelectedColor.Controls.Add(this.lblRedValue);
            this.gbxSelectedColor.Controls.Add(this.lblBlueValue);
            this.gbxSelectedColor.Location = new System.Drawing.Point(3, 3);
            this.gbxSelectedColor.Name = "gbxSelectedColor";
            this.gbxSelectedColor.Size = new System.Drawing.Size(126, 76);
            this.gbxSelectedColor.TabIndex = 5;
            this.gbxSelectedColor.TabStop = false;
            this.gbxSelectedColor.Text = "Selected color";
            // 
            // lblPcValueText
            // 
            this.lblPcValueText.AutoSize = true;
            this.lblPcValueText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPcValueText.Location = new System.Drawing.Point(6, 19);
            this.lblPcValueText.Margin = new System.Windows.Forms.Padding(3);
            this.lblPcValueText.Name = "lblPcValueText";
            this.lblPcValueText.Size = new System.Drawing.Size(24, 13);
            this.lblPcValueText.TabIndex = 0;
            this.lblPcValueText.Text = "PC:";
            // 
            // lblPcValue
            // 
            this.lblPcValue.AutoSize = true;
            this.lblPcValue.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.lblPcValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPcValue.Location = new System.Drawing.Point(57, 18);
            this.lblPcValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblPcValue.Name = "lblPcValue";
            this.lblPcValue.Size = new System.Drawing.Size(63, 15);
            this.lblPcValue.TabIndex = 0;
            this.lblPcValue.Text = "0x000000";
            // 
            // lblSnesValueText
            // 
            this.lblSnesValueText.AutoSize = true;
            this.lblSnesValueText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSnesValueText.Location = new System.Drawing.Point(6, 38);
            this.lblSnesValueText.Margin = new System.Windows.Forms.Padding(3);
            this.lblSnesValueText.Name = "lblSnesValueText";
            this.lblSnesValueText.Size = new System.Drawing.Size(39, 13);
            this.lblSnesValueText.TabIndex = 0;
            this.lblSnesValueText.Text = "SNES:";
            // 
            // lblSnesValue
            // 
            this.lblSnesValue.AutoSize = true;
            this.lblSnesValue.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.lblSnesValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSnesValue.Location = new System.Drawing.Point(57, 37);
            this.lblSnesValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblSnesValue.Name = "lblSnesValue";
            this.lblSnesValue.Size = new System.Drawing.Size(49, 15);
            this.lblSnesValue.TabIndex = 0;
            this.lblSnesValue.Text = "0x0000";
            // 
            // lblGreenValue
            // 
            this.lblGreenValue.AutoSize = true;
            this.lblGreenValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblGreenValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGreenValue.Location = new System.Drawing.Point(57, 57);
            this.lblGreenValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblGreenValue.Name = "lblGreenValue";
            this.lblGreenValue.Size = new System.Drawing.Size(13, 13);
            this.lblGreenValue.TabIndex = 0;
            this.lblGreenValue.Text = "0";
            // 
            // lblRedValue
            // 
            this.lblRedValue.AutoSize = true;
            this.lblRedValue.ForeColor = System.Drawing.Color.Red;
            this.lblRedValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRedValue.Location = new System.Drawing.Point(6, 57);
            this.lblRedValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblRedValue.Name = "lblRedValue";
            this.lblRedValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRedValue.Size = new System.Drawing.Size(13, 13);
            this.lblRedValue.TabIndex = 0;
            this.lblRedValue.Text = "0";
            // 
            // lblBlueValue
            // 
            this.lblBlueValue.AutoSize = true;
            this.lblBlueValue.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBlueValue.Location = new System.Drawing.Point(93, 57);
            this.lblBlueValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblBlueValue.Name = "lblBlueValue";
            this.lblBlueValue.Size = new System.Drawing.Size(13, 13);
            this.lblBlueValue.TabIndex = 0;
            this.lblBlueValue.Text = "0";
            // 
            // gbxZoom
            // 
            this.gbxZoom.Controls.Add(this.cbxZoom);
            this.gbxZoom.Location = new System.Drawing.Point(3, 85);
            this.gbxZoom.Name = "gbxZoom";
            this.gbxZoom.Size = new System.Drawing.Size(56, 44);
            this.gbxZoom.TabIndex = 7;
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
            // gbxROMViewing
            // 
            this.gbxROMViewing.Controls.Add(this.btnNextByte);
            this.gbxROMViewing.Controls.Add(this.btnLastByte);
            this.gbxROMViewing.Location = new System.Drawing.Point(65, 85);
            this.gbxROMViewing.Name = "gbxROMViewing";
            this.gbxROMViewing.Size = new System.Drawing.Size(64, 44);
            this.gbxROMViewing.TabIndex = 9;
            this.gbxROMViewing.TabStop = false;
            this.gbxROMViewing.Text = "ROM";
            // 
            // btnNextByte
            // 
            this.btnNextByte.AutoSize = true;
            this.btnNextByte.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNextByte.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.btnNextByte.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNextByte.Location = new System.Drawing.Point(35, 15);
            this.btnNextByte.Name = "btnNextByte";
            this.btnNextByte.Size = new System.Drawing.Size(23, 23);
            this.btnNextByte.TabIndex = 1;
            this.btnNextByte.Text = "+";
            this.btnNextByte.UseVisualStyleBackColor = true;
            // 
            // btnLastByte
            // 
            this.btnLastByte.AutoSize = true;
            this.btnLastByte.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLastByte.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.btnLastByte.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLastByte.Location = new System.Drawing.Point(6, 15);
            this.btnLastByte.Name = "btnLastByte";
            this.btnLastByte.Size = new System.Drawing.Size(23, 23);
            this.btnLastByte.TabIndex = 0;
            this.btnLastByte.Text = "-";
            this.btnLastByte.UseVisualStyleBackColor = true;
            // 
            // PaletteOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxSelectedColor);
            this.Controls.Add(this.gbxZoom);
            this.Controls.Add(this.gbxROMViewing);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PaletteOptions";
            this.Size = new System.Drawing.Size(132, 132);
            this.gbxSelectedColor.ResumeLayout(false);
            this.gbxSelectedColor.PerformLayout();
            this.gbxZoom.ResumeLayout(false);
            this.gbxROMViewing.ResumeLayout(false);
            this.gbxROMViewing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxSelectedColor;
        private System.Windows.Forms.Label lblPcValueText;
        private System.Windows.Forms.Label lblPcValue;
        private System.Windows.Forms.Label lblSnesValueText;
        private System.Windows.Forms.Label lblSnesValue;
        private System.Windows.Forms.Label lblGreenValue;
        private System.Windows.Forms.Label lblRedValue;
        private System.Windows.Forms.Label lblBlueValue;
        private System.Windows.Forms.GroupBox gbxZoom;
        private System.Windows.Forms.ComboBox cbxZoom;
        private System.Windows.Forms.GroupBox gbxROMViewing;
        private System.Windows.Forms.Button btnNextByte;
        private System.Windows.Forms.Button btnLastByte;
    }
}
