using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    partial class EditorControl
    {
        #region Constant and readonly fields
        /// <summary>
        /// The fallback <see cref="SelectMouseButtons"/> value.
        /// This field is constant.
        /// </summary>
        private const MouseButtons FallbackSelectMouseButton = MouseButtons.Left;

        /// <summary>
        /// The fallback number of rows to scroll the editor from the mouse wheel scroll.
        /// This field is constant.
        /// </summary>
        private const int MouseWheelScrollRows = 4;
        #endregion

        #region Fields
        /// <summary>
        /// A value that is true if the user's mouse click stays on the same tile.
        /// </summary>
        private bool sameTile;

        /// <summary>
        /// The <see cref="MouseButtons"/> to be held when selecting a region.
        /// </summary>
        private MouseButtons selectRegionMouseButton;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="MouseButtons"/> to be held when selecting a region.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("The mouse buttons to be held when selecting a region.")]
        [DefaultValue(FallbackSelectMouseButton)]
        public MouseButtons SelectMouseButtons
        {
            get { return this.selectRegionMouseButton; }
            set { this.selectRegionMouseButton = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Processes the <see cref="Control.MouseMove"/> logic for the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnInRangeMouseMove(MouseEventArgs e)
        {
            if (this.Editor == null || this.Editor.Data.Data == IntPtr.Zero)
                return;

            // Determinde if the selection needs to be finalized.
            if (this.Editor.Selecting &&
                e.Button != this.selectRegionMouseButton)
                this.Editor.FinalizeSelection();

            // Get the current tile.
            Point tile = this.GetTileFromPixel(e.Location);

            // Determine whether we are still on the same tile.
            this.sameTile &= this.Editor.Active.RelativePoint == tile;

            // Update the active tile.
            this.Editor.SetActiveTile(tile);

            base.OnInRangeMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ProcessEditorMouseDown(e);
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Processes the <see cref="Control.MouseDown"/> logic for the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorMouseDown(MouseEventArgs e)
        {
            if (this.Editor == null || this.Editor.Data.Data == IntPtr.Zero)
                return;

            // Update the active tile.
            this.Editor.SetActiveTile(this.GetTileFromPixel(e.Location));

            // Process possible selections.
            if (e.Button == this.selectRegionMouseButton)
            {
                // Create a new selection
                this.Editor.InitializeSelection();

                // Initialize this boolean to true for tile mouse click processing
                this.sameTile = true;
            }
        }

        /// <summary>
        /// Raise the <see cref="Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            ProcessEditorMouseUp(e);
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Processes the <see cref="Control.MouseUp"/> logic for the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorMouseUp(MouseEventArgs e)
        {
            if (this.Editor == null || this.Editor.Data.Data == IntPtr.Zero)
                return;
            
            // The mouse button is no longer being held down.
            this.sameTile = false;
        }

        /// <summary>
        /// Rauses the <see cref="Control.MouseClick"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Determine whether the click event happened on the same tile.
            if (this.IsSameTile)
                OnTileMouseClick(e);

            base.OnMouseClick(e);
        }

        /// <summary>
        /// Rauses the <see cref="TileMouseClick"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnTileMouseClick(MouseEventArgs e)
        {
            if (TileMouseClick != null)
                TileMouseClick(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseDoubleClick"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            // Determine whether the click event happened on the same tile.
            if (this.IsSameTile)
                OnEditorMouseDoubleClick(e);

            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Rauses the <see cref="TileMouseDoubleClick"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnEditorMouseDoubleClick(MouseEventArgs e)
        {
            if (TileMouseDoubleClick != null)
                TileMouseDoubleClick(this, e);
        }

        /// <summary>
        /// Raises the <see cref="MouseWheel"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ProcessEditorMouseWheel(e);
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Processes the <see cref="MouseWheel"/> logic for the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorMouseWheel(MouseEventArgs e)
        {
            if (this.Editor != null)
                this.Editor.Scroll(new Size(0, (-MouseWheelScrollRows * e.Delta) / DrawControl.MouseWheelThreshold));
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a tile is clicked by the mouse.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("Occurs when a tile is clicked by the mouse.")]
        public event MouseEventHandler TileMouseClick;

        /// <summary>
        /// Occurs when a tile is double-clicked by the mouse.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("Occurs when a tile is clicked by the mouse.")]
        public event MouseEventHandler TileMouseDoubleClick;
        #endregion
    }
}