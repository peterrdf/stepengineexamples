
// VersionDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CVersionDlg dialog
class CVersionDlg : public CDialogEx
{
// Construction
public:
	CVersionDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_VERSION_DIALOG };

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
	int staticTimeStamp;
	CStatic m_static_I;
	CStatic m_static_II;
	CListBox m_ListEnvironment;
};
