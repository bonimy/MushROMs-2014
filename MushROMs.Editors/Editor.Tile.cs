/* Deals with the tile data of the editor.
 * Almost every editor uses a tile as a base.
 * This portion of the editor class deals with
 * the sizing and optional zoom scales of the
 * tile data. For example, a gfx tile would have
 * an 8x8 size, a map16 tile would be 16x16, and 
 * a palette color could be 1x1. To make the palette
 * color more visually noticable, you could give it a
 * 16x16 zoom for example. As well, the gfx tile could be
 * given a 2x2 zoom. The cell size is then defined as
 * the product of the tile size and zoom size. So, for
 * example, if a gfx tile has an 8x8 tile size and a
 * 2x2 zoom size, then its cell size is 16x16.
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
        /// The fallback tile width value.
        /// This field is constant.
        /// </summary>
        private const int FallbackTileWidth = 8;
        /// <summary>
        /// The fallback tile height value.
        /// This field is constant.
        /// </summary>
        private const int FallbackTileHeight = FallbackTileWidth;

        /// <summary>
        /// The fallback zoom width value.
        /// This field is constant.
        /// </summary>
        private const int FallbackZoomWidth = 1;
        /// <summary>
        /// The fallback zoom height value.
        /// This field is constant.
        /// </summary>
        private const int FallbackZoomHeight = FallbackZoomWidth;
        #endregion

        #region Variables
        /// <summary>
        /// The horizontal size of a single tile.
        /// </summary>
        private int tileW;
        /// <summary>
        /// The vertical size of a single tile.
        /// </summary>
        private int tileH;

        /// <summary>
        /// The horizontal zoom factor.
        /// </summary>
        private int zoomW;
        /// <summary>
        /// The vertical zoom factor.
        /// </summary>
        private int zoomH;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the horizontal size of a single tile.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Tile size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TileWidth
        {
            get { return this.tileW; }
            set { SetTileSize(value, this.tileH); }
        }
        /// <summary>
        /// Gets or sets the vertical size of a single tile.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Tile size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TileHeight
        {
            get { return this.tileH; }
            set { SetTileSize(this.tileW, value); }
        }
        /// <summary>
        /// Gets or sets the vertical and horizontal size of a single tile.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Tile size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Category("Appearance")]
        [Description("The vertical and horizontal size of a single tile.")]
        public Size TileSize
        {
            get { return new Size(this.TileWidth, this.TileHeight); }
            set { SetTileSize(value.Width, value.Height); }
        }

        /// <summary>
        /// Gets or sets the horizontal zoom factor.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Zoom size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomWidth
        {
            get { return this.zoomW; }
            set { SetZoomSize(value, this.zoomH); }
        }
        /// <summary>
        /// Gets or sets the vertical zoom factor.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Zoom size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomHeight
        {
            get { return this.zoomH; }
            set { SetZoomSize(this.zoomW, value); }
        }
        /// <summary>
        /// Gets or sets the vertical and horizontal zoom factor.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Zoom size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Category("Appearance")]
        [Description("The vertical and horizontal zoom factor.")]
        public Size ZoomSize
        {
            get { return new Size(this.ZoomWidth, this.ZoomHeight); }
            set { SetZoomSize(value.Width, value.Height); }
        }

        /// <summary>
        /// Gets the horizontal size of a single tile mutliplied by its
        /// zoom factor.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellWidth
        {
            get { return this.TileWidth * this.ZoomWidth; }
        }
        /// <summary>
        /// Gets the vertical size of a single tile mutliplied by its zoom
        /// factor.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellHeight
        {
            get { return this.TileHeight * this.ZoomHeight; }
        }
        /// <summary>
        /// Gets the size of a single tile multiplied by the zoom factor.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CellSize
        {
            get { return new Size(this.CellWidth, this.CellHeight); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the tile size of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="tileW">
        /// The width of the tile size.
        /// </param>
        /// <param name="tileH">
        /// The height of the tile size.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Tile size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        protected virtual void SetTileSize(int tileW, int tileH)
        {
            // Make sure the tile size is valid.
            if (tileW <= 0 || tileH <= 0)
                throw new ArgumentException(Resources.ErrorTileSize);

            // Avoid redundant setting.
            if (this.tileW == tileW && this.tileH == tileH)
                return;

            // Set the tile size.
            this.tileW = tileW;
            this.tileH = tileH;

            OnTileSizeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="TileSizeChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnTileSizeChanged(EventArgs e)
        {
            if (TileSizeChanged != null)
                TileSizeChanged(this, e);

            OnCellSizeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Sets the zoom size of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="zoomW">
        /// The width of the zoom size.
        /// </param>
        /// <param name="zoomH">
        /// The height of the zoom size.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Zoom size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        protected virtual void SetZoomSize(int zoomW, int zoomH)
        {
            // Make sure the zoom size is valid.
            if (zoomW <= 0 || zoomH <= 0)
                throw new ArgumentException(Resources.ErrorZoomSize);

            // Avoid redundant setting.
            if (this.zoomW == zoomW && this.zoomH == zoomH)
                return;

            //Set the zoom size.
            this.zoomW = zoomW;
            this.zoomH = zoomH;

            OnZoomSizeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="ZoomSizeChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnZoomSizeChanged(EventArgs e)
        {
            if (ZoomSizeChanged != null)
                ZoomSizeChanged(this, e);

            OnCellSizeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="CellSizeChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnCellSizeChanged(EventArgs e)
        {
            if (CellSizeChanged != null)
                CellSizeChanged(this, e);

            OnVisibleSizeChanged(EventArgs.Empty);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the tile size of the <see cref="Editor"/> changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the tile size of the editor changes.")]
        public event EventHandler TileSizeChanged;

        /// <summary>
        /// Occurs when the zoom size of the <see cref="Editor"/> changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the zoom size of the editor changes.")]
        public event EventHandler ZoomSizeChanged;

        /// <summary>
        /// Occurs when the cell size of the <see cref="Editor"/> changes.
        /// </summary>
        [Category("Appearance")]
        [Description("Occurs when the cell size of the editor changes.")]
        public event EventHandler CellSizeChanged;
        #endregion
    }
}