using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Editors;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides a specified menu system for an <see cref="EditorMdiForm"/>.
    /// </summary>
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<MenuComponents, Component>))]
    public abstract class MenuComponents : Component
    {
        #region Variables
        /// <summary>
        /// A value that determines whether basic controls have been enabled.
        /// </summary>
        private bool enabled;

        /// <summary>
        /// The <see cref="EditorMdiForm"/> this instance is attached to.
        /// </summary>
        private EditorMdiForm editorMdiForm;

        /// <summary>
        /// The attached <see cref="EditorMenuStrip"/>.
        /// </summary>
        private EditorMenuStrip mnuMain;
        /// <summary>
        /// The attached <see cref="EditorToolStrip"/>.
        /// </summary>
        private EditorToolStrip tlsMain;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="EditorMdiForm"/> of this instance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual EditorMdiForm EditorMdiForm
        {
            get { return this.editorMdiForm; }
            set
            {
                // Avoid redundant setting
                if (this.editorMdiForm == value)
                    return;

                // Remove the events from the last editor form if it extisted.
                if (this.editorMdiForm != null)
                {
                    this.editorMdiForm.EditorFormAdded -= EditorMdiForm_EditorFormAdded;
                    this.editorMdiForm.ActiveEditorChanged -= EditorMdiForm_ActiveEditorChanged;
                }

                // Add the events to the new editor form if it exists.
                if ((this.editorMdiForm = value) != null)
                {
                    this.editorMdiForm.EditorFormAdded += new EditorFormEventHandler(EditorMdiForm_EditorFormAdded);
                    this.editorMdiForm.ActiveEditorChanged += new EditorFormEventHandler(EditorMdiForm_ActiveEditorChanged);
                    this.editorMdiForm.EditorFormRemoved += new EditorFormEventHandler(EditorMdiForm_EditorFormRemoved);
                }
            }
        }

        /// <summary>
        /// Gets the current <see cref="EditorForm"/> of this instance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected EditorForm CurrentEditor
        {
            get { return this.editorMdiForm.CurrentEditor; }
        }

        /// <summary>
        /// Gets the attached <see cref="EditorMenuStrip"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual EditorMenuStrip MenuStrip
        {
            get
            {
                return this.mnuMain;
            }
            set
            {
                // Avoid redundant setting.
                if (this.mnuMain == value)
                    return;

                // Remove events from the last menu if it existed.
                if (this.mnuMain != null)
                {
                    RemoveMenuItemEvent(this.mnuMain.TsmNew, New_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmOpen, Open_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmSave, Save_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmSaveAs, SaveAs_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmSaveAll, SaveAll_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmExit, Exit_Click);

                    RemoveMenuItemEvent(this.mnuMain.TsmUndo, Undo_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmRedo, Redo_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmCut, Cut_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmCopy, Copy_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmPaste, Paste_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmDelete, Delete_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmGoTo, GoTo_Click);

                    RemoveMenuItemEvent(this.mnuMain.TsmCascade, Cascade_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmTileVertical, TileVertical_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmTileHorizontal, TileHorizontal_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmArrangeIcons, ArrangeIcons_Click);
                    RemoveMenuItemEvent(this.mnuMain.TsmCloseAll, CloseAll_Click);

                    RemoveMenuItemEvent(this.mnuMain.TsmCustomize, CustomizeSettings_Click);
                }

                // Add events to the new menu if it exists.
                if ((this.mnuMain = value) != null)
                {
                    AddMenuItemEvent(this.mnuMain.TsmNew, New_Click);
                    AddMenuItemEvent(this.mnuMain.TsmOpen, Open_Click);
                    AddMenuItemEvent(this.mnuMain.TsmSave, Save_Click);
                    AddMenuItemEvent(this.mnuMain.TsmSaveAs, SaveAs_Click);
                    AddMenuItemEvent(this.mnuMain.TsmSaveAll, SaveAll_Click);
                    AddMenuItemEvent(this.mnuMain.TsmExit, Exit_Click);

                    AddMenuItemEvent(this.mnuMain.TsmUndo, Undo_Click);
                    AddMenuItemEvent(this.mnuMain.TsmRedo, Redo_Click);
                    AddMenuItemEvent(this.mnuMain.TsmCut, Cut_Click);
                    AddMenuItemEvent(this.mnuMain.TsmCopy, Copy_Click);
                    AddMenuItemEvent(this.mnuMain.TsmPaste, Paste_Click);
                    AddMenuItemEvent(this.mnuMain.TsmDelete, Delete_Click);
                    AddMenuItemEvent(this.mnuMain.TsmGoTo, GoTo_Click);

                    AddMenuItemEvent(this.mnuMain.TsmCascade, Cascade_Click);
                    AddMenuItemEvent(this.mnuMain.TsmTileVertical, TileVertical_Click);
                    AddMenuItemEvent(this.mnuMain.TsmTileHorizontal, TileHorizontal_Click);
                    AddMenuItemEvent(this.mnuMain.TsmArrangeIcons, ArrangeIcons_Click);
                    AddMenuItemEvent(this.mnuMain.TsmCloseAll, CloseAll_Click);

                    AddMenuItemEvent(this.mnuMain.TsmCustomize, CustomizeSettings_Click);
                }
            }
        }

        /// <summary>
        /// Gets the attached <see cref="EditorToolStrip"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual EditorToolStrip ToolStrip
        {
            get
            {
                return this.tlsMain;
            }
            set
            {
                // Avoid redundant setting.
                if (this.tlsMain == value)
                    return;

                // Remove events from the last toop strip if it existed.
                if (this.tlsMain != null)
                {
                    RemoveMenuItemEvent(this.tlsMain.TsbNew, New_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbOpen, Open_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbSave, Save_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbSaveAll, SaveAll_Click);

                    RemoveMenuItemEvent(this.tlsMain.TsbUndo, Undo_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbRedo, Redo_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbCut, Cut_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbCopy, Copy_Click);
                    RemoveMenuItemEvent(this.tlsMain.TsbPaste, Paste_Click);
                }

                // Add events to the new tool strip if it exists.
                if ((this.tlsMain = value) != null)
                {
                    AddMenuItemEvent(this.tlsMain.TsbNew, New_Click);
                    AddMenuItemEvent(this.tlsMain.TsbOpen, Open_Click);
                    AddMenuItemEvent(this.tlsMain.TsbSave, Save_Click);
                    AddMenuItemEvent(this.tlsMain.TsbSaveAll, SaveAll_Click);

                    AddMenuItemEvent(this.tlsMain.TsbUndo, Undo_Click);
                    AddMenuItemEvent(this.tlsMain.TsbRedo, Redo_Click);
                    AddMenuItemEvent(this.tlsMain.TsbCut, Cut_Click);
                    AddMenuItemEvent(this.tlsMain.TsbCopy, Copy_Click);
                    AddMenuItemEvent(this.tlsMain.TsbPaste, Paste_Click);
                }
            }
        }

        /// <summary>
        /// Gets a <see cref="StringCollection"/> of the recently opened files.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract StringCollection RecentFiles { get; }

        /// <summary>
        /// Gets the maximum allowable size of <see cref="RecentFiles"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected abstract int MaxRecentFiles { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds <paramref name="handler"/> to the <see cref="ToolStripItem.Click"/>
        /// event of <paramref name="tsm"/>.
        /// </summary>
        /// <param name="tsm">
        /// The <see cref="ToolStripItem"/> to add <paramref name="handler"/> to.
        /// </param>
        /// <param name="handler">
        /// The <see cref="EventHandler"/> to add to the <see cref="ToolStripItem.Click"/>
        /// event of <paramref name="tsm"/>.
        /// </param>
        protected virtual void AddMenuItemEvent(ToolStripItem tsm, EventHandler handler)
        {
            if (tsm != null)
                tsm.Click += handler;
        }

        /// <summary>
        /// Removes <paramref name="handler"/> from the <see cref="ToolStripItem.Click"/>
        /// event of <paramref name="tsm"/>.
        /// </summary>
        /// <param name="tsm">
        /// The <see cref="ToolStripItem"/> to remove <paramref name="handler"/> from.
        /// </param>
        /// <param name="handler">
        /// The <see cref="EventHandler"/> to remove from the <see cref="ToolStripItem.Click"/>
        /// event of <paramref name="tsm"/>.
        /// </param>
        protected void RemoveMenuItemEvent(ToolStripItem tsm, EventHandler handler)
        {
            if (tsm != null)
                tsm.Click -= handler;
        }

        /// <summary>
        /// Enable or disable the basic edit menu items.
        /// </summary>
        /// <param name="enabled">
        /// The <see cref="ToolStripItem.Enabled"/> value to set for the
        /// basic menu commands.
        /// </param>
        protected virtual void ToggleToolStripItemsEnabled(bool enabled)
        {
            // Set the base enabled value.
            this.enabled = enabled;

            // Create an array of all the tool strip items.
            ToolStripItem[] items = {
                this.mnuMain.TsmSave, this.mnuMain.TsmSaveAs, this.mnuMain.TsmSaveAll,
                this.mnuMain.TsmUndo, this.mnuMain.TsmRedo,
                this.mnuMain.TsmCut, this.mnuMain.TsmCopy, this.mnuMain.TsmPaste, this.mnuMain.TsmDelete,
                this.tlsMain.TsbSave, this.tlsMain.TsbSaveAll,
                this.tlsMain.TsbUndo, this.tlsMain.TsbRedo,
                this.tlsMain.TsbCut, this.tlsMain.TsbCopy };

            // Loop to toggle all of their enabled flags.
            for (int i = items.Length; --i >= 0; )
                if (items[i] != null)
                    items[i].Enabled = enabled;

            // Toggle enabled values of special menu commands.
            SetPasteEnabled();
            SetUndoRedoEnabled();
            SetGoToEnabled();
        }

        /// <summary>
        /// Display all the recent files inside of <see cref="RecentFiles"/>.
        /// </summary>
        protected virtual void DisplayRecentFiles()
        {
            // Make sure recent files list actually exist.
            if (this.RecentFiles == null)
                return;

            // Add all of the recent files.
            StringCollection files = this.RecentFiles;
            for (int i = files.Count; --i >= 0; )
                AddRecentFile(files[i]);
        }

        /// <summary>
        /// Adds <paramref name="path"/> to the list of <see cref="RecentFiles"/>.
        /// </summary>
        /// <param name="path">
        /// The full path of the most recently opened file.
        /// </param>
        protected virtual void AddRecentFile(string path)
        {
            // Make sure recent files list actually exists.
            if (this.RecentFiles == null)
                return;

            // Remove the file to avoid repeats.
            RemoveRecentFile(path);

            // Create the menu item and add its click event.
            ToolStripMenuItem tsm = new ToolStripMenuItem();
            tsm.Text = path;
            tsm.Tag = path;
            tsm.Click += new EventHandler(RecentFiles_Click);

            // Set to the top of the recent files list.
            this.mnuMain.TsmOpenRecent.DropDownItems.Insert(0, tsm);
        }

        /// <summary>
        /// Removes <paramref name="path"/> from <see cref="RecentFiles"/>.
        /// </summary>
        /// <param name="path">
        /// The full path of the file to remove.
        /// </param>
        protected virtual void RemoveRecentFile(string path)
        {
            // Make sure recent files list actually exists.
            if (RecentFiles == null)
                return;

            // Find the menu item(s) with the same match path
            ToolStripItemCollection items = this.mnuMain.TsmOpenRecent.DropDownItems;
            for (int i = items.Count; --i >= 0; )
                if ((string)items[i].Tag == path)
                    items[i].Dispose();

            // Remove any extra files that go beyond the max limit.
            int max = this.MaxRecentFiles;
            while (items.Count > max)
                items[max].Dispose();
        }

        /// <summary>
        /// Stores <see cref="RecentFiles"/> as menu items to allow for easier access.
        /// </summary>
        public virtual void StoreRecentFiles()
        {
            // Make sure recent files list actually exists.
            if (this.RecentFiles == null)
                return;

            // Get all of the recent files form the tool stip collection.
            ToolStripItemCollection items = this.mnuMain.TsmOpenRecent.DropDownItems;

            // Add all of the items to the recent files list.
            this.RecentFiles.Clear();
            for (int i = items.Count; --i >= 0; )
                this.RecentFiles.Insert(0, (string)items[i].Tag);
        }

        /// <summary>
        /// Enable or disable the 'Undo' and 'Redo' menu items.
        /// </summary>
        protected virtual void SetUndoRedoEnabled()
        {
            // Determine whether we can undo.
            bool undo = this.CurrentEditor != null && this.CurrentEditor.Editor.History.CanUndo;

            // Set undo enabled to the controls that exist.
            if (this.mnuMain.TsmUndo != null)
                this.mnuMain.TsmUndo.Enabled = undo;
            if (this.tlsMain.TsbUndo != null)
                this.tlsMain.TsbUndo.Enabled = undo;

            // Determine whether we can redo.
            bool redo = this.CurrentEditor != null && this.CurrentEditor.Editor.History.CanRedo;

            // Set redo enabled to the control that exist.
            if (this.mnuMain.TsmRedo != null)
                this.mnuMain.TsmRedo.Enabled = redo;
            if (this.tlsMain.TsbRedo != null)
                this.tlsMain.TsbRedo.Enabled = redo;
        }

        /// <summary>
        /// Enable or disable the 'Paste' menu items.
        /// </summary>
        protected virtual void SetPasteEnabled()
        {
            // Determine whether we can paste.
            bool paste = this.enabled && this.EditorMdiForm.CopyData != null;

            // Set paste enabled to the controls that exist.
            if (this.mnuMain.TsmPaste != null)
                this.mnuMain.TsmPaste.Enabled = paste;
            if (this.tlsMain.TsbPaste != null)
                this.tlsMain.TsbPaste.Enabled = paste;
        }

        /// <summary>
        /// Enable or disable the 'Go To' menu items.
        /// </summary>
        protected abstract void SetGoToEnabled();

        /// <summary>
        /// Initializes a new context menu.
        /// </summary>
        /// <returns>
        /// A derived from of <see cref="EditorContextMenuStrip"/>.
        /// </returns>
        protected abstract EditorContextMenuStrip CreateContextMenu();

        private void EditorMdiForm_EditorFormAdded(object sender, EditorFormEventArgs e)
        {
            e.EditorForm.Editor.DataReset += new EventHandler(Editor_DataModified);
            e.EditorForm.Editor.DataModified += new EventHandler(Editor_DataModified);
            e.EditorForm.Editor.FileSaved += new EventHandler(Editor_FileSaved);

            // Create the editor context menu and set its events.
            EditorContextMenuStrip cms = CreateContextMenu();
            cms.Tag = e.EditorForm;
            AddMenuItemEvent(cms.TsmCut, Cut_Click);
            AddMenuItemEvent(cms.TsmCopy, Copy_Click);
            AddMenuItemEvent(cms.TsmPaste, Paste_Click);
            AddMenuItemEvent(cms.TsmDelete, Delete_Click);
            cms.Opening += new CancelEventHandler(EditorContextMenuStrip_Opening);
            e.EditorForm.MainEditorControl.ContextMenuStrip = cms;

            // Enable the tool strip items.
            ToggleToolStripItemsEnabled(true);

            // Add the path if this isn't a new file.
            if (e.EditorForm.Editor.FileDataType != FileDataTypes.FromFile)
            {
                AddRecentFile(e.EditorForm.Editor.FilePath);
                StoreRecentFiles();
            }
        }

        private void EditorMdiForm_EditorFormRemoved(object sender, EditorFormEventArgs e)
        {
            if (this.editorMdiForm.Editors.Count == 0)
                ToggleToolStripItemsEnabled(false);
        }

        private void EditorMdiForm_ActiveEditorChanged(object sender, EditorFormEventArgs e)
        {
            SetUndoRedoEnabled();
        }

        private void EditorContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip cms = (ContextMenuStrip)sender;
            EditorForm form = (EditorForm)cms.Tag;
            form.Activate();
        }

        private void Editor_DataModified(object sender, EventArgs e)
        {
            SetUndoRedoEnabled();
        }

        private void Editor_FileSaved(object sender, EventArgs e)
        {
            Editor editor = (Editor)sender;
            AddRecentFile(editor.FilePath);
            StoreRecentFiles();
        }

        private void New_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.NewEditorForm();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.OpenEditorForm();
        }

        private void RecentFiles_Click(object sender, EventArgs e)
        {
            // Get the menu item that sent the event.
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            // The tag of the menu item has the path of the file to open.
            this.editorMdiForm.OpenEditorForm((string)tsm.Tag);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.SaveEditorForm();
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.SaveEditorFormAs();
        }

        private void SaveAll_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.SaveAllEditorForms();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.Close();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
            {
                this.CurrentEditor.Editor.Undo();
                SetUndoRedoEnabled();
            }
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
            {
                this.CurrentEditor.Editor.Redo();
                SetUndoRedoEnabled();
            }
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.Cut();
            SetPasteEnabled();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.Copy();
            SetPasteEnabled();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.Paste();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.DeleteSelection();
        }

        private void GoTo_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.GoTo();
        }

        private void CustomizeSettings_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.CustomizeSettings();
        }

        private void Cascade_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVertical_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontal_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIcons_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAll_Click(object sender, EventArgs e)
        {
            this.editorMdiForm.CloseAllEditorForms();
        }
        #endregion
    }
}