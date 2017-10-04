using System;
using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Editors;

namespace MushROMs.SNESControls.PaletteEditor
{
    public partial class BlendForm : RGBForm
    {
        public BlendModes BlendMode
        {
            get
            {
                if (this.rdbMultiply.Checked)
                    return BlendModes.Multiply;
                if (this.rdbScreen.Checked)
                    return BlendModes.Screen;
                if (this.rdbOverlay.Checked)
                    return BlendModes.Overlay;
                if (this.rdbHardLight.Checked)
                    return BlendModes.HardLight;
                if (this.rdbSoftLight.Checked)
                    return BlendModes.SoftLight;
                if (this.rdbColorDodge.Checked)
                    return BlendModes.ColorDodge;
                if (this.rdbLinearDodge.Checked)
                    return BlendModes.LinearDodge;
                if (this.rdbColorBurn.Checked)
                    return BlendModes.ColorBurn;
                if (this.rdbLinearBurn.Checked)
                    return BlendModes.LinearBurn;
                if (this.rdbVividLight.Checked)
                    return BlendModes.VividLight;
                if (this.rdbLinearLight.Checked)
                    return BlendModes.LinearLight;
                if (this.rdbDifference.Checked)
                    return BlendModes.Difference;
                if (this.rdbDarken.Checked)
                    return BlendModes.Darken;
                if (this.rdbLighten.Checked)
                    return BlendModes.Lighten;
                throw new InvalidEnumArgumentException();
            }
            set
            {
                switch (value)
                {
                    case BlendModes.Multiply:
                        this.rdbMultiply.Checked = true;
                        break;
                    case BlendModes.Screen:
                        this.rdbScreen.Checked = true;
                        break;
                    case BlendModes.Overlay:
                        this.rdbOverlay.Checked = true;
                        break;
                    case BlendModes.HardLight:
                        this.rdbHardLight.Checked = true;
                        break;
                    case BlendModes.SoftLight:
                        this.rdbSoftLight.Checked = true;
                        break;
                    case BlendModes.ColorDodge:
                        this.rdbColorDodge.Checked = true;
                        break;
                    case BlendModes.LinearDodge:
                        this.rdbLinearDodge.Checked = true;
                        break;
                    case BlendModes.ColorBurn:
                        this.rdbColorBurn.Checked = true;
                        break;
                    case BlendModes.LinearBurn:
                        this.rdbLinearBurn.Checked = true;
                        break;
                    case BlendModes.VividLight:
                        this.rdbVividLight.Checked = true;
                        break;
                    case BlendModes.LinearLight:
                        this.rdbLinearLight.Checked = true;
                        break;
                    case BlendModes.Difference:
                        this.rdbDifference.Checked = true;
                        break;
                    case BlendModes.Darken:
                        this.rdbDarken.Checked = true;
                        break;
                    case BlendModes.Lighten:
                        this.rdbLighten.Checked = true;
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }
        }

        public BlendForm()
        {
            InitializeComponent();
        }

        private void BlendModes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = (RadioButton)sender;
            if (rdb.Checked)
                OnColorValueChanged(EventArgs.Empty);
        }
    }
}
