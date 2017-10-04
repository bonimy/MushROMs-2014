using System;
using System.ComponentModel;
using MushROMs.Unmanaged;
using MushROMs.SNES.Properties;

namespace MushROMs.SNES
{
    unsafe partial class GFX
    {
        public event EventHandler ArrangeFormatChanged;

        private ArrangeFormats arrangeFormat;
        private Pointer adf;

        public ArrangeFormats ArrangeFormat
        {
            get { return this.arrangeFormat; }
            set { SetArrangeFormat(value); }
        }

        public Pointer ADF
        {
            get { return this.adf; }
        }

        protected override void OnViewSizeChanged(EventArgs e)
        {
            SetADF();
            base.OnViewSizeChanged(e);
        }

        protected virtual void SetADF()
        {
            this.adf = Pointer.CreatePointer(this.NumViewTiles * sizeof(int));
            SetArrangeFormat(this.arrangeFormat);
        }

        private void SetArrangeFormat(ArrangeFormats arrangeFormat)
        {
            int* tiles = (int*)this.adf.Data;
            switch (arrangeFormat)
            {
                case ArrangeFormats.Custom:
                case ArrangeFormats.Horizontal:
                    for (int i = this.NumViewTiles; --i >= 0; )
                        tiles[i] = i;
                    break;
                case ArrangeFormats.Vertical:
                    for (int y = this.ViewHeight; --y >= 0; )
                        for (int x = this.ViewWidth; --x >= 0; )
                            tiles[(y * this.ViewWidth) + x] = (x * this.ViewHeight) + y;
                    break;
                case ArrangeFormats.ADF16x16A:
                    for (int y = 0; y < this.ViewHeight; y += 2)
                    {
                        for (int x = 0; x < this.ViewWidth; x += 2)
                        {
                            tiles[(y * this.ViewWidth) + x] = (y * this.ViewWidth) + (x * 4);
                            tiles[(y * this.ViewWidth) + x + 1] = (y * this.ViewWidth) + (x * 4) + 1;
                            tiles[((y + 1) * this.ViewWidth) + x] = (y * this.ViewWidth) + (x * 4) + 2;
                            tiles[((y + 1) * this.ViewWidth) + x + 2] = (y * this.ViewWidth) + (x * 4) + 3;
                        }
                        if (this.ViewWidth % 2 != 0)
                        {
                            tiles[((y + 1) * this.ViewWidth) - 1] = ((y + 2) * this.ViewWidth) - 2;
                            tiles[((y + 2) * this.ViewWidth) - 1] = ((y + 2) * this.ViewWidth) - 1;
                        }
                    }
                    if (this.ViewHeight % 2 != 0)
                        for (int x = this.ViewWidth; x > 0 ; --x)
                            tiles[this.NumViewTiles - x] = this.NumViewTiles - x;
                    break;
                case ArrangeFormats.ADF16x16B:
                    for (int y = 0; y < this.ViewHeight; y += 2)
                    {
                        for (int x = 0; x < this.ViewWidth; x += 2)
                        {
                            tiles[(y * this.ViewWidth) + x] = (y * this.ViewWidth) + (x * 4);
                            tiles[((y + 1) * this.ViewWidth) + x] = (y * this.ViewWidth) + (x * 4) + 1;
                            tiles[(y * this.ViewWidth) + x + 1] = (y * this.ViewWidth) + (x * 4) + 2;
                            tiles[((y + 1) * this.ViewWidth) + x + 2] = (y * this.ViewWidth) + (x * 4) + 3;
                        }
                        if (this.ViewWidth % 2 != 0)
                        {
                            tiles[((y + 1) * this.ViewWidth) - 1] = ((y + 2) * this.ViewWidth) - 2;
                            tiles[((y + 2) * this.ViewWidth) - 1] = ((y + 2) * this.ViewWidth) - 1;
                        }
                    }
                    if (this.ViewHeight % 2 != 0)
                        for (int x = this.ViewWidth; x > 0; --x)
                            tiles[this.NumViewTiles - x] = this.NumViewTiles - x;
                    break;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorArrangeFormat);
            }

            this.arrangeFormat = arrangeFormat;
            this.CanScrollSelection = this.arrangeFormat == ArrangeFormats.Horizontal;
        }

        protected virtual void OnArrangeFormatChanged(EventArgs e)
        {
            if (ArrangeFormatChanged != null)
                ArrangeFormatChanged(this, e);
        }
    }

    public enum ArrangeFormats
    {
        Horizontal,
        ADF16x16A,
        ADF16x16B,
        Vertical,
        Custom
    }
}
