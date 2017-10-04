using System;
using MushROMs.Unmanaged;

namespace MushROMs.LunarCompress
{
    unsafe partial class LC
    {
        #region Constant and read-only fields
        /// <summary>
        /// The number of bits in a byte.
        /// </summary>
        public const int BitsPerByte = 8;
        /// <summary>
        /// The width, in pixels, of a single GFX Tile.
        /// </summary>
        public const int PixelsPerPlane = 8;
        /// <summary>
        /// The height, in pixels, of a single GFX Tile.
        /// </summary>
        public const int PlanesPerTile = PixelsPerPlane;
        /// <summary>
        /// The total size, in pixels, of a single GFX Tile.
        /// </summary>
        public const int PixelsPerTile = PixelsPerPlane * PlanesPerTile;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the number of bits alloted to each pixel given the
        /// <paramref name="graphicsFormat"/>.
        /// </summary>
        /// <param name="graphicsFormat">
        /// The <see cref="GraphicsFormats"/> value of the pixel data.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> of the number of bits each pixel uses.
        /// </returns>
        public static int BitsPerPixel(GraphicsFormats graphicsFormat)
        {
            // The graphics formats enumeration is designed with this in mind.
            return (int)graphicsFormat & 0x0F;
        }

        /// <summary>
        /// Gets the number bytes alloted to each plane (row) of a tile
        /// given the <paramref name="graphicsFormat"/>.
        /// </summary>
        /// <param name="graphicsFormat">
        /// The <see cref="GraphicsFormats"/> value of the pixel data.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> of the number of bytes each tile plane uses.
        /// </returns>
        public static int BytesPerPlane(GraphicsFormats graphicsFormat)
        {
            /* This method always returns the same value as bits per pixel.
             * It's existance is merely to provide context between bytes or bits.
             * */
            return BitsPerPixel(graphicsFormat) * PixelsPerPlane / BitsPerByte;
        }

        /// <summary>
        /// Gets the number of bytes alloted to each tile given the
        /// <paramref name="graphicsFormat"/>.
        /// </summary>
        /// <param name="graphicsFormat">
        /// The <see cref="GraphicsFormats"/> value of the pixel data.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> of the number of bytes each tile uses.
        /// </returns>
        public static int BytesPerTile(GraphicsFormats graphicsFormat)
        {
            // An excellent example of the contextual value of bytes per plane.
            return BytesPerPlane(graphicsFormat) * PlanesPerTile;
        }

        /// <summary>
        /// Gets the number of colors each pixel can access given the
        /// <paramref name="graphicsFormat"/>.
        /// </summary>
        /// <param name="graphicsFormat">
        /// The <see cref="GraphicsFormats"/> value of the pixel data.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> of the number of the maximum color index
        /// for each pixel.
        /// </returns>
        public static int ColorsPerPixel(GraphicsFormats graphicsFormat)
        {
            return 1 << BitsPerPixel(graphicsFormat);
        }

        /// <summary>
        /// Converts standard 8x8 SNES Tiles into an indexed pixel map, useful for
        /// drawing to a bitmap when combined with a palette (see the 
        /// <see cref="Render8x8(IntPtr, int, int, int, int, IntPtr, IntPtr, ushort, Render8x8Flags)"/> function).
        /// </summary>
        /// <param name="source">
        /// Byte array of SNES source graphics.
        /// </param>
        /// <param name="address">
        /// Starting offset to begin reading at.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 Tiles to convert
        /// </param>
        /// <param name="graphicsFormat">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// A byte array of the pixel map.
        /// </returns>
        /// <remarks>
        /// Each 8x8 Tile is converted into an array of 64 bytes.  Each byte represents 
        /// the color number of a single pixel.  1bpp Tiles use color numbers from 0-1, 
        /// 2bpp is 0-3, 3bpp is 0-7, 4bpp is 0-15, etc.  The bytes are in line order by 
        /// Tile (the first 8 bytes are for the 8 pixels on the top line of the first 
        /// 8x8 Tile, the next 8 bytes are for the 8 pixels on the second line of the 
        /// same 8x8 Tile, and so on until you get to the next Tile).
        /// 
        /// In other words, the format is basically like having a 256 color bitmap 
        /// with a width of 8 and a height of 8*NumTiles, except there is no palette
        /// included.
        /// 
        /// The source array size must be at least <paramref name="numTiles"/>*8*BPP bytes.
        /// </remarks>
        public static byte[] CreatePixelMap(byte[] source, int address, int numTiles, GraphicsFormats graphicsFormat)
        {
            byte[] data = new byte[numTiles * PixelsPerTile];
            fixed (byte* src = &source[address])
            fixed (byte* dest = data)
                return CreatePixelMap((IntPtr)src, (IntPtr)dest, numTiles, graphicsFormat) ? data : null;
        }

        /// <summary>
        /// Converts standard 8x8 SNES Tiles into an indexed pixel map, useful for
        /// drawing to a bitmap when combined with a palette (see the 
        /// <see cref="Render8x8(IntPtr, int, int, int, int, IntPtr, IntPtr, ushort, Render8x8Flags)"/> function).
        /// </summary>
        /// <param name="source">
        /// Pointer to byte array of SNES source graphics.
        /// </param>
        /// <param name="destination">
        /// Pointer to byte array of destination graphics.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 Tiles to convert.
        /// </param>
        /// <param name="graphicsFormat">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Each 8x8 Tile is converted into an array of 64 bytes.  Each byte represents 
        /// the color number of a single pixel.  1bpp Tiles use color numbers from 0-1, 
        /// 2bpp is 0-3, 3bpp is 0-7, 4bpp is 0-15, etc.  The bytes are in line order by 
        /// Tile (the first 8 bytes are for the 8 pixels on the top line of the first 
        /// 8x8 Tile, the next 8 bytes are for the 8 pixels on the second line of the 
        /// same 8x8 Tile, and so on until you get to the next Tile).
        /// 
        /// In other words, the format is basically like having a 256 color bitmap 
        /// with a width of 8 and a height of 8*NumTiles, except there is no palette
        /// included.
        /// 
        /// The source array size must be at least <paramref name="numTiles"/>*8*BPP bytes.
        /// The destination array size must be at least <paramref name="numTiles"/>*64 bytes.
        /// The <paramref name="source"/> and <paramref name="destination"/> variables must NOT point to the same array.
        /// </remarks>
        public static bool CreatePixelMap(IntPtr source, IntPtr destination, int numTiles, GraphicsFormats graphicsFormat)
        {
            switch (graphicsFormat)
            {
                case GraphicsFormats.Mode7_8BPP:
                    Memory.CopyMemory(destination, source, numTiles * BytesPerTile(graphicsFormat));
                    return true;
                default:
                    return LunarCreatePixelMap(source, destination, numTiles, graphicsFormat);
            }
        }

        /// <summary>
        /// Converts an indexed pixel map (such as one created with
        /// <see cref="CreatePixelMap(byte[], int, int, GraphicsFormats)"/>) into standard 8x8 SNES BPP Tiles.
        /// </summary>
        /// <param name="source">
        /// Byte array of source graphics.
        /// </param>
        /// <param name="address">
        /// Starting offset to begin reading at.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 Tiles to convert.
        /// </param>
        /// <param name="graphicsFormat">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// A byte array of the 8x8 SNES BPP Tiles
        /// </returns>
        /// <remarks>
        /// The source array size must be at least <paramref name="numTiles"/>*64 bytes.
        /// </remarks>
        public static byte[] CreateBPPMap(byte[] source, int address, int numTiles, GraphicsFormats graphicsFormat)
        {
            byte[] data = new byte[(numTiles * ((int)graphicsFormat & 0x0F)) << 3];
            fixed (byte* src = &source[address])
            fixed (byte* dest = data)
                return LunarCreateBppMap((IntPtr)src, (IntPtr)dest, numTiles, graphicsFormat) ? data : null;
        }

        /// <summary>
        /// Converts an indexed pixel map (such as one created with
        /// <see cref="CreatePixelMap(IntPtr, IntPtr, int, GraphicsFormats)"/> into standard 8x8 SNES BPP Tiles.
        /// </summary>
        /// <param name="source">
        /// Pointer to byte array of source graphics.
        /// </param>
        /// <param name="destination">
        /// Pointer to byte array of SNES destination graphics.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 Tiles to convert
        /// </param>
        /// <param name="graphicsFormat">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// True on sucess, false on fail.
        /// </returns>
        /// <remarks>
        /// The source array size must be at least <paramref name="numTiles"/>*64 bytes.
        /// The destination array size must be at least <paramref name="numTiles"/>*8*BPP bytes.
        /// The <paramref name="source"/> and <paramref name="destination"/> variables must NOT point to the same array.
        /// </remarks>
        public static bool CreateBPPMap(IntPtr source, IntPtr destination, int numTiles, GraphicsFormats graphicsFormat)
        {
            switch (graphicsFormat)
            {
                case GraphicsFormats.Mode7_8BPP:
                    Memory.CopyMemory(destination, source, numTiles * BytesPerTile(graphicsFormat));
                    return true;
                default:
                    return LunarCreateBppMap(source, destination, numTiles, graphicsFormat);
            }
        }

        /// <summary>
        /// Draws an SNES Tile to a PC bitmap (with optional effects). Both the sprite and
        /// BG Tile data types are supported.
        /// </summary>
        /// <param name="pixels">
        /// The pixel data as an array.
        /// </param>
        /// <param name="width">
        /// Width of the bitmap you're drawing to.
        /// </param>
        /// <param name="height">
        /// Height of the bitmap you're drawing to.
        /// </param>
        /// <param name="x">
        /// X-position in the bitmap to draw the Tile to.
        /// </param>
        /// <param name="y">
        /// Y-position in the bitmap to draw the Tile to.
        /// </param>
        /// <param name="pixelMap">
        /// A pixel map of SNES Tiles. The array should have at least 0x400 Tiles (0x10000 bytes) for
        /// BG Tiles, or 0x200 Tiles (0x8000 bytes) for sprite Tiles.
        /// </param>
        /// <param name="palette">
        /// An array of 32-bit ints containg the PC colors to use to render the Tiles. The array should
        /// contain at least 16 palettes of 16 colors each (0x400 bytes).
        /// </param>
        /// <param name="Tile">
        /// SNES data that defines the Tile number and flags used to render the Tile.
        /// </param>
        /// <param name="flags">
        /// Special flags for rendering. These flags can be combined.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool Render8x8(byte[] pixels, int width, int height, int x, int y, byte[] pixelMap, uint[] palette, ushort Tile, Render8x8Flags flags)
        {
            fixed (byte* scan0 = pixels)
            fixed (byte* map = pixelMap)
            fixed (uint* colors = palette)
                return Render8x8((IntPtr)scan0, width, height, x, y, (IntPtr)map, (IntPtr)colors, Tile, flags);
        }

        /// <summary>
        /// Draws an SNES Tile to a PC bitmap (with optional effects). Both the sprite and
        /// BG Tile data types are supported.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to the BitmapData Scan0 variable.
        /// </param>
        /// <param name="width">
        /// Width of the bitmap you're drawing to.
        /// </param>
        /// <param name="height">
        /// Height of the bitmap you're drawing to.
        /// </param>
        /// <param name="x">
        /// X-position in the bitmap to draw the Tile to.
        /// </param>
        /// <param name="y">
        /// Y-position in the bitmap to draw the Tile to.
        /// </param>
        /// <param name="pixelMap">
        /// Pointer to a pixel map of SNES Tiles. The array should have at least 0x400 Tiles (0x10000 bytes) for
        /// BG Tiles, or 0x200 Tiles (0x8000 bytes) for sprite Tiles.
        /// </param>
        /// <param name="palette">
        /// Pointer to an array of 32-bit ints containg the PC colors to use to render the Tiles. The array should
        /// contain at least 16 palettes of 16 colors each (0x400 bytes).
        /// </param>
        /// <param name="Tile">
        /// SNES data that defines the Tile number and flags used to render the Tile.
        /// </param>
        /// <param name="flags">
        /// Special flags for rendering. These flags can be combined.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool Render8x8(IntPtr scan0, int width, int height, int x, int y, IntPtr pixelMap, IntPtr palette, ushort Tile, Render8x8Flags flags)
        {
            return LunarRender8x8(scan0, width, height, x, y, pixelMap, palette, Tile, flags);
        }
        #endregion
    }
}
