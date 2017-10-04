using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.Unmanaged;

namespace MushROMs.SNES
{
    public partial class GFX
    {
        public unsafe partial class Tile : Editor
        {
            #region Constant and read-only fields
            /// <summary>
            /// The fallback graphics format of the <see cref="GFX"/> data.
            /// This field is constant.
            /// </summary>
            private const GraphicsFormats FallbackGraphicsFormat = GraphicsFormats.SNES_4BPP;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the <see cref="GraphicsFormat"/> changes.
            /// </summary>
            public event EventHandler GraphicsFormatChanged;
            #endregion

            #region Fields
            /// <summary>
            /// The <see cref="GraphicsFormats"/> value of the <see cref="GFX"/>.
            /// </summary>
            private GraphicsFormats graphicsFormat;
            #endregion

            #region Properties
            /// <summary>
            /// Gets the <see cref="Byte"/>* array at a specified byte address.
            /// </summary>
            /// <param name="address">
            /// The address in the current <see cref="GFX"/>.
            /// </param>
            /// <returns>
            /// A <see cref="UInt16"/>* array.
            /// </returns>
            /// <remarks>
            /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
            /// is thrown if <paramref name="address"/> is outside the bounds
            /// of the <see cref="Data"/> array. Therefore, it is up to the
            /// user to carefully monitor improper accesses to unsafe memory
            /// otherwise it will lead to undefined behavior.
            /// </remarks>
            public byte* this[int address]
            {
                get { return (byte*)this.Data.Data + address; }
            }

            /// <summary>
            /// Gets or sets the <see cref="GraphicsFormats"/> value of the
            /// <see cref="GFX"/>.
            /// </summary>
            public GraphicsFormats GraphicsFormat
            {
                get { return this.graphicsFormat; }
                set { this.graphicsFormat = value; OnGraphicsFormatChanged(EventArgs.Empty); }
            }

            /// <summary>
            /// Gets the bits per pixel of the <see cref="GFX"/>.
            /// </summary>
            public int BitsPerPixel
            {
                get { return LC.BitsPerPixel(this.graphicsFormat); }
            }

            /// <summary>
            /// Gets the bytes per plane of the <see cref="GFX"/>.
            /// </summary>
            public int BytesPerPlane
            {
                get { return LC.BytesPerPlane(this.graphicsFormat); }
            }

            /// <summary>
            /// Gets the bytes per tile of the <see cref="GFX"/>.
            /// </summary>
            public int BytesPerTile
            {
                get { return LC.BytesPerTile(this.graphicsFormat); }
            }

            /// <summary>
            /// Gets the max colors per pixel of the <see cref="GFX"/>.
            /// </summary>
            public int MaxColorsPerPixel
            {
                get { return LC.ColorsPerPixel(this.graphicsFormat); }
            }

            /// <summary>
            /// Get the number of 8x8 tiles.
            /// </summary>
            public int NumTiles
            {
                get { return this.MapLength / LC.PixelsPerTile; }
            }
            #endregion

            public Tile()
            {
                this.Zero.IndexArgs = new object[] { (int)0 };

                this.TileSize = new Size(1, 1);
                this.ZoomSize = new Size(0x10, 0x10);
                this.ViewSize = new Size(0x10, 0x10);
            }

            public void Initialize(IEditorData data)
            {
                EditorData copy = (EditorData)data;

                this.graphicsFormat = copy.GraphicsFormat;
                this.MapSize = new Size(
                    copy.Selection.Width * LC.PixelsPerPlane,
                    copy.Selection.Height * LC.PlanesPerTile);
                ResetData(this.MapLength * sizeof(byte));

                IntPtr middle = Memory.CreateMemory(this.Data.Size);
                fixed (byte* pixels = copy.Pixels)
                {
                    LC.CreatePixelMap((IntPtr)pixels, middle, this.NumTiles, this.graphicsFormat);
                }

                byte* src = (byte*)middle;
                byte* dest = this[0];

                for (int h = this.MapHeight / LC.PlanesPerTile; --h >= 0; )
                    for (int w = this.MapWidth / LC.PixelsPerPlane; --w >= 0; )
                        for (int y = LC.PlanesPerTile; --y >= 0; )
                            for (int x = LC.PixelsPerPlane; --x >= 0; )
                                dest[x + (this.MapWidth * y) + (LC.PixelsPerPlane * w) + (this.MapWidth * LC.PlanesPerTile * h)] =
                                 src[x + (LC.PixelsPerPlane * y) + (LC.PixelsPerTile * w) + (this.MapWidth * LC.PlanesPerTile * h)];

                Memory.FreeMemory(middle);
            }

            /// <summary>
            /// Raises the <see cref="GraphicsFormatChanged"/> event.
            /// </summary>
            /// <param name="e">
            /// An <see cref="EventArgs"/> that contains the event data.
            /// </param>
            protected virtual void OnGraphicsFormatChanged(EventArgs e)
            {
                if (this.Data.Size > 0)
                    base.SetMapLength(this.Data.Size / BytesPerTile);

                if (GraphicsFormatChanged != null)
                    GraphicsFormatChanged(this, e);
            }

            public void Draw(IntPtr scan0, Palette palette, int address)
            {
                // Make sure the data actually exists.
                if (this.Data.Data == IntPtr.Zero)
                    return;

                // The Palette data in SNES RGB format
                ushort* snes = palette[address];

                // The Palette data in PC RGB format
                uint* colors = stackalloc uint[this.MaxColorsPerPixel];
                for (int i = this.MaxColorsPerPixel; --i >= 0; )
                    colors[i] = LC.SNEStoPCRGB(snes[i]);

                // Draw the GFX
                Draw(
                    scan0,
                    (IntPtr)this[this.Zero.Address],
                    this.VisibleTileRegion.Width,
                    this.VisibleTileRegion.Height,
                    colors,
                    this.MapWidth,
                    this.MapHeight,
                    this.ViewWidth,
                    this.ViewHeight,
                    this.ZoomWidth,
                    this.ZoomHeight);
            }

            public static void Draw(IntPtr scan0, IntPtr pixels, int xMax, int yMax, uint* colors, int mapW, int mapH, int viewW, int viewH, int zoomW, int zoomH)
            {
                // The Bitmap pixel data
                uint* dest = (uint*)scan0;

                // The pixel data
                byte* src = (byte*)pixels;

                // The current pixel color.
                uint color = colors[*src];

                int width = viewW * zoomW;
                int height = viewH * zoomH;

                for (int h = yMax; --h >= 0; )
                    for (int w = xMax; --w >= 0; )
                        for (int j = zoomH; --j >= 0; )
                            for (int i = zoomW; --i >= 0; )
                                dest[i + (j * width) + (w * zoomW) + (h * width * zoomH)] = colors[src[w + (h * mapW)]];
            }
        }
    }
}