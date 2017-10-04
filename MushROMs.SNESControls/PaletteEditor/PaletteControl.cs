using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNES;

namespace MushROMs.SNESControls.PaletteEditor
{
    public unsafe partial class PaletteControl : EditorControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Palette Palette
        {
            get { return (Palette)this.Editor; }
        }

        public PaletteControl()
        {
            this.timer = new System.Timers.Timer(200);
            this.timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            this.timer.Start();

            this.Paint += new PaintEventHandler(EditorControl_Paint);
            this.WritePixels += new EventHandler(EditorControl_WritePixels);
        }

        protected override Editors.Editor InitializeEditor()
        {
            return new Palette();
        }
    }
}