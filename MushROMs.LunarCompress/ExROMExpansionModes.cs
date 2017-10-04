
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies the expansion method to use for the ROM when expanding beyond 4MB.
    /// </summary>
    public enum ExROMExpansionModes
    {
        /// <summary>
        /// 48 Mbit expansion.
        /// </summary>
        LC_48MbExHiROM1 = 48,
        /// <summary>
        /// Higher compatibility, but uses up to 1 meg of the new
        /// space. Do not use this unless the ROM doesn't load or
        /// has problems with the other options.
        /// </summary>
        LC_48MbExHiROM2 = (0x100 | 48),
        /// <summary>
        /// 64 Mbit expansion.
        /// </summary>
        LC_64MbExHiROM1 = 64,
        /// <summary>
        /// Higher compatibility, but uses up to 2 meg of the new
        /// space. Do not use this unless the ROM doesn't load or
        /// has problems with the other options.
        /// </summary>
        LC_64MbExHiROM2 = (0x100 | 64),
        /// <summary>
        /// For LoROMs that use the 00:8000-6F:FFFF map.
        /// </summary>
        LC_48MbExLoROM1 = (0x1000 | 48),
        /// <summary>
        /// For LoROMs that use the 80:8000-FF:FFFF map.
        /// </summary>
        LC_48MbExLoROM2 = (0x2000 | 48),
        /// <summary>
        /// Higher compatibility, but uses up most of the new space.
        /// Do not use this unless the ROM doesn't load or has
        /// problems with the other options.
        /// </summary>
        LC_48MbExLoROM3 = (0x4000 | 48),
        /// <summary>
        /// For LoROMs that use the 00:8000-6F:FFFF map.
        /// </summary>
        LC_64MbExLoROM1 = (0x1000 | 64),
        /// <summary>
        /// For LoROMs that use the 80:8000-FF:FFFF map.
        /// </summary>
        LC_64MbExLoROM2 = (0x2000 | 64),
        /// <summary>
        /// Higher compatibility, but uses up most of the new space.
        /// Do not use this unless the ROM doesn't load or has
        /// problems with the other options.
        /// </summary>
        LC_64MbExLoROM3 = (0x4000 | 64),
    }
}
