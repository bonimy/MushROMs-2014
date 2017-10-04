using System;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;
using MushROMs.SNES;
using MushROMs.SNESControls.Properties;

namespace MushROMs.SNESControls.PaletteEditor
{
    public unsafe partial class PaletteSettingsForm : Form
    {
        private const int FallbackColumns = Palette.SNESPaletteWidth;
        private const int FallbackRows = Palette.SNESPaletteRows;
        private const uint FallbackColor = 0xF800F8;         //SNES-Magenta
        private const PaletteFileFormats FallbackFormat = PaletteFileFormats.TPL;
        private const PaletteZoomScales FallbackZoom = PaletteZoomScales.Zoom32x;

        private const PaletteBGSizes FallbackBGSize = PaletteBGSizes.Size8x;
        private const uint FallbackBGColor1 = 0xFFF8F8F8;
        private const uint FallbaclBGColor2 = 0xFFC0C0C0;

        private const int FallbackDashLength1 = 4;
        private const int FallbackDashLength2 = FallbackDashLength1;
        private const uint FallbackDashColor1 = 0xFF000000;
        private const uint FallbackDashColor2 = 0xFFFFFFFF;

        private static readonly Cursor FallbackCursor = Cursors.Cross;

        public event EventHandler SettingsCustomized;

        public Size ViewSize
        {
            get { return new Size((int)this.nudColumns.Value, (int)this.nudRows.Value); }
            set { this.nudColumns.Value = value.Width; this.nudRows.Value = value.Height; }
        }

        public PaletteZoomScales ZoomSize
        {
            get { return (PaletteZoomScales)((this.cbxZoom.SelectedIndex + 1) * 8); }
            set { this.cbxZoom.SelectedIndex = ((int)value / 8) - 1; }
        }

        public Color BackColor1
        {
            get { return this.cpkBackColor1.SelectedColor; }
            set { this.cpkBackColor1.SelectedColor = Color.FromArgb(0xFF, value); }
        }

        public Color BackColor2
        {
            get { return this.cpkBackColor2.SelectedColor; }
            set { this.cpkBackColor2.SelectedColor = Color.FromArgb(0xFF, value); }
        }

        public PaletteBGSizes BGSize
        {
            get { return (PaletteBGSizes)(1 << this.cbxBackZoom.SelectedIndex); }
            set { this.cbxBackZoom.SelectedIndex = (int)Math.Log((int)value, 2); }
        }

        public int DashLength1
        {
            get { return (int)this.nudDashLength1.Value; }
            set { this.nudDashLength1.Value = value; }
        }

        public Color DashColor1
        {
            get { return this.cpkDashColor1.SelectedColor; }
            set { this.cpkDashColor1.SelectedColor = Color.FromArgb(0xFF, value); }
        }

        public int DashLength2
        {
            get { return (int)this.nudDashLength2.Value; }
            set { this.nudDashLength2.Value = value; }
        }

        public Color DashColor2
        {
            get { return this.cpkDashColor2.SelectedColor; }
            set { this.cpkDashColor2.SelectedColor = Color.FromArgb(0xFF, value); }
        }

        public PaletteSettingsForm()
        {
            InitializeComponent();

            this.drwBGExample.DrawImageWhen = DrawImageWhen.AfterPaint;

            if (Settings.Default.FirstTime)
            {
                ResetSettings();
                Settings.Default.FirstTime = false;
                Settings.Default.Save();
            }

            ShowSettings();
        }

        private void ShowSettings()
        {
            this.ViewSize = Settings.Default.PaletteViewSize;
            this.ZoomSize = (PaletteZoomScales)Settings.Default.PaletteZoomScale;
            this.BackColor1 = Settings.Default.PaletteBGColor1;
            this.BackColor2 = Settings.Default.PaletteBGColor2;
            this.BGSize = (PaletteBGSizes)Settings.Default.PaletteBGSize;
            this.DashLength1 = Settings.Default.PaletteDashLength1;
            this.DashColor1 = Settings.Default.PaletteDashColor1;
            this.DashLength2 = Settings.Default.PaletteDashLength2;
            this.DashColor2 = Settings.Default.PaletteDashColor2;
        }

        private void ResetSettings()
        {
            Settings.Default.PaletteViewSize = new Size(FallbackColumns, FallbackRows);
            Settings.Default.PaletteZoomScale = (int)FallbackZoom;
            Settings.Default.PaletteBGSize = (int)FallbackBGSize;
            Settings.Default.PaletteBGColor1 = LC.PCToSystemColor(FallbackBGColor1);
            Settings.Default.PaletteBGColor2 = LC.PCToSystemColor(FallbaclBGColor2);
            Settings.Default.PaletteDashLength1 = FallbackDashLength1;
            Settings.Default.PaletteDashLength2 = FallbackDashLength2;
            Settings.Default.PaletteDashColor1 = LC.PCToSystemColor(FallbackDashColor1);
            Settings.Default.PaletteDashColor2 = LC.PCToSystemColor(FallbackDashColor2);
        }

        public void ApplyCustomSettings()
        {
            Settings.Default.PaletteViewSize = this.ViewSize;
            Settings.Default.PaletteZoomScale = (int)this.ZoomSize;
            Settings.Default.PaletteBGSize = (int)this.BGSize;
            Settings.Default.PaletteBGColor1 = this.BackColor1;
            Settings.Default.PaletteBGColor2 = this.BackColor2;
            Settings.Default.PaletteDashLength1 = this.DashLength1;
            Settings.Default.PaletteDashLength2 = this.DashLength2;
            Settings.Default.PaletteDashColor1 = this.DashColor1;
            Settings.Default.PaletteDashColor2 = this.DashColor2;
            Settings.Default.Save();
        }

        private void RedrawExample(object sender, EventArgs e)
        {
            this.drwBGExample.Redraw();
        }

        private void drwBGExample_WritePixels(object sender, EventArgs e)
        {
            int width = this.drwBGExample.UserImageSize.Width;
            int height = this.drwBGExample.UserImageSize.Height;

            int bgSize = 1 << this.cbxBackZoom.SelectedIndex;
            uint bgColor1 = LC.SystemToPCColor(this.cpkBackColor1.SelectedColor);
            uint bgColor2 = LC.SystemToPCColor(this.cpkBackColor2.SelectedColor);

            uint* scan0 = (uint*)this.drwBGExample.Scan0.Data;
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    scan0[(y * width) + x] = ((x & (int)bgSize) ^ (y & (int)bgSize)) == 0 ? bgColor1 : bgColor2;
        }

        private void drwBGExample_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, this.drwBGExample.ClientRectangle);

            Rectangle r = this.drwBGExample.UserImageRectangle;
            g.FillRectangle(Brushes.Black,
                new Rectangle(r.X - 1, r.Y - 1, r.Width + 3, r.Height + 3));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetSettings();
            ShowSettings();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyCustomSettings();
            Settings.Default.Save();

            if (SettingsCustomized != null)
                SettingsCustomized(this, EventArgs.Empty);
        }
    }
}