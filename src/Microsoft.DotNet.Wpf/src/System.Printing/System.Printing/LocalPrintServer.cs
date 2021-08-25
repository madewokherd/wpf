using System;
using System.Runtime.InteropServices;

namespace System.Printing {

public sealed class LocalPrintServer : PrintServer
{
	public PrintQueue DefaultPrintQueue
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public LocalPrintServer()
	{
		throw new NotImplementedException();
	}

	public LocalPrintServer(LocalPrintServerIndexedProperty[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public LocalPrintServer(LocalPrintServerIndexedProperty[] propertiesFilter, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public LocalPrintServer(PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public LocalPrintServer(string[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public LocalPrintServer(string[] propertiesFilter, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public sealed override void Commit()
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool ConnectToPrintQueue(PrintQueue printer)
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool ConnectToPrintQueue(string printQueuePath)
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool DisconnectFromPrintQueue(PrintQueue printer)
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool DisconnectFromPrintQueue(string printQueuePath)
	{
		throw new NotImplementedException();
	}

	public static PrintQueue GetDefaultPrintQueue()
	{
		throw new NotImplementedException();
	}

	public sealed override void Refresh()
	{
		throw new NotImplementedException();
	}
}
}
