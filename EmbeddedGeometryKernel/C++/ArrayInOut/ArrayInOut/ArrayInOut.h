
// ArrayInOut.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CArrayInOutApp:
// See ArrayInOut.cpp for the implementation of this class
//

class CArrayInOutApp : public CWinApp
{
public:
	CArrayInOutApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
	virtual BOOL InitApplication();
	virtual int ExitInstance();
};

extern CArrayInOutApp theApp;