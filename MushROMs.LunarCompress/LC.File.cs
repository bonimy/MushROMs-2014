using System;

namespace MushROMs.LunarCompress
{
    unsafe partial class LC
    {
        #region Constant and read-only fields
        /// <summary>
        /// Header flag. A file with no header.
        /// </summary>
        private const int NOHEADER = 0;
        /// <summary>
        /// Header flag. A file with a header.
        /// </summary>
        private const int HEADER = 1;
        /// <summary>
        /// File flag. Do not seek to the address.
        /// </summary>
        private const int NOSEEK = 0;
        /// <summary>
        /// File flag. Seek to the address.
        /// </summary>
        private const int SEEK = 1;
        /// <summary>
        /// File flag. The DLL creates its own file array.
        /// </summary>
        private const int CREATEARRAY = 0x10;
        /// <summary>
        /// File flag. Automatically save the contents of the byte array back to the file on close.
        /// </summary>
        private const int SAVEONCLOSE = 0x20;
        #endregion

        #region Fields
        /// <summary>
        /// A value indicating whether a file is open in Lunar Compress.
        /// </summary>
        private static bool open = false;
        /// <summary>
        /// The path of the currently open file or <see cref="String.Empty"/> if no
        /// file is open or was ever specified.
        /// </summary>
        private static string path = string.Empty;
        /// <summary>
        /// A pointer to the byte array that Lunar Compress references for its functions.
        /// It will return <see cref="IntPtr.Zero"/> when no array is open.
        /// </summary>
        private static IntPtr data = IntPtr.Zero;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether a file is open in Lunar Compress.
        /// </summary>
        public static bool Open
        {
            get { return LC.open; }
        }

        /// <summary>
        /// Gets the path of the currently open file or null if no file is open or was ever
        /// specified.
        /// </summary>
        public static string Path
        {
            get { return LC.path; }
        }

        /// <summary>
        /// Gets a pointer to the byte array that Lunar Compress references for its functions.
        /// It will return <see cref="IntPtr.Zero"/> when no array is open.
        /// </summary>
        public static IntPtr Data
        {
            get { return LC.data; }
        }

        /// <summary>
        /// Gets the size of the file in bytes that is currently open in the DLL.
        /// It will return 0 on failure.
        /// </summary>
        public static int FileSize
        {
            get { return LunarGetFileSize(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Open file for access by the DLL.  Files of any size can be opened, since the 
        /// DLL does not load the entire file into RAM for manipulations.
        /// If another file is already open, <see cref="CloseFile()"/> will be used
        /// to close it first. File is open in read-only mode by default.
        /// </summary>
        /// <param name="path">
        /// Name of File.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// The DLL does not prevent other applications from reading/writing to the 
        /// file at the same time, though of course that isn't recommended.
        /// </remarks>
        public static bool OpenFile(string path)
        {
            return OpenFile(path, FileModes.ReadOnly);
        }

        /// <summary>
        /// Open file for access by the DLL.  Files of any size can be opened, since the 
        /// DLL does not load the entire file into RAM for manipulations.
        /// If another file is already open, <see cref="CloseFile()"/> will be used
        /// to close it first.
        /// </summary>
        /// <param name="path">
        /// Name of File.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// The DLL does not prevent other applications from reading/writing to the 
        /// file at the same time, though of course that isn't recommended.
        /// </remarks>
        public static bool OpenFile(string path, FileModes fileMode)
        {
            CloseFile();
            LC.open = LunarOpenFile(path, fileMode);
            LC.path = LC.open ? path : string.Empty;
            return LC.open;
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it
        /// first.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path)
        {
            return OpenRAMFile(path, FileModes.ReadOnly, LockArraySizeOptions.None, false, 0);
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it
        /// first.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode)
        {
            return OpenRAMFile(path, fileMode, LockArraySizeOptions.None, false, 0);
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it
        /// first.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode, LockArraySizeOptions flags)
        {
            return OpenRAMFile(path, fileMode, flags, false, 0);
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it
        /// first.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <param name="saveOnClose">
        /// If true, will automatically save the contents of the byte array back to the file
        /// when <see cref="CloseFile()"/> is called. This has no effect if the file was opened
        /// in read-only mode.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode, LockArraySizeOptions flags, bool saveOnClose)
        {
            return OpenRAMFile(path, fileMode, flags, saveOnClose, 0);
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it
        /// first.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <param name="saveOnClose">
        /// If true, will automatically save the contents of the byte array back to the file
        /// when <see cref="CloseFile()"/> is called. This has no effect if the file was opened
        /// in read-only mode.
        /// </param>
        /// <param name="minSize">
        /// The minimum size of the array to be allocated.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode, LockArraySizeOptions flags, bool saveOnClose, int minSize)
        {
            CloseFile();
            LC.data = LunarOpenRAMFile(path, (int)fileMode | (int)flags | CREATEARRAY | (saveOnClose ? SAVEONCLOSE : 0), minSize);
            LC.open = LC.data != IntPtr.Zero;
            LC.path = LC.open ? path : string.Empty;
            return LC.data;
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.  
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it 
        /// first.
        /// </summary>
        /// <param name="data">
        /// Pointer to array of bytes to use as file.
        /// </param>
        /// <param name="size">
        /// Size of the user-supplied array.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(IntPtr data, int size)
        {
            return OpenRAMFile(data, size, FileModes.ReadOnly, LockArraySizeOptions.None);
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.  
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it 
        /// first.  
        /// </summary>
        /// <param name="data">
        /// Pointer to array of bytes to use as file.
        /// </param>
        /// <param name="size">
        /// Size of the user-supplied array.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(IntPtr data, int size, FileModes fileMode)
        {
            return OpenRAMFile(data, size, fileMode, LockArraySizeOptions.None);
        }

        /// <summary>
        /// "Open" a byte array in RAM for access by the DLL as though it were a file.  
        /// If another file is already open, <see cref="CloseFile()"/> will be used to close it 
        /// first.  
        /// </summary>
        /// <param name="data">
        /// Pointer to array of bytes to use as file.
        /// </param>
        /// <param name="size">
        /// Size of the user-supplied array.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for
        /// decompressing structures from memory, or if your program loads an entire
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </remarks>
        public static IntPtr OpenRAMFile(IntPtr data, int size, FileModes fileMode, LockArraySizeOptions flags)
        {
            CloseFile();
            return LC.data = LunarOpenRAMFile(data, (int)fileMode | (int)flags, size);
        }

        /// <summary>
        /// Saves the currently open byte array in RAM to a file.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool SaveRAMFile()
        {
            return LunarSaveRAMFile(null);
        }

        /// <summary>
        /// Saves the currently open byte array in RAM to a file.
        /// </summary>
        /// <param name="path">
        /// The file to write to. You can specify null to save back
        /// to the same file if <see cref="OpenRAMFile(string)"/> was used.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// If you specify the name of a file that already exists, it will be overwritten.
        /// </remarks>
        public static bool SaveRAMFile(string path)
        {
            if (LunarSaveRAMFile(path))
            {
                if (path != null)
                    LC.path = path;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Closes the file or RAM byte array currently open in the DLL.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool CloseFile()
        {
            bool closed = LunarCloseFile();
            if (closed)
            {
                LC.open = false;
                LC.path = string.Empty;
                LC.data = IntPtr.Zero;
            }
            return closed;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <returns>
        /// A byte array of the data from the currently open file on success, otherwise null.
        /// </returns>
        public static byte[] ReadFile()
        {
            return ReadFile(0, LC.FileSize, false);
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <returns>
        /// A byte array of the data from the currently open file on success, otherwise null.
        /// </returns>
        public static byte[] ReadFile(int address, int size)
        {
            return ReadFile(address, size, false);
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// A byte array of the data from the currently open file on success, otherwise null.
        /// </returns>
        public static byte[] ReadFile(int address, int size, bool seek)
        {
            byte[] data = new byte[size];
            fixed (byte* dest = data)
                return ReadFile((IntPtr)dest, address, size, seek) ? data : null;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="dest">
        /// Pointer to the destination byte array.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool ReadFile(IntPtr dest)
        {
            return ReadFile(dest, 0, LC.FileSize, false);
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="dest">
        /// Pointer to the destination byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool ReadFile(IntPtr dest, int address, int size)
        {
            return ReadFile(dest, address, size, false);
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="dest">
        /// Pointer to the destination byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool ReadFile(IntPtr dest, int address, int size, bool seek)
        {
            return LunarReadFile(dest, size, address, (seek ? SEEK : NOSEEK));
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(byte[] data, int address)
        {
            return WriteFile(data, address, data.Length, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(byte[] data, int address, int size)
        {
            return WriteFile(data, address, size, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(byte[] data, int address, int size, bool seek)
        {
            fixed (byte* ptr = data)
                return WriteFile((IntPtr)ptr, address, size, seek);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Pointer to source byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(IntPtr data, int address, int size)
        {
            return WriteFile(data, address, size, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Pointer to source byte array.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(IntPtr data, int address, int size, bool seek)
        {
            return LunarWriteFile(data, size, address, (seek ? SEEK : NOSEEK));
        }

        /// <summary>
        /// Changes the bytes used for scanning for free space and erasing data.
        /// </summary>
        /// <param name="val1">
        /// First free space byte.
        /// </param>
        /// <param name="val2">
        /// Second free space byte.
        /// </param>
        /// <param name="erase">
        /// Byte for erasing (good idea to make same as <paramref name="val1"/> or <paramref name="val2"/>).
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool SetFreeBytes(byte val1, byte val2, byte erase)
        {
            return LunarSetFreeBytes((val1 | (val2 << 8) | (erase << 0x10)));
        }

        /// <summary>
        /// Converts a SNES ROM Address to a PC file offset.
        /// </summary>
        /// <param name="pointer">
        /// SNES address to convert.
        /// </param>
        /// <param name="romType">
        /// The ROM type.
        /// </param>
        /// <param name="header">
        /// True if the ROM has a 0x200 byte copier header.
        /// </param>
        /// <returns>
        /// The PC file offset of the SNES ROM address.
        /// It will return an undefined value for non-ROM addresses.
        /// </returns>
        /// <remarks>
        /// Do NOT specify an ExROM type if your ROM is not larger than 32 Mbit!
        ///
        /// A warning on 64 Mbit ROMs:  Since banks 7E and 7F are used for RAM instead
        /// of ROM, the SNES cannot access the last 64 KB of a 64 Mbit ExLoROM file
        /// or 64 KB from the second and fourth last 32K chunks in a 64 Mbit ExHiROM file.
        /// Thus you should not store anything past $7F:01FF PC in an ExLoROM file with
        /// a copier header, or in the ranges $7E:0200-7E:81FF and $7F:0200-7F:81FF PC 
        /// in an ExHiROM file with a copier header.
        /// 
        /// Also note that for 64 Mbit ExHiROM files, the area from 0000-7FFF of banks
        /// $70 - $77 is usually used by SRAM, so the corresponding areas in the ROM 
        /// are not accessible.
        /// </remarks>
        public static int SNEStoPC(int pointer, ROMTypes romType, bool header)
        {
            return LunarSNEStoPC(pointer, romType, (header ? HEADER : NOHEADER));
        }

        /// <summary>
        /// Converts PC file offset to SNES ROM address.
        /// </summary>
        /// <param name="pointer">
        /// PC file offset to convert
        /// </param>
        /// <param name="romType">
        /// The ROM type.
        /// </param>
        /// <param name="header">
        /// True if the ROM has a 0x200 byte copier header.
        /// </param>
        /// <returns>
        /// The SNES ROM address of the PC file offset.
        /// It will return an undefined value for non-ROM addresses.
        /// </returns>
        /// <remarks>
        /// Do NOT specify an ExROM type if your ROM is not larger than 32 Mbit!
        ///
        /// A warning on 64 Mbit ROMs:  Since banks 7E and 7F are used for RAM instead
        /// of ROM, the SNES cannot access the last 64 KB of a 64 Mbit ExLoROM file
        /// or 64 KB from the second and fourth last 32K chunks in a 64 Mbit ExHiROM file.
        /// Thus you should not store anything past $7F:01FF PC in an ExLoROM file with
        /// a copier header, or in the ranges $7E:0200-7E:81FF and $7F:0200-7F:81FF PC 
        /// in an ExHiROM file with a copier header.
        /// 
        /// Also note that for 64 Mbit ExHiROM files, the area from 0000-7FFF of banks
        /// $70 - $77 is usually used by SRAM, so the corresponding areas in the ROM 
        /// are not accessible.
        /// </remarks>
        public static int PCtoSNES(int pointer, ROMTypes romType, bool header)
        {
            return LunarPCtoSNES(pointer, romType, (header ? HEADER : NOHEADER));
        }

        /// <summary>
        /// Erases an area in a file/ROM by overwriting it with 0's. The 0 byte used 
        /// to erase the area can be changed with the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="size">
        /// Number of bytes to zero out.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// This function will not prevent you from erasing past the end of the file,
        /// which will expand the file size.
        /// </remarks>
        public static bool EraseArea(int offset, int size)
        {
            return LunarEraseArea(offset, size);
        }

        /// <summary>
        /// Expands a SNES ROM by appending 0's to the end of the file, and fixes the
        /// size byte (if possible). The 0 byte used for expansion can be changed with
        /// the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="mBits">
        /// MegaBits to expand the ROM to (0-32).
        /// For sizes greater than 32Mbit, use <see cref="ExpandROM(ExROMExpansionModes)"/>
        /// </param>
        /// <returns>
        /// The expanded size of the ROM. If the ROM is bigger than the size passed, returns the ROMs original size.
        /// Returns 0 on fail.
        /// </returns>
        /// <remarks>
        /// Be warned that the EXLOROM_1 type expansions will physically move the ROM's
        /// original data banks to PC offset 40:0000, which may cause problems if your
        /// code is set up to use hard coded PC file offsets.  Also note that not all
        /// ROMs can be expanded to sizes above 32 Mbits -- check the documentation of 
        /// the Lunar Expand utility for more details (you can get it from FuSoYa's
        /// site).
        /// 
        /// If your emulator can play ToP, it supports 48 Mbit ExHiROM games.  For
        /// 64 Mbit HiROM and 32 &amp; 64 MBit LoROM games, you must use Snes9x 1.39a 
        /// (or 1.39mk3) or higher (zsnes does not yet support these).
        /// </remarks>
        public static int ExpandROM(int mBits)
        {
            return LunarExpandROM(mBits);
        }

        /// <summary>
        /// Expands a SNES ROM by appending 0's to the end of the file, and fixes the
        /// size byte (if possible). The 0 byte used for expansion can be changed with
        /// the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="mBits">
        /// MegaBits to expand the ROM to.
        /// For sizes less than 32Mbit, use <see cref="ExpandROM(ExROMExpansionModes)"/>
        /// </param>
        /// <returns>
        /// The expanded size of the ROM. If the ROM is bigger than the size passed, returns the ROMs original size.
        /// Returns 0 on fail.
        /// </returns>
        /// <remarks>
        /// Be warned that the EXLOROM_1 type expansions will physically move the ROM's
        /// original data banks to PC offset 40:0000, which may cause problems if your
        /// code is set up to use hard coded PC file offsets.  Also note that not all
        /// ROMs can be expanded to sizes above 32 Mbits -- check the documentation of 
        /// the Lunar Expand utility for more details (you can get it from FuSoYa's
        /// site).
        /// 
        /// If your emulator can play ToP, it supports 48 Mbit ExHiROM games.  For
        /// 64 Mbit HiROM and 32 &amp; 64 MBit LoROM games, you must use Snes9x 1.39a 
        /// (or 1.39mk3) or higher (zsnes does not yet support these).
        /// </remarks>
        public static int ExpandROM(ExROMExpansionModes mBits)
        {
            return LunarExpandROM((int)mBits);
        }
        #endregion
    }
}
