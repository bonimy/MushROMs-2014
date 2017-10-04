namespace MushROMs.SNESEditor.PaletteEditor
{
    partial class PaletteMenuStrip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmGoTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInvert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmColorize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBlend = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHorizontalGradient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVerticalGradient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEditBackColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCustomize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNew,
            this.tsmOpen,
            this.tsmOpenRecent,
            this.toolStripSeparator1,
            this.tsmSave,
            this.tsmSaveAs,
            this.tsmSaveAll,
            this.toolStripSeparator2,
            this.tsmExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmNew
            // 
            this.tsmNew.Image = global::MushROMs.SNESEditor.Properties.Resources.DocumentHS;
            this.tsmNew.Name = "tsmNew";
            this.tsmNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmNew.Size = new System.Drawing.Size(187, 22);
            this.tsmNew.Text = "&New";
            this.tsmNew.ToolTipText = "Create new Palette file.";
            // 
            // tsmOpen
            // 
            this.tsmOpen.Image = global::MushROMs.SNESEditor.Properties.Resources.openHS;
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmOpen.Size = new System.Drawing.Size(187, 22);
            this.tsmOpen.Text = "&Open";
            this.tsmOpen.ToolTipText = "Open existing Palette file.";
            // 
            // tsmOpenRecent
            // 
            this.tsmOpenRecent.Name = "tsmOpenRecent";
            this.tsmOpenRecent.Size = new System.Drawing.Size(187, 22);
            this.tsmOpenRecent.Text = "Open &Recent";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // tsmSave
            // 
            this.tsmSave.Image = global::MushROMs.SNESEditor.Properties.Resources.saveHS;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(187, 22);
            this.tsmSave.Text = "&Save";
            this.tsmSave.ToolTipText = "Save current Palette to file.";
            // 
            // tsmSaveAs
            // 
            this.tsmSaveAs.Name = "tsmSaveAs";
            this.tsmSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.tsmSaveAs.Size = new System.Drawing.Size(187, 22);
            this.tsmSaveAs.Text = "Save &As";
            this.tsmSaveAs.ToolTipText = "Save current Palette to new path.";
            // 
            // tsmSaveAll
            // 
            this.tsmSaveAll.Image = global::MushROMs.SNESEditor.Properties.Resources.SaveAllHS;
            this.tsmSaveAll.Name = "tsmSaveAll";
            this.tsmSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tsmSaveAll.Size = new System.Drawing.Size(187, 22);
            this.tsmSaveAll.Text = "Save A&ll";
            this.tsmSaveAll.ToolTipText = "Save all open Palettes.";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(187, 22);
            this.tsmExit.Text = "E&xit";
            this.tsmExit.ToolTipText = "Exit the application.";
            // 
            // tsmEdit
            // 
            this.tsmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUndo,
            this.tsmRedo,
            this.toolStripSeparator3,
            this.tsmCut,
            this.tsmCopy,
            this.tsmPaste,
            this.tsmDelete,
            this.toolStripSeparator5,
            this.tsmGoTo});
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(39, 20);
            this.tsmEdit.Text = "&Edit";
            // 
            // tsmUndo
            // 
            this.tsmUndo.Image = global::MushROMs.SNESEditor.Properties.Resources.Edit_UndoHS;
            this.tsmUndo.Name = "tsmUndo";
            this.tsmUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmUndo.Size = new System.Drawing.Size(157, 22);
            this.tsmUndo.Text = "&Undo";
            this.tsmUndo.ToolTipText = "Undo last action.";
            // 
            // tsmRedo
            // 
            this.tsmRedo.Image = global::MushROMs.SNESEditor.Properties.Resources.Edit_RedoHS;
            this.tsmRedo.Name = "tsmRedo";
            this.tsmRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmRedo.Size = new System.Drawing.Size(157, 22);
            this.tsmRedo.Text = "&Redo";
            this.tsmRedo.ToolTipText = "Redo last action.";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // tsmCut
            // 
            this.tsmCut.Image = global::MushROMs.SNESEditor.Properties.Resources.CutHS;
            this.tsmCut.Name = "tsmCut";
            this.tsmCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmCut.Size = new System.Drawing.Size(157, 22);
            this.tsmCut.Text = "Cu&t";
            this.tsmCut.ToolTipText = "Cut selection data.";
            // 
            // tsmCopy
            // 
            this.tsmCopy.Image = global::MushROMs.SNESEditor.Properties.Resources.CopyHS;
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCopy.Size = new System.Drawing.Size(157, 22);
            this.tsmCopy.Text = "&Copy";
            this.tsmCopy.ToolTipText = "Copy selection data.";
            // 
            // tsmPaste
            // 
            this.tsmPaste.Image = global::MushROMs.SNESEditor.Properties.Resources.PasteHS;
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmPaste.Size = new System.Drawing.Size(157, 22);
            this.tsmPaste.Text = "&Paste";
            this.tsmPaste.ToolTipText = "Paste copy data to index.";
            // 
            // tsmDelete
            // 
            this.tsmDelete.Image = global::MushROMs.SNESEditor.Properties.Resources.DeleteHS;
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmDelete.Size = new System.Drawing.Size(157, 22);
            this.tsmDelete.Text = "&Delete";
            this.tsmDelete.ToolTipText = "Delete selection.";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(154, 6);
            // 
            // tsmGoTo
            // 
            this.tsmGoTo.Enabled = false;
            this.tsmGoTo.Name = "tsmGoTo";
            this.tsmGoTo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.tsmGoTo.Size = new System.Drawing.Size(157, 22);
            this.tsmGoTo.Text = "&Go To...";
            this.tsmGoTo.ToolTipText = "Go to address.";
            // 
            // tsmTools
            // 
            this.tsmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmInvert,
            this.tsmColorize,
            this.tsmBlend,
            this.tsmGrayscale,
            this.tsmHorizontalGradient,
            this.tsmVerticalGradient,
            this.toolStripSeparator4,
            this.tsmEditBackColor});
            this.tsmTools.Name = "tsmTools";
            this.tsmTools.Size = new System.Drawing.Size(48, 20);
            this.tsmTools.Text = "&Tools";
            // 
            // tsmInvert
            // 
            this.tsmInvert.Image = global::MushROMs.SNESEditor.Properties.Resources.mnuInvert;
            this.tsmInvert.Name = "tsmInvert";
            this.tsmInvert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmInvert.Size = new System.Drawing.Size(220, 22);
            this.tsmInvert.Text = "&Invert";
            this.tsmInvert.ToolTipText = "Invert colors of the selection.";
            // 
            // tsmColorize
            // 
            this.tsmColorize.Image = global::MushROMs.SNESEditor.Properties.Resources.mnuColorize;
            this.tsmColorize.Name = "tsmColorize";
            this.tsmColorize.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.tsmColorize.Size = new System.Drawing.Size(220, 22);
            this.tsmColorize.Text = "&Colorize";
            this.tsmColorize.ToolTipText = "Colorize the Palette selection.";
            // 
            // tsmBlend
            // 
            this.tsmBlend.Image = global::MushROMs.SNESEditor.Properties.Resources.mnuBlend;
            this.tsmBlend.Name = "tsmBlend";
            this.tsmBlend.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tsmBlend.Size = new System.Drawing.Size(220, 22);
            this.tsmBlend.Text = "&Blend";
            this.tsmBlend.ToolTipText = "Blend the Palette selection.";
            // 
            // tsmGrayscale
            // 
            this.tsmGrayscale.Image = global::MushROMs.SNESEditor.Properties.Resources.mnuGrayscale;
            this.tsmGrayscale.Name = "tsmGrayscale";
            this.tsmGrayscale.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.tsmGrayscale.Size = new System.Drawing.Size(220, 22);
            this.tsmGrayscale.Text = "&Grayscale";
            this.tsmGrayscale.ToolTipText = "Convert the selection to grayscale.";
            // 
            // tsmHorizontalGradient
            // 
            this.tsmHorizontalGradient.Image = global::MushROMs.SNESEditor.Properties.Resources.mnuHorizontalGradient;
            this.tsmHorizontalGradient.Name = "tsmHorizontalGradient";
            this.tsmHorizontalGradient.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.tsmHorizontalGradient.Size = new System.Drawing.Size(220, 22);
            this.tsmHorizontalGradient.Text = "&Horizontal Gradient";
            this.tsmHorizontalGradient.ToolTipText = "Create a horizontal gradient within the selection.";
            // 
            // tsmVerticalGradient
            // 
            this.tsmVerticalGradient.Image = global::MushROMs.SNESEditor.Properties.Resources.mnuVerticalGradient;
            this.tsmVerticalGradient.Name = "tsmVerticalGradient";
            this.tsmVerticalGradient.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.tsmVerticalGradient.Size = new System.Drawing.Size(220, 22);
            this.tsmVerticalGradient.Text = "&Vertical Gradient";
            this.tsmVerticalGradient.ToolTipText = "Create a vertical gradient within the selection.";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(217, 6);
            // 
            // tsmEditBackColor
            // 
            this.tsmEditBackColor.Name = "tsmEditBackColor";
            this.tsmEditBackColor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmEditBackColor.Size = new System.Drawing.Size(220, 22);
            this.tsmEditBackColor.Text = "Edit &Back Color";
            this.tsmEditBackColor.ToolTipText = "Edit the back color (only applies to MW3 files).";
            // 
            // tsmOptions
            // 
            this.tsmOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCustomize});
            this.tsmOptions.Name = "tsmOptions";
            this.tsmOptions.Size = new System.Drawing.Size(61, 20);
            this.tsmOptions.Text = "&Options";
            // 
            // tsmCustomize
            // 
            this.tsmCustomize.Name = "tsmCustomize";
            this.tsmCustomize.Size = new System.Drawing.Size(147, 22);
            this.tsmCustomize.Text = "&Customize";
            this.tsmCustomize.ToolTipText = "Customize the Palette Editor settings.";
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.tsmAbout.Size = new System.Drawing.Size(126, 22);
            this.tsmAbout.Text = "&About";
            this.tsmAbout.ToolTipText = "About the Palette Editor.";
            // 
            // PaletteMenuStrip
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmEdit,
            this.tsmTools,
            this.tsmOptions,
            this.tsmHelp});
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmTools;
        private System.Windows.Forms.ToolStripMenuItem tsmOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmNew;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAs;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmCut;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmInvert;
        private System.Windows.Forms.ToolStripMenuItem tsmColorize;
        private System.Windows.Forms.ToolStripMenuItem tsmBlend;
        private System.Windows.Forms.ToolStripMenuItem tsmGrayscale;
        private System.Windows.Forms.ToolStripMenuItem tsmHorizontalGradient;
        private System.Windows.Forms.ToolStripMenuItem tsmVerticalGradient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmEditBackColor;
        private System.Windows.Forms.ToolStripMenuItem tsmCustomize;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmGoTo;
    }
}
