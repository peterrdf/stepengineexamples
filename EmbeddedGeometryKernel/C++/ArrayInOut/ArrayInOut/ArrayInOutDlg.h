
// ArrayInOutDlg.h : header file
//

#pragma once


// CArrayInOutDlg dialog
class CArrayInOutDlg : public CDialogEx
{
// Construction
public:
	CArrayInOutDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_ARRAYINOUT_DIALOG };

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
	CButton pStreamingImport;
	CButton pStreamingExport;
	afx_msg void OnBnClickedButtonImportPath();
	afx_msg void OnBnClickedButtonExportPath();
	CEdit pImportPath;
	CEdit pExportPath;
	CButton pOK;
	afx_msg void OnBnClickedButtonExport();
	afx_msg void OnBnClickedButtonImport();
	CButton pExport;
	CButton pImport;
};