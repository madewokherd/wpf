using System;
using System.Printing;
using System.Runtime.InteropServices;

namespace System.Printing.IndexedProperties {

[Serializable]
public sealed class PrintJobStatusProperty : PrintProperty
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

	public PrintJobStatusProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintJobStatusProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator PrintJobStatus(PrintJobStatusProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
