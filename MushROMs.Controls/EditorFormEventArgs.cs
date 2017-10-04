using System;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents the method that will handle the <see cref="EditorMdiForm"/> events
    /// pertaining to its child <see cref="EditorForm"/>s.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// An <see cref="EditorFormEventArgs"/> that contains the event data.
    /// </param>
    public delegate void EditorFormEventHandler(object sender, EditorFormEventArgs e);

    /// <summary>
    /// Provides data for <see cref="EditorMdiForm"/> events.
    /// </summary>
    public class EditorFormEventArgs : EventArgs
    {
        /// <summary>
        /// The <see cref="EditorForm"/> of this event.
        /// </summary>
        private EditorForm editorForm;

        /// <summary>
        /// Gets the <see cref="EditorForm"/> of this event.
        /// </summary>
        public EditorForm EditorForm
        {
            get { return this.editorForm; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorFormEventArgs"/> class.
        /// </summary>
        /// <param name="form">
        /// The <see cref="EditorForm"/> of the event.
        /// </param>
        public EditorFormEventArgs(EditorForm form)
        {
            this.editorForm = form;
        }
    }
}