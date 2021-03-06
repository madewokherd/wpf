// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


//+----------------------------------------------------------------------------
//

//
//-----------------------------------------------------------------------------

#include "precomp.hpp"

CDWriteFactory g_DWriteLoader;

//+-----------------------------------------------------------------------------
//
//  Member:
//      CDWriteFactory::CDWriteFactory
//
//  Synopsis:
//      ctor
//
//------------------------------------------------------------------------------
CDWriteFactory::CDWriteFactory() 
{
}

//+-----------------------------------------------------------------------------
//
//  Member:
//      CDWriteFactory::~CDWriteFactory
//
//  Synopsis:
//      dtor. Unloads DWrite.
//
//------------------------------------------------------------------------------
CDWriteFactory::~CDWriteFactory()
{
    Shutdown();
}

//+-----------------------------------------------------------------------------
//
//  Member:
//      CDWriteFactory::Startup
//
//  Synopsis:
//      Initializes the CS. This is not done in the ctor because this can fail. This MUST be called
//      before using CDWriteFactory.
//
//------------------------------------------------------------------------------

HRESULT
CDWriteFactory::Startup()
{
    HRESULT hr = S_OK;
    
    Assert(!m_csManagement.IsValid());

    MIL_THR(m_csManagement.Init());
    
    Assert(m_csManagement.IsValid() || FAILED(hr));

    RRETURN(hr);
}

//+-----------------------------------------------------------------------------
//
//  Member:
//      CDWriteFactory::Shutdown
//
//  Synopsis:
//      Unloads DWrite
//
//------------------------------------------------------------------------------
void
CDWriteFactory::Shutdown()
{
    m_csManagement.DeInit();
}

//+-----------------------------------------------------------------------------
//
//  Member:
//      CDWriteFactory::DWriteCreateFactory
//
//  Synopsis:
//      Loads DWrite if needed and returns the requested factory
//
//------------------------------------------------------------------------------
HRESULT 
CDWriteFactory::DWriteCreateFactory(
    __in DWRITE_FACTORY_TYPE factoryType,
    __in REFIID iid,
    __out IUnknown **factory
    )
{
    HRESULT hr = S_OK;
    
    IFC(::DWriteCreateFactory(factoryType, iid, factory));
    
Cleanup:
    RRETURN(hr);
}


