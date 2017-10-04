using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MushROMs.SNESEditor
{
    public partial class MasterForm : Form
    {
        private PaletteEditor.PaletteMdiForm paletteEditor;
        private GFXEditor.GFXMdiForm gfxEditor;

        public MasterForm()
        {
            InitializeComponent();

            this.paletteEditor = new PaletteEditor.PaletteMdiForm();
            this.paletteEditor.FormClosing += new FormClosingEventHandler(paletteEditor_FormClosing);
            this.paletteEditor.VisibleChanged += new EventHandler(paletteEditor_VisibleChanged);
            AddOwnedForm(this.paletteEditor);

            this.gfxEditor = new GFXEditor.GFXMdiForm();
            this.gfxEditor.VisibleChanged += new EventHandler(gfxEditor_VisibleChanged);
            this.gfxEditor.FormClosing += new FormClosingEventHandler(gfxEditor_FormClosing);
            AddOwnedForm(this.gfxEditor);

            this.tsmGFXEditor.Checked = true;
        }

        private void tsmPaletteEditor_CheckedChanged(object sender, EventArgs e)
        {
            this.paletteEditor.Visible = this.tsmPaletteEditor.Checked;
        }

        private void paletteEditor_VisibleChanged(object sender, EventArgs e)
        {
            this.tsmPaletteEditor.Checked = this.paletteEditor.Visible;
        }

        private void tsmGFXEditor_CheckedChanged(object sender, EventArgs e)
        {
            this.gfxEditor.Visible = this.tsmGFXEditor.Checked;
        }

        private void gfxEditor_VisibleChanged(object sender, EventArgs e)
        {
            this.tsmGFXEditor.Checked = this.gfxEditor.Visible;
        }

        private void paletteEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.paletteEditor.Visible = false;
            }
        }

        private void gfxEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.gfxEditor.Visible = false;
            }
        }
    }
}
