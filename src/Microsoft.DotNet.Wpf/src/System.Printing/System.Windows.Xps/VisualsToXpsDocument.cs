using System;
using System.Printing;
using System.Windows.Documents.Serialization;
using System.Windows.Media;

namespace System.Windows.Xps {

public class VisualsToXpsDocument : SerializerWriterCollator
{
	internal VisualsToXpsDocument()
	{
	}

	public override void BeginBatchWrite()
	{
		throw new NotImplementedException();
	}

	public override void Cancel()
	{
		throw new NotImplementedException();
	}

	public override void CancelAsync()
	{
		throw new NotImplementedException();
	}

	public override void EndBatchWrite()
	{
		throw new NotImplementedException();
	}

	public override void Write(Visual visual)
	{
		throw new NotImplementedException();
	}

	public override void Write(Visual visual, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(Visual visual)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(Visual visual, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(Visual visual, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(Visual visual, PrintTicket printTicket, object userSuppliedState)
	{
		throw new NotImplementedException();
	}
}
}
