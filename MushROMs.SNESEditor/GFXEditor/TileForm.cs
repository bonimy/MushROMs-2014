using System.Drawing;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNES;
using MushROMs.SNESControls.GFXEditor;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class TileForm : ComplementEditorForm
    {
        private readonly int RemainderWidth;
        private readonly int RemainderHeight;

        public override IEditorControl MainEditorControl
        {
            get { return this.gfxTileControl; }
        }

        public override string Status
        {
            set
            {
                this.tssMain.Text = value;
            }
        }

        public GFXTileZoomScales ZoomScale
        {
            get { return this.gfxTileStatus.ZoomScale; }
            set { this.gfxTileStatus.ZoomScale = value; }
        }

        public GFX.Tile GFXTiles
        {
            get { return (GFX.Tile)this.Editor; }
        }

        public TileForm()
        {
            InitializeComponent();

            this.InvokeSetEditorSizeFromForm = new MethodInvoker(SetEditorSizeFromForm);
            this.InvokeSetFormSizeFromEditor = new MethodInvoker(SetFormSizeFromEditor);

            this.gfxTileStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.hsbTileEditor.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.vsbTileEditor.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            this.RemainderWidth = this.ClientSize.Width - this.MainEditorControl.ClientSize.Width;
            this.RemainderHeight = this.ClientSize.Height - this.MainEditorControl.ClientSize.Height;
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

        private void gfxTileStatus_ZoomScaleChanged(object sender, System.EventArgs e)
        {
            int zoom = (int)this.ZoomScale;
            this.GFXTiles.ZoomSize = new Size(zoom, zoom);
        }
    }
}
