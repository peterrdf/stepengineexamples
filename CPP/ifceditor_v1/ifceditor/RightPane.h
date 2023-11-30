#pragma once


#include "ifcengine\include\ifcengine.h"


struct	STRUCT_INSTANCE
{
	SdaiEntity		ifcEntity;
	SdaiInstance	ifcInstance;

	HTREEITEM		hItem;

	SdaiAttr		attribute;
	const char		* attributeName;

	bool			expanded;
};

// CRightPane form view

class CRightPane : public CTreeView
{
	DECLARE_DYNCREATE(CRightPane)

protected:
	CRightPane();           // protected constructor used by dynamic creation
	virtual ~CRightPane();

public:
	enum { IDD = IDD_RIGHTPANE };

protected:
	virtual void	DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual void	OnInitialUpdate();
	virtual void	InsertItemInstanceEntity(SdaiEntity ifcEntity, HTREEITEM hParent);
	virtual void	InsertItemInstanceAttribute(SdaiInstance ifcInstance, SdaiAttr attribute, const char * attributeName, HTREEITEM hParent);
	virtual int_t	InsertItemAttributes(SdaiEntity ifcEntity, SdaiInstance ifcInstance, HTREEITEM hParent);
	virtual void	InsertItemReferences(SdaiInstance ifcInstance, SdaiAttr attribute, HTREEITEM hParent);
	virtual void	InsertItemInstance(SdaiInstance ifcInstance, HTREEITEM hParent);
	virtual void	SelectedEntity(SdaiEntity ifcEntity);

	virtual void	CreateAttributeLabelInstance(SdaiInstance iInstance, std::wstring& strLabel);
	virtual void	CreateAttributeLabelBoolean(bool bValue, std::wstring& strLabel);
	virtual void	CreateAttributeLabelLogical(char* szValue, std::wstring& strLabel);
	virtual void	CreateAttributeLabelEnumeration(char* szValue, std::wstring& strLabel);
	virtual void	CreateAttributeLabelReal(double dValue, std::wstring& strLabel);
	virtual void	CreateAttributeLabelInteger(int_t iValue, std::wstring& strLabel);
	virtual void	CreateAttributeLabelString(wchar_t* szValue, std::wstring& strLabel);
	virtual bool	CreateAttributeLabelADB(SdaiADB ADB, std::wstring& strLabel);
	virtual bool	CreateAttributeLabelAggregationElement(SdaiAggr aggregation, int_t aggrType, SdaiInteger iIndex, std::wstring& strLabel);
	virtual bool	CreateAttributeLabelAggregation(SdaiAggr sdaiAggregation, std::wstring& strLabel);
	virtual bool	CreateAttributeLabel(SdaiInstance ifcInstance, SdaiAttr attribute, std::wstring& strLabel);

	virtual void	CreateAttributeReferencesADB(SdaiADB ADB, HTREEITEM hParent);
	virtual void	CreateAttributeReferencesAggregationElement(SdaiAggr aggregation, int_t iAggrType, SdaiInteger iIndex, HTREEITEM hParent);
	virtual void	CreateAttributeReferencesAggregation(SdaiAggr aggregation, HTREEITEM hParent);
	virtual void	CreateAttributeReferences(SdaiInstance ifcInstance, SdaiAttr attribute, HTREEITEM hParent);

	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
	afx_msg void OnTvnItemexpanding(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnNMClick(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnNMRClick(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnDestroy();
};


