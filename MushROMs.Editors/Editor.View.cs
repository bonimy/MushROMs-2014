/* Deals with the view region of the editor.
 * Within the map, there exists a specified region that is
 * viewable to the user. The view region has a specified
 * origin and size.
 */

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
        /// The fallback view width value.
        /// This field is constant.
        /// </summary>
        private const int FallbackViewWidth = 0x10;
        /// <summary>
        /// The fallback view height value.
        /// This field is constant.
        /// </summary>
        private const int FallbackViewHeight = 8;
        #endregion

        #region Fields
        /// <summary>
        /// The number of tiles that make up a single row of the view region.
        /// </summary>
        private int viewW;
        /// <summary>
        /// The number of tiles that make up a single column of the view region.
        /// </summary>
        private int viewH;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the number of tiles that make up a single row of
        /// the view region.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// View size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewWidth
        {
            get { return this.viewW; }
            set { SetViewSize(value, this.viewH); }
        }
        /// <summary>
        /// Gets or sets the number of tiles that make up a single column
        /// of the view region.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// View size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewHeight
        {
            get { return this.viewH; }
            set { SetViewSize(this.viewW, value); }
        }
        /// <summary>
        /// Gets or sets the number of columns and rows of the view region.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// View size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Category("Appearance")]
        [Description("The number of columns and rows of the view region.")]
        public Size ViewSize
        {
            get { return new Size(this.ViewWidth, this.ViewHeight); }
            set { SetViewSize(value.Width, value.Height); }
        }
        /// <summary>
        /// Gets the number of tiles that make up the view region.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NumViewTiles
        {
            get { return this.ViewHeight * this.ViewWidth; }
        }

        /// <summary>
        /// Gets the number of tiles that are actually visible in the view region.
        /// </summary>
        /// <remarks>
        /// This property is intended for linear editors.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int NumVisibleTiles
        {
            get { return Math.Min(this.MapLength - this.Zero.Index, this.NumViewTiles); }
        }

        /// <summary>
        /// Gets the <see cref="Size"/> of the tile region that is actually visible.
        /// </summary>
        /// <remarks>
        /// This property is intended for rectangular editors.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Size VisibleTileRegion
        {
            get { return new Size(Math.Min(this.ViewWidth, this.ViewHeight), Math.Min(this.ViewHeight, this.MapHeight)); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the view size of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="viewW">
        /// The width of the view size.
        /// </param>
        /// <param name="viewH">
        /// The height of the view size.
        /// </param>
        /// <exception cref="ArgumentException">
        /// View size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        protected virtual void SetViewSize(int viewW, int viewH)
        {
            // Make sure the view size is valid.
            if (viewW <= 0 || viewH <= 0)
                throw new ArgumentException(Resources.ErrorViewSize);

            // Avoid redundant setting.
            if (this.viewW == viewW && this.viewH == viewH)
                return;

            // Set the view size.
            this.viewW = viewW;
            this.viewH = viewH;

            OnViewSizeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="ViewSizeChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnViewSizeChanged(EventArgs e)
        {
            // Change the map dimensions for linear displays
            if (this.linear)
            {
                // Set the rectangular map dimensions to fit the view width.
                this.mapW = this.viewW;
                this.mapH = this.mapL / this.viewW;
                this.mapR = this.mapL % this.viewW;

                OnSelectedTilesChanged(EventArgs.Empty);
            }

            // Keep the zero point in the view boundary.
            if (this.mapW > 0)
                SetZeroPointBoundary();

            // Invoke the event.
            if (ViewSizeChanged != null)
                ViewSizeChanged(this, e);

            // The editor size has changed.
            OnVisibleSizeChanged(EventArgs.Empty);
        }
        #endregion

        #region Events
        /// <summary>
        /// Ocurs when the view size of the <see cref="Editor"/> changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the size of the editor changes.")]
        public event EventHandler ViewSizeChanged;
        #endregion
    }
}