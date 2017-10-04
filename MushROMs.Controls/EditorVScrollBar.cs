using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents a standard vertical scroll bar for an
    /// <see cref="EditorControl"/>.
    /// </summary>
    public class EditorVScrollBar : EditorScrollBar
    {
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="EditorControl"/> associated with this <see cref="EditorScrollBar"/>.
        /// </summary>
        public override IEditorControl EditorControl
        {
            get { return base.EditorControl; }
            set
            {
                if (this.EditorControl == value)
                    return;

                base.EditorControl = value;

                if (this.EditorControl != null)
                    this.EditorControl.EditorVScrollBar = this;
            }
        }

        /// <returns>
        /// A <see cref="CreateParams"/> that contains the required
        /// creation parameters when the handle t the control is
        /// created.
        /// </returns>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= EditorScrollBar.SBS_VERT;
                return cp;
            }
        }

        /// <summary>
        /// The dedault <see cref="Size"/> of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(SystemInformation.VerticalScrollBarWidth, 80); }
        }

        /// <summary>
        /// Gets a value indicating whether the control's elements are
        /// aligned to support locales using right-to-left fonts.
        /// </summary>
        /// <returns>
        /// The <see cref="RightToLeft.No"/> value.
        /// </returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override RightToLeft RightToLeft
        {
            get { return RightToLeft.No; }
            set { }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="RightToLeft"/> property
        /// changes.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler RightToLeftChanged
        {
            add { base.RightToLeftChanged += value; }
            remove { base.RightToLeftChanged -= value; }
        }
        #endregion
    }
}