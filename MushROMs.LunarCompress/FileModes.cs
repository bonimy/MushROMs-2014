
namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies options on how a file should be handled when opened.
    /// </summary>
    public enum FileModes
    {
        /// <summary>
        /// Open an existing file in read-only mode (default).
        /// </summary>
        ReadOnly = 0x00,
        /// <summary>
        /// Open an existing file in read and write mode.
        /// </summary>
        ReadWrite = 0x01,
        /// <summary>
        /// Create a new file in read and write mode, ersase the existing file (if any).
        /// </summary>
        CreateReadWrite = 0x02
    }
}
