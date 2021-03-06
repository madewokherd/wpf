// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//------------------------------------------------------------------------------
//
//
//
//------------------------------------------------------------------------------
#pragma once

#include <windows.h> // for DUMMYUNIONNAME
#include "evntprov.h"
#pragma warning(push)
// Disable warning 4748 in the generated code for code coverage builds.
// Disable warning 4996 in the generated code, which uses deprecated GetVersionEx API. 
//    Since wpf-etw.h is automatically generated by running mc.exe (Message Compiler) - a 
//    standard Event Log tool that is part of the build environment - we do not have the 
//    ability to remove calls to GetVersionEx from within this header. 
#pragma warning(disable:4748 4061 4996)
#include "wpf-etw.h"
#pragma warning(pop)
#include "wpf-etw-valuemaps.h"

#define ETW_ENABLED_CHECK(level) (MICROSOFT_WINDOWS_WPF_PROVIDER_Context.IsEnabled && level <= MICROSOFT_WINDOWS_WPF_PROVIDER_Context.Level)
