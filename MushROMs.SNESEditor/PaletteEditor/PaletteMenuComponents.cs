using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;
using MushROMs.SNES;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor.PaletteEditor
{
    public partial class PaletteMenuComponents : MenuComponents
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaletteMdiForm EditorMdiForm
        {
            get { return (PaletteMdiForm)base.EditorMdiForm; }
            protected set
            {
                // Avoid redundant setting.
                if (this.EditorMdiForm == value)
                    return;

                if (this.EditorMdiForm != null)
                {
                    this.EditorMdiForm.EditorFormAdded -= EditorMdiForm_EditorFormAdded;
                    this.EditorMdiForm.EditorFormRemoved -= EditorMdiForm_EditorFormRemoved;
                    this.EditorMdiForm.ActiveEditorChanged -= EditorMdiForm_ActiveEditorChanged;
                }

                if ((base.EditorMdiForm = value) != null)
                {
                    this.EditorMdiForm.EditorFormAdded += new EditorFormEventHandler(EditorMdiForm_EditorFormAdded);
                    this.EditorMdiForm.EditorFormRemoved += new EditorFormEventHandler(EditorMdiForm_EditorFormRemoved);
                    this.EditorMdiForm.ActiveEditorChanged += new EditorFormEventHandler(EditorMdiForm_ActiveEditorChanged);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private new PaletteForm CurrentEditor
        {
            get { return this.EditorMdiForm.CurrentEditor; }
        }

        protected new PaletteMenuStrip MenuStrip
        {
            get { return (PaletteMenuStrip)base.MenuStrip; }
            set
            {
                if (base.MenuStrip != null)
                {
                    RemoveMenuItemEvent(this.MenuStrip.TsmInvert, Invert_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmColorize, Colorize_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmBlend, Blend_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmGrayscale, Grayscale_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmHorizontalGradient, HorizontalGradient_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmVerticalGradient, VerticalGradient_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmEditBackColor, EditBackColor_Click);
                }

                if ((base.MenuStrip = value) != null)
                {
                    AddMenuItemEvent(this.MenuStrip.TsmInvert, Invert_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmColorize, Colorize_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmBlend, Blend_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmGrayscale, Grayscale_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmHorizontalGradient, HorizontalGradient_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmVerticalGradient, VerticalGradient_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmEditBackColor, EditBackColor_Click);
                }
            }
        }

        protected new PaletteToolStrip ToolStrip
        {
            get { return (PaletteToolStrip)base.ToolStrip; }
            set
            {
                if (base.ToolStrip != null)
                {
                    RemoveMenuItemEvent(this.ToolStrip.TsbInvert, Invert_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbColorize, Colorize_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbBlend, Blend_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbGrayscale, Grayscale_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbHorizontalGradient, HorizontalGradient_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbVerticalGradient, VerticalGradient_Click);
                }

                if ((base.ToolStrip = value) != null)
                {
                    AddMenuItemEvent(this.ToolStrip.TsbInvert, Invert_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbColorize, Colorize_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbBlend, Blend_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbGrayscale, Grayscale_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbHorizontalGradient, HorizontalGradient_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbVerticalGradient, VerticalGradient_Click);
                }
            }
        }

        protected override int MaxRecentFiles
        {
            get { return Settings.Default.MaxRecentFiles; }
        }

        public override StringCollection RecentFiles
        {
            get { return Settings.Default.LastPaletteFiles; }
        }

        public PaletteMenuComponents(PaletteMdiForm editorMdiForm, PaletteMenuStrip menuStrip, PaletteToolStrip toolStrip)
        {
            this.EditorMdiForm = editorMdiForm;
            this.MenuStrip = menuStrip;
            this.ToolStrip = toolStrip;
            this.EditorMdiForm.MainMenuStrip = menuStrip;
            DisplayRecentFiles();
            ToggleToolStripItemsEnabled(false);
        }

        protected override void ToggleToolStripItemsEnabled(bool enabled)
        {
            ToolStripItem[] items = {
                this.MenuStrip.TsmInvert, this.MenuStrip.TsmColorize,
                this.MenuStrip.TsmBlend, this.MenuStrip.TsmGrayscale,
                this.MenuStrip.TsmHorizontalGradient, this.MenuStrip.TsmVerticalGradient,
                this.MenuStrip.TsmEditBackColor,
                this.ToolStrip.TsbInvert, this.ToolStrip.TsbColorize,
                this.ToolStrip.TsbBlend, this.ToolStrip.TsbGrayscale,
                this.ToolStrip.TsbHorizontalGradient, this.ToolStrip.TsbVerticalGradient };


            for (int i = items.Length; --i >= 0; )
                if (items[i] != null)
                    items[i].Enabled = enabled;

            base.ToggleToolStripItemsEnabled(enabled);
        }

        protected override void SetGoToEnabled()
        {
            ToolStripItem tsm = this.MenuStrip.TsmGoTo;
            if (tsm == null)
                return;

            if (this.CurrentEditor == null || this.EditorMdiForm.Editors.Count == 0)
            {
                tsm.Enabled = false;
                return;
            }

            Palette palette = this.CurrentEditor.Palette;
            tsm.Enabled = palette.FileFormat == PaletteFileFormats.SNES;
        }

        protected override EditorContextMenuStrip CreateContextMenu()
        {
            PaletteContextMenuStrip cms = new PaletteContextMenuStrip();
            AddMenuItemEvent(cms.TsmInvert, Invert_Click);
            AddMenuItemEvent(cms.TsmColorize, Colorize_Click);
            AddMenuItemEvent(cms.TsmBlend, Blend_Click);
            AddMenuItemEvent(cms.TsmGrayscale, Grayscale_Click);
            AddMenuItemEvent(cms.TsmHorizontalGradient, HorizontalGradient_Click);
            AddMenuItemEvent(cms.TsmVerticalGradient, VerticalGradient_Click);
            return cms;
        }

        private void Palette_BackColorChanged(object sender, EventArgs e)
        {
            SetBackColorImage();
        }

        private void EditorMdiForm_EditorFormAdded(object sender, EditorFormEventArgs e)
        {
            ((Palette)e.EditorForm.MainEditorControl.Editor).BackColorChanged += new EventHandler(Palette_BackColorChanged);
        }

        private void EditorMdiForm_EditorFormRemoved(object sender, EditorFormEventArgs e)
        {
            if (this.EditorMdiForm.Editors.Count == 0)
                this.MenuStrip.TsmEditBackColor.Image = null;
        }

        private void EditorMdiForm_ActiveEditorChanged(object sender, EditorFormEventArgs e)
        {
            SetGoToEnabled();
            SetBackColorImage();
        }

        private void SetBackColorImage()
        {
            const int height = 0x10;
            const int width = height;

            Color color = LC.SNESToSystemColor(this.CurrentEditor.Palette.BackColor);
            color = Color.FromArgb(0xFF, color);

            Bitmap bmp = new Bitmap(height, width);
            Graphics g = Graphics.FromImage(bmp);

            Rectangle rect = new Rectangle(0, 0, height - 1, width - 1);
            g.FillRectangle(new SolidBrush(color), rect);
            g.DrawRectangle(Pens.Black, rect);

            this.MenuStrip.TsmEditBackColor.Image = bmp;
        }

        private void Invert_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Palette.Invert();
        }

        private void Colorize_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Colorize();
        }

        private void Grayscale_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Grayscale();
        }

        public void Blend_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Blend();
        }

        private void HorizontalGradient_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Palette.Gradient(GradientStyles.Horizontal);
        }

        private void VerticalGradient_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Palette.Gradient(GradientStyles.Vertical);
        }

        private void EditBackColor_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.EditBackColor();
        }
    }
}