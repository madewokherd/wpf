using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Printing.IndexedProperties {

[Serializable]
public abstract class PrintProperty : IDisposable, IDeserializationCallback
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

	protected internal bool IsInitialized
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
		[param: MarshalAs(UnmanagedType.U1)]
		protected set
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
	}

	public abstract object Value
	{
		get;
		set;
	}

	protected PrintProperty(string attributeName)
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}

	protected virtual void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public virtual void OnDeserialization(object sender)
	{
		throw new NotImplementedException();
	}
}
}
