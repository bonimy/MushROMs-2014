using System;
using System.ComponentModel;
using System.Drawing;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    partial class Editor
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback scroll selection value.
        /// This field is constant.
        /// </summary>
        private const bool FallbackCanScrollSelection = true;

        /// <summary>
        /// The fallback horizontal <see cref="ScrollEnd"/> value.
        /// This field is constant.
        /// </summary>
        private const ScrollEnd FallbackHScrollEnd = ScrollEnd.Last;
        /// <summary>
        /// The fallback vertical <see cref="ScrollEnd"/> value.
        /// This field is constant.
        /// </summary>
        private const ScrollEnd FallbackVScrollEnd = ScrollEnd.Full;
        #endregion

        #region Fields
        /// <summary>
        /// The address of the first visible tile in the view region.
        /// </summary>
        /// <remarks>
        /// This address is the relative origin of the view region. All
        /// relative coordinates are taken with respect to this address.
        /// </remarks>
        private ZeroPoint zero;

        /// <summary>
        /// A value that determines whether the <see cref="Editor"/> should
        /// automatically scroll itself while selecting.
        /// </summary>
        private bool scroll;

        /// <summary>
        /// The horizontal <see cref="ScrollEnd"/> of the <see cref="Editor"/>.
        /// </summary>
        private ScrollEnd hScrollEnd;
        /// <summary>
        /// The vertical <see cref="ScrollEnd"/> of the <see cref="Editor"/>.
        /// </summary>
        private ScrollEnd vScrollEnd;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the address of the first visible tile in the view region.
        /// </summary>
        /// <remarks>
        /// This address is the relative origin of the view region. All
        /// relative coordinates are taken with respect to this address.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ZeroPoint Zero
        {
            get { return this.zero; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the
        /// <see cref="Editor"/> should automatically scroll itself while
        /// selecting.
        /// </summary>
        [Category("Scroll")]
        [DefaultValue(FallbackCanScrollSelection)]
        [Description("Determines whether the editor can automatically scroll itself while selecting.")]
        public bool CanScrollSelection
        {
            get { return this.scroll; }
            set { this.scroll = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal <see cref="ScrollEnd"/> of the
        /// <see cref="Editor"/>.
        /// </summary>
        [Category("Scroll")]
        [DefaultValue(FallbackHScrollEnd)]
        [Description("The horizontal scroll end of the editor.")]
        public ScrollEnd HScrollEnd
        {
            get { return this.hScrollEnd; }
            set { this.hScrollEnd = value; OnHScrollEndChanged(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets or sets the vertical <see cref="ScrollEnd"/> of the <see cref="Editor"/>.
        /// </summary>
        [Category("Scroll")]
        [DefaultValue(FallbackVScrollEnd)]
        [Description("The vertical scroll end of the editor.")]
        public ScrollEnd VScrollEnd
        {
            get { return this.vScrollEnd; }
            set { this.vScrollEnd = value; OnVScrollEndChanged(EventArgs.Empty); }
        }

        /// <summary>
        /// Gets the number of extra tiles the <see cref="Editor"/> can
        /// scroll past horizontally.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int HScrollLimit
        {
            get
            {
                switch (this.HScrollEnd)
                {
                    case ScrollEnd.Full:
                        return 0;
                    case ScrollEnd.Last:
                        return this.ViewWidth - 1;
                    case ScrollEnd.None:
                        return this.ViewWidth;
                    default:
                        throw new InvalidEnumArgumentException(Resources.ErrorScrollEnd);
                }
            }
        }

        /// <summary>
        /// Gets the number of extra tiles the <see cref="Editor"/> can
        /// scroll past vertically.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int VScrollLimit
        {
            get
            {
                switch (this.vScrollEnd)
                {
                    case ScrollEnd.Full:
                        return 0;
                    case ScrollEnd.Last:
                        return this.ViewHeight - 1;
                    case ScrollEnd.None:
                        return this.ViewHeight;
                    default:
                        throw new InvalidEnumArgumentException(Resources.ErrorScrollEnd);
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the active tile of the editor, making sure to scroll the
        /// view region if necessary.
        /// </summary>
        /// <param name="tile">
        /// The active tile relative to the <see cref="ZeroPoint"/>.
        /// </param>
        public virtual void SetActiveTile(Point tile)
        {
            // Get the IndexPoint of the tile.
            IndexPoint ip = new IndexPoint(this.Zero);
            ip.RelativePoint = tile;

            // The scroll amount of the zero point if the selection goes out of view range.
            Size delta = Size.Empty;

            // Determine whether the active tile is outside of the horizontal boundary.
            if (ip.RelativeX >= this.ViewWidth)
            {
                // Set the active tile to the right edge of the view region.
                ip.RelativeX = this.ViewWidth - 1;

                // Set the scroll offset right one tile if permissable.
                if (this.Zero.X + 1 < this.MapWidth)
                    delta.Width = 1;
            }
            else if (ip.RelativeX < 0)  // Repeat the check for the left edge.
            {
                ip.RelativeX = 0;
                if (this.Zero.X - 1 >= 0)
                    delta.Width = -1;
            }

            // Now check for the vertical boundaries.
            if (ip.RelativeY >= this.ViewHeight)
            {
                ip.RelativeY = this.ViewHeight - 1;
                if (this.Zero.Y + 1 < this.MapHeight)
                    delta.Height = 1;
            }
            else if (ip.RelativeY < 0)
            {
                ip.RelativeY = 0;
                if (this.Zero.Y - 1 >= 0)
                    delta.Height = -1;
            }

            // Do scrolling logic if selecting and allowed to scroll.
            if (this.Selecting && this.CanScrollSelection)
            {
                // Adjust horizontal scrolling given the editor boundaries.
                if (Math.Abs(ip.RelativeX - this.Selection.First.RelativeX + delta.Width) >= (this.MapIsLinear ? this.ViewWidth : this.MapWidth))
                    delta.Width = 0;

                // Adjust vertical scrolling.
                if (Math.Abs(ip.AbsoluteY + delta.Height) >= this.MapHeight)
                    delta.Height = 0;

                // Scroll the editor.
                if (delta != Size.Empty && this.CanScrollSelection)
                    Scroll(delta);
            }

            // Set the active point.
            this.Active.RelativePoint = ip.RelativePoint;
        }

        /// <summary>
        /// Scrolls the view region of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="amount">
        /// The amount to scroll the <see cref="Editor"/>.
        /// </param>
        /// <remarks>
        /// The scroll amounts will be bounded their horizontal and
        /// vertical <see cref="ScrollEnd"/> values.
        /// </remarks>
        public virtual void Scroll(Size amount)
        {
            if (!this.CanScrollSelection)
                return;

            // Do nothing if no scroll.
            if (amount == Size.Empty)
                return;

            // Set the new zero point.
            this.zero.Point += amount;
        }

        private void Zero_AddressChanged(object sender, EventArgs e)
        {
            SetZeroPointBoundary();

            // Changing the zero index means the visible boundary path has changed.
            OnSelectedTilesChanged(EventArgs.Empty);
            OnVisibleChange(EventArgs.Empty);
        }

        /// <summary>
        /// Bounds the <see cref="ZeroPoint"/>.
        /// </summary>
        protected virtual void SetZeroPointBoundary()
        {
            Point p = this.Zero.Point;

            // Set the left and right boundaries.
            if (p.X > this.HScrollLimit + this.MapWidth - this.ViewWidth)
                p.X = this.HScrollLimit + this.MapWidth - this.ViewWidth;
            if (p.X < 0)
                p.X = 0;

            // Set the top and bottom boundaries.
            if (p.Y > this.VScrollLimit + this.MapHeight - this.ViewHeight)
                p.Y = this.VScrollLimit + this.MapHeight - this.ViewHeight;
            if (p.Y < 0)
                p.Y = 0;

            if (p != this.Zero.Point)
            {
                this.Zero.Point = p;
                return;
            }
        }

        /// <summary>
        /// Raises the <see cref="HScrollEndChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnHScrollEndChanged(EventArgs e)
        {
            if (HScrollEndChanged != null)
                HScrollEndChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VScrollEndChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnVScrollEndChanged(EventArgs e)
        {
            if (VScrollEndChanged != null)
                VScrollEndChanged(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when <see cref="Zero"/> changes.
        /// </summary>
        [Category("Scroll")]
        [Description("Occurs when the zero-point changes.")]
        public event EventHandler ZeroAddressChanged
        {
            add { this.Zero.AddressChanged += value; }
            remove { this.Zero.AddressChanged -= value; }
        }

        /// <summary>
        /// Occurs when the <see cref="HScrollEnd"/> value changes.
        /// </summary>
        [Category("Scroll")]
        [Description("Occurs when the horizontal scroll end changes.")]
        public event EventHandler HScrollEndChanged;

        /// <summary>
        /// Occurs when the <see cref="VScrollEnd"/> value changes.
        /// </summary>
        [Category("Scroll")]
        [Description("Occurs when the vertical scroll end changes.")]
        public event EventHandler VScrollEndChanged;
        #endregion
    }
}
