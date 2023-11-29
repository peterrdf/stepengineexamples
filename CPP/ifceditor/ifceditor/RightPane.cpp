// RightPane.cpp : implementation file
//

#include "stdafx.h"
#include "ifceditor.h"
#include "RightPane.h"

#include "ifcengine\include\engine.h"

#include <cstring>
#include <iostream>

#ifdef WIN64
extern	__int64	model;
#else
extern	__int32	model;
#endif


// CRightPane

IMPLEMENT_DYNCREATE(CRightPane, CTreeView)

CRightPane::CRightPane()
	: CTreeView()
{

}

CRightPane::~CRightPane()
{
}

void CRightPane::DoDataExchange(CDataExchange* pDX)
{
	CTreeView::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CRightPane, CTreeView)
	ON_NOTIFY_REFLECT(TVN_ITEMEXPANDING, &CRightPane::OnTvnItemexpanding)
	ON_NOTIFY_REFLECT(NM_CLICK, &CRightPane::OnNMClick)
	ON_NOTIFY_REFLECT(NM_RCLICK, &CRightPane::OnNMRClick)
	ON_WM_DESTROY()
END_MESSAGE_MAP()



// CRightPane message handlers


STRUCT_INSTANCE	* new_STRUCT_INSTANCE(int_t ifcInstance)
{
	STRUCT_INSTANCE	* instance = new STRUCT_INSTANCE;

	instance->ifcInstance = ifcInstance;
	instance->hItem = 0;
	instance->expanded = false;

	instance->attribute = 0;
	instance->attributeName = nullptr;

	return	instance;
}

void	CreateInstanceName(int_t instance, wchar_t * buffer)
{
	size_t	index = 0;
	buffer[index++] = '#';
	_i64tow_s(internalGetP21Line(instance), &buffer[index], 33, 10);
	index += wcslen(&buffer[index]);
	buffer[index++] = ' ';
	buffer[index++] = '=';
	buffer[index++] = ' ';
	wchar_t	* className = 0;
	engiGetEntityName(sdaiGetInstanceType(instance), sdaiUNICODE, (const char**) &className);
	memcpy(&buffer[index], className, wcslen(className) * sizeof(wchar_t));
	index += wcslen(className);
	buffer[index++] = '(';
	buffer[index++] = ')';
	buffer[index] = 0;
}

void CRightPane::InsertItemInstanceEntity(int_t ifcEntity, HTREEITEM hParent)
{
	wchar_t	* className = 0;
	engiGetEntityName(ifcEntity, sdaiUNICODE, (const char**) &className);

	TV_INSERTSTRUCT		tvstruct;
	tvstruct.hParent = hParent;
	tvstruct.hInsertAfter = TVI_LAST;
	tvstruct.item.mask = TVIF_IMAGE | TVIF_SELECTEDIMAGE | TVIF_TEXT | TVIF_PARAM | TVIF_CHILDREN;
	tvstruct.item.cChildren = 0;
	tvstruct.item.pszText = className;
	tvstruct.item.iImage = 0;
	tvstruct.item.iSelectedImage = 0;
	tvstruct.item.lParam = (LPARAM) 0;

	GetTreeCtrl().InsertItem(&tvstruct);
}

void CRightPane::CreateAttributeLabelInstance(SdaiInstance iInstance, std::wstring& strLabel)
{
	ASSERT(iInstance != 0);

	CString strValue;
#ifdef WIN64
	strValue.Format(_T("%lld"), internalGetP21Line(iInstance));
#else
	strValue.Format(_T("%ld"), internalGetP21Line(iInstance));
#endif // WIN64

	strLabel += L"#";
	strLabel += strValue;
}

void CRightPane::CreateAttributeLabelBoolean(bool bValue, std::wstring& strLabel)
{
	strLabel += bValue ? L".T." : L".F.";
}

void CRightPane::CreateAttributeLabelLogical(char* szValue, std::wstring& strLabel)
{
	strLabel += L".";
	strLabel += CA2W(szValue);
	strLabel += L".";
}

void CRightPane::CreateAttributeLabelEnumeration(char* szValue, std::wstring& strLabel)
{
	strLabel += L".";
	strLabel += CA2W(szValue);
	strLabel += L".";
}

void CRightPane::CreateAttributeLabelReal(double dValue, std::wstring& strLabel)
{
	CString strValue;
	strValue.Format(_T("%f"), dValue);

	strLabel += strValue;
}

void CRightPane::CreateAttributeLabelInteger(int_t iValue, std::wstring& strLabel)
{
	CString strValue;
	strValue.Format(_T("%lld"), iValue);

	strLabel += strValue;
}

void CRightPane::CreateAttributeLabelString(wchar_t* szValue, std::wstring& strLabel)
{
	if (szValue != nullptr)
	{
		strLabel += L"'";
		strLabel += szValue;
		strLabel += L"'";
	}
	else
	{
		strLabel += L"''";
	}
}

bool CRightPane::CreateAttributeLabelADB(SdaiADB ADB, std::wstring& strLabel)
{
	bool bHasChildren = false;

	strLabel += (const wchar_t*) sdaiGetADBTypePath(ADB, sdaiUNICODE);

	strLabel += L" (";

	switch (sdaiGetADBType(ADB))
	{
		case  sdaiADB:
		{
			SdaiADB attributeDataBlock = 0;
			if (sdaiGetADBValue(ADB, sdaiADB, &attributeDataBlock))
			{
				bHasChildren |= CreateAttributeLabelADB(attributeDataBlock, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiAGGR:
		{
			SdaiAggr valueAggr = nullptr;
			SdaiInstance iValueInstance = 0;
			if (sdaiGetADBValue(ADB, sdaiAGGR, &valueAggr)) {
				strLabel += L"(";
				bHasChildren |= CreateAttributeLabelAggregation(valueAggr, strLabel);
				strLabel += L")";
			}
			else if (sdaiGetADBValue(ADB, sdaiINSTANCE, &iValueInstance))
			{
				CreateAttributeLabelInstance(iValueInstance, strLabel);

				bHasChildren = true;
			}
			else
			{
				assert(iValueInstance == 0);
				ASSERT(FALSE);
			}
		}
		break;

		case  sdaiINSTANCE:
		{
			SdaiInstance iValue = 0;
			if (sdaiGetADBValue(ADB, sdaiINSTANCE, &iValue))
			{
				CreateAttributeLabelInstance(iValue, strLabel);

				bHasChildren = true;
			}
			else
			{
				assert(iValue == 0);
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiBOOLEAN:
		{
			bool bValue = false;
			if (sdaiGetADBValue(ADB, sdaiBOOLEAN, &bValue))
			{
				CreateAttributeLabelBoolean(bValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiLOGICAL:
		{
			char* szValue = nullptr;
			if (sdaiGetADBValue(ADB, sdaiLOGICAL, &szValue))
			{
				CreateAttributeLabelLogical(szValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiENUM:
		{
			char* szValue = nullptr;
			if (sdaiGetADBValue(ADB, sdaiENUM, &szValue))
			{
				CreateAttributeLabelEnumeration(szValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiREAL:
		{
			double dValue = 0.;
			if (sdaiGetADBValue(ADB, sdaiREAL, &dValue))
			{
				CreateAttributeLabelReal(dValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiINTEGER:
		{
			int_t iValue = 0;
			if (sdaiGetADBValue(ADB, sdaiINTEGER, &iValue))
			{
				CreateAttributeLabelInteger(iValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case  sdaiSTRING:
		{
			wchar_t	* szValue = nullptr;
			if (sdaiGetADBValue(ADB, sdaiUNICODE, &szValue))
			{
				CreateAttributeLabelString(szValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		default:
		{
			ASSERT(FALSE);
		}
		break;
	} // switch (sdaiGetADBType(ADB)) 

	strLabel += L")";

	return	bHasChildren;
}

bool CRightPane::CreateAttributeLabelAggregationElement(SdaiAggr aggregation, int_t aggrType, SdaiInteger iIndex, std::wstring& strLabel)
{
	bool bHasChildren = false;

	switch (aggrType)
	{
		case sdaiADB:
		{
			SdaiADB attributeDataBlock = 0;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiADB, &attributeDataBlock))
			{
				bHasChildren |= CreateAttributeLabelADB(attributeDataBlock, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiAGGR:
		{
			SdaiAggr valueAggr = nullptr;
			SdaiInstance    iValueInstance = 0;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiAGGR, &valueAggr))
			{
				strLabel += L"(";
				bHasChildren |= CreateAttributeLabelAggregation(valueAggr, strLabel);
				strLabel += L")";
			}
			else if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiINSTANCE, &iValueInstance))
			{
				CreateAttributeLabelInstance(iValueInstance, strLabel);

				bHasChildren = true;
			}
			else
			{
				assert(iValueInstance == 0);
				ASSERT(FALSE);
			}
		}
		break;

		case sdaiINSTANCE:
		{
			SdaiInstance iValue = 0;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiINSTANCE, &iValue))
			{
				CreateAttributeLabelInstance(iValue, strLabel);

				bHasChildren = true;
			}
			else
			{
				assert(iValue == 0);
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiBOOLEAN:
		{
			bool bValue = false;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiBOOLEAN, &bValue))
			{
				CreateAttributeLabelBoolean(bValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiLOGICAL:
		{
			char* szValue = nullptr;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiLOGICAL, &szValue))
			{
				CreateAttributeLabelLogical(szValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiENUM:
		{
			char* szValue = nullptr;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiENUM, &szValue))
			{
				CreateAttributeLabelEnumeration(szValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiREAL:
		{
			double dValue = 0.;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiREAL, &dValue))
			{
				CreateAttributeLabelReal(dValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiINTEGER:
		{
			int_t iValue = 0;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiINTEGER, &iValue))
			{
				CreateAttributeLabelInteger(iValue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiSTRING:
		{
			wchar_t* szVlue = nullptr;
			if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiUNICODE, &szVlue))
			{
				CreateAttributeLabelString(szVlue, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		default:
		{
			ASSERT(FALSE);
		}
		break;
	} // switch (iAggrType)

	return	bHasChildren;
}

bool CRightPane::CreateAttributeLabelAggregation(SdaiAggr sdaiAggregation, std::wstring& strLabel)
{
	bool bHasChildren = false;

	SdaiInteger iMemberCount = sdaiGetMemberCount(sdaiAggregation);
	if (iMemberCount == 0)
	{
		return  bHasChildren;
	}

	int_t iAggrType = 0;
	engiGetAggrType(sdaiAggregation, &iAggrType);

	SdaiInteger iIndex = 0;
	bHasChildren |= CreateAttributeLabelAggregationElement(sdaiAggregation, iAggrType, iIndex++, strLabel);

	while (iIndex < iMemberCount)
	{
		strLabel += L", ";

		bHasChildren |= CreateAttributeLabelAggregationElement(sdaiAggregation, iAggrType, iIndex++, strLabel);
	}

	return	bHasChildren;
}

bool CRightPane::CreateAttributeLabel(SdaiInstance ifcInstance, SdaiAttr attribute, std::wstring& strLabel)
{
	bool bHasChildren = false;

    int_t iAttributeType = engiGetAttrType(attribute);
    if (iAttributeType & engiTypeFlagAggr ||
		iAttributeType & engiTypeFlagAggrOption)
		iAttributeType = sdaiAGGR;

	switch (iAttributeType)
	{
        case 0:
		{
			strLabel += L"$";
		}		
        break;

		case sdaiADB:
		{
			SdaiADB attributeDataBlock = 0;
			if (sdaiGetAttr(ifcInstance, attribute, sdaiADB, &attributeDataBlock)) 
			{
				ASSERT(attributeDataBlock != nullptr);

				bHasChildren |= CreateAttributeLabelADB(attributeDataBlock, strLabel);
			}
			else
			{
				ASSERT(FALSE);

				strLabel += L"$";
			}
		}
		break;

		case sdaiAGGR:
		{
			SdaiAggr valueAggr = nullptr;
			SdaiInstance iValueInstance = 0;
			if (sdaiGetAttr(ifcInstance, attribute, sdaiAGGR, &valueAggr)) 
			{
				strLabel += L"(";
				bHasChildren |= CreateAttributeLabelAggregation(valueAggr, strLabel);
				strLabel += L")";
			}
			else if (sdaiGetAttr(ifcInstance, attribute, sdaiINSTANCE, &iValueInstance)) 
			{
				CreateAttributeLabelInstance(iValueInstance, strLabel);

				bHasChildren = true;
			}
			else 
			{
				assert(iValueInstance == 0);
				strLabel += L"$";
			}
		}
		break;

        case sdaiINSTANCE:
        {
            SdaiInstance iValue = 0;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiINSTANCE, &iValue)) 
			{
				CreateAttributeLabelInstance(iValue, strLabel);

				bHasChildren = true;
			}
            else 
			{
				assert(iValue == 0);
				if (engiGetAttrDerived(sdaiGetInstanceType(ifcInstance), attribute))
				{
					strLabel += L"*";
                }
                else 
				{
					strLabel += L"$";
                }
            }
        }
        break;

        case sdaiBOOLEAN:
        {
            bool bValue = false;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiBOOLEAN, &bValue)) 
			{
				CreateAttributeLabelBoolean(bValue, strLabel);
            }
            else 
			{
				strLabel += L"$";
            }
        }
        break;

        case sdaiLOGICAL:
        {
            char* szValue = nullptr;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiLOGICAL, &szValue)) 
			{
				CreateAttributeLabelLogical(szValue, strLabel);
            }
            else 
			{
				strLabel += L"$";
            }
        }
        break;

		case sdaiENUM:
        {
            char* szValue = nullptr;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiENUM, &szValue))
			{
				CreateAttributeLabelEnumeration(szValue, strLabel);
            }
            else 
			{
				strLabel += L"$";
            }
        }
        break;

        case sdaiREAL:
        {
            double dValue = 0.;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiREAL, &dValue)) {
				CreateAttributeLabelReal(dValue, strLabel);
            }
            else 
			{
				strLabel += L"$";
            }
        }
        break;

        case sdaiINTEGER:
        {
            int_t iValue = 0;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiINTEGER, &iValue)) 
			{
				CreateAttributeLabelInteger(iValue, strLabel);
            }
            else 
			{
				strLabel += L"$";
            }
        }
        break;

		case sdaiSTRING:
        {
            wchar_t* szValue = nullptr;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiUNICODE, &szValue)) 
			{
				CreateAttributeLabelString(szValue, strLabel);
            }
            else 
			{
				strLabel += L"$";
            }
        }
        break;

		default:
		{
			ASSERT(FALSE);
		}
        break;
	} // switch (iAttributeType)

	return bHasChildren;
}

void CRightPane::CreateAttributeReferencesADB(SdaiADB ADB, HTREEITEM hParent)
{
    switch (sdaiGetADBType(ADB)) 
	{
        case sdaiADB:
        {
            SdaiADB attributeDataBlock = 0;
            if (sdaiGetADBValue(ADB, sdaiADB, &attributeDataBlock)) 
			{
				CreateAttributeReferencesADB(attributeDataBlock, hParent);
            }
        }
        break;

        case sdaiAGGR:
        {
            SdaiAggr valueAggr = nullptr;
            SdaiInstance iValueInstance = 0;
            if (sdaiGetADBValue(ADB, sdaiAGGR, &valueAggr)) {
				CreateAttributeReferencesAggregation(valueAggr, hParent);
            }
            else if (sdaiGetADBValue(ADB, sdaiINSTANCE, &iValueInstance)) 
			{
				InsertItemInstance(iValueInstance, hParent);
			}
            else 
			{
                ASSERT(FALSE);
            }
        }
        break;

        case sdaiINSTANCE:
        {
            SdaiInstance iValue = 0;
            if (sdaiGetADBValue(ADB, sdaiINSTANCE, &iValue))
			{
				InsertItemInstance(iValue, hParent);
			}
        }
        break;

        case  sdaiBOOLEAN:
        case  sdaiLOGICAL:
        case  sdaiENUM:
        case  sdaiREAL:
        case  sdaiINTEGER:
        case  sdaiSTRING:
			break;

        default:
		{
			ASSERT(FALSE);
		}        
        break;
    } // switch (sdaiGetADBType(ADB)) 
}

void CRightPane::CreateAttributeReferencesAggregationElement(SdaiAggr aggregation, int_t iAggrType, SdaiInteger iIndex, HTREEITEM hParent)
{
    switch (iAggrType)
	{
        case sdaiADB:
        {
            SdaiADB  attributeDataBlock = 0;
            if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiADB, &attributeDataBlock))
			{
				CreateAttributeReferencesADB(attributeDataBlock, hParent);
            }
        }
        break;

		case sdaiAGGR:
        {
            SdaiAggr valueAggr = nullptr;
            SdaiInstance iValueInstance = 0;
            if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiAGGR, &valueAggr)) {
				CreateAttributeReferencesAggregation(valueAggr, hParent);
            }
            else if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiINSTANCE, &iValueInstance)) {
				InsertItemInstance(iValueInstance, hParent);
			}
            else 
			{
                ASSERT(FALSE);
            }
        }
        break;

		case sdaiINSTANCE:
        {
            SdaiInstance iValue = 0;
            if (sdaiGetAggrByIndex(aggregation, iIndex, sdaiINSTANCE, &iValue))
			{
				InsertItemInstance(iValue, hParent);
			}
        }
        break;

        case sdaiBOOLEAN:
        case sdaiLOGICAL:
        case sdaiENUM:
        case sdaiREAL:
        case sdaiINTEGER:
        case sdaiSTRING:
			break;

		default:
		{
			ASSERT(FALSE);
		}        
        break;
    } // switch (iAggrType)
}

void CRightPane::CreateAttributeReferencesAggregation(SdaiAggr aggregation, HTREEITEM hParent)
{
    SdaiInteger iMemberCount = sdaiGetMemberCount(aggregation);
	if (iMemberCount == 0)
	{
		return;
	}        

    int_t iAggrType = 0;
    engiGetAggrType(aggregation, &iAggrType);

    SdaiInteger iIndex = 0;
	CreateAttributeReferencesAggregationElement(aggregation, iAggrType, iIndex++, hParent);

    while (iIndex < iMemberCount)
	{
		CreateAttributeReferencesAggregationElement(aggregation, iAggrType, iIndex++, hParent);
    }
}

void CRightPane::CreateAttributeReferences(SdaiInstance ifcInstance, SdaiAttr attribute, HTREEITEM hParent)
{
    int_t iAttrType = engiGetAttrType(attribute);
    if (iAttrType & engiTypeFlagAggr ||
		iAttrType & engiTypeFlagAggrOption)
		iAttrType = sdaiAGGR;

    switch (iAttrType)
	{
        case 0:
		{
		}
        break;

        case sdaiADB:
		{
            SdaiADB attributeDataBlock = 0;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiADB, &attributeDataBlock)) 
			{
                ASSERT(attributeDataBlock != nullptr);

				CreateAttributeReferencesADB(attributeDataBlock, hParent);
            }
        }
        break;

        case sdaiAGGR:
        {
            SdaiAggr valueAggr = nullptr;
            SdaiInstance iValueInstance = 0;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiAGGR, &valueAggr)) {
				CreateAttributeReferencesAggregation(valueAggr, hParent);
            }
            else if (sdaiGetAttr(ifcInstance, attribute, sdaiINSTANCE, &iValueInstance)) {
				InsertItemInstance(iValueInstance, hParent);
			}
        }
        break;

        case sdaiINSTANCE:
        {
            SdaiInstance iValue = 0;
            if (sdaiGetAttr(ifcInstance, attribute, sdaiINSTANCE, &iValue)) {
				InsertItemInstance(iValue, hParent);
            }
        }
        break;

        case sdaiBOOLEAN:
        case sdaiLOGICAL:
        case sdaiENUM:
        case sdaiREAL:
		case sdaiINTEGER:
		case sdaiSTRING:
			break;

		default:
		{
			ASSERT(FALSE);
		}        
        break;
    } // switch (iAttrType)
}

void CRightPane::InsertItemInstanceAttribute(SdaiInstance ifcInstance, SdaiAttr attribute, const char * attributeName, HTREEITEM hParent)
{
	std::wstring strAttribute = CA2W(attributeName);
	strAttribute += L" = ";

	bool	children = CreateAttributeLabel(ifcInstance, attribute, strAttribute);

	STRUCT_INSTANCE	* instance = new_STRUCT_INSTANCE(ifcInstance);
	instance->attribute = attribute;
	instance->attributeName = attributeName;

	TV_INSERTSTRUCT		tvstruct;
	tvstruct.hParent = hParent;
	tvstruct.hInsertAfter = TVI_LAST;
	tvstruct.item.mask = TVIF_IMAGE | TVIF_SELECTEDIMAGE | TVIF_TEXT | TVIF_PARAM | TVIF_CHILDREN;
	if (children) {
		tvstruct.item.cChildren = 1;
	}
	else {
		tvstruct.item.cChildren = 0;
	}
	tvstruct.item.pszText = &strAttribute[0];
	tvstruct.item.iImage = 0;
	tvstruct.item.iSelectedImage = 0;
	tvstruct.item.lParam = (LPARAM) instance;

	instance->hItem = GetTreeCtrl().InsertItem(&tvstruct);
}

SdaiInteger CRightPane::InsertItemAttributes(SdaiEntity ifcEntity, SdaiInstance ifcInstance, HTREEITEM hParent)
{
	if (ifcEntity) {
		int_t   indexOffset = InsertItemAttributes(engiGetEntityParent(ifcEntity), ifcInstance, hParent);

		const bool  countedWithParents = false,
					countedWithInverse = true;

		SdaiInteger index = 0;
		SdaiAttr    attribute = engiGetEntityAttributeByIndex(
										ifcEntity,
										index++,
										countedWithParents,
										countedWithInverse
									);
		while (attribute) {
			char    * attributeName = nullptr;
			engiGetEntityArgumentName(ifcEntity, indexOffset++, sdaiSTRING, (const char**) &attributeName);

			InsertItemInstanceAttribute(ifcInstance, attribute, attributeName, hParent);

			attribute = engiGetEntityAttributeByIndex(
								ifcEntity,
								index++,
								countedWithParents,
								countedWithInverse
							);
		}

		return	indexOffset;
	}

	return	0;
}

void CRightPane::InsertItemReferences(SdaiInstance ifcInstance, SdaiAttr attribute, HTREEITEM hParent)
{
	CreateAttributeReferences(ifcInstance, attribute, hParent);
}

void CRightPane::InsertItemInstance(SdaiInstance ifcInstance, HTREEITEM hParent)
{
	if (ifcInstance) {
		wchar_t	buffer[512];
		CreateInstanceName(ifcInstance, buffer);

		STRUCT_INSTANCE	* instance = new_STRUCT_INSTANCE(ifcInstance);

		TV_INSERTSTRUCT		tvstruct;
		tvstruct.hParent = hParent;
		tvstruct.hInsertAfter = TVI_LAST;
		tvstruct.item.mask = TVIF_IMAGE | TVIF_SELECTEDIMAGE | TVIF_TEXT | TVIF_PARAM | TVIF_CHILDREN;
		tvstruct.item.cChildren = 1;
		tvstruct.item.pszText = buffer;
		tvstruct.item.iImage = 0;
		tvstruct.item.iSelectedImage = 0;
		tvstruct.item.lParam = (LPARAM) instance;

		instance->hItem = GetTreeCtrl().InsertItem(&tvstruct);
	}
}

void CRightPane::OnInitialUpdate()
{
	CTreeView::OnInitialUpdate();

	CImageList* pImageList = new CImageList();
	pImageList->Create(16, 16, ILC_COLOR4, 0, 0);

	CTreeCtrl *tst = &GetTreeCtrl();

	::SetWindowLong(*tst, GWL_STYLE, TVS_EDITLABELS|TVS_LINESATROOT|TVS_HASLINES|TVS_HASBUTTONS|TVS_INFOTIP|::GetWindowLong(*tst, GWL_STYLE));

	GetTreeCtrl().SetImageList(pImageList, TVSIL_NORMAL);
	GetTreeCtrl().DeleteAllItems();
}

void CRightPane::SelectedEntity(SdaiEntity ifcEntity)
{
	GetTreeCtrl().DeleteAllItems();

	SdaiAggr	ifcObjects = xxxxGetEntityAndSubTypesExtent(model, ifcEntity);
	SdaiInteger	noIfcObjects = sdaiGetMemberCount(ifcObjects);

	for (SdaiInteger i = 0; i < noIfcObjects; i++) {
		SdaiInstance	ifcObject = 0;
		sdaiGetAggrByIndex(ifcObjects, i, sdaiINSTANCE, &ifcObject);

		InsertItemInstance(ifcObject, 0);
	}
}

LRESULT CRightPane::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	if (message == ID_UPDATE_RIGHT_VIEW) {
		SelectedEntity(lParam);
	}

	return CTreeView::WindowProc(message, wParam, lParam);
}


void CRightPane::OnTvnItemexpanding(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMTREEVIEW* pnmtv = (NMTREEVIEW*) pNMHDR;
	HTREEITEM hItem = pnmtv->itemNew.hItem;

	STRUCT_INSTANCE	* instance = (STRUCT_INSTANCE *) GetTreeCtrl().GetItemData(hItem);
	if (instance && instance->expanded == false) {
		if (instance->attribute) {
			InsertItemReferences(instance->ifcInstance, instance->attribute, instance->hItem);
		}
		else {
			InsertItemAttributes(sdaiGetInstanceType(instance->ifcInstance), instance->ifcInstance, instance->hItem);
		}
		instance->expanded = true;
	}

	*pResult = 0;
}

void CRightPane::OnNMClick(NMHDR *pNMHDR, LRESULT *pResult)
{
	//...
	UNREFERENCED_PARAMETER(pNMHDR);
	*pResult = 0;
}


void CRightPane::OnNMRClick(NMHDR *pNMHDR, LRESULT *pResult)
{
	UNREFERENCED_PARAMETER(pNMHDR);
	DWORD		pos = GetMessagePos();
	CPoint		pt(LOWORD(pos), HIWORD(pos));
	GetTreeCtrl().ScreenToClient(&pt);

	HTREEITEM	hItem = GetTreeCtrl().GetFirstVisibleItem();

	while (hItem) {
		CRect r;
		GetTreeCtrl().GetItemRect(hItem, &r, true);

		if (r.PtInRect(pt)) {
			STRUCT_INSTANCE	* instance = (STRUCT_INSTANCE *) GetTreeCtrl().GetItemData(hItem);
			if (instance) {
				if (instance->attribute == 0 && instance->attributeName == 0) {
					HMENU	hMenu = ::CreatePopupMenu();
					
					int64_t	owlInstance = 0;
					owlBuildInstance(engiGetEntityModel(sdaiGetInstanceType(instance->ifcInstance)), instance->ifcInstance, &owlInstance);

					if (owlInstance) {
						::AppendMenu(hMenu, 0, 1, L"export geometry as *.bin");
					}
					else {
						::AppendMenu(hMenu, MF_DISABLED, 1, L"no geometry attached to this instance");
					}

					DWORD	posa = GetMessagePos();
					CPoint	pta(LOWORD(posa), HIWORD(posa));
					
					int sel = ::TrackPopupMenuEx(hMenu, 
						TPM_CENTERALIGN | TPM_RETURNCMD,
						pta.x,// + r.right,
						pta.y,// + r.top,
						GetTreeCtrl(),
						NULL);
					::DestroyMenu(hMenu);
					if (sel > 0) {
						int	express_id = (int) internalGetP21Line(instance->ifcInstance);

						char strFilter[] = { "Text Files (*.rdf)|*.rdf|" }, buff[512];
						_itoa_s(express_id, buff, 512, 10);
						CFileDialog FileDlg(false, CString(".rdf"), CString("item #") + CString(buff) + CString(".rdf"), 0, CString(strFilter)); 

						if (FileDlg.DoModal() == IDOK) {
							assert(sel == 1);

							CString	fileName = FileDlg.GetFolderPath() + "\\" + FileDlg.GetFileName();

							char	fileNameChar[512];
							int		i = 0;
							while  (fileName[i]) {
								fileNameChar[i] = (char) fileName[i];
								i++;
							}
							fileNameChar[i] = 0;
							SaveInstanceTree(owlInstance, fileNameChar);
						}
					}
					cleanMemory(model, 1);
				}
			}
		}
		hItem = GetTreeCtrl().GetNextVisibleItem(hItem);
	}

	*pResult = 0;
}

void CRightPane::OnDestroy()
{
	auto imageList = GetTreeCtrl().SetImageList(nullptr, TVSIL_NORMAL);
	
	CTreeView::OnDestroy();
	
	if (imageList) {
		delete imageList;
	}
}
