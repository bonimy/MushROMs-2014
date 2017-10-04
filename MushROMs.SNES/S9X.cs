using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    internal static class S9X
    {
        /// <summary>
        /// The file extension of S9X save states.
        /// This field is constant.
        /// </summary>
        internal const string Extension000 = ".000";
        /// <summary>
        /// The file extension of S9X save states from .000 to .009.
        /// This field is constant.
        /// </summary>
        internal const string Extension00x = ".00";

        /// <summary>
        /// The header of a SNES9x v1.51 save state.
        /// </summary>
        internal const string S9XHeader151 = "#!snes9x";
        /// <summary>
        /// The header of a SNES9x v1.53 save state.
        /// </summary>
        internal const string S9XHeader153 = "#!s9xsnp";
        /// <summary>
        /// A token representation of a SNES9x v1.53 version number.
        /// </summary>
        internal const int S9XV153 = 153;
        /// <summary>
        /// A token representation of a SNES9x v1.51 version number.
        /// </summary>
        internal const int S9XV151 = 151;
        /// <summary>
        /// The byte size (minus file name) of a SNES9x v1.53 save state.
        /// </summary>
        internal const int S9XSize153 = 0x11D073;
        /// <summary>
        /// The byte size (minus file name) of a SNES9x v1.51 save state.
        /// </summary>
        internal const int S9XSize151 = 0x11C670;
        /// <summary>
        /// The byte offset (minus file name) of a SNES9x v1.53 <see cref="Palette"/>.
        /// </summary>
        internal const int PaletteOffset153 = 0xB9;
        /// <summary>
        /// The byte offset (minus file name) of a SNES9x v1.53 <see cref="Palette"/>.
        /// </summary>
        internal const int PaletteOffset151 = 0xBE;
        /// <summary>
        /// The byte address of the SNES9x file path length.
        /// </summary>
        internal const int PathSizeAddress = 0x12;
        /// <summary>
        /// The number of digits on a the SNES9x file path length.
        /// </summary>
        internal const int PathDigits = 6;

        internal static bool IsS9XExt(string path)
        {
            path = Path.GetExtension(path).ToLower();
            return path.Length == 4 && path.StartsWith(Extension00x) && path[3] >= '0' && path[3] <= '9';
        }

        internal static string[] CreateFilter()
        {
            List<string> list = new List<string>();
            for (int i = 0; i <= 9; i++)
                list.Add(Extension00x + i.ToString());
            return list.ToArray();
        }

        /// <summary>
        /// Gets the <see cref="Version"/> of the save state.
        /// </summary>
        /// <param name="data">
        /// The save state data.
        /// </param>
        /// <returns>
        /// The <see cref="Version"/> number.
        /// </returns>
        /// <remarks>
        /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
        /// is thrown if <paramref name="address"/> is outside the bounds
        /// of the <see cref="Data"/> array. Therefore, it is up to the
        /// user to carefully monitor improper accesses to unsafe memory
        /// otherwise it will lead to undefined behavior.
        /// </remarks>
        internal unsafe static Version GetVersion(IntPtr data)
        {
            // Compare the data with the different SNES9x save state headers.
            if (new string((sbyte*)data, 0, S9X.S9XHeader151.Length) == S9X.S9XHeader151)
                return Version.V151;
            else if (new string((sbyte*)data, 0, S9X.S9XHeader153.Length) == S9X.S9XHeader153)
                return Version.V153;

            // Return none if no headers matched.
            return Version.None;
        }

        /// <summary>
        /// Gets the length of the internal file path of the SNES9x save
        /// state.
        /// </summary>
        /// <param name="data">
        /// The save state data.
        /// </param>
        /// <returns>
        /// The string length of the file path.
        /// </returns>
        /// <remarks>
        /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
        /// is thrown if <paramref name="address"/> is outside the bounds
        /// of the <see cref="Data"/> array. Therefore, it is up to the
        /// user to carefully monitor improper accesses to unsafe memory
        /// otherwise it will lead to undefined behavior.
        /// </remarks>
        internal static unsafe int GetPathLength(IntPtr data)
        {
            int length = 0;
            if (!int.TryParse(new string((sbyte*)data, S9X.PathSizeAddress, S9X.PathDigits), out length))
                throw new ArgumentException(Resources.ErrorS9XFormat);
            return length;
        }

        /// <summary>
        /// Gets the size of the SNES9x data excluding the path size of
        /// its reference file.
        /// </summary>
        /// <param name="version">
        /// The <see cref="Version"/> of the save state.
        /// </param>
        /// <returns>
        /// The size, in bytes, of the save state.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// SNES9x save state is an unknown format.
        /// </exception>
        internal static int GetSize(Version version)
        {
            switch (version)
            {
                case Version.V151:
                    return S9XSize151;
                case Version.V153:
                    return S9XSize153;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorS9XVersion);
            }
        }

        /// <summary>
        /// Gets the byte offset of the <see cref="Palette"/> data of the
        /// SNES9x save state.
        /// </summary>
        /// <param name="version">
        /// The <see cref="Version"/> of the save state.
        /// </param>
        /// <returns>
        /// The byte offset of the <see cref="Palette"/> data.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// SNES9x save state is an unknown format.
        /// </exception>
        internal static int GetPaletteOffset(Version version)
        {
            switch (version)
            {
                case Version.V151:
                    return PaletteOffset151;
                case Version.V153:
                    return PaletteOffset153;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorS9XVersion);
            }
        }

        /// <summary>
        /// Specifies constant defining which SNES9x version the save state is from.
        /// </summary>
        internal enum Version
        {
            /// <summary>
            /// Not a SNES9x save state file.
            /// </summary>
            None,
            /// <summary>
            /// SNES9x v1.51 save states.
            /// </summary>
            V151,
            /// <summary>
            /// SNES9x v1.53 save states.
            /// </summary>
            V153
        }
    }
}
