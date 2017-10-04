using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.Editors;
using MushROMs.SNES;
using MushROMs.SNESControls.PaletteEditor;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor.PaletteEditor
{
    public partial class PaletteMdiForm : EditorMdiForm
    {
        #region Constant and read-only fields
        private const PaletteFileFormats FallbackFileFormat = PaletteFileFormats.TPL;
        #endregion

        #region Fields
        private static PaletteSettingsForm SettingsForm;
        #endregion

        #region Properties
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaletteForm CurrentEditor
        {
            get { return (PaletteForm)base.CurrentEditor; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Status
        {
            set { this.tssMain.Text = value; }
        }
        #endregion

        #region Constructors
        public PaletteMdiForm()
        {
            InitializeComponent();

            this.MenuComponents = new PaletteMenuComponents(this, this.mnuMain, this.tlsMain);

            SettingsForm = new PaletteSettingsForm();
            SettingsForm.SettingsCustomized += new EventHandler(SettingsChanged);

            this.OpenFileDialog.DefaultExt = Palette.GetExtension(FallbackFileFormat);
            this.OpenFileDialog.Filter = Palette.CreateFilter(PaletteFileFormats.None);
            this.OpenFileDialog.Title = Properties.Resources.OpenPaletteTitle;
        }
        #endregion

        #region Methods
        public override void NewEditorForm()
        {
            CreatePaletteForm dlg = new CreatePaletteForm();
            dlg.EnableCopyOption = this.CopyData != null;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.CopyFrom)
                    NewEditorForm(this.CopyData);
                else
                    NewEditorForm(dlg.NumColors);
            }
        }

        public void NewEditorForm(IEditorData data)
        {
            PaletteForm form = (PaletteForm)InitializeNewEditor();
            form.Palette.Initialize(data);
            OnEditorFormAdded(new EditorFormEventArgs(form));
        }

        public void NewEditorForm(int numColors)
        {
            PaletteForm form = (PaletteForm)InitializeNewEditor();
            form.Palette.Initialize(numColors);
            OnEditorFormAdded(new EditorFormEventArgs(form));
        }

        protected override EditorForm InitializeNewEditor()
        {
            PaletteForm form = new PaletteForm();
            form.MainEditorControl.WritePixels += new EventHandler(EditorControl_WritePixels);
            return form;
        }

        private void EditorControl_WritePixels(object sender, EventArgs e)
        {
            PaletteControl control = (PaletteControl)sender;
            control.Palette.Draw(control.Scan0.Data);
        }

        public override void CustomizeSettings()
        {
            if (SettingsForm.ShowDialog() == DialogResult.OK)
                MushROMs.SNESControls.Properties.Settings.Default.Save();
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            RedrawAllEditorForms();
        }

        private void PaletteMdiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
        #endregion
    }
}