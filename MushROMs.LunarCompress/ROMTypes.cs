
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies ROM addressing modes.
    /// </summary>
    public enum ROMTypes
    {
        /// <summary>
        /// No format specified.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// For LoROM games up to 32 Mbit ROM sizes.
        /// </summary>
        LoROM = 0x01,
        /// <summary>
        /// For HiROM games up to 32 Mbit ROM sizes.
        /// </summary>
        HiROM = 0x02,
        /// <summary>
        /// For HiROM games above 32 Mbit.
        /// </summary>
        ExHiROM = 0x04,
        /// <summary>
        /// For LoROM games above 32 Mbit.
        /// </summary>
        ExLoROM = 0x08,
        /// <summary>
        /// For LoROM games up to 32 Mbit ROM sizes. Always uses 80:8000 map.
        /// </summary>
        LoROM2 = 0x10,
        /// <summary>
        /// For HiROM games up to 32 Mbit ROM sizes. Always uses 40:0000 map.
        /// </summary>
        HiROM2 = 0x20,
    }
}
