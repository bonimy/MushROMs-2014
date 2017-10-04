using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents the base <see cref="Form"/> for the <see cref="EditorMdiForm"/>.
    /// </summary>
    public class MdiForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdiForm"/> class.
        /// </summary>
        public MdiForm()
        {
            this.AllowDrop = true;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
        }
    }
}