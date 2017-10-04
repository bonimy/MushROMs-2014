using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNES;
using MushROMs.SNESControls.PaletteEditor;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor.PaletteEditor
{
    public unsafe partial class PaletteForm : EditorForm
    {
        private readonly int RemainderWidth;
        private readonly int RemainderHeight;

        IEditorData preview;
        private BlendForm blendDialog;
        private ColorizeForm colorizeDialog;
        private GrayscaleForm grayscaleDialog;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Palette Palette
        {
            get { return this.paletteControl.Palette; }
        }

        public override IEditorControl MainEditorControl
        {
            get { return this.PaletteControl; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaletteControl PaletteControl
        {
            get { return this.paletteControl; }
        }

        public PaletteZoomScales PaletteZoomScale
        {
            get { return this.paletteStatus.PaletteZoomScale; }
            set { this.paletteStatus.PaletteZoomScale = value; }
        }

        public MushROMs.SNESControls.Properties.Settings ControlSettings
        {
            get { return MushROMs.SNESControls.Properties.Settings.Default; }
        }

        public override string Status
        {
            set { this.tssMain.Text = value; }
        }

        public PaletteForm()
        {
            InitializeComponent();

            this.InvokeSetEditorSizeFromForm = new MethodInvoker(SetEditorSizeFromForm);
            this.InvokeSetFormSizeFromEditor = new MethodInvoker(SetFormSizeFromEditor);
            this.InvokeUpdateStatus = new MethodInvoker(UpdateStatus);
            this.InvokeGoTo = new MethodInvoker(GoTo);

            this.paletteStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.hsbPalette.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.vsbPalette.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            this.RemainderWidth = this.ClientSize.Width - this.MainEditorControl.ClientSize.Width;
            this.RemainderHeight = this.ClientSize.Height - this.MainEditorControl.ClientSize.Height;

            this.Palette.FileFormatChanged += new EventHandler(Palette_FileFormatChanged);

            this.SaveFileDialog.Title = Resources.SavePaletteTitle;

            this.colorizeDialog = new ColorizeForm();
            this.colorizeDialog.ColorValueChanged += new EventHandler(ColorizeDialog_ColorValueChanged);

            this.grayscaleDialog = new GrayscaleForm();
            this.grayscaleDialog.ColorValueChanged += new EventHandler(GrayscaleDialog_ColorValueChanged);

            this.blendDialog = new BlendForm();
            this.blendDialog.ColorValueChanged += new EventHandler(BlendDialog_ColorValueChanged);
        }

        private void PaletteForm_Load(object sender, EventArgs e)
        {
            this.PaletteZoomScale = (PaletteZoomScales)this.ControlSettings.PaletteZoomScale;
        }

        protected void UpdateStatus()
        {
            if (this.Editor.Zero.Address < 0 || this.Editor.Active.Index >= this.Editor.MapLength)
                return;

            this.paletteStatus.ActiveColor = *this.Palette[this.Palette.Active.Address];
            
            StringBuilder sb = new StringBuilder();
            if (this.Palette.FileFormat == PaletteFileFormats.SNES)
            {
                sb.Append(Resources.TextPCAddress);
                sb.Append(": 0x");
                sb.Append(this.Editor.Selection.Min.Address.ToString("X6"));
            }
            else
            {
                sb.Append(Resources.TextStartIndex);
                sb.Append(": 0x");
                sb.Append(this.Editor.Selection.Min.Index.ToString("X"));
            }
            sb.Append(", ");
            sb.Append(Resources.TextWidth);
            sb.Append(": 0x");
            sb.Append(this.Editor.Selection.Width.ToString("X"));

            sb.Append(", ");
            sb.Append(Resources.TextHeight);
            sb.Append(": 0x");
            sb.Append(this.Editor.Selection.Height.ToString("X"));

            this.Status = sb.ToString();
        }

        public override void SaveEditorAs()
        {
            this.SaveFileDialog.Filter = Palette.CreateFilter(this.Palette.FileFormat);
            this.SaveFileDialog.DefaultExt = Palette.GetExtension(this.Palette.FileFormat);
            base.SaveEditorAs();
        }

        public void GoTo()
        {
            GoToForm dlg = new GoToForm();
            dlg.Address = this.Editor.Zero.Address;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.Editor.Zero.Address = dlg.Address;
        }

        private void SetFormSizeFromEditor()
        {
            this.ClientSize = new Size(
                this.Editor.VisibleSize.Width + this.RemainderWidth,
                this.Editor.VisibleSize.Height + this.RemainderHeight);
        }

        private void SetEditorSizeFromForm()
        {
            this.Editor.ViewSize = new Size(
                (this.ClientSize.Width - this.RemainderWidth) / this.Editor.CellSize.Width,
                (this.ClientSize.Height - this.RemainderHeight) / this.Editor.CellSize.Height);
        }

        private void Palette_FileFormatChanged(object sender, EventArgs e)
        {
            this.paletteStatus.ShowAddressScrolling = this.Palette.FileFormat == PaletteFileFormats.SNES;
        }

        private void PaletteOptions_ZoomScaleChanged(object sender, EventArgs e)
        {
            int zoom = (int)this.PaletteZoomScale;
            this.Palette.ZoomSize = new Size(zoom, zoom);
        }

        private void PaletteOptions_NextByteClick(object sender, EventArgs e)
        {
            this.Palette.Zero.Address += 1;
        }

        private void PaletteOptions_LastByteClick(object sender, EventArgs e)
        {
            if (this.Palette.Zero.Address > 0)
                this.Palette.Zero.Address -= 1;
        }

        private void PaletteControl_TileMouseDoubleClick(object sender, MouseEventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = LC.SNESToSystemColor(*this.Palette[this.Palette.Active.Address]);
            dlg.FullOpen = true;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.Palette.EditPaletteColor(this.Palette.Active.Address, LC.SystemToSNESColor(dlg.Color));
        }

        public void Colorize()
        {
            this.preview = this.Palette.CreateCopy();
            DialogResult result = this.colorizeDialog.ShowDialog();
            this.Palette.ModifyData(this.preview, false, false);

            if (result == DialogResult.OK)
            {
                this.Palette.Colorize(this.preview,
                                      this.colorizeDialog.Colorize,
                                      this.colorizeDialog.Hue,
                                      this.colorizeDialog.Saturation,
                                      this.colorizeDialog.Lightness,
                                      this.colorizeDialog.Effectiveness,
                                      false);
            }
        }

        private void ColorizeDialog_ColorValueChanged(object sender, EventArgs e)
        {
            if (this.colorizeDialog.Preview)
                this.Palette.Colorize(this.preview,
                                      this.colorizeDialog.Colorize,
                                      this.colorizeDialog.Hue,
                                      this.colorizeDialog.Saturation,
                                      this.colorizeDialog.Lightness,
                                      this.colorizeDialog.Effectiveness,
                                      true);
            else
                this.Palette.ModifyData(this.preview, false, false);
        }

        public void Grayscale()
        {
            this.preview = this.Palette.CreateCopy();
            DialogResult result = this.grayscaleDialog.ShowDialog();
            this.Palette.ModifyData(this.preview, false, false);

            if (result == DialogResult.OK)
                this.Palette.Grayscale(this.preview,
                                       this.grayscaleDialog.Red,
                                       this.grayscaleDialog.Green,
                                       this.grayscaleDialog.Blue,
                                       false);
        }

        private void GrayscaleDialog_ColorValueChanged(object sender, EventArgs e)
        {
            if (this.grayscaleDialog.Preview)
                this.Palette.Grayscale(this.preview,
                                       this.grayscaleDialog.Red,
                                       this.grayscaleDialog.Green,
                                       this.grayscaleDialog.Blue,
                                       true);
            else
                this.Palette.ModifyData(this.preview, false, false);
        }

        public void Blend()
        {
            this.preview = this.Palette.CreateCopy();
            DialogResult result = this.blendDialog.ShowDialog();
            this.Palette.ModifyData(this.preview, false, false);

            if (result == DialogResult.OK)
                this.Palette.Blend(this.preview, ExpandedColor.FromArgb(this.blendDialog.Red, this.blendDialog.Green, this.blendDialog.Blue), this.blendDialog.BlendMode, false);
        }

        private void BlendDialog_ColorValueChanged(object sender, EventArgs e)
        {
            if (this.blendDialog.Preview)
                this.Palette.Blend(this.preview, ExpandedColor.FromArgb(this.blendDialog.Red, this.blendDialog.Green, this.blendDialog.Blue), this.blendDialog.BlendMode, true);
            else
                this.Palette.ModifyData(this.preview, false, false);
        }

        public void EditBackColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = LC.SNESToSystemColor(this.Palette.BackColor);
            dlg.FullOpen = true;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.Palette.BackColor = LC.SystemToSNESColor(dlg.Color);
        }
    }
}