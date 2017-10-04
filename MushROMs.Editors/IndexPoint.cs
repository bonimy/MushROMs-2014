using System;
using System.Drawing;

namespace MushROMs.Editors
{
    /// <summary>
    /// Represents a point in two-dimensional space that has a unique
    /// index value.
    /// </summary>
    public sealed class IndexPoint
    {
        #region Fields
        /// <summary>
        /// The <see cref="ZeroPoint"/> of this <see cref="IndexPoint"/>.
        /// </summary>
        private readonly ZeroPoint zero;

        /// <summary>
        /// The address of the <see cref="IndexPoint"/>.
        /// </summary>
        private int address;
        /// <summary>
        /// The absolute x-coordinate.
        /// </summary>
        private int absoluteX;
        /// <summary>
        /// The absolute y-coordinate.
        /// </summary>
        private int absoluteY;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="ZeroPoint"/> of this <see cref="IndexPoint"/>.
        /// </summary>
        internal ZeroPoint Zero
        {
            get { return this.zero; }
        }

        /// <summary>
        /// Gets or sets the absolute x-coordinate.
        /// </summary>
        public int AbsoluteX
        {
            get { return this.absoluteX; }
            set { SetIndexPoint(value, this.AbsoluteY); }
        }
        /// <summary>
        /// Gets or sets the absolute y-coordinate.
        /// </summary>
        public int AbsoluteY
        {
            get { return this.absoluteY; }
            set { SetIndexPoint(this.AbsoluteX, value); }
        }
        /// <summary>
        /// Gets or sets the absolute coordinates.
        /// </summary>
        public Point AbsolutePoint
        {
            get { return new Point(this.AbsoluteX, this.AbsoluteY); }
            set { SetIndexPoint(value.X, value.Y); }
        }

        /// <summary>
        /// Gets or sets the relative x-coordinate.
        /// </summary>
        public int RelativeX
        {
            get { return this.AbsoluteX - this.Zero.X; }
            set { SetIndexPoint(value + this.Zero.X, this.AbsoluteY); }
        }
        /// <summary>
        /// Gets or sets the relative y-coordinate.
        /// </summary>
        public int RelativeY
        {
            get { return this.AbsoluteY - this.Zero.Y; }
            set { SetIndexPoint(this.AbsoluteX, value + this.Zero.Y); }
        }
        /// <summary>
        /// Gets or sets the relative coordinates.
        /// </summary>
        public Point RelativePoint
        {
            get { return new Point(this.RelativeX, this.RelativeY); }
            set { SetIndexPoint(value.X + this.Zero.X, value.Y + this.Zero.Y); }
        }

        /// <summary>
        /// Gets or sets the absolute index.
        /// </summary>
        public int Index
        {
            get { return this.Zero.GetIndex(this.AbsoluteX, this.AbsoluteY); }
            set { SetIndexPoint(this.Zero.GetXCoordinate(value), this.Zero.GetYCoordinate(value)); }
        }

        /// <summary>
        /// Gets the absolute byte address.
        /// </summary>
        public int Address
        {
            get { return this.address; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexPoint"/> class.
        /// </summary>
        /// <param name="zero">
        /// The <see cref="ZeroPoint"/> of the <see cref="IndexPoint"/>.
        /// </param>
        public IndexPoint(ZeroPoint zero)
        {
            this.zero = zero;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a copy of this <see cref="IndexPoint"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IndexPoint"/> that is representationally the
        /// same as this <see cref="IndexPoint"/>.
        /// </returns>
        public IndexPoint Copy()
        {
            IndexPoint ip = new IndexPoint(this.zero);
            ip.address = this.address;
            ip.absoluteX = this.absoluteX;
            ip.absoluteY = this.absoluteY;
            return ip;
        }

        /// <summary>
        /// Sets the absolute coordinates of the <see cref="IndexPoint"/>.
        /// </summary>
        /// <param name="absoluteX">
        /// The value of the x-coordinate.
        /// </param>
        /// <param name="absoluteY">
        /// The value of the y-coordinate.
        /// </param>
        private void SetIndexPoint(int absoluteX, int absoluteY)
        {
            this.absoluteX = absoluteX;
            this.absoluteY = absoluteY;
            OnIndexChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="IndexChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        private void OnIndexChanged(EventArgs e)
        {
            this.address = this.Zero.Editor.GetAddressFromIndex(this.Index, this.zero.IndexArgs);
            if (IndexChanged != null)
                IndexChanged(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when <see cref="Index"/> changes.
        /// </summary>
        public event EventHandler IndexChanged;
        #endregion
    }
}