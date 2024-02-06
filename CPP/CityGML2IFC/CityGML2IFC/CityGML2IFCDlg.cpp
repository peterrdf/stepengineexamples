
// CityGML2IFCDlg.cpp : implementation file
//

#include "pch.h"
#include "framework.h"
#include "CityGML2IFC.h"
#include "CityGML2IFCDlg.h"
#include "afxdialogex.h"

#include "engine.h"
#include "ifcengine.h"
#include "gisengine.h"

#include <experimental/filesystem>
namespace fs = std::experimental::filesystem;

#include <string>
#include <bitset>
#include <algorithm>
#include <iostream>
#include <fstream>
#include <time.h>
using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// ************************************************************************************************
void STDCALL LogCallbackImpl(enumLogEvent enLogEvent, const char* szEvent)
{
	TRACE(L"\n%d: %S", (int)enLogEvent, szEvent);
}

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


// CCityGML2IFCDlg dialog



CCityGML2IFCDlg::CCityGML2IFCDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_CITYGML2IFC_DIALOG, pParent)
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

void CCityGML2IFCDlg::SetFormatSettings(int64_t iModel)
{
	string strSettings = "111111000000001111000001110001";

	bitset<64> bitSettings(strSettings);
	int64_t iSettings = bitSettings.to_ulong();

	string strMask = "11111111111111111111011101110111";
	bitset <64> bitMask(strMask);
	int64_t iMask = bitMask.to_ulong();

	SetFormat(iModel, (int64_t)iSettings, (int64_t)iMask);

	SetBehavior(iModel, 2048 + 4096, 2048 + 4096);
}

void CCityGML2IFCDlg::OnBnClickedOk()
{
	OwlModel iModel = CreateModel();
	ASSERT(iModel != 0);

	SetFormatSettings(iModel);

	wchar_t szAppPath[_MAX_PATH];
	::GetModuleFileName(::GetModuleHandle(nullptr), szAppPath, sizeof(szAppPath));

	fs::path pthExe = szAppPath;
	auto pthRootFolder = pthExe.parent_path();
	wstring strRootFolder = pthRootFolder.wstring();
	strRootFolder += L"\\";

	SetGISOptionsW(strRootFolder.c_str(), true, LogCallbackImpl);

	ImportGISModelW(iModel, L"D:\\Temp\\gisengine in\\FZKHouseLoD2.gml");

	CloseModel(iModel);

	// TODO: Add your control notification handler code here
	CDialogEx::OnOK();
}
