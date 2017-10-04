
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies bank types and boundaries.
    /// </summary>
    public enum BankTypes
    {
        /// <summary>
        /// Ignore bank boundaries and header.
        /// </summary>
        NoBank = 0x00,
        /// <summary>
        /// LoROM
        /// </summary>
        LoROM = 0x01,
        /// <summary>
        /// HiROM
        /// </summary>
        HiROM = 0x02
    }
}
