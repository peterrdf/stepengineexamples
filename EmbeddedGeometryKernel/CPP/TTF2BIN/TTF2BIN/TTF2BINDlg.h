
// TTF2RDFDlg.h : header file
//

#pragma once

#include "Text2RDF.h"

// CTTF2RDFDlg dialog
class CTTF2RDFDlg : public CDialogEx
{
// Construction
public:
	CTTF2RDFDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_TTF2RDF_DIALOG };

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
	CString m_strRDFFile;
	CString m_strText;
	afx_msg void OnBnClickedButtonTtfFile();
	afx_msg void OnBnClickedButtonRdfFile();
	afx_msg void OnEnChangeEditText();
	int m_iGeometry;
	afx_msg void OnBnClickedRadioLinesAndCurves();
	afx_msg void OnBnClickedRadioFace2d();
	afx_msg void OnBnClickedRadioExtrsusionAreaSolid();
};
