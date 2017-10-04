using System;
using System.Drawing;

namespace MushROMs.Editors
{
    /// <summary>
    /// Represents a selection in an <see cref="Editor"/>.
    /// </summary>
    public sealed class Selection : ISelection
    {
        #region Fields
        /// <summary>
        /// The <see cref="IndexPoint"/> of the first selected tile.
        /// </summary>
        private IndexPoint first;
        /// <summary>
        /// The <see cref="IndexPoint"/> of the last selected tile.
        /// </summary>
        private IndexPoint last;

        /// <summary>
        /// The <see cref="IndexPoint"/> of the tile with the
        /// lesser-valued coordinates.
        /// </summary>
        private IndexPoint min;
        /// <summary>
        /// The <see cref="IndexPoint"/> of the tile with the
        /// greater-valued coordinates.
        /// </summary>
        private IndexPoint max;

        /// <summary>
        /// The width of the view region when the <see cref="Selection"/>
        /// was made.
        /// </summary>
        private int containerWidth;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="IndexPoint"/> of the first selected tile.
        /// </summary>
        public IndexPoint First
        {
            get { return this.first; }
        }

        /// <summary>
        /// Gets the <see cref="IndexPoint"/> of the last selected tile.
        /// </summary>
        public IndexPoint Last
        {
            get { return this.last; }
        }

        /// <summary>
        /// Gets the <see cref="IndexPoint"/> of the tile with the
        /// lesser-valued coordinates.
        /// </summary>
        public IndexPoint Min
        {
            get { return this.min; }
        }

        /// <summary>
        /// Gets the <see cref="IndexPoint"/> of the tile with the
        /// greater-valued coordinates.
        /// </summary>
        public IndexPoint Max
        {
            get { return this.max; }
        }

        /// <summary>
        /// Gets the width of the <see cref="Selection"/>.
        /// </summary>
        public int Width
        {
            get { return this.Max.RelativeX - this.Min.RelativeX + 1; }
        }

        /// <summary>
        /// Gets the height of the <see cref="Selection"/>.
        /// </summary>
        public int Height
        {
            get { return this.Max.RelativeY - this.Min.RelativeY + 1; }
        }

        /// <summary>
        /// Gets the <see cref="Size"/> of the <see cref="Selection"/>.
        /// </summary>
        public Size Size
        {
            get { return new Size(this.Width, this.Height); }
        }

        /// <summary>
        /// Gets the number of tiles of the <see cref="Selection"/>.
        /// </summary>
        public int NumTiles
        {
            get { return Width * Height; }
        }

        /// <summary>
        /// Gets the width of the view region when the
        /// <see cref="Selection"/> was made.
        /// </summary>
        public int ContainerWidth
        {
            get { return this.containerWidth; }
        }

        /// <summary>
        /// Gets the starting address of the <see cref="Selection"/>.
        /// </summary>
        public int StartAddress
        {
            get { return this.min.Address; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Selection"/> class.
        /// </summary>
        /// <param name="first">
        /// The <see cref="IndexPoint"/> of the first coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        public Selection(IndexPoint first)
        {
            this.first = first.Copy();
            this.last = first.Copy();
            this.min = first.Copy();
            this.max = first.Copy();
            this.containerWidth = first.Zero.Editor.MapWidth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Selection"/> class.
        /// </summary>
        /// <param name="first">
        /// The <see cref="IndexPoint"/> of the first coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        /// <param name="last">
        /// The <see cref="IndexPoint"/> of the last coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        public Selection(IndexPoint first, IndexPoint last)
        {
            this.first = first.Copy();
            this.last = new IndexPoint(first.Zero);
            this.min = new IndexPoint(first.Zero);
            this.max = new IndexPoint(first.Zero);
            this.containerWidth = first.Zero.Editor.MapWidth;
            Update(last);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Selection"/> class.
        /// </summary>
        /// <param name="first">
        /// The <see cref="IndexPoint"/> of the first coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        /// <param name="size">
        /// The <see cref="Size"/> of the <see cref="Selection"/>
        /// </param>
        public Selection(IndexPoint first, Size size)
        {
            this.first = first.Copy();
            this.last = new IndexPoint(first.Zero);
            this.min = new IndexPoint(first.Zero);
            this.max = new IndexPoint(first.Zero);
            this.containerWidth = first.Zero.Editor.MapWidth;
            Update(size);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a copy of this <see cref="Selection"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="Selection"/> that is representationally the
        /// same as this <see cref="Selection"/>.
        /// </returns>
        public ISelection Copy()
        {
            return new Selection(this.First, this.Last);
        }

        /// <summary>
        /// Updates the <see cref="Selection"/> to span onto the given
        /// <see cref="IndexPoint"/>s.
        /// </summary>
        /// <param name="first">
        /// The <see cref="IndexPoint"/> of the first coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        /// <param name="last">
        /// The <see cref="IndexPoint"/> of the last coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        public void Update(IndexPoint first, IndexPoint last)
        {
            this.First.RelativePoint = first.RelativePoint;
            Update(last);
        }

        /// <summary>
        /// Updates the <see cref="Selection"/> to span onto the given
        /// <see cref="IndexPoint"/>s.
        /// </summary>
        /// <param name="first">
        /// The <see cref="IndexPoint"/> of the first coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        /// <param name="size">
        /// The <see cref="Size"/> of the <see cref="Selection"/>
        /// </param>
        public void Update(IndexPoint first, Size size)
        {
            this.First.RelativePoint = first.RelativePoint;
            Update(size);
        }

        /// <summary>
        /// Updates the <see cref="Selection"/> to span onto the given
        /// <see cref="IndexPoint"/>.
        /// </summary>
        /// <param name="last">
        /// The <see cref="IndexPoint"/> of the last coordinate of the
        /// <see cref="Selection"/>.
        /// </param>
        public void Update(IndexPoint last)
        {
            this.Last.RelativePoint = last.RelativePoint;
            Update();
        }

        /// <summary>
        /// Updates the <see cref="Selection"/> to have the given <see cref="Size"/>.
        /// </summary>
        /// <param name="size">
        /// The <see cref="Size"/> of the <see cref="Selection"/>.
        /// </param>
        public void Update(Size size)
        {
            this.Last.RelativePoint = this.First.RelativePoint + size - new Size(1, 1);
            Update();
        }

        /// <summary>
        /// Updates <see cref="Min"/> and <see cref="Max"/>
        /// </summary>
        private void Update()
        {
            this.Min.RelativeX = Math.Min(this.First.RelativeX, this.Last.RelativeX);
            this.Min.RelativeY = Math.Min(this.First.RelativeY, this.Last.RelativeY);
            this.Max.RelativeX = Math.Max(this.First.RelativeX, this.Last.RelativeX);
            this.Max.RelativeY = Math.Max(this.First.RelativeY, this.Last.RelativeY);
        }
        #endregion
    }
}