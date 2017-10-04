using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;

namespace MushROMs.SNESEditor.PaletteEditor
{
    public partial class PaletteContextMenuStrip : EditorContextMenuStrip
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
        public ToolStripMenuItem TsmInvert
        {
            get { return this.tsmInvert; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmColorize
        {
            get { return this.tsmColorize; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmBlend
        {
            get { return this.tsmBlend; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmGrayscale
        {
            get { return this.tsmGrayscale; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmHorizontalGradient
        {
            get { return this.tsmHorizontalGradient; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmVerticalGradient
        {
            get { return this.tsmVerticalGradient; }
        }

        public PaletteContextMenuStrip()
        {
            InitializeComponent();
        }
    }
}