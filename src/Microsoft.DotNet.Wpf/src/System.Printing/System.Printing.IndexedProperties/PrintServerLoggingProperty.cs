using System;
using System.Printing;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

public sealed class PrintServerLoggingProperty : PrintProperty
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

	public PrintServerLoggingProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintServerLoggingProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator PrintServerEventLoggingTypes(PrintServerLoggingProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
