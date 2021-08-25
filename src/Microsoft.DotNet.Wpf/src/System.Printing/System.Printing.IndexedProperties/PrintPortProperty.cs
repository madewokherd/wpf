using System;
using System.Printing;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

public sealed class PrintPortProperty : PrintProperty
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

	public PrintPortProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintPortProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator PrintPort(PrintPortProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
