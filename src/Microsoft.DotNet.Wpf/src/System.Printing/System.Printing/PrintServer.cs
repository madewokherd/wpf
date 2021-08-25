using System;
using System.Printing.IndexedProperties;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Printing {

public class PrintServer : PrintSystemObject
{
	public bool BeepEnabled
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

	public ThreadPriority DefaultPortThreadPriority
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public ThreadPriority DefaultSchedulerPriority
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public string DefaultSpoolDirectory
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

	public PrintServerEventLoggingTypes EventLog
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

	protected bool IsDelayInitialized
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

	public int MajorVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public int MinorVersion
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
			base.Name = value;
		}
	}

	public bool NetPopup
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

	public ThreadPriority PortThreadPriority
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

	public bool RestartJobOnPoolEnabled
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

	public int RestartJobOnPoolTimeout
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

	public ThreadPriority SchedulerPriority
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

	public byte SubSystemVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public PrintServer()
	{
		throw new NotImplementedException();
	}

	public PrintServer(PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintServer(string path)
	{
		throw new NotImplementedException();
	}

	public PrintServer(string path, PrintServerIndexedProperty[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public PrintServer(string path, PrintServerIndexedProperty[] propertiesFilter, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintServer(string path, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public PrintServer(string path, string[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public PrintServer(string path, string[] propertiesFilter, PrintSystemDesiredAccess desiredAccess)
	{
		throw new NotImplementedException();
	}

	public override void Commit()
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static bool DeletePrintQueue(PrintQueue printQueue)
	{
		throw new NotImplementedException();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static bool DeletePrintQueue(string printQueueName)
	{
		throw new NotImplementedException();
	}

	public PrintQueue GetPrintQueue(string printQueueName)
	{
		throw new NotImplementedException();
	}

	public PrintQueue GetPrintQueue(string printQueueName, string[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection GetPrintQueues()
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection GetPrintQueues(EnumeratedPrintQueueTypes[] enumerationFlag)
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection GetPrintQueues(PrintQueueIndexedProperty[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection GetPrintQueues(PrintQueueIndexedProperty[] propertiesFilter, EnumeratedPrintQueueTypes[] enumerationFlag)
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection GetPrintQueues(string[] propertiesFilter)
	{
		throw new NotImplementedException();
	}

	public PrintQueueCollection GetPrintQueues(string[] propertiesFilter, EnumeratedPrintQueueTypes[] enumerationFlag)
	{
		throw new NotImplementedException();
	}

	public PrintQueue InstallPrintQueue(string printQueueName, string driverName, string[] portNames, string printProcessorName, PrintPropertyDictionary initialParameters)
	{
		throw new NotImplementedException();
	}

	public PrintQueue InstallPrintQueue(string printQueueName, string driverName, string[] portNames, string printProcessorName, PrintQueueAttributes printQueueAttributes)
	{
		throw new NotImplementedException();
	}

	public PrintQueue InstallPrintQueue(string printQueueName, string driverName, string[] portNames, string printProcessorName, PrintQueueAttributes printQueueAttributes, PrintQueueStringProperty printQueueProperty, int printQueuePriority, int printQueueDefaultPriority)
	{
		throw new NotImplementedException();
	}

	public PrintQueue InstallPrintQueue(string printQueueName, string driverName, string[] portNames, string printProcessorName, PrintQueueAttributes printQueueAttributes, string printQueueShareName, string printQueueComment, string printQueueLocation, string printQueueSeparatorFile, int printQueuePriority, int printQueueDefaultPriority)
	{
		throw new NotImplementedException();
	}

	protected sealed override void InternalDispose([MarshalAs(UnmanagedType.U1)] bool disposing)
	{
		throw new NotImplementedException();
	}

	public override void Refresh()
	{
		throw new NotImplementedException();
	}
}
}
