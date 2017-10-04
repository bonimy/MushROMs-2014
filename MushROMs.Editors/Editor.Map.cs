/* Deals with the map of the editor.
 * The map is an array of tiles (e.g. gfx tiles, map16 tiles,
 * palette color squares, etc.) that are arranged in a 2D array.
 * However, it is important to note that while the map is a 2D
 * grid, the data of the map can be linear (palettes, gfx, tiles)
 * or rectangular (level data) and it is important to specify this
 * property. This property determines how the map behaves
 * if the editor size changes at any time.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using MushROMs.Unmanaged;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    partial class Editor
    {
        #region Fields
        /// <summary>
        /// A value that determines whether the map region resizes with
        /// the view region.
        /// </summary>
        private bool linear;
        /// <summary>
        /// The number of tiles that make up the map.
        /// </summary>
        private int mapL;
        /// <summary>
        /// The number of tiles that make up a single row of the map.
        /// </summary>
        private int mapW;
        /// <summary>
        /// The number of tiles that make up a single column of the map.
        /// </summary>
        private int mapH;
        /// <summary>
        /// The number of tiles remaining after the final row of the map.
        /// </summary>
        private int mapR;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value that determines whether the data region resizes
        /// with the view region.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MapIsLinear
        {
            get { return this.linear; }
        }
        /// <summary>
        /// Gets or sets the number of tiles that make up the data.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is less than or equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MapLength
        {
            get { return this.mapL; }
            protected set { SetMapLength(value); }
        }
        /// <summary>
        /// Gets or sets the number of tiles that make up a single row of
        /// the data.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Map size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MapWidth
        {
            get { return this.mapW; }
            protected set { SetMapSize(value, this.mapH); }
        }
        /// <summary>
        /// Gets or sets the number of tiles that make up a single column
        /// of the data.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Map size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MapHeight
        {
            get { return this.mapH; }
            protected set { SetMapSize(this.mapW, value); }
        }
        /// <summary>
        /// Gets or sets the size of the data region.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Map size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MapSize
        {
            get { return new Size(this.MapWidth, this.MapHeight); }
            protected set { SetMapSize(value.Width, value.Height); }
        }
        /// <summary>
        /// Gets the number of tiles remaining in the final row of the data.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MapRemainder
        {
            get { return this.mapR; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the tile map size of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="mapW">
        /// The width of the tile map size.
        /// </param>
        /// <param name="mapH">
        /// The height of the tile map size.
        /// </param>
        /// <remarks>
        /// When calling this method, the map will become rectangular.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Map size has a width or height component that is less than or
        /// equal to zero.
        /// </exception>
        protected virtual void SetMapSize(int mapW, int mapH)
        {
            // Make sure the map parameters are in a valid range
            if (mapW <= 0 || mapH <= 0)
                throw new ArgumentException(Resources.ErrorMapSize);

            // Avoid redundant setting.
            if (!this.linear && this.mapW == mapW && this.mapH == mapH)
                return;

            // The map data is rectangular
            this.linear = false;

            // Set the map parameters
            this.mapW = mapW;
            this.mapH = mapH;
            this.mapL = mapW * mapH;
            this.mapR = 0;

            // The map has been reset
            OnMapReset(EventArgs.Empty);
        }

        /// <summary>
        /// Sets the tile map length of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="mapL">
        /// The total number of tiles that make up the map.
        /// </param>
        /// <remarks>
        /// When calling this method, the method will become linear.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="mapL"/> is less than or equal to zero.
        /// </exception>
        protected virtual void SetMapLength(int mapL)
        {
            // Make sure the map length is valid.
            if (mapL <= 0)
                throw new ArgumentException(Resources.ErrorLengthNotPositive);

            // Avoid redundant setting.
            if (this.linear && this.mapL == mapL)
                return;

            // The map data is linear.
            this.linear = true;

            // Set the map parameters (the map width is now linked to the view width)
            this.mapW = this.viewW;
            this.mapH = mapL / this.viewW;
            this.mapL = mapL;
            this.mapR = mapL % this.viewW;

            // The map has been reset
            OnMapReset(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="MapReset"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnMapReset(EventArgs e)
        {
            // Reset the selection.
            this.selection = new Selection(new IndexPoint(this.zero));

            // Reset the selected tiles.
            Memory.FreeMemory(this.selectedTiles);
            this.selectedTiles = Memory.CreateMemory(this.MapLength * sizeof(byte));
            SetSelectedTiles();

            // Reset zero address and active index
            this.Zero.Address = 0;
            this.Active.Index = 0;

            if (MapReset != null)
                MapReset(this, e);

            OnVisibleChange(EventArgs.Empty);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the map size changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the map size or length changes.")]
        public event EventHandler MapReset;
        #endregion
    }
}