#include <stdlib.h>
#include <string.h>

//Defines a keyword for exporting dll functions
#define EXPORT extern "C" __declspec(dllexport)

EXPORT void* CreateMemory(const int size)
{
	return malloc(size);
}

EXPORT void* CreateEmptyMemory(const int num, const int size)
{
	return calloc(num, size);
}

EXPORT void* ResizeMemory(void* ptr, const int size)
{
	return realloc(ptr, size);
}

EXPORT void FreeMemory(void* ptr)
{
	free(ptr);
}

EXPORT bool CompareMemory(const void* ptr1, const void* ptr2, int num)
{
	return memcmp(ptr1, ptr2, num) == 0;
}

EXPORT void* SetMemory(void* ptr, int value, int num)
{
	return memset(ptr, value, num);
}

EXPORT void* CopyMemory(void* destination, const void* source, int size)
{
	return memcpy(destination, source, size);
}

EXPORT void* MoveMemory(void* destination, const void* source, int size)
{
	return memmove(destination, source, size);
}