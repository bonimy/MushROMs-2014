using System;
using System.Windows.Forms;

namespace MushROMs.SNESControls.PaletteEditor
{
    public sealed partial class ColorizeForm : Form
    {
        public event EventHandler ColorValueChanged;

        private const int FallbackHue = 0;
        private const int FallbackSaturation = 0;
        private const int FallbackLuminosity = 0;
        private const int FallbackCHue = 0;
        private const int FallbackCSaturation = 25;
        private const int FallbackCLuminosity = 50;
        private const int FallbackCEffectiveness = 100;

        private int hue, sat, lum, cHue, cSat, cLum;
        private bool runEvent;

        public int Hue
        {
            get { return this.ltbHue.Value; }
            private set { this.ltbHue.Value = value; }
        }

        public int Saturation
        {
            get { return this.ltbSaturation.Value; }
            private set { this.ltbSaturation.Value = value; }
        }

        public int Lightness
        {
            get { return this.ltbLightness.Value; }
            private set { this.ltbLightness.Value = value; }
        }

        public int Effectiveness
        {
            get { return this.ltbEffectiveness.Value; }
            private set { this.ltbEffectiveness.Value = value; }
        }

        public bool Colorize
        {
            get { return this.chkColorize.Checked; }
            private set { this.chkColorize.Checked = value; }
        }

        public bool Preview
        {
            get { return this.chkPreview.Checked; }
            set { this.chkPreview.Checked = value; }
        }

        public ColorizeForm()
        {
            InitializeComponent();

            this.hue = FallbackHue;
            this.sat = FallbackSaturation;
            this.lum = FallbackLuminosity;
            this.cHue = FallbackCHue;
            this.cSat = FallbackCSaturation;
            this.cLum = FallbackCLuminosity;

            ResetValues();

            this.runEvent = true;
        }

        public void ResetValues()
        {
            this.runEvent = false;

            if (this.Colorize)
            {
                this.Hue = FallbackCHue;
                this.Saturation = FallbackCSaturation;
                this.Lightness = FallbackCLuminosity;
                this.Effectiveness = FallbackCEffectiveness;
            }
            else
            {
                this.Hue = FallbackHue;
                this.Saturation = FallbackSaturation;
                this.Lightness = FallbackLuminosity;
            }

            this.runEvent = true;
            this.btnReset.Enabled = false;
            OnColorValueChanged(EventArgs.Empty);
        }

        private void SwitchValues()
        {
            this.ltbEffectiveness.Enabled =
            this.lblEffectiveness.Enabled =
            this.ntbEffectiveness.Enabled = this.Colorize;
            this.runEvent = false;   //Prevents OnColorValueChanged event.

            if (this.Colorize)
            {
                this.hue = this.Hue;
                this.sat = this.Saturation;
                this.lum = this.Lightness;

                this.ltbHue.Minimum = 0;
                this.ltbHue.Maximum = 360;
                this.ltbSaturation.Minimum = this.ltbLightness.Minimum = 0;
                this.ltbSaturation.Maximum = this.ltbLightness.Maximum = 100;
                this.ltbSaturation.TickFrequency = this.ltbLightness.TickFrequency = 5;

                this.Hue = this.cHue;
                this.Saturation = this.cSat;
                this.Lightness = this.cLum;
            }
            else
            {
                this.cHue = this.Hue;
                this.cSat = this.Saturation;
                this.cLum = this.Lightness;

                this.ltbHue.Minimum = -180;
                this.ltbHue.Maximum = 180;
                this.ltbSaturation.Minimum = this.ltbLightness.Minimum = -100;
                this.ltbSaturation.Maximum = this.ltbLightness.Maximum = 100;
                this.ltbSaturation.TickFrequency = this.ltbLightness.TickFrequency = 10;

                this.Hue = this.hue;
                this.Saturation = this.sat;
                this.Lightness = this.lum;
            }

            this.runEvent = true;
            OnColorValueChanged(EventArgs.Empty);
        }

        private void OnColorValueChanged(EventArgs e)
        {
            if (this.runEvent)
                if (ColorValueChanged != null)
                    ColorValueChanged(this, e);
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void Colorize_CheckedChanged(object sender, EventArgs e)
        {
            SwitchValues();
        }

        private void Preview_CheckedChanged(object sender, EventArgs e)
        {
            OnColorValueChanged(EventArgs.Empty);
        }

        private void HSLE_ValueChanged(object sender, EventArgs e)
        {
            this.btnReset.Enabled = true;
            OnColorValueChanged(EventArgs.Empty);
        }

        private void ColorizeForm_Shown(object sender, EventArgs e)
        {
            OnColorValueChanged(EventArgs.Empty);
        }
    }
}