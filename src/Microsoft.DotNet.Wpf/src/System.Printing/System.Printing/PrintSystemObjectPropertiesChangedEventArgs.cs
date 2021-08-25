using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace System.Printing {

public class PrintSystemObjectPropertiesChangedEventArgs : EventArgs, IDisposable
{
	public StringCollection PropertiesNames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintSystemObjectPropertiesChangedEventArgs(StringCollection events)
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
