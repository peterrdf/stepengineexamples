
// VersionDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Version.h"
#include "VersionDlg.h"
#include "afxdialogex.h"

#include "stepengine/include/engine.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CVersionDlg dialog




CVersionDlg::CVersionDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CVersionDlg::IDD, pParent)
	, staticTimeStamp(0)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CVersionDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_I, m_static_I);
	DDX_Control(pDX, IDC_STATIC_II, m_static_II);
	DDX_Control(pDX, IDC_LIST_ENVIRONMENT, m_ListEnvironment);
}

BEGIN_MESSAGE_MAP(CVersionDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CVersionDlg message handlers

void	ConvertToWChar(wchar_t * output, size_t size, char * input)
{
	if (input) {
		size_t i = 0;
		while (input[i] && i < size - 1) {
			output[i] = (wchar_t) input[i];
			i++;
		}
		output[i] = 0;
	}
}

BOOL CVersionDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	char	* timeStamp = nullptr;
	__int64	revision = GetRevision(&timeStamp);

	wchar_t	buffer[512];
	ConvertToWChar(buffer, 512, timeStamp);

	m_static_I.SetWindowTextW(buffer);

	_itow_s((int) revision, buffer, 512, 10);

	m_static_II.SetWindowTextW(buffer);

	if (revision > 303) {
		char	* environmentVariables = nullptr,
				* developmentVariables = nullptr;
		__int64	environment = GetEnvironment(
									&environmentVariables,
									&developmentVariables
								);
		ASSERT(environment == revision);

		int		offset = 0;
		while (environmentVariables[offset]) {
			wchar_t	buffer[512];
			int		i = 0;

			while (environmentVariables[offset + i] != ':' && environmentVariables[offset + i] != ';' && environmentVariables[offset + i] != 0) {
				buffer[7 + i] = environmentVariables[offset + i];
				i++;
			}
			buffer[7 + i] = 0;

			if (environmentVariables[offset + i] == ':') {
				i++;
				if (environmentVariables[offset + i] == 'T') {
					buffer[0] = 'T';
					buffer[1] = 'r';
					buffer[2] = 'u';
					buffer[3] = 'e';
					buffer[4] = ' ';
					buffer[5] = ' ';
					buffer[6] = ' ';
				}
				else {
					ASSERT(environmentVariables[offset + i] == 'F');
					buffer[0] = 'F';
					buffer[1] = 'a';
					buffer[2] = 'l';
					buffer[3] = 's';
					buffer[4] = 'e';
					buffer[5] = ' ';
					buffer[6] = ' ';
				}
				m_ListEnvironment.AddString(buffer);
				i++;

				if (environmentVariables[offset + i] == ';') {
					i++;
				}
				else {
					ASSERT(environmentVariables[offset + i] == 0);
				}
			}
			else {
				ASSERT(false);
			}
			offset += i;
		}
	}
	else {
		m_ListEnvironment.AddString(L"n.a.");
	}

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CVersionDlg::OnPaint()
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
HCURSOR CVersionDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

