using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    partial class GFX
    {
        #region Extension strings
        /// <summary>
        /// The file extension of CHR files.
        /// This field is constant.
        /// </summary>
        private const string ExtensionCHR = ".chr";
        /// <summary>
        /// The default extension of a new file.
        /// This field is constant.
        /// </summary>
        private const string FallbackNewFileExtension = ExtensionCHR;
        #endregion

        #region Formatting Methods
        /// <summary>
        /// Gets the <see cref="GFXFileFormats"/> value associated with the file type of
        /// <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        /// The path of the file.
        /// </param>
        /// <returns>
        /// The <see cref="GFXFileFormats"/> value associated with <see cref="path"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is null.
        /// </exception>
        public static GFXFileFormats GetFileFormat(string path)
        {
            // Check for empty strings first.
            if (path == string.Empty)
                return GFXFileFormats.None;

            // Get the extension
            string ext = Path.GetExtension(path).ToLower();

            // Check all possible palette file extensions.
            if (ext == ExtensionCHR)
                return GFXFileFormats.CHR;
            if (ROM.IsROMExt(ext))
                return GFXFileFormats.SNES;
            if (S9X.IsS9XExt(ext))
                return GFXFileFormats.S9X;
            if (ZST.IsZSTFile(ext))
                return GFXFileFormats.ZST;

            // Everything else will be read as a raw binary file.
            return GFXFileFormats.BIN;
        }

        /// <summary>
        /// Gets the standard file extension associated with a
        /// <see cref="GFXFileFormats"/> value.
        /// </summary>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value to find the
        /// extension of.
        /// </param>
        /// <returns>
        /// A file extension associated with <paramref name="format"/>.
        /// </returns>
        public static string GetExtension(GFXFileFormats format)
        {
            switch (format)
            {
                case GFXFileFormats.CHR:
                    return ExtensionCHR;
                case GFXFileFormats.BIN:
                    return ROM.ExtensionBIN;
                case GFXFileFormats.SNES:
                    return ROM.ExtensionSMC;
                case GFXFileFormats.ZST:
                    return ZST.ExtensionZST;
                case GFXFileFormats.S9X:
                    return S9X.Extension000;
                case GFXFileFormats.None:
                    return FallbackNewFileExtension;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }
        }

        public static bool IsValidSize(GraphicsFormats graphicsType, int size)
        {
            return size > 0 && (size % LC.BytesPerTile(graphicsType)) == 0;
        }

        public static int GetNumTiles(GraphicsFormats graphicsType, int size)
        {
            return size / LC.BytesPerTile(graphicsType);
        }

        /// <summary>
        /// Creates a file dialog extension filter given <paramref name="format"/>.
        /// </summary>
        /// <param name="format">
        /// The base <see cref="GFXFileFormats"/> value of the data.
        /// </param>
        /// <returns>
        /// A file dialog extension filter.
        /// </returns>
        public static string CreateFilter(GFXFileFormats format)
        {
            List<string> names = new List<string>();
            List<string[]> extensions = new List<string[]>();
            List<string> ext = new List<string>();

            names.Add(Resources.FilterOptionsAll);

            ext.AddRange(new string[] {
                ExtensionCHR,
                ROM.ExtensionBIN });

            if (format == GFXFileFormats.SNES || format == GFXFileFormats.None)
                ext.AddRange(ROM.CreateFilter());

            if (format == GFXFileFormats.S9X || format == GFXFileFormats.None)
                ext.AddRange(S9X.CreateFilter());

            if (format == GFXFileFormats.ZST || format == GFXFileFormats.None)
                ext.AddRange(ZST.CreateFilter());

            extensions.Add(ext.ToArray());

            if (format == GFXFileFormats.SNES || format == GFXFileFormats.None)
            {
                names.Add(Resources.FilterOptionsSNS);
                extensions.Add(ROM.CreateFilter());
            }

            names.Add(Resources.FilterOptionsCHR);
            extensions.Add(new string[] { ExtensionCHR });

            if (format == GFXFileFormats.S9X || format == GFXFileFormats.None)
            {
                names.Add(Resources.FilterOptionsS9X);
                extensions.Add(S9X.CreateFilter());
            }

            if (format == GFXFileFormats.ZST || format == GFXFileFormats.None)
            {
                names.Add(Resources.FilterOptionsZST);
                extensions.Add(ZST.CreateFilter());
            }

            names.Add(Resources.FilterOptionsBIN);
            extensions.Add(new string[] { ROM.ExtensionBIN });

            return IOHelper.CreateFilter(names, extensions);
        }
        #endregion
    }
}
