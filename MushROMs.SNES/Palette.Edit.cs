using System;
using System.ComponentModel;
using System.Drawing;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    unsafe partial class Palette
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
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Initialize the copy data.
            EditorData copy = new EditorData(selection);

            // Derefernce values
            int address = selection.StartAddress;
            int index0 = GetIndexFromAddress(address, null);
            ushort* colors = this[address % SNESColorSize];

            int height = selection.Height;
            int width = selection.Width;
            int container = selection.ContainerWidth;
            int length = this.MapLength;

            // Write the color data.
            for (int h = height; --h >= 0; )
            {
                int index = index0 + (h * container) + width - 1;
                for (int w = width; --w >= 0; --index)
                    if (index >= 0 && index < length)
                        copy.Colors[h, w] = colors[index];
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
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Properly cast the copy data.
            EditorData copy = (EditorData)data;

            // Initialize the pasted data to the selection.
            EditorData paste = new EditorData(new Selection(this.Selection.First, copy.Selection.Size));

            // Copy the source data.
            fixed (ushort* src = copy.Colors)
            fixed (ushort* dest = paste.Colors)
                for (int i = copy.Colors.Length; --i >= 0; )
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
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Create a copy of the current selection.
            EditorData data = (EditorData)CreateCopy();

            // Set all the colors to black within the selection.
            fixed (ushort* src = data.Colors)
                for (int i = data.Colors.Length; --i >= 0; )
                    src[i] = 0;

            // Modify the palette to show the selection has been blacked out.
            ModifyData(data, true, false);
        }

        /// <summary>
        /// Edit a single color at a specified address.
        /// </summary>
        /// <param name="address">
        /// The byte address of the color to edit.
        /// </param>
        /// <param name="color">
        /// The new 15-bit RGB color value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// The address was outside the range of the data.
        /// </exception>
        public void EditPaletteColor(int address, ushort color)
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Get the index point of the address.
            IndexPoint ip = new IndexPoint(this.Zero);
            ip.Index = GetIndexFromAddress(address, null);

            // Don't go out of the memory range.
            if (ip.Index < 0 || ip.Index >= this.MapLength)
                throw new IndexOutOfRangeException(Resources.ErrorAddressOutOfRange);

            // Create the copy data for the new color.
            EditorData data = new EditorData(new Selection(ip));
            data.Colors[0, 0] = color;

            // Modify the palette to show the new color.
            ModifyData(data, true, false);
        }

        /// <summary>
        /// Inverts the colors within the current selection.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.
        /// </exception>
        public void Invert()
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Create a copy of the current selection.
            EditorData data = (EditorData)CreateCopy();

            // Invert all the colors within the selection.
            fixed (ushort* src = data.Colors)
                for (int i = data.Colors.Length; --i >= 0; )
                    src[i] ^= 0x7FFF;

            // Modify the palette to show the selection with the inverted colors.
            ModifyData(data, true, false);
        }

        /// <summary>
        /// Colorizes the current <see cref="Selection"/> of the
        /// <see cref="Palette"/> with color data from a given source.
        /// </summary>
        /// <param name="data">
        /// The source <see cref="Palette"/> data to colorize.
        /// </param>
        /// <param name="colorize">
        /// If true, <paramref name="data"/> will be colorized. If false,
        /// <paramref name="data"/> will instead have it HSL properties
        /// modified.
        /// </param>
        /// <param name="hue">
        /// The Hue change of the color data.
        /// </param>
        /// <param name="sat">
        /// The Saturation change of the color data.
        /// </param>
        /// <param name="lum">
        /// The Luminosity change of the color data.
        /// </param>
        /// <param name="eff">
        /// The Effective value of the colorization.
        /// </param>
        /// <param name="preview">
        /// If true, this colorizing edit will not be added to the
        /// undo/redo history.
        /// </param>
        /// <exception cref="ArgumentException">
        /// hue, saturation, luminosity, or effectivness is outside the
        /// valid range.  -or-
        /// The two selections do not match in size.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.  -or-
        /// The editor data is null.
        /// </exception>
        public void Colorize(IEditorData data, bool colorize, int hue, int sat, int lum, int eff, bool preview)
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Properly cast the source data.
            EditorData source = (EditorData)data;

            // Create a copy of the current selection.
            EditorData copy = new EditorData(data.Selection);

            // Make sure the selections have the same size.
            if (data.Selection.NumTiles != copy.Selection.NumTiles)
                throw new ArgumentException(Resources.ErrorSelectionsMismatch);

            // Colorize or modify all the colors.
            fixed (ushort* src = source.Colors)
            fixed (ushort* dest = copy.Colors)
            {
                for (int i = source.Colors.Length; --i >= 0; )
                {
                    ExpandedColor x = LC.SNESToSystemColor(src[i]);
                    if (colorize)
                        dest[i] = LC.SystemToSNESColor((Color)x.Colorize(hue, sat, lum, eff));
                    else
                        dest[i] = LC.SystemToSNESColor((Color)x.Modify(hue, sat, lum));
                }
            }

            // Modify the palette to show the selection has been colorized.
            ModifyData(copy, !preview, false);
        }

        /// <summary>
        /// Creates a grayscale of <see cref="Palette"/> colors in the
        /// current <see cref="Selection"/> from a given source.
        /// </summary>
        /// <param name="data">
        /// The source <see cref="Palette"/> data to grayscale.
        /// </param>
        /// <param name="red">
        /// The component Red weight of the grayscale.
        /// </param>
        /// <param name="green">
        /// The component Green weight of the grayscale.
        /// </param>
        /// <param name="blue">
        /// The component Blue weight of the grayscale.
        /// </param>
        /// <param name="preview">
        /// If true, this grayscale edit will not be added to the
        /// undo/redo history.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.  -or-
        /// The editor data is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// At least one weighted value is less than zero.
        /// </exception>
        public void Grayscale(IEditorData data, int red, int green, int blue, bool preview)
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Properly cast the source data.
            EditorData source = (EditorData)data;

            // Create a copy of the current selection.
            EditorData copy = new EditorData(data.Selection);

            // Make sure the selections have the same size.
            if (data.Selection.NumTiles != copy.Selection.NumTiles)
                throw new ArgumentException(Resources.ErrorSelectionsMismatch);

            // Grayscale the colors.
            fixed (ushort* src = source.Colors)
            fixed (ushort* dest = copy.Colors)
            {
                for (int i = source.Colors.Length; --i >= 0; )
                {
                    ExpandedColor x = LC.SNESToSystemColor(src[i]);
                    dest[i] = LC.SystemToSNESColor((Color)x.GrayScale(red, green, blue));
                }
            }

            // Modify the palette to show the selection has been grayscaled.
            ModifyData(copy, !preview, false);
        }

        /// <summary>
        /// Creates a blended mask between <see cref="Palette"/> data from
        /// a given source and masking color.
        /// </summary>
        /// <param name="data">
        /// The source <see cref="Palette"/> data to blend.
        /// </param>
        /// <param name="color">
        /// The masking color to blend the <see cref="Palette"/> data with.
        /// </param>
        /// <param name="mode">
        /// The <see cref="BlendModes"/> value to use.
        /// </param>
        /// <param name="preview">
        /// If true, this blending edit will not be added to the
        /// undo/redo history.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.  -or-
        /// The editor data is null.
        /// </exception>
        public void Blend(IEditorData data, ExpandedColor color, BlendModes mode, bool preview)
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Properly cast the source data.
            EditorData source = (EditorData)data;

            // Create a copy of the current selection.
            EditorData copy = new EditorData(data.Selection);

            // Make sure the selections have the same size.
            if (data.Selection.NumTiles != copy.Selection.NumTiles)
                throw new ArgumentException(Resources.ErrorSelectionsMismatch);

            // Blend the colors.
            fixed (ushort* src = source.Colors)
            fixed (ushort* dest = copy.Colors)
            {
                for (int i = source.Colors.Length; --i >= 0; )
                {
                    ExpandedColor xColor = LC.SNESToSystemColor(src[i]);
                    dest[i] = LC.SystemToSNESColor((Color)xColor.Blend(color, mode));
                }
            }

            // Modify the palette to show the selection has been blended.
            ModifyData(copy, !preview, false);
        }

        /// <summary>
        /// Creates a gradient of the colors within the current
        /// <see cref="Selection"/>.
        /// </summary>
        /// <param name="style">
        /// The <see cref="GradientStyles"/> value of the gradient.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The palette data has not been initialized.
        /// </exception>
        public void Gradient(GradientStyles style)
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Create a copy of the current selection.
            EditorData copy = (EditorData)CreateCopy();

            // The last index of either the row or column.
            int last;

            // Create the gradient.
            switch (style)
            {
                case GradientStyles.Horizontal:

                    // The last index of the row.
                    last = copy.Selection.Width - 1;

                    // Create the gradient across evere row.
                    for (int h = copy.Selection.Height; --h >= 0; )
                    {
                        // Get the first color of the row.
                        Color c1 = LC.SNESToSystemColor(copy.Colors[h, 0]);

                        // Get the last color of the row.
                        Color c2 = LC.SNESToSystemColor(copy.Colors[h, last]);

                        // Create a linear gradient across all the colors inbetween.
                        for (int w = last; --w >= 1;)
                            copy.Colors[h, w] = LC.PCtoSNESRGB(
                                (uint)((((c1.R * (last - w)) + (c2.R * w)) / last) << 0x10 |
                                       (((c1.G * (last - w)) + (c2.G * w)) / last) << 8 |
                                       (((c1.B * (last - w)) + (c2.B * w)) / last)));
                    }
                    break;
                case GradientStyles.Vertical:

                    // The last index of the column.
                    last = copy.Selection.Height - 1;

                    // Create the gradient across every column.
                    for (int w = copy.Selection.Width; --w >= 0; )
                    {
                        // Get the first color of the column.
                        Color c1 = LC.SNESToSystemColor(copy.Colors[0, w]);

                        // Get the last color of the column.
                        Color c2 = LC.SNESToSystemColor(copy.Colors[last, w]);

                        // Create a linear gradient across all the colors inbetween.
                        for (int h = last; --h >= 1;)
                            copy.Colors[h, w] = LC.PCtoSNESRGB(
                                (uint)((((c1.R * (last - h)) + (c2.R * h)) / last) << 0x10 |
                                       (((c1.G * (last - h)) + (c2.G * h)) / last) << 8 |
                                       (((c1.B * (last - h)) + (c2.B * h)) / last)));
                    }
                    break;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorGradientStyle);
            }

            // Modify the palette to show the selection has had the gradient applied.
            ModifyData(copy, true, false);
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
        /// The palette data has not been initialized.
        /// </exception>
        protected override void WriteCopyData(IEditorData data, int startAddress)
        {
            // Make sure the palette data actually exists.
            if (this.Data.Data == IntPtr.Zero)
                throw new ArgumentNullException(Resources.ErrorPaleteNotInitialized);

            // Properly cast the edit data.
            EditorData copy = (EditorData)data;

            // Derefernce values
            ushort* colors = this[startAddress % SNESColorSize];
            int index0 = GetIndexFromAddress(startAddress, null);

            int height = data.Selection.Height;
            int width = data.Selection.Width;
            int container = data.Selection.ContainerWidth;
            int length = this.MapLength;

            // Write the color data.
            for (int h = height; --h >= 0; )
            {
                int index = GetIndexFromAddress(startAddress, null) + (h * container) + width - 1;
                for (int w = width; --w >= 0; --index)
                    if (index >= 0 && index < length)
                        colors[index] = copy.Colors[h, w];
            }
        }
        #endregion

        /// <summary>
        /// Contains <see cref="Palette"/> data from a <see cref="Selection"/>.
        /// </summary>
        protected class EditorData : IEditorData
        {
            /// <summary>
            /// An array of the color data within the <see cref="Selection"/>.
            /// </summary>
            private ushort[,] colors;
            /// <summary>
            /// The <see cref="Selection"/> of the <see cref="Palette"/> data.
            /// </summary>
            private ISelection selection;

            /// <summary>
            /// Gets an array of the color data within the
            /// <see cref="Selection"/>.
            /// </summary>
            public ushort[,] Colors
            {
                get { return this.colors; }
            }

            /// <summary>
            /// Gets the <see cref="Selection"/> of the
            /// <see cref="Palette"/> data.
            /// </summary>
            public ISelection Selection
            {
                get { return this.selection; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="EditorData"/> class.
            /// </summary>
            /// <param name="selection">
            /// The <see cref="Selection"/> of the data.
            /// </param>
            protected internal EditorData(ISelection selection)
            {
                this.colors = new ushort[selection.Height, selection.Width];
                this.selection = selection.Copy();
            }
        }
    }

    /// <summary>
    /// Specifies constant defining different gradient styles.
    /// </summary>
    public enum GradientStyles
    {
        /// <summary>
        /// Create a linear horizontal gradient.
        /// </summary>
        Horizontal,
        /// <summary>
        /// Create a linear vertical gradient.
        /// </summary>
        Vertical
    }
}