using System;

namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifes flags to set when doing RATS-related functions. The flags can be combined.
    /// </summary>
    [Flags]
    public enum RATFunctionFlags : uint
    {
        /// <summary>
        /// Don't set any flags.
        /// </summary>
        None = 0,
        /// <summary>
        /// Avoid storing data at positions that cross LoROM bank boundaries.
        /// </summary>
        LoROM = 0x100,
        /// <summary>
        /// Avoid storing data at positions that cross HiROM bank boundaries.
        /// </summary>
        HiROM = 0x200,
        /// <summary>
        /// Similar to <see cref="LoROM"/>, but also avoids writing above 7F:0000.
        /// </summary>
        ExLoROM = 0x10000,
        /// <summary>
        /// Similar to <see cref="HiROM"/>, but also voids the 0-7FFF area of banks $70-$77 (SRAM), or writing above 7E:0000.
        /// </summary>
        ExHiROM = 0x400,
        /// <summary>
        /// Don't write a RATS tag for the data.
        /// </summary>
        NoWriteRAT = 0x2000,
        /// <summary>
        /// Don't write the data in the RATS tag.
        /// </summary>
        NoWriteData = 0x8000,
        /// <summary>
        /// If the data cannot be stored in the ROM, Lunar Compress.dll will expand the ROM to 1, 2, 3, or 4MB and try again.
        /// </summary>
        ExpandROM = 0x20000,
        /// <summary>
        /// Don't scan the supplied data and remove embedded RATS within it (done by setting the bytes to 0).
        /// </summary>
        NoScanData = 0x40000,
        /// <summary>
        /// Writes neither the RATS tag nor the data.
        /// </summary>
        NoWrite = 0x80000,
        /// <summary>
        /// Allow data storage across bank boundaries, but avoid inaccessible ExHiROM areas.
        /// </summary>
        ExHiROMRange = 0x100000,
        /// <summary>
        /// Allow data storage across bank boundaries, but avoid inaccessible ExLoROM areas.
        /// </summary>
        ExLoROMRange = 0x200000
    }
}
