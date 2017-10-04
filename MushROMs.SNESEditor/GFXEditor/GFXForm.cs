using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;
using MushROMs.SNES;
using MushROMs.SNESControls.GFXEditor;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class GFXForm : EditorForm
    {
        private readonly int RemainderWidth;
        private readonly int RemainderHeight;

        private SaveFileDialog sfdGFX;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GFX GFX
        {
            get { return this.gfxControl.GFX; }
        }

        public override IEditorControl MainEditorControl
        {
            get { return this.gfxControl; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GFXControl GFXControl
        {
            get { return this.gfxControl; }
        }

        public GFXZoomScales GFXZoomScale
        {
            get { return this.gfxStatus.GFXZoomScale; }
            set { this.gfxStatus.GFXZoomScale = value; }
        }

        public MushROMs.SNESControls.Properties.Settings ControlSettings
        {
            get { return MushROMs.SNESControls.Properties.Settings.Default; }
        }

        public GraphicsFormats GraphicsFormat
        {
            get { return this.gfxStatus.GraphicsFormat; }
            set { this.gfxStatus.GraphicsFormat = value; }
        }

        public override string Status
        {
            set { this.tssMain.Text = value; }
        }

        public GFXForm()
        {
            InitializeComponent();

            this.InvokeSetEditorSizeFromForm = new MethodInvoker(SetEditorSizeFromForm);
            this.InvokeSetFormSizeFromEditor = new MethodInvoker(SetFormSizeFromEditor);
            this.InvokeUpdateStatus = new MethodInvoker(UpdateStatus);
            this.InvokeGoTo = new MethodInvoker(GoTo);

            this.gfxStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.hsbGFX.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.vsbGFX.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            this.RemainderWidth = this.Width - this.Editor.VisibleSize.Width;
            this.RemainderHeight = this.Height - this.Editor.VisibleSize.Height;

            this.GFX.FileFormatChanged += new EventHandler(GFX_FileFormatChanged);
            this.GFX.FileSaved += new EventHandler(GFX_FileSaved);

            this.sfdGFX = new SaveFileDialog();
            this.sfdGFX.Title = Resources.SaveGFXTitle;
        }

        private void GFXForm_Load(object sender, EventArgs e)
        {
            this.GraphicsFormat = this.ControlSettings.GraphicsFormat;
            this.GFXZoomScale = (GFXZoomScales)this.ControlSettings.GFXZoomScale;
        }

        protected void UpdateStatus()
        {
            if (this.Editor.Zero.Address < 0 || this.Editor.Active.Index >= this.Editor.MapLength)
                return;

            StringBuilder sb = new StringBuilder();
            if (this.GFX.FileFormat == GFXFileFormats.SNES)
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

        protected unsafe override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x0231)
            {
                int* rect = (int*)m.LParam.ToPointer();
                this.rsbRect = new Rectangle(this.Location, this.Size);
            }
            if (m.Msg == 0x0214)
            {
                int* rect = (int*)m.LParam.ToPointer();
                int width = rect[2] - rect[0];
                width -= this.RemainderWidth;
                width = width - (width % this.Editor.CellSize.Width);
                width += this.RemainderWidth;
                if (this.rsbRect.Right + 2 != rect[2])
                    rect[2] = rect[0] + width;
                else
                    rect[0] = rect[2] - width;

                int height = rect[3] - rect[1];
                height -= this.RemainderHeight;
                height = height - (height % this.Editor.CellSize.Height);
                height += this.RemainderHeight;
                if (this.rsbRect.Bottom + 73 != rect[3])
                    rect[3] = rect[1] + height;
                else
                    rect[1] = rect[3] - height;
            }
            base.DefWndProc(ref m);
        }

        private Rectangle rsbRect;

        public override void SaveEditorAs()
        {
            this.sfdGFX.Filter = GFX.CreateFilter(this.GFX.FileFormat);
            this.sfdGFX.DefaultExt = GFX.GetExtension(this.GFX.FileFormat);
            if (this.sfdGFX.ShowDialog() == DialogResult.OK)
                SaveEditor(this.sfdGFX.FileName);
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
            this.Size = new Size(
                this.Editor.VisibleSize.Width + this.RemainderWidth,
                this.Editor.VisibleSize.Height + this.RemainderHeight);
        }

        private void SetEditorSizeFromForm()
        {
            this.Editor.ViewSize = new Size(
                (this.Width - this.RemainderWidth) / this.Editor.CellSize.Width,
                (this.Height - this.RemainderHeight) / this.Editor.CellSize.Height);
        }

        private void GFX_FileFormatChanged(object sender, EventArgs e)
        {
            this.gfxStatus.ShowAddressScrolling = this.GFX.FileFormat == GFXFileFormats.SNES;
        }

        private void GFX_FileSaved(object sender, EventArgs e)
        {
            SetFormName();
        }

        private void GFXStatus_ZoomScaleChanged(object sender, EventArgs e)
        {
            int zoom = (int)this.GFXZoomScale;
            this.GFX.ZoomSize = new Size(zoom, zoom);
        }

        private void GFXStatus_NextByte(object sender, EventArgs e)
        {
            this.GFX.Zero.Address += 1;
        }

        private void GFXStatus_LastByte(object sender, EventArgs e)
        {
            if (this.GFX.Zero.Address > 0)
                this.GFX.Zero.Address -= 1;
        }

        private void GFXStatus_GraphicsFormatChanged(object sender, EventArgs e)
        {
            this.GFX.GraphicsFormat = this.GraphicsFormat;
            this.vsbGFX.Reset();
            this.hsbGFX.Reset();
            this.MainEditorControl.Redraw();
        }

        public void RotateRight90()
        {
            this.GFX.RotateRight90();
        }

        public void RotateLeft90()
        {
            this.GFX.RotateLeft90();
        }

        public void Rotate180()
        {
            this.GFX.Rotate180();
        }

        public void FlipVertical()
        {
            this.GFX.FlipVertical();
        }

        public void FlipHorizontal()
        {
            this.GFX.FlipHorizontal();
        }

        public void RotateTilesRight90()
        {
            this.GFX.RotateTilesRight90();
        }

        public void RotateTilesLeft90()
        {
            this.GFX.RotateTilesLeft90();
        }

        public void RotateTiles180()
        {
            this.GFX.RotateTiles180();
        }

        public void FlipTilesVertical()
        {
            this.GFX.FlipTilesVertical();
        }

        public void FlipTilesHorizontal()
        {
            this.GFX.FlipTilesHorizontal();
        }
    }
}
