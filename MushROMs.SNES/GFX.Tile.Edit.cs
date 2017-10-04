using System;
using System.Collections.Generic;
using System.Text;
using MushROMs.Editors;

namespace MushROMs.SNES
{
    partial class GFX
    {
        partial class Tile
        {
            public override IEditorData CreateCopy(ISelection selection)
            {
                throw new NotImplementedException();
            }

            public override void Paste(IEditorData data)
            {
                throw new NotImplementedException();
            }

            public override void DeleteSelection()
            {
                throw new NotImplementedException();
            }

            protected override void WriteCopyData(IEditorData data, int StartAddress)
            {
                throw new NotImplementedException();
            }
        }
    }
}
