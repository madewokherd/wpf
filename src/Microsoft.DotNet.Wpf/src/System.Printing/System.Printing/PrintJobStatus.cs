using System;

namespace System.Printing {

[Flags]
public enum PrintJobStatus
{
	Blocked = 0x200,
	Completed = 0x1000,
	Deleted = 0x100,
	Deleting = 0x4,
	Error = 0x2,
	None = 0x0,
	Offline = 0x20,
	PaperOut = 0x40,
	Paused = 0x1,
	Printed = 0x80,
	Printing = 0x10,
	Restarted = 0x800,
	Retained = 0x2000,
	Spooling = 0x8,
	UserIntervention = 0x400
}
}
