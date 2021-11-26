// Wrappers calling SetLastError before a winapi call.

#include <windows.h>

LONG WINAPI GetWindowLongWrapper(HWND hwnd, INT index)
{
	SetLastError(0);
	return GetWindowLongW(hwnd, index);
}

LONG_PTR WINAPI GetWindowLongPtrWrapper(HWND hwnd, INT index)
{
	SetLastError(0);
	return GetWindowLongPtrW(hwnd, index);
}

INT WINAPI GetWindowTextWrapper(HWND hwnd, LPWSTR string, INT length)
{
	SetLastError(0);
	return GetWindowTextW(hwnd, string, length);
}

HWND WINAPI GetParentWrapper(HWND hwnd)
{
	SetLastError(0);
	return GetParent(hwnd);
}

HWND WINAPI SetFocusWrapper(HWND hwnd)
{
	SetLastError(0);
	return SetFocus(hwnd);
}

LONG WINAPI SetWindowLongWrapper(HWND hwnd, INT index, LONG newlong)
{
	SetLastError(0);
	return SetWindowLongW(hwnd, index, newlong);
}

LONG_PTR WINAPI SetWindowLongPtrWrapper(HWND hwnd, INT index, LONG_PTR newlong)
{
	SetLastError(0);
	return SetWindowLongPtrW(hwnd, index, newlong);
}

INT WINAPI GetKeyboardLayoutListWrapper(INT size, HKL* list)
{
	SetLastError(0);
	return GetKeyboardLayoutList(size, list);
}

INT WINAPI MapWindowPointsWrapper(HWND from, HWND to, LPPOINT points, UINT count)
{
	SetLastError(0);
	return MapWindowPoints(from, to, points, count);
}

BOOL WINAPI EnableWindowWrapper(HWND hwnd, BOOL enable)
{
	SetLastError(0);
	return EnableWindow(hwnd, enable);
}
