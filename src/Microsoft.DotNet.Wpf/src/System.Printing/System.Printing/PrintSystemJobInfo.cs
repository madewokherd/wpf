using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Printing {

public class PrintSystemJobInfo : PrintSystemObject
{
	public PrintQueue HostingPrintQueue
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintServer HostingPrintServer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsBlocked
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsCompleted
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsDeleted
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsDeleting
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsInError
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsOffline
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPaperOut
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPaused
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPrinted
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPrinting
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsRestarted
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsRetained
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsSpooling
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsUserInterventionRequired
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

	public string JobName
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

	public int JobSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintJobStatus JobStatus
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public Stream JobStream
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int NumberOfPages
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int NumberOfPagesPrinted
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int PositionInPrintQueue
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintJobPriority Priority
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int StartTimeOfDay
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public string Submitter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public DateTime TimeJobSubmitted
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int TimeSinceStartedPrinting
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int UntilTimeOfDay
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	internal PrintSystemJobInfo()
	{
	}

	public void Cancel()
	{
		throw new NotImplementedException();
	}

	public override void Commit()
	{
		throw new NotImplementedException();
	}

	public static PrintSystemJobInfo Get(PrintQueue printQueue, int jobIdentifier)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public void Pause()
	{
		throw new NotImplementedException();
	}

	public override void Refresh()
	{
		throw new NotImplementedException();
	}

	public void Restart()
	{
		throw new NotImplementedException();
	}

	public void Resume()
	{
		throw new NotImplementedException();
	}
}
}
