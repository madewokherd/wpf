using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

[Serializable]
public sealed class PrintStreamProperty : PrintProperty
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

	public PrintStreamProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintStreamProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator Stream(PrintStreamProperty attributeRef)
	{
		throw new NotImplementedException();
	}
}
}
