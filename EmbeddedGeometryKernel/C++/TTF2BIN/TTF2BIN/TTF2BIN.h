
// TTF2BIN.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CTTF2BINApp:
// See TTF2BIN.cpp for the implementation of this class
//

class CTTF2BINApp : public CWinApp
{
public:
	CTTF2BINApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CTTF2BINApp theApp;