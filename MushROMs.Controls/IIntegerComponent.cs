using System;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides an interface to expose integer components
    /// </summary>
    public interface IIntegerComponent
    {
        /// <summary>
        /// Occurs when <see cref="Value"/> changes.
        /// </summary>
        event EventHandler ValueChanged;

        /// <summary>
        /// Gets or sets a numeric value associated with the component.
        /// </summary>
        int Value { get; set; }
    }
}