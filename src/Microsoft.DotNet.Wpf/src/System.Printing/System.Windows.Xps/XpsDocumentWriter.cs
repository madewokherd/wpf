using System;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Media;

namespace System.Windows.Xps {

public class XpsDocumentWriter : SerializerWriter
{
	public override event WritingCancelledEventHandler WritingCancelled
	{
		add
		{
		}
		remove
		{
		}
	}

	public override event WritingCompletedEventHandler WritingCompleted
	{
		add
		{
		}
		remove
		{
		}
	}

	public override event WritingPrintTicketRequiredEventHandler WritingPrintTicketRequired
	{
		add
		{
		}
		remove
		{
		}
	}

	public override event WritingProgressChangedEventHandler WritingProgressChanged
	{
		add
		{
		}
		remove
		{
		}
	}

	internal XpsDocumentWriter()
	{
	}

	public override void CancelAsync()
	{
		throw new NotImplementedException();
	}

	public override SerializerWriterCollator CreateVisualsCollator()
	{
		throw new NotImplementedException();
	}

	public override SerializerWriterCollator CreateVisualsCollator(PrintTicket documentSequencePrintTicket, PrintTicket documentPrintTicket)
	{
		throw new NotImplementedException();
	}

	public virtual void raise_WritingCancelled(object sender, WritingCancelledEventArgs args)
	{
		throw new NotImplementedException();
	}

	public virtual void raise_WritingCompleted(object sender, WritingCompletedEventArgs e)
	{
		throw new NotImplementedException();
	}

	public virtual void raise_WritingPrintTicketRequired(object sender, WritingPrintTicketRequiredEventArgs e)
	{
		throw new NotImplementedException();
	}

	public virtual void raise_WritingProgressChanged(object sender, WritingProgressChangedEventArgs e)
	{
		throw new NotImplementedException();
	}

	public void Write(string documentPath)
	{
		throw new NotImplementedException();
	}

	public void Write(string documentPath, XpsDocumentNotificationLevel notificationLevel)
	{
		throw new NotImplementedException();
	}

	public override void Write(DocumentPaginator documentPaginator)
	{
		throw new NotImplementedException();
	}

	public override void Write(DocumentPaginator documentPaginator, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void Write(FixedDocument fixedDocument)
	{
		throw new NotImplementedException();
	}

	public override void Write(FixedDocument fixedDocument, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void Write(FixedDocumentSequence fixedDocumentSequence)
	{
		throw new NotImplementedException();
	}

	public override void Write(FixedDocumentSequence fixedDocumentSequence, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void Write(FixedPage fixedPage)
	{
		throw new NotImplementedException();
	}

	public override void Write(FixedPage fixedPage, PrintTicket printTicket)
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

	public void WriteAsync(string documentPath)
	{
		throw new NotImplementedException();
	}

	public void WriteAsync(string documentPath, XpsDocumentNotificationLevel notificationLevel)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(DocumentPaginator documentPaginator)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(DocumentPaginator documentPaginator, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(DocumentPaginator documentPaginator, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(DocumentPaginator documentPaginator, PrintTicket printTicket, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocument fixedDocument)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocument fixedDocument, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocument fixedDocument, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocument fixedDocument, PrintTicket printTicket, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocumentSequence fixedDocumentSequence)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocumentSequence fixedDocumentSequence, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocumentSequence fixedDocumentSequence, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedDocumentSequence fixedDocumentSequence, PrintTicket printTicket, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedPage fixedPage)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedPage fixedPage, object userSuppliedState)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedPage fixedPage, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void WriteAsync(FixedPage fixedPage, PrintTicket printTicket, object userSuppliedState)
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
