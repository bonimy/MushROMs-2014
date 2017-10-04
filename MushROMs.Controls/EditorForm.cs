using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MushROMs.Editors;
using MushROMs.Controls.Properties;

namespace MushROMs.Controls
{
    /// <summary>
    /// A <see cref="Form"/> that contains functionality for
    /// managing an <see cref="MainEditorControl"/>.
    /// </summary>
    public partial class EditorForm : DesignerForm
    {
        #region Delegates
        /// <summary>
        /// Invokes a void method with no parameters that is called when
        /// the <see cref="EditorForm"/> should be resized to fit the
        /// <see cref="Editor"/>.
        /// </summary>
        protected MethodInvoker InvokeSetFormSizeFromEditor;

        /// <summary>
        /// Invokes a void method with no parameters that is called when
        /// the <see cref="Editor"/> should be resized to fit the
        /// <see cref="EditorForm"/>.
        /// </summary>
        protected MethodInvoker InvokeSetEditorSizeFromForm;

        /// <summary>
        /// Invokes a void method with no parameters that is called when
        /// the status of the <see cref="EditorForm"/> should be updated.
        /// </summary>
        protected MethodInvoker InvokeUpdateStatus;

        /// <summary>
        /// Invoked a void method with no parameters that is called when
        /// a 'Go To' command is internally called.
        /// </summary>
        protected internal MethodInvoker InvokeGoTo;
        #endregion

        #region Fileds
        private Editor editor;
        private IEditorControl mainEditorControl;

        /// <summary>
        /// The <see cref="SaveFileDialog"/> of the editor.
        /// </summary>
        private SaveFileDialog sfd;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="EditorMdiForm"/> associated with the <see cref="EditorForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual EditorMdiForm EditorMdiForm
        {
            get { return (EditorMdiForm)this.MdiParent; }
            set { this.MdiParent = value; }
        }

        /// <summary>
        /// Gets the <see cref="MainEditorControl"/> associated with the <see cref="EditorForm"/>.
        /// </summary>
        public virtual IEditorControl MainEditorControl
        {
            get { return this.mainEditorControl; }
            set
            {
                if (this.MainEditorControl != null)
                    this.MainEditorControl.EditorChanged -= MainEditorControl_EditorChanged;

                this.mainEditorControl = value;

                if (this.MainEditorControl != null)
                    this.MainEditorControl.EditorChanged += new EventHandler(MainEditorControl_EditorChanged);
            }
        }

        /// <summary>
        /// Gets the <see cref="Editor"/> associated with this <see cref="EditorForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Editor Editor
        {
            get { return this.editor; }
            protected set
            {
                if (this.Editor != null)
                {
                    this.Text = string.Empty;

                    this.Editor.DataReset -= Editor_DataModified;
                    this.Editor.DataModified -= Editor_DataModified;
                    this.Editor.Active.IndexChanged -= Editor_UpdateStatus;
                    this.Editor.SelectedTilesChanged -= Editor_UpdateStatus;
                    this.Editor.ZoomSizeChanged -= Editor_SetFormSize;
                    this.Editor.ViewSizeChanged -= Editor_SetFormSize;

                    this.SizeChanged -= EditorForm_SetEditorSize;
                }

                this.editor = value;

                if (this.Editor != null)
                {
                    SetFormName();

                    this.Editor.DataReset += new EventHandler(Editor_DataModified);
                    this.Editor.DataModified += new EventHandler(Editor_DataModified);
                    this.Editor.Active.IndexChanged += new EventHandler(Editor_UpdateStatus);
                    this.Editor.SelectedTilesChanged += new EventHandler(Editor_UpdateStatus);
                    this.Editor.ZoomSizeChanged += new EventHandler(Editor_SetFormSize);
                    this.Editor.ViewSizeChanged += new EventHandler(Editor_SetFormSize);

                    this.SizeChanged += new EventHandler(EditorForm_SetEditorSize);
                }
            }
        }

        /// <summary>
        /// Gets the current <see cref="IEditorData"/> for the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IEditorData CopyData
        {
            get { return this.EditorMdiForm.CopyData; }
            set { this.EditorMdiForm.CopyData = value; }
        }

        /// <summary>
        /// Gets the <see cref="SaveFileDialog"/> of the editor.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual SaveFileDialog SaveFileDialog
        {
            get { return this.sfd; }
        }

        /// <summary>
        /// Gets or sets the status of the <see cref="EditorForm"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string Status
        {
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorForm"/> class.
        /// </summary>
        public EditorForm()
        {
            this.sfd = new SaveFileDialog();
        }
        #endregion

        #region Methods
        private void MainEditorControl_EditorChanged(object sender, EventArgs e)
        {
            if (this.MainEditorControl != null)
                this.Editor = this.MainEditorControl.Editor;
            else
            this.Editor = null;
        }

        /// <summary>
        /// Sets the form name (usually the file name) when data is modified.
        /// </summary>
        protected virtual void SetFormName()
        {
            // The title of the form is the file name of the path
            this.Text = this.Editor.Title;
        }

        /// <summary>
        /// Saves the data of the <see cref="Editor"/> back to its file.
        /// </summary>
        public virtual void SaveEditor()
        {
            SaveEditor(this.Editor.FilePath);
        }

        /// <summary>
        /// Saves the data of the <see cref="Editor"/> to a new path.
        /// </summary>
        public virtual void SaveEditorAs()
        {
            if (this.sfd.ShowDialog() == DialogResult.OK)
                SaveEditor(sfd.FileName);
        }

        /// <summary>
        /// Saves the data of the <see cref="Editor"/> to <paramref name="path"/>.
        /// </summary>
        /// <param name="path"></param>
        public virtual void SaveEditor(string path)
        {
            // Prompt "Save as" method if path is empty.
            if (path == string.Empty)
            {
                SaveEditorAs();
                return;
            }

            try // Everything loves to go wrong with file I/O.
            {
                this.Editor.Save(path);
            }
            catch (IOException ex)
            {
                // Let the user try to open the file again if it is an I/O error.
                if (MessageBox.Show(ex.Message, Resources.DialogTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    SaveEditor(path);
                return;
            }
#if !DEBUG
            catch (Exception ex)
            {
                // Catch any other errors and cancel saving.
                MessageBox.Show(ex.Message, Resources.DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#endif
        }

        /// <summary>
        /// Cuts the current selection and stores it to <see cref="CopyData"/>.
        /// </summary>
        public virtual void Cut()
        {
            Copy();
            DeleteSelection();
        }

        /// <summary>
        /// Creates a copy of the current selection.
        /// </summary>
        public virtual void Copy()
        {
            this.CopyData = this.Editor.CreateCopy();
        }

        /// <summary>
        /// Pastes <see cref="CopyData"/> to the current selection.
        /// </summary>
        public virtual void Paste()
        {
            this.Editor.Paste(this.CopyData);
        }

        /// <summary>
        /// Deletes the current selection.
        /// </summary>
        public virtual void DeleteSelection()
        {
            this.Editor.DeleteSelection();
        }

        /// <summary>
        /// Raises the <see cref="Control.Resize"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnResize(EventArgs e)
        {
            if (this.Editor != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Resources.StatusViewWidth);
                sb.Append(": 0x");
                sb.Append(this.Editor.ViewSize.Width.ToString("X"));
                sb.Append(", ");
                sb.Append(Resources.StatusViewHeight);
                sb.Append(": 0x");
                sb.Append(this.Editor.ViewSize.Height.ToString("X"));
                this.Status = sb.ToString();
            }

            base.OnResize(e);
        }

        /// <summary>
        /// Raises the <see cref="Form.ResizeEnd"/> event
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnResizeEnd(EventArgs e)
        {
            if (InvokeSetFormSizeFromEditor != null)
                InvokeSetFormSizeFromEditor();
            base.OnResizeEnd(e);
        }

        /// <summary>
        /// Raises the <see cref="Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="FormClosingEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.Editor != null)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (this.Editor.History.Unsaved)
                    {
                        DialogResult result = MessageBox.Show(Resources.UnsavedFile, Resources.DialogTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                            SaveEditor();
                        else if (result == DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }
            
            base.OnFormClosing(e);
        }

        private void Editor_DataModified(object sender, EventArgs e)
        {
            SetFormName();
        }

        private void Editor_UpdateStatus(object sender, EventArgs e)
        {
            if (InvokeUpdateStatus != null)
                InvokeUpdateStatus();
        }

        private void Editor_SetFormSize(object sender, EventArgs e)
        {
            if (InvokeSetFormSizeFromEditor != null)
                InvokeSetFormSizeFromEditor();
        }

        private void EditorForm_SetEditorSize(object sender, EventArgs e)
        {
            if (InvokeSetEditorSizeFromForm != null)
                InvokeSetEditorSizeFromForm();
        }
        #endregion
    }
}