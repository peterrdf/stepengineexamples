
// STP2JS-Example.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "STP2JS-Example.h"
#include "STP2JS-ExampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


wchar_t	* GL_fileName = nullptr, *GL_pathSchemas = nullptr, *GL_WebGL_URI = nullptr, *GL_jsFileName = nullptr, *GL_binFileName = nullptr;


// CSTP2JSExampleApp

BEGIN_MESSAGE_MAP(CSTP2JSExampleApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CSTP2JSExampleApp construction

CSTP2JSExampleApp::CSTP2JSExampleApp()
{
	// support Restart Manager
	m_dwRestartManagerSupportFlags = AFX_RESTART_MANAGER_SUPPORT_RESTART;

	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CSTP2JSExampleApp object

CSTP2JSExampleApp theApp;


// CSTP2JSExampleApp initialization

BOOL CSTP2JSExampleApp::InitInstance()
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

	CSTP2JSExampleDlg dlg;
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



BOOL CSTP2JSExampleApp::InitApplication()
{
	size_t	i = (size_t) wcslen(this->m_pszHelpFilePath) - (size_t) wcslen(this->m_pszExeName) - 4;

	ASSERT(GL_fileName == nullptr && GL_pathSchemas == nullptr && GL_binFileName == nullptr && GL_jsFileName == nullptr && GL_WebGL_URI == nullptr && i > 0);

	GL_fileName = new wchar_t[i + wcslen(L"as1-oc-214.stp") + 1];
	memcpy(&GL_fileName[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&GL_fileName[i], L"as1-oc-214.stp", (wcslen(L"as1-oc-214.stp") + 1) * sizeof(wchar_t));

	GL_binFileName = new wchar_t[i + wcslen(L"file.bin") + 1];
	memcpy(&GL_binFileName[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&GL_binFileName[i], L"file.bin", (wcslen(L"file.bin") + 1) * sizeof(wchar_t));

	GL_jsFileName = new wchar_t[i + wcslen(L"\\web\\webgl\\geom.js") + 1];
	memcpy(&GL_jsFileName[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&GL_jsFileName[i], L"\\web\\webgl\\geom.js", (wcslen(L"\\web\\webgl\\geom.js") + 1) * sizeof(wchar_t));

	GL_WebGL_URI = new wchar_t[i + wcslen(L"\\web\\webgl\\index.html") + 1];
	memcpy(&GL_WebGL_URI[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&GL_WebGL_URI[i], L"\\web\\webgl\\index.html", (wcslen(L"\\web\\webgl\\index.html") + 1) * sizeof(wchar_t));

	return CWinApp::InitApplication();
}
