using System;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents a track bar that links to another <see cref="IIntegerComponent"/>.
    /// </summary>
    public class LinkedTrackBar : TrackBar, IIntegerComponent
    {
        /// <summary>
        /// The <see cref="IIntegerComponent"/> that links to this
        /// <see cref="LinkedTrackBar"/>.
        /// </summary>
        private IIntegerComponent integerComponent;

        /// <summary>
        /// Gets or sets the <see cref="IIntegerComponent"/> that links to this
        /// <see cref="LinkedTrackBar"/>.
        /// </summary>
        public IIntegerComponent IntegerComponent
        {
            get { return this.integerComponent; }
            set
            {
                // Do not link to itself
                if (this == value)
                    return;

                // Avoid redundant setting.
                if (this.integerComponent == value)
                    return;

                // Remove event from last component
                if (this.integerComponent != null)
                    this.integerComponent.ValueChanged -= NumericControl_ValueChanged;

                // Observe value of component
                if ((this.integerComponent = value) != null)
                    this.integerComponent.ValueChanged += new EventHandler(NumericControl_ValueChanged);
            }
        }

        /// <summary>
        /// Raises the <see cref="TrackBar.ValueChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnValueChanged(EventArgs e)
        {
            this.integerComponent.Value = this.Value;
            base.OnValueChanged(e);
        }

        private void NumericControl_ValueChanged(object sender, EventArgs e)
        {
            if (this.integerComponent.Value >= this.Minimum &&
                this.integerComponent.Value <= this.Maximum)
                this.Value = this.integerComponent.Value;
        }
    }
}