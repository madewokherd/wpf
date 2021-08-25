using System;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

public sealed class PrintByteArrayProperty : PrintProperty
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

	public PrintByteArrayProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintByteArrayProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator byte[](PrintByteArrayProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
