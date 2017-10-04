/* Deals with the file data of the editor.
 * The file data is a separate entity from the
 * editor data. But it is usually good practice to
 * have the file data translate what the editor data
 * will be. For example, the file data can be
 * compressed GFX and after it is loaded,
 * programmatically sets the editor data to the raw
 * GFX.
 */

using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    partial class Editor
    {
        #region Constant and read-only variables
        /// <summary>
        /// Text appended to the end of the form name when the data is unsaved.
        /// </summary>
        private const string UnsavedNotification = "*";
        #endregion

        #region Fields
        /// <summary>
        /// A <see cref="FileDataTypes"/> value describing
        /// the file data of the <see cref="Editor"/>.
        /// </summary>
        private FileDataTypes fileDataType;

        /// <summary>
        /// The file name of this file if it is untitled.
        /// </summary>
        private string untitled;

        /// <summary>
        /// The file path.
        /// </summary>
        private string fp;
        /// <summary>
        /// The file data.
        /// </summary>
        private byte[] fData;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a <see cref="FileDataTypes"/> value describing
        /// the file data of the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual FileDataTypes FileDataType
        {
            get { return this.fileDataType; }
            protected set { this.fileDataType = value; }
        }

        /// <summary>
        /// Gets or sets the file name of this file if it is untitled.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string Untitled
        {
            get { return this.untitled; }
            set { this.untitled = value; }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string FilePath
        {
            get { return this.fp; }
            protected set { this.fp = value; }
        }
        /// <summary>
        /// Gets the file data.
        /// </summary>
        /// <remarks>
        /// Classes deriving from <see cref="Editor"/> can set the file
        /// data programmatically.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual byte[] FileData
        {
            get { return this.fData; }
            protected set { this.fData = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// When overriden in a derived class, creates a file name for an
        /// untitled file.
        /// </summary>
        /// <returns>
        /// A file name for an untitled file.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <see cref="Editor"/> does not have file data.
        /// </exception>
        protected virtual string CreateUntitledFileName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens a binary file, reads the contents of the file and stores
        /// it to <see cref="FileData"/>.
        /// </summary>
        /// <param name="path">
        /// The file to open for reading.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only white space,
        /// or contains one or more invalid characters as defined by
        /// <see cref="Path.InvalidPathChars"/>.  -or-
        /// <see cref="Editor"/> does not have file data.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is null.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum
        /// length. For example, on Windows-based platforms, paths must be less than
        /// 248 characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// This operation is not supported on the current platform.  -or-
        /// path specified a directory.  -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file specified in <paramref name="path"/> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is an invalid format.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        public virtual void Open(string path)
        {
            this.FilePath = path;

            OnFileOpening(EventArgs.Empty);
            this.FileData = File.ReadAllBytes(this.FilePath);
            this.FileDataType = FileDataTypes.FromFile;
            OnFileOpened(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="FileOpening"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnFileOpening(EventArgs e)
        {
            if (FileOpening != null)
                FileOpening(this, e);
        }

        /// <summary>
        /// Raises the <see cref="FileOpened"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnFileOpened(EventArgs e)
        {
            if (FileOpened != null)
                FileOpened(this, e);
        }

        /// <summary>
        /// Saves the contents <see cref="FileData"/> to <see cref="FilePath"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// <see cref="FilePath"/> is a zero-length string, contains only white space,
        /// or contains one or more invalid characters as defined by
        /// <see cref="Path.InvalidPathChars"/>.  -or-
        /// <see cref="Editor"/> does not have file data.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <see cref="FilePath"/> is null or <see cref="Data"/> is empty.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum
        /// length. For example, on Windows-based platforms, paths must be less than
        /// 248 characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// This operation is not supported on the current platform.  -or-
        /// path specified a directory.  -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file specified in <see cref="FilePath"/> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <see cref="FilePath"/> is an invalid format.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        public virtual void Save()
        {
            Save(this.FilePath);
        }

        /// <summary>
        /// Saves the contents of <see cref="FileData"/> to <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        /// The file to write to.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only white space,
        /// or contains one or more invalid characters as defined by
        /// <see cref="Path.InvalidPathChars"/>.  -or-
        /// <see cref="Editor"/> does not have file data.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is null or <see cref="Data"/> is empty.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum
        /// length. For example, on Windows-based platforms, paths must be less than
        /// 248 characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// This operation is not supported on the current platform.  -or-
        /// path specified a directory.  -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file specified in <paramref name="path"/> was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is an invalid format.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        public virtual void Save(string path)
        {
            if (this.FileDataType == FileDataTypes.NotAFile)
                throw new ArgumentException(Resources.ErrorNotFile);

            this.FilePath = path;

            OnFileSaving(EventArgs.Empty);
            File.WriteAllBytes(this.FilePath, this.FileData);
            OnFileSaved(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="FileSaving"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnFileSaving(EventArgs e)
        {
            if (FileSaving != null)
                FileSaving(this, e);
        }

        /// <summary>
        /// Raises the <see cref="FileSaved"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnFileSaved(EventArgs e)
        {
            this.History.SetSaveIndex();

            if (FileSaved != null)
                FileSaved(this, e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs before <see cref="Open(string)"/> reads
        /// the file data from the file path.
        /// </summary>
        [Category("File")]
        [Description("Occurs before file data is opened in the editor.")]
        public event EventHandler FileOpening;

        /// <summary>
        /// Occurs when the <see cref="Open(string)"/> method
        /// is called.
        /// </summary>
        [Category("File")]
        [Description("Occurs when file data is opened in the editor.")]
        public event EventHandler FileOpened;

        /// <summary>
        /// Occurs before <see cref="Save(string)"/> saves
        /// the file data back to the file path.
        /// </summary>
        [Category("File")]
        [Description("Occurs before editor data is saved to a file.")]
        public event EventHandler FileSaving;

        /// <summary>
        /// Occurs when the <see cref="Save(string)"/> method
        /// is called.
        /// </summary>
        [Category("File")]
        [Description("Occurs when editor data is saved to a file.")]
        public event EventHandler FileSaved;
        #endregion
    }
}