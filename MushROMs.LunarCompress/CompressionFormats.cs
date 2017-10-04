
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies the compression algorithm to use.
    /// </summary>
    public enum CompressionFormats
    {
        /// <summary>
        /// No compression format. This is a designer variable not intended to
        /// be used directly with <see cref="LunarCompress"/> functions.
        /// </summary>
        None = -1,
        /// <summary>
        /// LZ1 Compression Format. Found in Zelda3 and Japanese SMW.
        /// </summary>
        LZ1 = 0,
        /// <summary>
        /// LZ2 Compression Format. Found in SMW, YI, and Zelda3.
        /// </summary>
        LZ2 = 1,
        /// <summary>
        /// LZ3 Compression Format. Found in Pokemon Gold and Silver.
        /// </summary>
        LZ3 = 2,
        /// <summary>
        /// LZ4 Compression Format. Found in Super Mario RPG.
        /// </summary>
        LZ4 = 3,
        /// <summary>
        /// LZ5 Compression Format. Found in Metroid3, Super Mario Kart, and Sim City.
        /// </summary>
        LZ5 = 4,
        /// <summary>
        /// LZ6 Compression Format. Found in Sailor Moon and Sailor Moon R.
        /// </summary>
        LZ6 = 5,
        /// <summary>
        /// LZ7 Compression Format. Found in Secret of Mana.
        /// </summary>
        LZ7 = 6,
        /// <summary>
        /// LZ8 Compression Format. Found in Super Mario RPG.
        /// </summary>
        LZ8 = 7,
        /// <summary>
        /// LZ9 Compression Format. Found in Lufia 1/2.
        /// </summary>
        LZ9 = 8,
        /// <summary>
        /// LZ10 Compression Format. Found in RoboTrek.
        /// </summary>
        LZ10 = 9,
        /// <summary>
        /// LZ11 Compression Format. Found in Harvest Moon.
        /// </summary>
        LZ11 = 10,
        /// <summary>
        /// LZ12 Compression Format. Found in Gradius 3.
        /// </summary>
        LZ12 = 11,
        /// <summary>
        /// LZ13 Compression Format. Found in Chrono Trigger.
        /// </summary>
        LZ13 = 12,
        /// <summary>
        /// LZ14 Compression Format. Found in Famicom Tentai/Detective Club 2.
        /// </summary>
        LZ14 = 13,
        /// <summary>
        /// LZ15 Compression Format. Found in Star Fox and Star Fox 2.
        /// </summary>
        LZ15 = 14,
        /// <summary>
        /// LZ16 Compression Format. Found in Yoshi's Island.
        /// </summary>
        LZ16 = 15,
        /// <summary>
        /// LZ17 Compression Format. A format used by Angel for the Sailor Moon
        /// RPG game.
        /// </summary>
        LZ17 = 16,
        /// <summary>
        /// LZ18 Compression Format. A format used by Bandai for the Sailor
        /// Moon R side-scroller fighting game.
        /// </summary>
        LZ18 = 17,
        /// <summary>
        /// RLE1 Compression Format. Used in backgrounds of SMW.
        /// </summary>
        RLE1 = 100,
        /// <summary>
        /// RLE2 Compression Format. Used in overworld of SMW.
        /// </summary>
        RLE2 = 101,
        /// <summary>
        /// RLE3 Compression Format. Used in Mega Man X.
        /// </summary>
        RLE3 = 102,
        /// <summary>
        /// RLE4 Compression Format. Used in title screen of Radical Dreamers.
        /// </summary>
        RLE4 = 103
    }
}
