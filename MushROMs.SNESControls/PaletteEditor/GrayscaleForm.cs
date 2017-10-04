using System;
using MushROMs.Editors;

namespace MushROMs.SNESControls.PaletteEditor
{
    public partial class GrayscaleForm : RGBForm
    {
        public GrayscaleForm()
        {
            InitializeComponent();
        }

        private void Luma_Click(object sender, EventArgs e)
        {
            this.runEvent = false;
            this.ltbRed.Value = (int)((ExpandedColor.LumaRedWeight * 100.0f) + 0.5f);
            this.ltbGreen.Value = (int)((ExpandedColor.LumaGreenWeight * 100.0f) + 0.5f);
            this.ltbBlue.Value = (int)((ExpandedColor.LumaBlueWeight * 100.0f) + 0.5f);
            this.runEvent = true;

            OnColorValueChanged(EventArgs.Empty);
        }

        private void Even_Click(object sender, EventArgs e)
        {
            this.runEvent = false;
            this.ltbRed.Value = 100;
            this.ltbGreen.Value = 100;
            this.ltbBlue.Value = 100;
            this.runEvent = true;

            OnColorValueChanged(EventArgs.Empty);
        }
    }
}
