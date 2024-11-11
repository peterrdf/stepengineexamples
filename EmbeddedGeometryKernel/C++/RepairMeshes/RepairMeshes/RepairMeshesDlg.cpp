
// RepairMeshesDlg.cpp : implementation file
//

#include "stdafx.h"
#include "RepairMeshes.h"
#include "RepairMeshesDlg.h"
#include "afxdialogex.h"

#include "Step1.h"
#include "Step2.h"
#include "Step3.h"
#include "Step4.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CRepairMeshesDlg dialog


extern	wchar_t	* fileName_STEP1_output,
				* fileName_STEP3_output;

SIMPLE_TRIANGLE	* mySimpleTriangles = nullptr;


CRepairMeshesDlg::CRepairMeshesDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CRepairMeshesDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CRepairMeshesDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST2, mVertices);
	DDX_Control(pDX, IDC_LIST1, mPolygons);
}

BEGIN_MESSAGE_MAP(CRepairMeshesDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_STEP1, &CRepairMeshesDlg::OnBnClickedButtonStep1)
	ON_BN_CLICKED(IDC_BUTTON_STEP2, &CRepairMeshesDlg::OnBnClickedButtonStep2)
	ON_BN_CLICKED(IDC_BUTTON_STEP3, &CRepairMeshesDlg::OnBnClickedButtonStep3)
	ON_BN_CLICKED(IDC_BUTTON_STEP4, &CRepairMeshesDlg::OnBnClickedButtonStep4)
END_MESSAGE_MAP()


// CRepairMeshesDlg message handlers

BOOL CRepairMeshesDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CRepairMeshesDlg::OnPaint()
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
HCURSOR CRepairMeshesDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CRepairMeshesDlg::OnBnClickedButtonStep1()
{
	Step1(
			fileName_STEP1_output
		);
}


void CRepairMeshesDlg::OnBnClickedButtonStep2()
{
	mySimpleTriangles = Step2(
								fileName_STEP1_output
							);
}

void CRepairMeshesDlg::OnBnClickedButtonStep3()
{
	Step3(
			fileName_STEP3_output,
			mySimpleTriangles
		);
}


void CRepairMeshesDlg::OnBnClickedButtonStep4()
{
	Step4(
			fileName_STEP3_output,
			&mVertices,
			&mPolygons
		);
}
