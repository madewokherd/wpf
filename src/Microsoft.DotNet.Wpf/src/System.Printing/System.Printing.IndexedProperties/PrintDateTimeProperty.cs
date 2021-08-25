using System;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

[Serializable]
public sealed class PrintDateTimeProperty : PrintProperty
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

	public PrintDateTimeProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintDateTimeProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator ValueType(PrintDateTimeProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
