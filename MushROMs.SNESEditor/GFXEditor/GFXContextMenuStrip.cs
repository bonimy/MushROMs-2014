using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class GFXContextMenuStrip : EditorContextMenuStrip
    {
        public override ToolStripMenuItem TsmCut
        {
            get { return this.tsmCut; }
        }

        public override ToolStripMenuItem TsmCopy
        {
            get { return this.tsmCopy; }
        }

        public override ToolStripMenuItem TsmPaste
        {
            get { return this.tsmPaste; }
        }

        public override ToolStripMenuItem TsmDelete
        {
            get { return this.tsmDelete; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmSendToTileEditor
        {
            get { return this.tsmSendToTileEditor; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmRotateRight90
        {
            get { return this.tsmRotateRight90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmRotateLeft90
        {
            get { return this.tsmRotateLeft90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmRotate180
        {
            get { return this.tsmRotate180; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmFlipVertical
        {
            get { return this.tsmFlipVertical; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmFlipHorizontal
        {
            get { return this.tsmFlipHorizontal; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmRotateTilesRight90
        {
            get { return this.tsmRotateTilesRight90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmRotateTilesLeft90
        {
            get { return this.tsmRotateTilesLeft90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmRotateTiles180
        {
            get { return this.tsmRotateTiles180; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmFlipTilesVertical
        {
            get { return this.tsmFlipTilesVertical; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmFlipTilesHorizontal
        {
            get { return this.tsmFlipTilesHorizontal; }
        }

        public GFXContextMenuStrip()
        {
            InitializeComponent();
        }
    }
}