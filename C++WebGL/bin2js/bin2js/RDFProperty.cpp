#include "stdafx.h"
#include "RDFProperty.h"
#include <assert.h>

#ifdef _LINUX
#include<wx/string.h>
#endif // _LINUX

// ------------------------------------------------------------------------------------------------
CRDFProperty::CRDFProperty(int64_t iInstance)
	: m_iInstance(iInstance)
	, m_strName(L"")
	, m_iType(-1)
{
	assert(m_iInstance != 0);

	/*
	* Name
	*/
	char * szPropertyName = NULL;
	GetNameOfProperty(m_iInstance, &szPropertyName);

#ifndef _LINUX
    LOG_DEBUG("*** PROPERTY " << szPropertyName);

	m_strName = CA2W(szPropertyName);

	/*
	* Parents
	*/
	int64_t iParentClass = GetClassParentsByIterator(m_iInstance, 0);
	while (iParentClass != 0)
	{
		char * szParentClassName = NULL;
		GetNameOfClass(iParentClass, &szParentClassName);

		LOG_DEBUG("*** PARENT CLASS " << szParentClassName);

		iParentClass = GetClassParentsByIterator(m_iInstance, iParentClass);
	}
#endif // _LINUX
}

// ------------------------------------------------------------------------------------------------
CRDFProperty::~CRDFProperty()
{
}

// ------------------------------------------------------------------------------------------------
// Getter
int64_t CRDFProperty::getInstance() const
{
	return m_iInstance;
}

// ------------------------------------------------------------------------------------------------
// Getter
const wchar_t * CRDFProperty::getName() const
{
	return m_strName.c_str();
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFProperty::getType() const
{
	return m_iType;
}

// ------------------------------------------------------------------------------------------------
wstring CRDFProperty::getTypeAsString() const
{
	wstring strType = L"unknown";

	switch (getType())
	{
	case TYPE_OBJECTTYPE:
	{
		strType = L"owl:ObjectProperty";
	}
	break;

	case TYPE_BOOL_DATATYPE:
	{
		strType = L"owl:DatatypeProperty";
	}
	break;

	case TYPE_CHAR_DATATYPE:
	{
		strType = L"owl:DatatypeProperty";
	}
	break;

	case TYPE_DOUBLE_DATATYPE:
	{
		strType = L"owl:DatatypeProperty";
	}
	break;

	case TYPE_INT_DATATYPE:
	{
		strType = L"owl:DatatypeProperty";
	}
	break;

	default:
	{
		assert(false);
	}
	break;
	} // switch (getType())

	return strType;
}

// ------------------------------------------------------------------------------------------------
wstring CRDFProperty::getRange() const
{
	wstring strRange = L"unknown";

	switch (getType())
	{
	case TYPE_OBJECTTYPE:
	{
		strRange = L"xsd:object";
	}
	break;

	case TYPE_BOOL_DATATYPE:
	{
		strRange = L"xsd:boolean";
	}
	break;

	case TYPE_CHAR_DATATYPE:
	{
		strRange = L"xsd:string";
	}
	break;

	case TYPE_DOUBLE_DATATYPE:
	{
		strRange = L"xsd:double";
	}
	break;

	case TYPE_INT_DATATYPE:
	{
		strRange = L"xsd:integer";
	}
	break;

	default:
	{
		assert(false);
	}
	break;
	} // switch (getType())

	return strRange;
}

// ------------------------------------------------------------------------------------------------
wstring CRDFProperty::getCardinality(int64_t iInstance) const
{
	assert(iInstance != 0);

	wchar_t szBuffer[100];

	int64_t iCard = 0;

	switch (getType())
	{
	case TYPE_OBJECTTYPE:
	{
		int64_t * piInstances = NULL;
		GetObjectProperty(iInstance, getInstance(), &piInstances, &iCard);
	}
	break;

	case TYPE_BOOL_DATATYPE:
	{
		assert(false); // TODO
	}
	break;

	case TYPE_CHAR_DATATYPE:
	{
		char ** szValue = NULL;
		GetDatatypeProperty(iInstance, getInstance(), (void **)&szValue, &iCard);
	}
	break;

	case TYPE_DOUBLE_DATATYPE:
	{
		double * pdValue = NULL;
		GetDatatypeProperty(iInstance, getInstance(), (void **)&pdValue, &iCard);
	}
	break;

	case TYPE_INT_DATATYPE:
	{
		int64_t * piValue = NULL;
		GetDatatypeProperty(iInstance, getInstance(), (void **)&piValue, &iCard);
	}
	break;

	default:
	{
		assert(false);
	}
	break;
	} // switch (getType())

	int64_t	iMinCard = 0;
	int64_t iMaxCard = 0;
	GetRestrictions(iInstance, iMinCard, iMaxCard);

	if ((iMinCard == -1) && (iMaxCard == -1))
	{
		swprintf(szBuffer, 100, L"%lld of [0 - infinity>", iCard);
	}
	else
	{
		if (iMaxCard == -1)
		{
			swprintf(szBuffer, 100, L"%lld of [%lld - infinity>", iCard, iMinCard);
		}
		else
		{
			swprintf(szBuffer, 100, L"%lld of [%lld - %lld]", iCard, iMinCard, iMaxCard);
		}
	}

	return szBuffer;
}

// ------------------------------------------------------------------------------------------------
void CRDFProperty::GetRestrictions(int64_t iInstance, int64_t & iMinCard, int64_t & iMaxCard) const
{
	assert(iInstance != 0);

	int64_t iClassInstance = GetInstanceClass(iInstance);
	assert(iClassInstance != 0);

	iMinCard = 0;
	iMaxCard = 0;
	GetClassPropertyCardinalityRestriction(iClassInstance, getInstance(), &iMinCard, &iMaxCard);
}
