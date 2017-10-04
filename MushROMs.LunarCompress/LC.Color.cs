using System.Drawing;

namespace MushROMs.LunarCompress
{
    partial class LC
    {
        #region Constant and read-only fields
        /// <summary>
        /// Alpha mask for SNES color.
        /// This field is constant.
        /// </summary>
        private const ushort SNESAlphaMask = 0x8000;
        /// <summary>
        /// Alpha mask for PC color.
        /// This field is constant.
        /// </summary>
        private const uint PCAlphaMask  = 0xFF000000;
        #endregion

        #region Methods
        /// <summary>
        /// Converts a standard SNES 15-bit color into a PC 24-bit color.
        /// </summary>
        /// <param name="snesColor">
        /// SNES RGB value. (?bbbbbgg gggrrrrr)
        /// </param>
        /// <returns>
        /// PC color value. (00000000 rrrrr000 ggggg000 bbbbb000)
        /// </returns>
        public static uint SNEStoPCRGB(ushort snesColor)
        {
            return SNEStoPCRGB(snesColor, false);
        }

        /// <summary>
        /// Converts a standard SNES 16-bit color into a PC 32-bit color.
        /// </summary>
        /// <param name="snesColor">
        /// SNES RGB value. (abbbbbgg gggrrrrr)
        /// </param>
        /// <param name="alpha">
        /// When true, the alpha bit will be included. Otherwise, it is treated as zero.
        /// </param>
        /// <returns>
        /// PC color value. (aaaaaaaa rrrrr000 ggggg000 bbbbb000)
        /// </returns>
        public static uint SNEStoPCRGB(ushort snesColor, bool alpha)
        {
            uint color = LunarSNEStoPCRGB(snesColor);
            if (alpha && ((snesColor & SNESAlphaMask) != 0))
                color |= PCAlphaMask;

            return color;
        }

        /// <summary>
        /// Converts a standard PC 24-bit color into the nearest SNES 15-bit color, by
        /// rounding each color component to the nearest 5-bit value.
        /// </summary>
        /// <param name="pcColor">
        /// PC RGB value (???????? rrrrrrrr gggggggg bbbbbbbb).
        /// </param>
        /// <returns>
        /// SNES color value. (0bbbbbgg gggrrrrr)
        /// </returns>
        public static ushort PCtoSNESRGB(uint pcColor)
        {
            return PCtoSNESRGB(pcColor, false);
        }

        /// <summary>
        /// Converts a standard PC 32-bit color into the nearest SNES 16-bit color, by
        /// rounding each color component to the nearest 5-bit value.
        /// </summary>
        /// <param name="pcColor">
        /// PC RGB value (aaaaaaaa rrrrrrrr gggggggg bbbbbbbb).
        /// </param>
        /// <param name="alpha">
        /// When true, the alpha bit will be included. Otherwise, it is treated as zero.
        /// </param>
        /// <returns>
        /// SNES color value. (abbbbbgg gggrrrrr)
        /// </returns>
        public static ushort PCtoSNESRGB(uint pcColor, bool alpha)
        {
            ushort color = LunarPCtoSNESRGB(pcColor);
            if (alpha && ((pcColor & PCAlphaMask) != 0))
                color |= SNESAlphaMask;
            return color;
        }

        /// <summary>
        /// Rounds an RGB color integer to the neigherest 8 byte value.
        /// </summary>
        /// <param name="color">
        /// The RGB color integer.
        /// </param>
        /// <returns>
        /// A rounded value of <paramref name="color"/>.
        /// </returns>
        public static uint RoundColorMidpoint(uint color)
        {
            // Get the RGB components individually. We add 4 so rounding is based around the midpoint.
            uint r = (color & 0xFF0000) + 0x040000;
            uint g = (color & 0x00FF00) + 0x000400;
            uint b = (color & 0x0000FF) + 0x000004;

            // Round the component to the nearest valid value. Do not let exceed 255.
            r = r > 0xFF0000 ? 0xF80000 : r & 0xF80000;
            g = g > 0x00FF00 ? 0x00F800 : g & 0x00F800;
            b = b > 0x0000FF ? 0x0000F8 : b & 0x0000F8;

            // Return composition of all rounded components.
            return (color & PCAlphaMask) | r | g | b;
        }

        /// <summary>
        /// Rounds a <see cref="Color"/> to the neigherest 8 byte value.
        /// </summary>
        /// <param name="color">
        /// The <see cref="Color"/>.
        /// </param>
        /// <returns>
        /// A rounded value of <paramref name="color"/>.
        /// </returns>
        public static Color RoundColorValue(Color color)
        {
            // Get the RGB components individually. We add 4 so rounding is based around the midpoint.
            int r = color.R + 4;
            int g = color.G + 4;
            int b = color.B + 4;

            // Round the component to the nearest valid value. Do not let exceed 255.
            r = r > 0xFF ? 0xF8 : r & 0xF8;
            g = g > 0xFF ? 0xF8 : g & 0xF8;
            b = b > 0xFF ? 0xF8 : b & 0xF8;

            // Return composition of all rounded components.
            return Color.FromArgb(color.A, r, g, b);
        }

        /// <summary>
        /// Converts a <see cref="Color"/> to a 24-bit PC color.
        /// </summary>
        /// <param name="color">
        /// A <see cref="Color"/>.
        /// </param>
        /// <returns>
        /// A 24-bit PC color the <see cref="Color"/>.
        /// </returns>
        public static uint SystemToPCColor(Color color)
        {
            return RoundColorMidpoint((uint)color.ToArgb());
        }

        /// <summary>
        /// Converts a 24-bit PC color to a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">
        /// PC color value.
        /// </param>
        /// <returns>
        /// A <see cref="Color"/> of <paramref name="color"/>.
        /// </returns>
        public static Color PCToSystemColor(uint color)
        {
            return Color.FromArgb((int)RoundColorMidpoint(color));
        }

        /// <summary>
        /// Converts a <see cref="Color"/> to a 15-bit SNES color.
        /// </summary>
        /// <param name="color">
        /// A <see cref="Color"/>.
        /// </param>
        /// <returns>
        /// A 15-bit SNES color the <see cref="Color"/>.
        /// </returns>
        public static ushort SystemToSNESColor(Color color)
        {
            return PCtoSNESRGB(SystemToPCColor(color));
        }

        /// <summary>
        /// Converts a 15-bit PC color to a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">
        /// PC color value.
        /// </param>
        /// <returns>
        /// A <see cref="Color"/> of <paramref name="color"/>.
        /// </returns>
        public static Color SNESToSystemColor(ushort color)
        {
            return PCToSystemColor(SNEStoPCRGB(color));
        }
        #endregion
    }
}