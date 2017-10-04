using System;
using System.Windows.Forms;
using MushROMs.SNES;

namespace MushROMs.SNESControls.GFXEditor
{
    /// <summary>
    /// Represents a common dialog box that enables the user
    /// to create a custom <see cref="GFX"/>.
    /// This class cannot be inherited.
    /// </summary>
    public sealed partial class CreateGFXForm : Form
    {
        #region Constant and read-only fields
        /// <summary>
        /// The fallback number of tiles.
        /// This field is constant.
        /// </summary>
        private const int FallbackNumTiles = 0x100;

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
        /// Gets or sets the number of tiles of the new <see cref="GFX"/>.
        /// </summary>
        public int NumTiles
        {
            get { return (int)this.nudNumTiles.Value; }
            set { this.nudNumTiles.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the option to create
        /// a <see cref="GFX"/> from copy data will be enabled.
        /// </summary>
        public bool EnableCopyOption
        {
            get { return this.chkFromCopy.Enabled; }
            set { this.chkFromCopy.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the new
        /// <see cref="GFX"/> will come from copy data.
        /// </summary>
        public bool CopyFrom
        {
            get { return this.chkFromCopy.Enabled && this.chkFromCopy.Checked; }
            set { this.chkFromCopy.Checked = this.chkFromCopy.Enabled = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGFXForm"/> class.
        /// </summary>
        public CreateGFXForm()
        {
            InitializeComponent();
            Reset();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets the <see cref="CreateGFXForm"/> to its default values.
        /// </summary>
        public void Reset()
        {
            this.NumTiles = FallbackNumTiles;
            this.EnableCopyOption = FallbackCopyOption;
        }

        private void FromCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.gbxNumColors.Enabled = !chkFromCopy.Checked;
        }
        #endregion
    }
}
