
// StreamingInOutDlg.cpp : implementation file
//

#include "stdafx.h"
#include "StreamingInOut.h"
#include "StreamingInOutDlg.h"
#include "afxdialogex.h"

#include "In.h"
#include "Out.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


extern	wchar_t	* importFileName, * exportFileName;

int64_t	myModel = 0;


// CStreamingInOutDlg dialog



CStreamingInOutDlg::CStreamingInOutDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CStreamingInOutDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CStreamingInOutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_BUTTON_IMPORT_PATH, pStreamingImport);
	DDX_Control(pDX, IDC_BUTTON_EXPORT_PATH, pStreamingExport);
	DDX_Control(pDX, IDC_EDIT1, pImportPath);
	DDX_Control(pDX, IDC_EDIT2, pExportPath);
	DDX_Control(pDX, IDOK, pOK);
	DDX_Control(pDX, IDC_BUTTON_EXPORT, pExport);
	DDX_Control(pDX, IDC_BUTTON_IMPORT, pImport);
}

BEGIN_MESSAGE_MAP(CStreamingInOutDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_IMPORT_PATH, &CStreamingInOutDlg::OnBnClickedButtonImportPath)
	ON_BN_CLICKED(IDC_BUTTON_EXPORT_PATH, &CStreamingInOutDlg::OnBnClickedButtonExportPath)
	ON_BN_CLICKED(IDC_BUTTON_EXPORT, &CStreamingInOutDlg::OnBnClickedButtonExport)
	ON_BN_CLICKED(IDC_BUTTON_IMPORT, &CStreamingInOutDlg::OnBnClickedButtonImport)
END_MESSAGE_MAP()


// CStreamingInOutDlg message handlers

BOOL CStreamingInOutDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	pImportPath.SetWindowTextW(importFileName);
	pExportPath.SetWindowTextW(exportFileName);

	pExport.EnableWindow(false);
	pOK.EnableWindow(false);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CStreamingInOutDlg::OnPaint()
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
HCURSOR CStreamingInOutDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CStreamingInOutDlg::OnBnClickedButtonImportPath()
{
	wchar_t	fileName[512];
	pImportPath.GetWindowTextW(fileName, 511);

	CFileDialog dlgFile(true);
	OPENFILENAME& ofn = dlgFile.GetOFN();
	ofn.lpstrFile = fileName;
	ofn.nMaxFile = 512;

	if (dlgFile.DoModal() == IDOK) {
		CString	name = dlgFile.GetPathName();
		pImportPath.SetWindowTextW(name);
	}
}

void CStreamingInOutDlg::OnBnClickedButtonExportPath()
{
	wchar_t	fileName[512];
	pExportPath.GetWindowTextW(fileName, 511);

	CFileDialog dlgFile(true);
	OPENFILENAME& ofn = dlgFile.GetOFN();
	ofn.lpstrFile = fileName;
	ofn.nMaxFile = 512;

	if (dlgFile.DoModal() == IDOK) {
		CString	name = dlgFile.GetPathName();
		pExportPath.SetWindowTextW(name);
	}
}

void CStreamingInOutDlg::OnBnClickedButtonImport()
{
	pImport.EnableWindow(false);

	wchar_t	buff[512];
	pImportPath.GetWindowTextW(buff, 511);

	myModel = OpenModelByStream(buff);

	if (myModel) {
		pExport.EnableWindow(true);
		pImportPath.EnableWindow(false);
		pStreamingImport.EnableWindow(false);
	}
	else {
		pImport.EnableWindow(true);
	}
}

void CStreamingInOutDlg::OnBnClickedButtonExport()
{
	pExport.EnableWindow(false);

	if (myModel) {
		wchar_t	buff[512];
		pExportPath.GetWindowTextW(buff, 511);

		SaveModelByStream(myModel, buff);

		CloseModel(myModel);
		myModel = 0;

		pOK.EnableWindow(true);
		pExportPath.EnableWindow(false);
		pStreamingExport.EnableWindow(false);
	}
	else {
		pExport.EnableWindow(false);
	}
}

