using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    partial class DrawControl
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback <see cref="OverrideInputKeys"/> value.
        /// This field is read-only.
        /// </summary>
        internal static readonly Keys[] FallbackOverrideInputKeys = new Keys[] {
            Keys.Up,    Keys.Up    | Keys.Shift, Keys.Up    | Keys.Control, Keys.Up    | Keys.Shift | Keys.Control,
            Keys.Left,  Keys.Left  | Keys.Shift, Keys.Left  | Keys.Control, Keys.Left  | Keys.Shift | Keys.Control,
            Keys.Down,  Keys.Down  | Keys.Shift, Keys.Down  | Keys.Control, Keys.Down  | Keys.Shift | Keys.Control,
            Keys.Right, Keys.Right | Keys.Shift, Keys.Right | Keys.Control, Keys.Right | Keys.Shift | Keys.Control };
        #endregion

        #region Fields
        /// <summary>
        /// The current combination of <see cref="Keys"/> that are pressed.
        /// </summary>
        private static Keys currentKeys;
        /// <summary>
        /// The previous combination of <see cref="Keys"/> that were pressed.
        /// </summary>
        private static Keys previousKeys;
        /// <summary>
        /// The combination of <see cref="Keys"/> that were just now pressed.
        /// </summary>
        private static Keys activeKeys;

        /// <summary>
        /// A set of <see cref="Keys"/> combinations that are ignored as input <see cref="Keys"/>.
        /// </summary>
        private Keys[] overrideInputKeys;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current combination of <see cref="Keys"/> that are pressed.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Keys CurrentKeys
        {
            get { return EditorControl.currentKeys; }
        }

        /// <summary>
        /// Gets the previous combination of <see cref="Keys"/> that were pressed.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Keys PreviousKeys
        {
            get { return EditorControl.previousKeys; }
        }

        /// <summary>
        /// Gets the combination of <see cref="Keys"/> that were just now pressed.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Keys ActiveKeys
        {
            get { return EditorControl.activeKeys; }
        }

        /// <summary>
        /// Gets a value the determines whether the Control modifier key is being pressed.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool ControlKeyHeld
        {
            get { return (Control.ModifierKeys & Keys.Control) == Keys.Control; }
        }

        /// <summary>
        /// Gets a value the determines whether the Shift modifier key is being pressed.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool ShiftKeyHeld
        {
            get { return (Control.ModifierKeys & Keys.Shift) == Keys.Shift; }
        }

        /// <summary>
        /// Gets a value the determines whether the Alt modifier key is being pressed.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool AltKeyHeld
        {
            get { return (Control.ModifierKeys & Keys.Alt) == Keys.Alt; }
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

        #region Methods
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
            // Determine which additional keys will be input keys.
            if (this.OverrideInputKeys != null)
                for (int i = this.OverrideInputKeys.Length; --i >= 0; )
                    if (this.OverrideInputKeys[i] == keyData)
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
            if (this.OverrideInputKeys != null)
                for (int i = this.OverrideInputKeys.Length; --i >= 0; )
                    if (this.OverrideInputKeys[i] == keyData)
                        return false;

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyDown"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Update current, previous, and active keys.
            EditorControl.previousKeys = EditorControl.currentKeys;
            EditorControl.currentKeys = e.KeyCode;
            EditorControl.activeKeys = EditorControl.currentKeys & ~EditorControl.previousKeys;

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyUp"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            // Update current, previous, and active keys.
            EditorControl.previousKeys = EditorControl.currentKeys;
            EditorControl.currentKeys &= ~e.KeyCode;
            EditorControl.activeKeys = Keys.None;

            base.OnKeyUp(e);
        }
        #endregion
    }
}
