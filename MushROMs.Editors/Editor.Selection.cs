/* Deals with selections in the editor.
 * Most editors need some way to select tiles and
 * the process is usually very generic. Here,
 * rectangular selections have been added. Also,
 * the selection is not lost when the view region
 * changes in position or size.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using MushROMs.Unmanaged;

namespace MushROMs.Editors
{
    unsafe partial class Editor
    {
        #region Constant and read-only fields
        /// <summary>
        /// The default <see cref="SelectType"/> of the editor.
        /// This field is constant.
        /// </summary>
        private const SelectType FallbackSelectType = SelectType.Rectangular;
        #endregion

        #region Fields
        /// <summary>
        /// The <see cref="IndexPoint"/> of the active tile.
        /// </summary>
        private IndexPoint active;

        /// <summary>
        /// A value that determines whether the user is currently creating
        /// a selection in the <see cref="Editor"/>.
        /// </summary>
        private bool selecting;
        /// <summary>
        /// The <see cref="SelectType"/> value of the
        /// <see cref="Editor"/>.
        /// </summary>
        private SelectType selectType;
        /// <summary>
        /// A <see cref="Selection"/> defining the selected tiles of the
        /// <see cref="Editor"/>.
        /// </summary>
        private Selection selection;
        /// <summary>
        /// A pointer to a map of tiles that are selected in the
        /// <see cref="Editor"/>.
        /// </summary>
        private IntPtr selectedTiles;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="IndexPoint"/> of the active tile.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IndexPoint Active
        {
            get { return this.active; }
        }

        /// <summary>
        /// Gets a value that determines whether the active index is in the <see cref="Editor"/> map.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsActiveInMap
        {
            get { return this.Active.Index >= 0 && this.Active.Index < this.MapWidth; }
        }

        /// <summary>
        /// Gets a value that determines whether the user is currently
        /// creating a <see cref="Selection"/> in the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selecting
        {
            get { return this.selecting; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SelectType"/> value of the
        /// <see cref="Editor"/>.
        /// </summary>
        [Category("Selection")]
        [DefaultValue(FallbackSelectType)]
        public SelectType SelectType
        {
            get { return this.selectType; }
            set { this.selectType = value; }
        }

        /// <summary>
        /// Gets the current <see cref="Selection"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Selection Selection
        {
            get { return this.selection; }
        }

        /// <summary>
        /// Gets a pointer to a map of tiles that are selected in the
        /// <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr SelectedTiles
        {
            get { return this.selectedTiles; }
        }
        #endregion

        #region Methods
        private void Active_IndexChanged(object sender, EventArgs e)
        {
            UpdateSelection();
        }

        /// <summary>
        /// Initializes a new <see cref="Selection"/> to the
        /// <see cref="Editor"/>.
        /// </summary>
        public virtual void InitializeSelection()
        {
            this.selection = new Selection(this.active);
            OnSelectionInitialized(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="SelectionInitialized"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> containing the event data.
        /// </param>
        protected virtual void OnSelectionInitialized(EventArgs e)
        {
            this.selecting = true;
            SetSelectedTiles();
            if (SelectionInitialized != null)
                SelectionInitialized(this, e);
        }

        /// <summary>
        /// Updates the <see cref="Selection"/> to the active tile.
        /// </summary>
        protected virtual void UpdateSelection()
        {
            // Make sure we are selecting.
            if (this.Selecting)
            {
                if (this.SelectType != SelectType.Single)
                {
                    // Get the tile to select to
                    IndexPoint ip = this.Active.Copy();

                    // Make a square selection if necessary.
                    if (this.SelectType == SelectType.Square)
                    {
                        // Get the vertical parameters
                        int firstX = this.Selection.First.RelativeX;
                        int width = ip.RelativeX - firstX;
                        int xSign = Math.Sign(width);
                        if (xSign == 0)
                            xSign = 1;
                        width = Math.Abs(width);

                        // Get the horizontal parameters
                        int firstY = this.Selection.First.RelativeY;
                        int height = ip.RelativeY - firstY;
                        int ySign = Math.Sign(height);
                        if (ySign == 0)
                            ySign = -1;
                        height = Math.Abs(height);

                        // Make a square selection from the largest value
                        if (height > width)
                            ip.RelativeX = firstX + xSign * height;
                        else
                            ip.RelativeY = firstY + ySign * width;
                    }

                    // Update the selection.
                    this.Selection.Update(ip);
                }
                else
                    InitializeSelection();

                OnSelectionModified(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectionModified"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> containing the event data.
        /// </param>
        protected virtual void OnSelectionModified(EventArgs e)
        {
            SetSelectedTiles();
            if (SelectionModified != null)
                SelectionModified(this, e);
        }

        /// <summary>
        /// Finalizes the <see cref="Selection"/> of the
        /// <see cref="Editor"/> so that it can no longer be modified.
        /// </summary>
        public virtual void FinalizeSelection()
        {
            OnSelectionFinalized(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="SelectionFinalized"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> containing the event data.
        /// </param>
        protected virtual void OnSelectionFinalized(EventArgs e)
        {
            this.selecting = false;
            if (SelectionFinalized != null)
                SelectionFinalized(this, e);
        }

        /// <summary>
        /// Sets the selected tiles to the <see cref="Editor"/>.
        /// </summary>
        protected virtual void SetSelectedTiles()
        {
            // Visual Studio designer will crash without this check.
            if (this.MapLength <= 0)
                return;

            // Clear the selection map.
            Memory.SetMemory(this.SelectedTiles, 0, sizeof(byte) * this.MapLength);

            // Create the selected tiles array.
            bool* tiles = (bool*)this.SelectedTiles;

            // Iterate across every row.
            for (int y = this.Selection.Height; --y >= 0; )
            {
                // Get the start index for this row.
                int j = this.Selection.Min.Index + (y * this.Selection.ContainerWidth);

                // Iterate across every selected tile for the row.
                for (int x = this.Selection.Width; --x >= 0; j++)
                    if (j >= 0 && j < this.MapLength)
                        tiles[j] = true;
            }

            OnSelectedTilesChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="SelectedTilesChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnSelectedTilesChanged(EventArgs e)
        {
            if (SelectedTilesChanged != null)
                SelectedTilesChanged(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="Selection"/> is initialized.
        /// </summary>
        [Category("Selection")]
        [Description("Occurs when the selection is initialized.")]
        public event EventHandler SelectionInitialized;

        /// <summary>
        /// Occurs when the <see cref="Selection"/> is modified.
        /// </summary>
        [Category("Selection")]
        [Description("Occurs when the selection is modified.")]
        public event EventHandler SelectionModified;

        /// <summary>
        /// Occurs when the <see cref="Selection"/> is finalized.
        /// </summary>
        [Category("Selection")]
        [Description("Occurs when the selection is finialized.")]
        public event EventHandler SelectionFinalized;

        /// <summary>
        /// Occurs when the selected tiles change.
        /// </summary>
        [Category("Selection")]
        [Description("Occurs when the selected tiles change.")]
        public event EventHandler SelectedTilesChanged;

        /// <summary>
        /// Occurs when <see cref="Active"/> changes.
        /// </summary>
        [Category("Selection")]
        [Description("Occurs when the active index changes.")]
        public event EventHandler ActiveIndexChanged
        {
            add { this.active.IndexChanged += value; }
            remove { this.active.IndexChanged -= value; }
        }
        #endregion
    }

    #region Enumerations
    /// <summary>
    /// Specifies constants defining what kind of select regions
    /// to use in an <see cref="Editor"/>.
    /// </summary>
    public enum SelectType
    {
        /// <summary>
        /// Only a single tile at a time can be selected.
        /// </summary>
        Single,
        /// <summary>
        /// Rectangular regions can be selected.
        /// </summary>
        Rectangular,
        /// <summary>
        /// Rectangular regions with same width and height can be selected.
        /// </summary>
        Square
    }
    #endregion
}