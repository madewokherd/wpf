using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace MS.Internal.PrintWin32Thunk {

internal class XpsPrintStream : Stream
{
	public override bool CanRead
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanSeek
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanTimeout
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override bool CanWrite
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
		}
	}

	public static XpsPrintStream CreateXpsPrintStream()
	{
		throw new NotImplementedException();
	}

	protected override void Dispose(bool A_0)
	{
	}

	public override void Flush()
	{
	}

	public IStream GetManagedIStream()
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
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
	}
}
}
