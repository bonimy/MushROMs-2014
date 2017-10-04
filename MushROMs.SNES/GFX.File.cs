using System;
using System.ComponentModel;
using System.IO;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    unsafe partial class GFX
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
        /// The <see cref="GFXFileFormats"/> value of the
        /// <see cref="GFX"/> file data.
        /// </summary>
        private GFXFileFormats fileFormat;
        /// <summary>
        /// A value the determines whether the file data originally came
        /// from a compressed file.
        /// </summary>
        private bool compressed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="GFXFileFormats"/> value of the
        /// <see cref="GFX"/> file data.
        /// </summary>
        public GFXFileFormats FileFormat
        {
            get { return this.fileFormat; }
            set { this.fileFormat = value; OnFileFormatChanged(EventArgs.Empty); }
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
        /// Number of tiles is less than or equal to zero.
        /// </exception>
        protected override void SetMapLength(int mapL)
        {
            if (mapL <= 0)
                throw new ArgumentException(Resources.ErrorNumGFXTiles);

            base.SetMapLength(mapL);
            ResetData(this.MapLength * this.BytesPerTile * sizeof(byte));
        }

        protected override string CreateUntitledFileName()
        {
            return "Untitled" + (++UntitledNumber).ToString() + ".chr";
        }

        /// <summary>
        /// Initialize the <see cref="GFX"/> to have a specified
        /// number of tiles.
        /// </summary>
        /// <param name="numTiles">
        /// The number of tiles of the new <see cref="GFX"/>.
        /// </param>
        /// <param name="format">
        /// The <see cref="GraphicsFormats"/> value of the GFX.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Number of tiles is less than or equal to zero.
        /// </exception>
        public void Initialize(int numTiles)
        {
            // File is being programmatically created.
            this.FileDataType = FileDataTypes.ProgramCreated;

            // Set the number of tiles (the map length).
            this.MapLength = numTiles;

            // Clear all the tile pixel data.
            byte* tiles = this[0];
            for (int i = numTiles; --i >= 0; )
                tiles[i] = 0;
        }

        public void Initialize(IEditorData data)
        {
            // File is being programmatically created.
            this.FileDataType = FileDataTypes.ProgramCreated;

            // Properly cast the color data.
            EditorData copy = (EditorData)data;

            // Set the number of colors (the map length).
            this.MapLength = copy.Pixels.Length / LC.BytesPerTile(copy.GraphicsFormat);

            // Copy all the colors to the new palette.
            byte* colors = this[0];
            fixed (byte* src = copy.Pixels)
                for (int i = copy.Pixels.Length; --i >= 0; )
                    colors[i] = src[i];

            // This has no predetermined file format.
            this.FileFormat = GFXFileFormats.None;
        }

        public void Initialize(byte[] data, GFXFileFormats format)
        {
            // File is being programmatically created.
            this.FileDataType = FileDataTypes.ProgramCreated;

            // Set the file data to the source data.
            this.FileData = data;

            // Determine whethere the data was originally compressed.
            if (this.compressed = format == GFXFileFormats.S9X && GZip.IsCompressed(this.FileData))
                this.FileData = GZip.Decompress(this.FileData);

            // Load the GFX data.
            fixed (byte* ptr = this.FileData)
                LoadGFXData(ptr, this.FileData.Length, format);
        }

        /// <summary>
        /// Raises the <see cref="FileOpened"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The graphics file format is unknown or not supported.
        /// </exception>
        protected override void OnFileOpened(EventArgs e)
        {
            // File is from a path.
            this.FileDataType = FileDataTypes.FromFile;

            // Get the file fromat from the path.
            GFXFileFormats format = GFX.GetFileFormat(this.FilePath);

            // Determine whethere the data was originally compressed.
            if (this.compressed = format == GFXFileFormats.S9X && GZip.IsCompressed(this.FileData))
                this.FileData = GZip.Decompress(this.FileData);

            // Load the gfx data.
            fixed (byte* ptr = this.FileData)
                LoadGFXData(ptr, this.FileData.Length, format);

            base.OnFileOpened(e);
        }

        /// <summary>
        /// Loads the <see cref="GFX"/> data from a file source.
        /// </summary>
        /// <param name="data">
        /// A pointer to a byte array of the <see cref="GFX"/> file data.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of the data.
        /// </param>
        /// <param name="format">
        /// The <see cref="GFXFileFormats"/> value of the file data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The graphics file format is unknown or not supported.
        /// </exception>
        private void LoadGFXData(byte* data, int size, GFXFileFormats format)
        {
            // Make sure the data actually exists.
            if ((IntPtr)data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorFileData);

            // Make sure the size of the data is valid for the given format.
            int bpp = 8;
            for (; bpp > 0; bpp-- )
                if (IsValidSize((GraphicsFormats)bpp, size))
                    break;
            if (bpp == 0)
                throw new ArgumentException(Resources.ErrorInvalidFileDataSize);
            if (bpp < this.BitsPerPixel)
                this.graphicsFormat = (GraphicsFormats)bpp;

            // Set the number of tiles (the map length).
            this.MapLength = GetNumTiles(this.graphicsFormat, size);

            // Properly cast the data pointers.
            byte* dest = (byte*)this.Data.Data;

            switch (format)
            {
                case GFXFileFormats.CHR:    // YY-chr GFX
                case GFXFileFormats.BIN:

                    // Do nothing. Treat as direct translation to gfx data.
                    break;
                case GFXFileFormats.SNES:   // SNES ROM files

                    // Ignore header data if it exists.
                    data += size & ROM.LoBankSize;
                    break;

                case GFXFileFormats.ZST:    // ZSNES save state data
                    throw new NotImplementedException();

                case GFXFileFormats.S9X:    // SNES9x save state data.
                    throw new NotImplementedException();

                default:
                    throw new InvalidEnumArgumentException();
            }

            // Copy all the tile data for the given formats.
            for (int i = this.Data.Size; --i >= 0; )
                dest[i] = data[i];

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
            GFXFileFormats format = GetFileFormat(this.FilePath);

            // The new file data.
            byte[] data;

            // Determine how the file data will be initialized.
            switch (format)
            {
                case GFXFileFormats.S9X:    // Save states
                case GFXFileFormats.ZST:

                    if (File.Exists(this.FilePath))         // Get the file data from the new path
                        data = File.ReadAllBytes(this.FilePath);
                    else if (this.fileFormat == format)     // Use the source file data.
                        data = this.FileData;
                    else
                        throw new ArgumentException(Resources.ErrorSaveStateFile);
                    break;

                default:    // Create the new file data for any other format.
                    data = new byte[this.MapLength / this.BytesPerTile];
                    break;
            }

            // Decompress SNES9x save state data.
            if (format == GFXFileFormats.S9X && GZip.IsCompressed(data))
                data = GZip.Decompress(data);

            // Save the gfx data.
            fixed (byte* ptr = data)
                SaveGFXData(ptr, data.Length, format);

            // Compress SNES9x save state data.
            if (format == GFXFileFormats.S9X && this.compressed)
                data = GZip.Compress(data);

            // Set the file data.
            this.FileData = data;

            base.OnFileSaving(e);
        }

        /// <summary>
        /// Save the raw <see cref="GFX"
        /// <param name="data">
        /// A pointer to a byte array of the <see cref="GFX"/> file data.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of the data.
        /// </param>
        /// <param name="format">
        /// The <see cref="GFXFileFormats"/> value of the file data.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Size of data is not a valid size for the specified format.  -or-
        /// Unknown SNES9x version.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The file data is null.
        /// </exception>
        /// <exception cref="InvalidEnumArgumentException">
        /// The GFX file format is unknown or not supported.
        /// </exception>
        private void SaveGFXData(byte* data, int size, GFXFileFormats format)
        {
            // Make sure the data actually exists.
            if ((IntPtr)data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorFileData);

            // Make sure the size of the data is valid for the given format.
            if (!IsValidSize(this.graphicsFormat, size))
                throw new ArgumentException(Resources.ErrorInvalidFileDataSize);

            byte* src = (byte*)this.Data.Size;

            switch (format)
            {
                case GFXFileFormats.CHR:    // YY-chr GFX
                case GFXFileFormats.BIN:

                    // Do nothing. Treat as direct translation to gfx data.
                    break;
                case GFXFileFormats.SNES:   // SNES ROM files

                    // Ignore header data if it exists.
                    data += size & ROM.LoBankSize;
                    break;

                case GFXFileFormats.ZST:    // ZSNES save state data
                    throw new NotImplementedException();

                case GFXFileFormats.S9X:    // SNES9x save state data.
                    throw new NotImplementedException();

                default:
                    throw new InvalidEnumArgumentException();
            }

            for (int i = this.MapLength; --i >= 0; )
                data[i] = src[i];

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

    /// <summary>
    /// Specifies cosntants defining different file formats for
    /// <see cref="GFX"/> data.
    /// </summary>
    public enum GFXFileFormats
    {
        /// <summary>
        /// No file format was specified. This usually specifies
        /// a new file.
        /// </summary>
        None = -1,
        /// <summary>
        /// YY-CHR data. This data has no special properties or headers
        /// but is instead treated as a direct translation to raw
        /// <see cref="GFX"/> color data.
        /// </summary>
        CHR = 0,
        /// <summary>
        /// Binary data. This data has no special properties or headers
        /// but is instead treated as a direct translation to raw
        /// <see cref="GFX"/> color data.
        /// </summary>
        BIN = 1,
        /// <summary>
        /// SNES9x save state file. The GFX data is pulled from the VRAM.
        /// </summary>
        S9X = 2,
        /// <summary>
        /// ZSNES save state file. The GFX data is pulled from the VRAM.
        /// </summary>
        ZST = 3,
        /// <summary>
        /// Reads from an SNES ROM. The header is always ignored if it is found
        /// to exist. While not all of the data is <see cref="GFX"/> data,
        /// it is treated as such.
        /// </summary>
        SNES = 4
    }
}
