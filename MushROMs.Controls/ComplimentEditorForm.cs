using System;
using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Editors;

namespace MushROMs.Controls
{
    /// <summary>
    /// A menuless <see cref="Form"/> that provides functionality for
    /// hosting an additional <see cref="Editor"/> but is not meant to
    /// be the primary focus of the editor (for example a palette viewer
    /// in the GFX editor).
    /// </summary>
    public class ComplementEditorForm : EditorForm
    {
        private const int WS_SYSMENU = 0x80000;

        public ComplementEditorForm()
        {
            this.MinimizeBox =
            this.MaximizeBox = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }
    }
}
