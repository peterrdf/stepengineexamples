
// STP2JS-Example.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CSTP2JSExampleApp:
// See STP2JS-Example.cpp for the implementation of this class
//

class CSTP2JSExampleApp : public CWinApp
{
public:
	CSTP2JSExampleApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
	virtual BOOL InitApplication();
};

extern CSTP2JSExampleApp theApp;