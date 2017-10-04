/* Deals with the data of the editor.
 * No data handling is done in the base editor class,
 * how it is used is up to the programmer. The data can
 * represent palette colors in a standardized format,
 * level data, or gfx data after decompression. What
 * the data represents, as well as properly managing
 * it's memory, is the responsibility of the programmer.
 */

using System;
using System.ComponentModel;
using MushROMs.Unmanaged;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    partial class Editor
    {
        #region Fields
        /// <summary>
        /// A <see cref="Pointer"/> to data describing what the 
        /// <see cref="Editor"/> is editing.
        /// </summary>
        /// <remarks>
        /// The data this <see cref="IntPtr"/> points to is determined
        /// completely by the user. The <see cref="Editor"/> does nothing
        /// with this <see cref="Pointer"/> other than including it in the
        /// class for the sake of the user.
        /// </remarks>
        private Pointer data;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a <see cref="Pointer"/> to data describing what the
        /// <see cref="Editor"/> is editing.
        /// </summary>
        /// <remarks>
        /// The data this <see cref="IntPtr"/> points to is determined
        /// completely by the user. The <see cref="Editor"/> does nothing
        /// with this <see cref="Pointer"/> other than including it in the
        /// class for the sake of the user.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Pointer Data
        {
            get { return this.data; }
            protected set { this.data = value; OnDataReset(EventArgs.Empty); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reinitializes the <see cref="Data"/> pointer with specified
        /// <paramref name="length"/>. Whatever <see cref="Data"/> pointed
        /// to beforehand will be freed from memory.
        /// </summary>
        /// <param name="length">
        /// The size, in bytes, of the data array.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="length"/> is less than or equal to zero.
        /// </exception>
        protected virtual void ResetData(int length)
        {
            ResetData(length, true);
        }

        /// <summary>
        /// Reinitializes the <see cref="Data"/> pointer with specified
        /// <paramref name="length"/>. Whatever <see cref="Data"/> pointed
        /// to beforehand will be freed from memory only if
        /// <paramref name="free"/> is set.
        /// </summary>
        /// <param name="length">
        /// The size, in bytes, of the data array.
        /// </param>
        /// <param name="free">
        /// If true, the previous data will be freed from memory.
        /// </param>
        /// <remarks>
        /// The old data should only not be freed if it is used somewhere
        /// else, which is generally not recommended without good reason.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="length"/> is less than or equal to zero.
        /// </exception>
        protected virtual void ResetData(int length, bool free)
        {
            // Ensure a non-negative length.
            if (length < 0)
                throw new ArgumentException(Resources.ErrorLengthNegative);

            // Free the old data if prompted to do so.
            if (free)
                this.Data.Free();

            // Create the new data.
            this.Data = Pointer.CreatePointer(length);
        }

        /// <summary>
        /// Raises the <see cref="DataReset"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnDataReset(EventArgs e)
        {
            if (this.FileDataType == FileDataTypes.ProgramCreated)
                CreateUntitledFileName();

            if (DataReset != null)
                DataReset(this, e);

            OnVisibleChange(EventArgs.Empty);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when <see cref="Data"/> is reset.
        /// </summary>
        [Category("Data")]
        [Description("Occurs when data of the editor is reset.")]
        public event EventHandler DataReset;
        #endregion
    }
}