using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNES;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor.GFXEditor
{
    public class GFXMenuComponents : MenuComponents
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new GFXMdiForm EditorMdiForm
        {
            get { return (GFXMdiForm)base.EditorMdiForm; }
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
        private new GFXForm CurrentEditor
        {
            get { return this.EditorMdiForm.CurrentEditor; }
        }

        protected new GFXMenuStrip MenuStrip
        {
            get { return (GFXMenuStrip)base.MenuStrip; }
            set
            {
                // Avoid redundant setting.
                if (base.MenuStrip == value)
                    return;

                if (base.MenuStrip != null)
                {
                    RemoveMenuItemEvent(this.MenuStrip.TsmSendToTileEditor, SendToTileEditor_Click);

                    RemoveMenuItemEvent(this.MenuStrip.TsmRotateRight90, RotateRight90_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmRotateLeft90, RotateLeft90_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmRotate180, Rotate180_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmFlipVertical, FlipVertical_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmFlipHorizontal, FlipHorizontal_Click);

                    RemoveMenuItemEvent(this.MenuStrip.TsmRotateTilesRight90, RotateTilesRight90_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmRotateTilesLeft90, RotateTilesLeft90_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmRotateTiles180, RotateTiles180_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmFlipTilesVertical, FlipTilesVertical_Click);
                    RemoveMenuItemEvent(this.MenuStrip.TsmFlipTilesHorizontal, FlipTilesHorizontal_Click);
                }

                if ((base.MenuStrip = value) != null)
                {
                    AddMenuItemEvent(this.MenuStrip.TsmSendToTileEditor, SendToTileEditor_Click);

                    AddMenuItemEvent(this.MenuStrip.TsmRotateRight90, RotateRight90_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmRotateLeft90, RotateLeft90_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmRotate180, Rotate180_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmFlipVertical, FlipVertical_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmFlipHorizontal, FlipHorizontal_Click);

                    AddMenuItemEvent(this.MenuStrip.TsmRotateTilesRight90, RotateTilesRight90_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmRotateTilesLeft90, RotateTilesLeft90_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmRotateTiles180, RotateTiles180_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmFlipTilesVertical, FlipTilesVertical_Click);
                    AddMenuItemEvent(this.MenuStrip.TsmFlipTilesHorizontal, FlipTilesHorizontal_Click);
                }
            }
        }

        protected new GFXToolStrip ToolStrip
        {
            get { return (GFXToolStrip)base.ToolStrip; }
            set
            {
                // Avoid redundant setting.
                if (base.ToolStrip == value)
                    return;

                if (base.ToolStrip != null)
                {
                    RemoveMenuItemEvent(this.ToolStrip.TsbRotateRight90, RotateRight90_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbRotateLeft90, RotateLeft90_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbRotate180, Rotate180_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbFlipVertical, FlipVertical_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbFlipHorizontal, FlipHorizontal_Click);

                    RemoveMenuItemEvent(this.ToolStrip.TsbRotateTilesRight90, RotateTilesRight90_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbRotateTilesLeft90, RotateTilesLeft90_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbRotateTiles180, RotateTiles180_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbFlipTilesVertical, FlipTilesVertical_Click);
                    RemoveMenuItemEvent(this.ToolStrip.TsbFlipTilesHorizontal, FlipTilesHorizontal_Click);
                }

                if ((base.ToolStrip = value) != null)
                {
                    AddMenuItemEvent(this.ToolStrip.TsbRotateRight90, RotateRight90_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbRotateLeft90, RotateLeft90_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbRotate180, Rotate180_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbFlipVertical, FlipVertical_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbFlipHorizontal, FlipHorizontal_Click);

                    AddMenuItemEvent(this.ToolStrip.TsbRotateTilesRight90, RotateTilesRight90_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbRotateTilesLeft90, RotateTilesLeft90_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbRotateTiles180, RotateTiles180_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbFlipTilesVertical, FlipTilesVertical_Click);
                    AddMenuItemEvent(this.ToolStrip.TsbFlipTilesHorizontal, FlipTilesHorizontal_Click);
                }
            }
        }

        protected override int MaxRecentFiles
        {
            get { return Settings.Default.MaxRecentFiles; }
        }

        public override StringCollection RecentFiles
        {
            get { return Settings.Default.LastGFXFiles; }
        }

        public GFXMenuComponents(GFXMdiForm gfxMdiForm, GFXMenuStrip menuStrip, GFXToolStrip toolStrip)
        {
            this.EditorMdiForm = gfxMdiForm;
            this.MenuStrip = menuStrip;
            this.ToolStrip = toolStrip;
            DisplayRecentFiles();
            ToggleToolStripItemsEnabled(false);
        }

        protected override void ToggleToolStripItemsEnabled(bool enabled)
        {
            ToolStripItem[] items = {
                this.MenuStrip.TsmSendToTileEditor,
                this.MenuStrip.TsmRotateRight90, this.MenuStrip.TsmRotateLeft90, this.MenuStrip.TsmRotate180,
                this.MenuStrip.TsmFlipVertical, this.MenuStrip.TsmFlipHorizontal,
                this.MenuStrip.TsmRotateTilesRight90, this.MenuStrip.TsmRotateTilesLeft90, this.MenuStrip.TsmRotateTiles180,
                this.MenuStrip.TsmFlipTilesVertical, this.MenuStrip.TsmFlipTilesHorizontal,
                this.ToolStrip.TsbRotateRight90, this.ToolStrip.TsbRotateLeft90, this.ToolStrip.TsbRotate180,
                this.ToolStrip.TsbFlipVertical, this.ToolStrip.TsbFlipHorizontal,
                this.ToolStrip.TsbRotateTilesRight90, this.ToolStrip.TsbRotateTilesLeft90, this.ToolStrip.TsbRotateTiles180,
                this.ToolStrip.TsbFlipTilesVertical, this.ToolStrip.TsbFlipTilesHorizontal };
            
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

            GFX gfx = this.CurrentEditor.GFX;
            tsm.Enabled = gfx.FileFormat == GFXFileFormats.SNES;
        }

        protected override EditorContextMenuStrip CreateContextMenu()
        {
            GFXContextMenuStrip cms = new GFXContextMenuStrip();
            AddMenuItemEvent(cms.TsmSendToTileEditor, SendToTileEditor_Click);

            AddMenuItemEvent(cms.TsmRotateRight90, RotateRight90_Click);
            AddMenuItemEvent(cms.TsmRotateLeft90, RotateLeft90_Click);
            AddMenuItemEvent(cms.TsmRotate180, Rotate180_Click);
            AddMenuItemEvent(cms.TsmFlipVertical, FlipVertical_Click);
            AddMenuItemEvent(cms.TsmFlipHorizontal, FlipHorizontal_Click);

            AddMenuItemEvent(cms.TsmRotateTilesRight90, RotateTilesRight90_Click);
            AddMenuItemEvent(cms.TsmRotateTilesLeft90, RotateTilesLeft90_Click);
            AddMenuItemEvent(cms.TsmRotateTiles180, RotateTiles180_Click);
            AddMenuItemEvent(cms.TsmFlipTilesVertical, FlipTilesVertical_Click);
            AddMenuItemEvent(cms.TsmFlipTilesHorizontal, FlipTilesHorizontal_Click);
            return cms;
        }

        private void EditorMdiForm_EditorFormAdded(object sender, EditorFormEventArgs e)
        {
            
        }

        private void EditorMdiForm_EditorFormRemoved(object sender, EditorFormEventArgs e)
        {
        }

        private void EditorMdiForm_ActiveEditorChanged(object sender, EditorFormEventArgs e)
        {
            SetGoToEnabled();
        }

        private void SendToTileEditor_Click(object sender, EventArgs e)
        {
            this.EditorMdiForm.SendToTileEditor();
        }

        private void RotateRight90_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.RotateRight90();
        }

        private void RotateLeft90_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.RotateLeft90();
        }

        private void Rotate180_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.Rotate180();
        }

        private void FlipVertical_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.FlipVertical();
        }

        private void FlipHorizontal_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.FlipHorizontal();
        }

        private void RotateTilesRight90_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.RotateTilesRight90();
        }

        private void RotateTilesLeft90_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.RotateTilesLeft90();
        }

        private void RotateTiles180_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.RotateTiles180();
        }

        private void FlipTilesVertical_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.FlipTilesVertical();
        }

        private void FlipTilesHorizontal_Click(object sender, EventArgs e)
        {
            if (this.CurrentEditor != null)
                this.CurrentEditor.FlipTilesHorizontal();
        }
    }
}