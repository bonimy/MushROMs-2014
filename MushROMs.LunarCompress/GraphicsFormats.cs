
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Format of SNES graphics
    /// </summary>
    public enum GraphicsFormats
    {
        /// <summary>
        /// Use no format. This is a designer variable. Do not use with <see cref="LunarCompress"/> functions.
        /// </summary>
        None = 0,
        /// <summary>
        /// 1 bit per pixel. Uses color numbers 0-1.
        /// </summary>
        SNES_1BPP = 1,
        /// <summary>
        /// 2 bits per pixel. Uses color numbers 0-3.
        /// </summary>
        SNES_2BPP = 2,
        /// <summary>
        /// 3 bits per pixel. Uses color numbers 0-7.
        /// </summary>
        SNES_3BPP = 3,
        /// <summary>
        /// 4 bits per pixel. Uses color numbers 0-15.
        /// </summary>
        SNES_4BPP = 4,
        /// <summary>
        /// 5 bits per pixel. Uses color numbers 0-31.
        /// </summary>
        SNES_5BPP = 5,
        /// <summary>
        /// 6 bits per pixel. Uses color numbers 0-63.
        /// </summary>
        SNES_6BPP = 6,
        /// <summary>
        /// 7 bits per pixel. Uses color numbers 0-127.
        /// </summary>
        SNES_7BPP = 7,
        /// <summary>
        /// 8 bits per pixel. Uses color numbers 0-255.
        /// </summary>
        SNES_8BPP = 8,
        /// <summary>
        /// 4 bits per pixel. Uses color numbers 0-15. This format is intended for Game Boy Advanced. (Unofficial support)
        /// </summary>
        GBA_4BPP = 0x14,
        /// <summary>
        /// 8 bits per pixel. Uses color numbers 0-255. This format is intended for Mode7. (Unofficial support)
        /// </summary>
        Mode7_8BPP = 0x28
    }
}
