
#pragma once

#include "ViewBase.h"
#include "Controller.h"
#include "IFCAttribute.h"

#include <map>
#include <string>

// ************************************************************************************************
class CPropertiesToolBar : public CMFCToolBar
{
public:

	virtual void OnUpdateCmdUI(CFrameWnd* /*pTarget*/, BOOL bDisableIfNoHndler)
	{
		CMFCToolBar::OnUpdateCmdUI((CFrameWnd*) GetOwner(), bDisableIfNoHndler);
	}

	virtual BOOL AllowShowOnList() const { return FALSE; }
};

// ************************************************************************************************
class CApplicationPropertyData
{

private:  // Members

	enumApplicationProperty m_enApplicationProperty;

public: // Methods

	CApplicationPropertyData(enumApplicationProperty enApplicationProperty);

	enumApplicationProperty GetType() const;
};

// ************************************************************************************************
class CApplicationProperty : public CMFCPropertyGridProperty
{

public: // Methods
	CApplicationProperty(const CString& strName, const COleVariant& vtValue, LPCTSTR szDescription, DWORD_PTR dwData);
	CApplicationProperty(const CString& strGroupName, DWORD_PTR dwData, BOOL bIsValueList);
	virtual ~CApplicationProperty();
};

// ************************************************************************************************
class CIFCInstanceData
{

private:  // Members

	CController* m_pController;
	CIFCInstance* m_pInstance;

public: // Methods

	CIFCInstanceData(CController* pController, CIFCInstance* pInstance)
		: m_pController(pController)
		, m_pInstance(pInstance)
	{}
	virtual ~CIFCInstanceData() {}

	CController* GetController() const { return m_pController; }
	CIFCInstance* GetInstance() const { return m_pInstance; }
};

// ************************************************************************************************
class CIFCInstanceAttributeData : public CIFCInstanceData
{

private:  // Members

	CIFCAttribute* m_pAttribute;

public: // Methods

	CIFCInstanceAttributeData(CController* pController, CIFCInstance* pInstance, CIFCAttribute* pAttribute)
		: CIFCInstanceData(pController, pInstance)
		, m_pAttribute(pAttribute)
	{}
	virtual ~CIFCInstanceAttributeData() {}

	CIFCAttribute* GetAttribute() const { return m_pAttribute; }
};

// ************************************************************************************************
class CIFCInstanceAggrAttributeData : public CIFCInstanceAttributeData
{

private:  // Members

	SdaiInteger m_iIndex;

public: // Methods

	CIFCInstanceAggrAttributeData(CController* pController, CIFCInstance* pInstance, CIFCAttribute* pAttribute, SdaiInteger iIndex)
		: CIFCInstanceAttributeData(pController, pInstance, pAttribute)
		, m_iIndex(iIndex)
	{}
	virtual ~CIFCInstanceAggrAttributeData() {}

	SdaiInteger GetIndex() const { return m_iIndex; }
};

// ************************************************************************************************
class CIFCInstanceAttribute : public CMFCPropertyGridProperty
{

public: // Methods

	CIFCInstanceAttribute(const CString& strName, const COleVariant& vtValue, LPCTSTR szDescription, DWORD_PTR dwData);
	virtual ~CIFCInstanceAttribute();

	virtual CString FormatProperty();
	virtual BOOL TextToVar(const CString& strText);
	virtual CWnd* CreateInPlaceEdit(CRect rectEdit, BOOL& bDefaultFormat);
	void EnableSpinControlInt64();
};

// ************************************************************************************************
class CPropertiesWnd
	: public CDockablePane
	, public CViewBase
{

public: // Methods

	// --------------------------------------------------------------------------------------------
	// CViewBase
	virtual void OnModelChanged() override;
	virtual void OnShowMetaInformation() override;
	virtual void OnTargetInstanceChanged(CViewBase* pSender) override;
	virtual void OnInstanceSelected(CViewBase* pSender) override;
	virtual void OnViewRelations(CViewBase* pSender, int64_t iInstance) override;
	virtual void OnViewRelations(CViewBase* pSender, CEntity* pEntity) override;
	virtual void OnApplicationPropertyChanged(CViewBase* pSender, enumApplicationProperty enApplicationProperty) override;

protected: // Methods

	// --------------------------------------------------------------------------------------------
	// Support for properties
	afx_msg LRESULT OnPropertyChanged(__in WPARAM wparam, __in LPARAM lparam);

	void LoadApplicationProperties();
	void LoadInstanceProperties();
	void LoadIFCInstanceProperties();

	void LoadInstanceAttributes();
	void CreateADBGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateAGGRGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateBoolGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateEnumGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateIntGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateLogicalGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateRealGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateStringGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);
	void CreateUnicodeGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName);

	void UpdateADBAttribute(CInstanceBase* pInstance, CIFCAttribute* pAttribute, const CString& strName, const CString& strValue);
	void UpdateAGGRAttribute(CInstanceBase* pInstance, CIFCAttribute* pAttribute, const CString& strName, const CString& strValue, SdaiInteger iIndex);

	afx_msg void OnViewModeChanged();
	afx_msg void OnDestroy();

// Construction
public:
	CPropertiesWnd() noexcept;

	void AdjustLayout();

// Attributes
public:
	void SetVSDotNetLook(BOOL bSet)
	{
		m_wndPropList.SetVSDotNetLook(bSet);
		m_wndPropList.SetGroupNameFullWidth(bSet);
	}

protected:
	CFont m_fntPropList;
	CComboBox m_wndObjectCombo;
	CPropertiesToolBar m_wndToolBar;
	CMFCPropertyGridCtrl m_wndPropList;

// Implementation
public:
	virtual ~CPropertiesWnd();

protected:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnExpandAllProperties();
	afx_msg void OnUpdateExpandAllProperties(CCmdUI* pCmdUI);
	afx_msg void OnSortProperties();
	afx_msg void OnUpdateSortProperties(CCmdUI* pCmdUI);
	afx_msg void OnProperties1();
	afx_msg void OnUpdateProperties1(CCmdUI* pCmdUI);
	afx_msg void OnProperties2();
	afx_msg void OnUpdateProperties2(CCmdUI* pCmdUI);
	afx_msg void OnSetFocus(CWnd* pOldWnd);
	afx_msg void OnSettingChange(UINT uFlags, LPCTSTR lpszSection);	

	DECLARE_MESSAGE_MAP()

	void InitPropList();
	void SetPropListFont();

	int m_nComboHeight;
};

