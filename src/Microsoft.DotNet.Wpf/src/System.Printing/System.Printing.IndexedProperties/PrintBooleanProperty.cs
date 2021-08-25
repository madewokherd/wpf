using System;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

public sealed class PrintBooleanProperty : PrintProperty
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

	public PrintBooleanProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintBooleanProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static implicit operator bool(PrintBooleanProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
