using System.Drawing;

namespace MushROMs.Editors
{
    public interface ISelection
    {
        int StartAddress { get; }

        int Width { get; }
        int Height { get; }
        Size Size { get; }

        int NumTiles { get; }

        int ContainerWidth { get; }

        ISelection Copy();
    }
}
