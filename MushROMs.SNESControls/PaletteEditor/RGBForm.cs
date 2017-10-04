using System;
using System.Windows.Forms;

namespace MushROMs.SNESControls.PaletteEditor
{
    public partial class RGBForm : Form
    {
        public event EventHandler ColorValueChanged;

        protected bool runEvent;

        public int Red
        {
            get { return this.ltbRed.Value; }
        }

        public int Green
        {
            get { return this.ltbGreen.Value; }
        }

        public int Blue
        {
            get { return this.ltbBlue.Value; }
        }

        public bool Preview
        {
            get { return this.chkPreview.Checked; }
        }

        public RGBForm()
        {
            InitializeComponent();

            ResetValues();
            this.runEvent = true;
        }

        public void ResetValues()
        {
            this.runEvent = false;
            this.ltbRed.Value = this.ltbRed.Maximum;
            this.ltbGreen.Value = this.ltbGreen.Maximum;
            this.ltbBlue.Value = this.ltbBlue.Maximum;
            this.runEvent = true;

            OnColorValueChanged(EventArgs.Empty);
        }

        protected virtual void OnColorValueChanged(EventArgs e)
        {
            if (this.runEvent)
                if (ColorValueChanged != null)
                    ColorValueChanged(this, e);
        }

        private void RGB_ValueChanged(object sender, EventArgs e)
        {
            OnColorValueChanged(EventArgs.Empty);
        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            OnColorValueChanged(EventArgs.Empty);
        }

        private void RGBForm_Shown(object sender, EventArgs e)
        {
            OnColorValueChanged(EventArgs.Empty);
        }
    }
}