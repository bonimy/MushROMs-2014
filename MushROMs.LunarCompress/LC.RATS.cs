using System;

namespace MushROMs.LunarCompress
{
    unsafe partial class LC
    {
        #region Constant and read-only fields
        /// <summary>
        /// RAT function flag. Data to erase is compressed.
        /// </summary>
        private const int COMPRESSED = 0x800;
        #endregion
        
        #region Fields
        /// <summary>
        /// The first free space byte.
        /// </summary>
        private static byte free1 = 0;
        /// <summary>
        /// The second free space byte.
        /// </summary>
        private static byte free2 = 0;
        /// <summary>
        /// The erase byte.
        /// </summary>
        private static byte erase = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the first free space byte.
        /// </summary>
        public static byte FreeByte1
        {
            get { return LC.free1; }
            set { SetFreeBytes(value, LC.free2, LC.erase); }
        }

        /// <summary>
        /// Gets or sets the first free space byte.
        /// </summary>
        public static byte FreeByte2
        {
            get { return LC.free2; }
            set { SetFreeBytes(LC.free1, value, LC.erase); }
        }

        /// <summary>
        /// Gets or sets the erase byte.
        /// </summary>
        public static byte EraseByte
        {
            get { return LC.erase; }
            set { SetFreeBytes(LC.free1, LC.free2, value); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Verifies free space in the ROM available for inserting data (free space is
        /// defined as an area filled with 0's). The 0 byte used to check 
        /// for free space can be changed with the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="addressStart">
        /// File offset to start searching for space.
        /// </param>
        /// <param name="addressEnd">
        /// File offset to start searching
        /// </param>
        /// <param name="size">
        /// Amount of free space to find, in bytes.
        /// </param>
        /// <param name="bankType">
        /// Ignore results the cross the specified bank boundaries.
        /// </param>
        /// <returns>
        /// Returns the file offset of the first valid location in the specified range
        /// that matches the size of the free space requested.  A value of zero indicates
        /// failure.
        /// </returns>
        public static int VerifyFreeSpace(int addressStart, int addressEnd, int size, BankTypes bankType)
        {
            return LunarVerifyFreeSpace(addressStart, addressEnd, size, bankType);
        }

        /// <summary>
        /// Scans the ROM in the user-defined range for free space to store the user
        /// supplied data of the given size, and prepends a RAT to it.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data)
        {
            return WriteRATArea(ref data, 0, LC.FileSize, 0, 0, data.Length, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in the user-defined range for free space to store the user
        /// supplied data of the given size, and prepends a RAT to it.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange)
        {
            return WriteRATArea(ref data, minRange, maxRange, 0, 0, data.Length, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange, int preferredAddress)
        {
            return WriteRATArea(ref data, minRange, maxRange, preferredAddress, 0, data.Length, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange, int preferredAddress, int offset, int size)
        {
            return WriteRATArea(ref data, minRange, maxRange, preferredAddress, offset, size, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality. Multiple flags can be combined.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange, int preferredAddress, int offset, int size, RATFunctionFlags flags)
        {
            return WriteRATArea(ref data, minRange, maxRange, preferredAddress, offset, size, flags);
        }

        /// <summary>
        /// Scans the ROM in the user-defined range for free space to store the user
        /// supplied data of the given size, and prepends a RAT to it.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(ref byte[] data)
        {
            return WriteRATArea(ref data, 0, LC.FileSize, 0, 0, data.Length, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in the user-defined range for free space to store the user
        /// supplied data of the given size, and prepends a RAT to it.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(ref byte[] data, int minRange, int maxRange)
        {
            return WriteRATArea(ref data, minRange, maxRange, 0, 0, data.Length, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(ref byte[] data, int minRange, int maxRange, int preferredAddress)
        {
            return WriteRATArea(ref data, minRange, maxRange, preferredAddress, 0, data.Length, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(ref byte[] data, int minRange, int maxRange, int preferredAddress, int offset, int size)
        {
            return WriteRATArea(ref data, minRange, maxRange, preferredAddress, offset, size, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality. Multiple flags can be combined.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(ref byte[] data, int minRange, int maxRange, int preferredAddress, int offset, int size, RATFunctionFlags flags)
        {
            fixed (byte* ptr = &data[offset])
                return WriteRATArea((IntPtr)ptr, size, minRange, maxRange, preferredAddress, flags);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(IntPtr data, int size)
        {
            return WriteRATArea(data, size, 0, LC.FileSize, 0, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(IntPtr data, int size, int minRange, int maxRange)
        {
            return WriteRATArea(data, size, minRange, maxRange, 0, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(IntPtr data, int size, int minRange, int maxRange, int preferredAddress)
        {
            return WriteRATArea(data, size, minRange, maxRange, preferredAddress, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality. Multiple flags can be combined.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(IntPtr data, int size, int minRange, int maxRange, int preferredAddress, RATFunctionFlags flags)
        {
            return LunarWriteRatArea(data, size, preferredAddress, maxRange, maxRange, flags);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address)
        {
            return EraseRATArea(address, RATEraseFlags.None, CompressionFormats.None, 0);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality.
        /// </param>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address, RATEraseFlags flags)
        {
            return EraseRATArea(address, flags, CompressionFormats.None, 0);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality.
        /// </param>
        /// <param name="compressionFormat">
        /// If the data has no RAT, the function can attempt to get the size by using <see cref="Decompress(CompressionFormats)"/>.
        /// Do not attempt to use a format that requires knowing the size of the decompressed data in advance, such as <see cref="CompressionFormats.RLE3"/>.
        /// </param>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address, RATEraseFlags flags, CompressionFormats compressionFormat)
        {
            return EraseRATArea(address, flags, compressionFormat, 0);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality.
        /// </param>
        /// <param name="compressionFormat">
        /// If the data has no RAT, the function can attempt to get the size by using <see cref="Decompress(CompressionFormats)"/>.
        /// Do not attempt to use a format that requires knowing the size of the decompressed data in advance, such as <see cref="CompressionFormats.RLE3"/>.
        /// </param>
        /// <param name="size">
        /// Default size to use for the data to erase, in bytes. This is only used if there is no RAT for the data
        /// and the decompression fails. If you don't want the function to erase anything in this case, set the value to 0.
        /// </param>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address, RATEraseFlags flags, CompressionFormats compressionFormat, int size)
        {
            return LunarEraseRatArea(address, size, (int)flags | (compressionFormat != CompressionFormats.None ? ((int)compressionFormat | COMPRESSED) : 0));
        }

        /// <summary>
        /// Determines the size of a segment of data defined by a RAT.  This may be
        /// useful in some cases if you want to know the size without actually erasing
        /// anything, but otherwise it's not necessary.
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <returns>
        /// The size of the data, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int GetRATAreaSize(int address)
        {
            return GetRATAreaSize(address, CompressionFormats.None);
        }

        /// <summary>
        /// Determines the size of a segment of data defined by a RAT.  This may be
        /// useful in some cases if you want to know the size without actually erasing
        /// anything, but otherwise it's not necessary.
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="compressionFormat">
        /// If the data has no RAT, the function can attempt to get the size by using <see cref="Decompress(CompressionFormats)"/>.
        /// Do not attempt to use a format that requires knowing the size of the decompressed data in advance, such as <see cref="CompressionFormats.RLE3"/>.
        /// </param>
        /// <returns>
        /// The size of the data, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int GetRATAreaSize(int address, CompressionFormats compressionFormat)
        {
            return LunarGetRatAreaSize(address, (compressionFormat != CompressionFormats.None ? ((int)compressionFormat | COMPRESSED) : 0));
        }
        #endregion
    }
}
