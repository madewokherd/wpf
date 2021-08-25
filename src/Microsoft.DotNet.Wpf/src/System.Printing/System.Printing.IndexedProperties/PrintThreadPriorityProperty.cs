using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Printing.IndexedProperties {

public sealed class PrintThreadPriorityProperty : PrintProperty
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

	public PrintThreadPriorityProperty(string attributeName)
		: base(null)
	{
		throw new NotImplementedException();
	}

	public PrintThreadPriorityProperty(string attributeName, object attributeValue)
		: base(null)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public static implicit operator ThreadPriority(PrintThreadPriorityProperty attribRef)
	{
		throw new NotImplementedException();
	}
}
}
