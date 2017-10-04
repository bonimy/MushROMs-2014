using System;
using System.Windows.Forms;
using MushROMs.SNES;

namespace MushROMs.SNESControls.PaletteEditor
{
    /// <summary>
    /// Represents a common dialog box that enables the user
    /// to create a custom <see cref="Palette"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed partial class CreatePaletteForm : Form
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback number of colors.
        /// This field is constant.
        /// </summary>
        private const int FallbackNumColors = 0x100;

        /// <summary>
        /// The fallback copy option value.
        /// This field is constant.
        /// </summary>
        private const bool FallbackCopyOption = true;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the dialog box title.
        /// </summary>
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        /// <summary>
        /// Gets or sets the number of colors of the new <see cref="Palette"/>.
        /// </summary>
        public int NumColors
        {
            get { return (int)this.nudNumColors.Value; }
            set { this.nudNumColors.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the option to create
        /// a <see cref="Palette"/> from copy data will be enabled.
        /// </summary>
        public bool EnableCopyOption
        {
            get { return this.chkFromCopy.Enabled; }
            set { this.chkFromCopy.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the new
        /// <see cref="Palette"/> will come from copy data.
        /// </summary>
        public bool CopyFrom
        {
            get { return this.chkFromCopy.Enabled && this.chkFromCopy.Checked; }
            set { this.chkFromCopy.Checked = this.chkFromCopy.Enabled = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePaletteForm"/> class.
        /// </summary>
        public CreatePaletteForm()
        {
            InitializeComponent();
            Reset();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets the <see cref="CreatePaletteForm"/> to its default values.
        /// </summary>
        public void Reset()
        {
            this.NumColors = FallbackNumColors;
            this.EnableCopyOption = FallbackCopyOption;
        }

        private void FromCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.gbxNumColors.Enabled = !chkFromCopy.Checked;
        }
        #endregion
    }
}