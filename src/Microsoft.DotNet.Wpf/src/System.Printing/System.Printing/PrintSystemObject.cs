using System;
using System.Printing.IndexedProperties;
using System.Runtime.InteropServices;

namespace System.Printing {

public abstract class PrintSystemObject : IDisposable
{
	protected bool IsDisposed
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			throw new NotImplementedException();
		}
	}

	public virtual string Name
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

	public virtual PrintSystemObject Parent
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintPropertyDictionary PropertiesCollection
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	protected PrintSystemObject()
	{
		throw new NotImplementedException();
	}

	protected PrintSystemObject(PrintSystemObjectLoadMode mode)
	{
		throw new NotImplementedException();
	}

	protected static string[] BaseAttributeNames()
	{
		throw new NotImplementedException();
	}

	public abstract void Commit();

	public void Dispose()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}

	protected void Initialize()
	{
		throw new NotImplementedException();
	}

	protected virtual void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public abstract void Refresh();
}
}
