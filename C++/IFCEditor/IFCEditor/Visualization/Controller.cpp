#include "pch.h"
#include "Controller.h"
#include "Model.h"
#include "ViewBase.h"

// ------------------------------------------------------------------------------------------------
CController::CController()
	: m_pModel(nullptr)
	, m_bUpdatingModel(false)
	, m_setViews()
	, m_pTargetInstance(nullptr)
	, m_pSelectedInstance(nullptr)
	, m_bScaleAndCenter(FALSE)
{
}

// ------------------------------------------------------------------------------------------------
CController::~CController()
{
}

// ------------------------------------------------------------------------------------------------
CModel* CController::GetModel() const
{
	return m_pModel;
}

// ------------------------------------------------------------------------------------------------
void CController::SetModel(CModel* pModel)
{
	ASSERT(pModel != nullptr);

	m_pModel = pModel;

	m_pSelectedInstance = nullptr;
	m_pTargetInstance = nullptr;

	m_bUpdatingModel = true;

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnModelChanged();
	}

	m_bUpdatingModel = false;
}

CInstanceBase* CController::LoadInstance(OwlInstance iInstance)
{
	ASSERT(iInstance != 0);
	ASSERT(m_pModel != nullptr);

	m_pSelectedInstance = nullptr;

	if ((m_pTargetInstance != nullptr) && (m_pTargetInstance->GetInstance() == iInstance))
	{
		return nullptr;
	}

	m_pTargetInstance = nullptr;

	m_bUpdatingModel = true;

	auto pInstance = m_pModel->LoadInstance(iInstance);

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnModelUpdated();
	}

	m_bUpdatingModel = false;

	return pInstance;
}

// ------------------------------------------------------------------------------------------------
void CController::RegisterView(CViewBase* pView)
{
	ASSERT(pView != nullptr);
	ASSERT(m_setViews.find(pView) == m_setViews.end());

	m_setViews.insert(pView);
}

// ------------------------------------------------------------------------------------------------
void CController::UnRegisterView(CViewBase* pView)
{
	ASSERT(pView != nullptr);
	ASSERT(m_setViews.find(pView) != m_setViews.end());

	m_setViews.erase(pView);
}

// ------------------------------------------------------------------------------------------------
const set<CViewBase*> & CController::GetViews()
{
	return m_setViews;
}

// ------------------------------------------------------------------------------------------------
void CController::ZoomToInstance()
{
	ASSERT(m_pModel != nullptr);
	ASSERT(m_pSelectedInstance != nullptr);

	m_pModel->ZoomToInstance(m_pSelectedInstance);

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnWorldDimensionsChanged();
	}
}

// ------------------------------------------------------------------------------------------------
void CController::ZoomOut()
{
	ASSERT(m_pModel != nullptr);

	m_pModel->ZoomOut();

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnWorldDimensionsChanged();
	}
}

// ------------------------------------------------------------------------------------------------
void CController::SaveInstance()
{
	ASSERT(m_pModel != nullptr);
	ASSERT(m_pSelectedInstance != nullptr);

	TCHAR szFilters[] = _T("BIN Files (*.bin)|*.bin|All Files (*.*)|*.*||");
	CFileDialog dlgFile(FALSE, _T("bin"), m_pSelectedInstance->GetName().c_str(),
		OFN_OVERWRITEPROMPT | OFN_HIDEREADONLY, szFilters);

	if (dlgFile.DoModal() != IDOK)
	{
		return;
	}

	SaveInstanceTreeW(m_pSelectedInstance->GetInstance(), dlgFile.GetPathName());
}

void CController::SaveInstance(OwlInstance iInstance)
{
	ASSERT(iInstance != 0);

	wstring strName;
	wstring strUniqueName;
	CInstanceBase::BuildInstanceNames(m_pModel->GetSdaiModel(), iInstance, strName, strUniqueName);

	TCHAR szFilters[] = _T("BIN Files (*.bin)|*.bin|All Files (*.*)|*.*||");
	CFileDialog dlgFile(FALSE, _T("bin"), strUniqueName.c_str(),
		OFN_OVERWRITEPROMPT | OFN_HIDEREADONLY, szFilters);

	if (dlgFile.DoModal() != IDOK)
	{
		return;
	}

	SaveInstanceTreeW(iInstance, dlgFile.GetPathName());
}

// ------------------------------------------------------------------------------------------------
void CController::ScaleAndCenter()
{
	ASSERT(FALSE); // OBSOLETE
	/*m_pModel->ScaleAndCenter();

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnWorldDimensionsChanged();
	}*/
}

// ------------------------------------------------------------------------------------------------
void CController::ShowMetaInformation(CInstanceBase* /*pInstance*/)
{
	ASSERT(FALSE); // OBSOLETE
}

// ------------------------------------------------------------------------------------------------
void CController::SetTargetInstance(CViewBase* pSender, CInstanceBase* pInstance)
{
	if (m_bUpdatingModel)
	{
		return;
	}

	if (m_pTargetInstance == pInstance)
	{
		return;
	}

	m_pTargetInstance = pInstance;

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnTargetInstanceChanged(pSender);
	}
}

// ------------------------------------------------------------------------------------------------
CInstanceBase* CController::GetTargetInstance() const
{
	return m_pTargetInstance;
}

// ------------------------------------------------------------------------------------------------
void CController::SelectInstance(CViewBase* pSender, CInstanceBase* pInstance)
{
	if (m_bUpdatingModel)
	{
		return;
	}

	m_pSelectedInstance = pInstance;

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnInstanceSelected(pSender);
	}
}

// ------------------------------------------------------------------------------------------------
CInstanceBase* CController::GetSelectedInstance() const
{
	return m_pSelectedInstance;
}

// ------------------------------------------------------------------------------------------------
BOOL CController::GetScaleAndCenter() const
{
	return m_bScaleAndCenter;
}

// ------------------------------------------------------------------------------------------------
void CController::SetScaleAndCenter(BOOL bScaleAndCenter)
{
	m_bScaleAndCenter = bScaleAndCenter;
}

// ------------------------------------------------------------------------------------------------
void CController::OnInstancesEnabledStateChanged(CViewBase* pSender)
{
	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnInstancesEnabledStateChanged(pSender);
	}
}

// ------------------------------------------------------------------------------------------------
void CController::OnApplicationPropertyChanged(CViewBase* pSender, enumApplicationProperty enApplicationProperty)
{
	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnApplicationPropertyChanged(pSender, enApplicationProperty);
	}
}

// ------------------------------------------------------------------------------------------------
void CController::OnViewRelations(CViewBase* pSender, int64_t iInstance)
{
	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnViewRelations(pSender, iInstance);
	}
}

// ------------------------------------------------------------------------------------------------
void CController::OnViewRelations(CViewBase* pSender, CEntity* pEntity)
{
	m_pTargetInstance = nullptr;

	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnViewRelations(pSender, pEntity);
	}
}

// ------------------------------------------------------------------------------------------------
void CController::OnInstanceAttributeEdited(CViewBase* pSender, SdaiInstance iInstance, SdaiAttr pAttribute)
{
	auto itView = m_setViews.begin();
	for (; itView != m_setViews.end(); itView++)
	{
		(*itView)->OnInstanceAttributeEdited(pSender, iInstance, pAttribute);
	}
}