using System;
using System.Runtime.InteropServices;

namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Provides a strong class-library (originally built in C) for SNES-related functionally in programming.
    /// </summary>
    public static unsafe partial class LC
    {
        #region Constant and read-only fields
        /// <summary>
        /// Specifies the path of the Lunar Compress.dll file.
        /// This field is constant.
        /// </summary>
        public const string DLLPath = "Libraries\\" +
#if x86
            "x86\\"
#elif x64
            "x64\\"
#endif
             + "Lunar Compress.dll";
        #endregion

        #region Properties
        /// <summary>
        /// The current version of the DLL as an integer.
        /// For example, version 1.30 of the DLL would return "130" (decimal).
        /// </summary>
        public static int Version
        {
            get { return LunarVersion(); }
        }
        #endregion

        #region Methods
        [DllImport(DLLPath)]
        private static extern int LunarVersion();
        [DllImport(DLLPath)]
        private static extern bool LunarOpenFile(string fileName, FileModes fileMode);
        [DllImport(DLLPath)]
        private static extern IntPtr LunarOpenRAMFile(string data, int fileMode, int size);
        [DllImport(DLLPath)]
        private static extern IntPtr LunarOpenRAMFile(IntPtr data, int fileMode, int size);
        [DllImport(DLLPath)]
        private static extern bool LunarSaveRAMFile(string fileName);
        [DllImport(DLLPath)]
        private static extern bool LunarCloseFile();
        [DllImport(DLLPath)]
        private static extern int LunarGetFileSize();
        [DllImport(DLLPath)]
        private static extern bool LunarReadFile(IntPtr destination, int size, int address, int seek);
        [DllImport(DLLPath)]
        private static extern bool LunarWriteFile(IntPtr source, int size, int address, int seek);
        [DllImport(DLLPath)]
        private static extern bool LunarSetFreeBytes(int value);
        [DllImport(DLLPath)]
        private static extern int LunarSNEStoPC(int pointer, ROMTypes romType, int header);
        [DllImport(DLLPath)]
        private static extern int LunarPCtoSNES(int pointer, ROMTypes romType, int header);
        [DllImport(DLLPath)]
        private static extern int LunarDecompress(IntPtr destination, int addressToStart, int maxDataSize, CompressionFormats format1, int format2, IntPtr lastROMPosition);
        [DllImport(DLLPath)]
        private static extern int LunarRecompress(IntPtr source, IntPtr destination, int dataSize, int maxDataSize, CompressionFormats format, int format2);
        [DllImport(DLLPath)]
        private static extern bool LunarEraseArea(int address, int size);
        [DllImport(DLLPath)]
        private static extern int LunarExpandROM(int mBits);
        [DllImport(DLLPath)]
        private static extern int LunarVerifyFreeSpace(int addressStart, int addressEnd, int size, BankTypes bankType);
        [DllImport(DLLPath)]
        private static extern bool LunarIPSCreate(IntPtr hwnd, string ipsFileName, string romFileName, string rom2FileName, IPSOptions flags);
        [DllImport(DLLPath)]
        private static extern bool LunarIPSApply(IntPtr hwnd, string ipsFileName, string romFileName, IPSOptions flags);
        [DllImport(DLLPath)]
        private static extern bool LunarCreatePixelMap(IntPtr source, IntPtr destination, int numTiles, GraphicsFormats gfxType);
        [DllImport(DLLPath)]
        private static extern bool LunarCreateBppMap(IntPtr source, IntPtr destination, int numTiles, GraphicsFormats gfxType);
        [DllImport(DLLPath)]
        private static extern uint LunarSNEStoPCRGB(ushort snesColor);
        [DllImport(DLLPath)]
        private static extern ushort LunarPCtoSNESRGB(uint pcColor);
        [DllImport(DLLPath)]
        private static extern bool LunarRender8x8(IntPtr theMapBits, int theWidth, int theHeight, int displayAtX, int displayAtY, IntPtr pixelMap, IntPtr pcPalette, int map8Tile, Render8x8Flags extra);
        [DllImport(DLLPath)]
        private static extern int LunarWriteRatArea(IntPtr theData, int size, int preferredAddress, int minRange, int maxRange, RATFunctionFlags flags);
        [DllImport(DLLPath)]
        private static extern int LunarEraseRatArea(int address, int size, int flags);
        [DllImport(DLLPath)]
        private static extern int LunarGetRatAreaSize(int address, int flags);
        #endregion
    }
}