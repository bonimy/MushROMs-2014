using System;
using System.IO;
using System.IO.Compression;

namespace MushROMs.Editors
{
	/// <summary>
	/// Provides constants and static methods for GZip functionality.
	/// </summary>
	public static class GZip
    {
        #region Constant and read-only fields
        /// <summary>
		/// The magic number value of a GZip header.
        /// This field is constant.
		/// </summary>
        public static readonly byte[] MagicNumber = { 0x1F, 0x8B };
        #endregion

        #region Methods
        /// <summary>
		/// Determines whether a block of data is possibly a GZip
        /// compressed file.
		/// </summary>
		/// <param name="data">
		/// The possibly compressed data to inspect.
		/// </param>
		/// <returns>
		/// True if the first two bytes match the
        /// <see cref="MagicNumber"/>, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
		public static bool IsCompressed(byte[] data)
        {
            if (data.Length < MagicNumber.Length)
                return false;

            for (int i = MagicNumber.Length; --i >= 0; )
                if (data[i] != MagicNumber[i])
                    return false;

            return true;
		}

        /// <summary>
        /// Decompress a GZip compressed data array.
        /// </summary>
        /// <param name="data">
        /// The data to decompress.
        /// </param>
        /// <remarks>
        /// The function checks that <paramref name="data"/> has the GZip
        /// magic number. If it does not, no action is done.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
        public static byte[] Decompress(byte[] data)
        {
			if (!IsCompressed(data))
				return (byte[])data.Clone();

			const int size = 0x10000;
			byte[] buffer = new byte[size];
			int count = 0;

			using (MemoryStream memory = new MemoryStream())
			{
				using (GZipStream stream = new GZipStream(
                    new MemoryStream(data), CompressionMode.Decompress))
					while ((count = stream.Read(buffer, 0, size)) > 0)
						memory.Write(buffer, 0, count);
				return memory.ToArray();
			}
		}

		/// <summary>
		/// Compress a data array to a GZip format.
		/// </summary>
		/// <param name="data">
		/// The data to compress.
		/// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is null.
        /// </exception>
		public static byte[] Compress(byte[] data)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				using (GZipStream stream = new GZipStream(
                    memory, CompressionMode.Compress, true))
					stream.Write(data, 0, data.Length);
				return memory.ToArray();
			}
        }
        #endregion
    }
}