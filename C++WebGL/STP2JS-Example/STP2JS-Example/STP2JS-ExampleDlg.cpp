
// STP2JS-ExampleDlg.cpp : implementation file
//

#include "stdafx.h"
#include "STP2JS-Example.h"
#include "STP2JS-ExampleDlg.h"
#include "afxdialogex.h"

#include "./stp2bin/include/stp2bin.h"
#include "./bin2js/include/bin2js.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


extern	wchar_t	* GL_fileName, * GL_binFileName, * GL_jsFileName, * GL_WebGL_URI;


// CSTP2JSExampleDlg dialog



CSTP2JSExampleDlg::CSTP2JSExampleDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CSTP2JSExampleDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSTP2JSExampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_BUTTON1, pPath);
	DDX_Control(pDX, IDC_EDIT1, pSTPFileName);
	DDX_Control(pDX, IDC_CHECK1, pQuality);
	DDX_Control(pDX, IDC_CHECK2, pPrepareBooleanOperations);
	DDX_Control(pDX, IDC_CHECK3, pPrepareBoundaryRepresentation);
	DDX_Control(pDX, IDC_BUTTON3, pConvert);
	DDX_Control(pDX, IDC_BUTTON2, pOpenHTML);
	DDX_Control(pDX, IDCANCEL, pCancel);
	DDX_Control(pDX, IDOK, pOK);
}

BEGIN_MESSAGE_MAP(CSTP2JSExampleDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON3, &CSTP2JSExampleDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON2, &CSTP2JSExampleDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON1, &CSTP2JSExampleDlg::OnBnClickedButton1)
END_MESSAGE_MAP()


// CSTP2JSExampleDlg message handlers

BOOL CSTP2JSExampleDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	pSTPFileName.SetWindowTextW(GL_fileName);
	pOpenHTML.EnableWindow(false);
	pOK.EnableWindow(false);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CSTP2JSExampleDlg::OnPaint()
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
HCURSOR CSTP2JSExampleDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CSTP2JSExampleDlg::OnBnClickedButton3()
{
	pConvert.EnableWindow(false);
	pPath.EnableWindow(false);
	pQuality.EnableWindow(false);
	pPrepareBooleanOperations.EnableWindow(false);
	pPrepareBoundaryRepresentation.EnableWindow(false);

	wchar_t	buff[512];
	pSTPFileName.GetWindowTextW(buff, 512);

	//
	//	Conversion from IFC to BIN
	//
	double	qualityValue = 0.5;
	if (pQuality.GetCheck()) {
		qualityValue = 100.0;
	}

	bool	prepareBooleanOperations;
	if (pPrepareBooleanOperations.GetCheck()) {
		prepareBooleanOperations = true;
	}
	else {
		prepareBooleanOperations = false;
	}

	bool	prepareBoundaryRepresentation;
	if (pPrepareBoundaryRepresentation.GetCheck()) {
		prepareBoundaryRepresentation = true;
	}
	else {
		prepareBoundaryRepresentation = false;
	}

	setQuality(
			qualityValue,
			prepareBooleanOperations,
			prepareBoundaryRepresentation
		);

	STP2BIN(buff, GL_binFileName);

	//
	//	Conversion from BIN to JS / JavaScript
	//
	generateJS(GL_binFileName, GL_jsFileName);

	pOpenHTML.EnableWindow(true);
	pOK.EnableWindow(true);
	pCancel.EnableWindow(false);
}


void CSTP2JSExampleDlg::OnBnClickedButton2()
{
	ShellExecuteW(nullptr, L"open", GL_WebGL_URI, nullptr, nullptr, SW_SHOWNORMAL);
}


void CSTP2JSExampleDlg::OnBnClickedButton1()
{
	wchar_t	fileName[512];
	pSTPFileName.GetWindowTextW(fileName, 511);

	CFileDialog dlgFile(true);
	OPENFILENAME& ofn = dlgFile.GetOFN();
	ofn.lpstrFile = fileName;
	ofn.nMaxFile = 512;

	if (dlgFile.DoModal() == IDOK) {
		CString	name = dlgFile.GetPathName();
		pSTPFileName.SetWindowTextW(name);
	}
}
