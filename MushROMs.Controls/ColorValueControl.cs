using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MushROMs.Editors;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents a control that stores a <see cref="Color"/> value and prompts the user with
    /// a <see cref="ColorDialog"/> to modify the color when clicked.
    /// </summary>
    [DefaultEvent("ColorValueChanged")]
    [DefaultProperty("SelectedColor")]
    [Description("Provides a control for representing and manipulating a color value.")]
    public unsafe class ColorValueControl : DrawControl
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback client width of the control.
        /// This field is constant.
        /// </summary>
        private const int FallbackClientWidth = 0x10;
        /// <summary>
        /// The fallback client height of the control.
        /// This field is constant.
        /// </summary>
        private const int FallbackClientHeight = FallbackClientWidth;
        /// <summary>
        /// The fallback color of the control.
        /// This field is readonly.
        /// </summary>
        private static readonly Color FallbackColor = Color.Empty;
        #endregion

        #region Fields
        /// <summary>
        /// The current <see cref="Color"/> value of the control.
        /// </summary>
        private Color selectedColor;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current <see cref="Color"/> value of the control.
        /// </summary>
        [Category("Editor")]
        [DefaultValue("Black")]
        [Description("The selected color for the control.")]
        public Color SelectedColor
        {
            get { return this.selectedColor; }
            set { this.selectedColor = value; OnColorValueChanged(EventArgs.Empty); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorValueControl"/> class.
        /// </summary>
        public ColorValueControl()
        {
            // Set default size of the control.
            this.ClientWidth = FallbackClientWidth;
            this.ClientHeight = FallbackClientHeight;

            // Set the drawing events.
            this.WritePixels += new EventHandler(ColorValueControl_WritePixels);
            this.Paint += new PaintEventHandler(ColorValueControl_Paint);

            // Set the default selected color.
            this.SelectedColor = FallbackColor;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="Control.Click"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnClick(EventArgs e)
        {
            // Show a color dialog to edit the color.
            ColorDialog dlg = new ColorDialog();
            dlg.FullOpen = true;
            dlg.Color = this.selectedColor;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.SelectedColor = dlg.Color;

            base.OnClick(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.KeyDown"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                OnClick(e);

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.EnabledChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // The control is grayed out when disabled.
            this.Invalidate();

            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.GotFocus"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnGotFocus(EventArgs e)
        {
            // The control shows a rectangle when focused.
            this.Invalidate();

            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.LostFocus"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnLostFocus(EventArgs e)
        {
            // Clear the rectangle when lost focus.
            this.Invalidate();

            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the <see cref="ColorValueChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnColorValueChanged(EventArgs e)
        {
            // Redraw the control when the color changes.
            Redraw();

            if (ColorValueChanged != null)
                ColorValueChanged(this, e);
        }

        private void ColorValueControl_WritePixels(object sender, EventArgs e)
        {
            // Get the color
            uint color = (uint)this.SelectedColor.ToArgb();

            // Gray out the control if it is disabled
            if (!this.Enabled)
                color = (uint)((ExpandedColor)this.SelectedColor).LumaGrayScale().ToArgb();

            // Draw the control with the solid color.
            uint* pixels = (uint*)this.Scan0.Data;
            for (int i = this.ClientWidth * this.ClientWidth; --i >= 0; )
                pixels[i] = color;
        }

        private void ColorValueControl_Paint(object sender, PaintEventArgs e)
        {
            // Make sure control is focused.
            if (!this.Focused)
                return;

            // These two pens make a black and white dotted line.
            Pen p1 = new Pen(Color.Black, 1);
            p1.DashStyle = DashStyle.Dot;
            Pen p2 = new Pen(Color.White, 1);
            p2.DashStyle = DashStyle.Dot;
            p2.DashOffset = 1;

            // Draw a black and white dotted rectangle around the control when it is focused.
            Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            e.Graphics.DrawRectangle(p1, r);
            e.Graphics.DrawRectangle(p2, r);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the selected color value of the control changes.
        /// </summary>
        [Category("Editor")]
        [Description("Occurs when the selected color value of the control changes.")]
        public event EventHandler ColorValueChanged;
        #endregion
    }
}