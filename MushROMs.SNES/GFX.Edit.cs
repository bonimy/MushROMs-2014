using System;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNES.Properties;
using MushROMs.Unmanaged;

namespace MushROMs.SNES
{
    unsafe partial class GFX
    {
        #region Methods
        /// <summary>
        /// Creates a copy of the data in <paramref name="selection"/>.
        /// </summary>
        /// <param name="selection">
        /// The <see cref="Selection"/> of the data.
        /// </param>
        /// <returns>
        /// A copy of the data at <paramref name="selection"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selection"/> is null.  -or-
        /// The palette data has not been initialized.
        /// </exception>
        public override IEditorData CreateCopy(ISelection selection)
        {
            // Make sure the gfx data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorGFXNotInitialized);

            Selection s = (Selection)selection;

            // Initialize the copy data.
            EditorData copy = new EditorData(this.graphicsFormat, s);

            // Derefernce values
            int address = selection.StartAddress;
            int bpt = LC.BytesPerTile(copy.GraphicsFormat);
            byte* tiles = this[address % bpt];
            int index0 = GetIndexFromAddress(address, copy.GraphicsFormat);

            int height = s.Height;
            int width = s.Width;
            int container = s.ContainerWidth;
            int length = this.MapLength * bpt;

            // Copy the tile data.
            for (int h = height; --h >= 0; )
            {
                int index = ((index0 + (h * container) + width) * bpt) - 1;
                for (int w = width; --w >= 0; )
                    for (int p = bpt; --p >= 0; --index)
                        if (index >= 0 && index < length)
                            copy.Pixels[h, w, p] = tiles[index];
            }

            return copy;
        }

        /// <summary>
        /// Pastes the contents of <see cref="data"/> to the active index
        /// of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selection"/> is null.  -or-
        /// The palette data has not been initialized.
        /// </exception>
        public override void Paste(IEditorData data)
        {
            // Make sure the gfx data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorGFXNotInitialized);

            // Properly cast the copy data.
            EditorData copy = (EditorData)data;

            // Initialize the pasted data to the selection.
            EditorData paste = new EditorData(copy.GraphicsFormat, new Selection(this.Selection.Min, copy.Selection.Size));

            // Copy the source data.
            fixed (byte* src = copy.Pixels)
            fixed (byte* dest = paste.Pixels)
                for (int i = copy.Pixels.Length; --i >= 0; )
                    dest[i] = src[i];

            // Write the data.
            ModifyData(paste, true, false);
        }

        /// <summary>
        /// Deletes or clears the current <see cref="Selection"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.
        /// </exception>
        public override void DeleteSelection()
        {
            // Make sure the gfx data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorGFXNotInitialized);

            // Create a copy of the current selection.
            EditorData data = (EditorData)CreateCopy();

            // Set all the tiles to the zero index pixel.
            fixed (byte* src = data.Pixels)
                for (int i = data.Pixels.Length; --i >= 0; )
                    src[i] = 0;

            // Modify the palette to show the selection has been blacked out.
            ModifyData(data, true, false);
        }

        public void RotateRight90()
        {

        }

        public void RotateLeft90()
        {

        }

        public void Rotate180()
        {

        }

        public void FlipVertical()
        {

        }

        public void FlipHorizontal()
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Create a copy of the current selection.
            EditorData data = (EditorData)CreateCopy();

            int numTiles = data.Selection.NumTiles;

            Pointer pointer = Pointer.CreatePointer(numTiles * LC.PixelsPerTile * sizeof(byte));
            fixed (byte* bpp = data.Pixels)
            {
                LC.CreatePixelMap((IntPtr)bpp, pointer.Data, numTiles, data.GraphicsFormat);
                byte* pixels = (byte*)pointer.Data;
                for (int i = numTiles; --i >= 0; )
                {
                    for (int h = LC.PlanesPerTile; --h >= 0; )
                    {
                        int index = (i * LC.PixelsPerTile) + (h * LC.PixelsPerPlane);
                        for (int w = LC.PixelsPerPlane / 2; --w >= 0; )
                        {
                            pixels[index + w] ^= pixels[index + LC.PixelsPerPlane - w - 1];
                            pixels[index + LC.PixelsPerPlane - w - 1] ^= pixels[index + w];
                            pixels[index + w] ^= pixels[index + LC.PixelsPerPlane - w - 1];
                        }
                    }
                }

                LC.CreateBPPMap(pointer.Data, (IntPtr)bpp, numTiles, data.GraphicsFormat);
            }

            ModifyData(data, true, false);
        }

        public void RotateTilesRight90()
        {

        }

        public void RotateTilesLeft90()
        {

        }

        public void RotateTiles180()
        {

        }

        public void FlipTilesVertical()
        {

        }

        public void FlipTilesHorizontal()
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Create a copy of the current selection.
            EditorData data = (EditorData)CreateCopy();

            int numTiles = data.Selection.NumTiles;

            Pointer pointer = Pointer.CreatePointer(numTiles * LC.PixelsPerTile * sizeof(byte));
            fixed (byte* bpp = data.Pixels)
            {
                LC.CreatePixelMap((IntPtr)bpp, pointer.Data, numTiles, data.GraphicsFormat);
                byte* pixels = (byte*)pointer.Data;
                for (int i = numTiles; --i >= 0; )
                {
                    for (int h = LC.PlanesPerTile; --h >= 0; )
                    {
                        int index = (i * LC.PixelsPerTile) + (h * LC.PixelsPerPlane);
                        for (int w = LC.PixelsPerPlane / 2; --w >= 0; )
                        {
                            pixels[index + w] ^= pixels[index + LC.PixelsPerPlane - w - 1];
                            pixels[index + LC.PixelsPerPlane - w - 1] ^= pixels[index + w];
                            pixels[index + w] ^= pixels[index + LC.PixelsPerPlane - w - 1];
                        }
                    }
                }

                LC.CreateBPPMap(pointer.Data, (IntPtr)bpp, numTiles, data.GraphicsFormat);
            }

            ModifyData(data, true, false);
        }

        /// <summary>
        /// Writes data from an <see cref="IEditorData"/> interface.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write.
        /// </param>
        /// <param name="startAddress">
        /// The starting address in the editor to get the data from.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.  -or-
        /// <paramref name="min"/> is null.  -or-
        /// The gfx data has not been initialized.
        /// </exception>
        protected override void WriteCopyData(IEditorData data, int startAddress)
        {
            // Make sure the gfx data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorGFXNotInitialized);

            // Initialize the copy data.
            EditorData copy = (EditorData)data;

            // Derefernce values
            int bpt = LC.BytesPerTile(copy.GraphicsFormat);
            byte* tiles = this[startAddress % bpt];
            int index0 = GetIndexFromAddress(startAddress, copy.GraphicsFormat);

            int height = data.Selection.Height;
            int width = data.Selection.Width;
            int container = data.Selection.ContainerWidth;
            int length = this.MapLength * bpt;

            // Copy the tile data.
            for (int h = height; --h >= 0; )
            {
                int index = ((index0 + (h * container) + width) * bpt) - 1;
                for (int w = width; --w >= 0; )
                    for (int p = bpt; --p >= 0; --index)
                        if (index >= 0 && index < length)
                            tiles[index] = copy.Pixels[h, w, p];
            }
        }
        #endregion

        /// <summary>
        /// Contains <see cref="GFX"/> data from a <see cref="Selection"/>.
        /// </summary>
        protected class EditorData : IEditorData
        {
            /// <summary>
            /// An array of BPP pixel data within the <see cref="Selection"/>.
            /// </summary>
            private byte[,,] pixels;
            /// <summary>
            /// The <see cref="GraphicsFormats"/> value of the pixel data.
            /// </summary>
            private GraphicsFormats format;
            /// <summary>
            /// The <see cref="Selection"/> of the <see cref="GFX"/> data.
            /// </summary>
            private ISelection selection;

            /// <summary>
            /// Gets an array of BPP pixel data within the
            /// <see cref="Selection"/>.
            /// </summary>
            public byte[,,] Pixels
            {
                get { return this.pixels; }
            }

            /// <summary>
            /// Gets the <see cref="GraphicsFormats"/> value of the pixel data.
            /// </summary>
            public GraphicsFormats GraphicsFormat
            {
                get { return this.format; }
            }

            /// <summary>
            /// Gets the <see cref="Selection"/> of the
            /// <see cref="GFX"/> data.
            /// </summary>
            public ISelection Selection
            {
                get { return this.selection; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="GFXData"/> class.
            /// </summary>
            /// <param name="format">
            /// The <see cref="GraphicsFormats"/> value of the pixel data.
            /// </param>
            /// <param name="selection">
            /// The <see cref="Selection"/> of the data.
            /// </param>
            protected internal EditorData(GraphicsFormats format, ISelection selection)
            {
                this.pixels = new byte[selection.Height, selection.Width, LC.BytesPerTile(format)];
                this.format = format;
                this.selection = selection.Copy();
            }
        }
    }
}