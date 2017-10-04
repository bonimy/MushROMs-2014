
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies options on how to handle a file's memory array size.
    /// </summary>
    public enum LockArraySizeOptions
    {
        /// <summary>
        /// Do not lock array.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Lock array size and do not allow expansion.
        /// </summary>
        LockAndNoExpansion = 0x04,
        /// <summary>
        /// Lock array size except for expansion.
        /// </summary>
        LockWithExpansion = 0x08
    }
}
