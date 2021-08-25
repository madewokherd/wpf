using System;
using System.Runtime.InteropServices;

namespace MS.Internal.PrintWin32Thunk {

internal class SafeMemoryHandle : SafeHandle
{
	public override bool IsInvalid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public static SafeMemoryHandle Null
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual int Size
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public SafeMemoryHandle(IntPtr Win32Pointer)
		: base((IntPtr)0, ownsHandle: false)
	{
	}

	public void CopyFromArray(byte[] source, int startIndex, int length)
	{
	}

	public void CopyToArray(byte[] destination, int startIndex, int length)
	{
	}

	public static SafeMemoryHandle Create(int byteCount)
	{
		throw new NotImplementedException();
	}

	protected override bool ReleaseHandle()
	{
		throw new NotImplementedException();
	}

	public static bool TryCreate(int byteCount, ref SafeMemoryHandle result)
	{
		throw new NotImplementedException();
	}

	public static SafeMemoryHandle Wrap(IntPtr Win32Pointer)
	{
		throw new NotImplementedException();
	}
}
}
