/* The GFX class derives from the Editor class and
 * serves as the primary source for editing SNES tile
 * graphics. The data format follows a 'Bits Per Pixel'
 * struture where the number of bits determines the
 * maximum color index size of each pixel. For example,
 * 4BPP graphics allows 16 (4 bit size) colors for each
 * pixel.
 * 
 *      1 1 1 1 2 2 2 2 - 3 3 3 3 4 4 4 4 (first four pixels)
 *      
 * The most complex part of this Editor is the ease with
 * which the user should be able to switch formats.
 * 
 * As a final note, all of the graphics formats derive
 * from the Lunar Compress DLL. The DLL also converts
 * the 'Bits Per Pixel' graphics to an indexed bitmap,
 * which is much easier to handle.
 * */

using System;
using MushROMs.Editors;
using MushROMs.LunarCompress;

namespace MushROMs.SNES
{
    /// <summary>
    /// Provides data for editing SNES graphics data.
    /// </summary>
    public unsafe partial class GFX : Editor
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback graphics format of the <see cref="GFX"/> data.
        /// This field is constant.
        /// </summary>
        private const GraphicsFormats FallbackGraphicsFormat = GraphicsFormats.SNES_4BPP;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="GraphicsFormat"/> changes.
        /// </summary>
        public event EventHandler GraphicsFormatChanged;
        #endregion

        #region Fields
        /// <summary>
        /// The <see cref="GraphicsFormats"/> value of the <see cref="GFX"/>.
        /// </summary>
        private GraphicsFormats graphicsFormat;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Byte"/>* array at a specified byte address.
        /// </summary>
        /// <param name="address">
        /// The address in the current <see cref="GFX"/>.
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
        public byte* this[int address]
        {
            get { return (byte*)this.Data.Data + address; }
        }

        /// <summary>
        /// Gets the number of tiles that are actually visible in the view
        /// region.
        /// </summary>
        public override int NumVisibleTiles
        {
            get { return Math.Min((this.Data.Size - this.Zero.Address) / this.BytesPerTile, this.NumViewTiles); }
        }

        /// <summary>
        /// Gets or sets the <see cref="GraphicsFormats"/> value of the
        /// <see cref="GFX"/>.
        /// </summary>
        public GraphicsFormats GraphicsFormat
        {
            get { return this.graphicsFormat; }
            set { this.graphicsFormat = value; OnGraphicsFormatChanged(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets the bits per pixel of the <see cref="GFX"/>.
        /// </summary>
        public int BitsPerPixel
        {
            get { return LC.BitsPerPixel(this.graphicsFormat); }
        }

        /// <summary>
        /// Gets the bytes per plane of the <see cref="GFX"/>.
        /// </summary>
        public int BytesPerPlane
        {
            get { return LC.BytesPerPlane(this.graphicsFormat); }
        }

        /// <summary>
        /// Gets the bytes per tile of the <see cref="GFX"/>.
        /// </summary>
        public int BytesPerTile
        {
            get { return LC.BytesPerTile(this.graphicsFormat); }
        }

        /// <summary>
        /// Gets the max colors per pixel of the <see cref="GFX"/>.
        /// </summary>
        public int ColorsPerPixel
        {
            get { return LC.ColorsPerPixel(this.graphicsFormat); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instant of the <see cref="GFX"/> class.
        /// </summary>
        public GFX()
        {
            this.Zero.IndexArgs = new object[] { (int)0 };

            // Set the initial graphics format of the gfx.
            this.graphicsFormat = FallbackGraphicsFormat;

            this.arrangeFormat = ArrangeFormats.Horizontal;
            SetADF();
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
            return GetAddressFromIndex(index, (int)args[0], this.graphicsFormat);
        }

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
        /// <param name="graphicsFormat">
        /// The <see cref="GraphicsFormats"/> value of the data.
        /// </param>
        /// <returns>
        /// The byte address of the element.
        /// </returns>
        public int GetAddressFromIndex(int index, int context, GraphicsFormats graphicsFormat)
        {
            int bpt = LC.BytesPerTile(graphicsFormat);
            return (index * bpt) + (context % bpt);
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
            return GetIndexFromAddress(address, this.graphicsFormat);
        }

        /// <summary>
        /// Gets the specified index of an element from the absolute byte
        /// address.
        /// </summary>
        /// <param name="address">
        /// The byte address of the element to get the index of.
        /// </param>
        /// <param name="graphicsFormat">
        /// The <see cref="GraphicsFormats"/> value of the data.
        /// </param>
        /// <returns>
        /// The index of the element.
        /// </returns>
        public int GetIndexFromAddress(int address, GraphicsFormats graphicsFormat)
        {
            return address / LC.BytesPerTile(graphicsFormat);
        }

        /// <summary>
        /// Raises the <see cref="GraphicsFormatChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnGraphicsFormatChanged(EventArgs e)
        {
            if (this.Data.Size > 0)
                base.SetMapLength(this.Data.Size / BytesPerTile);

            if (GraphicsFormatChanged != null)
                GraphicsFormatChanged(this, e);
        }
        #endregion
    }
}