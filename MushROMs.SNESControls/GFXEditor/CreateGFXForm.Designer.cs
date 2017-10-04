namespace MushROMs.SNESControls.GFXEditor
{
    partial class CreateGFXForm
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
            this.chkFromCopy = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbxGraphicsFormat = new System.Windows.Forms.ComboBox();
            this.gbxNumColors = new System.Windows.Forms.GroupBox();
            this.lblNumColors = new System.Windows.Forms.Label();
            this.nudNumTiles = new System.Windows.Forms.NumericUpDown();
            this.gbxGraphicsFormat = new System.Windows.Forms.GroupBox();
            this.gbxNumColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTiles)).BeginInit();
            this.gbxGraphicsFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkFromCopy
            // 
            this.chkFromCopy.AutoSize = true;
            this.chkFromCopy.Location = new System.Drawing.Point(12, 115);
            this.chkFromCopy.Name = "chkFromCopy";
            this.chkFromCopy.Size = new System.Drawing.Size(130, 17);
            this.chkFromCopy.TabIndex = 2;
            this.chkFromCopy.Text = "Create from copy data";
            this.chkFromCopy.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // cbxGraphicsFormat
            // 
            this.cbxGraphicsFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxGraphicsFormat.FormattingEnabled = true;
            this.cbxGraphicsFormat.ItemHeight = 13;
            this.cbxGraphicsFormat.Items.AddRange(new object[] {
            "SNES 1BPP",
            "SNES 2BPP",
            "SNES 3BPP",
            "SNES 4BPP",
            "SNES 5BPP",
            "SNES 6BPP",
            "SNES 7BPP",
            "SNES 8BPP",
            "Mode 7 8BPP",
            "GBA 4BPP"});
            this.cbxGraphicsFormat.Location = new System.Drawing.Point(6, 19);
            this.cbxGraphicsFormat.Name = "cbxGraphicsFormat";
            this.cbxGraphicsFormat.Size = new System.Drawing.Size(144, 21);
            this.cbxGraphicsFormat.TabIndex = 0;
            // 
            // gbxNumColors
            // 
            this.gbxNumColors.Controls.Add(this.lblNumColors);
            this.gbxNumColors.Controls.Add(this.nudNumTiles);
            this.gbxNumColors.Location = new System.Drawing.Point(12, 12);
            this.gbxNumColors.Name = "gbxNumColors";
            this.gbxNumColors.Size = new System.Drawing.Size(156, 45);
            this.gbxNumColors.TabIndex = 0;
            this.gbxNumColors.TabStop = false;
            this.gbxNumColors.Text = "Number of tiles";
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
            this.nudNumTiles.Hexadecimal = true;
            this.nudNumTiles.Location = new System.Drawing.Point(24, 19);
            this.nudNumTiles.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.nudNumTiles.Maximum = new decimal(new int[] {
            8388608,
            0,
            0,
            0});
            this.nudNumTiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumTiles.Name = "nudNumColors";
            this.nudNumTiles.Size = new System.Drawing.Size(126, 20);
            this.nudNumTiles.TabIndex = 0;
            this.nudNumTiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNumTiles.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // gbxGraphicsFormat
            // 
            this.gbxGraphicsFormat.Controls.Add(this.cbxGraphicsFormat);
            this.gbxGraphicsFormat.Location = new System.Drawing.Point(12, 63);
            this.gbxGraphicsFormat.Name = "gbxGraphicsFormat";
            this.gbxGraphicsFormat.Size = new System.Drawing.Size(156, 46);
            this.gbxGraphicsFormat.TabIndex = 1;
            this.gbxGraphicsFormat.TabStop = false;
            this.gbxGraphicsFormat.Text = "Graphics format";
            // 
            // CreateGFXForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(180, 173);
            this.Controls.Add(this.gbxGraphicsFormat);
            this.Controls.Add(this.gbxNumColors);
            this.Controls.Add(this.chkFromCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateGFXForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Create new GFX";
            this.gbxNumColors.ResumeLayout(false);
            this.gbxNumColors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTiles)).EndInit();
            this.gbxGraphicsFormat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkFromCopy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbxGraphicsFormat;
        private System.Windows.Forms.GroupBox gbxNumColors;
        private System.Windows.Forms.Label lblNumColors;
        private System.Windows.Forms.NumericUpDown nudNumTiles;
        private System.Windows.Forms.GroupBox gbxGraphicsFormat;
    }
}