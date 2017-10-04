namespace MushROMs.SNESControls.PaletteEditor
{
    partial class PaletteSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteSettingsForm));
            this.gbxMarker = new System.Windows.Forms.GroupBox();
            this.lblDashColor2 = new System.Windows.Forms.Label();
            this.lblDashColor1 = new System.Windows.Forms.Label();
            this.nudDashLength2 = new System.Windows.Forms.NumericUpDown();
            this.nudDashLength1 = new System.Windows.Forms.NumericUpDown();
            this.lblDashLength2 = new System.Windows.Forms.Label();
            this.lblDashLength1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbxBackground = new System.Windows.Forms.GroupBox();
            this.cbxBackZoom = new System.Windows.Forms.ComboBox();
            this.lblBackZoomSize = new System.Windows.Forms.Label();
            this.lblBackColor2 = new System.Windows.Forms.Label();
            this.lblBackColor1 = new System.Windows.Forms.Label();
            this.gbxSizing = new System.Windows.Forms.GroupBox();
            this.cbxZoom = new System.Windows.Forms.ComboBox();
            this.lblZoomSize = new System.Windows.Forms.Label();
            this.nudColumns = new System.Windows.Forms.NumericUpDown();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.lblNumColumns = new System.Windows.Forms.Label();
            this.lblNumRows = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.cpkDashColor2 = new MushROMs.Controls.ColorValueControl();
            this.cpkDashColor1 = new MushROMs.Controls.ColorValueControl();
            this.cpkBackColor2 = new MushROMs.Controls.ColorValueControl();
            this.cpkBackColor1 = new MushROMs.Controls.ColorValueControl();
            this.drwBGExample = new MushROMs.Controls.DrawControl();
            this.gbxMarker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength1)).BeginInit();
            this.gbxBackground.SuspendLayout();
            this.gbxSizing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxMarker
            // 
            this.gbxMarker.Controls.Add(this.lblDashColor2);
            this.gbxMarker.Controls.Add(this.lblDashColor1);
            this.gbxMarker.Controls.Add(this.cpkDashColor2);
            this.gbxMarker.Controls.Add(this.cpkDashColor1);
            this.gbxMarker.Controls.Add(this.nudDashLength2);
            this.gbxMarker.Controls.Add(this.nudDashLength1);
            this.gbxMarker.Controls.Add(this.lblDashLength2);
            this.gbxMarker.Controls.Add(this.lblDashLength1);
            resources.ApplyResources(this.gbxMarker, "gbxMarker");
            this.gbxMarker.Name = "gbxMarker";
            this.gbxMarker.TabStop = false;
            // 
            // lblDashColor2
            // 
            resources.ApplyResources(this.lblDashColor2, "lblDashColor2");
            this.lblDashColor2.Name = "lblDashColor2";
            // 
            // lblDashColor1
            // 
            resources.ApplyResources(this.lblDashColor1, "lblDashColor1");
            this.lblDashColor1.Name = "lblDashColor1";
            // 
            // nudDashLength2
            // 
            resources.ApplyResources(this.nudDashLength2, "nudDashLength2");
            this.nudDashLength2.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudDashLength2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDashLength2.Name = "nudDashLength2";
            this.nudDashLength2.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // nudDashLength1
            // 
            resources.ApplyResources(this.nudDashLength1, "nudDashLength1");
            this.nudDashLength1.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudDashLength1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDashLength1.Name = "nudDashLength1";
            this.nudDashLength1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblDashLength2
            // 
            resources.ApplyResources(this.lblDashLength2, "lblDashLength2");
            this.lblDashLength2.Name = "lblDashLength2";
            // 
            // lblDashLength1
            // 
            resources.ApplyResources(this.lblDashLength1, "lblDashLength1");
            this.lblDashLength1.Name = "lblDashLength1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gbxBackground
            // 
            this.gbxBackground.Controls.Add(this.cpkBackColor2);
            this.gbxBackground.Controls.Add(this.cpkBackColor1);
            this.gbxBackground.Controls.Add(this.drwBGExample);
            this.gbxBackground.Controls.Add(this.cbxBackZoom);
            this.gbxBackground.Controls.Add(this.lblBackZoomSize);
            this.gbxBackground.Controls.Add(this.lblBackColor2);
            this.gbxBackground.Controls.Add(this.lblBackColor1);
            resources.ApplyResources(this.gbxBackground, "gbxBackground");
            this.gbxBackground.Name = "gbxBackground";
            this.gbxBackground.TabStop = false;
            // 
            // cbxBackZoom
            // 
            this.cbxBackZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBackZoom.FormattingEnabled = true;
            this.cbxBackZoom.Items.AddRange(new object[] {
            resources.GetString("cbxBackZoom.Items"),
            resources.GetString("cbxBackZoom.Items1"),
            resources.GetString("cbxBackZoom.Items2"),
            resources.GetString("cbxBackZoom.Items3")});
            resources.ApplyResources(this.cbxBackZoom, "cbxBackZoom");
            this.cbxBackZoom.Name = "cbxBackZoom";
            this.cbxBackZoom.SelectedIndexChanged += new System.EventHandler(this.RedrawExample);
            // 
            // lblBackZoomSize
            // 
            resources.ApplyResources(this.lblBackZoomSize, "lblBackZoomSize");
            this.lblBackZoomSize.Name = "lblBackZoomSize";
            // 
            // lblBackColor2
            // 
            resources.ApplyResources(this.lblBackColor2, "lblBackColor2");
            this.lblBackColor2.Name = "lblBackColor2";
            // 
            // lblBackColor1
            // 
            resources.ApplyResources(this.lblBackColor1, "lblBackColor1");
            this.lblBackColor1.Name = "lblBackColor1";
            // 
            // gbxSizing
            // 
            this.gbxSizing.Controls.Add(this.cbxZoom);
            this.gbxSizing.Controls.Add(this.lblZoomSize);
            this.gbxSizing.Controls.Add(this.nudColumns);
            this.gbxSizing.Controls.Add(this.nudRows);
            this.gbxSizing.Controls.Add(this.lblNumColumns);
            this.gbxSizing.Controls.Add(this.lblNumRows);
            resources.ApplyResources(this.gbxSizing, "gbxSizing");
            this.gbxSizing.Name = "gbxSizing";
            this.gbxSizing.TabStop = false;
            // 
            // cbxZoom
            // 
            this.cbxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZoom.FormattingEnabled = true;
            this.cbxZoom.Items.AddRange(new object[] {
            resources.GetString("cbxZoom.Items"),
            resources.GetString("cbxZoom.Items1"),
            resources.GetString("cbxZoom.Items2"),
            resources.GetString("cbxZoom.Items3")});
            resources.ApplyResources(this.cbxZoom, "cbxZoom");
            this.cbxZoom.Name = "cbxZoom";
            // 
            // lblZoomSize
            // 
            resources.ApplyResources(this.lblZoomSize, "lblZoomSize");
            this.lblZoomSize.Name = "lblZoomSize";
            // 
            // nudColumns
            // 
            resources.ApplyResources(this.nudColumns, "nudColumns");
            this.nudColumns.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudColumns.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudColumns.Name = "nudColumns";
            this.nudColumns.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nudRows
            // 
            resources.ApplyResources(this.nudRows, "nudRows");
            this.nudRows.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudRows.Name = "nudRows";
            this.nudRows.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // lblNumColumns
            // 
            resources.ApplyResources(this.lblNumColumns, "lblNumColumns");
            this.lblNumColumns.Name = "lblNumColumns";
            // 
            // lblNumRows
            // 
            resources.ApplyResources(this.lblNumRows, "lblNumRows");
            this.lblNumRows.Name = "lblNumRows";
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cpkDashColor2
            // 
            resources.ApplyResources(this.cpkDashColor2, "cpkDashColor2");
            this.cpkDashColor2.BackColor = System.Drawing.Color.Magenta;
            this.cpkDashColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpkDashColor2.Name = "cpkDashColor2";
            this.cpkDashColor2.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkDashColor2.UserImageLocation = new System.Drawing.Point(0, 0);
            this.cpkDashColor2.UserImageSize = new System.Drawing.Size(150, 150);
            // 
            // cpkDashColor1
            // 
            resources.ApplyResources(this.cpkDashColor1, "cpkDashColor1");
            this.cpkDashColor1.BackColor = System.Drawing.Color.Magenta;
            this.cpkDashColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpkDashColor1.Name = "cpkDashColor1";
            this.cpkDashColor1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkDashColor1.UserImageLocation = new System.Drawing.Point(0, 0);
            this.cpkDashColor1.UserImageSize = new System.Drawing.Size(150, 150);
            // 
            // cpkBackColor2
            // 
            this.cpkBackColor2.BackColor = System.Drawing.Color.Magenta;
            this.cpkBackColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.cpkBackColor2, "cpkBackColor2");
            this.cpkBackColor2.Name = "cpkBackColor2";
            this.cpkBackColor2.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkBackColor2.UserImageLocation = new System.Drawing.Point(0, 0);
            this.cpkBackColor2.UserImageSize = new System.Drawing.Size(150, 150);
            this.cpkBackColor2.ColorValueChanged += new System.EventHandler(this.RedrawExample);
            // 
            // cpkBackColor1
            // 
            this.cpkBackColor1.BackColor = System.Drawing.Color.Magenta;
            this.cpkBackColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.cpkBackColor1, "cpkBackColor1");
            this.cpkBackColor1.Name = "cpkBackColor1";
            this.cpkBackColor1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkBackColor1.UserImageLocation = new System.Drawing.Point(0, 0);
            this.cpkBackColor1.UserImageSize = new System.Drawing.Size(150, 150);
            this.cpkBackColor1.ColorValueChanged += new System.EventHandler(this.RedrawExample);
            // 
            // drwBGExample
            // 
            this.drwBGExample.BackColor = System.Drawing.Color.Magenta;
            this.drwBGExample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.drwBGExample, "drwBGExample");
            this.drwBGExample.Name = "drwBGExample";
            this.drwBGExample.UseCustomImageRectange = true;
            this.drwBGExample.UserImageLocation = new System.Drawing.Point(8, 8);
            this.drwBGExample.UserImageSize = new System.Drawing.Size(32, 32);
            this.drwBGExample.WritePixels += new System.EventHandler(this.drwBGExample_WritePixels);
            this.drwBGExample.Paint += new System.Windows.Forms.PaintEventHandler(this.drwBGExample_Paint);
            // 
            // PaletteSettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.gbxMarker);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxBackground);
            this.Controls.Add(this.gbxSizing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteSettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.gbxMarker.ResumeLayout(false);
            this.gbxMarker.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength1)).EndInit();
            this.gbxBackground.ResumeLayout(false);
            this.gbxBackground.PerformLayout();
            this.gbxSizing.ResumeLayout(false);
            this.gbxSizing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxMarker;
        private System.Windows.Forms.Label lblDashColor2;
        private System.Windows.Forms.Label lblDashColor1;
        private Controls.ColorValueControl cpkDashColor2;
        private Controls.ColorValueControl cpkDashColor1;
        private System.Windows.Forms.NumericUpDown nudDashLength2;
        private System.Windows.Forms.NumericUpDown nudDashLength1;
        private System.Windows.Forms.Label lblDashLength2;
        private System.Windows.Forms.Label lblDashLength1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbxBackground;
        private Controls.ColorValueControl cpkBackColor2;
        private Controls.ColorValueControl cpkBackColor1;
        private Controls.DrawControl drwBGExample;
        private System.Windows.Forms.ComboBox cbxBackZoom;
        private System.Windows.Forms.Label lblBackZoomSize;
        private System.Windows.Forms.Label lblBackColor2;
        private System.Windows.Forms.Label lblBackColor1;
        private System.Windows.Forms.GroupBox gbxSizing;
        private System.Windows.Forms.ComboBox cbxZoom;
        private System.Windows.Forms.Label lblZoomSize;
        private System.Windows.Forms.NumericUpDown nudColumns;
        private System.Windows.Forms.NumericUpDown nudRows;
        private System.Windows.Forms.Label lblNumColumns;
        private System.Windows.Forms.Label lblNumRows;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnApply;

    }
}