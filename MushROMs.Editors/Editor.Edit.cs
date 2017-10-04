/* Deals with the data modification of the editor.
 * Standard edit functions are copy, paste, delete,
 * undo, and redo. Some of these methods are abstract
 * and will need to be implemented in derived classes.
 * */

using System;
using System.ComponentModel;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    partial class Editor
    {
        #region Fields
        /// <summary>
        /// The last edit action done to the <see cref="Editor"/> data.
        /// </summary>
        private IEditorData lastEdit;

        /// <summary>
        /// The undo and redo data of the <see cref="Editor"/>.
        /// </summary>
        private History<IEditorData> history;
        #endregion

        #region Properties
        /// <summary>
        /// The last edit action done to the <see cref="Editor"/> data.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IEditorData LastEdit
        {
            get { return this.lastEdit; }
            protected set { this.lastEdit = value; }
        }

        /// <summary>
        /// Gets the undo and redo data of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public History<IEditorData> History
        {
            get { return this.history; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Undoes the last data modification of <see cref="Editor"/>.
        /// </summary>
        public virtual void Undo()
        {
            this.History.InvokeUndo();
        }

        private void History_Undo(object sender, EventArgs e)
        {
            ModifyData(this.History.CurrentUndoData, false, false);
        }

        /// <summary>
        /// Redoes the previous data modification of <see cref="Editor"/>
        /// that was undone.
        /// </summary>
        public virtual void Redo()
        {
            this.History.InvokeRedo();
        }

        private void History_Redo(object sender, EventArgs e)
        {
            ModifyData(this.History.CurrentRedoData, false, false);
        }

        /// <summary>
        /// Creates a copy of the data at the current <see cref="Selection"/>.
        /// </summary>
        /// <returns>
        /// A copy of the data at the current <see cref="Selection"/>.
        /// </returns>
        public virtual IEditorData CreateCopy()
        {
            return CreateCopy(this.Selection);
        }

        /// <summary>
        /// When overridden in a derived class, creates a copy of the data
        /// in <paramref name="selection"/>.
        /// </summary>
        /// <param name="selection">
        /// The <see cref="ISelection"/> of the data.
        /// </param>
        /// <returns>
        /// A copy of the data at <paramref name="selection"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selection"/> is null.
        /// </exception>
        public virtual IEditorData CreateCopy(ISelection selection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, pastes the contents of
        /// <paramref name="data"/> to the activeindex of the
        /// <see cref="Editor"/>.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
        public virtual void Paste(IEditorData data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, deletes or clears the
        /// current <see cref="Selection"/>.
        /// </summary>
        public virtual void DeleteSelection()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes data from <see cref="Editor"/> to an
        /// <see cref="IEditorData"/> interface.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
        protected virtual void WriteCopyData(IEditorData data)
        {
            WriteCopyData(data, data.Selection.StartAddress);
        }

        /// <summary>
        /// When overridden in a derived class, writes data from
        /// an <see cref="IEditorData"/> interface.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write.
        /// </param>
        /// <param name="startAddress">
        /// The starting address in the editor to get the data from.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.  -or-
        /// <paramref name="min"/> is null.
        /// </exception>
        protected virtual void WriteCopyData(IEditorData data, int startAddress)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies <see cref="Data"/>.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write to <see cref="Editor"/>.
        /// </param>
        /// <param name="history">
        /// If true, <paramref name="data"/> will be added to the undo/redo
        /// history.
        /// </param>
        /// <param name="silent">
        /// If true, <see cref="DataModified"/> will not be invoked.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
        public virtual void ModifyData(IEditorData data, bool history, bool silent)
        {
            ModifyData(data, data.Selection.StartAddress, history, silent);
        }

        /// <summary>
        /// Modifies <see cref="Data"/>.
        /// </summary>
        /// <param name="data">
        /// The <see cref="IEditorData"/> to write to <see cref="Editor"/>.
        /// </param>
        /// <param name="startAddress">
        /// The starting address in the editor to get the data from.
        /// </param>
        /// <param name="history">
        /// If true, <paramref name="data"/> will be added to the undo/redo
        /// history.
        /// </param>
        /// <param name="silent">
        /// If true, <see cref="DataModified"/> will not be invoked.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
        public virtual void ModifyData(IEditorData data, int startAddress, bool history, bool silent)
        {
            if (data == null)
                throw new ArgumentNullException(Resources.ErrorDataNull);

            // Add the history data if prompted to do so.
            if (history)
            {
                this.History.AddUndoData(CreateCopy(data.Selection));
                this.History.AddRedoData(data);
            }

            // Write the copy data.
            WriteCopyData(data, startAddress);

            // Ignore invoking data modified event if this is a silent modification.
            if (silent)
                return;

            // Set last edit and invoke data modified event.
            this.lastEdit = data;
            OnDataModified(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="DataModified"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnDataModified(EventArgs e)
        {
            if (DataModified != null)
                DataModified(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when <see cref="Data"/> is modified by the user.
        /// </summary>
        /// <remarks>
        /// <see cref="Data"/> is never implicitly modified by the base
        /// <see cref="Editor"/> class. How it becomes modified, as well
        /// as when to call the event is up to the programmer.
        /// </remarks>
        [Category("Property Changed")]
        [Description("Occurs when the data of the editor is modified.")]
        public event EventHandler DataModified;
        #endregion
    }
}