
// TTF2RDF.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CTTF2RDFApp:
// See TTF2RDF.cpp for the implementation of this class
//

class CTTF2RDFApp : public CWinApp
{
public:
	CTTF2RDFApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CTTF2RDFApp theApp;