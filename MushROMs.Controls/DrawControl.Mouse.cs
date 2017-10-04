using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Editors;

namespace MushROMs.Controls
{
    partial class DrawControl
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback <see cref="ProcessMouseOnChange"/> value.
        /// This field is constant.
        /// </summary>
        private const bool FallbackProcessMouseOnChange = true;

        /// <summary>
        /// The fallback <see cref="ProcessMouseInRange"/> value.
        /// This field is constant.
        /// </summary>
        private const bool FallbackProcessMouseInRange = true;

        /// <summary>
        /// Represents a <see cref="Point"/> that is out of range in
        /// the <see cref="EditorControl"/>.
        /// This field is read-only.
        /// </summary>
        public static readonly Point MouseOutOfRange = new Point(-1, -1);

        /// <summary>
        /// Defines the delta threshold of a mouse wheel rotation.
        /// This field is constant.
        /// </summary>
        public const int MouseWheelThreshold = 120;
        #endregion

        #region Fields
        /// <summary>
        /// The current location of the mouse in the <see cref="DrawControl"/>.
        /// </summary>
        private Point currentMousePoint;
        /// <summary>
        /// The previous location of the mouse in the <see cref="DrawControl"/>.
        /// </summary>
        private Point previousMousePoint;
        /// <summary>
        /// The current <see cref="MouseButtons"/> of the mouse in
        /// the <see cref="DrawControl"/>.
        /// </summary>
        private MouseButtons currentMouseButtons;
        /// <summary>
        /// The previous <see cref="MouseButtons"/> of the mouse in
        /// the <see cref="DrawControl"/>.
        /// </summary>
        private MouseButtons previousMouseButtons;
        /// <summary>
        /// The <see cref="MouseButtons"/> pressed at this moment in
        /// the <see cref="DrawControl"/>.
        /// </summary>
        private MouseButtons activeMouseButtons;

        /// <summary>
        /// A value that determines whether mouse handling will only
        /// occur if the mouse is moving.
        /// </summary>
        private bool processMouseOnChange;
        /// <summary>
        /// A value that determines whether mouse handling will only
        /// occur if the mouse in in the editor client region.
        /// </summary>
        private bool processMouseInRange;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value that determines whether mouse handling
        /// will only occur if the mouse is moving.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("Determines whether mouse handling will occur only while the mouse is moving.")]
        [DefaultValue(FallbackProcessMouseOnChange)]
        public bool ProcessMouseOnChange
        {
            get { return this.processMouseOnChange; }
            set { this.processMouseOnChange = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether mouse handling
        /// will only occur if the mouse in in the editor client region.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("Determines whether mouse handling will occur only while the mouse is in the client area.")]
        [DefaultValue(FallbackProcessMouseInRange)]
        public bool ProcessMouseInRange
        {
            get { return this.processMouseInRange; }
            set { this.processMouseInRange = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Ignore mouse processing unless moving if told to do so.
            if (this.ProcessMouseOnChange && this.currentMousePoint == e.Location)
                return;

            // Ignore mouse processing unless in range if told to do so.
            if (this.ProcessMouseInRange && !MathHelper.IsInBoundary(this.Location, this.ClientRectangle))
                return;

            // Update current and previous mouse locations.
            this.previousMousePoint = this.currentMousePoint;
            this.currentMousePoint = e.Location;

            OnInRangeMouseMove(e);

            // Continue standard mouse processing.
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="InRangeMouseMove"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnInRangeMouseMove(MouseEventArgs e)
        {
            if (InRangeMouseMove != null)
                InRangeMouseMove(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseLeave"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Update previous mouse location
            this.previousMousePoint = this.currentMousePoint;

            // Set current mouse location to be out of range.
            this.currentMousePoint = MouseOutOfRange;

            // Continue standard mouse processing.
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Update current, previous, and active mouse buttons.
            this.previousMouseButtons = this.currentMouseButtons;
            this.currentMouseButtons = e.Button;
            this.activeMouseButtons = this.currentMouseButtons & ~this.previousMouseButtons;

            // Continue standard mouse processing.
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raise the <see cref="Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Update the current and previous mouse buttons.
            this.previousMouseButtons = this.currentMouseButtons;
            this.currentMouseButtons &= ~e.Button;
            this.activeMouseButtons = MouseButtons.None;

            // Continue standard mouse processing.
            base.OnMouseUp(e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse wheel moves while the control has focus.
        /// </summary>
        [Browsable(true)]
        [Category("Mouse")]
        [Description("Occurs when the mouse wheel moves while the control has focus.")]
        public new event MouseEventHandler MouseWheel
        {
            add { base.MouseWheel += value; }
            remove { base.MouseWheel -= value; }
        }

        /// <summary>
        /// Occurs when the mouse moves while in the defined valid range.
        /// </summary>
        [Category("Mouse")]
        [Description("Occurs when the mouse moves while in the defined valid range.")]
        public event MouseEventHandler InRangeMouseMove;
        #endregion
    }
}
