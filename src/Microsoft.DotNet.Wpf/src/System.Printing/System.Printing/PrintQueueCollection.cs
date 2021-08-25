using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Printing {

public class PrintQueueCollection : PrintSystemObjects, IEnumerable<PrintQueue>, IEnumerable, IDisposable
{
	public static object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintQueueCollection()
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection(PrintServer printServer, string[] propertyFilter)
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection(PrintServer printServer, string[] propertyFilter, EnumeratedPrintQueueTypes[] enumerationFlag)
	{
		throw new NotImplementedException();
	}

	public void Add(PrintQueue printObject)
	{
		throw new NotImplementedException();
	}

	protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}

	public virtual IEnumerator<PrintQueue> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	public virtual IEnumerator GetNonGenericEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}
}
}
