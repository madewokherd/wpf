using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Xps.Packaging;

namespace System.Printing {

public class PrintQueueStream : Stream
{
	public override bool CanRead
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanSeek
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanWrite
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public int JobIdentifier
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override long Length
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override long Position
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

	public PrintQueueStream(PrintQueue printQueue, string printJobName)
	{
		throw new NotImplementedException();
	}

	public PrintQueueStream(PrintQueue printQueue, string printJobName, [MarshalAs(UnmanagedType.U1)] bool commitDataOnClose)
	{
		throw new NotImplementedException();
	}

	public PrintQueueStream(PrintQueue printQueue, string printJobName, [MarshalAs(UnmanagedType.U1)] bool commitDataOnClose, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
	{
		throw new NotImplementedException();
	}

	public override void Close()
	{
		throw new NotImplementedException();
	}

	protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
	{
	}

	public override void EndWrite(IAsyncResult asyncResult)
	{
		throw new NotImplementedException();
	}

	public override void Flush()
	{
		throw new NotImplementedException();
	}

	public void HandlePackagingProgressEvent(object sender, PackagingProgressEventArgs e)
	{
		throw new NotImplementedException();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		throw new NotImplementedException();
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotImplementedException();
	}
}
}
