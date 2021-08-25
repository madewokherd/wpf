using System;
using System.Runtime.InteropServices;

namespace System.Printing {

public class PrintSystemObjectPropertyChangedEventArgs : EventArgs, IDisposable
{
	public string PropertyName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintSystemObjectPropertyChangedEventArgs(string eventName)
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
