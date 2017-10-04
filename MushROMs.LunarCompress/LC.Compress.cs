using System;

namespace MushROMs.LunarCompress
{
    unsafe partial class LC
    {
        #region Methods
        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte[] data, CompressionFormats compressionFormat)
        {
            return GetCompressSize(data, compressionFormat, 0, data.Length, 0);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte[] data, CompressionFormats compressionFormat, int address, int dataSize)
        {
            return GetCompressSize(data, compressionFormat, address, dataSize, 0);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte[] data, CompressionFormats compressionFormat, int address, int dataSize, int otherOptions)
        {
            fixed (byte* ptr = &data[address])
                return GetCompressSize((IntPtr)ptr, compressionFormat, dataSize, otherOptions);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(IntPtr data, CompressionFormats compressionFormat, int dataSize)
        {
            return GetCompressSize(data, compressionFormat, dataSize, 0);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// A pointer to byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(IntPtr data, CompressionFormats compressionFormat, int dataSize, int otherOptions)
        {
            return LunarRecompress(data, IntPtr.Zero, dataSize, 0, compressionFormat, otherOptions);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat)
        {
            return Recompress(data, compressionFormat, GetCompressSize(data, compressionFormat), 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int address, int dataSize)
        {
            return Recompress(data, compressionFormat, GetCompressSize(data, compressionFormat, address, dataSize), 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into destination.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
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
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int maxSize)
        {
            return Recompress(data, compressionFormat, maxSize, 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into destination.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
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
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int maxSize, int address, int dataSize)
        {
            return Recompress(data, compressionFormat, maxSize, address, dataSize, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into destination
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
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
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int maxSize, int address, int dataSize, int otherOptions)
        {
            byte[] compressed = new byte[maxSize];
            fixed (byte* src = data)
            fixed (byte* dest = compressed)
                return LunarRecompress((IntPtr)src, (IntPtr)dest, dataSize, maxSize, compressionFormat, otherOptions) != 0 ? compressed : null;
        }

        /// <summary>
        /// Compress data from a byte array and place it into another array. 
        /// </summary>
        /// <param name="source">
        /// Pointer to the source byte array of data to compress.
        /// </param>
        /// <param name="destination">
        /// Pointer of the destination byte array for compressed data.
        /// </param>
        /// <param name="dataSize">
        /// Size of the data to compress in bytes.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
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
        public static int Recompress(IntPtr source, IntPtr destination, int dataSize, int maxSize, CompressionFormats compressionFormat)
        {
            return Recompress(source, destination, dataSize, maxSize, compressionFormat, 0);
        }

        /// <summary>
        /// Compress data from a byte array and place it into another array. 
        /// </summary>
        /// <param name="source">
        /// Pointer to the source byte array of data to compress.
        /// </param>
        /// <param name="destination">
        /// Pointer of the destination byte array for compressed data.
        /// </param>
        /// <param name="dataSize">
        /// Size of the data to compress in bytes.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 Tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
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
        public static int Recompress(IntPtr source, IntPtr destination, int dataSize, int maxSize, CompressionFormats compressionFormat, int otherOptions)
        {
            return LunarRecompress(source, destination, dataSize, maxSize, compressionFormat, otherOptions);
        }
        #endregion
    }
}