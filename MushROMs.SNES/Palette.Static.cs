using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using MushROMs.Editors;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    partial class Palette
    {
        #region Constant and read-only fields
        /// <summary>
        /// The file extension of TPL files.
        /// This field is constant.
        /// </summary>
        private const string ExtensionTPL = ".tpl";
        /// </summary>
        /// <summary>
        /// The file extension of MW3 files.
        /// This field is constant.
        /// </summary>
        private const string ExtensionMW3 = ".mw3";
        /// <summary>
        /// The file extension of PAL files.
        /// This field is constant.
        /// </summary>
        private const string ExtensionPAL = ".pal";
        /// <summary>
        /// The default extension of a new file.
        /// This field is constant.
        /// </summary>
        private const string FallbackNewFileExtension = ExtensionTPL;

        /// <summary>
        /// The four-byte header of an SNES TPL file.
        /// </summary>
        private static readonly byte[] TPLHeader = {
            (byte)'T',
            (byte)'P',
            (byte)'L',
            (byte)2 };
        #endregion

        #region Methods
        /// <summary>
        /// Gets the <see cref="PaletteFileFormats"/> value associated with the file type of
        /// <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        /// The path of the file.
        /// </param>
        /// <returns>
        /// The <see cref="PaletteFileFormats"/> value associated with <see cref="path"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is null.
        /// </exception>
        public static PaletteFileFormats GetFileFormat(string path)
        {
            // Check for empty strings first.
            if (path == string.Empty)
                return PaletteFileFormats.None;

            // Get the extension
            string ext = Path.GetExtension(path).ToLower();

            // Check all possible palette file extensions.
            if (ext == ExtensionTPL)
                return PaletteFileFormats.TPL;
            if (ext == ExtensionMW3)
                return PaletteFileFormats.MW3;
            if (ext == ExtensionPAL)
                return PaletteFileFormats.PAL;
            if (ROM.IsROMExt(ext))
                return PaletteFileFormats.SNES;
            if (S9X.IsS9XExt(ext))
                return PaletteFileFormats.S9X;
            if (ZST.IsZSTFile(ext))
                return PaletteFileFormats.ZST;

            // Everything else will be read as a raw binary file.
            return PaletteFileFormats.BIN;
        }

        /// <summary>
        /// Gets the standard file extension associated with a
        /// <see cref="PaletteFileFormats"/> value.
        /// </summary>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value to find the
        /// extension of.
        /// </param>
        /// <returns>
        /// A file extension associated with <paramref name="format"/>.
        /// </returns>
        public static string GetExtension(PaletteFileFormats format)
        {
            switch (format)
            {
                case PaletteFileFormats.TPL:
                    return ExtensionTPL;
                case PaletteFileFormats.MW3:
                    return ExtensionMW3;
                case PaletteFileFormats.PAL:
                    return ExtensionPAL;
                case PaletteFileFormats.BIN:
                    return ROM.ExtensionBIN;
                case PaletteFileFormats.SNES:
                    return ROM.ExtensionSMC;
                case PaletteFileFormats.ZST:
                    return ZST.ExtensionZST;
                case PaletteFileFormats.S9X:
                    return S9X.Extension000;
                case PaletteFileFormats.None:
                    return FallbackNewFileExtension;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }
        }

        /// <summary>
        /// Determines whether <paramref name="size"/> is a valid size given <paramref name="format"/>.
        /// </summary>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of the data.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="size"/> is a valid size, otherwise false.
        /// </returns>
        public static bool IsValidSize(PaletteFileFormats format, int size)
        {
            switch (format)
            {
                case PaletteFileFormats.TPL:
                    return size >= TPLHeader.Length && (size - TPLHeader.Length) % SNESColorSize == 0;
                case PaletteFileFormats.PAL:
                    return size > 0 && size % PALColorSize == 0;
                case PaletteFileFormats.MW3:
                    return size >= SNESColorSize && (size - SNESColorSize) % SNESColorSize == 0;
                case PaletteFileFormats.BIN:
                case PaletteFileFormats.None:
                    return size > 0 && size % SNESColorSize == 0;
                case PaletteFileFormats.SNES:
                    return size > 0 && (size & ~ROM.HeaderSize) % ROM.LoBankSize == 0;
                case PaletteFileFormats.ZST:
                    return size >= ZST.PaletteOffset + (SNESPaletteSize * SNESColorSize);
                case PaletteFileFormats.S9X:
                    return size >= S9X.S9XSize151 || size >= S9X.S9XSize153;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }
        }

        /// <summary>
        /// Gets the number of colors given <paramref name="format"/> and
        /// <paramref name="size"/>.
        /// </summary>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of the data.
        /// </param>
        /// <returns>
        /// The number of colors of the data.
        /// </returns>
        public static int GetNumColors(PaletteFileFormats format, int size)
        {
            switch (format)
            {
                case PaletteFileFormats.TPL:
                    return (size - TPLHeader.Length) / SNESColorSize;
                case PaletteFileFormats.PAL:
                    return size / PALColorSize;
                case PaletteFileFormats.MW3:
                    return (size / SNESColorSize) - 1;
                case PaletteFileFormats.BIN:
                case PaletteFileFormats.None:
                    return size / SNESColorSize;
                case PaletteFileFormats.SNES:
                    return (size & ~ROM.HeaderSize) / SNESColorSize;
                case PaletteFileFormats.S9X:
                case PaletteFileFormats.ZST:
                    return SNESPaletteSize;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }
        }

        /// <summary>
        /// Gets the size, in bytes, given <paramref name="format"/> and
        /// <paramref name="numColors"/>.
        /// </summary>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value.
        /// </param>
        /// <param name="numColors">
        /// The number of colors of the data.
        /// </param>
        /// <returns>
        /// The size, in bytes, of the data.
        /// </returns>
        public static int GetFormatSize(PaletteFileFormats format, int numColors)
        {
            switch (format)
            {
                case PaletteFileFormats.TPL:
                    return numColors * SNESColorSize + TPLHeader.Length;
                case PaletteFileFormats.PAL:
                    return numColors * PALColorSize;
                case PaletteFileFormats.MW3:
                    return (numColors + 1) * SNESColorSize;
                case PaletteFileFormats.BIN:
                case PaletteFileFormats.SNES:
                case PaletteFileFormats.None:
                    return numColors * SNESColorSize;
                case PaletteFileFormats.ZST:
                case PaletteFileFormats.S9X:
                    throw new InvalidEnumArgumentException(Resources.ErrorSaveStateSize);
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }
        }

        /// <summary>
        /// Creates a file dialog extension filter given <paramref name="format"/>.
        /// </summary>
        /// <param name="format">
        /// The base <see cref="PaletteFileFormats"/> value of the data.
        /// </param>
        /// <returns>
        /// A file dialog extension filter.
        /// </returns>
        public static string CreateFilter(PaletteFileFormats format)
        {
            List<string> names = new List<string>();
            List<string[]> extensions = new List<string[]>();
            List<string> ext = new List<string>();

            names.Add(Resources.FilterOptionsAll);

            ext.AddRange(new string[] {
                ExtensionTPL,
                ExtensionPAL,
                ExtensionMW3,
                ROM.ExtensionBIN });

            if (format == PaletteFileFormats.SNES || format == PaletteFileFormats.None)
                ext.AddRange(ROM.CreateFilter());

            if (format == PaletteFileFormats.S9X || format == PaletteFileFormats.None)
                ext.AddRange(S9X.CreateFilter());

            if (format == PaletteFileFormats.ZST || format == PaletteFileFormats.None)
                ext.AddRange(ZST.CreateFilter());

            extensions.Add(ext.ToArray());

            if (format == PaletteFileFormats.SNES || format == PaletteFileFormats.None)
            {
                names.Add(Resources.FilterOptionsSNS);
                extensions.Add(ROM.CreateFilter());
            }

            names.Add(Resources.FilterOptionsTPL);
            extensions.Add(new string[] { Palette.ExtensionTPL });

            names.Add(Resources.FilterOptionsPAL);
            extensions.Add(new string[] { Palette.ExtensionPAL });

            names.Add(Resources.FilterOptionsMW3);
            extensions.Add(new string[] { Palette.ExtensionMW3 });

            if (format == PaletteFileFormats.S9X || format == PaletteFileFormats.None)
            {
                names.Add(Resources.FilterOptionsS9X);
                extensions.Add(S9X.CreateFilter());
            }

            if (format == PaletteFileFormats.ZST || format == PaletteFileFormats.None)
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