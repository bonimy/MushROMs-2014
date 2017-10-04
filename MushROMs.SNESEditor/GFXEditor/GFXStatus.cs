using System;
using System.Windows.Forms;
using MushROMs.LunarCompress;
using MushROMs.SNESControls.GFXEditor;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class GFXStatus : UserControl
    {
        public event EventHandler GraphicsFormatChanged
        {
            add { this.cbxGraphicsFormat.SelectedIndexChanged += value; }
            remove { this.cbxGraphicsFormat.SelectedIndexChanged -= value; }
        }

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
            add { this.btnLastByte.Click -= value; }
            remove { this.btnLastByte.Click -= value; }
        }

        public GraphicsFormats GraphicsFormat
        {
            get
            {
                switch (this.cbxGraphicsFormat.SelectedIndex)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        return (GraphicsFormats)(this.cbxGraphicsFormat.SelectedIndex + 1);
                    case 8:
                        return GraphicsFormats.Mode7_8BPP;
                    case 9:
                        return GraphicsFormats.GBA_4BPP;
                    default:
                        return GraphicsFormats.None;
                }
            }
            set
            {
                switch (value)
                {
                    case GraphicsFormats.SNES_1BPP:
                    case GraphicsFormats.SNES_2BPP:
                    case GraphicsFormats.SNES_3BPP:
                    case GraphicsFormats.SNES_4BPP:
                    case GraphicsFormats.SNES_5BPP:
                    case GraphicsFormats.SNES_6BPP:
                    case GraphicsFormats.SNES_7BPP:
                    case GraphicsFormats.SNES_8BPP:
                        this.cbxGraphicsFormat.SelectedIndex = (int)value - 1;
                        break;
                    case GraphicsFormats.Mode7_8BPP:
                        this.cbxGraphicsFormat.SelectedIndex = 8;
                        break;
                    case GraphicsFormats.GBA_4BPP:
                        this.cbxGraphicsFormat.SelectedIndex = 9;
                        break;
                }
            }
        }

        public GFXZoomScales GFXZoomScale
        {
            get { return (GFXZoomScales)(this.cbxZoom.SelectedIndex + 1); }
            set { this.cbxZoom.SelectedIndex = ((int)value - 1); }
        }

        public bool ShowAddressScrolling
        {
            get { return this.gbxROMViewing.Visible; }
            set { this.gbxROMViewing.Visible = value; }
        }

        public GFXStatus()
        {
            InitializeComponent();
        }
    }
}