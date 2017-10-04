using System;
using System.ComponentModel;
using System.Drawing;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    /// <summary>
    /// Provides constants and static methods for common math functions.
    /// </summary>
    public static class MathHelper
    {
        #region Methods
        /// <summary>
        /// Determines whether <paramref name="point"/> is in
        /// <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="point">
        /// The <see cref="Point"/> to inspect.
        /// </param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> to serve as the boundary.
        /// </param>
        /// <returns>
        /// True if <paramref name="point"/> is in
        /// <paramref name="rectangle"/>, otherwise false.
        /// </returns>
        public static bool IsInBoundary(Point point, Rectangle rectangle)
        {
            return point.X >= rectangle.X && point.X < rectangle.X + rectangle.Width &&
                point.Y >= rectangle.Y && point.Y < rectangle.Y + rectangle.Height;
        }

        /// <summary>
        /// Determines whether two numbers are within a certain range of
        /// each other.
        /// </summary>
        /// <param name="v1">
        /// The first number.
        /// </param>
        /// <param name="v2">
        /// The second number.
        /// </param>
        /// <param name="range">
        /// The maximum permissable range outside of the boundary.
        /// </param>
        /// <returns>
        /// True if the magnitude of the difference of
        /// <paramref name="v1"/> and <paramref name="v2"/> is less than
        /// <paramref name="range"/>.
        /// </returns>
        public static bool ApproximatelyEquals(float v1, float v2, float range)
        {
            return Math.Abs(v1 - v2) < range;
        }

        /// <summary>
        /// Rounds a value to a boundary if it is in a certain range of it.
        /// </summary>
        /// <param name="value">
        /// The original value.
        /// </param>
        /// <param name="boundary">
        /// The rounding boundary.
        /// </param>
        /// <param name="range">
        /// The maximum permissable range outside of the boundary.
        /// </param>
        /// <returns>
        /// If <paramref name="value"/> is in the range of
        /// <paramref name="boundary"/>, then <paramref name="boundary"/>
        /// is returned, otherwise <paramref name="value"/>.
        /// </returns>
        public static float RoundTo(float value, float boundary, float range)
        {
            return ApproximatelyEquals(value, boundary, range) ? boundary : value;
        }

        /// <summary>
        /// Rounds a value between two boundaries if it is in a given
        /// range of one of them.
        /// </summary>
        /// <param name="value">
        /// The original value.
        /// </param>
        /// <param name="left">
        /// The first boundary.
        /// </param>
        /// <param name="right">
        /// The second boundary.
        /// </param>
        /// <param name="range">
        /// The maximum permissable range outside of the boundary.
        /// </param>
        /// <returns>
        /// One of the boundaries if <paramref name="value"/> is between
        /// <paramref name="range"/> of it, otherwise
        /// <paramref name="value"/> is returned.
        /// </returns>
        /// <remarks>
        /// It is wise to keep the two boundaries from overlapping
        /// </remarks>
        internal static float RoundToBoundary(float value, float left, float right, float range)
        {
            value = RoundTo(value, left, range);
            return RoundTo(value, right, range);
        }

        /// <summary>
        /// Determines whether a <see cref="char"/> is a valid decimal
        /// number.
        /// </summary>
        /// <param name="c">
        /// The character value.
        /// </param>
        /// <returns>
        /// true if <paramref name="c"/> is a decimal character, otherwise
        /// false.
        /// </returns>
        public static bool IsValidNumChar(char c)
        {
            return IsValidNumChar(c, NumberBase.Decimal);
        }

        /// <summary>
        /// Determines whether a <see cref="char"/> is a valid number in a
        /// given base representation.
        /// </summary>
        /// <param name="c">
        /// The character value.
        /// </param>
        /// <param name="numberBase">
        /// The base representation of the number.
        /// </param>
        /// <returns>
        /// true if <paramref name="c"/> is a numeric character in a given
        /// base representation, otherwise false.
        /// </returns>
        public static bool IsValidNumChar(char c, NumberBase numberBase)
        {
            switch (numberBase)
            {
                case NumberBase.Hexadecimal:
                    if (c >= '0' && c <= '9')
                        return true;
                    else if (c >= 'a' && c <= 'f')
                        return true;
                    else if (c <= 'A' && c >= 'F')
                        return true;
                    else
                        return false;
                case NumberBase.Binary:
                case NumberBase.Octal:
                case NumberBase.Decimal:
                    return c >= '0' && c <= '0' + (int)numberBase - 1;
                default:
                    throw new InvalidEnumArgumentException(Resources.ErrorNumberBase);
            }
        }
        #endregion
    }

    #region Enumerations
    /// <summary>
    /// Specifies constants defining which base number representation is in.
    /// </summary>
    public enum NumberBase
    {
        /// <summary>
        /// Base-2 binary number format.
        /// </summary>
        Binary = 2,
        /// <summary>
        /// Base-8 octal number format.
        /// </summary>
        Octal = 8,
        /// <summary>
        /// Base-10 decimal number format.
        /// </summary>
        Decimal = 10,
        /// <summary>
        /// Base-16 hexadecimal number format.
        /// </summary>
        Hexadecimal = 0x10,
    }
    #endregion
}