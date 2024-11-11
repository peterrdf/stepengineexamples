
// RepairMeshesDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CRepairMeshesDlg dialog
class CRepairMeshesDlg : public CDialogEx
{
// Construction
public:
	CRepairMeshesDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_REPAIRMESHES_DIALOG };

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
	afx_msg void OnBnClickedButtonStep1();
	afx_msg void OnBnClickedButtonStep2();
	afx_msg void OnBnClickedButtonStep3();
	afx_msg void OnBnClickedButtonStep4();
	CListBox mVertices;
	CListBox mPolygons;
};
