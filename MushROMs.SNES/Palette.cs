/* The Palette class derives from the Editor class and
 * serves as the primary source for editing SNES color
 * data whose format is as follows:
 * 
 *      ? r r r r r g g - g g g b b b b b (16-bits)
 * 
 * The most significant bit is always ignored. However.
 * it can serve as a handy way to use alpha transparency,
 * but there aren't many practical purposes for that.
 * */

using System;
using System.Drawing;
using MushROMs.Editors;

namespace MushROMs.SNES
{
    /// <summary>
    /// Provides data for drawing editing 15-bit SNES color data.
    /// </summary>
    public unsafe partial class Palette : Editor
    {
        #region Constant and read-only fields
        /// <summary>
        /// The default number of colors in a single row of SNES
        /// <see cref="Palette"/> data.
        /// This field is constant.
        /// </summary>
        public const int SNESPaletteWidth = 0x10;
        /// <summary>
        /// The default number of rows in the <see cref="Palette"/> data.
        /// This field is constant.
        /// </summary>
        public const int SNESPaletteRows = 0x10;
        /// <summary>
        /// The default size of the <see cref="Palette"/> data.
        /// This field is constant.
        /// </summary>
        public const int SNESPaletteSize = SNESPaletteRows * SNESPaletteWidth;

        /// <summary>
        /// Gets the size, in bytes, of a <see cref="Palette"/> color in its standard format.
        /// </summary>
        private const int SNESColorSize = sizeof(ushort);
        /// <summary>
        /// Gets the size, in bytes, of a PAL color size.
        /// </summary>
        private const int PALColorSize = 3;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when <see cref="BackColor"/> changes.
        /// </summary>
        public event EventHandler BackColorChanged;
        #endregion

        #region Fields
        /// <summary>
        /// The back color of the <see cref="Palette"/> data.
        /// </summary>
        /// <remarks>
        /// The back color is only relevant in .MW3 files.
        /// </remarks>
        private ushort backColor;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="UInt16"/>* array at a specified byte address.
        /// </summary>
        /// <param name="address">
        /// The address in the current <see cref="Palette"/>.
        /// </param>
        /// <returns>
        /// A <see cref="UInt16"/>* array.
        /// </returns>
        /// <remarks>
        /// In the interest of unsafe code, no <see cref="IndexOutOfRangeException"/>
        /// is thrown if <paramref name="address"/> is outside the bounds
        /// of the <see cref="Data"/> array. Therefore, it is up to the
        /// user to carefully monitor improper accesses to unsafe memory
        /// otherwise it will lead to undefined behavior.
        /// </remarks>
        public ushort* this[int address]
        {
            get { return (ushort*)((byte*)this.Data.Data + address); }
        }

        /// <summary>
        /// Gets the number of tiles that are actually visible in the view
        /// region.
        /// </summary>
        public override int NumVisibleTiles
        {
            get { return Math.Min((this.Data.Size - this.Zero.Address) / SNESColorSize, this.NumViewTiles); }
        }

        /// <summary>
        /// gets or sets the back color of the <see cref="Palette"/> data.
        /// </summary>
        /// <remarks>
        /// The back color is only relevant in .MW3 files.
        /// </remarks>
        public ushort BackColor
        {
            get { return this.backColor; }
            set { this.backColor = value; OnBackColorChanged(EventArgs.Empty); }
            }
        #endregion

        #region Constructors()
        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class.
        /// </summary>
        public Palette()
        {
            this.Zero.IndexArgs = new object[] { (int)0 };

            this.ViewSize = new Size(SNESPaletteWidth, SNESPaletteRows);
            this.TileSize = new Size(1, 1);
            this.ZoomSize = new Size(0x10, 0x10);

            this.FileDataType = FileDataTypes.ProgramCreated;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the absolute byte address of an element at the specified
        /// index.
        /// </summary>
        /// <param name="index">
        /// The index of the element to get the address of.
        /// </param>
        /// <param name="context">
        /// A contextual value for the conversion.
        /// </param>
        /// <returns>
        /// The byte address of the element.
        /// </returns>
        public override int GetAddressFromIndex(int index, object[] args)
        {
            return (index * SNESColorSize) + ((int)args[0] % SNESColorSize);
        }

        /// <summary>
        /// Gets the specified index of an element from the absolute byte
        /// address.
        /// </summary>
        /// <param name="address">
        /// The byte address of the element to get the index of.
        /// </param>
        /// <returns>
        /// The index of the element.
        /// </returns>
        public override int GetIndexFromAddress(int address, object[] args)
        {
            return address / SNESColorSize;
        }

        /// <summary>
        /// Raises the <see cref="BackColorChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnBackColorChanged(EventArgs e)
        {
            if (BackColorChanged != null)
                BackColorChanged(this, e);
        }
        #endregion
    }
}