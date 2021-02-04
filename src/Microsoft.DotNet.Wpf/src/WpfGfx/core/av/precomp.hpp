// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


/**************************************************************************
*

*
* Module Name:
*
*
* Created:
*
*
**************************************************************************/

#define INITGUID

#include <wpfsdl.h>
#include "std.h"
#include "d2d1.h"

#ifndef _MSC_VER
// Wine Mono hack: There are way too many "overrides" in the wrong place, just ignore them.
#define override
#endif

#include "common\common.h"
#include "scanop\scanop.h"

// needed for sw.h
#include "glyph\glyph.h"

// needed for api_include.h
#include "geometry\geometry.h"

// needed for resources.h
#include "api\api_include.h"

// Needed for sw.h and hw.h
#include "targets\targets.h"

// Needed for hw.h
#include "sw\sw.h"

// Needed for CD3DDeviceLevel1
#include "hw\hw.h"

#include "resources\resources.h"

#define EC_COMPLETE                         0x01

#include <wmprealestate.h>
#include <crescent\wmpdll_i.c>
#include <strmif.h>
#include <oleauto.h>
#include <stdio.h>
#include <wmp.h>
#include <wmpservices.h>
#include <wmpids.h>
#include <control.h> // for IVideoWindow
#include <uuids.h>
#include <d3d9.h>
#include <vmr9.h>
#include <time.h>
#include <attributesbase.h>
#include <objsafe.h>

#define NOTIMPL_METHOD { RIP("METHOD NOT IMPLEMENTED"); RRETURN(E_NOTIMPL); }
#include "nserror.h"
#include "globals.h"
#include "util.h"
#include "av.h"
#include "wgx_av_types.h"

#include "StateThread.h"
#include "mediaeventproxy.h"
#include "UniqueList.h"
#include "CompositionNotifier.h"
#include "MediaInstance.h"
#include "mediabuffer.h"
#include "avtrace.h"
#include "dxvamanagerwrapper.h"
#include "internal.h"
#include "SampleQueue.h"
#include "SampleScheduler.h"
#include "ClockWrapper.h"
#include "EvrPresenter.h"
#include "DummyPlayer.h"
#include "fakepp.h"
#include "WmpEventHandler.h"
#include "playerstate.h"
#include "PresenterWrapper.h"
#include "WmpStateEngine.h"
#include "WmpStateEngineProxy.h"
#include "SharedState.h"
#include "WmpPlayer.h"
#include "WmpClientSite.h"
#include "Wmp11ClientSite.h"
#include "avloader.h"
#include "EvrFilterWrapper.h"
#include "hwmediabuffer.h"
#include "swmediabuffer.h"
#include "UpdateState.h"

#include "activate.h"

#define OAFALSE (0)

//
// This function is defined in Streams.h but I cannot include that header since it has a naming
// clash with milcore's queue.h
//
void WINAPI DeleteMediaType(
    AM_MEDIA_TYPE *pmt
);


#define WPP_INIT_TRACING(...)
#define WPP_CLEANUP(...)
#define LogAVDataX(...)
#define LogAVDataM(...)
#define TRACEF(...)
#define TRACEFID(...)
#define EXPECT_SUCCESS(...)
#define EXPECT_SUCCESSID(...)

#define LODWORD(x) ((DWORD)x)
