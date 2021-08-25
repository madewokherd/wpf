using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Printing {

public class PrintJobInfoCollection : PrintSystemObjects, IEnumerable<PrintSystemJobInfo>, IEnumerable, IDisposable
{
	public PrintJobInfoCollection(PrintQueue printQueue, string[] propertyFilter)
	{
		throw new NotImplementedException();
	}

	public void Add(PrintSystemJobInfo printObject)
	{
		throw new NotImplementedException();
	}

	protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}

	public virtual IEnumerator<PrintSystemJobInfo> GetEnumerator()
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
