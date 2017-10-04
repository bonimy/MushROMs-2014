using System;
using MushROMs.LunarCompress;

namespace MushROMs.SNES
{
    unsafe partial class Palette
    {
        /// <summary>
        /// Draws the <see cref="Palette"/> pixel data to an <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to an array of bytes that contains the pixel data.
        /// </param>
        /// <remarks>
        /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
        /// is thrown if anything is outside the bounds of an array.
        /// Therefore, it is up to the user to carefully monitor improper
        /// accesses to unsafe memory otherwise it will lead to undefined behavior.
        /// </remarks>
        public void Draw(IntPtr scan0)
        {
            Draw(
                scan0,
                this[this.Zero.Address],
                this.NumVisibleTiles,
                this.ViewWidth,
                this.ViewHeight,
                this.CellWidth,
                this.CellHeight,
                this.VisibleWidth);
        }

        /// <summary>
        /// Draws the <see cref="Palette"/> pixel data to an <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to an array of bytes that contains the pixel data.
        /// </param>
        /// <param name="colors">
        /// The source of the <see cref="Palette"/> colors.
        /// </param>
        /// <param name="numColors">
        /// The number of colors to draw.
        /// </param>
        /// <param name="viewW">
        /// The width of the view region.
        /// </param>
        /// <param name="viewH">
        /// The height of the view region.
        /// </param>
        /// <param name="cellW">
        /// The width of a single cell tile.
        /// </param>
        /// <param name="cellH">
        /// The height of a single cell tile.
        /// </param>
        /// <param name="width">
        /// The width, in pixels, of the view region.
        /// </param>
        /// <remarks>
        /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
        /// is thrown if anything is outside the bounds of an array.
        /// Therefore, it is up to the user to carefully monitor improper
        /// accesses to unsafe memory otherwise it will lead to undefined behavior.
        /// </remarks>
        public static void Draw(IntPtr scan0, ushort* colors, int numColors, int viewW, int viewH, int cellW, int cellH, int width)
        {
            // The Bitmap pixel data
            uint* dest = (uint*)scan0;

            // The current color to draw
            uint color = LC.SNEStoPCRGB(*colors);

            // Special offseters for fast-speed drawing.
            int row  = width * cellH - width;    // Offset to next row
            int cell = width * cellH - cellW;    // Offset to next cell

            // Draw the Palette data
            for (int y = viewH; --y >= 0; dest += row, colors += viewW)
                for (int x = 0; x < viewW && --numColors >= 0; ++x, dest -= cell, color = LC.SNEStoPCRGB(colors[x]))
                    for (int i = cellH; --i >= 0; dest += width)
                        for (int j = cellW; --j >= 0; )
                            dest[j] = color;
        }
    }
}
