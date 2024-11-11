
// RepairMeshes.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "RepairMeshes.h"
#include "RepairMeshesDlg.h"

#include <assert.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


wchar_t	* fileName_STEP1_output = nullptr,
		* fileName_STEP3_output = nullptr;


// CRepairMeshesApp

BEGIN_MESSAGE_MAP(CRepairMeshesApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CRepairMeshesApp construction

CRepairMeshesApp::CRepairMeshesApp()
{
	// support Restart Manager
	m_dwRestartManagerSupportFlags = AFX_RESTART_MANAGER_SUPPORT_RESTART;

	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CRepairMeshesApp object

CRepairMeshesApp theApp;


// CRepairMeshesApp initialization

BOOL CRepairMeshesApp::InitInstance()
{
	// InitCommonControlsEx() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// Set this to include all the common control classes you want to use
	// in your application.
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();


	AfxEnableControlContainer();

	// Create the shell manager, in case the dialog contains
	// any shell tree view or shell list view controls.
	CShellManager *pShellManager = new CShellManager;

	// Activate "Windows Native" visual manager for enabling themes in MFC controls
	CMFCVisualManager::SetDefaultManager(RUNTIME_CLASS(CMFCVisualManagerWindows));

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	// of your final executable, you should remove from the following
	// the specific initialization routines you do not need
	// Change the registry key under which our settings are stored
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization
	SetRegistryKey(_T("Local AppWizard-Generated Applications"));

	CRepairMeshesDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}
	else if (nResponse == -1)
	{
		TRACE(traceAppMsg, 0, "Warning: dialog creation failed, so application is terminating unexpectedly.\n");
		TRACE(traceAppMsg, 0, "Warning: if you are using MFC controls on the dialog, you cannot #define _AFX_NO_MFC_CONTROLS_IN_DIALOGS.\n");
	}

	// Delete the shell manager created above.
	if (pShellManager != NULL)
	{
		delete pShellManager;
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}



BOOL CRepairMeshesApp::InitApplication()
{
	int	i = (int) wcslen(this->m_pszHelpFilePath) - (int) wcslen(this->m_pszExeName) - 4;

	assert(fileName_STEP1_output == nullptr && fileName_STEP3_output == nullptr && i);

	fileName_STEP1_output = new wchar_t[i + wcslen(L"STEP1-output.bin") + 1];
	memcpy(&fileName_STEP1_output[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&fileName_STEP1_output[i], L"STEP1-output.bin", (wcslen(L"STEP1-output.bin") + 1) * sizeof(wchar_t));

	fileName_STEP3_output = new wchar_t[i + wcslen(L"STEP3-output.bin") + 1];
	memcpy(&fileName_STEP3_output[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&fileName_STEP3_output[i], L"STEP3-output.bin", (wcslen(L"STEP3-output.bin") + 1) * sizeof(wchar_t));

	return CWinApp::InitApplication();
}


int CRepairMeshesApp::ExitInstance()
{
	if (fileName_STEP1_output) {
		delete[] fileName_STEP1_output;
		fileName_STEP1_output = nullptr;
	}

	if (fileName_STEP3_output) {
		delete[] fileName_STEP3_output;
		fileName_STEP3_output = nullptr;
	}

	return CWinApp::ExitInstance();
}
