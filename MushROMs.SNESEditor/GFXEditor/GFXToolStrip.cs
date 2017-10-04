using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class GFXToolStrip : EditorToolStrip
    {
        public override ToolStripButton TsbNew
        {
            get { return this.tsbNew; }
        }

        public override ToolStripButton TsbOpen
        {
            get { return this.tsbOpen; }
        }

        public override ToolStripButton TsbSave
        {
            get { return this.tsbSave; }
        }

        public override ToolStripButton TsbSaveAll
        {
            get { return this.tsbSaveAll; }
        }

        public override ToolStripButton TsbUndo
        {
            get { return this.tsbUndo; }
        }

        public override ToolStripButton TsbRedo
        {
            get { return this.tsbRedo; }
        }

        public override ToolStripButton TsbCut
        {
            get { return this.tsbCut; }
        }

        public override ToolStripButton TsbCopy
        {
            get { return this.tsbCopy; }
        }

        public override ToolStripButton TsbPaste
        {
            get { return this.tsbPaste; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbRotateRight90
        {
            get { return this.tsbRotateRight90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbRotateLeft90
        {
            get { return this.tsbRotateLeft90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbRotate180
        {
            get { return this.tsbRotate180; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbFlipVertical
        {
            get { return this.tsbFlipVertical; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbFlipHorizontal
        {
            get { return this.tsbFlipHorizontal; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbRotateTilesRight90
        {
            get { return this.tsbRotateTilesRight90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbRotateTilesLeft90
        {
            get { return this.tsbRotateTilesLeft90; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbRotateTiles180
        {
            get { return this.tsbRotateTiles180; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbFlipTilesVertical
        {
            get { return this.tsbFlipTilesVertical; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbFlipTilesHorizontal
        {
            get { return this.tsbFlipTilesHorizontal; }
        }

        public GFXToolStrip()
        {
            InitializeComponent();
        }
    }
}