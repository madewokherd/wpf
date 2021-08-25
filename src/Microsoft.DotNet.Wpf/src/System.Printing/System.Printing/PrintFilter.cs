using System;
using System.Runtime.InteropServices;

namespace System.Printing {

public abstract class PrintFilter : PrintSystemObject
{
	internal PrintFilter()
	{
	}

	protected override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}
}
}
