using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MushROMs.Unmanaged;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides an empty control with predefined settings for rendering images.
    /// </summary>
    [DefaultEvent("Paint")]
    [DefaultProperty("ClientSize")]
    [Description("Provides a control with predefined settings for rendering images.")]
    public partial class DrawControl : UserControl
    {
        #region Constants and read-only variables
        /// <summary>
        /// The fallback value for drawing the image to the <see cref="DrawControl"/>.
        /// This field is constant.
        /// </summary>
        private const DrawImageWhen FallbackDrawImageWhen = DrawImageWhen.BeforeBeginPaint;

        /// <summary>
        /// The fallback <see cref="UseCustomImageRectange"/> value.
        /// This field is constant.
        /// </summary>
        private const bool FallbackUseCustomImage = false;
        #endregion

        #region Fields
        /// <summary>
        /// A value indicating whether the <see cref="DrawControl"/> should redraw its bitmap.
        /// </summary>
        private bool redraw;
        /// <summary>
        /// A value that determines whether the <see cref="DrawControl"/> is currently redrawing itself.
        /// </summary>
        private bool redrawing;

        /// <summary>
        /// A value that determines whether <see cref="image"/> will have a custom size determined
        /// outside of <see cref="DrawControl"/>.
        /// </summary>
        private bool custom;
        /// <summary>
        /// A value that determines when to draw the prerendered image.
        /// </summary>
        private DrawImageWhen drawImageWhen;
        /// <summary>
        /// The <see cref="Bitmap"/> data of the <see cref="EditorControl"/>.
        /// </summary>
        private Bitmap image;
        /// <summary>
        /// The <see cref="Rectangle"/> of <see cref="image"/>.
        /// </summary>
        private Rectangle imgRect;
        /// <summary>
        /// A <see cref="Pointer"/> that points to an array containing the pixel
        /// data of the <see cref="EditorControl"/>.
        /// </summary>
        private Pointer scan0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value that determines whether <see cref="image"/> will
        /// have a custom size determined outside of <see cref="DrawControl"/>.
        /// </summary>
        [Browsable(true)]
        [Category("Drawing")]
        [DefaultValue(FallbackUseCustomImage)]
        [Description("Determines whether DrawControl has a custom image size.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool UseCustomImageRectange
        {
            get { return this.custom; }
            set { this.custom = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines when to draw the prerendered image.
        /// </summary>
        [Browsable(true)]
        [Category("Drawing")]
        [DefaultValue(FallbackDrawImageWhen)]
        [Description("Determines when to draw the prerendered image")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DrawImageWhen DrawImageWhen
        {
            get { return this.drawImageWhen; }
            set { this.drawImageWhen = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets the <see cref="Bitmap"/> data of the <see cref="EditorControl"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Bitmap UserImage
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Gets the pixel location of <see cref="UserImage"/>.
        /// </summary>
        /// <remarks>
        /// This value is only relevant if <see cref="UseCustomImageRectange"/>
        /// is set to true. Otherwise, it is not used by <see cref="DrawControl"/>.
        /// </remarks>
        [Browsable(true)]
        [Category("Drawing")]
        [Description("The custom location of the DrawControl region.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Point UserImageLocation
        {
            get { return this.imgRect.Location; }
            set { this.imgRect.Location = value; }
        }

        /// <summary>
        /// Gets or sets the size, in pixels, of <see cref="UserImage"/>.
        /// </summary>
        /// <remarks>
        /// This value is only relevant if <see cref="UseCustomImageRectange"/>
        /// is set to true. Otherwise, it is not used by <see cref="DrawControl"/>.
        /// </remarks>
        [Browsable(true)]
        [Category("Drawing")]
        [Description("The custom size of the DrawControl image.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Size UserImageSize
        {
            get { return this.imgRect.Size; }
            set { this.imgRect.Size = value; }
        }

        /// <summary>
        /// Gets or sets the region of <see cref="UserImage"/>.
        /// </summary>
        /// <remarks>
        /// This value is only relevant if <see cref="UseCustomImageRectange"/>
        /// is set to true. Otherwise, it is not used by <see cref="DrawControl"/>.
        /// </remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle UserImageRectangle
        {
            get { return this.imgRect; }
            set { this.imgRect = value; }
        }

        /// <summary>
        /// Gets a <see cref="Pointer"/> that points to an array containing
        /// the pixel data of the <see cref="EditorControl"/>.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pointer Scan0
        {
            get { return this.scan0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control should redraw its surface
        /// using a secondary buffer to reduce or prevent flicker.
        /// </summary>
        [Browsable(true)]   // This accessor is redone so it can be browsable in the designer.
        [Category("Editor")]
        [DefaultValue(true)]
        [Description("A value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool DoubleBuffered
        {
            get { return base.DoubleBuffered; }
            set { base.DoubleBuffered = value; }
        }

        /// <summary>
        /// Gets or sets the height and width of the client area of the control.
        /// </summary>
        [Browsable(true)]   // This accessor is redone so it can be browsable in the designer.
        [Description("The size of the client area of the form.")]
        public new Size ClientSize
        {
            get { return base.ClientSize; }
            set { base.ClientSize = value; }
        }

        /// <summary>
        /// Gets or sets the width of the client area of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ClientWidth
        {
            get { return this.ClientSize.Width; }
            set { this.ClientSize = new Size(value, this.ClientHeight); }
        }

        /// <summary>
        /// Gets or sets the height of the client area of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ClientHeight
        {
            get { return this.ClientSize.Height; }
            set { this.ClientSize = new Size(this.ClientWidth, value); }
        }

        /// <summary>
        /// Gets the height and width of the border around the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size BorderSize
        {
            get
            {
                switch (this.BorderStyle)
                {
                    case BorderStyle.None:
                        return Size.Empty;

                    case BorderStyle.FixedSingle:
                        return SystemInformation.BorderSize;

                    case BorderStyle.Fixed3D:
                        return SystemInformation.Border3DSize;

                    default:    //This should never occur.
                        return Size.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the width of the border around the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderWidth
        {
            get { return this.BorderSize.Width; }
        }

        /// <summary>
        /// Gets the height of the border around the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderHeight
        {
            get { return this.BorderSize.Height; }
        }

        /// <summary>
        /// Gets the total border width around the contol. This is essentially double the <see cref="BorderSize"/> value.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FullBorderWidth
        {
            get { return this.FullBorderSize.Width; }
        }

        /// <summary>
        /// Gets the total border height around the contol. This is essentially double the <see cref="BorderSize"/> value.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FullBorderHeight
        {
            get { return this.FullBorderSize.Height; }
        }

        /// <summary>
        /// Gets the total border size around the contol. This is essentially double the <see cref="BorderSize"/> value.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size FullBorderSize
        {
            get { return this.BorderSize + this.BorderSize; }
        }

        /// <summary>
        /// Gets or sets the border style of the control.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// The assigned value is not one of the <see cref="BorderStyle"/> values.
        /// </exception>
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; OnBorderStyleChanged(EventArgs.Empty); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawControl"/> class.
        /// </summary>
        public DrawControl()
        {
            // Set control properties
            this.DoubleBuffered = true;                 // Removes flickering.
            this.BackColor = Color.Magenta;             // A very noticable default color.
            this.BorderStyle = BorderStyle.FixedSingle; // Basic border style.
            this.Margin = Padding.Empty;                // Most draw controls are placed with no margins desired.
            this.Padding = Padding.Empty;               // There should also be no padding inside the control.
            this.ResizeRedraw = true;                   // Control will always be redrawn when resizing.

            // Set custom paint styles
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // Set custom imaging options
            this.UseCustomImageRectange = FallbackUseCustomImage;
            this.UserImageLocation = Point.Empty;
            this.UserImageSize = this.ClientSize;
            this.DrawImageWhen = FallbackDrawImageWhen;

            // Initialize mouse processing options
            this.ProcessMouseOnChange = FallbackProcessMouseOnChange;
            this.ProcessMouseInRange = FallbackProcessMouseInRange;

            // Initialize pixel data.
            this.scan0 = Pointer.CreatePointer(0);

            // Initialize fallback override input keys
            this.OverrideInputKeys = new Keys[FallbackOverrideInputKeys.Length];
            for (int i = FallbackOverrideInputKeys.Length; --i >= 0; )
                this.OverrideInputKeys[i] = FallbackOverrideInputKeys[i];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tell the control to redraw its image before the next paint event.
        /// </summary>
        public void Redraw()
        {
            this.redraw = true;
            this.Invalidate();
        }

        /// <summary>
        /// Reinitiales <see cref="Scan0"/> pointer and disposes the current <see cref="UserImage"/>.
        /// </summary>
        /// <remarks>
        /// The size of the <see cref="Scan0"/> pointer is determined by the number of pixels in the
        /// client region. Each pixel is 32 bits.
        /// </remarks>
        protected virtual void ResetBitmapPixels()
        {
            // Set number of pixels (32-bit ARGB pixel data)
            int size = sizeof(uint) * this.ClientHeight * this.ClientWidth;
            if (this.UseCustomImageRectange)
                size = sizeof(uint) * this.UserImageSize.Height * this.UserImageSize.Width;

            // Set pixel pointer
            this.Scan0.Resize(size);
        }

        /// <summary>
        /// Writes the <see cref="Scan0"/> pointer to the <see cref="Bitmap"/>.
        /// </summary>
        protected virtual void DrawPixels()
        {
            DrawPixels(false);
        }

        /// <summary>
        /// Writes the <see cref="Scan0"/> pointer to the <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="alpha">
        /// If true, the <see cref="PixelFormat"/> will have an alpha component.
        /// </param>
        protected virtual void DrawPixels(bool alpha)
        {
            // Get size of bitmap
            int width = this.ClientWidth;
            int height = this.ClientHeight;
            if (this.UseCustomImageRectange)
            {
                width = this.UserImageSize.Width;
                height = this.UserImageSize.Height;
            }

            // Create bitmap
            this.image = new Bitmap(width, height, width * 4,
                alpha ? PixelFormat.Format32bppArgb : PixelFormat.Format32bppRgb,
                this.Scan0.Data);
        }

        /// <summary>
        /// Raises the <see cref="BorderStyleChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            if (BorderStyleChanged != null)
                BorderStyleChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="BeginPaint"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="PaintEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnBeginPaint(PaintEventArgs e)
        {
            // Initialize the pixel data.
            if (this.redraw && !this.redrawing)
            {
                ResetBitmapPixels();
                Memory.SetMemory(this.Scan0.Data, 0, this.Scan0.Size);
                OnWritePixels(EventArgs.Empty);
            }

            // Draw the image when it is valid to do so.
            if (this.DrawImageWhen == DrawImageWhen.BeforeBeginPaint)
                DrawUserImageToControl(e.Graphics);

            if (BeginPaint != null)
                BeginPaint(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="PaintEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Stuff to do before main paint event.
            OnBeginPaint(e);

            // Draw the image when it is valid to do so.
            if (this.DrawImageWhen == DrawImageWhen.BeforePaint)
                DrawUserImageToControl(e.Graphics);

            // Main paint event.
            base.OnPaint(e);

            // Draw the image when it is valid to do so.
            if (this.DrawImageWhen == DrawImageWhen.AfterPaint)
                DrawUserImageToControl(e.Graphics);

            // Stuff to do after main paint event.
            OnEndPaint(e);
        }

        /// <summary>
        /// Raises the <see cref="EndPaint"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="PaintEventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnEndPaint(PaintEventArgs e)
        {
            this.redraw =
            this.redrawing = false;

            if (EndPaint != null)
                EndPaint(this, e);

            // Draw the image when it is valid to do so.
            if (this.DrawImageWhen == DrawImageWhen.AfterEndPaint)
                DrawUserImageToControl(e.Graphics);
        }

        /// <summary>
        /// Draws <see cref="UserImage"/> to the <see cref="DrawControl"/>.
        /// </summary>
        /// <param name="graphics">
        /// The <see cref="Graphics"/> of the control.
        /// </param>
        private void DrawUserImageToControl(Graphics graphics)
        {
            if (this.UserImage != null && this.Enabled)
                graphics.DrawImageUnscaled(this.UserImage, this.UseCustomImageRectange ? this.UserImageLocation : Point.Empty);
        }

        /// <summary>
        /// Raises the <see cref="WritePixels"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnWritePixels(EventArgs e)
        {
            // This prevents this event from being called again until it is done.
            this.redrawing = true;

            if (WritePixels != null)
                WritePixels(this, e);

            // Draw the pixel data to the bitmap.
            DrawPixels();
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="BorderStyle"/> property changes.
        /// </summary>
        [Browsable(true)]
        [Category("Property Changed")]
        [Description("Event raised when the value of the BorderStyle property is changed on Control.")]
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// Occurs before the <see cref="Control.Paint"/> event is raised.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Occurs before the Paint event is raised.")]
        public event PaintEventHandler BeginPaint;

        /// <summary>
        /// Occurs after the <see cref="Control.Paint"/> event has been raised.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Occurs after the Paint event has been raised.")]
        public event PaintEventHandler EndPaint;

        /// <summary>
        /// Occurs when the pixels are ready to be written by the user before the <see cref="Control.Paint"/> event.
        /// </summary>
        [Browsable(true)]
        [Category("Editor")]
        [Description("Occurs when the pixels are ready to be written by the user before the Paint event.")]
        public event EventHandler WritePixels;
        #endregion
    }

    /// <summary>
    /// Specifies constants defining when the prerendered image of the <see cref="DrawControl"/> should be drawn.
    /// </summary>
    public enum DrawImageWhen
    {
        /// <summary>
        /// Do not draw the image automatically.
        /// </summary>
        DontDraw,
        /// <summary>
        /// Draw the image before the <see cref="DrawControl.BeginPaint"/> event.
        /// </summary>
        BeforeBeginPaint,
        /// <summary>
        /// Draw the image before the <see cref="Control.Paint"/> event.
        /// </summary>
        BeforePaint,
        /// <summary>
        /// Draw the image after the <see cref="Control.Paint"/> event.
        /// </summary>
        AfterPaint,
        /// <summary>
        /// Draw the image after the <see cref="DrawControl.EndPaint"/> event.
        /// </summary>
        AfterEndPaint
    }
}