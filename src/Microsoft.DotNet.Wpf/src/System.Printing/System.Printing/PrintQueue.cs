using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Xps;

namespace System.Printing {

public class PrintQueue : PrintSystemObject
{
	public virtual int AveragePagesPerMinute
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int ClientPrintSchemaVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual string Comment
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

	public PrintJobSettings CurrentJobSettings
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual PrintTicket DefaultPrintTicket
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

	public virtual int DefaultPriority
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

	public virtual string Description
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public string FullName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool HasPaperProblem
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool HasToner
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual PrintServer HostingPrintServer
	{
		get
		{
			throw new NotImplementedException();
		}
		protected set
		{
			throw new NotImplementedException();
		}
	}

	public bool InPartialTrust
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

	public bool IsBidiEnabled
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsBusy
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsDevQueryEnabled
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsDirect
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsDoorOpened
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsHidden
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

	public bool IsInitializing
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsIOActive
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsManualFeedRequired
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsNotAvailable
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

	public bool IsOutOfMemory
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsOutOfPaper
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsOutputBinFull
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPaperJammed
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

	public bool IsPendingDeletion
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPowerSaveOn
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

	public bool IsProcessing
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsPublished
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsQueued
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsRawOnlyEnabled
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsServerUnknown
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsShared
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsTonerLow
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsWaiting
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsWarmingUp
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool IsXpsDevice
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool KeepPrintedJobs
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual string Location
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

	public static int MaxPrintSchemaVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public sealed override string Name
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

	public bool NeedUserIntervention
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual int NumberOfJobs
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool PagePunt
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool PrintingIsCancelled
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

	public virtual int Priority
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

	public PrintQueueAttributes QueueAttributes
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual PrintDriver QueueDriver
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

	public virtual PrintPort QueuePort
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

	public virtual PrintProcessor QueuePrintProcessor
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

	public PrintQueueStatus QueueStatus
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public bool ScheduleCompletedJobsFirst
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			throw new NotImplementedException();
		}
	}

	public virtual string SeparatorFile
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

	public virtual string ShareName
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

	public virtual int StartTimeOfDay
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

	public virtual int UntilTimeOfDay
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

	public virtual PrintTicket UserPrintTicket
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

	public PrintQueue(PrintServer printServer, string printQueueName)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, int printSchemaVersion)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, int printSchemaVersion, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, PrintQueueIndexedProperty[] propertyFilter)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, PrintQueueIndexedProperty[] propertyFilter, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, string[] propertyFilter)
	{
		throw new NotImplementedException();
	}

	public PrintQueue(PrintServer printServer, string printQueueName, string[] propertyFilter, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintSystemJobInfo AddJob()
	{
		throw new NotImplementedException();
	}

	public PrintSystemJobInfo AddJob(string jobName)
	{
		throw new NotImplementedException();
	}

	public PrintSystemJobInfo AddJob(string jobName, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public PrintSystemJobInfo AddJob(string jobName, string documentPath, [MarshalAs(UnmanagedType.U1)] bool fastCopy)
	{
		throw new NotImplementedException();
	}

	public PrintSystemJobInfo AddJob(string jobName, string documentPath, [MarshalAs(UnmanagedType.U1)] bool fastCopy, PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public override void Commit()
	{
		throw new NotImplementedException();
	}

	public static XpsDocumentWriter CreateXpsDocumentWriter(ref double width, ref double height)
	{
		throw new NotImplementedException();
	}

	public static XpsDocumentWriter CreateXpsDocumentWriter(ref PrintDocumentImageableArea documentImageableArea)
	{
		throw new NotImplementedException();
	}

	public static XpsDocumentWriter CreateXpsDocumentWriter(ref PrintDocumentImageableArea documentImageableArea, ref PageRangeSelection pageRangeSelection, ref PageRange pageRange)
	{
		throw new NotImplementedException();
	}

	public static XpsDocumentWriter CreateXpsDocumentWriter(PrintQueue printQueue)
	{
		throw new NotImplementedException();
	}

	public static XpsDocumentWriter CreateXpsDocumentWriter(string jobDescription, ref PrintDocumentImageableArea documentImageableArea)
	{
		throw new NotImplementedException();
	}

	public static XpsDocumentWriter CreateXpsDocumentWriter(string jobDescription, ref PrintDocumentImageableArea documentImageableArea, ref PageRangeSelection pageRangeSelection, ref PageRange pageRange)
	{
		throw new NotImplementedException();
	}

	public PrintSystemJobInfo GetJob(int jobId)
	{
		throw new NotImplementedException();
	}

	public PrintCapabilities GetPrintCapabilities()
	{
		throw new NotImplementedException();
	}

	public PrintCapabilities GetPrintCapabilities(PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public MemoryStream GetPrintCapabilitiesAsXml()
	{
		throw new NotImplementedException();
	}

	public MemoryStream GetPrintCapabilitiesAsXml(PrintTicket printTicket)
	{
		throw new NotImplementedException();
	}

	public PrintJobInfoCollection GetPrintJobInfoCollection()
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public System.Printing.ValidationResult MergeAndValidatePrintTicket(PrintTicket basePrintTicket, PrintTicket deltaPrintTicket)
	{
		throw new NotImplementedException();
	}

	public System.Printing.ValidationResult MergeAndValidatePrintTicket(PrintTicket basePrintTicket, PrintTicket deltaPrintTicket, PrintTicketScope scope)
	{
		throw new NotImplementedException();
	}

	public virtual void Pause()
	{
		throw new NotImplementedException();
	}

	public virtual void Purge()
	{
		throw new NotImplementedException();
	}

	public override void Refresh()
	{
		throw new NotImplementedException();
	}

	public virtual void Resume()
	{
		throw new NotImplementedException();
	}
}
}
