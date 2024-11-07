
// STP2JS-ExampleDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CSTP2JSExampleDlg dialog
class CSTP2JSExampleDlg : public CDialogEx
{
// Construction
public:
	CSTP2JSExampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_STP2JSEXAMPLE_DIALOG };

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
	CButton pPath;
	CEdit pSTPFileName;
	CButton pQuality;
	CButton pPrepareBooleanOperations;
	CButton pPrepareBoundaryRepresentation;
	CButton pConvert;
	CButton pOpenHTML;
	CButton pCancel;
	CButton pOK;
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton1();
};
