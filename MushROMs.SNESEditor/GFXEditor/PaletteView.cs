using System;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.Editors;
using MushROMs.SNES;
using MushROMs.SNESControls.PaletteEditor;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class PaletteView : ComplementEditorForm
    {
        public override IEditorControl MainEditorControl
        {
            get { return this.paletteControl; }
        }

        public PaletteControl PaletteControl
        {
            get { return (PaletteControl)this.MainEditorControl; }
        }

        public Palette Palette
        {
            get { return (Palette)this.Editor; }
        }

        public override string Status
        {
            set
            {
                //base.Status = value;
            }
        }

        public PaletteView()
        {
            InitializeComponent();
            this.Editor = this.paletteControl.Palette;
            this.paletteControl.Palette.CanScrollSelection = false;
            this.paletteControl.Palette.SelectType = SelectType.Single;
        }

        private void paletteControl_WritePixels(object sender, EventArgs e)
        {
            PaletteControl control = (PaletteControl)sender;
            control.Palette.Draw(control.Scan0.Data);
        }
    }
}
