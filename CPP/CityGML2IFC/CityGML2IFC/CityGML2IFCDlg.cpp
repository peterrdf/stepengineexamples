
// CityGML2IFCDlg.cpp : implementation file
//

#include "pch.h"
#include "framework.h"
#include "CityGML2IFC.h"
#include "CityGML2IFCDlg.h"
#include "afxdialogex.h"


#include "baseIfc.h"

#include "_gis2ifc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// ************************************************************************************************
void STDCALL LogCallbackImpl(enumLogEvent enLogEvent, const char* szEvent)
{
	//#todo - progress dlg
	TRACE(L"\n%d: %S", (int)enLogEvent, szEvent);
}

void CCityGML2IFCDlg::ExportFile(const wstring& strInputFile)
{
	assert(!m_strRootFolder.empty());
	assert(!strInputFile.empty());

	wstring strOutputFile = strInputFile;
	strOutputFile += L".ifc";

	_gis2ifc exporter(m_strRootFolder, LogCallbackImpl);
	exporter.execute(strInputFile, strOutputFile);
}

void CCityGML2IFCDlg::ExportFiles(const fs::path& pthInputFolder)
{
	for (const auto& entry : fs::directory_iterator(pthInputFolder))
	{
		if (fs::is_directory(entry))
		{
			ExportFiles(entry.path());

			continue;
		}

		ExportFile(entry.path());
	}
}

// ************************************************************************************************
// CAboutDlg dialog used for App About
class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()

// ************************************************************************************************
// CCityGML2IFCDlg dialog
CCityGML2IFCDlg::CCityGML2IFCDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_CITYGML2IFC_DIALOG, pParent)
	, m_strRootFolder(L"")
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CCityGML2IFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CCityGML2IFCDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CCityGML2IFCDlg::OnBnClickedOk)
END_MESSAGE_MAP()

// ************************************************************************************************
// CCityGML2IFCDlg message handlers
BOOL CCityGML2IFCDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != nullptr)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CCityGML2IFCDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CCityGML2IFCDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CCityGML2IFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CCityGML2IFCDlg::OnBnClickedOk()
{
	wchar_t szAppPath[_MAX_PATH];
	::GetModuleFileName(::GetModuleHandle(nullptr), szAppPath, sizeof(szAppPath));

	fs::path pthExe = szAppPath;
	auto pthRootFolder = pthExe.parent_path();
	m_strRootFolder = pthRootFolder.wstring();
	m_strRootFolder += L"\\";

	/*{
		wstring strInputFile = L"D:\\Temp\\gisengine in\\InfraGMLTest\\DenHaag_01.xml";
		ExportFile(strInputFile);
	}*/

	/* TEST */
	{
		wstring strInputFolder = L"D:\\Temp\\gisengine in";
		ExportFiles(strInputFolder);
	}	

	// TODO: Add your control notification handler code here
	CDialogEx::OnOK();
}
