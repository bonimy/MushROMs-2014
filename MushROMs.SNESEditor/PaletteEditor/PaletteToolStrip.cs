using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;

namespace MushROMs.SNESEditor.PaletteEditor
{
    public partial class PaletteToolStrip : EditorToolStrip
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
        public ToolStripButton TsbInvert
        {
            get { return this.tsbInvert; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbColorize
        {
            get { return this.tsbColorize; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbBlend
        {
            get { return this.tsbBlend; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbGrayscale
        {
            get { return this.tsbGrayscale; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbHorizontalGradient
        {
            get { return this.tsbHorizontalGradient; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripButton TsbVerticalGradient
        {
            get { return this.tsbVerticalGradient; }
        }

        public PaletteToolStrip()
        {
            InitializeComponent();
        }
    }
}