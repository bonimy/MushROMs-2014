using System;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Editors;
using MushROMs.Unmanaged;

namespace MushROMs.Controls
{
    public interface IEditorControl
    {
        event EventHandler EditorChanged;

        event EventHandler WritePixels;

        event MouseEventHandler TileMouseClick;
        event MouseEventHandler TileMouseDoubleClick;

        Editor Editor { get; set; }

        EditorHScrollBar EditorHScrollBar { get; set; }
        EditorVScrollBar EditorVScrollBar { get; set; }

        ContextMenuStrip ContextMenuStrip { get; set; }

        void Redraw();

        Size ClientSize { get; }

        Pointer Scan0 { get; }
    }
}