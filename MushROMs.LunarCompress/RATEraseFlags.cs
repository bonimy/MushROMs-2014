using System;

namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Used when determining data with no RATS tags which may be compressed data. The flags can be combined.
    /// </summary>
    [Flags]
    public enum RATEraseFlags : uint
    {
        /// <summary>
        /// Don't set any flags.
        /// </summary>
        None = 0,
        /// <summary>
        /// Don't erase the RATS tag for the data (if it exists).
        /// </summary>
        NoEraseRAT = 0x1000,
        /// <summary>
        /// Don't erase the data.
        /// </summary>
        NoEraseData = 0x4000,
    }
}
