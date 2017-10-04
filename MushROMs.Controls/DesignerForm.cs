using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// A <see cref="Form"/> that acts as a base for graphical
    /// design.
    /// </summary>
    public partial class DesignerForm : Form
    {
        #region Variables
        /// <summary>
        /// A set of <see cref="Keys"/> combinations that are ignored as input <see cref="Keys"/>.
        /// </summary>
        private Keys[] overrideInputKeys;

        /// <summary>
        /// A value that determines whether the <see cref="EditorForm"/> is
        /// resizing.
        /// </summary>
        private bool resizing;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value that determines whether the <see cref="EditorForm"/> is
        /// resizing.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Resizing
        {
            get { return this.resizing; }
        }

        /// <summary>
        /// Gets or sets a set of <see cref="Keys"/> combinations that are ignored as input <see cref="Keys"/>.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("Provides a collection of keys to override as input keys.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Keys[] OverrideInputKeys
        {
            get { return this.overrideInputKeys; }
            set { this.overrideInputKeys = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorForm"/> class.
        /// </summary>
        public DesignerForm()
        {
            this.KeyPreview = true;

            // Initialize override input keys
            this.overrideInputKeys = new Keys[EditorControl.FallbackOverrideInputKeys.Length];
            for (int i = EditorControl.FallbackOverrideInputKeys.Length; --i >= 0; )
                this.overrideInputKeys[i] = EditorControl.FallbackOverrideInputKeys[i];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="Form.ResizeBegin"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnResizeBegin(EventArgs e)
        {
            this.resizing = true;
            base.OnResizeBegin(e);
        }

        /// <summary>
        /// Raises the <see cref="Form.ResizeEnd"/> event
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnResizeEnd(EventArgs e)
        {
            this.resizing = false;
            base.OnResizeEnd(e);
        }

        /// <summary>
        /// Determines whether the specified key is a regular input key or a special
        /// key that requires preprocessing.
        /// </summary>
        /// <param name="keyData">
        /// One of the <see cref="Keys"/> values.
        /// </param>
        /// <returns>
        /// true if the specified key is a regular input key; otherwise, false.
        /// </returns>
        protected override bool IsInputKey(Keys keyData)
        {
            if (this.overrideInputKeys != null)
                for (int i = this.overrideInputKeys.Length; --i >= 0; )
                    if (this.overrideInputKeys[i] == keyData)
                        return true;

            return base.IsInputKey(keyData);
        }

        /// <summary>
        /// Processes a dialog box key.
        /// </summary>
        /// <param name="keyData">
        /// One of the <see cref="Keys"/> values that represents the key to process.
        /// </param>
        /// <returns>
        /// true if the keystroke was processed and consumed by the control; otherwise,
        /// false to allow further processing.
        /// </returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.overrideInputKeys != null)
                for (int i = this.overrideInputKeys.Length; --i >= 0; )
                    if (this.overrideInputKeys[i] == keyData)
                        return false;

            return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}