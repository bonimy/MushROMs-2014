using System;
using System.ComponentModel;
using System.IO;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    unsafe partial class Palette
    {
        #region Events
        /// <summary>
        /// Occurs when <see cref="FileFormat"/> changes.
        /// </summary>
        public event EventHandler FileFormatChanged;
        #endregion

        #region Fields
        /// <summary>
        /// The untitled number for new files.
        /// </summary>
        private static int UntitledNumber;

        /// <summary>
        /// The <see cref="PaletteFileFormats"/> value of the
        /// <see cref="Palette"/> file data.
        /// </summary>
        private PaletteFileFormats fileFormat;
        /// <summary>
        /// A value the determines whether the file data originally came
        /// from a compressed file.
        /// </summary>
        private bool compressed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="PaletteFileFormats"/> value of the
        /// <see cref="Palette"/> file data.
        /// </summary>
        public PaletteFileFormats FileFormat
        {
            get { return this.fileFormat; }
            protected set { this.fileFormat = value; OnFileFormatChanged(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets a value the determines whether the file data
        /// originally came from a compressed file.
        /// </summary>
        public bool Compressed
        {
            get { return this.compressed; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the tile map length of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="mapL">
        /// The total number of tiles that make up the map.
        /// </param>
        /// <remarks>
        /// When calling this method, the method will become linear.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Number of colors is less than or equal to zero.
        /// </exception>
        protected override void SetMapLength(int mapL)
        {
            if (mapL <= 0)
                throw new ArgumentException(Resources.ErrorNumPaletteTiles);

            ResetData(mapL * SNESColorSize);
            base.SetMapLength(mapL);
        }

        protected override string CreateUntitledFileName()
        {
            return "Untitled" + (++UntitledNumber).ToString() + ".tpl";
        }

        /// <summary>
        /// Initialize the <see cref="Palette"/> data to have a specified
        /// number of colors.
        /// </summary>
        /// <param name="numColors">
        /// The number of colors of the new <see cref="Palette"/>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Number of colors is less than or equal to zero.
        /// </exception>
        public void Initialize(int numColors)
        {
            // File is being programmatically created.
            this.FileDataType = FileDataTypes.ProgramCreated;
            this.Untitled = CreateUntitledFileName();

            // Set the number of colors (the map length).
            this.MapLength = numColors;

            // Give all the colors a default value.
            ushort* colors = this[0];
            for (int i = numColors; --i >= 0; )
                colors[i] = 0x5555;

            // This has no predetermined file format.
            this.FileFormat = PaletteFileFormats.None;

            OnDataModified(EventArgs.Empty);
        }

        /// <summary>
        /// Initialize the <see cref="Palette"/> data from source data.
        /// </summary>
        /// <param name="data">
        /// The source color data to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The editor data is null.
        /// </exception>
        public void Initialize(IEditorData data)
        {
            // File is being programmatically created.
            this.FileDataType = FileDataTypes.ProgramCreated;
            this.Untitled = CreateUntitledFileName();

            // Properly cast the color data.
            EditorData copy = (EditorData)data;

            // Set the number of colors (the map length).
            this.MapLength = copy.Colors.Length;

            // Copy all the colors to the new palette.
            ushort* colors = this[0];
            fixed (ushort* src = copy.Colors)
                for (int i = this.MapLength; --i >= 0; )
                    colors[i] = src[i];

            // This has no predetermined file format.
            this.FileFormat = PaletteFileFormats.None;
        }

        /// <summary>
        /// Initialize the <see cref="Palette"/> data from
        /// binary data in a specified format.
        /// </summary>
        /// <param name="data">
        /// The source formatted data.
        /// </param>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value of the file data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// The file does not have the proper header for a SNES Tile Layer
        /// Pro Palette file.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The palette file format is unknown or not supported.
        /// </exception>
        public void Initialize(byte[] data, PaletteFileFormats format)
        {
            // File is being programmatically created.
            this.FileDataType = FileDataTypes.ProgramCreated;
            this.Untitled = CreateUntitledFileName();

            // Set the file data to the source data.
            this.FileData = data;

            // Determine whethere the data was originally compressed.
            if (this.compressed = format == PaletteFileFormats.S9X && GZip.IsCompressed(this.FileData))
                this.FileData = GZip.Decompress(this.FileData);

            // Load the palette data.
            fixed (byte* ptr = this.FileData)
                LoadPaletteData(ptr, this.FileData.Length, format);
        }

        /// <summary>
        /// Raises the <see cref="FileOpened"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// The file does not have the proper header for a SNES Tile Layer
        /// Pro Palette file.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The palette file format is unknown or not supported.
        /// </exception>
        protected override void OnFileOpened(EventArgs e)
        {
            // File has an associated path.
            this.FileDataType = FileDataTypes.ProgramCreated;

            // Get the file fromat from the path.
            PaletteFileFormats format = Palette.GetFileFormat(this.FilePath);

            // Determine whethere the data was originally compressed.
            if (this.compressed = format == PaletteFileFormats.S9X && GZip.IsCompressed(this.FileData))
                this.FileData = GZip.Decompress(this.FileData);

            // Load the palette data.
            fixed (byte* ptr = this.FileData)
                LoadPaletteData(ptr, this.FileData.Length, format);

            base.OnFileOpened(e);
        }

        /// <summary>
        /// Loads <see cref="Palette"/> data from a file source.
        /// </summary>
        /// <param name="data">
        /// A pointer to a byte array of the <see cref="Palette"/> file
        /// data.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of the data.
        /// </param>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value of the file data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// The file does not have the proper header for a SNES Tile Layer
        /// Pro Palette file.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The palette file format is unknown or not supported.
        /// </exception>
        private void LoadPaletteData(byte* data, int size, PaletteFileFormats format)
        {
            // Make sure the data actually exists.
            if ((IntPtr)data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorFileData);

            // Make sure the size of the data is valid for the given format.
            if (!IsValidSize(format, size))
                throw new ArgumentException(Resources.ErrorInvalidFileDataSize);

            // Set the number of colors (the map length).
            this.MapLength = GetNumColors(format, size);

            // Properly cast the data pointers.
            ushort* src = (ushort*)data;
            ushort* dest = (ushort*)this.Data.Data;

            // Load palette data depending on the format.
            switch (format)
            {
                case PaletteFileFormats.TPL:    // Tile Layer Pro palettes
                    
                    //Check to make sure the header is correct.
                    for (int i = TPLHeader.Length; --i >= 0; )
                        if (data[i] != TPLHeader[i])
                            throw new ArgumentException(Resources.ErrorTPLFormat);

                    // Offset the source pointer past the header.
                    src = (ushort*)(data += TPLHeader.Length);
                    break;

                case PaletteFileFormats.PAL:    // 24-bit RGB color data

                    // Set every color
                    for (int i = this.MapLength, x = this.MapLength * PALColorSize; --i >= 0; )
                    {
                        uint color = (uint)data[--x];
                        color |= (uint)data[--x] << 8;
                        color |= (uint)data[--x] << 0x10;
                        dest[i] = LC.PCtoSNESRGB(color);
                    }
                    goto end;

                case PaletteFileFormats.MW3:    // Lunar Magic palette data

                    // Set the back color.
                    this.backColor = src[this.MapLength];
                    break;

                case PaletteFileFormats.BIN:    // Raw binary data

                    // Do nothing. Treat as direct translation to palette data.
                    break;

                case PaletteFileFormats.SNES:   // SNES ROM files

                    // Ignore header data if it exists.
                    src = (ushort*)(data += size & ROM.LoBankSize);
                    break;

                case PaletteFileFormats.ZST:    // ZSNES save state data

                    // Offset the source pointer to the ZSNES palette offset.
                    src = (ushort*)(data += ZST.PaletteOffset);
                    break;

                case PaletteFileFormats.S9X:    // SNES9x save state

                    // Get the version of the SNES9x save state.
                    S9X.Version version = S9X.GetVersion((IntPtr)data);

                    // Make sure we actually got a valid version.
                    if (version == S9X.Version.None)
                        throw new ArgumentException(Resources.ErrorS9XVersion);

                    // Get the save state's internal path's string length.
                    int pathLen = S9X.GetPathLength((IntPtr)data);

                    // Make sure the SNES9x save state is valid.
                    if (size != S9X.GetSize(version) + pathLen)
                        throw new ArgumentException(Resources.ErrorS9XFormat);

                    // Update the data to the palette offset.
                    data += S9X.GetPaletteOffset(version) + pathLen;

                    // Get the color data.
                    for (int i = this.MapLength, x = this.Data.Size; --i >= 0; )
                        dest[i] = (ushort)(data[--x] | (data[--x] << 8));
                    goto end;

                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }

            // Copy all the color data for given file formats.
            for (int i = this.MapLength; --i >= 0; )
                dest[i] = src[i];

        end:
            // Set the file format.
            this.FileFormat = format;
        }

        /// <summary>
        /// Raises the <see cref="Editor.FileSaving"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Cannot save data as a save state if it was not originally a save state.
        /// There is no referenced save state data to include.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// This operation is not supported on the current platform.  -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        protected override void  OnFileSaving(EventArgs e)
        {
            // File now has an associated path.
            this.FileDataType = FileDataTypes.FromFile;

            // Get the new file format.
            PaletteFileFormats format = GetFileFormat(this.FilePath);

            // The new file data.
            byte[] data;

            // Determine how the file data will be initialized.
            switch (format)
            {
                case PaletteFileFormats.S9X:    // Save states
                case PaletteFileFormats.ZST:

                    if (File.Exists(this.FilePath))         // Get the file data from the new path
                        data = File.ReadAllBytes(this.FilePath);
                    else if (this.fileFormat == format)     // Use the source file data.
                        data = this.FileData;
                    else
                        throw new ArgumentException(Resources.ErrorSaveStateFile);
                    break;

                default:    // Create the new file data for any other format.
                    data = new byte[GetFormatSize(format, this.MapLength)];
                    break;
            }

            // Decompress SNES9x save state data.
            if (format == PaletteFileFormats.S9X && GZip.IsCompressed(data))
                data = GZip.Decompress(data);

            // Save the palette data.
            fixed (byte* ptr = data)
                SavePaletteData(ptr, data.Length, format);

            // Compress SNES9x save state data.
            if (format == PaletteFileFormats.S9X && this.compressed)
                data = GZip.Compress(data);

            // Set the file data.
            this.FileData = data;

            base.OnFileSaving(e);
        }

        /// <summary>
        /// Save the raw <see cref="Palette"/> data in the specified file format.
        /// </summary>
        /// <param name="data">
        /// A pointer to a byte array of the <see cref="Palette"/> file
        /// data.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of the data.
        /// </param>
        /// <param name="format">
        /// The <see cref="PaletteFileFormats"/> value of the file data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// The file does not have the proper header for a SNES Tile Layer
        /// Pro Palette file.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The palette file format is unknown or not supported.
        /// </exception>
        private void SavePaletteData(byte* data, int size, PaletteFileFormats format)
        {
            // Make sure the data actually exists.
            if ((IntPtr)data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorFileData);

            // Make sure the size of the data is valid for the given format.
            if (!IsValidSize(format, size))
                throw new ArgumentException(Resources.ErrorInvalidFileDataSize);

            // Properly cast the data pointers.
            ushort* src = this[0];
            ushort* dest = (ushort*)data;

            // Save paltte data depending on the format.
            switch (format)
            {
                case PaletteFileFormats.TPL:    // Tile Layer Pro palettes

                    //Add the file header.
                    for (int i = TPLHeader.Length; --i >= 0; )
                        data[i] = TPLHeader[i];

                    // Offset the destination pointer past the header.
                    dest = (ushort*)(data += TPLHeader.Length);
                    break;

                case PaletteFileFormats.PAL:    // 24-bit RGB color data

                    // Set every color.
                    for (int i = this.MapLength, x = this.MapLength * PALColorSize; --i >= 0; )
                    {
                        uint color = LC.SNEStoPCRGB(src[i]);
                        data[--x] = (byte)color;
                        data[--x] = (byte)(color >> 8);
                        data[--x] = (byte)(color >> 0x10);
                    }
                    goto end;

                case PaletteFileFormats.MW3:    // Lunar Magic palette data

                    // Set the back color.
                    dest[this.MapLength] = this.backColor;
                    break;

                case PaletteFileFormats.SNES:   // SNES ROM files
                    // Ignore header data if it exists.
                    dest = (ushort*)(data += size % ROM.HeaderSize);
                    break;

                case PaletteFileFormats.BIN:    // Raw binary data.

                    // Do nothing. Treat as direct translation to palette data.
                    break;

                case PaletteFileFormats.ZST:    //ZSNES save state data

                    // Offset the destination pointer to the ZSNES palette offset.
                    dest = (ushort*)(data += ZST.PaletteOffset);
                    break;

                case PaletteFileFormats.S9X:    // SNES9x save state

                    // Get the version of the SNES9x save state.
                    S9X.Version version = S9X.GetVersion((IntPtr)data);

                    // Make sure we actually got a valid version.
                    if (version == S9X.Version.None)
                        throw new ArgumentException(Resources.ErrorS9XVersion);

                    // Get the save state's internal path's string length.
                    int pathLen = S9X.GetPathLength((IntPtr)data);

                    // Make sure the SNES9x save state is valid.
                    if (size != S9X.GetSize(version) + pathLen)
                        throw new ArgumentException(Resources.ErrorS9XFormat);

                    // Update the data to the palette offset.
                    data += S9X.GetPaletteOffset(version) + pathLen;

                    // Set every color
                    for (int i = this.MapLength, x = this.Data.Size; --i >= 0; )
                    {
                        ushort color = src[i];
                        data[--x] = (byte)color;
                        data[--x] = (byte)(color >> 8);
                    }
                    goto end;

                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorPaletteFileFormatUnknown);
            }

            // Copy all the color data for the given file formats.
            for (int i = this.MapLength; --i >= 0; )
                dest[i] = src[i];

        end:
            // Set the file format.
            this.FileFormat = format;
        }

        /// <summary>
        /// Raises the <see cref="FileFormatChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnFileFormatChanged(EventArgs e)
        {
            if (FileFormatChanged != null)
                FileFormatChanged(this, e);
        }
        #endregion
    }

    #region Enumerations
    /// <summary>
    /// Specifies cosntants defining different file formats for
    /// <see cref="Palette"/> data.
    /// </summary>
    public enum PaletteFileFormats
    {
        /// <summary>
        /// No file format was specified. This usually specifies
        /// a new file.
        /// </summary>
        None = -1,
        /// <summary>
        /// A Tile Layer Pro <see cref="Palette"/> file. These
        /// files have a header of a "TPL" string followed by
        /// the byte value 0x02 to signify SNES color data.
        /// </summary>
        TPL = 0,
        /// <summary>
        /// A 24-bit RGB color data format.
        /// </summary>
        PAL = 1,
        /// <summary>
        /// Lunar Magic's <see cref="Palette"/> file. These files
        /// have a terminating color which defined the background
        /// of Super Mario World levels.
        /// </summary>
        MW3 = 2,
        /// <summary>
        /// Binary data. This data has no special properties or headers
        /// but is instead treated as a direct translation to raw
        /// <see cref="Palette"/> color data.
        /// </summary>
        BIN = 3,
        /// <summary>
        /// SNES9x save state file. The color data is pulled from the VRAM
        /// and is always 256 colors.
        /// </summary>
        S9X = 4,
        /// <summary>
        /// ZSNES save state file. The color data is pulled form the VRAM and
        /// is always 256 colors.
        /// </summary>
        ZST = 5,
        /// <summary>
        /// Reads from an SNES ROM. The header is always ignored if it is found
        /// to exist. While not all of the data is <see cref="Palette"/> data,
        /// it is treated as such.
        /// </summary>
        SNES = 6
    }
    #endregion
}