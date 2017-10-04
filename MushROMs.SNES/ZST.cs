using System.Collections.Generic;
using System.IO;

namespace MushROMs.SNES
{
    internal static class ZST
    {
        /// <summary>
        /// The file extension of ZST save states.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionZST = ".zst";
        /// <summary>
        /// The file extension of ZST save states from .z00 to .z09.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionZSx = ".zs";
        /// <summary>
        /// The file extension of ZST save states from .z00 to .z99.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionZxx = ".z";
        /// <summary>
        /// The file extension of ZSS save states.
        /// This field is constant.
        /// </summary>
        internal const string ExtensionZSS = ".zss";

        /// <summary>
        /// The header a of ZSNES save state.
        /// </summary>
        internal const string ZSTHeader = "ZSNES Save State File V143";
        /// <summary>
        /// The byte offset of a ZSNES <see cref="Palette"/>.
        /// </summary>
        internal const int PaletteOffset = 0x618;

        internal static bool IsZSTFile(string path)
        {
            path = Path.GetExtension(path).ToLower();
            return path == ExtensionZST ||
                   path == ExtensionZSS ||
                   path.Length == 4 && path.StartsWith(ExtensionZSx) && path[3] >= '0' && path[3] <= '9' ||
                   path.Length == 4 && path.StartsWith(ExtensionZxx) && path[2] >= '1' && path[2] <= '9' && path[3] >= '0' && path[3] <= '9';
        }

        internal static string[] CreateFilter()
        {
            List<string> list = new List<string>();

            list.Add(ExtensionZST);
            list.Add(ExtensionZSS);

            for (int i = 0; i <= 9; i++)
                list.Add(ExtensionZSx + i.ToString());

            for (int i = 10; i <= 99; i++)
                list.Add(ExtensionZxx + i.ToString());

            return list.ToArray();
        }
    }
}
