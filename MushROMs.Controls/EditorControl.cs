using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using MushROMs.Editors;

namespace MushROMs.Controls
{
    /// <summary>
    /// A <see cref="DrawControl"/> that encapsulates the <see cref="Editor"/> class.
    /// </summary>
    public unsafe partial class EditorControl : DrawControl, IEditorControl
    {
        #region Fields
        /// <summary>
        /// The <see cref="Editor"/> that this <see cref="EditorControl"/> encapsulates.
        /// </summary>
        private Editor editor;

        /// <summary>
        /// The <see cref="EditorVScrollBar"/> of this <see cref="EditorControl"/>.
        /// </summary>
        private EditorVScrollBar vScrollBar;
        /// <summary>
        /// The <see cref="EditorHScrollBar"/> of this <see cref="EditorControl"/>.
        /// </summary>
        private EditorHScrollBar hScrollBar;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="Editor"/> changes.
        /// </summary>
        public event EventHandler EditorChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Editor"/> associated with the <see cref="EditorControl"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Editor Editor
        {
            get { return this.editor; }
            set
            {
                if (this.Editor == value)
                    return;

                if (this.Editor != null)
                {
                    this.Editor.SelectedTilesChanged -= Editor_BoundaryChanged;
                    this.Editor.VisibleSizeChanged -= Editor_EditorSizeChanged;
                    this.Editor.VisibleChange -= Editor_EditorVisibilityChanged;
                    this.Editor.Active.IndexChanged -= Editor_ActiveIndexChanged;
                    this.Editor.DataModified -= Editor_DataModified;
                }

                this.editor = value;

                if (this.Editor != null)
                {
                    this.Editor.SelectedTilesChanged += new EventHandler(Editor_BoundaryChanged);
                    this.Editor.VisibleSizeChanged += new EventHandler(Editor_EditorSizeChanged);
                    this.Editor.VisibleChange += new EventHandler(Editor_EditorVisibilityChanged);
                    this.Editor.Active.IndexChanged += new EventHandler(Editor_ActiveIndexChanged);
                    this.Editor.DataModified += new EventHandler(Editor_DataModified);
                }

                OnEditorChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets a value that is true if the user's mouse click is on the
        /// same tile.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual bool IsSameTile
        {
            get
            {
                if (this.Editor == null)
                    return false;
                return this.sameTile && this.Editor.IsActiveInMap;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="EditorVScrollBar"/> of this
        /// <see cref="EditorControl"/>.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("The vertical scroll bar that handles the scrolling for this editor control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EditorVScrollBar EditorVScrollBar
        {
            get { return this.vScrollBar; }
            set
            {
                if (this.EditorVScrollBar == value)
                    return;

                this.vScrollBar = value;

                if (this.EditorVScrollBar != null)
                    this.EditorVScrollBar.EditorControl = this;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="EditorHScrollBar"/> of this
        /// <see cref="EditorControl"/>.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("The horizontal scroll bar that handles the scrolling for this editor control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EditorHScrollBar EditorHScrollBar
        {
            get { return this.hScrollBar; }
            set
            {
                if (this.EditorHScrollBar == value)
                    return;

                this.hScrollBar = value;

                if (this.EditorHScrollBar != null)
                    this.EditorHScrollBar.EditorControl = this;
            }
        }

        /// <summary>
        /// Gets the height and width of the client area of the control.
        /// </summary>
        [Localizable(true)]
        [Browsable(true)]   // This accessor is redone so it can be browsable in the designer.
        [Description("The size of the client area of the form.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size ClientSize
        {
            get { return base.ClientSize; }
        }

        /// <summary>
        /// Gets or sets the height and width of the control
        /// </summary>
        [Localizable(true)]
        [Browsable(true)]
        [Description("The size of the control in pixels.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get { return base.Size; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorControl"/> class.
        /// </summary>
        public EditorControl()
        {
            // Initialize the control's editor.
            this.Editor = InitializeEditor();

            // Draw the editor data.
            SetControlSize();
            Redraw();

            // Initialize selection mouse buttons.
            this.selectRegionMouseButton = FallbackSelectMouseButton;

            // Initialize selection boundary.
            this.boundary = new GraphicsPath();
        }
        #endregion

        #region Methods
        /// <summary>
        /// When overridden in a derived class, initializes the
        /// <see cref="Editor"/> that will be used in the
        /// <see cref="EditorControl"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="Editor"/> of the <see cref="EditorControl"/>.
        /// </returns>
        protected virtual Editor InitializeEditor()
        {
            return new Editor();
        }

        /// <summary>
        /// Sets the client size of the <see cref="EditorControl"/> from the <see cref="Editor"/>.
        /// </summary>
        protected virtual void SetControlSize()
        {
            if (this.Editor != null)
                base.ClientSize = this.Editor.VisibleSize;
        }

        /// <summary>
        /// Gets the coordinates of a tile on the map given a
        /// pixel location.
        /// </summary>
        /// <param name="pixel">
        /// The pixel location to get the tile from.
        /// </param>
        /// <returns>
        /// A <see cref="Point"/> of the tile's relative coordinates.
        /// </returns>
        public virtual Point GetTileFromPixel(Point pixel)
        {
            if (this.Editor == null)
                return Point.Empty;

            // Each tile coordinate is separated exactly by the cell size.
            Point p = new Point(pixel.X / this.Editor.CellSize.Width, pixel.Y / this.Editor.CellSize.Height);

            // We round from the left boundary for negative coordinates.
            if (pixel.X < 0)
                p.X--;
            if (pixel.Y < 0)
                p.Y--;

            return p;
        }

        /// <summary>
        /// Raises the <see cref="EditorChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnEditorChanged(EventArgs e)
        {
            if (EditorChanged != null)
                EditorChanged(this, e);
        }

        private void Editor_BoundaryChanged(object sender, EventArgs e)
        {
            SetBoundary();
        }

        private void Editor_ActiveIndexChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Editor_EditorSizeChanged(object sender, EventArgs e)
        {
            SetControlSize();
        }

        private void Editor_EditorVisibilityChanged(object sender, EventArgs e)
        {
            SetBoundary();
            Redraw();
        }

        private void Editor_DataModified(object sender, EventArgs e)
        {
            Redraw();
        }
        #endregion
    }
}