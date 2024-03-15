
#include "pch.h"
#include "framework.h"

#include "PropertiesWnd.h"
#include "Resource.h"
#include "MainFrm.h"
#include "IFCEditor.h"
#include "IFCModel.h"
#include "OpenGLIFCView.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

/////////////////////////////////////////////////////////////////////////////
// CResourceViewBar

#define TRUE_VALUE_PROPERTY L"Yes"
#define FALSE_VALUE_PROPERTY L"No"
#define ROTATION_MODE_XY L"2D"
#define ROTATION_MODE_XYZ L"3D"

// ------------------------------------------------------------------------------------------------
CApplicationPropertyData::CApplicationPropertyData(enumApplicationProperty enApplicationProperty)
{
	m_enApplicationProperty = enApplicationProperty;
}

// ------------------------------------------------------------------------------------------------
enumApplicationProperty CApplicationPropertyData::GetType() const
{
	return m_enApplicationProperty;
}

// ------------------------------------------------------------------------------------------------
CApplicationProperty::CApplicationProperty(const CString& strName, const COleVariant& vtValue, LPCTSTR szDescription, DWORD_PTR dwData)
	: CMFCPropertyGridProperty(strName, vtValue, szDescription, dwData)
{
}

// ------------------------------------------------------------------------------------------------
CApplicationProperty::CApplicationProperty(const CString& strGroupName, DWORD_PTR dwData, BOOL bIsValueList)
	: CMFCPropertyGridProperty(strGroupName, dwData, bIsValueList)
{
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ CApplicationProperty::~CApplicationProperty()
{
	delete (CApplicationPropertyData*)GetData();
}

// ------------------------------------------------------------------------------------------------
CIFCInstanceAttribute::CIFCInstanceAttribute(const CString& strName, const COleVariant& vtValue, LPCTSTR szDescription, DWORD_PTR dwData)
	: CMFCPropertyGridProperty(strName, vtValue, szDescription, dwData)
{
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ CIFCInstanceAttribute::~CIFCInstanceAttribute()
{
	delete (CIFCInstanceAttributeData*)GetData();
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ CString CIFCInstanceAttribute::FormatProperty()
{
	if (m_varValue.vt != VT_I8)
	{
		return __super::FormatProperty();
	}

	CString strValue;
	strValue.Format(L"%lld", m_varValue.llVal);

	return strValue;
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ BOOL CIFCInstanceAttribute::TextToVar(const CString& strText)
{
	/*
	* Support for int64_t
	*/
	if (m_varValue.vt != VT_I8)
	{
		return __super::TextToVar(strText);
	}

	m_varValue.llVal = _ttoi64(strText);

	return TRUE;
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ CWnd* CIFCInstanceAttribute::CreateInPlaceEdit(CRect rectEdit, BOOL& bDefaultFormat)
{
	/*
	* Support for int64_t
	*/
	bool bInt64 = false;
	if (m_varValue.vt == VT_I8)
	{
		// Cheat the base class
		m_varValue.vt = VT_I4;
		bInt64 = true;
	}

	CWnd* pWnd = __super::CreateInPlaceEdit(rectEdit, bDefaultFormat);

	if (bInt64)
	{
		// Restore type
		m_varValue.vt = VT_I8;
	}

	return pWnd;
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CIFCInstanceAttribute::EnableSpinControlInt64()
{
	ASSERT(m_varValue.vt == VT_I8);

	// Cheat the base class
	m_varValue.vt = VT_I4;

	EnableSpinControl(TRUE, 0, INT_MAX);

	m_varValue.vt = VT_I8;
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnModelChanged() /*override*/
{
	// Application and Properties are disabled!!!
	
	//m_wndObjectCombo.SetCurSel(0 /*Application*/);

	//LoadApplicationProperties();
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnShowMetaInformation() /*override*/
{
	// Application and Properties are disabled!!!
	
	//m_wndObjectCombo.SetCurSel(1 /*Properties*/);

	//ASSERT(FALSE); // TODO
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnTargetInstanceChanged(CViewBase* pSender) /*override*/
{
	m_wndObjectCombo.SetCurSel(0 /*Attributes*/); // Application and Properties are disabled!!!

	LoadInstanceAttributes();
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnInstanceSelected(CViewBase* /*pSender*/) /*override*/
{
	// Application and Properties are disabled!!!
	
	//m_wndObjectCombo.SetCurSel(1 /*Properties*/);

	//LoadInstanceProperties();
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnViewRelations(CViewBase* pSender, int64_t iInstance) /*override*/
{
	m_wndObjectCombo.SetCurSel(0 /*Attributes*/); // Application and Properties are disabled!!!

	LoadInstanceAttributes();
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnViewRelations(CViewBase* pSender, CEntity* pEntity) /*override*/
{
	m_wndObjectCombo.SetCurSel(0 /*Attributes*/); // Application and Properties are disabled!!!

	LoadInstanceAttributes();
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CPropertiesWnd::OnApplicationPropertyChanged(CViewBase* pSender, enumApplicationProperty /*enApplicationProperty*/) /*override*/
{
	if (pSender == this)
	{
		return;
	}

	if (m_wndObjectCombo.GetCurSel() == 0)
	{
		// Application and Properties are disabled!!!
		//LoadApplicationProperties();
	}
}

// ------------------------------------------------------------------------------------------------
/*afx_msg*/ LRESULT CPropertiesWnd::OnPropertyChanged(__in WPARAM /*wparam*/, __in LPARAM lparam)
{
	auto pController = GetController();
	if (pController == nullptr)
	{
		ASSERT(FALSE);

		return 0;
	}

	// Application and Properties are disabled!!!
#pragma region Application
	//if (m_wndObjectCombo.GetCurSel() == 0)
	//{
	//	auto pOpenGLView = GetController()->GetView<COpenGLView>();
	//	ASSERT(pOpenGLView != nullptr);

	//	auto ioglRender = dynamic_cast<_ioglRenderer*>(pOpenGLView);
	//	ASSERT(ioglRender != nullptr);

	//	auto pBlinnPhongProgram = ioglRender->_getOGLProgramAs<_oglBlinnPhongProgram>();
	//	ASSERT(pBlinnPhongProgram != nullptr);

	//	auto pApplicationProperty = dynamic_cast<CApplicationProperty*>((CMFCPropertyGridProperty*)lparam);
	//	if (pApplicationProperty != nullptr)
	//	{
	//		CString strValue = pApplicationProperty->GetValue();

	//		auto pData = (CApplicationPropertyData*)pApplicationProperty->GetData();
	//		ASSERT(pData != nullptr);

	//		switch (pData->GetType())
	//		{
	//		case enumApplicationProperty::ShowFaces:
	//		{
	//			pOpenGLView->ShowFaces(strValue == TRUE_VALUE_PROPERTY ? TRUE : FALSE);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::ShowFaces);
	//		}
	//		break;

	//		case enumApplicationProperty::ShowConceptualFacesWireframes:
	//		{
	//			pOpenGLView->ShowConceptualFacesPolygons(strValue == TRUE_VALUE_PROPERTY ? TRUE : FALSE);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::ShowConceptualFacesWireframes);
	//		}
	//		break;

	//		case enumApplicationProperty::ShowLines:
	//		{
	//			pOpenGLView->ShowLines(strValue == TRUE_VALUE_PROPERTY ? TRUE : FALSE);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::ShowLines);
	//		}
	//		break;

	//		case enumApplicationProperty::ShowPoints:
	//		{
	//			pOpenGLView->ShowPoints(strValue == TRUE_VALUE_PROPERTY ? TRUE : FALSE);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::ShowPoints);
	//		}
	//		break;

	//		case enumApplicationProperty::RotationMode:
	//		{
	//			pOpenGLView->SetRotationMode(strValue == ROTATION_MODE_XY ? enumRotationMode::XY : enumRotationMode::XYZ);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::RotationMode);
	//		}
	//		break;

	//		case enumApplicationProperty::PointLightingLocation:
	//		{
	//			auto pProperty = pApplicationProperty->GetParent();
	//			ASSERT(pProperty != nullptr);
	//			ASSERT(dynamic_cast<CApplicationProperty*>(pProperty) != nullptr);
	//			ASSERT(((CApplicationPropertyData*)dynamic_cast<CApplicationProperty*>(pProperty)->
	//				GetData())->GetType() == enumApplicationProperty::PointLightingLocation);
	//			ASSERT(pProperty->GetSubItemsCount() == 3);

	//			auto pX = pProperty->GetSubItem(0);
	//			auto pY = pProperty->GetSubItem(1);
	//			auto pZ = pProperty->GetSubItem(2);

	//			pBlinnPhongProgram->_setPointLightingLocation(glm::vec3(
	//				(float)_wtof((LPCTSTR)(CString)pX->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pY->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pZ->GetValue()))
	//			);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::PointLightingLocation);
	//		}
	//		break;

	//		case enumApplicationProperty::AmbientLightWeighting:
	//		{
	//			auto pProperty = pApplicationProperty->GetParent();
	//			ASSERT(pProperty != nullptr);
	//			ASSERT(dynamic_cast<CApplicationProperty*>(pProperty) != nullptr);
	//			ASSERT(((CApplicationPropertyData*)dynamic_cast<CApplicationProperty*>(pProperty)->
	//				GetData())->GetType() == enumApplicationProperty::AmbientLightWeighting);
	//			ASSERT(pProperty->GetSubItemsCount() == 3);

	//			auto pX = pProperty->GetSubItem(0);
	//			auto pY = pProperty->GetSubItem(1);
	//			auto pZ = pProperty->GetSubItem(2);

	//			pBlinnPhongProgram->_setAmbientLightWeighting(
	//				(float)_wtof((LPCTSTR)(CString)pX->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pY->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pZ->GetValue())
	//			);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::AmbientLightWeighting);
	//		}
	//		break;

	//		case enumApplicationProperty::DiffuseLightWeighting:
	//		{
	//			auto pProperty = pApplicationProperty->GetParent();
	//			ASSERT(pProperty != nullptr);
	//			ASSERT(dynamic_cast<CApplicationProperty*>(pProperty) != nullptr);
	//			ASSERT(((CApplicationPropertyData*)dynamic_cast<CApplicationProperty*>(pProperty)->
	//				GetData())->GetType() == enumApplicationProperty::DiffuseLightWeighting);
	//			ASSERT(pProperty->GetSubItemsCount() == 3);

	//			auto pX = pProperty->GetSubItem(0);
	//			auto pY = pProperty->GetSubItem(1);
	//			auto pZ = pProperty->GetSubItem(2);

	//			pBlinnPhongProgram->_setDiffuseLightWeighting(
	//				(float)_wtof((LPCTSTR)(CString)pX->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pY->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pZ->GetValue())
	//			);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::DiffuseLightWeighting);
	//		}
	//		break;

	//		case enumApplicationProperty::SpecularLightWeighting:
	//		{
	//			auto pProperty = pApplicationProperty->GetParent();
	//			ASSERT(pProperty != nullptr);
	//			ASSERT(dynamic_cast<CApplicationProperty*>(pProperty) != nullptr);
	//			ASSERT(((CApplicationPropertyData*)dynamic_cast<CApplicationProperty*>(pProperty)->
	//				GetData())->GetType() == enumApplicationProperty::SpecularLightWeighting);
	//			ASSERT(pProperty->GetSubItemsCount() == 3);

	//			auto pX = pProperty->GetSubItem(0);
	//			auto pY = pProperty->GetSubItem(1);
	//			auto pZ = pProperty->GetSubItem(2);

	//			pBlinnPhongProgram->_setSpecularLightWeighting(
	//				(float)_wtof((LPCTSTR)(CString)pX->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pY->GetValue()),
	//				(float)_wtof((LPCTSTR)(CString)pZ->GetValue())
	//			);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::SpecularLightWeighting);
	//		}
	//		break;

	//		case enumApplicationProperty::MaterialShininess:
	//		{
	//			float fValue = (float)_wtof((LPCTSTR)strValue);

	//			pBlinnPhongProgram->_setMaterialShininess(fValue);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::MaterialShininess);
	//		}
	//		break;

	//		case enumApplicationProperty::Contrast:
	//		{
	//			float fValue = (float)_wtof((LPCTSTR)strValue);

	//			pBlinnPhongProgram->_setContrast(fValue);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::Contrast);
	//		}
	//		break;

	//		case enumApplicationProperty::Brightness:
	//		{
	//			float fValue = (float)_wtof((LPCTSTR)strValue);

	//			pBlinnPhongProgram->_setBrightness(fValue);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::Brightness);
	//		}
	//		break;

	//		case enumApplicationProperty::Gamma:
	//		{
	//			float fValue = (float)_wtof((LPCTSTR)strValue);

	//			pBlinnPhongProgram->_setGamma(fValue);

	//			GetController()->OnApplicationPropertyChanged(this, enumApplicationProperty::Gamma);
	//		}
	//		break;

	//		default:
	//			ASSERT(FALSE);
	//			break;
	//		} // switch (pData->GetType())

	//		return 0;
	//	} // if (pApplicationProperty != nullptr)
	//} // if (m_wndObjectCombo.GetCurSel() == 0)
#pragma endregion

	// Application and Properties are disabled!!!
#pragma region Properties
#pragma endregion // Properties

	// Application and Properties are disabled!!!
#pragma region Attributes
	if (m_wndObjectCombo.GetCurSel() == 0)
	{
		auto pAttribute = dynamic_cast<CIFCInstanceAttribute*>((CMFCPropertyGridProperty*)lparam);
		if (pAttribute == nullptr)
		{
			return 0;
		}

		CString strName = pAttribute->GetParent()->GetName();

		CString strValue = pAttribute->GetValue();
		strValue.Trim();

		auto pData = (CIFCInstanceAttributeData*)pAttribute->GetData();
		if (pData == nullptr)
		{
			ASSERT(FALSE);

			return 0;
		}

		switch (pData->GetAttribute()->GetType()) 
		{
			case sdaiADB:
			{
				UpdateADBAttribute(pData->GetInstance(), pData->GetAttribute(), strName, strValue);

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			case sdaiENUM:
			{
				sdaiPutAttrBN(
					pData->GetInstance()->GetInstance(),
					CW2A((LPCTSTR)strName),
					pData->GetAttribute()->GetType(),
					CW2A(strValue));

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			case sdaiINTEGER:
			{
				int_t iValue = 0;
				if (!strValue.IsEmpty())
				{
					iValue = (int_t)atoll(CW2A((LPCTSTR)strValue));
				}

				sdaiPutAttrBN(
					pData->GetInstance()->GetInstance(), 
					CW2A((LPCTSTR)strName), 
					pData->GetAttribute()->GetType(), 
					&iValue);

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			case sdaiLOGICAL:
			{
				sdaiPutAttrBN(
					pData->GetInstance()->GetInstance(),
					CW2A((LPCTSTR)strName),
					pData->GetAttribute()->GetType(),
					CW2A(strValue));

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			case sdaiREAL:
			case sdaiNUMBER:
			{
				double dValue = 0.;
				if (!strValue.IsEmpty())
				{
					dValue = atof(CW2A((LPCTSTR)strValue));
				}

				sdaiPutAttrBN(
					pData->GetInstance()->GetInstance(), 
					CW2A((LPCTSTR)strName), 
					pData->GetAttribute()->GetType(), 
					&dValue);

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			case sdaiSTRING:
			{
				sdaiPutAttrBN(
					pData->GetInstance()->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiUNICODE,
					(LPCTSTR)strValue);

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			case sdaiUNICODE:
			{
				sdaiPutAttrBN(
					pData->GetInstance()->GetInstance(),
					CW2A((LPCTSTR)strName),
					pData->GetAttribute()->GetType(),
					(LPCTSTR)strValue);

				pController->OnInstanceAttributeEdited(this, pData->GetInstance()->GetInstance(), pData->GetAttribute()->GetInstance());
			}
			break;

			default:
			{
				ASSERT(FALSE); // TODO
			}
			break;
		} // switch (pData->GetAttribute()->GetType()) 
	}
#pragma endregion // Attributes

	return 0;
}

// ------------------------------------------------------------------------------------------------
void CPropertiesWnd::LoadApplicationProperties()
{
	m_wndPropList.RemoveAll();
	m_wndPropList.AdjustLayout();

	SetPropListFont();

	m_wndPropList.EnableHeaderCtrl(FALSE);
	m_wndPropList.EnableDescriptionArea();
	m_wndPropList.SetVSDotNetLook();
	m_wndPropList.MarkModifiedProperties();

	auto pController = GetController();
	if (pController == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pOpenGLView = GetController()->GetView<COpenGLView>();
	if (pOpenGLView == nullptr)
	{
		return;
	}

#pragma region View
	auto pViewGroup = new CMFCPropertyGridProperty(_T("View"));

	{
		auto pProperty = new CApplicationProperty(_T("Faces"),
			pOpenGLView->AreFacesShown() ? TRUE_VALUE_PROPERTY : FALSE_VALUE_PROPERTY, _T("Faces"),
			(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::ShowFaces));
		pProperty->AddOption(TRUE_VALUE_PROPERTY);
		pProperty->AddOption(FALSE_VALUE_PROPERTY);
		pProperty->AllowEdit(FALSE);

		pViewGroup->AddSubItem(pProperty);
	}

	{
		auto pProperty = new CApplicationProperty(_T("Conceptual faces wireframes"),
			pOpenGLView->AreConceptualFacesPolygonsShown() ? TRUE_VALUE_PROPERTY : FALSE_VALUE_PROPERTY,
			_T("Conceptual faces wireframes"),
			(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::ShowConceptualFacesWireframes));
		pProperty->AddOption(TRUE_VALUE_PROPERTY);
		pProperty->AddOption(FALSE_VALUE_PROPERTY);
		pProperty->AllowEdit(FALSE);

		pViewGroup->AddSubItem(pProperty);
	}

	{
		auto pProperty = new CApplicationProperty(_T("Lines"), pOpenGLView->AreLinesShown() ? TRUE_VALUE_PROPERTY : FALSE_VALUE_PROPERTY,
			_T("Lines"),
			(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::ShowLines));
		pProperty->AddOption(TRUE_VALUE_PROPERTY);
		pProperty->AddOption(FALSE_VALUE_PROPERTY);
		pProperty->AllowEdit(FALSE);

		pViewGroup->AddSubItem(pProperty);
	}

	{
		auto pProperty = new CApplicationProperty(_T("Points"), pOpenGLView->ArePointsShown() ? TRUE_VALUE_PROPERTY : FALSE_VALUE_PROPERTY,
			_T("Points"),
			(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::ShowPoints));
		pProperty->AddOption(TRUE_VALUE_PROPERTY);
		pProperty->AddOption(FALSE_VALUE_PROPERTY);
		pProperty->AllowEdit(FALSE);

		pViewGroup->AddSubItem(pProperty);
	}

	{
		auto pProperty = new CApplicationProperty(
			_T("Rotation mode"),
			pOpenGLView->GetRotationMode() == enumRotationMode::XY ? ROTATION_MODE_XY : ROTATION_MODE_XYZ,
			_T("XY/XYZ"),
			(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::RotationMode));
		pProperty->AddOption(ROTATION_MODE_XY);
		pProperty->AddOption(ROTATION_MODE_XYZ);
		pProperty->AllowEdit(FALSE);

		pViewGroup->AddSubItem(pProperty);
	}
#pragma endregion

#pragma region OpenGL
	auto ioglRender = dynamic_cast<_ioglRenderer*>(pOpenGLView);
	ASSERT(ioglRender != nullptr);

	auto pBlinnPhongProgram = ioglRender->_getOGLProgramAs<_oglBlinnPhongProgram>();
	if (pBlinnPhongProgram != nullptr)
	{
		auto pOpenGL = new CMFCPropertyGridProperty(_T("OpenGL"));
		pViewGroup->AddSubItem(pOpenGL);

#pragma region Point light position
		{
			auto pPointLightingLocation = new CApplicationProperty(_T("Point lighting location"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::PointLightingLocation), TRUE);
			pPointLightingLocation->AllowEdit(FALSE);

			// X
			{
				auto pProperty = new CApplicationProperty(
					_T("X"),
					(_variant_t)pBlinnPhongProgram->_getPointLightingLocation().x,
					_T("X"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::PointLightingLocation));
				pPointLightingLocation->AddSubItem(pProperty);
			}

			// Y
			{
				auto pProperty = new CApplicationProperty(
					_T("Y"),
					(_variant_t)pBlinnPhongProgram->_getPointLightingLocation().y,
					_T("Y"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::PointLightingLocation));
				pPointLightingLocation->AddSubItem(pProperty);
			}

			// Z
			{
				auto pProperty = new CApplicationProperty(
					_T("Z"),
					(_variant_t)pBlinnPhongProgram->_getPointLightingLocation().z,
					_T("Z"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::PointLightingLocation));
				pPointLightingLocation->AddSubItem(pProperty);
			}

			pOpenGL->AddSubItem(pPointLightingLocation);
		}
#pragma endregion

#pragma region Ambient light weighting
		{
			auto pAmbientLightWeighting = new CApplicationProperty(_T("Ambient light weighting"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::AmbientLightWeighting), TRUE);
			pAmbientLightWeighting->AllowEdit(FALSE);

			// X
			{
				auto pProperty = new CApplicationProperty(
					_T("X"),
					(_variant_t)pBlinnPhongProgram->_getAmbientLightWeighting().x,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::AmbientLightWeighting));
				pAmbientLightWeighting->AddSubItem(pProperty);
			}

			// Y
			{
				auto pProperty = new CApplicationProperty(
					_T("Y"),
					(_variant_t)pBlinnPhongProgram->_getAmbientLightWeighting().y,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::AmbientLightWeighting));
				pAmbientLightWeighting->AddSubItem(pProperty);
			}

			// Z
			{
				auto pProperty = new CApplicationProperty(
					_T("Z"),
					(_variant_t)pBlinnPhongProgram->_getAmbientLightWeighting().z,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::AmbientLightWeighting));
				pAmbientLightWeighting->AddSubItem(pProperty);
			}

			pOpenGL->AddSubItem(pAmbientLightWeighting);
		}
#pragma endregion

#pragma region Diffuse light weighting
		{
			auto pDiffuseLightWeighting = new CApplicationProperty(_T("Diffuse light weighting"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::DiffuseLightWeighting), TRUE);
			pDiffuseLightWeighting->AllowEdit(FALSE);

			// X
			{
				auto pProperty = new CApplicationProperty(
					_T("X"),
					(_variant_t)pBlinnPhongProgram->_getDiffuseLightWeighting().x,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::DiffuseLightWeighting));
				pDiffuseLightWeighting->AddSubItem(pProperty);
			}

			// X
			{
				auto pProperty = new CApplicationProperty(
					_T("Y"),
					(_variant_t)pBlinnPhongProgram->_getDiffuseLightWeighting().y,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::DiffuseLightWeighting));
				pDiffuseLightWeighting->AddSubItem(pProperty);
			}

			// Z
			{
				auto pProperty = new CApplicationProperty(
					_T("Z"),
					(_variant_t)pBlinnPhongProgram->_getDiffuseLightWeighting().z,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::DiffuseLightWeighting));
				pDiffuseLightWeighting->AddSubItem(pProperty);
			}

			pOpenGL->AddSubItem(pDiffuseLightWeighting);
		}
#pragma endregion

#pragma region Specular light weighting
		{
			auto pSpecularLightWeighting = new CApplicationProperty(_T("Specular light weighting"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::SpecularLightWeighting), TRUE);
			pSpecularLightWeighting->AllowEdit(FALSE);

			// X
			{
				auto pProperty = new CApplicationProperty(
					_T("X"),
					(_variant_t)pBlinnPhongProgram->_getSpecularLightWeighting().x,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::SpecularLightWeighting));
				pSpecularLightWeighting->AddSubItem(pProperty);
			}

			// X
			{
				auto pProperty = new CApplicationProperty(
					_T("Y"),
					(_variant_t)pBlinnPhongProgram->_getSpecularLightWeighting().y,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::SpecularLightWeighting));
				pSpecularLightWeighting->AddSubItem(pProperty);
			}

			// Z
			{
				auto pProperty = new CApplicationProperty(
					_T("Z"),
					(_variant_t)pBlinnPhongProgram->_getSpecularLightWeighting().z,
					_T("[0.0 - 1.0]"),
					(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::SpecularLightWeighting));
				pSpecularLightWeighting->AddSubItem(pProperty);
			}

			pOpenGL->AddSubItem(pSpecularLightWeighting);
		}
#pragma endregion

#pragma region Material shininess
		{
			auto pMaterialShininess = new CApplicationProperty(
				_T("Material shininess"),
				(_variant_t)pBlinnPhongProgram->_getMaterialShininess(),
				_T("[0.0 - 1.0]"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::MaterialShininess));

			pOpenGL->AddSubItem(pMaterialShininess);
		}
#pragma endregion

#pragma region Contrast
		{
			auto pContrast = new CApplicationProperty(
				_T("Contrast"),
				(_variant_t)pBlinnPhongProgram->_getContrast(),
				_T("Contrast"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::Contrast));

			pOpenGL->AddSubItem(pContrast);
		}
#pragma endregion

#pragma region Brightness
		{
			auto pBrightness = new CApplicationProperty(
				_T("Brightness"),
				(_variant_t)pBlinnPhongProgram->_getBrightness(),
				_T("Brightness"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::Brightness));

			pOpenGL->AddSubItem(pBrightness);
		}
#pragma endregion

#pragma region Gamma
		{
			auto pGamma = new CApplicationProperty(
				_T("Gamma"),
				(_variant_t)pBlinnPhongProgram->_getGamma(),
				_T("Gamma"),
				(DWORD_PTR)new CApplicationPropertyData(enumApplicationProperty::Gamma));

			pOpenGL->AddSubItem(pGamma);
		}
#pragma endregion
	} // if (pBlinnPhongProgram != nullptr)
#pragma endregion

	m_wndPropList.AddProperty(pViewGroup);
}

// ------------------------------------------------------------------------------------------------
void CPropertiesWnd::LoadInstanceProperties()
{
	m_wndPropList.RemoveAll();
	m_wndPropList.AdjustLayout();

	SetPropListFont();

	m_wndPropList.EnableHeaderCtrl(FALSE);
	m_wndPropList.EnableDescriptionArea();
	m_wndPropList.SetVSDotNetLook();
	m_wndPropList.MarkModifiedProperties();

	auto pContoller = GetController();
	if (pContoller == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	if (GetController()->GetSelectedInstance() == nullptr)
	{
		return;
	}

	auto pModel = pContoller->GetModel();
	if (pModel == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	switch (pModel->GetType())
	{
		case enumModelType::IFC:
		{
			LoadIFCInstanceProperties();
		}
		break;

		default:
		{
			ASSERT(FALSE); // Unknown
		}
		break;
	} // switch (pModel ->GetType())
}

// ------------------------------------------------------------------------------------------------
void CPropertiesWnd::LoadIFCInstanceProperties()
{
	auto pContoller = GetController();
	if (pContoller == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pModel = pContoller->GetModel();
	if (pModel == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pIFCModel = dynamic_cast<CIFCModel*>(pModel);
	if (pIFCModel == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pPropertyProvider = pIFCModel->GetPropertyProvider();

	auto pInstance = dynamic_cast<CIFCInstance*>(GetController()->GetSelectedInstance());
	if (pInstance == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pPropertySetCollection = pPropertyProvider->GetPropertySetCollection(pInstance->GetInstance());
	if (pPropertySetCollection == nullptr)
	{
		return;
	}

	auto pInstanceGridGroup = new CMFCPropertyGridProperty(pInstance->GetName().c_str());

	for (auto pPropertySet : pPropertySetCollection->PropertySets())
	{
		auto pPropertySetGroup = new CMFCPropertyGridProperty(pPropertySet->GetName().c_str());

		pInstanceGridGroup->AddSubItem(pPropertySetGroup);

		for (auto pProperty : pPropertySet->Properties())
		{
			auto pGridProperty = new CMFCPropertyGridProperty(
				pProperty->GetName().c_str(),
				(_variant_t)pProperty->GetValue().c_str(),
				L""); // Description
			pGridProperty->AllowEdit(FALSE);

			pPropertySetGroup->AddSubItem(pGridProperty);
		}
	}

	m_wndPropList.AddProperty(pInstanceGridGroup);

}

void CPropertiesWnd::LoadInstanceAttributes()
{
	m_wndPropList.RemoveAll();
	m_wndPropList.AdjustLayout();

	SetPropListFont();

	m_wndPropList.EnableHeaderCtrl(FALSE);
	m_wndPropList.EnableDescriptionArea();
	m_wndPropList.SetVSDotNetLook();
	m_wndPropList.MarkModifiedProperties();

	auto pContoller = GetController();
	if (pContoller == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pTargetInstance = pContoller->GetTargetInstance();
	if (pTargetInstance == nullptr)
	{
		return;
	}

	auto pModel = pContoller->GetModel();
	if (pModel == nullptr)
	{
		return;
	}	

	auto pIFCModel = dynamic_cast<CIFCModel*>(pModel);
	if (pIFCModel == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pInstanceGroup = new CMFCPropertyGridProperty(pTargetInstance->GetName().c_str());

	SdaiEntity iEntity = sdaiGetInstanceType(pTargetInstance->GetInstance());
	ASSERT(iEntity != 0);

	auto pAttributeProvider = pIFCModel->GetAttributeProvider();
	auto& vecAttributes = pAttributeProvider->GetInstanceAttributes(pTargetInstance->GetInstance());	
	for (SdaiInteger iIndex = 0; iIndex < (SdaiInteger)vecAttributes.size(); iIndex++)
	{
		auto pAttribute = vecAttributes[iIndex];

		wchar_t* szAttributeName = nullptr;
		engiGetEntityArgumentName(
			iEntity,
			iIndex,
			sdaiUNICODE,
			(char**)&szAttributeName);

		switch(pAttribute->GetType())
		{
			case sdaiADB:
			{
				CreateADBGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);				
			}
			break;

			case sdaiAGGR:
			{
				// NA
			}
			break;

			case sdaiENUM:
			{
				CreateEnumGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);				
			}
			break;			

			case sdaiINSTANCE:
			{
				// NA
			}
			break;

			case sdaiINTEGER:
			{
				CreateIntGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);
			}
			break;

			case sdaiLOGICAL:
			{
				CreateLogicalGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);
			}
			break;

			case sdaiREAL:
			case sdaiNUMBER:
			{
				CreateRealGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);				
			}
			break;

			case sdaiSTRING:
			{
				CreateStringGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);				
			}
			break;

			case sdaiUNICODE:
			{
				CreateUnicodeGridProperty(pInstanceGroup, pTargetInstance, pAttribute, szAttributeName);				
			}
			break;

			default:
			{
				TRACE(L"\nNot supported Attribute: %d", pAttribute->GetType());
			}
			break;
		} // switch(pAttribute->GetType())
	} // for (SdaiInteger iIndex = ...

	m_wndPropList.AddProperty(pInstanceGroup);
}

void CPropertiesWnd::CreateADBGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	SdaiADB pADB = nullptr;
	if (sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&pADB))
	{
		switch (sdaiGetADBType(pADB))
		{
			case sdaiADB:
			{
				TRACE(L"\nNot supported Attribute: %d", pAttribute->GetType());
			}
			break;

			case sdaiAGGR:
			{
				// NA
			}
			break;

			case sdaiENUM:
			{
				char* szValue = nullptr;
				sdaiGetADBValue(pADB, sdaiGetADBType(pADB), &szValue);

				auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
				pParentGridProperty->AddSubItem(pAttributeProperty);

				auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)CA2W(szValue), szAttributeName,
					(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

				pAttributeProperty->AddSubItem(pAttributeValue);
			}
			break;

			case sdaiINSTANCE:
			{
				// NA
			}
			break;

			case sdaiINTEGER:
			{
				int_t iValue = 0;
				sdaiGetADBValue(pADB, sdaiGetADBType(pADB), &iValue);

				auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
				pParentGridProperty->AddSubItem(pAttributeProperty);

				auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)iValue, szAttributeName,
					(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

				pAttributeProperty->AddSubItem(pAttributeValue);
			}
			break;

			case sdaiLOGICAL:
			{
				char* szValue = nullptr;
				sdaiGetADBValue(pADB, sdaiGetADBType(pADB), &szValue);

				auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
				pParentGridProperty->AddSubItem(pAttributeProperty);

				auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)CA2W(szValue), szAttributeName,
					(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

				pAttributeProperty->AddSubItem(pAttributeValue);
			}
			break;

			case sdaiREAL:
			case sdaiNUMBER:
			{
				double dValue = 0.;
				sdaiGetADBValue(pADB, sdaiGetADBType(pADB), &dValue);

				auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
				pParentGridProperty->AddSubItem(pAttributeProperty);

				auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)dValue, szAttributeName,
					(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

				pAttributeProperty->AddSubItem(pAttributeValue);
			}
			break;

			case sdaiSTRING:
			{
				wchar_t* szValue = nullptr;
				sdaiGetADBValue(pADB, sdaiGetADBType(pADB), &szValue);

				auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
				pParentGridProperty->AddSubItem(pAttributeProperty);

				auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)szValue, szAttributeName,
					(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

				pAttributeProperty->AddSubItem(pAttributeValue);
			}
			break;

			case sdaiUNICODE:
			{
				wchar_t* szValue = nullptr;
				sdaiGetADBValue(pADB, sdaiGetADBType(pADB), &szValue);

				auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
				pParentGridProperty->AddSubItem(pAttributeProperty);

				auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)szValue, szAttributeName,
					(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

				pAttributeProperty->AddSubItem(pAttributeValue);
			}
			break;

			default:
			{
				TRACE(L"\nNot supported Attribute: %d", (int)sdaiGetADBType(pADB));
			}
			break;
		} // switch (sdaiGetADBType(pADB))
	}
}

void CPropertiesWnd::CreateEnumGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	string strValue;
	char* szValue = nullptr;
	if (sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&szValue))
	{
		strValue = szValue;
	}

	auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
	pParentGridProperty->AddSubItem(pAttributeProperty);

	auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)CA2W(strValue.c_str()), szAttributeName,
		(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

	pAttributeProperty->AddSubItem(pAttributeValue);
}

void CPropertiesWnd::CreateIntGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	int_t iValue = 0;
	sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&iValue);

	auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
	pParentGridProperty->AddSubItem(pAttributeProperty);

	auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)iValue, szAttributeName,
		(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

	pAttributeProperty->AddSubItem(pAttributeValue);
}

void CPropertiesWnd::CreateLogicalGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	string strValue;
	char* szValue = nullptr;
	if (sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&szValue))
	{
		strValue = szValue;
	}

	auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
	pParentGridProperty->AddSubItem(pAttributeProperty);

	auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)CA2W(strValue.c_str()), szAttributeName,
		(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

	pAttributeProperty->AddSubItem(pAttributeValue);
}

void CPropertiesWnd::CreateRealGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	double dValue = 0.;
	sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&dValue);

	auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
	pParentGridProperty->AddSubItem(pAttributeProperty);

	auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)dValue, szAttributeName,
		(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

	pAttributeProperty->AddSubItem(pAttributeValue);
}

void CPropertiesWnd::CreateStringGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	wstring strValue;
	wchar_t* szValue = nullptr;
	if (sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		sdaiUNICODE,
		&szValue))
	{
		strValue = szValue;
	}

	auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
	pParentGridProperty->AddSubItem(pAttributeProperty);

	auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)strValue.c_str(), szAttributeName,
		(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

	pAttributeProperty->AddSubItem(pAttributeValue);
}

void CPropertiesWnd::CreateUnicodeGridProperty(CMFCPropertyGridProperty* pParentGridProperty, CInstanceBase* pInstance, CIFCAttribute* pAttribute, const wchar_t* szAttributeName)
{
	wstring strValue;
	wchar_t* szValue = nullptr;
	if (sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&szValue))
	{
		strValue = szValue;
	}

	auto pAttributeProperty = new CMFCPropertyGridProperty(szAttributeName);
	pParentGridProperty->AddSubItem(pAttributeProperty);

	auto pAttributeValue = new CIFCInstanceAttribute(L"value", (_variant_t)strValue.c_str(), szAttributeName,
		(DWORD_PTR)new CIFCInstanceAttributeData(GetController(), dynamic_cast<CIFCInstance*>(pInstance), pAttribute));

	pAttributeProperty->AddSubItem(pAttributeValue);
}

void CPropertiesWnd::UpdateADBAttribute(CInstanceBase* pInstance, CIFCAttribute* pAttribute, const CString& strName, const CString& strValue)
{
	SdaiADB pADB = nullptr;
	if (sdaiGetAttr(
		pInstance->GetInstance(),
		pAttribute->GetInstance(),
		pAttribute->GetType(),
		&pADB))
	{
		const char* szTypePath = sdaiGetADBTypePath(pADB, sdaiGetADBType(pADB));

		switch (sdaiGetADBType(pADB))
		{
			case sdaiENUM:
			{
				SdaiADB pValue = sdaiCreateADB(sdaiGetADBType(pADB), (LPCSTR)CW2A(strValue));
				sdaiPutADBTypePath(pValue, 1, szTypePath);
				sdaiPutAttrBN(
					pInstance->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiADB,
					pValue);
				sdaiDeleteADB(pValue);
			}
			break;

			case sdaiINTEGER:
			{
				int_t iValue = 0;
				if (!strValue.IsEmpty())
				{
					iValue = (int_t)atoll(CW2A((LPCTSTR)strValue));
				}

				SdaiADB pValue = sdaiCreateADB(sdaiGetADBType(pADB), &iValue);
				sdaiPutADBTypePath(pValue, 1, szTypePath);
				sdaiPutAttrBN(
					pInstance->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiADB,
					pValue);
				sdaiDeleteADB(pValue);
			}
			break;

			case sdaiLOGICAL:
			{
				SdaiADB pValue = sdaiCreateADB(sdaiGetADBType(pADB), (LPCSTR)CW2A(strValue));
				sdaiPutADBTypePath(pValue, 1, szTypePath);
				sdaiPutAttrBN(
					pInstance->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiADB,
					pValue);
				sdaiDeleteADB(pValue);
			}
			break;

			case sdaiREAL:
			case sdaiNUMBER:
			{
				double dValue = 0.;
				if (!strValue.IsEmpty())
				{
					dValue = atof(CW2A((LPCTSTR)strValue));
				}

				SdaiADB pValue = sdaiCreateADB(sdaiGetADBType(pADB), &dValue);
				sdaiPutADBTypePath(pValue, 1, szTypePath);
				sdaiPutAttrBN(
					pInstance->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiADB,
					pValue);
				sdaiDeleteADB(pValue);
			}
			break;

			case sdaiSTRING:
			{
				SdaiADB pValue = sdaiCreateADB(sdaiGetADBType(pADB), (LPCTSTR)strValue);
				sdaiPutADBTypePath(pValue, 1, szTypePath);
				sdaiPutAttrBN(
					pInstance->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiADB,
					pValue);
				sdaiDeleteADB(pValue);
			}
			break;

			case sdaiUNICODE:
			{
				SdaiADB pValue = sdaiCreateADB(sdaiGetADBType(pADB), (LPCTSTR)strValue);
				sdaiPutADBTypePath(pValue, 1, szTypePath);
				sdaiPutAttrBN(
					pInstance->GetInstance(),
					CW2A((LPCTSTR)strName),
					sdaiADB,
					pValue);
				sdaiDeleteADB(pValue);
			}
			break;

			default:
			{
				ASSERT(FALSE); // TODO
			}
			break;
		} // switch (sdaiGetADBType(pADB))
	}
}

void CPropertiesWnd::OnViewModeChanged()
{
	switch (m_wndObjectCombo.GetCurSel())
	{
		case 0:
		{
			LoadInstanceAttributes();
		}
		break;

		// Application and Properties are disabled!!!
		//case 0: // Application Properties
		//{
		//	LoadApplicationProperties();
		//}
		//break;

		//case 1: // Instance Properties
		//{
		//	LoadInstanceProperties();
		//}
		//break;

		default:
		{
			ASSERT(FALSE); // unknown mode
		}
		break;
	}
}

void CPropertiesWnd::OnDestroy()
{
	ASSERT(GetController() != nullptr);
	GetController()->UnRegisterView(this);

	__super::OnDestroy();
}

CPropertiesWnd::CPropertiesWnd() noexcept
{
	m_nComboHeight = 0;
}

CPropertiesWnd::~CPropertiesWnd()
{
}

BEGIN_MESSAGE_MAP(CPropertiesWnd, CDockablePane)
	ON_WM_CREATE()
	ON_WM_SIZE()
	ON_COMMAND(ID_EXPAND_ALL, OnExpandAllProperties)
	ON_UPDATE_COMMAND_UI(ID_EXPAND_ALL, OnUpdateExpandAllProperties)
	ON_COMMAND(ID_SORTPROPERTIES, OnSortProperties)
	ON_UPDATE_COMMAND_UI(ID_SORTPROPERTIES, OnUpdateSortProperties)
	ON_COMMAND(ID_PROPERTIES1, OnProperties1)
	ON_UPDATE_COMMAND_UI(ID_PROPERTIES1, OnUpdateProperties1)
	ON_COMMAND(ID_PROPERTIES2, OnProperties2)
	ON_UPDATE_COMMAND_UI(ID_PROPERTIES2, OnUpdateProperties2)
	ON_WM_SETFOCUS()
	ON_WM_SETTINGCHANGE()
	ON_WM_DESTROY()
	ON_REGISTERED_MESSAGE(AFX_WM_PROPERTY_CHANGED, OnPropertyChanged)
	ON_CBN_SELENDOK(ID_COMBO_PROPERTIES_VIEW, OnViewModeChanged)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CResourceViewBar message handlers

void CPropertiesWnd::AdjustLayout()
{
	if (GetSafeHwnd () == nullptr || (AfxGetMainWnd() != nullptr && AfxGetMainWnd()->IsIconic()))
	{
		return;
	}

	CRect rectClient;
	GetClientRect(rectClient);

	int cyTlb = m_wndToolBar.CalcFixedLayout(FALSE, TRUE).cy;

	m_wndObjectCombo.SetWindowPos(nullptr, rectClient.left, rectClient.top, rectClient.Width(), m_nComboHeight, SWP_NOACTIVATE | SWP_NOZORDER);
	m_wndToolBar.SetWindowPos(nullptr, rectClient.left, rectClient.top + m_nComboHeight, rectClient.Width(), cyTlb, SWP_NOACTIVATE | SWP_NOZORDER);
	m_wndPropList.SetWindowPos(nullptr, rectClient.left, rectClient.top + m_nComboHeight + cyTlb, rectClient.Width(), rectClient.Height() -(m_nComboHeight+cyTlb), SWP_NOACTIVATE | SWP_NOZORDER);
}

int CPropertiesWnd::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CDockablePane::OnCreate(lpCreateStruct) == -1)
		return -1;

	ASSERT(GetController() != nullptr);
	GetController()->RegisterView(this);

	CRect rectDummy;
	rectDummy.SetRectEmpty();

	// Create combo:
	const DWORD dwViewStyle = WS_CHILD | WS_VISIBLE | CBS_DROPDOWNLIST | WS_BORDER | CBS_SORT | WS_CLIPSIBLINGS | WS_CLIPCHILDREN;

	if (!m_wndObjectCombo.Create(dwViewStyle, rectDummy, this, ID_COMBO_PROPERTIES_VIEW))
	{
		TRACE0("Failed to create Properties Combo \n");
		return -1;      // fail to create
	}

	// Application and Properties are disabled!!!
	//m_wndObjectCombo.AddString(_T("Application"));
	//m_wndObjectCombo.AddString(_T("Properties"));
	m_wndObjectCombo.AddString(_T("Attributes"));
	m_wndObjectCombo.SetCurSel(0);

	CRect rectCombo;
	m_wndObjectCombo.GetClientRect (&rectCombo);

	m_nComboHeight = rectCombo.Height();

	if (!m_wndPropList.Create(WS_VISIBLE | WS_CHILD, rectDummy, this, 2))
	{
		TRACE0("Failed to create Properties Grid \n");
		return -1;      // fail to create
	}

	OnViewModeChanged();

	m_wndToolBar.Create(this, AFX_DEFAULT_TOOLBAR_STYLE, IDR_PROPERTIES);
	m_wndToolBar.LoadToolBar(IDR_PROPERTIES, 0, 0, TRUE /* Is locked */);
	m_wndToolBar.CleanUpLockedImages();
	m_wndToolBar.LoadBitmap(theApp.m_bHiColorIcons ? IDB_PROPERTIES_HC : IDR_PROPERTIES, 0, 0, TRUE /* Locked */);

	m_wndToolBar.SetPaneStyle(m_wndToolBar.GetPaneStyle() | CBRS_TOOLTIPS | CBRS_FLYBY);
	m_wndToolBar.SetPaneStyle(m_wndToolBar.GetPaneStyle() & ~(CBRS_GRIPPER | CBRS_SIZE_DYNAMIC | CBRS_BORDER_TOP | CBRS_BORDER_BOTTOM | CBRS_BORDER_LEFT | CBRS_BORDER_RIGHT));
	m_wndToolBar.SetOwner(this);

	// All commands will be routed via this control , not via the parent frame:
	m_wndToolBar.SetRouteCommandsViaFrame(FALSE);

	AdjustLayout();
	return 0;
}

void CPropertiesWnd::OnSize(UINT nType, int cx, int cy)
{
	CDockablePane::OnSize(nType, cx, cy);
	AdjustLayout();
}

void CPropertiesWnd::OnExpandAllProperties()
{
	m_wndPropList.ExpandAll();
}

void CPropertiesWnd::OnUpdateExpandAllProperties(CCmdUI* /* pCmdUI */)
{
}

void CPropertiesWnd::OnSortProperties()
{
	m_wndPropList.SetAlphabeticMode(!m_wndPropList.IsAlphabeticMode());
}

void CPropertiesWnd::OnUpdateSortProperties(CCmdUI* pCmdUI)
{
	pCmdUI->SetCheck(m_wndPropList.IsAlphabeticMode());
}

void CPropertiesWnd::OnProperties1()
{
	// TODO: Add your command handler code here
}

void CPropertiesWnd::OnUpdateProperties1(CCmdUI* /*pCmdUI*/)
{
	// TODO: Add your command update UI handler code here
}

void CPropertiesWnd::OnProperties2()
{
	// TODO: Add your command handler code here
}

void CPropertiesWnd::OnUpdateProperties2(CCmdUI* /*pCmdUI*/)
{
	// TODO: Add your command update UI handler code here
}

void CPropertiesWnd::InitPropList()
{
	SetPropListFont();

	m_wndPropList.EnableHeaderCtrl(FALSE);
	m_wndPropList.EnableDescriptionArea();
	m_wndPropList.SetVSDotNetLook();
	m_wndPropList.MarkModifiedProperties();

	CMFCPropertyGridProperty* pGroup1 = new CMFCPropertyGridProperty(_T("Appearance"));

	pGroup1->AddSubItem(new CMFCPropertyGridProperty(_T("3D Look"), (_variant_t) false, _T("Specifies the window's font will be non-bold and controls will have a 3D border")));

	CMFCPropertyGridProperty* pProp = new CMFCPropertyGridProperty(_T("Border"), _T("Dialog Frame"), _T("One of: None, Thin, Resizable, or Dialog Frame"));
	pProp->AddOption(_T("None"));
	pProp->AddOption(_T("Thin"));
	pProp->AddOption(_T("Resizable"));
	pProp->AddOption(_T("Dialog Frame"));
	pProp->AllowEdit(FALSE);

	pGroup1->AddSubItem(pProp);
	pGroup1->AddSubItem(new CMFCPropertyGridProperty(_T("Caption"), (_variant_t) _T("About"), _T("Specifies the text that will be displayed in the window's title bar")));

	m_wndPropList.AddProperty(pGroup1);

	CMFCPropertyGridProperty* pSize = new CMFCPropertyGridProperty(_T("Window Size"), 0, TRUE);

	pProp = new CMFCPropertyGridProperty(_T("Height"), (_variant_t) 250l, _T("Specifies the window's height"));
	pProp->EnableSpinControl(TRUE, 50, 300);
	pSize->AddSubItem(pProp);

	pProp = new CMFCPropertyGridProperty( _T("Width"), (_variant_t) 150l, _T("Specifies the window's width"));
	pProp->EnableSpinControl(TRUE, 50, 200);
	pSize->AddSubItem(pProp);

	m_wndPropList.AddProperty(pSize);

	CMFCPropertyGridProperty* pGroup2 = new CMFCPropertyGridProperty(_T("Font"));

	LOGFONT lf;
	CFont* font = CFont::FromHandle((HFONT) GetStockObject(DEFAULT_GUI_FONT));
	font->GetLogFont(&lf);

	_tcscpy_s(lf.lfFaceName, _T("Arial"));

	pGroup2->AddSubItem(new CMFCPropertyGridFontProperty(_T("Font"), lf, CF_EFFECTS | CF_SCREENFONTS, _T("Specifies the default font for the window")));
	pGroup2->AddSubItem(new CMFCPropertyGridProperty(_T("Use System Font"), (_variant_t) true, _T("Specifies that the window uses MS Shell Dlg font")));

	m_wndPropList.AddProperty(pGroup2);

	CMFCPropertyGridProperty* pGroup3 = new CMFCPropertyGridProperty(_T("Misc"));
	pProp = new CMFCPropertyGridProperty(_T("(Name)"), _T("Application"));
	pProp->Enable(FALSE);
	pGroup3->AddSubItem(pProp);

	CMFCPropertyGridColorProperty* pColorProp = new CMFCPropertyGridColorProperty(_T("Window Color"), RGB(210, 192, 254), nullptr, _T("Specifies the default window color"));
	pColorProp->EnableOtherButton(_T("Other..."));
	pColorProp->EnableAutomaticButton(_T("Default"), ::GetSysColor(COLOR_3DFACE));
	pGroup3->AddSubItem(pColorProp);

	static const TCHAR szFilter[] = _T("Icon Files(*.ico)|*.ico|All Files(*.*)|*.*||");
	pGroup3->AddSubItem(new CMFCPropertyGridFileProperty(_T("Icon"), TRUE, _T(""), _T("ico"), 0, szFilter, _T("Specifies the window icon")));

	pGroup3->AddSubItem(new CMFCPropertyGridFileProperty(_T("Folder"), _T("c:\\")));

	m_wndPropList.AddProperty(pGroup3);

	CMFCPropertyGridProperty* pGroup4 = new CMFCPropertyGridProperty(_T("Hierarchy"));

	CMFCPropertyGridProperty* pGroup41 = new CMFCPropertyGridProperty(_T("First sub-level"));
	pGroup4->AddSubItem(pGroup41);

	CMFCPropertyGridProperty* pGroup411 = new CMFCPropertyGridProperty(_T("Second sub-level"));
	pGroup41->AddSubItem(pGroup411);

	pGroup411->AddSubItem(new CMFCPropertyGridProperty(_T("Item 1"), (_variant_t) _T("Value 1"), _T("This is a description")));
	pGroup411->AddSubItem(new CMFCPropertyGridProperty(_T("Item 2"), (_variant_t) _T("Value 2"), _T("This is a description")));
	pGroup411->AddSubItem(new CMFCPropertyGridProperty(_T("Item 3"), (_variant_t) _T("Value 3"), _T("This is a description")));

	pGroup4->Expand(FALSE);
	m_wndPropList.AddProperty(pGroup4);
}

void CPropertiesWnd::OnSetFocus(CWnd* pOldWnd)
{
	CDockablePane::OnSetFocus(pOldWnd);
	m_wndPropList.SetFocus();
}

void CPropertiesWnd::OnSettingChange(UINT uFlags, LPCTSTR lpszSection)
{
	CDockablePane::OnSettingChange(uFlags, lpszSection);
	SetPropListFont();
}

void CPropertiesWnd::SetPropListFont()
{
	::DeleteObject(m_fntPropList.Detach());

	LOGFONT lf;
	afxGlobalData.fontRegular.GetLogFont(&lf);

	NONCLIENTMETRICS info;
	info.cbSize = sizeof(info);

	afxGlobalData.GetNonClientMetrics(info);

	lf.lfHeight = info.lfMenuFont.lfHeight;
	lf.lfWeight = info.lfMenuFont.lfWeight;
	lf.lfItalic = info.lfMenuFont.lfItalic;

	m_fntPropList.CreateFontIndirect(&lf);

	m_wndPropList.SetFont(&m_fntPropList);
	m_wndObjectCombo.SetFont(&m_fntPropList);
}
