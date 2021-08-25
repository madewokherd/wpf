using System;
using System.Runtime.InteropServices;

namespace System.Printing {

public abstract class PrintSystemObjects : IDisposable
{
	protected PrintSystemObjects()
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}
}
}
