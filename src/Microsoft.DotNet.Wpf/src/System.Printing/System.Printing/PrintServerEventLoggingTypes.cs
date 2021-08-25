using System;

namespace System.Printing {

[Flags]
public enum PrintServerEventLoggingTypes
{
	LogAllPrintingEvents = 0x5,
	LogPrintingErrorEvents = 0x2,
	LogPrintingInformationEvents = 0x4,
	LogPrintingSuccessEvents = 0x1,
	LogPrintingWarningEvents = 0x3,
	None = 0x0
}
}
