using System;
using MushROMs.LunarCompress;
using MushROMs.Unmanaged;

namespace MushROMs.SNES
{
    unsafe partial class GFX
    {
        /// <summary>
        /// Draws the <see cref="GFX"/> pixel data to an <see cref="IntPtr"/>
        /// using a provided <see cref="Palette"/>.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to an array of bytes that contains the pixel data.
        /// </param>
        /// <param name="palette">
        /// The source <see cref="Palette"/> color data.
        /// </param>
        /// <param name="address">
        /// The starting address of the <see cref="Palette"/> color data to use.
        /// </param>
        /// <remarks>
        /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
        /// is thrown if anything is outside the bounds of an array.
        /// Therefore, it is up to the user to carefully monitor improper
        /// accesses to unsafe memory otherwise it will lead to undefined behavior.
        /// </remarks>
        public void Draw(IntPtr scan0, Palette palette, int address)
        {
            // The GFX data still in BPP format
            byte* bpp = this[this.Zero.Address];

            // The GFX data in indexed pixel format
            IntPtr pixels = Memory.CreateMemory(this.NumVisibleTiles * LC.PixelsPerTile * sizeof(byte));
            LC.CreatePixelMap((IntPtr)bpp, pixels, this.NumVisibleTiles, this.graphicsFormat);

            // The Palette data in SNES RGB format
            ushort* snes = palette[address];

            // The Palette data in PC RGB format
            uint* colors = stackalloc uint[this.ColorsPerPixel];
            for (int i = this.ColorsPerPixel; --i >= 0; )
                colors[i] = LC.SNEStoPCRGB(snes[i]);

            // Draw the GFX
            Draw(
                scan0,
                (byte*)pixels,
                (int*)this.adf.Data,
                this.NumVisibleTiles,
                colors,
                this.ViewWidth,
                this.ViewHeight,
                this.TileWidth,
                this.TileHeight,
                this.ZoomWidth,
                this.ZoomHeight,
                this.VisibleWidth);

            // Release the pixel data from memory.
            Memory.FreeMemory(pixels);
        }

        /// <summary>
        /// Draw the <see cref="GFX"/> pixel data to an <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to an array of bytes that contains the pixel data.
        /// </param>
        /// <param name="pixels">
        /// The indexed pixel data.
        /// </param>
        /// <param name="numTiles">
        /// The number of tiles to draw.
        /// </param>
        /// <param name="colors">
        /// The source colors to use.
        /// </param>
        /// <param name="viewW">
        /// The width of the view region.
        /// </param>
        /// <param name="viewH">
        /// The height of the view region.
        /// </param>
        /// <param name="tileW">
        /// The width of a single tile.
        /// </param>
        /// <param name="tileH">
        /// The height of a single tile.
        /// </param>
        /// <param name="zoomW">
        /// The vertical zoom factor.
        /// </param>
        /// <param name="zoomH">
        /// The horizontal zoom factor.
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
        public static void Draw(IntPtr scan0, byte* src, int* adf, int numTiles, uint* colors, int viewW, int viewH, int tileW, int tileH, int zoomW, int zoomH, int width)
        {
            // The cell size.
            int cellW = tileW * zoomW;
            int cellH = tileH * zoomH;

            // Special offseters for fast-speed drawing.
            int plane = width * zoomH - cellW; // Offset to next cell plane
            int pixel = width * zoomH - zoomW; // Offset to next cell pixel

            // Draw the GFX data
            for (int y = viewH; --y >= 0; )
                for (int x = viewW; --x >= 0; )
                {
                    int index = adf[(y * viewW) + x];
                    if (index >= numTiles)
                        continue;

                    uint* dest = (uint*)scan0 + (y * cellH * width) + (x * cellW);
                    byte* pixels = src + (LC.PixelsPerTile * index);
                    uint color = colors[*pixels];

                    for (int h = tileH; --h >= 0; dest += plane)
                        for (int w = tileW; --w >= 0; dest -= pixel, color = colors[*(++pixels)])
                            for (int j = zoomH; --j >= 0; dest += width)
                                for (int i = zoomW; --i >= 0; )
                                    dest[i] = color;
                }
        }
    }
}