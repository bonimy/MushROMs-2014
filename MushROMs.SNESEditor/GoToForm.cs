using System.Windows.Forms;

namespace MushROMs.SNESEditor
{
    public partial class GoToForm : Form
    {
        public int Address
        {
            get { return this.itbAddress.Value; }
            set { this.itbAddress.Value = value; }
        }

        public GoToForm()
        {
            InitializeComponent();
        }
    }
}