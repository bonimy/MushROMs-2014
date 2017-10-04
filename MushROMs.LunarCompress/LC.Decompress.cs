using System;

namespace MushROMs.LunarCompress
{
    unsafe partial class LC
    {
        #region Methods
        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat)
        {
            return GetDecompressSize(compressionFormat, 0, 0);
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat, int offset)
        {
            return GetDecompressSize(compressionFormat, offset, 0);
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat, int offset, int otherOptions)
        {
            return LunarDecompress(IntPtr.Zero, offset, 0, compressionFormat, otherOptions, IntPtr.Zero);
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <param name="lastROMPosition">
        /// Returns the offset of the next byte that comes after the compressed data. This could be used to calculate the size
        /// of the compressed data after calling the function, using the simple function <paramref name="lastROMPosition"/>-<paramref name="offset"/>.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat, int offset, int otherOptions, out int lastROMPosition)
        {
            fixed (int* last = &lastROMPosition)
                return LunarDecompress(IntPtr.Zero, offset, 0, compressionFormat, otherOptions, (IntPtr)last);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <returns>
        /// A byte array of the decompressed data.
        /// </returns>
        public static byte[] Decompress(CompressionFormats compressionFormat)
        {
            return Decompress(compressionFormat, GetDecompressSize(compressionFormat), 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize)
        {
            return Decompress(compressionFormat, maxSize, 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset)
        {
            return Decompress(compressionFormat, maxSize, offset, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions)
        {
            byte[] data = new byte[maxSize];
            fixed (byte* dest = data)
                return Decompress((IntPtr)dest, compressionFormat, maxSize, offset, otherOptions) != 0 ? data : null;
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <param name="lastROMPosition">
        /// Returns the offset of the next byte that comes after the compressed data. This could be used to calculate the size
        /// of the compressed data after calling the function, using the simple function <paramref name="lastROMPosition"/>-<paramref name="offset"/>.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions, out int lastROMPosition)
        {
            byte[] data = new byte[maxSize];
            fixed (byte* dest = data)
                return Decompress((IntPtr)dest, compressionFormat, maxSize, offset, otherOptions, out lastROMPosition) != 0 ? data : null;
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If <paramref name="destination"/>=null, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(IntPtr destination, CompressionFormats compressionFormat)
        {
            return Decompress(destination, compressionFormat, GetDecompressSize(compressionFormat), 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(IntPtr destination, CompressionFormats compressionFormat, int maxSize)
        {
            return Decompress(destination, compressionFormat, maxSize, 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(IntPtr destination, CompressionFormats compressionFormat, int maxSize, int offset)
        {
            return Decompress(destination, compressionFormat, maxSize, offset, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(IntPtr destination, CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions)
        {
            return LunarDecompress(destination, offset, maxSize, compressionFormat, otherOptions, IntPtr.Zero);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <param name="lastROMPosition">
        /// Returns the offset of the next byte that comes after the compressed data. This could be used to calculate the size
        /// of the compressed data after calling the function, using the simple function <paramref name="lastROMPosition"/>-<paramref name="offset"/>.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(IntPtr destination, CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions, out int lastROMPosition)
        {
            fixed (int* last = &lastROMPosition)
                return LunarDecompress(destination, offset, maxSize, compressionFormat, otherOptions, (IntPtr)last);
        }
        #endregion
    }
}
