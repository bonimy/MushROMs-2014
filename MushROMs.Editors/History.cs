using System;
using System.Collections.Generic;

namespace MushROMs.Editors
{
    /// <summary>
    /// Represents a collection whose items hold undo and redo history
    /// data for a modifiable object.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in the list.
    /// </typeparam>
    public class History<T>
    {
        #region Events
        /// <summary>
        /// Occurs when a data element is added to the undo list.
        /// </summary>
        public event EventHandler UndoDataAdded;
        /// <summary>
        /// Occurs when a data element is added to the redo list.
        /// </summary>
        public event EventHandler RedoDataAdded;

        /// <summary>
        /// Occurs when an undo method is called.
        /// </summary>
        public event EventHandler Undo;
        /// <summary>
        /// Occurs when a redo method is called.
        /// </summary>
        public event EventHandler Redo;
        #endregion

        #region Fields
        /// <summary>
        /// The index value of the <see cref="History&lt;T&gt;"/> data
        /// when the last save method was called.
        /// </summary>
        private int saveIndex;
        /// <summary>
        /// The current index value of the <see cref="History&lt;T&gt;"/>.
        /// </summary>
        private int historyIndex;
        /// <summary>
        /// A value determining whether <see cref="Unsaved"/> should be
        /// forced to be true.
        /// </summary>
        private bool forceUnsaved;
        /// <summary>
        /// A collection containing all the undo data.
        /// </summary>
        private List<T> undo;
        /// <summary>
        /// A collection containing all the redo data.
        /// </summary>
        private List<T> redo;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the index value of the <see cref="History&lt;T&gt;"/>
        /// data when the last save method was called.
        /// </summary>
        public int SaveIndex
        {
            get { return this.saveIndex; }
        }

        /// <summary>
        /// Gets or sets a value determining whether <see cref="Unsaved"/>
        /// should be forced to be true.
        /// </summary>
        public bool ForceUnsaved
        {
            get { return this.forceUnsaved; }
            set { this.forceUnsaved = value; }
        }

        /// <summary>
        /// Gets a value determining whether the data is unsaved.
        /// </summary>
        public bool Unsaved
        {
            get { return this.historyIndex != this.saveIndex || this.forceUnsaved; }
        }

        /// <summary>
        /// Gets a value that determines whether an undo can be done.
        /// </summary>
        public bool CanUndo
        {
            get { return this.historyIndex > 0; }
        }

        /// <summary>
        /// Gets a value that determines whether a redo can be done.
        /// </summary>
        public bool CanRedo
        {
            get { return this.historyIndex < this.undo.Count; }
        }

        /// <summary>
        /// Gets the current undo data element.
        /// </summary>
        public T CurrentUndoData
        {
            get { return this.undo[this.historyIndex]; }
        }

        /// <summary>
        /// Gets the current redo data element.
        /// </summary>
        public T CurrentRedoData
        {
            get { return this.redo[this.historyIndex - 1]; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="History&lt;T&gt;"/> class.
        /// </summary>
        public History()
        {
            Reset();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="History&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="forceUnsaved">
        /// When true, the data is assumed unsaved.
        /// </param>
        public History(bool forceUnsaved)
        {
            Reset(forceUnsaved);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets the undo and redo data.
        /// </summary>
        public void Reset()
        {
            Reset(false);
        }

        /// <summary>
        /// Rests the undo and redo data.
        /// </summary>
        /// <param name="forceUnsaved">
        /// When true, the data is assumed unsaved.
        /// </param>
        public void Reset(bool forceUnsaved)
        {
            this.saveIndex =
            this.historyIndex = 0;

            this.undo = new List<T>();
            this.redo = new List<T>();

            this.forceUnsaved = forceUnsaved;
        }

        /// <summary>
        /// Sets <see cref="Unsaved"/> to true in this step of the
        /// undo/redo list.
        /// </summary>
        public void SetSaveIndex()
        {
            this.saveIndex = this.historyIndex;
            this.forceUnsaved = false;
        }

        /// <summary>
        /// Adds a data element to the undo list.
        /// </summary>
        /// <param name="data">
        /// The data element to add.
        /// </param>
        public void AddUndoData(T data)
        {
            if (this.historyIndex < this.undo.Count)
            {
                this.undo.RemoveRange(this.historyIndex, this.undo.Count - this.historyIndex);
                this.redo.RemoveRange(this.historyIndex, this.redo.Count - this.historyIndex);
            }

            this.undo.Add(data);

            OnUndoDataAdded(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="UndoDataAdded"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnUndoDataAdded(EventArgs e)
        {
            if (UndoDataAdded != null)
                UndoDataAdded(this, e);
        }

        /// <summary>
        /// Adds a data element to the redo list.
        /// </summary>
        /// <param name="data">
        /// The data element to add.
        /// </param>
        public void AddRedoData(T data)
        {
            this.redo.Add(data);
            this.historyIndex++;

            OnRedoDataAdded(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="RedoDataAdded"/> event
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnRedoDataAdded(EventArgs e)
        {
            if (RedoDataAdded != null)
                RedoDataAdded(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Undo"/> event.
        /// </summary>
        public void InvokeUndo()
        {
            if (this.historyIndex == 0)
                return;

            this.historyIndex--;
            OnUndo(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="Undo"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnUndo(EventArgs e)
        {
            if (Undo != null)
                Undo(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Redo"/> event.
        /// </summary>
        public void InvokeRedo()
        {
            if (this.historyIndex == this.redo.Count)
                return;

            this.historyIndex++;
            OnRedo(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="Redo"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnRedo(EventArgs e)
        {
            if (Redo != null)
                Redo(this, e);
        }
        #endregion
    }
}