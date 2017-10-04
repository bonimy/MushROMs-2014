using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides a specified <see cref="ToolStrip"/> that attaches to a <see cref="MenuComponents"/> class.
    /// </summary>
    public class EditorToolStrip : ToolStrip
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'New' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbNew
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Open' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbOpen
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Save' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbSave
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Save All' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbSaveAll
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Undo' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbUndo
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Redo' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbRedo
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Cut' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbCut
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Copy' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbCopy
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripButton"/> that prompts the 'Paste' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripButton TsbPaste
        {
            get { return null; }
        }
        #endregion
    }
}