namespace MushROMs.SNESControls.PaletteEditor
{
    partial class GrayscaleForm
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
            this.btnLuma = new System.Windows.Forms.Button();
            this.btnEven = new System.Windows.Forms.Button();
            this.gbxGrayscale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxGrayscale
            // 
            this.gbxGrayscale.Size = new System.Drawing.Size(388, 211);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(325, 229);
            this.btnCancel.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(244, 229);
            this.btnOK.TabIndex = 4;
            // 
            // ltbGreen
            // 
            this.ltbGreen.Size = new System.Drawing.Size(330, 45);
            // 
            // ntbGreen
            // 
            this.ntbGreen.Location = new System.Drawing.Point(342, 96);
            // 
            // ltbRed
            // 
            this.ltbRed.Size = new System.Drawing.Size(330, 45);
            // 
            // ntbRed
            // 
            this.ntbRed.Location = new System.Drawing.Point(342, 32);
            // 
            // ltbBlue
            // 
            this.ltbBlue.Size = new System.Drawing.Size(330, 45);
            // 
            // ntbBlue
            // 
            this.ntbBlue.Location = new System.Drawing.Point(342, 160);
            // 
            // btnLuma
            // 
            this.btnLuma.Location = new System.Drawing.Point(163, 229);
            this.btnLuma.Name = "btnLuma";
            this.btnLuma.Size = new System.Drawing.Size(75, 23);
            this.btnLuma.TabIndex = 3;
            this.btnLuma.Text = "&Luma";
            this.btnLuma.UseVisualStyleBackColor = true;
            this.btnLuma.Click += new System.EventHandler(this.Luma_Click);
            // 
            // btnEven
            // 
            this.btnEven.Location = new System.Drawing.Point(82, 229);
            this.btnEven.Name = "btnEven";
            this.btnEven.Size = new System.Drawing.Size(75, 23);
            this.btnEven.TabIndex = 2;
            this.btnEven.Text = "&Even";
            this.btnEven.UseVisualStyleBackColor = true;
            this.btnEven.Click += new System.EventHandler(this.Even_Click);
            // 
            // GrayscaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 264);
            this.Controls.Add(this.btnEven);
            this.Controls.Add(this.btnLuma);
            this.Name = "GrayscaleForm";
            this.Text = "Custom Grayscale";
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.chkPreview, 0);
            this.Controls.SetChildIndex(this.gbxGrayscale, 0);
            this.Controls.SetChildIndex(this.btnLuma, 0);
            this.Controls.SetChildIndex(this.btnEven, 0);
            this.gbxGrayscale.ResumeLayout(false);
            this.gbxGrayscale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ltbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltbBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLuma;
        private System.Windows.Forms.Button btnEven;
    }
}