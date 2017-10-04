using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.Editors;
using MushROMs.SNES;
using MushROMs.SNESControls.GFXEditor;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor.GFXEditor
{
    public partial class GFXMdiForm : EditorMdiForm
    {
        #region Constant and read-only fields
        private const GFXFileFormats FallbackFileFormat = GFXFileFormats.CHR;
        #endregion

        #region Fields
        private PaletteView paletteView;
        private TileForm tileForm;
        #endregion

        #region Properties
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new GFXForm CurrentEditor
        {
            get { return (GFXForm)base.CurrentEditor; }
        }

        public override string Status
        {
            set { this.tssMain.Text = value; }
        }

        public Palette Palette
        {
            get { return this.paletteView.Palette; }
        }
        #endregion

        #region Constructors
        public GFXMdiForm()
        {
            InitializeComponent();

            this.MenuComponents = new GFXMenuComponents(this, this.mnuMain, this.tlsMain);

            this.OpenFileDialog.DefaultExt = GFX.GetExtension(FallbackFileFormat);
            this.OpenFileDialog.Filter = GFX.CreateFilter(GFXFileFormats.None);
            this.OpenFileDialog.Title = Properties.Resources.OpenGFXTitle;

            this.paletteView = new PaletteView();
            this.paletteView.Palette.Open(Settings.Default.DefaultPalettePath);
            this.paletteView.Palette.DataModified += new EventHandler(RedrawEditors);
            this.paletteView.Palette.SelectedTilesChanged += new EventHandler(RedrawEditors);

            this.paletteView.MdiParent = this;
            this.paletteView.StartPosition = FormStartPosition.Manual;
            this.paletteView.Location = new Point(
                this.ClientSize.Width - this.paletteView.Width, 0);
            this.paletteView.Show();

            this.tileForm = new TileForm();
            this.tileForm.MainEditorControl.WritePixels += new EventHandler(TileControl_WritePixels);
            this.tileForm.MdiParent = this;
            this.tileForm.StartPosition = FormStartPosition.Manual;
            this.tileForm.Location = new Point(
                this.ClientSize.Width - this.tileForm.Width,
                this.ClientSize.Height - this.tileForm.Height);
            this.tileForm.Show();

            this.EditorFormAdded += new EditorFormEventHandler(GFXMdiForm_EditorFormAdded);
        }
        #endregion

        #region Methods
        public override void NewEditorForm()
        {
            CreateGFXForm dlg = new CreateGFXForm();
            dlg.EnableCopyOption = this.CopyData != null;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.CopyFrom)
                    NewEditorForm(this.CopyData);
                else
                    NewEditorForm(dlg.NumTiles);
            }
        }

        public void NewEditorForm(IEditorData data)
        {
            GFXForm form = (GFXForm)InitializeNewEditor();
            form.GFX.Initialize(data);
            OnEditorFormAdded(new EditorFormEventArgs(form));
        }

        public void NewEditorForm(int numTiles)
        {
            GFXForm form = (GFXForm)InitializeNewEditor();
            form.GFX.Initialize(numTiles);
            OnEditorFormAdded(new EditorFormEventArgs(form));
        }

        protected override EditorForm InitializeNewEditor()
        {
            GFXForm form = new GFXForm();
            form.MainEditorControl.WritePixels += new EventHandler(GFXControl_WritePixels);
            return form;
        }

        private void GFXControl_WritePixels(object sender, EventArgs e)
        {
            GFXControl control = (GFXControl)sender;
            int address = this.Palette.Selection.Min.Address;
            address -= this.Palette.Selection.Min.Address % this.Palette.ViewWidth;
            control.GFX.Draw(control.Scan0.Data, this.Palette, address);

        }

        private void TileControl_WritePixels(object sender, EventArgs e)
        {
            int address = this.Palette.Selection.Min.Address;
            address -= this.Palette.Selection.Min.Address % this.Palette.ViewWidth;
            this.tileForm.GFXTiles.Draw(this.tileForm.MainEditorControl.Scan0.Data, this.Palette, address);
        }

        public void SendToTileEditor()
        {
            if (this.CurrentEditor != null)
            {
                this.tileForm.GFXTiles.Initialize(this.CurrentEditor.Editor.CreateCopy());
                this.tileForm.MainEditorControl.Redraw();
            }
        }

        private void RedrawEditors(object sender, EventArgs e)
        {
            RedrawAllEditorForms();
            this.tileForm.MainEditorControl.Redraw();
        }

        private void GFXMdiForm_EditorFormAdded(object sender, EditorFormEventArgs e)
        {
            GFXForm form = (GFXForm)e.EditorForm;
            form.MainEditorControl.TileMouseDoubleClick += new MouseEventHandler(EditorControl_TileMouseDoubleClick);
        }

        private void EditorControl_TileMouseDoubleClick(object sender, EventArgs e)
        {
            SendToTileEditor();
        }
        #endregion
    }
}