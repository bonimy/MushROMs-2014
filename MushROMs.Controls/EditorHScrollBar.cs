using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents a standard horizontal scroll bar for an
    /// <see cref="EditorControl"/>.
    /// </summary>
    public class EditorHScrollBar : EditorScrollBar
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
                    this.EditorControl.EditorHScrollBar = this;
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
                cp.Style |= EditorScrollBar.SBS_HORZ;
                return cp;
            }
        }

        /// <summary>
        /// The dedault <see cref="Size"/> of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(80, SystemInformation.HorizontalScrollBarHeight); }
        }
        #endregion
    }
}