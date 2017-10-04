using System;

namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Specifies flags for extra options for IPS functions.
    /// </summary>
    [Flags]
    public enum IPSOptions : uint
    {
        /// <summary>
        /// No additional options.
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// Create a log file of the patch.
        /// </summary>
        Log = 0x80000000,
        /// <summary>
        /// Suppress all message boxes other than file name prompts.
        /// </summary>
        Quiet = 0x40000000,
        /// <summary>
        /// Display warnings for unusual but not fatal issues.
        /// </summary>
        ExtraWarnings = 0x20000000,
        /// <summary>
        /// Display "save as" dialog regardless of a file name being provided.
        /// </summary>
        ForceFileSaveAs = 0x10000000,
    }
}