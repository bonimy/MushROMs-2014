using System.Collections.Generic;
using System.Text;
using MushROMs.Editors;
using MushROMs.LunarCompress;

namespace MushROMs.SNES
{
    /// <summary>
    /// Represents binary data as an array of banks organized by a specified format.
    /// </summary>
    public unsafe class ROM
    {
        /// <summary>
        /// The file extension of SMC ROM files.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionSMC = ".smc";
        /// <summary>
        /// The file extension of SFC ROM files.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionSFC = ".sfc";
        /// <summary>
        /// The file extension of SWC ROM files.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionSWC = ".swc";
        /// <summary>
        /// The file extension of FIG ROM files.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionFIG = ".fig";
        /// <summary>
        /// The file extension of BIN files.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionBIN = ".bin";

        /// <summary>
        /// Specifies the size, in bytes, of a header. This field is constant.
        /// </summary>
        public const int HeaderSize = 0x200;

        /// <summary>
        /// Specifies the size, in bytes, of a bank for a <see cref="ROMTypes.LoROM"/> formatted <see cref="ROM"/>.
        /// This field is constant.
        /// </summary>
        public const int LoBankSize = 0x8000;
        /// <summary>
        /// Specifies the size, in bytes, of a bank for a <see cref="ROMTypes.HiROM"/> formatted <see cref="ROM"/>.
        /// This field is constant.
        /// </summary>
        public const int HiBankSize = 0x10000;

        /// <summary>
        /// Specifies constants defining which header-type a <see cref="ROM"/> has.
        /// </summary>
        public enum HeaderTypes
        {
            /// <summary>
            /// The <see cref="ROM"/> has no header.
            /// </summary>
            NoHeader = 0,
            /// <summary>
            /// The <see cref="ROM"/> has a 512 byte copier header.
            /// </summary>
            Header = HeaderSize
        }

        internal static bool IsROMExt(string ext)
        {
            return ext == ROM.ExtensionSMC || ext == ROM.ExtensionSFC || ext == ExtensionSWC || ext == ExtensionFIG;
        }

        internal static string[] CreateFilter()
        {
            List<string> list = new List<string>();
            list.AddRange(new string[] {
                ROM.ExtensionSMC,
                ROM.ExtensionSFC,
                ROM.ExtensionSWC,
                ROM.ExtensionFIG });
            return list.ToArray();
        }
    }
}