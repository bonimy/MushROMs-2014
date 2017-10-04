using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents a Windows text box control that accepts only integer
    /// inputs.
    /// </summary>
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [Description("A text box that only accepts integer values.")]
    public class IntegerTextBox : TextBox , IIntegerComponent
    {
        #region Events
        /// <summary>
        /// Occurs when the integer value in the text box has changed.
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Constant and read-only variables
        /// <summary>
        /// The fallback value of the <see cref="IntegerTextBox"/>.
        /// </summary>
        private const int FallbackValue = 0;
        /// <summary>
        /// The fallback option to allow hex.
        /// </summary>
        private const bool FallbackAllowHex = false;
        /// <summary>
        /// The fallback option to allow negative.
        /// </summary>
        private const bool FallbackAllowNegative = false;
        /// <summary>
        /// The fallback value of the <see cref="CharacterCasing"/>.
        /// </summary>
        private const CharacterCasing FallbackCharacterCasing = CharacterCasing.Upper;
        #endregion

        #region Variables
        /// <summary>
        /// A value determining whether the text box string is parsed as hexadecimal.
        /// </summary>
        private bool hex;
        /// <summary>
        /// A value determining whether negative numbers are allowed.
        /// </summary>
        private bool neg;
        /// <summary>
        /// The value represented by the text box.
        /// </summary>
        private int value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value determining whether the text box string is parsed as hexadecimal.
        /// </summary>
        [Category("Editor")]
        [DefaultValue(FallbackAllowHex)]
        [Description("Determines whether the control reads hexadecimal values or decimal.")]
        public bool Hexadecimal
        {
            get { return this.hex; }
            set { this.hex = value; SetValue(this.value); }
        }

        /// <summary>
        /// Gets or sets a value determining whether negative numbers are allowed.
        /// </summary>
        [Category("Editor")]
        [DefaultValue(FallbackAllowNegative)]
        [Description("Determines whether negative numbers are valid input.")]
        public bool AllowNegative
        {
            get { return this.neg; }
            set { this.neg = value; SetValue(this.value); }
        }

        /// <summary>
        /// Gets or sets the value represented by the text box.
        /// </summary>
        [Category("Editor")]
        [DefaultValue(FallbackValue)]
        [Description("The value written to the text box.")]
        public int Value
        {
            get { return this.value; }
            set { SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the character casing of the string in the text box.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(FallbackCharacterCasing)]
        [Description("Indicates if all characters should be left alone or converted to uppercase or lowercase.")]
        public new CharacterCasing CharacterCasing
        {
            get { return base.CharacterCasing; }
            set { base.CharacterCasing = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerTextBox"/> class.
        /// </summary>
        public IntegerTextBox()
        {
            this.hex = FallbackAllowHex;
            this.neg = FallbackAllowNegative;
            this.value = FallbackValue;
            this.Text = FallbackValue.ToString();
            this.CharacterCasing = FallbackCharacterCasing;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the value represented by the text box.
        /// </summary>
        /// <param name="value">
        /// The value to assign.
        /// </param>
        private void SetValue(int value)
        {
            // Set the value.
            this.value = value;

            // Make value positive if negative is not allowed.
            if (!this.neg && this.value < 0)
                this.value *= -1;

            // Stringize the value.
            this.Text = value.ToString(hex ? "X" : string.Empty);

            OnValueChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="ValueChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyPress"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyPressEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Handle key data first (for key handling)
            base.OnKeyPress(e);
            if (e.Handled)
                return;

            // Onlt accept valid number characters and formatters
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                return;
            else if (neg && e.KeyChar == '-' && this.SelectionStart == 0 && !this.Text.Contains("-"))
                return;
            else if (hex && e.KeyChar >= 'a' && e.KeyChar <= 'f')
                return;
            else if (hex && e.KeyChar >= 'A' && e.KeyChar <= 'F')
                return;
            else if (e.KeyChar == '\b')
                return;
            else
                e.Handled = true;
        }

        /// <summary>
        /// Raises the <see cref="Control.TextChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnTextChanged(EventArgs e)
        {
            // Save original value.
            int old = this.value;

            // Parse new value.
            int.TryParse(this.Text,
                this.hex ? NumberStyles.AllowHexSpecifier : (this.neg ? NumberStyles.AllowLeadingSign : NumberStyles.None),
                CultureInfo.InvariantCulture, out this.value);

            // Only raise event if value changed.
            if (this.value != old)
                OnValueChanged(EventArgs.Empty);

            base.OnTextChanged(e);
        }
        #endregion
    }
}