
namespace MushROMs.Editors
{
    /// <summary>
    /// Specifies constants defining the file data of an <see cref="Editor"/>.
    /// </summary>
    public enum FileDataTypes
    {
        /// <summary>
        /// The editor has no file data.
        /// </summary>
        NotAFile = -1,
        /// <summary>
        /// The file data was created internally rather than
        /// from an existing file.
        /// </summary>
        ProgramCreated = 0,
        /// <summary>
        /// The file data is from an existing file.
        /// </summary>
        FromFile = 1,
    }
}
