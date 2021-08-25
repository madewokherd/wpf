using System;

namespace System.Printing {

[Flags]
public enum PrintQueueAttributes
{
	Direct = 0x2,
	EnableBidi = 0x800,
	EnableDevQuery = 0x80,
	Hidden = 0x20,
	KeepPrintedJobs = 0x100,
	None = 0x0,
	Published = 0x2000,
	Queued = 0x1,
	RawOnly = 0x1000,
	ScheduleCompletedJobsFirst = 0x200,
	Shared = 0x8
}
}
