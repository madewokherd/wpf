using System;
using System.Runtime.InteropServices;

namespace System.Printing {

public sealed class PrintDriver : PrintFilter
{
	internal PrintDriver()
	{
	}

	public sealed override void Commit()
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public sealed override void Refresh()
	{
		throw new NotImplementedException();
	}
}
}
