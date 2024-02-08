
// CityGML2IFCDlg.h : header file
//

#pragma once

#include "engine.h"
#include "ifcengine.h"
#include "gisengine.h"

#include <experimental/filesystem>
namespace fs = std::experimental::filesystem;

#include <string>
#include <bitset>
#include <algorithm>
#include <iostream>
#include <fstream>
#include <time.h>
#include <map>
using namespace std;

// CCityGML2IFCDlg dialog
class CCityGML2IFCDlg : public CDialogEx
{

private: // Members

	OwlModel m_iOwlModel;

protected: // Methods

	void SetFormatSettings(int64_t iModel);
	void CreateBuildingRecursive(OwlInstance iInstance);

// Construction
public:
	CCityGML2IFCDlg(CWnd* pParent = nullptr);	// standard constructor

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_CITYGML2IFC_DIALOG };
#endif

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
	afx_msg void OnBnClickedOk();
};
