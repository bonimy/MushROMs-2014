using System;
using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Editors;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents the basic functionality of a scroll bar for a
    /// control containing an <see cref="Editor"/>.
    /// </summary>
    public abstract class EditorScrollBar : ScrollBar
    {
        #region Constant and read-only fields
        /// <summary>
        /// Horizontal scroll bar style.
        /// This field is constant.
        /// </summary>
        internal const int SBS_HORZ = 0x0000;
        /// <summary>
        /// Vertical scroll bar style.
        /// This field is constant.
        /// </summary>
        internal const int SBS_VERT = 0x0001;
        #endregion

        #region Variables
        /// <summary>
        /// The <see cref="EditorControl"/> associated with this <see cref="EditorScrollBar"/>.
        /// </summary>
        private IEditorControl editorControl;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="EditorControl"/> associated with this <see cref="EditorScrollBar"/>.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("The editor control associated with this scroll bar.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual IEditorControl EditorControl
        {
            get { return this.editorControl; }
            set
            {
                if (this.EditorControl == value)
                    return;

                // Detach the events from the last editor if it exists.
                if (this.Editor != null)
                {
                    this.EditorControl.Editor.Zero.AddressChanged -= Editor_AddressChanged;
                    this.EditorControl.Editor.DataReset -= Editor_Reset;
                    this.EditorControl.Editor.MapReset -= Editor_Reset;
                    this.EditorControl.Editor.VisibleSizeChanged -= Editor_Reset;

                    if (this.SBS == SBS_HORZ)
                        this.EditorControl.Editor.HScrollEndChanged -= Editor_Reset;
                    else
                        this.EditorControl.Editor.VScrollEndChanged -= Editor_Reset;
                }

                // Set the editor control.
                this.editorControl = value;

                // Attach the events to the new editor if it exists.
                if (this.Editor != null)
                {
                    this.EditorControl.Editor.Zero.AddressChanged += new EventHandler(Editor_AddressChanged);
                    this.EditorControl.Editor.DataReset += new EventHandler(Editor_Reset);
                    this.EditorControl.Editor.MapReset += new EventHandler(Editor_Reset);
                    this.EditorControl.Editor.VisibleSizeChanged += new EventHandler(Editor_Reset);

                    if (this.SBS == SBS_HORZ)
                        this.EditorControl.Editor.HScrollEndChanged += new EventHandler(Editor_Reset);
                    else
                        this.EditorControl.Editor.VScrollEndChanged += new EventHandler(Editor_Reset);
                }

                // Set the properties of the scroll bar for the new editor.
                Reset();
            }
        }
        
        /// <summary>
        /// Gets the <see cref="Editor"/> associated with the <see cref="EditorControl"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected Editor Editor
        {
            get
            {
                if (this.EditorControl == null)
                    return null;
                return this.EditorControl.Editor;
            }
        }

        /// <summary>
        /// Gets the scroll bar style of this <see cref="EditorScrollBar"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private int SBS
        {
            get { return this.CreateParams.Style & (SBS_HORZ | SBS_VERT); }
        }

        /// <summary>
        /// Gets or sets the <see cref="ScrollEnd"/> value of the <see cref="EditorScrollBar"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScrollEnd ScrollEnd
        {
            get
            {
                if (this.SBS == SBS_HORZ)
                    return this.Editor.HScrollEnd;
                else
                    return this.Editor.VScrollEnd;
            }
            set
            {
                if (this.SBS == SBS_HORZ)
                    this.Editor.HScrollEnd = value;
                else
                    this.Editor.VScrollEnd = value;
            }
        }

        /// <summary>
        /// Gets either view width or view height of the <see cref="Editor"/>
        /// depending on which style <see cref="EditorScrollBar"/> this is.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private int ViewN
        {
            get
            {
                if (this.SBS == SBS_HORZ)
                    return this.Editor.ViewSize.Width;
                else
                    return this.Editor.ViewSize.Height;
            }
        }

        /// <summary>
        /// Gets either map width or view height of the <see cref="Editor"/>
        /// depending on which style <see cref="EditorScrollBar"/> this is.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private int MapN
        {
            get
            {
                if (this.SBS == SBS_HORZ)
                    return this.Editor.MapSize.Width;
                else
                    return this.Editor.MapSize.Height;
            }
        }

        /// <summary>
        /// Gets or sets either zero x or y of the <see cref="Editor"/> depending
        /// on which style <see cref="EditorScrollBar"/> this is.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private int ZeroN
        {
            get
            {
                if (this.SBS == SBS_HORZ)
                    return this.Editor.Zero.X;
                else
                    return this.Editor.Zero.Y;
            }
            set
            {
                if (this.SBS == SBS_HORZ)
                    this.Editor.Zero.X = value;
                else
                    this.Editor.Zero.Y = value;
            }
        }

        /// <summary>
        /// Gets either the horizontal or vertical scroll limit of the
        /// <see cref="Editor"/> depending on which style
        /// <see cref="EditorScrollBar"/> this is.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private int NScrollLimit
        {
            get
            {
                if (this.SBS == SBS_HORZ)
                    return this.Editor.HScrollLimit;
                else
                    return this.Editor.VScrollLimit;
            }
        }

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the
        /// scroll box on the scroll bar control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int Value
        {
            get { return base.Value; }
            protected set { base.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="Value"/>
        /// property when the scroll box is moved a small distance.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The assigned value is less than 0.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int SmallChange
        {
            get { return base.SmallChange; }
            protected set { base.SmallChange = value; }
        }
        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="Value"/>
        /// property when the scroll box is moved a large distance.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The assigned value is less than 0.
        /// </exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int LargeChange
        {
            get { return base.LargeChange; }
            protected set { base.LargeChange = value; }
        }
        /// <summary>
        /// Gets or sets the lower limit of values of the scrollable range.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int Minimum
        {
            get { return base.Minimum; }
            protected set { base.Minimum = value; }
        }
        /// <summary>
        /// Gets or sets the upper limit of values of the scrollable range.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int Maximum
        {
            get { return base.Maximum; }
            protected set { base.Maximum = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets standard properties for the <see cref="EditorScrollBar"/>.
        /// </summary>
        public void Reset()
        {
            // Disable the scroll bar if no editor is present.
            if (this.Editor == null)
            {
                this.Enabled = false;
                return;
            }

            if (this.Enabled = this.ViewN < this.MapN + this.NScrollLimit)
                SetScrollParameters();
        }

        /// <summary>
        /// Sets the scrolling parameters of the <see cref="EditorScrollBar"/>.
        /// </summary>
        protected virtual void SetScrollParameters()
        {
            // We have to check for Range > 0 or else visual studio throws designer errors
            if (this.Enabled && this.Editor != null)
            {
                this.SmallChange = 1;
                this.LargeChange = this.ViewN;
                this.Minimum = 0;
                this.Maximum = this.MapN + this.NScrollLimit - 1;
                this.Value = this.ZeroN;
            }
        }

        /// <summary>
        /// Raises the <see cref="ScrollBar.Scroll"/> event.
        /// </summary>
        /// <param name="se">
        /// A <see cref="ScrollEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            ScrollEditor(se);
            base.OnScroll(se);
        }

        /// <summary>
        /// Sets the <see cref="ZeroPoint"/> of the <see cref="Editor"/>.
        /// </summary>
        /// <param name="se">
        /// A <see cref="ScrollEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ScrollEditor(ScrollEventArgs se)
        {
            if (this.Editor != null)
                if (se.NewValue != se.OldValue)
                    this.ZeroN = se.NewValue;
        }

        private void Editor_AddressChanged(object sender, EventArgs e)
        {
            SetScrollParameters();
        }

        private void Editor_Reset(object sender, EventArgs e)
        {
            Reset();
        }
        #endregion
    }
}