namespace MushROMs.SNESControls.PaletteEditor
{
    partial class CreatePaletteForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkFromCopy = new System.Windows.Forms.CheckBox();
            this.gbxNumColors = new System.Windows.Forms.GroupBox();
            this.lblNumColors = new System.Windows.Forms.Label();
            this.nudNumColors = new System.Windows.Forms.NumericUpDown();
            this.gbxNumColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumColors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 86);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkFromCopy
            // 
            this.chkFromCopy.AutoSize = true;
            this.chkFromCopy.Location = new System.Drawing.Point(12, 63);
            this.chkFromCopy.Name = "chkFromCopy";
            this.chkFromCopy.Size = new System.Drawing.Size(130, 17);
            this.chkFromCopy.TabIndex = 1;
            this.chkFromCopy.Text = "Create from copy data";
            this.chkFromCopy.UseVisualStyleBackColor = true;
            this.chkFromCopy.CheckedChanged += new System.EventHandler(this.FromCopy_CheckedChanged);
            // 
            // gbxNumColors
            // 
            this.gbxNumColors.Controls.Add(this.lblNumColors);
            this.gbxNumColors.Controls.Add(this.nudNumColors);
            this.gbxNumColors.Location = new System.Drawing.Point(12, 12);
            this.gbxNumColors.Name = "gbxNumColors";
            this.gbxNumColors.Size = new System.Drawing.Size(156, 45);
            this.gbxNumColors.TabIndex = 0;
            this.gbxNumColors.TabStop = false;
            this.gbxNumColors.Text = "Number of colors";
            // 
            // lblNumColors
            // 
            this.lblNumColors.AutoSize = true;
            this.lblNumColors.Location = new System.Drawing.Point(6, 21);
            this.lblNumColors.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblNumColors.Name = "lblNumColors";
            this.lblNumColors.Size = new System.Drawing.Size(18, 13);
            this.lblNumColors.TabIndex = 0;
            this.lblNumColors.Text = "0x";
            // 
            // nudNumColors
            // 
            this.nudNumColors.Hexadecimal = true;
            this.nudNumColors.Location = new System.Drawing.Point(24, 19);
            this.nudNumColors.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.nudNumColors.Maximum = new decimal(new int[] {
            8388608,
            0,
            0,
            0});
            this.nudNumColors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumColors.Name = "nudNumColors";
            this.nudNumColors.Size = new System.Drawing.Size(126, 20);
            this.nudNumColors.TabIndex = 0;
            this.nudNumColors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNumColors.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // CreatePaletteForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(180, 121);
            this.Controls.Add(this.gbxNumColors);
            this.Controls.Add(this.chkFromCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePaletteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Palette";
            this.gbxNumColors.ResumeLayout(false);
            this.gbxNumColors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumColors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkFromCopy;
        private System.Windows.Forms.GroupBox gbxNumColors;
        private System.Windows.Forms.Label lblNumColors;
        private System.Windows.Forms.NumericUpDown nudNumColors;

    }
}