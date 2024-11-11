
// RepairMeshes.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CRepairMeshesApp:
// See RepairMeshes.cpp for the implementation of this class
//

class CRepairMeshesApp : public CWinApp
{
public:
	CRepairMeshesApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
	virtual BOOL InitApplication();
	virtual int ExitInstance();
};

extern CRepairMeshesApp theApp;