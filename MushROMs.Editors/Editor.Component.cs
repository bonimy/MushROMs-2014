/* Deals with the component model of the editor.
 * Editors have a component design. This file
 * implements the necessary interface logic.
 * */

using System;
using System.ComponentModel;

namespace MushROMs.Editors
{
    partial class Editor : IComponent, IDisposable
    {
        #region Fields
        /// <summary>
        /// A value that detrmines whethere this instance has already been
        /// disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The <see cref="ISite"/> associated with the <see cref="Editor"/>.
        /// </summary>
        private ISite site;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="ISite"/> associated with the <see cref="Editor"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISite Site
        {
            get { return this.site; }
            set { this.site = value; }
        }
        #endregion

        #region Destructors
        /// <summary>
        /// Releases the unmanaged resources of the <see cref="Editor"/>.
        /// </summary>
        ~Editor()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Releases all resources used by the <see cref="Editor."/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Editor"/> and optionally
        /// releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                lock (this)
                {
                    if (site != null && site.Container != null)
                        site.Container.Remove(this);
                    Disposed(this, EventArgs.Empty);
                }
            }

            this.Data.Free();
            disposed = true;
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="Editor"/> is disposed by a call to the
        /// <see cref="Dispose"/>() method.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler Disposed;
        #endregion
    }
}
