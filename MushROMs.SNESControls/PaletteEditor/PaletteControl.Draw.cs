using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;
using System.Windows.Forms;
using MushROMs.LunarCompress;
using MushROMs.SNES;
using MushROMs.SNESControls.Properties;

namespace MushROMs.SNESControls.PaletteEditor
{
    public unsafe partial class PaletteControl
    {
        private System.Timers.Timer timer;
        private int dashOffset = 0;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.dashOffset--;
            this.Invalidate();
        }

        private void EditorControl_WritePixels(object sender, EventArgs e)
        {
            if (this.Editor == null || this.Editor.NumVisibleTiles < this.Editor.NumViewTiles)
            {
                uint* dest = (uint*)this.Scan0.Data;
                int width = this.ClientWidth;
                int height = this.ClientHeight;

                int bgSize = Settings.Default.PaletteBGSize;
                uint bgColor1 = LC.SystemToPCColor(Settings.Default.PaletteBGColor1);
                uint bgColor2 = LC.SystemToPCColor(Settings.Default.PaletteBGColor2);

                for (int i = width * height; --i >= 0; )
                    dest[i] = bgColor1;

                if (bgColor1 != bgColor2)
                {
                    int bgSize2 = bgSize << 1;
                    for (int y = height; (y -= bgSize) >= 0; )
                    {
                        int offset = y & bgSize;
                        int index = ((y + 1) * width) + offset;
                        for (int x = width; (x -= bgSize2) >= 0; )
                        {
                            index -= bgSize2;
                            for (int h = bgSize; --h >= 0; )
                            {
                                int index2 = index + (h * width) + bgSize;
                                for (int w = bgSize; --w >= 0; )
                                    dest[--index2] = bgColor2;
                            }
                        }
                    }
                }
            }
        }

        private void EditorControl_Paint(object sender, PaintEventArgs e)
        {
            if (this.Editor == null)
                return;

            Graphics g = e.Graphics;

            float DashLength1 = Settings.Default.PaletteDashLength1;
            float DashLength2 = Settings.Default.PaletteDashLength2;
            Pen p1 = new Pen(Settings.Default.PaletteDashColor1, 1);
            p1.DashStyle = DashStyle.Custom;
            p1.DashPattern = new float[] { DashLength1, DashLength2 };
            p1.DashOffset = this.dashOffset;

            Pen p2 = new Pen(Settings.Default.PaletteDashColor2, 1);
            p2.DashStyle = DashStyle.Custom;
            p2.DashPattern = new float[] { DashLength1, DashLength2 };
            p2.DashOffset = this.dashOffset + DashLength1;

            g.DrawPath(p1, this.SelectionBoundary);
            g.DrawPath(p2, this.SelectionBoundary);

            if (this.Editor.Active.Index >= 0 && this.Editor.Active.Index < this.Editor.MapLength)
            {
                p1.DashPattern = new float[] { 1, 1 };
                p1.DashOffset = 0;
                p2.DashPattern = new float[] { 1, 1 };
                p2.DashOffset = 1;

                Rectangle r = new Rectangle(this.Editor.Active.RelativeX * this.Editor.CellSize.Width + 2,
                                            this.Editor.Active.RelativeY * this.Editor.CellSize.Height + 2,
                                            this.Editor.CellSize.Width - 5,
                                            this.Editor.CellSize.Height - 5);
                g.DrawRectangle(p1, r);
                g.DrawRectangle(p2, r);
            }
        }
    }

    [Flags]
    public enum PaletteZoomScales
    {
        Zoom8x = 8,
        Zoom16x = 16,
        Zoom24x = 24,
        Zoom32x = 32
    }

    [Flags]
    public enum PaletteBGSizes
    {
        Size1x = 1,
        Size2x = 2,
        Size4x = 4,
        Size8x = 8,
    }
}