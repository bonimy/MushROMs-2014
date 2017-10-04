using System;

namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies flags for rendering 8x8 Tiles. The flags can be combined.
    /// </summary>
    [Flags]
    public enum Render8x8Flags : uint
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,
        /// <summary>.
        /// Invert the transparent area of the Tile.
        /// </summary>
        InvertTransparent = 1,
        /// <summary>
        ///  Invert the non-transparent area of the Tile.
        /// </summary>
        InvertOpaque = 2,
        /// <summary>
        /// Invert the Tile colors. Combination of <see cref="InvertTransparent"/> and <see cref="InvertOpaque"/>.
        /// </summary>
        Invert = InvertTransparent | InvertOpaque,
        /// <summary>
        /// Highlight the transparent area of the Tile red.
        /// </summary>
        RedTransparent = 4,
        /// <summary>
        /// Highlight the non-transparent area of the Tile red.
        /// </summary>
        RedOpaque = 8,
        /// <summary>
        /// Hightlight the Tile red. Combination of <see cref="RedTransparent"/> and <see cref="RedOpaque"/>.
        /// </summary>
        Red = RedTransparent | RedOpaque,
        /// <summary>
        /// Highlight the transparent area of the Tile green.
        /// </summary>
        GreenTransparent = 0x10,
        /// <summary>
        /// Highlight the non-transparent area of the Tile green.
        /// </summary>
        GreenOpaque = 0x20,
        /// <summary>
        /// Highlight the Tile green. Combination of <see cref="GreenTransparent"/> and <see cref="GreenOpaque"/>.
        /// </summary>
        Green = GreenTransparent | GreenOpaque,
        /// <summary>
        /// Highlight the transparent area of the Tile blue.
        /// </summary>
        BlueTransparent = 0x40,
        /// <summary>
        /// Highlight the non-transparent area of the Tile blue.
        /// </summary>
        BlueOpaque = 0x80,
        /// <summary>
        /// Highlight the Tile blue. Combination of <see cref="BlueTransparent"/> and <see cref="BlueOpaque"/>.
        /// </summary>
        Blue = BlueTransparent | BlueOpaque,
        /// <summary>
        /// Make the Tile translucent.
        /// </summary>
        Translucent = 0x100,
        /// <summary>
        /// Half-color mode.
        /// </summary>
        HalfColor = 0x200,
        /// <summary>
        /// SNES sub-screen addition mode.
        /// (Cannot use with <see cref="ScreenSub"/>).
        /// </summary>
        ScreenAdd = 0x400,
        /// <summary>
        /// SNES sub-screen subtraction mode.
        /// (Cannot use with <see cref="ScreenAdd"/>).
        /// </summary>
        ScreenSub = 0x800,
        /// <summary>
        /// Draw Tile only if Priority==0.
        /// </summary>
        Priority0 = 0x1000,
        /// <summary>
        /// Draw Tile only if Priority==1.
        /// </summary>
        Priority1 = 0x2000,
        /// <summary>
        /// Draw Tile only if Priority==2 (valid for <see cref="Sprite"/> only).
        /// </summary>
        Priority2 = 0x4000,
        /// <summary>
        /// Draw Tile only if Priority==3 (valid for <see cref="Sprite"/> only).
        /// </summary>
        Priority3 = 0x8000,
        /// <summary>
        /// Draw the Tile.
        /// </summary>
        Draw = Priority0 | Priority1 | Priority2 | Priority3,
        /// <summary>
        /// Draw transparent area of Tile opaque.
        /// </summary>
        Opaque = 0x10000,
        /// <summary>
        /// Uses sprite format for Map8Tile, adds 8 to palette #.
        /// </summary>
        Sprite = 0x20000,
        /// <summary>
        /// Make Tile translucent only if palette>=12.
        /// </summary>
        SpriteTranslucent = 0x40000,
        /// <summary>
        /// Draw Tile using 2bpp mode (default is 4bpp).
        /// </summary>
        GFX2BPP = 0x80000,
        /// <summary>
        /// Draw 16x16 Tile (default is 8x8).
        /// </summary>
        Tile16 = 0x100000,
        /// <summary>
        /// Draw 32x32 Tile (valid for <see cref="Sprite"/> only).
        /// </summary>
        Tile32 = 0x200000,
        /// <summary>
        /// Draw 64x64 Tile (valid for <see cref="Sprite"/> only).
        /// </summary>
        Tile64 = 0x400000
    }
}
