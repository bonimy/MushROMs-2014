using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides a specified <see cref="ContextMenuStrip"/> that attaches to an <see cref="EditorForm"/>.
    /// </summary>
    public class EditorContextMenuStrip : ContextMenuStrip
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Cut' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmCut
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Copy' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmCopy
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Paste' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmPaste
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Delete' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmDelete
        {
            get { return null; }
        }
        #endregion
    }
}
