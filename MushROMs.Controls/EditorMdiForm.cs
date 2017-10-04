using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MushROMs.Editors;
using MushROMs.Controls.Properties;

namespace MushROMs.Controls
{
    /// <summary>
    /// An MDI form that contains functionality for hosting
    /// multiple <see cref="EditorForm"/>s.
    /// </summary>
    public partial class EditorMdiForm : MdiForm
    {
        #region Delegates
        #endregion

        #region Events
        /// <summary>
        /// Occurs when an <see cref="EditorForm"/> is added.
        /// </summary>
        public event EditorFormEventHandler EditorFormAdded;

        /// <summary>
        /// Occurs when an <see cref="EditorForm"/> is removed.
        /// </summary>
        public event EditorFormEventHandler EditorFormRemoved;

        /// <summary>
        /// Occurs when the active <see cref="EditorForm"/> changes.
        /// </summary>
        public event EditorFormEventHandler ActiveEditorChanged;
        #endregion

        #region Fields
        /// <summary>
        /// A collection of the <see cref="EditorForm"/>s in this
        /// <see cref="EditorMdiForm"/>.
        /// </summary>
        private List<EditorForm> editors;

        /// <summary>
        /// A copy of a <see cref="Selection"/> when <see cref="Copy()"/>
        /// was invoked.
        /// </summary>
        private IEditorData copyData;

        /// <summary>
        /// The <see cref="OpenFileDialog"/> of the editor.
        /// </summary>
        private OpenFileDialog ofd;

        /// <summary>
        /// The <see cref="MenuComponents"/> of the editor.
        /// </summary>
        private MenuComponents menuComponents;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a collection of the <see cref="EditorForm"/>s in this
        /// <see cref="EditorMdiForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<EditorForm> Editors
        {
            get { return this.editors; }
        }

        /// <summary>
        /// Gets the currently active <see cref="EditorForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorForm CurrentEditor
        {
            get
            {
                for (int i = this.editors.Count; --i >= 0; )
                    if (this.ActiveMdiChild == this.editors[i])
                        return this.editors[i];
                return null;
            }
        }

        /// <summary>
        /// Gets a copy of a selection when <see cref="Copy()"/>
        /// was invoked.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEditorData CopyData
        {
            get { return this.copyData; }
            set { this.copyData = value; }
        }

        /// <summary>
        /// Sets the status of the <see cref="EditorMdiForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string Status
        {
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the <see cref="OpenFileDialog"/> of the <see cref="EditorMdiForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected OpenFileDialog OpenFileDialog
        {
            get { return this.ofd; }
        }

        /// <summary>
        /// Gets or sets the <see cref="MenuComponents"/> of the
        /// <see cref="EditorMdiForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected MenuComponents MenuComponents
        {
            get { return this.menuComponents; }
            set { this.menuComponents = value; }
        }

        /// <summary>
        /// The default <see cref="Size"/> of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(1000, 600); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorMdiForm"/> class.
        /// </summary>
        public EditorMdiForm()
        {
            this.editors = new List<EditorForm>();

            this.ofd = new OpenFileDialog();
            this.ofd.Multiselect = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new <see cref="EditorForm"/>.
        /// </summary>
        public virtual void NewEditorForm()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <see cref="EditorForm"/> from file data.
        /// </summary>
        public virtual void OpenEditorForm()
        {
            if (this.menuComponents != null &&
                this.menuComponents.RecentFiles != null &&
                this.menuComponents.RecentFiles.Count > 0)
            {
                string path = this.menuComponents.RecentFiles[0];
                this.ofd.DefaultExt = Path.GetExtension(path);
                this.ofd.InitialDirectory = Path.GetDirectoryName(path);
                this.ofd.FileName = Path.GetFileName(this.menuComponents.RecentFiles[0]);
            }

            if (this.ofd.ShowDialog() == DialogResult.OK)
                for (int i = this.ofd.FileNames.Length; --i >= 0; )
                    OpenEditorForm(this.ofd.FileNames[i]);
        }

        /// <summary>
        /// Creates a new <see cref="EditorForm"/> from file data.
        /// </summary>
        /// <param name="path">
        /// The path of the file.
        /// </param>
        public virtual void OpenEditorForm(string path)
        {
            // Determine whether the file exists first.
            if (!File.Exists(path))
            {
                //If the file does not exist, prompt to find it.
                StringBuilder sb = new StringBuilder(Resources.ErrorPathDoesNotExist);
                sb.Append("\n\n");
                sb.Append(path);
                sb.Append("\n\n");
                sb.Append(Resources.OptionLocateFile);

                if (MessageBox.Show(sb.ToString(), Resources.DialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    OpenEditorForm();
                else
                    this.Status = Resources.StatusFileNotFound;
                return;
            }

            // Determine that this file is not already open.
            string fullpath = Path.GetFullPath(path);
            for (int i = this.editors.Count; --i >= 0; )
            {
                // Ignore new files.
                if (this.editors[i].Editor.FileDataType != FileDataTypes.FromFile)
                    continue;

                string fn = Path.GetFullPath(this.editors[i].Editor.FilePath);
                if (fn != string.Empty && fullpath == Path.GetFullPath(fn))
                {
                    this.editors[i].Activate();
                    this.Status = Resources.StatusFileAlreadyOpen;
                    return;
                }
            }

            // Create a new editor
            EditorForm form = InitializeNewEditor();

            try // Everything loves to go wrong with file I/O.
            {                
                form.Editor.Open(path);
            }
            catch (IOException ex)
            {
                // Let the user try to open the file again if it is an I/O error.
                if (MessageBox.Show(ex.Message, Resources.DialogTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    OpenEditorForm(path);
                return;
            }
#if !DEBUG
            catch (Exception ex)
            {
                // Catch any other errors and cancel opening.
                MessageBox.Show(ex.Message, Resources.DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#endif
            OnEditorFormAdded(new EditorFormEventArgs(form));
        }

        /// <summary>
        /// Initialize a new <see cref="EditorForm"/> to add to this instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="EditorForm"/>.
        /// </returns>
        protected virtual EditorForm InitializeNewEditor()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the <see cref="EditorForm"/> data back to the file.
        /// </summary>
        public virtual void SaveEditorForm()
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.SaveEditor();
        }

        /// <summary>
        /// Saves the <see cref="EditorForm"/> data to a new path.
        /// </summary>
        public virtual void SaveEditorFormAs()
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.SaveEditorAs();
        }

        /// <summary>
        /// Save the <see cref="EditorForm"/> data to a file.
        /// </summary>
        /// <param name="path">
        /// The path pf the file.
        /// </param>
        public virtual void SaveEditorForm(string path)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.SaveEditor(path);
        }

        /// <summary>
        /// Saves all <see cref="EditorForm"/> data to their files.
        /// </summary>
        public virtual void SaveAllEditorForms()
        {
            for (int i = this.editors.Count; --i >= 0; )
                if (this.editors[i].Editor.FileDataType != FileDataTypes.NotAFile)
                    if (this.editors[i].Editor.History.Unsaved)
                        this.editors[i].SaveEditor();
        }

        /// <summary>
        /// Closes all of the <see cref="EditorForm"/>s in this <see cref="EditorMdiForm"/>.
        /// </summary>
        public virtual void CloseAllEditorForms()
        {
            for (int i = this.editors.Count; --i >= 0; )
                if (this.editors[i].Editor.FileDataType != FileDataTypes.NotAFile)
                    this.editors[i].Close();
        }

        /// <summary>
        /// Redraws all <see cref="EditorForm"/>s in the <see cref="EditorMdiForm"/>.
        /// </summary>
        public virtual void RedrawAllEditorForms()
        {
            for (int i = this.editors.Count; --i >= 0; )
                this.editors[i].MainEditorControl.Redraw();
        }

        /// <summary>
        /// Performs the <see cref="EditorForm.Cut()"/> operation of
        /// <see cref="CurrentEditor"/>.
        /// </summary>
        public virtual void Cut()
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Cut();
        }

        /// <summary>
        /// Performs the <see cref="EditorForm.Copy()"/> operation of
        /// <see cref="CurrentEditor"/>.
        /// </summary>
        public virtual void Copy()
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Copy();
        }

        /// <summary>
        /// Performs the <see cref="EditorForm.Paste()"/> operation of
        /// <see cref="CurrentEditor"/>.
        /// </summary>
        public virtual void Paste()
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Paste();
        }

        /// <summary>
        /// Performs the <see cref="EditorForm.DeleteSelection()"/> operation of
        /// <see cref="CurrentEditor"/>.
        /// </summary>
        public virtual void DeleteSelection()
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.DeleteSelection();
        }

        /// <summary>
        /// Opens a "Go To" dialog box for the <see cref="Editor"/>.
        /// </summary>
        public virtual void GoTo()
        {
            if (this.CurrentEditor != null)
                if (this.CurrentEditor.InvokeGoTo != null)
                    this.CurrentEditor.InvokeGoTo();
        }

        /// <summary>
        /// Customizes settings applied to <see cref="EditorMdiForm"/> and its children.
        /// </summary>
        public virtual void CustomizeSettings()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Raises the <see cref="EditorFormAdded"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EditorFormEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnEditorFormAdded(EditorFormEventArgs e)
        {
            // Add the form if its not already in the list of editors.
            EditorForm form = e.EditorForm;
            for (int i = this.editors.Count; --i >= 0; )
                if (this.editors[i] == form)
                    return;
            this.editors.Add(form);

            // Set properties to the form and show it.
            form.Activated += new EventHandler(EditorForm_Activated);
            form.FormClosed += new FormClosedEventHandler(EditorForm_FormClosed);
            form.MdiParent = this;
            form.Show();

            if (EditorFormAdded != null)
                EditorFormAdded(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EditorFormRemoved"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EditorFormEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnEditorFormRemoved(EditorFormEventArgs e)
        {
            if (EditorFormRemoved != null)
                EditorFormRemoved(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Control.DragEnter"/> event.
        /// </summary>
        /// <param name="drgevent">
        /// A <see cref="DragEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            ProcessEditorDragEnter(drgevent);
            base.OnDragEnter(drgevent);
        }

        /// <summary>
        /// Processes the <see cref="Control.DragEnter"/> logic for the
        /// <see cref="EditorMdiForm"/>.
        /// </summary>
        /// <param name="drgevent">
        /// A <see cref="DragEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorDragEnter(DragEventArgs drgevent)
        {
            // By default, only allow file drops.
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                drgevent.Effect = DragDropEffects.All;
                this.Status = Resources.StatusDragValid;
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
                this.Status = Resources.StatusDragInvalid;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.DragDrop"/> event.
        /// </summary>
        /// <param name="drgevent">
        /// A <see cref="DragEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            ProcessEditorDragDrop(drgevent);
            base.OnDragDrop(drgevent);
        }

        /// <summary>
        /// Processes the <see cref="Control.DragDrop"/> logic for the
        /// <see cref="EditorMdiForm"/>.
        /// </summary>
        /// <param name="drgevent">
        /// A <see cref="DragEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorDragDrop(DragEventArgs drgevent)
        {
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Invoke opening all of the files.
                string[] files = (string[])drgevent.Data.GetData(DataFormats.FileDrop, false);
                for (int i = 0; i < files.Length; i++)
                    OpenEditorForm(files[i]);
            }
        }

        /// <summary>
        /// Raises the <see cref="Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="FormClosingEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ProcessEditorClosing(e);
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Process the <see cref="Form.FormClosing"/> logic for the
        /// <see cref="EditorMdiForm"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="FormClosingEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void ProcessEditorClosing(FormClosingEventArgs e)
        {
            List<string> names = new List<string>();
            for (int i = this.Editors.Count; --i >= 0; )
                if (this.Editors[i].Editor.History.Unsaved)
                    names.Add(this.Editors[i].Editor.Title);

            if (names.Count > 0)
            {
                UnsavedForm dlg = new UnsavedForm();
                dlg.Files = names;
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.Yes)
                    SaveAllEditorForms();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void EditorForm_Activated(object sender, EventArgs e)
        {
            if (ActiveEditorChanged != null)
                ActiveEditorChanged(this, new EditorFormEventArgs((EditorForm)this.ActiveMdiChild));
        }

        private void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.editors.Remove((EditorForm)sender);
            OnEditorFormRemoved(new EditorFormEventArgs((EditorForm)sender));
        }
        #endregion
    }
}