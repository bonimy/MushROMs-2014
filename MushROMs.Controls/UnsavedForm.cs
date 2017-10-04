using System.Collections.Generic;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    public partial class UnsavedForm : Form
    {
        public List<string> Files
        {
            get
            {
                List<string> files = new List<string>();
                for (int i = 0; i < this.lbxFiles.Items.Count; i++)
                    files.Add((string)this.lbxFiles.Items[i]);
                return files;
            }
            set
            {
                this.lbxFiles.Items.Clear();
                this.lbxFiles.Items.AddRange(value.ToArray());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsavedForm"/> class.
        /// </summary>
        public UnsavedForm()
        {
            InitializeComponent();
        }
    }
}