using System;

namespace System.Printing {

[Flags]
public enum EnumeratedPrintQueueTypes
{
	Connections = 0x10,
	DirectPrinting = 0x2,
	EnableBidi = 0x800,
	EnableDevQuery = 0x80,
	Fax = 0x4000,
	KeepPrintedJobs = 0x100,
	Local = 0x40,
	PublishedInDirectoryServices = 0x2000,
	PushedMachineConnection = 0x40000,
	PushedUserConnection = 0x20000,
	Queued = 0x1,
	RawOnly = 0x1000,
	Shared = 0x8,
	TerminalServer = 0x8000,
	WorkOffline = 0x400
}
}
