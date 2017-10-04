using System;
using System.Drawing;

namespace MushROMs.Editors
{
    /// <summary>
    /// Represents a reference point in an <see cref="Editor"/>.
    /// </summary>
    public sealed class ZeroPoint
    {
        #region Fields
        /// <summary>
        /// The <see cref="Editor"/> that contains this <see cref="ZeroPoint"/>.
        /// </summary>
        private readonly Editor editor;

        /// <summary>
        /// The byte address.
        /// </summary>
        private int address;

        /// <summary>
        /// An array of parameter arguments when converting index to address.
        /// </summary>
        private object[] indexArgs;
        /// <summary>
        /// An array of parameter arguments when converting address to index.
        /// </summary>
        private object[] addressArgs;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Editor"/> that contains this <see cref="ZeroPoint"/>.
        /// </summary>
        internal Editor Editor
        {
            get { return this.editor; }
        }

        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        public int X
        {
            get { return GetXCoordinate(this.Index); }
            set { this.Index = GetIndex(value, this.Y); }
        }
        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        public int Y
        {
            get { return GetYCoordinate(this.Index); }
            set { this.Index = GetIndex(this.X, value); }
        }
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public Point Point
        {
            get { return GetCoordinates(this.Index); }
            set { this.Index = GetIndex(value.X, value.Y); }
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index
        {
            get { return this.editor.GetIndexFromAddress(this.Address, this.AddressArgs); }
            set { this.Address = this.editor.GetAddressFromIndex(value, this.IndexArgs); }
        }

        /// <summary>
        /// Gets or sets the byte address.
        /// </summary>
        public int Address
        {
            get { return this.address; }
            set { this.address = value; OnAddressChanged(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets or sets an array of parameter arguments when converting index to address.
        /// </summary>
        public object[] IndexArgs
        {
            get { return this.indexArgs; }
            set { this.indexArgs = value; }
        }
        /// <summary>
        /// Gets or sets an array of parameter arguments when converting index to address.
        /// </summary>
        public object[] AddressArgs
        {
            get { return this.addressArgs; }
            set { this.addressArgs = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroPoint"/> class.
        /// </summary>
        /// <param name="editor">
        /// The <see cref="Editor"/> that contains the
        /// <see cref="ZeroPoint"/>
        /// </param>
        public ZeroPoint(Editor editor)
        {
            this.editor = editor;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the absolute coordinates of a point given an index.
        /// </summary>
        /// <param name="index">
        /// The index of the point.
        /// </param>
        /// <returns>
        /// A <see cref="Point"/> representing the coordinates of
        /// <paramref name="index"/>.
        /// </returns>
        public Point GetCoordinates(int index)
        {
            return new Point(GetXCoordinate(index), GetYCoordinate(index));
        }

        /// <summary>
        /// Gets the absolute x-coordinate of a point given an index.
        /// </summary>
        /// <param name="index">
        /// The index of the point.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> representing the x-coordinate of
        /// <paramref name="index"/>.
        /// </returns>
        public int GetXCoordinate(int index)
        {
            return index % this.editor.MapWidth;
        }

        /// <summary>
        /// Gets the absolute y-coordinate of a point given an index.
        /// </summary>
        /// <param name="index">
        /// The index of the point.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> representing the y-coordinate of
        /// <paramref name="index"/>.
        /// </returns>
        public int GetYCoordinate(int index)
        {
            return index / this.editor.MapWidth;
        }

        /// <summary>
        /// Gets the index given the absolute coordinates of a point.
        /// </summary>
        /// <param name="point">
        /// The absolute coordinates of the point.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> representing the index of <paramref name="point"/>.
        /// </returns>
        public int GetIndex(Point point)
        {
            return GetIndex(point.X, point.Y);
        }

        /// <summary>
        /// Gets the index given the absolute coordinates of a point.
        /// </summary>
        /// <param name="x">
        /// The absolute x-coordinate of the point.
        /// </param>
        /// <param name="y">
        /// The absolute y-coordinate of the point.
        /// </param>
        /// <returns>
        /// An <see cref="Int32"/> representing the index of a <see cref="Point"/>
        /// with values <paramref name="x"/> and <paramref name="y"/>.
        /// </returns>
        public int GetIndex(int x, int y)
        {
            return (y * this.editor.MapWidth) + x;
        }

        /// <summary>
        /// Raises the <see cref="AddressChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        private void OnAddressChanged(EventArgs e)
        {
            if (AddressChanged != null)
                AddressChanged(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the address, index, or coordinates of this
        /// <see cref="ZeroPoint"/> changes.
        /// </summary>
        public event EventHandler AddressChanged;
        #endregion
    }
}