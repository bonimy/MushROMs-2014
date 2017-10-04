using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;

namespace MushROMs.SNESEditor.PaletteEditor
{
    public partial class PaletteMenuStrip : EditorMenuStrip
    {
        public override ToolStripMenuItem TsmNew
        {
            get { return this.tsmNew; }
        }

        public override ToolStripMenuItem TsmOpen
        {
            get { return this.tsmOpen; }
        }

        public override ToolStripMenuItem TsmOpenRecent
        {
            get { return this.tsmOpenRecent; }
        }

        public override ToolStripMenuItem TsmSave
        {
            get { return this.tsmSave; }
        }

        public override ToolStripMenuItem TsmSaveAs
        {
            get { return this.tsmSaveAs; }
        }

        public override ToolStripMenuItem TsmSaveAll
        {
            get { return this.tsmSaveAll; }
        }

        public override ToolStripMenuItem TsmExit
        {
            get { return this.tsmExit; }
        }

        public override ToolStripMenuItem TsmUndo
        {
            get { return this.tsmUndo; }
        }

        public override ToolStripMenuItem TsmRedo
        {
            get { return this.tsmRedo; }
        }

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

        public override ToolStripMenuItem TsmGoTo
        {
            get { return this.tsmGoTo; }
        }

        public override ToolStripMenuItem TsmCustomize
        {
            get { return this.tsmCustomize; }
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripMenuItem TsmEditBackColor
        {
            get { return this.tsmEditBackColor; }
        }

        public PaletteMenuStrip()
        {
            InitializeComponent();
        }
    }
}
