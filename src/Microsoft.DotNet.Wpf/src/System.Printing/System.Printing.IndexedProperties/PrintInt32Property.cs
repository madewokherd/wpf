using System;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

public sealed class PrintInt32Property : PrintProperty
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

	public PrintInt32Property(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintInt32Property(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator int(PrintInt32Property attribRef)
	{
		throw new NotImplementedException();
	}
}
}
