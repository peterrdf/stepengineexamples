
// TTF2BINDlg.h : header file
//

#pragma once

#include "Text2BIN.h"

// CTTF2BINDlg dialog
class CTTF2BINDlg : public CDialogEx
{
// Construction
public:
	CTTF2BINDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_TTF2BIN_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	void ExportASCII(char cStart, char cEnd);
	afx_msg void OnBnClickedButtonRun();
	CString m_strTTFFile;
	CString m_strBINFile;
	CString m_strText;
	afx_msg void OnBnClickedButtonTtfFile();
	afx_msg void OnBnClickedButtonBinFile();
	afx_msg void OnEnChangeEditText();
	int m_iGeometry;
	afx_msg void OnBnClickedRadioLinesAndCurves();
	afx_msg void OnBnClickedRadioFace2d();
	afx_msg void OnBnClickedRadioExtrsusionAreaSolid();
};
