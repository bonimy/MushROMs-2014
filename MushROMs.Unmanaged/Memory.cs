using System;
using System.Runtime.InteropServices;

namespace MushROMs.Unmanaged
{
    /// <summary>
    /// Provides constants and static methods for using unmanaged memory allocation.
    /// </summary>
    /// <remarks>
    /// These functions are pure unmanaged memory resources. They provide no disposal
    /// and have no interations with the garbage collector. It is up to the programmer
    /// to properly manage releasing this memory just like in C++.
    /// </remarks>
    public static class Memory
    {
        #region Constant and read-only fields
        /// <summary>
        /// Specifies the path of the Unmanaged.dll file.
        /// This field is constant.
        /// </summary>
        public const string UNMANAGED = "Libraries\\" +
#if x86
            "x86\\"
#elif x64
            "x64\\"
#endif
        + "Unmanaged.dll";
        #endregion

        #region Methods
        /// <summary>
        /// Allocates a block of <paramref name="size"/> bytes of memory, returning an <see cref="IntPtr"/>
        /// to the beginning of the block.
        /// </summary>
        /// <param name="size">
        /// Size of the memory block, in bytes.
        /// </param>
        /// <returns>
        /// On success, a pointer to the memory block allocated by the function.
        /// The type of this pointer is always <see cref="IntPtr"/>, which can be cast to the desired type of data
        /// pointer in order to be dereferenceable.
        /// If the function failed to allocate the requested block of memory, <see cref="IntPtr.Zero"/> is returned.
        /// </returns>
        /// <remarks>
        /// The content of the newly allocated block of memory is not initialized, remaining with indeterminate values.
        /// </remarks>
        /// <seealso cref="CreateEmptyMemory"/>
        [DllImport(UNMANAGED)]
        public static extern IntPtr CreateMemory(int size);

        /// <summary>
        /// Allocates a block of memory for an array of <paramref name="num"/> elements, each of them <paramref name="size"/>
        /// bytes long, and initializes all it bits to zero.
        /// </summary>
        /// <param name="num">
        /// Number of elements to allocate.
        /// </param>
        /// <param name="size">
        /// Size of each element.
        /// </param>
        /// <returns>
        /// On success, a pointer to the memory block allocated by the function.
        /// The type of this pointer is always <see cref="IntPtr"/>, which can be cast to the desired type of data
        /// pointer in order to be dereferenceable.
        /// If the function failed to allocate the requested block of memory, <see cref="IntPtr.Zero"/> is returned.
        /// </returns>
        /// <remarks>
        /// The effective result is the allocation of a zero-initialized memory block of (<paramref name="num"/>*<paramref name="size"/>) bytes.
        /// </remarks>
        [DllImport(UNMANAGED)]
        public static extern IntPtr CreateEmptyMemory(int num, int size);

        /// <summary>
        /// Changes the size of the memory block pointed to by <paramref name="ptr"/>.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to a memory block previously allocated with <see cref="CreateMemory(int)"/>,
        /// <see cref="CreateEmptyMemory(int, int)"/>, or <see cref="ResizeMemory(IntPtr, int)"/>.
        /// Alternatively, this can be a null pointer, in whch case a new block is allocated (as if
        /// <see cref="CreateMemory(int)"/> was called).
        /// </param>
        /// <param name="size">
        /// New size of the memory block, in bytes.
        /// </param>
        /// <returns>
        /// A pointer of the reallocated memory block, which may be either the same as <paramref name="ptr"/>
        /// or a new location.
        /// The type of this pointer is <see cref="IntPtr"/>, which can be cast to the desired type of data
        /// pointer in order to be dereferenceable.
        /// </returns>
        /// <remarks>
        /// The function may move the memory block to a new location (whose address is returned by the function).
        /// - The content of the memory block is preserved up to the lesser of the new and old sizes, even if the block
        /// is moved to a new location. If the new <paramref name="size"/> is larger, the value of the newly allocated
        /// portion is indeterminate.
        /// - In case that <paramref name="ptr"/> is <see cref="IntPtr.Zero"/>, the function behaves like <see cref="CreateMemory(int)"/>,
        /// assigning a new block of <paramref name="size"/> bytes and returning a pointer to its beginning.
        /// If the function fails to allocate the requested block of memory, a null pointer is returned, and the memory block pointed
        /// to by argument <paramref name="ptr"/> is not deallocated (it is still valid, and with its contents unchanged).
        /// </remarks>
        [DllImport(UNMANAGED)]
        public static extern IntPtr ResizeMemory(IntPtr ptr, int size);

        /// <summary>
        /// A block of memory previously allocated by a call to <see cref="CreateMemory(int)"/>, <see cref="CreateEmptyMemory(int, int)"/>,
        /// or <see cref="ResizeMemory(IntPtr, int)"/> is deallocated, making it available again for further allocations.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to a memory block previously allocated with <see cref="CreateMemory(int)"/>, <see cref="CreateEmptyMemory(int, int)"/>,
        /// or <see cref="ResizeMemory(IntPtr, int)"/>.
        /// </param>
        /// <remarks>
        /// If <paramref name="ptr"/> does not point to a block of memory allocated with the above functions, it causes undefined behavior.
        /// - If <paramref name="ptr"/> is null, the function does nothing.
        /// - Notice that this function does not change the value of <paramref name="ptr"/> itself, hence it still points to the same (now
        /// invalid) location.
        /// </remarks>
        [DllImport(UNMANAGED)]
        public static extern void FreeMemory(IntPtr ptr);

        /// <summary>
        /// Compares the first <paramref name="num"/> bytes of th block of memory pointed by <paramref name="ptr1"/> to the first <paramref name="num"/>
        /// bytes pointed by <paramref name="ptr2"/>.
        /// </summary>
        /// <param name="ptr1">
        /// Pointer to block of memory.
        /// </param>
        /// <param name="ptr2">
        /// Pointer to block of memory.
        /// </param>
        /// <param name="num">
        /// Number of bytes to compare.
        /// </param>
        /// <returns>
        /// true if the values all match, otherwise false.
        /// </returns>
        [DllImport(UNMANAGED)]
        public static extern bool CompareMemory(IntPtr ptr1, IntPtr ptr2, int num);

        /// <summary>
        /// Sets the first <paramref name="num"/> bytes of the block of memory pointed by <paramref name="ptr"/> to the
        /// specified <paramref name="value"/>.
        /// </summary>
        /// <param name="ptr">
        /// Pointer to the block of memory to fill.
        /// </param>
        /// <param name="value">
        /// Value to be set. The value is passed as an <see cref="Int32"/>, but the function fills the block of memory using the <see cref="Byte"/>
        /// of this <paramref name="value"/>.
        /// </param>
        /// <param name="num">
        /// Number of bytes to be set to the value.
        /// </param>
        /// <returns>
        /// <paramref name="ptr"/> is returned.
        /// </returns>
        [DllImport(UNMANAGED)]
        public static extern IntPtr SetMemory(IntPtr ptr, int value, int num);


        /// <summary>
        /// Copies the values of <paramref name="num"/> bytes from the location pointed by <paramref name="source"/> directly to the memory block
        /// pointed by <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination array where the content is to be copied, type-casted to a pointer of type <see cref="IntPtr"/>.
        /// </param>
        /// <param name="source">
        /// Pointer to the source of data to be copied, type-casted to a pointer of type <see cref="IntPtr"/>.
        /// </param>
        /// <param name="num">
        /// Number of bytes to copy.
        /// </param>
        /// <returns>
        /// <paramref name="destination"/> is returned.
        /// </returns>
        /// <remarks>
        /// The underlying type of the objects pointed by both the <paramref name="source"/> and <paramref name="destination"/> pointers are
        /// irrelevant for this function; The result is a binary copy of the data.
        /// - The function does not check for any terminating null character in <paramref name="source"/>, it always copies exactly
        /// <paramref name="num"/> bytes.
        /// - To avoid overflows, the size of the arrays pointed by both the <paramref name="destination"/> and <paramref name="source"/>
        /// parameters, shall be at least <paramref name="num"/> bytes, and should not overlap (for overlapping memory blocks,
        /// <see cref="MoveMemory(IntPtr, IntPtr, int)"/> is a safer approach).
        /// </remarks>
        [DllImport(UNMANAGED)]
        public static extern IntPtr CopyMemory(IntPtr destination, IntPtr source, int num);

        /// <summary>
        /// Copies the values of <paramref name="num"/> bytes from the location pointed by <paramref name="source"/> to the memory block
        /// pointed by <paramref name="destination"/>. Copying takes places as if an intermediate buffer were used, allowing the
        /// <paramref name="destination"/> and <paramref name="source"/> to overlap.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination array where the content is to be copied, type-casted to a pointer of type <see cref="IntPtr"/>.
        /// </param>
        /// <param name="source">
        /// Pointer to the source of data to be copied, type-casted to a pointer of type <see cref="IntPtr"/>.
        /// </param>
        /// <param name="num">
        /// Number of bytes to copy.
        /// </param>
        /// <returns>
        /// <paramref name="destination"/> is returned.
        /// </returns>
        /// <remarks>
        /// The underlying type of the objects pointed by both the <paramref name="source"/> and <paramref name="destination"/> pointers are
        /// irrelevant for this function; The result is a binary copy of the data.
        /// - The function does not check for any terminating null character in <paramref name="source"/>, it always copies exactly
        /// <paramref name="num"/> bytes.
        /// - To avoid overflows, the size of the arrays pointed by both the <paramref name="destination"/> and <paramref name="source"/>
        /// parameters, shall be at least <paramref name="num"/> bytes.
        /// </remarks>
        [DllImport(UNMANAGED)]
        public static extern IntPtr MoveMemory(IntPtr destination, IntPtr source, int num);
        #endregion
    }
}