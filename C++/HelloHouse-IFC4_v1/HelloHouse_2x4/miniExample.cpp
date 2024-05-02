////////////////////////////////////////////////////////////////////////
//  Author:  Peter Bonsma
//  Date:    31 July 2010
//  Project: IFC Engine Series (example using DLL)
//
//  This code may be used and edited freely,
//  also for commercial projects in open and closed source software
//
//  In case of use of the DLL:
//  be aware of license fee for use of this DLL when used commercially
//  more info for commercial use:	peter.bonsma@tno.nl
//
//  more info for using the IFC Engine DLL in other languages
//	and creation of specific code examples:
//									pim.vandenhelm@tno.nl
//								    peter.bonsma@rdf.bg
////////////////////////////////////////////////////////////////////////




#include "stdafx.h"
#include "miniExample.h"
#include "miniExampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMiniExampleApp

BEGIN_MESSAGE_MAP(CMiniExampleApp, CWinApp)
	//{{AFX_MSG_MAP(CMiniExampleApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

wchar_t    * ifcFileName;

/////////////////////////////////////////////////////////////////////////////
// CMiniExampleApp construction

CMiniExampleApp::CMiniExampleApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CMiniExampleApp object

CMiniExampleApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CMiniExampleApp initialization

BOOL CMiniExampleApp::InitInstance()
{
	AfxEnableControlContainer();

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

	CMiniExampleDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
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

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}

BOOL CMiniExampleApp::InitApplication() 
{
	int	i = wcslen(this->m_pszHelpFilePath) - wcslen(this->m_pszExeName) - 4;

	ASSERT(ifcFileName == NULL  &&  i > 0);

	ifcFileName = new wchar_t[i + wcslen(L"example.ifc") + 1];
	memcpy(&ifcFileName[0], this->m_pszHelpFilePath, i * sizeof(wchar_t));
	memcpy(&ifcFileName[i], L"example.ifc", (wcslen(L"example.ifc") + 1) * sizeof(wchar_t));
    
	return	CWinApp::InitApplication();
}
