
// TTF2RDFDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TTF2RDF.h"
#include "TTF2RDFDlg.h"
#include "afxdialogex.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CTTF2RDFDlg dialog



CTTF2RDFDlg::CTTF2RDFDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CTTF2RDFDlg::IDD, pParent)
	, m_strTTFFile(_T(""))
	, m_strRDFFile(_T(""))
	, m_strText(_T("RDF LTD."))
	, m_iGeometry(EXTRSUSION_AREA_SOLID_SET)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CTTF2RDFDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_TTF_FILE, m_strTTFFile);
	DDX_Text(pDX, IDC_EDIT_RDF_FILE, m_strRDFFile);
	DDX_Text(pDX, IDC_EDIT_TEXT, m_strText);
	DDX_Radio(pDX, IDC_RADIO_LINES_AND_CURVES, m_iGeometry);
}

BEGIN_MESSAGE_MAP(CTTF2RDFDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_RUN, &CTTF2RDFDlg::OnBnClickedButtonRun)
	ON_BN_CLICKED(IDC_BUTTON_TTF_FILE, &CTTF2RDFDlg::OnBnClickedButtonTtfFile)
	ON_BN_CLICKED(IDC_BUTTON_RDF_FILE, &CTTF2RDFDlg::OnBnClickedButtonRdfFile)
	ON_EN_CHANGE(IDC_EDIT_TEXT, &CTTF2RDFDlg::OnEnChangeEditText)
	ON_BN_CLICKED(IDC_RADIO_LINES_AND_CURVES, &CTTF2RDFDlg::OnBnClickedRadioLinesAndCurves)
	ON_BN_CLICKED(IDC_RADIO_FACE2D, &CTTF2RDFDlg::OnBnClickedRadioFace2d)
	ON_BN_CLICKED(IDC_RADIO_EXTRSUSION_AREA_SOLID, &CTTF2RDFDlg::OnBnClickedRadioExtrsusionAreaSolid)
END_MESSAGE_MAP()


// CTTF2RDFDlg message handlers

BOOL CTTF2RDFDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
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

void CTTF2RDFDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CTTF2RDFDlg::OnPaint()
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
HCURSOR CTTF2RDFDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnBnClickedButtonRun()
{
	UpdateData(TRUE);

	if (m_strTTFFile.IsEmpty())
	{
		::MessageBox(GetSafeHwnd(), _T("Please, select a TTF file."), _T("Error"), MB_ICONERROR | MB_OK);

		return;
	}

	if (m_strRDFFile.IsEmpty())
	{
		::MessageBox(GetSafeHwnd(), _T("Please, select an RDF file."), _T("Error"), MB_ICONERROR | MB_OK);

		return;
	}

	CString strText = m_strText;
	strText.Trim();

	if (strText.IsEmpty())
	{
		::MessageBox(GetSafeHwnd(), _T("Please, enter a text."), _T("Error"), MB_ICONERROR | MB_OK);

		return;
	}

	CText2RDF convertor(strText, m_strTTFFile, m_strRDFFile, m_iGeometry);
	convertor.Run();

	::MessageBox(GetSafeHwnd(), _T("Done."), _T("Information"), MB_ICONINFORMATION | MB_OK);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnBnClickedButtonTtfFile()
{
	CFileDialog dlgOpenFile(TRUE);
	OPENFILENAME & ofn = dlgOpenFile.GetOFN();
	ofn.lpstrFilter = _T("TTF Files (*.ttf)\0*.ttf\0All Files (*.*)\0*.*\0\0");
	ofn.lpstrTitle = _T("Please, select a file");

	if (dlgOpenFile.DoModal() != IDOK)
	{
		return;
	}

	m_strTTFFile = dlgOpenFile.GetPathName();

	UpdateData(FALSE);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnBnClickedButtonRdfFile()
{
	CFileDialog dlgOpenFile(FALSE);
	OPENFILENAME & ofn = dlgOpenFile.GetOFN();
	ofn.lpstrFilter = _T("RDF Files (*.rdf)\0*.rdf\0All Files (*.*)\0*.*\0\0");
	ofn.lpstrTitle = _T("Please, select a file");
	ofn.lpstrDefExt = _T(".rdf");

	if (dlgOpenFile.DoModal() != IDOK)
	{
		return;
	}

	m_strRDFFile = dlgOpenFile.GetPathName();

	UpdateData(FALSE);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnEnChangeEditText()
{
	UpdateData(TRUE);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnBnClickedRadioLinesAndCurves()
{
	UpdateData(TRUE);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnBnClickedRadioFace2d()
{
	UpdateData(TRUE);
}

// ------------------------------------------------------------------------------------------------
void CTTF2RDFDlg::OnBnClickedRadioExtrsusionAreaSolid()
{
	UpdateData(TRUE);
}
