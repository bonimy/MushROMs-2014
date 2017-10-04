using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides a specified <see cref="MenuStrip"/> that attaches to a <see cref="MenuComponents"/> class.
    /// </summary>
    public class EditorMenuStrip : MenuStrip
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'New' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmNew
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Open' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmOpen
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that contains a list of recently
        /// opened files.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmOpenRecent
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Save' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmSave
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Save As' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmSaveAs
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Save All' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmSaveAll
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Exit' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmExit
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Undo' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmUndo
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Redo' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmRedo
        {
            get { return null; }
        }

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

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Go To' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmGoTo
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Customize' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmCustomize
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Cascade' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmCascade
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Tile Vertical' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmTileVertical
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Tile Horizontal' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmTileHorizontal
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Arrange Icons' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmArrangeIcons
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="ToolStripMenuItem"/> that prompts the 'Close All' command.
        /// </summary>
        /// <remarks>
        /// If null, then it is assumed the attached command does not need to exist or
        /// is implemented elsewhere.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ToolStripMenuItem TsmCloseAll
        {
            get { return null; }
        }
        #endregion
    }
}