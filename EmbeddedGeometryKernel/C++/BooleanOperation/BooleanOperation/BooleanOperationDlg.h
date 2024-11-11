
// BooleanOperationDlg.h : header file
//

#pragma once
#include "afxwin.h"



static int64_t flagbit0 = 1;           // 2^^0    0000.0000..0000.0001
static int64_t flagbit1 = 2;           // 2^^1    0000.0000..0000.0010
static int64_t flagbit2 = 4;           // 2^^2    0000.0000..0000.0100
static int64_t flagbit3 = 8;           // 2^^3    0000.0000..0000.1000

static int64_t flagbit4 = 16;          // 2^^4    0000.0000..0001.0000
static int64_t flagbit5 = 32;          // 2^^5    0000.0000..0010.0000
static int64_t flagbit6 = 64;          // 2^^6    0000.0000..0100.0000
static int64_t flagbit7 = 128;         // 2^^7    0000.0000..1000.0000

static int64_t flagbit8 = 256;         // 2^^8    0000.0001..0000.0000
static int64_t flagbit9 = 512;         // 2^^9    0000.0010..0000.0000
static int64_t flagbit10 = 1024;       // 2^^10   0000.0100..0000.0000
static int64_t flagbit11 = 2048;       // 2^^11   0000.1000..0000.0000

static int64_t flagbit12 = 4096;       // 2^^12   0001.0000..0000.0000
static int64_t flagbit13 = 8192;       // 2^^13   0010.0000..0000.0000
static int64_t flagbit14 = 16384;      // 2^^14   0100.0000..0000.0000
static int64_t flagbit15 = 32768;      // 2^^15   1000.0000..0000.0000



// CBooleanOperationDlg dialog
class CBooleanOperationDlg : public CDialogEx
{
// Construction
public:
	CBooleanOperationDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_BOOLEANOPERATION_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CButton m_BooleanOperation;
	CButton m_FirstObject;
	CButton m_SecondObject;
	afx_msg void OnBnClickedButtonFirstObject();
	afx_msg void OnBnClickedButtonSecondObject();
	afx_msg void OnBnClickedButtonBooleanOperation();
};
