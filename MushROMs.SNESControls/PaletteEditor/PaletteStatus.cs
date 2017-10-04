using System;
using System.Windows.Forms;
using MushROMs.LunarCompress;

namespace MushROMs.SNESControls.PaletteEditor
{
    public partial class PaletteStatus : UserControl
    {
        public event EventHandler ZoomScaleChanged
        {
            add { this.cbxZoom.SelectedIndexChanged += value; }
            remove { this.cbxZoom.SelectedIndexChanged -= value; }
        }

        public event EventHandler NextByte
        {
            add { this.btnNextByte.Click += value; }
            remove { this.btnNextByte.Click -= value; }
        }
        public event EventHandler LastByte
        {
            add { this.btnLastByte.Click += value; }
            remove { this.btnLastByte.Click -= value; }
        }

        public ushort ActiveColor
        {
            set { SetActiveColor(value); }
        }

        public PaletteZoomScales PaletteZoomScale
        {
            get { return (PaletteZoomScales)(8 * (this.cbxZoom.SelectedIndex + 1)); }
            set { this.cbxZoom.SelectedIndex = ((int)value / 8) - 1; }
        }

        public bool ShowAddressScrolling
        {
            get { return this.gbxROMViewing.Visible; }
            set { this.gbxROMViewing.Visible = value; }
        }

        public PaletteStatus()
        {
            InitializeComponent();
        }

        private void SetActiveColor(ushort value)
        {
            uint color = LC.SNEStoPCRGB(value);
            this.lblPcValue.Text = "0x" + color.ToString("X6");
            this.lblSnesValue.Text = "0x" + LC.PCtoSNESRGB(color).ToString("X4");
            this.lblRedValue.Text = ((color >> 0x10) & 0xF8).ToString();
            this.lblGreenValue.Text = ((color >> 8) & 0xF8).ToString();
            this.lblBlueValue.Text = (color & 0xF8).ToString();
        }

        private void SetPaletteZoomScale(int value)
        {
            this.cbxZoom.SelectedIndex = ((int)value / 8) - 1;
        }
    }
}
