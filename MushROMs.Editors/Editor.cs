/* The Editor class is the primary tool of MushROMs.
 * This class is split up into many partial files, be
 * sure to check them all out.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace MushROMs.Editors
{
    /// <summary>
    /// Provides a generic editor of graphical tile data
    /// </summary>
    public partial class Editor
    {
        #region Properties
        /// <summary>
        /// Gets the title of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get
            {
                string title = this.FileDataType != FileDataTypes.ProgramCreated ? this.Untitled : Path.GetFileName(this.FilePath);
                if (this.History.Unsaved)
                    title += UnsavedNotification;
                return title;
            }
        }

        /// <summary>
        /// Gets the full width of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Width
        {
            get { return this.MapWidth * this.CellWidth; }
        }
        /// <summary>
        /// Gets the full height of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Height
        {
            get { return this.MapHeight * this.CellHeight; }
        }
        /// <summary>
        /// Gets the full size of the editor.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size Size
        {
            get { return new Size(this.Width, this.Height); }
        }

        /// <summary>
        /// Gets the visible width of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleWidth
        {
            get { return this.ViewWidth * this.CellWidth; }
        }
        /// <summary>
        /// Gets the visible height of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleHeight
        {
            get { return this.ViewHeight * this.CellHeight; }
        }
        /// <summary>
        /// Gets the visible size of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size VisibleSize
        {
            get { return new Size(this.VisibleWidth, this.VisibleHeight); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class.
        /// </summary>
        public Editor()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class together
        /// with the specified container.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IContainer"/> that represents the container for the editor.
        /// </param>
        public Editor(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            container.Add(this);
            Initialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes this instance of the <see cref="Editor"/> class.
        /// </summary>
        private void Initialize()
        {
            // Initialize the zero point.
            this.zero = new ZeroPoint(this);
            this.zero.AddressChanged += new EventHandler(Zero_AddressChanged);

            // Initialize the view region.
            this.viewW = FallbackViewWidth;
            this.viewH = FallbackViewHeight;

            // Initialize the tile size.
            this.tileW = FallbackTileWidth;
            this.tileH = FallbackTileHeight;
            this.zoomW = FallbackZoomWidth;
            this.zoomH = FallbackZoomHeight;

            // Initialize the active index.
            this.active = new IndexPoint(this.zero);
            this.active.IndexChanged += new EventHandler(Active_IndexChanged);

            // Initialize scrolling parameters.
            this.scroll = FallbackCanScrollSelection;
            this.selectType = FallbackSelectType;
            this.hScrollEnd = FallbackHScrollEnd;
            this.vScrollEnd = FallbackVScrollEnd;

            // Assume there is no file data type.
            this.fileDataType = FileDataTypes.NotAFile;

            // Initialize the file path to an empty string.
            this.fp = string.Empty;

            // Initialize the undo/redo data
            this.history = new History<IEditorData>();
            this.history.Undo += new EventHandler(History_Undo);
            this.history.Redo += new EventHandler(History_Redo);
        }

        /// <summary>
        /// Raises the <see cref="SizeChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnSizeChanged(EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VisibleSizeChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnVisibleSizeChanged(EventArgs e)
        {
            if (VisibleSizeChanged != null)
                VisibleSizeChanged(this, e);

            OnVisibleChange(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="VisibleChange"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnVisibleChange(EventArgs e)
        {
            if (VisibleChange != null)
                VisibleChange(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the size of <see cref="Editor"/> changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the size of the editor changes.")]
        public event EventHandler SizeChanged;
        /// <summary>
        /// Occurs when the visible size of <see cref="Editor"/> changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the size of the visible region changes.")]
        public event EventHandler VisibleSizeChanged;
        /// <summary>
        /// Occurs when any viewable properties of the <see cref="Editor"/> change.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when any viewable change is made.")]
        public event EventHandler VisibleChange;
        #endregion
    }
}