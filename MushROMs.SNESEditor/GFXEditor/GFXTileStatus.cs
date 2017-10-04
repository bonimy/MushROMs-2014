using System;
using System.Windows.Forms;
using MushROMs.SNESControls.GFXEditor;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class GFXTileStatus : UserControl
    {
        public event EventHandler ZoomScaleChanged
        {
            add { this.cbxZoom.SelectedIndexChanged += value; }
            remove { this.cbxZoom.SelectedIndexChanged -= value; }
        }

        public GFXTileZoomScales ZoomScale
        {
            get { return (GFXTileZoomScales)(8 * (this.cbxZoom.SelectedIndex + 1)); }
            set { this.cbxZoom.SelectedIndex = ((int)value - 1) / 8; }
        }

        public GFXTileStatus()
        {
            InitializeComponent();
        }
    }
}
