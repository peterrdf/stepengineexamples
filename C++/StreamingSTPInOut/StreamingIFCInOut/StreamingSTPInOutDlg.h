
// StreamingIFCInOutDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CStreamingIFCInOutDlg dialog
class CStreamingIFCInOutDlg : public CDialogEx
{
// Construction
public:
	CStreamingIFCInOutDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_STREAMINGIFCINOUT_DIALOG };

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
	CEdit pImportPath;
	CEdit pExportPath;
	afx_msg void OnBnClickedButtonImportPath();
	afx_msg void OnBnClickedButtonExportPath();
	afx_msg void OnBnClickedButtonImport();
	afx_msg void OnBnClickedButtonExport();
	CButton pOK;
	CButton pCancel;
	CButton pStreamingImport;
	CButton pImport;
	CButton pStreamingExport;
	CButton pExport;
};
