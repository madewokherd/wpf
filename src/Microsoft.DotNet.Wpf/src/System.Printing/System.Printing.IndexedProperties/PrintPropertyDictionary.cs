using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Printing.IndexedProperties {

[Serializable]
public class PrintPropertyDictionary : Hashtable, IDisposable, IDeserializationCallback, ISerializable
{
	public PrintPropertyDictionary()
	{
		throw new NotImplementedException();
	}

	protected PrintPropertyDictionary(SerializationInfo info, StreamingContext context)
	{
		throw new NotImplementedException();
	}

	public void Add(PrintProperty attributeValue)
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		throw new NotImplementedException();
	}

	public PrintProperty GetProperty(string attribName)
	{
		throw new NotImplementedException();
	}

	public override void OnDeserialization(object sender)
	{
		throw new NotImplementedException();
	}

	public void SetProperty(string attribName, PrintProperty attribValue)
	{
		throw new NotImplementedException();
	}
}
}
