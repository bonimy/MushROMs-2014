using System;
using System.Drawing;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    /// <summary>
    /// Represents a modifiable ARGB (alpha, red, green, blue) color
    /// with HSL (hue, saturation, luminosity) and CMY (cyan, magenta,
    /// yellow) components.
    /// </summary>
    public struct ExpandedColor
    {
        #region Constant and read-only fields
        /// <summary>
        /// Represents a color that is null.
        /// </summary>
        public static readonly ExpandedColor Empty = Color.Empty;

        /// <summary>
        /// Represents the maximum alpha value.
        /// This field is constant.
        /// </summary>
        private const float ColorMax = 255.0f;
        /// <summary>
        /// Represents the maximum hue value.
        /// This field is constant.
        /// </summary>
        private const float HueMax = 360.0f;
        /// <summary>
        /// Represents the maximum luminosity value.
        /// This field is constant.
        /// </summary>
        private const float LumMax = 240.0f;
        /// <summary>
        /// Represents the maximum saturation value.
        /// This field is constant.
        /// </summary>
        private const float SatMax = LumMax;
        /// <summary>
        /// Represents the maximum percentage value.
        /// This field is constant.
        /// </summary>
        private const float PercentMax = 100.0f;
        /// <summary>
        /// Represents the minimum error range.
        /// This field is constant.
        /// </summary>
        private const float MinError = 0.0001f;

        /// <summary>
        /// Represents the red component weight of a luma-corrected
        /// grayscale.
        /// This field is constant.
        /// </summary>
        public const float LumaRedWeight = 0.3f;
        /// <summary>
        /// Represents the green component weight of a luma-corrected
        /// grayscale.
        /// This field is constant.
        /// </summary>
        public const float LumaGreenWeight = 0.59f;
        /// <summary>
        /// Represents the blue component weight of a luma-corrected
        /// grasycale.
        /// This field is constant.
        /// </summary>
        public const float LumaBlueWeight = 0.11f;
        #endregion

        #region Fields
        /// <summary>
        /// The alpha component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float alpha;
        /// <summary>
        /// The red component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float red;
        /// <summary>
        /// The green component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float green;
        /// <summary>
        /// The blue component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float blue;
        /// <summary>
        /// The cyan component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float cyan;
        /// <summary>
        /// The magenta component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float magenta;
        /// <summary>
        /// The yellow component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float yellow;
        /// <summary>
        /// The hue component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float hue;
        /// <summary>
        /// The saturation component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float sat;
        /// <summary>
        /// The luminosity component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        private float lum;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the alpha component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float A
        {
            get { return this.alpha; }
        }
        /// <summary>
        /// Gets the red component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>>
        public float R
        {
            get { return this.red; }
        }
        /// <summary>
        /// Gets the green component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float G
        {
            get { return this.green; }
        }
        /// <summary>
        /// Gets the blue component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float B
        {
            get { return this.blue; }
        }
        /// <summary>
        /// Gets the cyan component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float C
        {
            get { return this.cyan; }
        }
        /// <summary>
        /// Gets the magenta component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float M
        {
            get { return this.magenta; }
        }
        /// <summary>
        /// Gets the yellow component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float Y
        {
            get { return this.yellow; }
        }
        /// <summary>
        /// Gets the hue component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float H
        {
            get { return this.hue; }
        }
        /// <summary>
        /// Gets the saturation component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float S
        {
            get { return this.sat; }
        }
        /// <summary>
        /// Gets the luminosity component of this <see cref="ExpandedColor"/>
        /// structure.
        /// </summary>
        public float L
        {
            get { return this.lum; }
        }

        /// <summary>
        /// Specifies whether this <see cref="ExpandedColor"/> structure
        /// is unitialized.
        /// </summary>
        public bool IsEmpty
        {
            get { return ((Color)this).IsEmpty; }
        }
        /// <summary>
        /// Gets a value indicating whether this
        /// <see cref="ExpandedColor"/> structure is a predefined color.
        /// Predefined colors are represented by the elements of the
        /// <see cref="KnownColor"/> enumeration.
        /// </summary>
        public bool IsKnownColor
        {
            get { return ((Color)this).IsKnownColor; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="ExpandedColor"/>
        /// structure is a named color or a member of the
        /// <see cref="KnownColor"/> enumeration.
        /// </summary>
        public bool IsNamedColor
        {
            get { return ((Color)this).IsNamedColor; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="ExpandedColor"/>
        /// structure is a system color. A system color is a color that is
        /// used in a Windows display element. System colors are represented
        /// by elements of the <see cref="KnownColor"/> enumeration.
        /// </summary>
        public bool IsSystemColor
        {
            get { return ((Color)this).IsSystemColor; }
        }
        /// <summary>
        /// Gets the name of this <see cref="ExpandedColor"/>.
        /// </summary>
        public string Name
        {
            get { return ((Color)this).Name; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the hue, saturation, and luminosity values from
        /// the red, green, and blue components.
        /// </summary>
        private void CalculateRGBtoHSL()
        {
            float r = this.red / ColorMax;
            float g = this.green / ColorMax;
            float b = this.blue / ColorMax;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float chroma = max - min;

            float l = (max + min) / 2.0f;
            float s = 0.0f;
            float h = 0.0f;

            if (chroma != 0)
            {
                s = chroma / (1 - Math.Abs((2 * l) - 1));

                if (max == r)
                    h = (g - b) / chroma;
                else if (max == g)
                    h = 2.0f + (b - r) / chroma;
                else
                    h = 4.0f + (r - g) / chroma;
                if (h < 0)
                    h += 6.0f;
            }

            this.hue = h * HueMax / 6.0f;
            this.sat = s * SatMax;
            this.lum = l * LumMax;
        }

        /// <summary>
        /// Calculates the red, green, and blue components from the hue,
        /// saturation, and luminosity values.
        /// </summary>
        private void CalculateHSLtoRGB()
        {
            float h = this.hue / (HueMax / 6.0f);
            float s = this.sat / SatMax;
            float l = this.lum / LumMax;

            float r = 0;
            float g = 0;
            float b = 0;

            float c = (1 - Math.Abs((2 * l) - 1)) * s;
            float x = c * (1 - Math.Abs((h % 2) - 1));

            if (h >= 0 && h < 1)
            { r = c; g = x; }
            else if (h >= 1 && h < 2)
            { r = x; g = c; }
            else if (h >= 2 && h < 3)
            { g = c; b = x; }
            else if (h >= 3 && h < 4)
            { g = x; b = c; }
            else if (h >= 4 && h < 5)
            { r = x; b = c; }
            else
            { r = c; b = x; }

            float m = l - (c / 2);
            r += m;
            g += m;
            b += m;

            this.red = r * ColorMax;
            this.green = g * ColorMax;
            this.blue = b * ColorMax;
        }

        /// <summary>
        /// Calculates the cyan, magenta, and yellow components from the
        /// red, green, and blue components.
        /// </summary>
        private void CalculateRGBtoCMY()
        {
            this.cyan = this.magenta = this.yellow = 0;
            float black = ColorMax - Math.Min(this.cyan, Math.Min(this.magenta, this.yellow));
            if (black != 0)
            {
                this.cyan = ColorMax * (black - this.red) / black;
                this.magenta = ColorMax * (black - this.green) / black;
                this.yellow = ColorMax * (black - this.blue) / black;
            }
        }

        /// <summary>
        /// Calculates the red, green, and blue components from the cyan,
        /// magenta, and yellow components.
        /// </summary>
        private void CMYtoRGB()
        {
            float white = ColorMax - Math.Min(this.cyan, Math.Min(this.magenta, this.yellow));
            this.red = (ColorMax - this.cyan) * white / ColorMax;
            this.green = (ColorMax - this.magenta) * white / ColorMax;
            this.blue = (ColorMax - this.yellow) * white / ColorMax;
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure whose hue,
        /// saturation, and luminosity are shifted from the current
        /// <see cref="ExpandedColor"/>.
        /// </summary>
        /// <param name="hDegrees">
        /// The hue rotation by degrees. Valid values are -180 to 180.
        /// </param>
        /// <param name="sPercent">
        /// The percent change of the saturation. Valid values are -100
        /// to 100.
        /// </param>
        /// <param name="lPercent">
        /// The percent change of the luminosity. Valid values are -100
        /// to 100.
        /// </param>
        /// <returns>
        /// An <see cref="ExpandedColor"/> structure whose hue,
        /// saturation, and luminosity of shifted from the current
        /// <see cref="ExpandedColor"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// hue, saturation, or luminosity is outside the valid range.
        /// </exception>
        public ExpandedColor Modify(float hDegrees, float sPercent, float lPercent)
        {
            hDegrees = MathHelper.RoundToBoundary(hDegrees, -HueMax / 2, HueMax / 2, MinError);
            sPercent = MathHelper.RoundToBoundary(sPercent, -PercentMax, PercentMax, MinError);
            lPercent = MathHelper.RoundToBoundary(lPercent, -PercentMax, PercentMax, MinError);

            if (hDegrees < -HueMax / 2 || hDegrees > HueMax / 2)
                throw new ArgumentException(Resources.ErrorModifyHue);
            if (sPercent < -PercentMax || sPercent > PercentMax)
                throw new ArgumentException(Resources.ErrorModifySat);
            if (lPercent < -PercentMax || lPercent > PercentMax)
                throw new ArgumentException(Resources.ErrorModifyLum);

            float h = this.hue + hDegrees;
            if (h < 0)
                h += HueMax;
            if (h > HueMax)
                h -= HueMax;

            float s = this.sat + ((sPercent > 0 ? SatMax - this.sat : this.sat) * sPercent / PercentMax);
            float l = this.lum + ((lPercent > 0 ? LumMax - this.lum : this.lum) * lPercent / PercentMax);
            return ExpandedColor.FromAhsl(this.alpha, h, s, l);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure that is a
        /// colorized version of the current <see cref="ExpandedColor"/>.
        /// </summary>
        /// <param name="hDegrees">
        /// The hue by degrees.
        /// </param>
        /// <param name="sPercent">
        /// The saturation intensity (valid range is 0 to 100).
        /// </param>
        /// <param name="lPercent">
        /// The luminosity intensity (valid range is 0 to 100).
        /// </param>
        /// <returns>
        /// An <see cref="ExpandedColor"/> structure that is a colorized
        /// version of the current <see cref="ExpandedColor"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// hue, saturation, or luminosity is outside the valid range.
        /// </exception>
        public ExpandedColor Colorize(float hDegrees, float sPercent, float lPercent)
        {
            return Colorize(hDegrees, sPercent, lPercent, PercentMax);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure that is a
        /// colorized version of the current <see cref="ExpandedColor"/>
        /// by a specified effective amount.
        /// </summary>
        /// <param name="hDegrees">
        /// The hue by degrees.
        /// </param>
        /// <param name="sPercent">
        /// The saturation intensity (valid range is 0 to 100).
        /// </param>
        /// <param name="lPercent">
        /// The luminosity intensity (valid range is 0 to 100).
        /// </param>
        /// <param name="eff">
        /// Determines how effective the colorizing is (valid range is 0
        /// to 100).
        /// </param>
        /// <returns>
        /// An <see cref="ExpandedColor"/> structure that is a colorized
        /// version of the current <see cref="ExpandedColor"/> by a
        /// specified effective amount.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// hue, saturation, luminosity, or effectivness is outside the valid range.
        /// </exception>
        public ExpandedColor Colorize(float hDegrees, float sPercent, float lPercent, float eff)
        {
            hDegrees = MathHelper.RoundToBoundary(hDegrees, 0, HueMax, MinError);
            sPercent = MathHelper.RoundToBoundary(sPercent, 0, PercentMax, MinError);
            lPercent = MathHelper.RoundToBoundary(lPercent, 0, PercentMax, MinError);
            eff = MathHelper.RoundToBoundary(eff, 0, PercentMax, MinError);

            if (hDegrees < 0 || hDegrees > HueMax)
                throw new ArgumentException(Resources.ErrorColorizeHue);
            if (sPercent < 0 || sPercent > PercentMax)
                throw new ArgumentException(Resources.ErrorColorizeSat);
            if (lPercent < 0 || lPercent > PercentMax)
                throw new ArgumentException(Resources.ErrorColorizeLum);
            if (eff < 0 || eff > PercentMax)
                throw new ArgumentException(Resources.ErrorColorizeEff);

            const float half = PercentMax / 2.0f;
            float l = this.lum + ((lPercent > half ? LumMax - this.lum : this.lum) * (lPercent - half) / half);
            ExpandedColor c = ExpandedColor.FromAhsl(hDegrees, SatMax * (sPercent / PercentMax), l);
            return ExpandedColor.FromArgb(this.alpha, (eff * c.red + (PercentMax - eff) * this.red) / PercentMax,
                                                      (eff * c.green + (PercentMax - eff) * this.green) / PercentMax,
                                                      (eff * c.blue + (PercentMax - eff) * this.blue) / PercentMax);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure whose RGB
        /// values are the negative of this <see cref="ExpandedColor"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="ExpandedColor"/> structure whose RGB values are
        /// the negative of this <see cref="ExpandedColor"/>.
        /// </returns>
        public ExpandedColor Invert()
        {
            return ExpandedColor.FromArgb(this.alpha, ColorMax - this.red, ColorMax - this.green, ColorMax - this.blue);
        }

        /// <summary>
        /// Creates an <see cref=" ExpandedColor"/> structure representing
        /// a Luma-weighted grayscale of this <see cref="ExpandedColor"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="ExpandedColor"/> structure representing a
        /// Luma-weigthed grascyale of this <see cref="ExpandedColor"/>.
        /// </returns>
        public ExpandedColor LumaGrayScale()
        {
            return GrayScale(LumaRedWeight, LumaGreenWeight, LumaBlueWeight);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure representing
        /// a custom-weigthed grayscale of this <see cref="ExpandedColor"/>.
        /// </summary>
        /// <param name="rWeight">
        /// The weight of the red component of the <see cref="ExpandedColor"/>.
        /// This value cannot be less than zero.
        /// </param>
        /// <param name="gWeight">
        /// The weight of the green component of the <see cref="ExpandedColor"/>.
        /// This value cannot be less than zero.
        /// </param>
        /// <param name="bWeight">
        /// The weight of the blue component of the <see cref="ExpandedColor"/>.
        /// This value cannot be less than zero.
        /// </param>
        /// <returns>
        /// An <see cref="ExpandedColor"/> structure representing a
        /// custom-weigthed grayscale of this <see cref="ExpandedColor"/>.
        /// </returns>
        /// <remarks>
        /// The weights are all normalized so that their sum is unity.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// At least one weighted value is less than zero.
        /// </exception>
        public ExpandedColor GrayScale(float rWeight, float gWeight, float bWeight)
        {
            if (rWeight < 0 || gWeight < 0 || bWeight < 0)
                throw new ArgumentOutOfRangeException(Resources.ErrorNegativeWeight);

            float total = rWeight + gWeight + bWeight;
            if (total == 0)
                return ExpandedColor.Empty;

            rWeight /= total;
            gWeight /= total;
            bWeight /= total;

            float lum = rWeight * (this.red / ColorMax) + gWeight * (this.green / ColorMax) + bWeight * (this.blue / ColorMax);
            return ExpandedColor.FromAhsl(this.alpha, this.hue, 0, lum * LumMax);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure which is a
        /// blend of this structure and another.
        /// </summary>
        /// <param name="bottom">
        /// The bottom layer to blend this <see cref="ExpandedColor"/> with.
        /// </param>
        /// <param name="mode">
        /// The <see cref="BlendModes"/> value to use.
        /// </param>
        /// <returns>
        /// A blended <see cref="ExpandedColor"/>.
        /// </returns>
        public ExpandedColor Blend(ExpandedColor bottom, BlendModes mode)
        {
            float[] c1 = { this.red / ColorMax, this.green / ColorMax, this.blue / ColorMax };
            float[] c2 = { bottom.red / ColorMax, bottom.green / ColorMax, bottom.blue / ColorMax };
            float[] c3 = new float[c1.Length];

            float h1 = this.hue / HueMax;
            float s1 = this.sat / SatMax;
            float l1 = this.lum / LumMax;
            switch (mode)
            {
                case BlendModes.Multiply:
                    for (int i = c1.Length; --i >= 0; )
                        c3[i] = c1[i] * c2[i];
                    break;
                case BlendModes.Screen:
                    for (int i = c1.Length; --i >= 0; )
                        c3[i] = 1 - (1 - c1[i]) * (1 - c2[i]);
                    break;
                case BlendModes.Overlay:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (c1[i] < 0.5)
                            c3[i] = 2 * c1[i] * c2[i];
                        else
                            c3[i] = 1 - 2 * (1 - c1[i]) * (1 - c2[i]);
                    }
                    break;
                case BlendModes.HardLight:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (c2[i] < 0.5)
                            c3[i] = 2 * c1[i] * c2[i];
                        else
                            c3[i] = 1 - 2 * (1 - c1[i]) * (1 - c2[i]);
                    }
                    break;
                case BlendModes.SoftLight:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (c2[i] < 0.5)
                            c3[i] = 2 * c1[i] * c2[i] + c1[i] * c1[i] * (1 - 2 * c2[i]);
                        else
                            c3[i] = 2 * c1[i] * (1 - c2[i]) + (float)Math.Sqrt(c1[i]) * (2 * c2[i] - 1);
                    }
                    break;
                case BlendModes.ColorDodge:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (c1[i] < 1)
                        {
                            c3[i] = c2[i] / (1 - c1[i]);
                            if (c3[i] > 1)
                                c3[i] = 1;
                        }
                        else
                            c3[i] = 1;
                    }
                    break;
                case BlendModes.LinearDodge:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        c3[i] = c1[i] + c2[i];
                        if (c3[i] > 1)
                            c3[i] = 1;
                    }
                    break;
                case BlendModes.ColorBurn:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (c1[i] > 0)
                        {
                            c3[i] = (1 - c2[i]) / c1[i];
                            if (c3[i] > 1)
                                c3[i] = 0;
                            else
                                c3[i] = 1 - c3[i];
                        }
                        else
                            c3[i] = 0;
                    }
                    break;
                case BlendModes.LinearBurn:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        c3[i] = c1[i] + c2[i] - 1;
                        if (c3[i] < 0)
                            c3[i] = 0;
                    }
                    break;
                case BlendModes.VividLight:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (l1 > 0.5)
                        {
                            if (c1[i] < 1)
                            {
                                c3[i] = c2[i] / (1 - c1[i]);
                                if (c3[i] > 1)
                                    c3[i] = 1;
                            }
                            else
                                c3[i] = 1;
                        }
                        else
                        {
                            if (c1[i] > 0)
                            {
                                c3[i] = (1 - c2[i]) / c1[i];
                                if (c3[i] > 1)
                                    c3[i] = 0;
                                else
                                    c3[i] = 1 - c3[i];
                            }
                            else
                                c3[i] = 0;
                        }
                    }
                    break;
                case BlendModes.LinearLight:
                    for (int i = c1.Length; --i >= 0; )
                    {
                        if (l1 > 0.5)
                        {
                            c3[i] = c1[i] + c2[i];
                            if (c3[i] > 1)
                                c3[i] = 1;
                        }
                        else
                        {
                            c3[i] = c1[i] + c2[i] - 1;
                            if (c3[i] < 0)
                                c3[i] = 0;
                        }
                    }
                    break;
                case BlendModes.Difference:
                    for (int i = c1.Length; --i >= 0; )
                        c3[i] = Math.Max(c1[i], c2[i]) - Math.Min(c1[i], c2[i]);
                    break;
                case BlendModes.Darken:
                    for (int i = c1.Length; --i >= 0; )
                        c3[i] = Math.Min(c1[i], c2[i]);
                    break;
                case BlendModes.Lighten:
                    for (int i = c1.Length; --i >= 0; )
                        c3[i] = Math.Max(c1[i], c2[i]);
                    break;
            }
            return ExpandedColor.FromArgb(c3[0] * ColorMax, c3[1] * ColorMax, c3[2] * ColorMax);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from a 32-bit ARGB value.
        /// </summary>
        /// <param name="argb">
        /// A value specifying the 32-bit ARGB value.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        public static ExpandedColor FromArgb(int argb)
        {
            return ExpandedColor.FromArgb((argb >> 0x18) & 0xFF, (argb >> 0x10) & 0x0FF, (argb >> 8) & 0xFF, argb & 0xFF);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the specified <see cref="Color"/>
        /// structure, but with the new specified alpha value. Although this method allows
        /// a 32-bit value to be passed for the alpha value, the value is limited to
        /// 8 bits.
        /// </summary>
        /// <param name="alpha">
        /// The alpha value for the new <see cref="ExpandedColor"/>. Valid values are 0 through 255.
        /// </param>
        /// <param name="baseColor">
        /// The <see cref="Color"/> from which to create the new <see cref="ExpandedColor"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// alpha is less than 0 or greater than 255.
        /// </exception>
        public static ExpandedColor FromArgb(float alpha, Color baseColor)
        {
            return ExpandedColor.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the specified <see cref="Color"/>
        /// structure, but with the new specified alpha value. Although this method allows
        /// a 32-bit value to be passed for the alpha value, the value is limited to
        /// 8 bits.
        /// </summary>
        /// <param name="alpha">
        /// The alpha value for the new <see cref="ExpandedColor"/>. Valid values are 0 through 255.
        /// </param>
        /// <param name="baseColor">
        /// The <see cref="ExpandedColor"/> from which to create the new <see cref="ExpandedColor"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// alpha is less than 0 or greater than 255.
        /// </exception>
        public static ExpandedColor FromArgb(float alpha, ExpandedColor baseColor)
        {
            return ExpandedColor.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the specified 8-bit color values
        /// (red, green, and blue). The alpha value is implicitly 255 (fully opaque).
        /// Although this method allows a 32-bit value to be passed for each color component,
        /// the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="red">
        /// The red component. Valid values are 0 through 255.
        /// </param>
        /// <param name="green">
        /// The green component. Valid values are 0 through 255.
        /// </param>
        /// <param name="blue">
        /// The blue component. Valid values are 0 through 255.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// red, green, or blue is less than 0 or greater than 255.
        /// </exception>
        public static ExpandedColor FromArgb(float red, float green, float blue)
        {
            return ExpandedColor.FromArgb(ColorMax, red, green, blue);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the four ARGB component (alpha,
        /// red, green, and blue) values. Although this method allows a 32-bit value
        /// to be passed for each component, the value of each component is limited to
        /// 8 bits.
        /// </summary>
        /// <param name="alpha">
        /// The alpha component. Valid values are 0 through 255.
        /// </param>
        /// <param name="red">
        /// The red component. Valid values are 0 through 255.
        /// </param>
        /// <param name="green">
        /// The green component. Valid values are 0 through 255.
        /// </param>
        /// <param name="blue">
        /// The blue component. Valid values are 0 through 255.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// alpha, red, green, or blue is less than 0 or greater than 255.
        /// </exception>
        public static ExpandedColor FromArgb(float alpha, float red, float green, float blue)
        {
            alpha = MathHelper.RoundToBoundary(alpha, 0, ColorMax, MinError);
            red = MathHelper.RoundToBoundary(red, 0, ColorMax, MinError);
            green = MathHelper.RoundToBoundary(green, 0, ColorMax, MinError);
            blue = MathHelper.RoundToBoundary(blue, 0, ColorMax, MinError);

            if (alpha < 0 || alpha > ColorMax)
                throw new ArgumentException(Resources.ErrorAlphaRange);
            if (red < 0 || red > ColorMax)
                throw new ArgumentException(Resources.ErrorRedRange);
            if (green < 0 || green > ColorMax)
                throw new ArgumentException(Resources.ErrorGreenRange);
            if (blue < 0 || blue > ColorMax)
                throw new ArgumentException(Resources.ErrorBlueRange);

            ExpandedColor x = new ExpandedColor();
            x.alpha = alpha;
            x.red = red;
            x.green = green;
            x.blue = blue;
            x.CalculateRGBtoCMY();
            x.CalculateRGBtoHSL();
            return x;
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from a 32-bit ACMY value.
        /// </summary>
        /// <param name="acmy">
        /// A value specifying the 32-bit ACMY value.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        public static ExpandedColor FromAcmy(int acmy)
        {
            return ExpandedColor.FromAcmy((acmy >> 0x18) & 0xFF, (acmy >> 0x10) & 0xFF, (acmy >> 8) & 0xFF, acmy & 0xFF);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the specified 8-bit color values
        /// (cyan, magenta, and yellow). The alpha value is implicitly 255 (fully opaque).
        /// Although this method allows a 32-bit value to be passed for each color component,
        /// the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="cyan">
        /// The cyan component. Valid values are 0 through 255.
        /// </param>
        /// <param name="magenta">
        /// The magenta component. Valid values are 0 through 255.
        /// </param>
        /// <param name="yellow">
        /// The yellow component. Valid values are 0 through 255.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// cyan, magenta, or yellow is less than 0 or greater than 255.
        /// </exception>
        public static ExpandedColor FromAcmy(float cyan, float magenta, float yellow)
        {
            return ExpandedColor.FromAcmy(ColorMax, cyan, magenta, yellow);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the four ACMY component (alpha,
        /// red, green, and blue) values. Although this method allows a 32-bit value
        /// to be passed for each component, the value of each component is limited to
        /// 8 bits.
        /// </summary>
        /// <param name="alpha">
        /// The alpha component. Valid values are 0 through 255.
        /// </param>
        /// <param name="cyan">
        /// The cyan component. Valid values are 0 through 255.
        /// </param>
        /// <param name="magenta">
        /// The magenta component. Valid values are 0 through 255.
        /// </param>
        /// <param name="yellow">
        /// The yellow component. Valid values are 0 through 255.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// alpha, cyan, magenta, or yellow is less than 0 or greater than 255.
        /// </exception>
        public static ExpandedColor FromAcmy(float alpha, float cyan, float magenta, float yellow)
        {
            alpha = MathHelper.RoundToBoundary(alpha, 0, ColorMax, MinError);
            cyan = MathHelper.RoundToBoundary(cyan, 0, ColorMax, MinError);
            magenta = MathHelper.RoundToBoundary(magenta, 0, ColorMax, MinError);
            yellow = MathHelper.RoundToBoundary(yellow, 0, ColorMax, MinError);

            if (alpha < 0 || alpha > ColorMax)
                throw new ArgumentException(Resources.ErrorAlphaRange);
            if (cyan < 0 || cyan > ColorMax)
                throw new ArgumentException(Resources.ErrorCyanRange);
            if (magenta < 0 || magenta > ColorMax)
                throw new ArgumentException(Resources.ErrorMagentaRange);
            if (yellow < 0 || yellow > ColorMax)
                throw new ArgumentException(Resources.ErrorYellowRange);

            ExpandedColor x = new ExpandedColor();
            x.alpha = alpha;
            x.cyan = cyan;
            x.magenta = magenta;
            x.yellow = yellow;
            x.CMYtoRGB();
            x.CalculateRGBtoHSL();
            return x;
        }
        
        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the specified 8-bit color values
        /// (hue, saturation, luminosity). The alpha value is implicitly 255 (fully opaque).
        /// Although this method allows a 32-bit value to be passed for each color component,
        /// the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="hue">
        /// The hue value. Valid values are 0 through 240.
        /// </param>
        /// <param name="sat">
        /// The saturation value. Valid values are 0 through 240.
        /// </param>
        /// <param name="lum">
        /// The luminosity value. Valid values are 0 through 240.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// hue, saturation, or luminosity is less than 0 or greater than 240.
        /// </exception>
        public static ExpandedColor FromAhsl(float hue, float sat, float lum)
        {
            return ExpandedColor.FromAhsl(ColorMax, hue, sat, lum);
        }

        /// <summary>
        /// Creates an <see cref="ExpandedColor"/> structure from the four AHSL component (alpha,
        /// hue, saturation, luminosity) values. Although this method allows a 32-bit value
        /// to be passed for each component, the value of each component is limited to
        /// 8 bits.
        /// </summary>
        /// <param name="alpha">
        /// The alpha component. Valid values are 0 through 255.
        /// </param>
        /// <param name="hue">
        /// The hue value. Valid values are 0 through 240.
        /// </param>
        /// <param name="sat">
        /// The saturation value. Valid values are 0 through 240.
        /// </param>
        /// <param name="lum">
        /// The luminosity value. Valid values are 0 through 240.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> structure that this method creates.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// alpha is less than 0 or greater than 255 or hue, saturation, or
        /// luminosity is less than 0 or greater than 240.
        /// </exception>
        public static ExpandedColor FromAhsl(float alpha, float hue, float sat, float lum)
        {
            alpha = MathHelper.RoundToBoundary(alpha, 0, ColorMax, MinError);
            hue = MathHelper.RoundToBoundary(hue, 0, HueMax, MinError);
            sat = MathHelper.RoundToBoundary(sat, 0, SatMax, MinError);
            lum = MathHelper.RoundToBoundary(lum, 0, LumMax, MinError);

            if (alpha < 0 || alpha > ColorMax)
                throw new ArgumentException(Resources.ErrorAlphaRange);
            if (hue < 0 || hue > HueMax)
                throw new ArgumentException(Resources.ErrorHueRange);
            if (sat < 0 || sat > SatMax)
                throw new ArgumentException(Resources.ErrorSatRange);
            if (lum < 0 || lum > LumMax)
                throw new ArgumentException(Resources.ErrorLumRange);

            ExpandedColor x = new ExpandedColor();
            x.alpha = alpha;
            x.hue = hue;
            x.sat = sat;
            x.lum = lum;
            x.CalculateHSLtoRGB();
            x.CalculateRGBtoCMY();
            return x;
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="ExpandedColor"/> structure.
        /// </summary>
        /// <returns>
        /// The 32-bit ARGB value of this <see cref="ExpandedColor"/>.
        /// </returns>
        public int ToArgb()
        {
            return ((Color)this).ToArgb();
        }

        /// <summary>
        /// Gets the <see cref="KnownColor"/> value of this <see cref="ExpandedColor"/> structure.
        /// </summary>
        /// <returns>
        /// An element of the <see cref="KnownColor"/> enumeration, if the <see cref="ExpandedColor"/>
        /// is created from a predefined color by using either the <see cref="Color.FromName(string)"/>
        /// method or the <see cref="Color.FromKnownColor(KnownColor)"/> method; otherwise, 0.
        /// </returns>
        public KnownColor ToKnownColor()
        {
            return ((Color)this).ToKnownColor();
        }

        /// <summary>
        /// Converts this <see cref="ExpandedColor"/> structure to a human-readable string.
        /// </summary>
        /// <returns>
        /// A string that is the name of this <see cref="ExpandedColor"/>, if the <see cref="ExpandedColor"/>
        /// is created from a predefined color by using either the <see cref="Color.FromName(string)"/>
        /// method or the <see cref="Color.FromKnownColor(KnownColor)"/> method; otherwise, a string that
        /// consists of the ARGB component names and their values
        /// </returns>
        public override string ToString()
        {
            return ((Color)this).ToString();
        }

        #region Designer Methods
        /// <summary>
        /// Converts the specified <see cref="ExpandedColor"/> structure
        /// to a <see cref="Color"/> structure
        /// </summary>
        /// <param name="c">
        /// The <see cref="ExpandedColor"/> to be converted.
        /// </param>
        /// <returns>
        /// The <see cref="Color"/> that results from the conversion.
        /// </returns>
        public static implicit operator ExpandedColor(Color c)
        {
            return ExpandedColor.FromArgb(c.A, c.R, c.G, c.B);
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> structure
        /// to a <see cref="ExpandedColor"/> structure
        /// </summary>
        /// <param name="c">
        /// The <see cref="Color"/> to be converted.
        /// </param>
        /// <returns>
        /// The <see cref="ExpandedColor"/> that results from the conversion.
        /// </returns>
        public static explicit operator Color(ExpandedColor c)
        {
            return Color.FromArgb((int)(c.alpha + 0.5f), (int)(c.red + 0.5f), (int)(c.green + 0.5f), (int)(c.blue + 0.5f));
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">
        /// Another object to compare to.
        /// </param>
        /// <returns>
        /// true if obj and this instance are the same type and represent the same value;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this == (ExpandedColor)obj;
        }

        /// <summary>
        /// Returns the hash code of this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return ((Color)this).GetHashCode();
        }

        /// <summary>
        /// Tests whether two specified <see cref="ExpandedColor"/> structures are equivalent.
        /// </summary>
        /// <param name="left">
        /// The <see cref="ExpandedColor"/> that is to the left of the inequality operator.
        /// </param>
        /// <param name="right">
        /// The <see cref="ExpandedColor"/> that is to the right of the inequality operator.
        /// </param>
        /// <returns>
        /// true if the two <see cref="ExpandedColor"/> structures are equal; otherwise, false.
        /// </returns>
        public static bool operator ==(ExpandedColor left, ExpandedColor right)
        {
            return (Color)left == (Color)right;
        }

        /// <summary>
        /// Tests whether two specified <see cref="ExpandedColor"/> structures are different.
        /// </summary>
        /// <param name="left">
        /// The <see cref="ExpandedColor"/> that is to the left of the inequality operator.
        /// </param>
        /// <param name="right">
        /// The <see cref="ExpandedColor"/> that is to the right of the inequality operator.
        /// </param>
        /// <returns>
        /// true if the two <see cref="ExpandedColor"/> structures are different; otherwise, false.
        /// </returns>
        public static bool operator !=(ExpandedColor left, ExpandedColor right)
        {
            return (Color)left != (Color)right;
        }
        #endregion
        #endregion
    }

    #region Enumerations
    /// <summary>
    /// Specifies constants defining color blend modes.
    /// </summary>
    public enum BlendModes
    {
        /// <summary>
        /// Multiply the layers.
        /// </summary>
        Multiply,
        /// <summary>
        /// Invert both layers, multiply them, then invert the result.
        /// </summary>
        Screen,
        /// <summary>
        /// Combine <see cref="Multiply"/> and <see cref="Screen"/>.
        /// </summary>
        Overlay,
        /// <summary>
        /// <see cref="Screen"/> with layers swapped.
        /// </summary>
        HardLight,
        /// <summary>
        /// A softer version of <see cref="HardLight"/>.
        /// </summary>
        SoftLight,
        /// <summary>
        /// Divide bottom layer by the inverted top layer.
        /// </summary>
        ColorDodge,
        /// <summary>
        /// Sum the layers.
        /// </summary>
        LinearDodge,
        /// <summary>
        /// Same as <see cref="ColorDodge"/>, but blending with white does not change the image.
        /// </summary>
        ColorBurn,
        /// <summary>
        /// Invert each layer, add them, then invert the result.
        /// </summary>
        LinearBurn,
        /// <summary>
        /// Combines <see cref="ColorDodge"/> and <see cref="ColorBurn"/>
        /// (rescaled so that neutral colors become middle gray).
        /// </summary>
        VividLight,
        /// <summary>
        /// Combines <see cref="LinearDodge"/> and <see cref="LinearBurn"/>
        /// (rescaled so that neutral colors become middle gray).
        /// </summary>
        LinearLight,
        /// <summary>
        /// Subtracts the larger color component with the smaller one.
        /// </summary>
        Difference,
        /// <summary>
        /// Retains the smallest components of each layer.
        /// </summary>
        Darken,
        /// <summary>
        /// Retains the largest components of each layer.
        /// </summary>
        Lighten
    }
    #endregion
}