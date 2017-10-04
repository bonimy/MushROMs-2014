using System;
using System.Runtime.InteropServices;

namespace MushROMs.Unmanaged
{
    /// <summary>
    /// Provides an unmanaged memory container.
    /// </summary>
    /// <remarks>
    /// These functions are pure unmanaged memory resources. They provide no disposal
    /// and have no interations with the garbage collector. It is up to the programmer
    /// to properly manage releasing this memory just like in C++.
    /// </remarks>
    public struct Pointer
    {
        #region Fields
        /// <summary>
        /// The <see cref="Memory"/> data.
        /// </summary>
        private IntPtr data;

        /// <summary>
        /// The size, in bytes, of the <see cref="Memory"/> data.
        /// </summary>
        private int size;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Memory"/> data.
        /// </summary>
        public IntPtr Data
        {
            get { return this.data; }
        }

        /// <summary>
        /// Gets the size, in bytes, of the <see cref="Memory"/> data.
        /// </summary>
        public int Size
        {
            get { return this.size; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a <see cref="Pointer"/> with the given size.
        /// </summary>
        /// <param name="size">
        /// The size, in bytes, of the memory.
        /// </param>
        /// <returns>
        /// A <see cref="Pointer"/> whose memory has the specified size.
        /// </returns>
        public static Pointer CreatePointer(int size)
        {
            Pointer pointer;
            pointer.data = Memory.CreateMemory(size);
            pointer.size = size;
            return pointer;
        }

        /// <summary>
        /// Creates a <see cref="Pointer"/> whose memory has been emptied.
        /// </summary>
        /// <param name="num">
        /// The number of elements to allocate.
        /// </param>
        /// <param name="size">
        /// The size, in bytes, of each element.
        /// </param>
        /// <returns>
        /// A <see cref="Pointer"/> whose memory has been emptied.
        /// </returns>
        public static Pointer CreateEmptyPointer(int num, int size)
        {
            Pointer pointer;
            pointer.data = Memory.CreateEmptyMemory(size, num);
            pointer.size = size;
            return pointer;
        }

        /// <summary>
        /// Resizes the memory in <see cref="Pointer"/> the specified size.
        /// </summary>
        /// <param name="size">
        /// The new size, in bytes, of the memory.
        /// </param>
        public void Resize(int size)
        {
            this.data = Memory.ResizeMemory(this.data, size);
            this.size = size;
        }

        /// <summary>
        /// Frees the memory in <see cref="Pointer"/>.
        /// </summary>
        public void Free()
        {
            Memory.FreeMemory(this.data);
            this.data = IntPtr.Zero;
            this.size = 0;
        }
        #endregion
    }
}