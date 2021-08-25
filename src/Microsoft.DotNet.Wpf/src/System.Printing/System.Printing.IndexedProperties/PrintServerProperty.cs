using System;
using System.Printing;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

public sealed class PrintServerProperty : PrintProperty
{
	public override object Value
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

	public PrintServerProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintServerProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator PrintServer(PrintServerProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
