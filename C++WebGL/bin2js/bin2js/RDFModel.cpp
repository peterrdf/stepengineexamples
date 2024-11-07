#include "stdafx.h"

#include "RDFModel.h"
#include "Generic.h"

#include <bitset>
#include <algorithm>

using namespace std;

#ifdef _LINUX
#include <cfloat>
#include <wx/wx.h>
#include <wx/stdpaths.h>
#include <cwchar>
#endif // _LINUX

// ------------------------------------------------------------------------------------------------
CRDFModel::CRDFModel()
	: m_iModel(0)
	, m_iCoordinateSystemModel(0)
	, m_mapRDFClasses()
	, m_mapRDFProperties()
	, m_mapRDFInstances()
	, m_iID(1)
	, m_fXmin(-1.f)
	, m_fXmax(1.f)
	, m_fYmin(-1.f)
	, m_fYmax(1.f)
	, m_fZmin(-1.f)
	, m_fZmax(1.f)
	, m_fBoundingSphereDiameter(1.f)
{
}

// ------------------------------------------------------------------------------------------------
CRDFModel::~CRDFModel()
{
	Clean();
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFModel::GetModel() const
{
	return m_iModel;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFModel::GetCoordinateSystemModel() const
{
	return m_iCoordinateSystemModel;
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::CreateDefaultModel()
{
	Clean();

	m_iModel = CreateModel();
	assert(m_iModel != 0);

	SetFormatSettings(m_iModel);

	LoadRDFModel();

	int64_t iCubeClass = GetClassByName(m_iModel, "Cube");
	int64_t iConeClass = GetClassByName(m_iModel, "Cone");
	int64_t iCylinderClass = GetClassByName(m_iModel, "Cylinder");
	int64_t iMaterialClass = GetClassByName(m_iModel, "Material");
	int64_t iTextureClass = GetClassByName(m_iModel, "Texture");
	int64_t iColorClass = GetClassByName(m_iModel, "Color");
	int64_t iColorComponentClass = GetClassByName(m_iModel, "ColorComponent");

	// Cube 1
	{
		int64_t iCubeInstance = CreateInstance(iCubeClass);
		int64_t iMaterialInstance = CreateInstance(iMaterialClass);
		int64_t iTextureInstance = CreateInstance(iTextureClass);
		int64_t iColorInstance = CreateInstance(iColorClass);
		int64_t iColorComponentInstance = CreateInstance(iColorComponentClass);

		double dScalingX = 1.;
		SetDatatypeProperty(iTextureInstance, GetPropertyByName(m_iModel, "scalingX"), &dScalingX, 1);

		double dScalingY = 1.;
		SetDatatypeProperty(iTextureInstance, GetPropertyByName(m_iModel, "scalingY"), &dScalingY, 1);

		double dValue = 7;
		SetDatatypeProperty(iCubeInstance, GetPropertyByName(m_iModel, "length"), &dValue, 1);

		double dR = 0.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "R"), &dR, 1);

		double dG = 1.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "G"), &dG, 1);

		double dB = 0.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "B"), &dB, 1);

		double dTransparency = 1.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "W"), &dTransparency, 1);

		SetObjectProperty(iColorInstance, GetPropertyByName(m_iModel, "ambient"), &iColorComponentInstance, 1);
		SetObjectProperty(iMaterialInstance, GetPropertyByName(m_iModel, "color"), &iColorInstance, 1);
		SetObjectProperty(iMaterialInstance, GetPropertyByName(m_iModel, "textures"), &iTextureInstance, 1);
		SetObjectProperty(iCubeInstance, GetPropertyByName(m_iModel, "material"), &iMaterialInstance, 1);
	}

	// Cone 1
	{
		int64_t iConeInstance = CreateInstance(iConeClass);
		int64_t iMaterialInstance = CreateInstance(iMaterialClass);
		int64_t iColorInstance = CreateInstance(iColorClass);
		int64_t iColorComponentInstance = CreateInstance(iColorComponentClass);

		double dValue = 4;
		SetDatatypeProperty(iConeInstance, GetPropertyByName(m_iModel, "radius"), &dValue, 1);

		dValue = 12;
		SetDatatypeProperty(iConeInstance, GetPropertyByName(m_iModel, "height"), &dValue, 1);

		int64_t iValue = 36;
		SetDatatypeProperty(iConeInstance, GetPropertyByName(m_iModel, "segmentationParts"), &iValue, 1);

		double dR = 0.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "R"), &dR, 1);

		double dG = 0.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "G"), &dG, 1);

		double dB = 1.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "B"), &dB, 1);

		double dTransparency = 1.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "W"), &dTransparency, 1);

		SetObjectProperty(iColorInstance, GetPropertyByName(m_iModel, "ambient"), &iColorComponentInstance, 1);
		SetObjectProperty(iMaterialInstance, GetPropertyByName(m_iModel, "color"), &iColorInstance, 1);
		SetObjectProperty(iConeInstance, GetPropertyByName(m_iModel, "material"), &iMaterialInstance, 1);
	}

	// Cylinder 1
	{
		int64_t iCylinderInstance = CreateInstance(iCylinderClass);
		int64_t iMaterialInstance = CreateInstance(iMaterialClass);
		int64_t iColorInstance = CreateInstance(iColorClass);
		int64_t iColorComponentInstance = CreateInstance(iColorComponentClass);

		double dValue = 6;
		SetDatatypeProperty(iCylinderInstance, GetPropertyByName(m_iModel, "radius"), &dValue, 1);

		dValue = 6;
		SetDatatypeProperty(iCylinderInstance, GetPropertyByName(m_iModel, "length"), &dValue, 1);

		int64_t iValue = 36;
		SetDatatypeProperty(iCylinderInstance, GetPropertyByName(m_iModel, "segmentationParts"), &iValue, 1);

		double dR = 1.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "R"), &dR, 1);

		double dG = 0.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "G"), &dG, 1);

		double dB = 0.0;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "B"), &dB, 1);

		double dTransparency = 0.5;
		SetDatatypeProperty(iColorComponentInstance, GetPropertyByName(m_iModel, "W"), &dTransparency, 1);

		SetObjectProperty(iColorInstance, GetPropertyByName(m_iModel, "ambient"), &iColorComponentInstance, 1);
		SetObjectProperty(iMaterialInstance, GetPropertyByName(m_iModel, "color"), &iColorInstance, 1);
		SetObjectProperty(iCylinderInstance, GetPropertyByName(m_iModel, "material"), &iMaterialInstance, 1);
	}

	LoadRDFInstances();
}

// ------------------------------------------------------------------------------------------------
const map<int64_t, CRDFClass *> & CRDFModel::GetRDFClasses() const
{
	return m_mapRDFClasses;
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::GetClassAncestors(int64_t iClassInstance, vector<int64_t> & vecAncestors) const
{
	assert(iClassInstance != 0);

	map<int64_t, CRDFClass *>::const_iterator itRDFClass = m_mapRDFClasses.find(iClassInstance);
	assert(itRDFClass != m_mapRDFClasses.end());

	CRDFClass * pRDFClass = itRDFClass->second;

	const vector<int64_t> & vecParentClasses = pRDFClass->getParentClasses();
	if (vecParentClasses.empty())
	{
		return;
	}

	for (size_t iParentClass = 0; iParentClass < vecParentClasses.size(); iParentClass++)
	{
		vecAncestors.insert(vecAncestors.begin(), vecParentClasses[iParentClass]);

		GetClassAncestors(vecParentClasses[iParentClass], vecAncestors);
	}
}

// ------------------------------------------------------------------------------------------------
const map<int64_t, CRDFProperty *> & CRDFModel::GetRDFProperties()
{
	return m_mapRDFProperties;
}

// ------------------------------------------------------------------------------------------------
const map<int64_t, CRDFInstance *> & CRDFModel::GetRDFInstances() const
{
	return m_mapRDFInstances;
}
// ------------------------------------------------------------------------------------------------
CRDFInstance * CRDFModel::GetRDFInstanceByID(int64_t iID)
{
	assert(iID != 0);

	map<int64_t, CRDFInstance *>::iterator itRDFInstances = m_mapRDFInstances.begin();
	for (; itRDFInstances != m_mapRDFInstances.end(); itRDFInstances++)
	{
		if (itRDFInstances->second->getID() == iID)
		{
			return itRDFInstances->second;
		}
	}

	assert(false);

	return NULL;
}

// ------------------------------------------------------------------------------------------------
CRDFInstance * CRDFModel::CreateNewInstance(int64_t iClassInstance)
{
	assert(iClassInstance != 0);

	int64_t iInstance = CreateInstance(iClassInstance);
	assert(iInstance != 0);

	CRDFInstance * pRDFInstance = new CRDFInstance(m_iID++, m_iModel, iInstance);

	pRDFInstance->CalculateMinMax(m_fXmin, m_fXmax, m_fYmin, m_fYmax, m_fZmin, m_fZmax);

	m_mapRDFInstances[iInstance] = pRDFInstance;

	return pRDFInstance;
}

// ------------------------------------------------------------------------------------------------
bool CRDFModel::DeleteInstance(CRDFInstance * pInstance)
{
	assert(pInstance != NULL);

	bool bResult = RemoveInstance(pInstance->getInstance()) == 0 ? true : false;

	map<int64_t, CRDFInstance *>::iterator itRDFInstance = m_mapRDFInstances.find(pInstance->getInstance());
	assert(itRDFInstance != m_mapRDFInstances.end());

	m_mapRDFInstances.erase(itRDFInstance);

	delete pInstance;

	return bResult;
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::GetCompatibleInstances(CRDFInstance * pRDFInstance, CObjectRDFProperty * pObjectRDFProperty, vector<int64_t> & vecCompatibleInstances) const
{
	assert(pRDFInstance != NULL);
	assert(pObjectRDFProperty != NULL);

	int64_t iClassInstance = GetInstanceClass(pRDFInstance->getInstance());
	assert(iClassInstance != 0);

	const vector<int64_t> & vecRestrictions = pObjectRDFProperty->getRestrictions();
	assert(!vecRestrictions.empty());

	const map<int64_t, CRDFInstance *> & mapRFDInstances = GetRDFInstances();

	map<int64_t, CRDFInstance *>::const_iterator itRFDInstances = mapRFDInstances.begin();
	for (; itRFDInstances != mapRFDInstances.end(); itRFDInstances++)
	{
		/*
		* Skip this instance
		*/
		if (itRFDInstances->second == pRDFInstance)
		{
			continue;
		}

		/*
		* Skip the instances that belong to a different model
		*/
		if (itRFDInstances->second->GetModel() != pRDFInstance->GetModel())
		{
			continue;
		}

		/*
		* Check this instance
		*/
		if (std::find(vecRestrictions.begin(), vecRestrictions.end(), itRFDInstances->second->getClassInstance()) != vecRestrictions.end())
		{
			vecCompatibleInstances.push_back(itRFDInstances->second->getInstance());

			continue;
		}

		/*
		* Check the ancestor classes
		*/

		vector<int64_t> vecAncestorClasses;
		CRDFClass::GetAncestors(iClassInstance, vecAncestorClasses);

		if (vecAncestorClasses.empty())
		{
			continue;
		}

		for (size_t iAncestorClass = 0; iAncestorClass < vecAncestorClasses.size(); iAncestorClass++)
		{
			if (find(vecRestrictions.begin(), vecRestrictions.end(), vecAncestorClasses[iAncestorClass]) != vecRestrictions.end())
			{
				vecCompatibleInstances.push_back(itRFDInstances->second->getInstance());

				break;
			}
		} // for (size_t iAncestorClass = ...
	} // for (; itRFDInstances != ...
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::GetWorldDimensions(float & fXmin, float & fXmax, float & fYmin, float & fYmax, float & fZmin, float & fZmax) const
{
	fXmin = m_fXmin;
	fXmax = m_fXmax;
	fYmin = m_fYmin;
	fYmax = m_fYmax;
	fZmin = m_fZmin;
	fZmax = m_fZmax;
}

// ------------------------------------------------------------------------------------------------
float CRDFModel::GetBoundingSphereDiameter() const
{
	return m_fBoundingSphereDiameter;
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::ZoomToInstance(int64_t iInstance)
{
	assert(iInstance != 0);
	assert(m_mapRDFInstances.find(iInstance) != m_mapRDFInstances.end());

	m_fXmin = FLT_MAX;
	m_fXmax = -FLT_MAX;
	m_fYmin = FLT_MAX;
	m_fYmax = -FLT_MAX;
	m_fZmin = FLT_MAX;
	m_fZmax = -FLT_MAX;

	m_mapRDFInstances[iInstance]->CalculateMinMax(m_fXmin, m_fXmax, m_fYmin, m_fYmax, m_fZmin, m_fZmax);

	m_fBoundingSphereDiameter = m_fXmax - m_fXmin;
	m_fBoundingSphereDiameter = max(m_fBoundingSphereDiameter, m_fYmax - m_fYmin);
	m_fBoundingSphereDiameter = max(m_fBoundingSphereDiameter, m_fZmax - m_fZmin);

#ifndef _LINUX
	LOG_DEBUG("X/Y/Z min: " << m_fXmin << ", " << m_fYmin << ", " << m_fZmin);
	LOG_DEBUG("X/Y/Z max: " << m_fXmax << ", " << m_fYmax << ", " << m_fZmax);
	LOG_DEBUG("World's bounding sphere diameter: " << m_fBoundingSphereDiameter);
#endif // _LINUX
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::OnInstancePropertyEdited(CRDFInstance * /*pInstance*/, CRDFProperty * /*pProperty*/)
{
	SetFormatSettings(m_iModel);

	map<int64_t, CRDFInstance *>::iterator itRDFInstances = m_mapRDFInstances.begin();
	for (; itRDFInstances != m_mapRDFInstances.end(); itRDFInstances++)
	{
		if (itRDFInstances->second->GetModel() != m_iModel)
		{
			continue;
		}

		itRDFInstances->second->Recalculate();
	}
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::Save(const wchar_t * szPath)
{
	SaveModelW(m_iModel, szPath);
}

// ------------------------------------------------------------------------------------------------
// Loads a model
// ------------------------------------------------------------------------------------------------
void CRDFModel::Load(const wchar_t * szPath)
{
	Clean();

#ifndef _LINUX
	LOG_DEBUG("OpenModelW() BEGIN");
#endif // _LINUX

#ifdef _LINUX
    wxString strPath(szPath);

	m_iModel = OpenModel(strPath.char_str());
#else
    m_iModel = OpenModelW(szPath);
#endif // _LINUX

	assert(m_iModel != 0);

#ifndef _LINUX
	LOG_DEBUG("OpenModelW() END");
#endif // _LINUX

	SetFormatSettings(m_iModel);

	// DISABLED **************************************************************************************
	//LoadRDFModel();

	LoadRDFInstances();
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::SetFormatSettings(int64_t iModel)
{
	// X, Y, Z, Nx, Ny, Nz, Tx, Ty, Ambient, Diffuse, Emissive, Specular
	string strSettings = "001111000000001011000001110001";

	bitset<64> bitSettings(strSettings);
	int64_t iSettings = bitSettings.to_ulong();

	string strMask = "11111111111111111011011101110111";
	bitset <64> bitMask(strMask);
	int64_t iMask = bitMask.to_ulong();

	int64_t iVertexLength = SetFormat(iModel, (int64_t)iSettings, (int64_t)iMask);

#ifndef _LINUX
    LOG_DEBUG("SetFormat(): Settings = " << strSettings << ", Mask = " << strMask << ", Vertex length: " << iVertexLength);
#endif // _LINUX
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::CreateCoordinateSystem()
{
	m_iCoordinateSystemModel = CreateModel();
	assert(m_iCoordinateSystemModel != 0);

	SetFormatSettings(m_iCoordinateSystemModel);

	{
		int64_t	segmentationParts, i;
		int64_t	object, objects[18];

		double	_11, _12, _13, _21, _22, _23, _31, _32, _33, _41, _42, _43,
			extrusionLength, height, length, points[20], radius;

		int64_t	classCollection = GetClassByName(m_iCoordinateSystemModel, "Collection"),
			classCone = GetClassByName(m_iCoordinateSystemModel, "Cone"),
			classCylinder = GetClassByName(m_iCoordinateSystemModel, "Cylinder"),
			classExtrudedPolygon = GetClassByName(m_iCoordinateSystemModel, "ExtrudedPolygon"),
			classLine3D = GetClassByName(m_iCoordinateSystemModel, "Line3D"),
			classTransformation = GetClassByName(m_iCoordinateSystemModel, "Transformation");

		int64_t		property_11 = GetPropertyByName(m_iCoordinateSystemModel, "_11"),
			property_12 = GetPropertyByName(m_iCoordinateSystemModel, "_12"),
			property_13 = GetPropertyByName(m_iCoordinateSystemModel, "_13"),
			property_21 = GetPropertyByName(m_iCoordinateSystemModel, "_21"),
			property_22 = GetPropertyByName(m_iCoordinateSystemModel, "_22"),
			property_23 = GetPropertyByName(m_iCoordinateSystemModel, "_23"),
			property_31 = GetPropertyByName(m_iCoordinateSystemModel, "_31"),
			property_32 = GetPropertyByName(m_iCoordinateSystemModel, "_32"),
			property_33 = GetPropertyByName(m_iCoordinateSystemModel, "_33"),
			property_41 = GetPropertyByName(m_iCoordinateSystemModel, "_41"),
			property_42 = GetPropertyByName(m_iCoordinateSystemModel, "_42"),
			property_43 = GetPropertyByName(m_iCoordinateSystemModel, "_43"),
			propertyExtrusionLength = GetPropertyByName(m_iCoordinateSystemModel, "extrusionLength"),
			propertyHeight = GetPropertyByName(m_iCoordinateSystemModel, "height"),
			propertyLength = GetPropertyByName(m_iCoordinateSystemModel, "length"),
			propertyObject = GetPropertyByName(m_iCoordinateSystemModel, "object"),
			propertyObjects = GetPropertyByName(m_iCoordinateSystemModel, "objects"),
			propertyPoints = GetPropertyByName(m_iCoordinateSystemModel, "points"),
			propertyRadius = GetPropertyByName(m_iCoordinateSystemModel, "radius"),
			propertySegmentationParts = GetPropertyByName(m_iCoordinateSystemModel, "segmentationParts");

		int64_t		instance_arrow_X = CreateInstance(classTransformation),
			instance_arrow_X_collection = CreateInstance(classCollection),
			instanceI = CreateInstance(classTransformation),
			instanceII = CreateInstance(classCollection),
			instanceIII = CreateInstance(classExtrudedPolygon),
			instanceIV = CreateInstance(classExtrudedPolygon),
			instanceV = CreateInstance(classTransformation),
			instanceXScaleCenter = CreateInstance(classTransformation),
			instanceYScaleCenter = CreateInstance(classTransformation),
			instanceZScaleCenter = CreateInstance(classTransformation),
			instanceVI = CreateInstance(classCone),
			instanceVII = CreateInstance(classCylinder),

			instance_arrow_Y = CreateInstance(classTransformation),
			instance_arrow_Y_collection = CreateInstance(classCollection),
			instanceVIII = CreateInstance(classTransformation),
			instanceIX = CreateInstance(classCollection),
			instanceX = CreateInstance(classExtrudedPolygon),

			instance_arrow_Z_collection = CreateInstance(classCollection),
			instanceXI = CreateInstance(classTransformation),
			instanceXII = CreateInstance(classExtrudedPolygon),

			instance_arrows = CreateInstance(classCollection),
			instance_lines = CreateInstance(classCollection),

			instanceLines[18];

		i = 0;
		float fXmin = m_fXmin - ((m_fXmax - m_fXmin) * .1f);
		float fXmax = m_fXmax + ((m_fXmax - m_fXmin) * .1f);
		float fYmin = m_fYmin - ((m_fYmax - m_fYmin) * .1f);
		float fYmax = m_fYmax + ((m_fYmax - m_fYmin) * .1f);
		while (i < 9) {
			instanceLines[i] = CreateInstance(classLine3D);

			points[0] = fXmin - ((m_fXmax - m_fXmin) * .1f);
			points[1] = (fYmin + (((fYmax - fYmin) / 8.f) * i));
			points[2] = m_fZmin + (m_fZmax - m_fZmin) / 2.f;
			points[3] = fXmax + ((m_fXmax - m_fXmin) * .1f);
			points[4] = (fYmin + (((fYmax - fYmin) / 8.f) * i));
			points[5] = m_fZmin + (m_fZmax - m_fZmin) / 2.f;

			SetDatatypeProperty(instanceLines[i], propertyPoints, &points, 6);

			i++;
		}

		while (i < 18) {
			instanceLines[i] = CreateInstance(classLine3D);

			points[0] = (fXmin + (((fXmax - fXmin) / 8.f) * (i - 9)));
			points[1] = fYmin - ((m_fYmax - m_fYmin) * .1f);
			points[2] = m_fZmin + (m_fZmax - m_fZmin) / 2.f;
			points[3] = (fXmin + (((fXmax - fXmin) / 8.f) * (i - 9)));
			points[4] = fYmax + ((m_fYmax - m_fYmin) * .1f);
			points[5] = m_fZmin + (m_fZmax - m_fZmin) / 2.f;

			SetDatatypeProperty(instanceLines[i], propertyPoints, &points, 6);

			i++;
		}

		//
		//	Line Collection
		//
		i = 0;
		while (i < 18) {
			objects[i] = instanceLines[i];
			i++;
		}

		SetObjectProperty(instance_lines, propertyObjects, (int64_t *)objects, 18);

		//
		//	Arrow Collection
		//
		objects[0] = instanceXScaleCenter;
		objects[1] = instanceYScaleCenter;
		objects[2] = instanceZScaleCenter;

		SetObjectProperty(instance_arrows, propertyObjects, (int64_t *)objects, 3);

		//
		//	X complete
		//
		_11 = 0;
		_12 = 1;
		_22 = 0;
		_23 = 1;
		_31 = 1;
		_33 = 0;
		object = instance_arrow_X_collection;
		SetDatatypeProperty(instance_arrow_X, property_11, &_11, 1);
		SetDatatypeProperty(instance_arrow_X, property_12, &_12, 1);
		SetDatatypeProperty(instance_arrow_X, property_22, &_22, 1);
		SetDatatypeProperty(instance_arrow_X, property_23, &_23, 1);
		SetDatatypeProperty(instance_arrow_X, property_31, &_31, 1);
		SetDatatypeProperty(instance_arrow_X, property_33, &_33, 1);
		SetObjectProperty(instance_arrow_X, propertyObject, &object, 1);

		//
		//	Scale and center
		//
		_11 = m_fBoundingSphereDiameter / 4.f;
		_22 = m_fBoundingSphereDiameter / 4.f;
		_33 = m_fBoundingSphereDiameter / 4.f;
		_41 = m_fXmin + ((m_fXmax - m_fXmin) / 2.f);
		_42 = m_fYmin + ((m_fYmax - m_fYmin) / 2.f);
		_43 = m_fZmin + ((m_fZmax - m_fZmin) / 2.f);
		object = instance_arrow_X;
		SetDatatypeProperty(instanceXScaleCenter, property_11, &_11, 1);
		SetDatatypeProperty(instanceXScaleCenter, property_22, &_22, 1);
		SetDatatypeProperty(instanceXScaleCenter, property_33, &_33, 1);
		SetDatatypeProperty(instanceXScaleCenter, property_41, &_41, 1);
		SetDatatypeProperty(instanceXScaleCenter, property_42, &_42, 1);
		SetDatatypeProperty(instanceXScaleCenter, property_43, &_43, 1);
		SetObjectProperty(instanceXScaleCenter, propertyObject, &object, 1);

		//
		//	X Collection
		//
		objects[0] = instanceI;
		objects[1] = instanceV;
		objects[2] = instanceVII;

		SetObjectProperty((int64_t)instance_arrow_X_collection, (int64_t)propertyObjects, (int64_t *)&objects, 3);

		//
		//	Transformation letter
		//
		_11 = 0;
		_13 = .6;
		_21 = .6;
		_22 = 0;
		_32 = 1;
		_33 = 0;
		_41 = -1.;
		_42 = 0;
		_43 = 4.3;
		object = instanceII;
		SetDatatypeProperty(instanceI, property_11, &_11, 1);
		SetDatatypeProperty(instanceI, property_13, &_13, 1);
		SetDatatypeProperty(instanceI, property_21, &_21, 1);
		SetDatatypeProperty(instanceI, property_22, &_22, 1);
		SetDatatypeProperty(instanceI, property_32, &_32, 1);
		SetDatatypeProperty(instanceI, property_33, &_33, 1);
		SetDatatypeProperty(instanceI, property_41, &_41, 1);
		SetDatatypeProperty(instanceI, property_42, &_42, 1);
		SetDatatypeProperty(instanceI, property_43, &_43, 1);
		SetObjectProperty(instanceI, propertyObject, &object, 1);

		//
		//	Inside Collection
		//
		objects[0] = instanceIII;
		objects[1] = instanceIV;

		SetObjectProperty((int64_t)instanceII, (int64_t)propertyObjects, (int64_t *)&objects, 2);

		//
		//	First Extruded Polygon
		//
		extrusionLength = 0.02;
		points[0] = 0.;
		points[1] = 0.;
		points[2] = 0.15;
		points[3] = 0.;
		points[4] = 1.;
		points[5] = 1.2;
		points[6] = .85;
		points[7] = 1.2;

		SetDatatypeProperty((int64_t)instanceIII, (int64_t)propertyExtrusionLength, &extrusionLength, 1);
		SetDatatypeProperty((int64_t)instanceIII, (int64_t)propertyPoints, &points, 8);

		//
		//	Second Extruded Polygon
		//
		extrusionLength = 0.02;
		points[0] = 0.;
		points[1] = 1.2;
		points[2] = .15;
		points[3] = 1.2;
		points[4] = 1.;
		points[5] = 0.;
		points[6] = .85;
		points[7] = 0.;

		SetDatatypeProperty(instanceIV, propertyExtrusionLength, &extrusionLength, 1);
		SetDatatypeProperty(instanceIV, propertyPoints, &points, 8);

		//
		//	Transformation arrow end
		//
		_11 = 1;
		_22 = 1;
		_33 = 1;
		_41 = 0;
		_42 = 0;
		_43 = 3.2;
		object = instanceVI;
		SetDatatypeProperty(instanceV, property_11, &_11, 1);
		SetDatatypeProperty(instanceV, property_22, &_22, 1);
		SetDatatypeProperty(instanceV, property_33, &_33, 1);
		SetDatatypeProperty(instanceV, property_41, &_41, 1);
		SetDatatypeProperty(instanceV, property_42, &_42, 1);
		SetDatatypeProperty(instanceV, property_43, &_43, 1);
		SetObjectProperty(instanceV, propertyObject, &object, 1);

		//
		//	Arrow end
		//
		radius = .3;
		height = .8;
		segmentationParts = 36;
		SetDatatypeProperty(instanceVI, propertyRadius, &radius, 1);
		SetDatatypeProperty(instanceVI, propertyHeight, &height, 1);
		SetDatatypeProperty(instanceVI, propertySegmentationParts, &segmentationParts, 1);

		//
		//	Arrow line
		//
		length = 3.;
		radius = .06;
		segmentationParts = 24;
		SetDatatypeProperty(instanceVII, propertyLength, &length, 1);
		SetDatatypeProperty(instanceVII, propertyRadius, &radius, 1);
		SetDatatypeProperty(instanceVII, propertySegmentationParts, &segmentationParts, 1);

		//
		//	Y complete
		//
		_11 = -1;
		_22 = 0;
		_23 = 1;
		_32 = 1;
		_33 = 0;
		object = instance_arrow_Y_collection;
		SetDatatypeProperty(instance_arrow_Y, property_11, &_11, 1);
		SetDatatypeProperty(instance_arrow_Y, property_22, &_22, 1);
		SetDatatypeProperty(instance_arrow_Y, property_23, &_23, 1);
		SetDatatypeProperty(instance_arrow_Y, property_32, &_32, 1);
		SetDatatypeProperty(instance_arrow_Y, property_33, &_33, 1);
		SetObjectProperty(instance_arrow_Y, propertyObject, &object, 1);

		//
		//	Scale and center
		//
		_11 = m_fBoundingSphereDiameter / 4.f;
		_22 = m_fBoundingSphereDiameter / 4.f;
		_33 = m_fBoundingSphereDiameter / 4.f;
		_41 = m_fXmin + ((m_fXmax - m_fXmin) / 2.f);
		_42 = m_fYmin + ((m_fYmax - m_fYmin) / 2.f);
		_43 = m_fZmin + ((m_fZmax - m_fZmin) / 2.f);
		object = instance_arrow_Y;
		SetDatatypeProperty(instanceYScaleCenter, property_11, &_11, 1);
		SetDatatypeProperty(instanceYScaleCenter, property_22, &_22, 1);
		SetDatatypeProperty(instanceYScaleCenter, property_33, &_33, 1);
		SetDatatypeProperty(instanceYScaleCenter, property_41, &_41, 1);
		SetDatatypeProperty(instanceYScaleCenter, property_42, &_42, 1);
		SetDatatypeProperty(instanceYScaleCenter, property_43, &_43, 1);
		SetObjectProperty(instanceYScaleCenter, propertyObject, &object, 1);

		//
		//	Y Collection
		//
		objects[0] = instanceVIII;
		objects[1] = instanceV;
		objects[2] = instanceVII;

		SetObjectProperty((int64_t)instance_arrow_Y_collection, (int64_t)propertyObjects, (int64_t *)&objects, 3);

		//
		//	Transformation letter
		//
		_11 = 0;
		_13 = .6;
		_21 = .6;
		_22 = 0;
		_32 = 1;
		_33 = 0;
		_41 = -1.;
		_42 = 0;
		_43 = 4.3;
		object = instanceIX;
		SetDatatypeProperty(instanceVIII, property_11, &_11, 1);
		SetDatatypeProperty(instanceVIII, property_13, &_13, 1);
		SetDatatypeProperty(instanceVIII, property_21, &_21, 1);
		SetDatatypeProperty(instanceVIII, property_22, &_22, 1);
		SetDatatypeProperty(instanceVIII, property_32, &_32, 1);
		SetDatatypeProperty(instanceVIII, property_33, &_33, 1);
		SetDatatypeProperty(instanceVIII, property_41, &_41, 1);
		SetDatatypeProperty(instanceVIII, property_42, &_42, 1);
		SetDatatypeProperty(instanceVIII, property_43, &_43, 1);
		SetObjectProperty(instanceVIII, propertyObject, &object, 1);

		//
		//	Inside Collection
		//
		objects[0] = instanceX;
		objects[1] = instanceIII;

		SetObjectProperty((int64_t)instanceIX, (int64_t)propertyObjects, (int64_t *)&objects, 2);

		//
		//	First Extruded Polygon for Y
		//
		extrusionLength = 0.02;
		points[0] = 0.;
		points[1] = 1.2;
		points[2] = 0.15;
		points[3] = 1.2;
		points[4] = .575;
		points[5] = .6;
		points[6] = .425;
		points[7] = .6;

		SetDatatypeProperty((int64_t)instanceX, (int64_t)propertyExtrusionLength, &extrusionLength, 1);
		SetDatatypeProperty((int64_t)instanceX, (int64_t)propertyPoints, &points, 8);

		//
		//	Z Collection
		//

		//
		//	Scale and center
		//
		_11 = m_fBoundingSphereDiameter / 4.f;
		_22 = m_fBoundingSphereDiameter / 4.f;
		_33 = m_fBoundingSphereDiameter / 4.f;
		_41 = m_fXmin + ((m_fXmax - m_fXmin) / 2.f);
		_42 = m_fYmin + ((m_fYmax - m_fYmin) / 2.f);
		_43 = m_fZmin + ((m_fZmax - m_fZmin) / 2.f);
		object = instance_arrow_Z_collection;
		SetDatatypeProperty(instanceZScaleCenter, property_11, &_11, 1);
		SetDatatypeProperty(instanceZScaleCenter, property_22, &_22, 1);
		SetDatatypeProperty(instanceZScaleCenter, property_33, &_33, 1);
		SetDatatypeProperty(instanceZScaleCenter, property_41, &_41, 1);
		SetDatatypeProperty(instanceZScaleCenter, property_42, &_42, 1);
		SetDatatypeProperty(instanceZScaleCenter, property_43, &_43, 1);
		SetObjectProperty(instanceZScaleCenter, propertyObject, &object, 1);

		objects[0] = instanceXI;
		objects[1] = instanceV;
		objects[2] = instanceVII;

		SetObjectProperty((int64_t)instance_arrow_Z_collection, (int64_t)propertyObjects, (int64_t *)&objects, 3);

		//
		//	Transformation letter
		//
		_11 = .6;
		_22 = 0.;
		_23 = .6;
		_32 = 1;
		_33 = 0;
		_41 = .4;
		_42 = 0;
		_43 = 4.3;
		object = instanceXII;
		SetDatatypeProperty(instanceXI, property_11, &_11, 1);
		SetDatatypeProperty(instanceXI, property_22, &_22, 1);
		SetDatatypeProperty(instanceXI, property_23, &_23, 1);
		SetDatatypeProperty(instanceXI, property_32, &_32, 1);
		SetDatatypeProperty(instanceXI, property_33, &_33, 1);
		SetDatatypeProperty(instanceXI, property_41, &_41, 1);
		SetDatatypeProperty(instanceXI, property_42, &_42, 1);
		SetDatatypeProperty(instanceXI, property_43, &_43, 1);
		SetObjectProperty(instanceXI, propertyObject, &object, 1);

		//
		//	Z Extruded Polygon
		//
		extrusionLength = 0.02;
		points[0] = 0.;
		points[1] = 0.;
		points[2] = 1.;
		points[3] = 0.;

		points[4] = 1.;
		points[5] = .15;
		points[6] = .18;
		points[7] = .15;

		points[8] = 1.;
		points[9] = 1.05;
		points[10] = 1.;
		points[11] = 1.2;

		points[12] = 0.;
		points[13] = 1.2;
		points[14] = 0.;
		points[15] = 1.05;

		points[16] = .82;
		points[17] = 1.05;
		points[18] = 0.;
		points[19] = .15;

		SetDatatypeProperty(instanceXII, propertyExtrusionLength, &extrusionLength, 1);
		SetDatatypeProperty(instanceXII, propertyPoints, &points, 20);
	}
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::LoadRDFModel()
{
    #ifndef _LINUX
    LOG_DEBUG("CRDFModel::LoadRDFModel() BEGIN");
	LOG_DEBUG("*** CLASSES ***");
    #endif // _LINUX

	int64_t	iClassInstance = GetClassesByIterator(m_iModel, 0);
	while (iClassInstance != 0)
	{
		m_mapRDFClasses[iClassInstance] = new CRDFClass(iClassInstance);

		iClassInstance = GetClassesByIterator(m_iModel, iClassInstance);
	} // while (iClassInstance != 0)

	#ifndef _LINUX
	LOG_DEBUG("*** END CLASSES ***");
	LOG_DEBUG("*** PROPERTIES ***");
	#endif // _LINUX

	int64_t iPropertyInstance = GetPropertiesByIterator(m_iModel, 0);
	while (iPropertyInstance != 0)
	{
		int64_t iPropertyType = GetPropertyType(iPropertyInstance);
		switch (iPropertyType)
		{
			case TYPE_OBJECTTYPE:
			{
				m_mapRDFProperties[iPropertyInstance] = new CObjectRDFProperty(iPropertyInstance);
			}
			break;

			case TYPE_BOOL_DATATYPE:
			{
				m_mapRDFProperties[iPropertyInstance] = new CBoolRDFProperty(iPropertyInstance);
			}
			break;

			case TYPE_CHAR_DATATYPE:
			{
				m_mapRDFProperties[iPropertyInstance] = new CStringRDFProperty(iPropertyInstance);
			}
			break;

			case TYPE_INT_DATATYPE:
			{
				m_mapRDFProperties[iPropertyInstance] = new CIntRDFProperty(iPropertyInstance);
			}
			break;

			case TYPE_DOUBLE_DATATYPE:
			{
				m_mapRDFProperties[iPropertyInstance] = new CDoubleRDFProperty(iPropertyInstance);
			}
			break;

			default:
				assert(false);
				break;
		} // switch (iPropertyType)

		map<int64_t, CRDFClass *>::iterator itRDFClasses = m_mapRDFClasses.begin();
		for (; itRDFClasses != m_mapRDFClasses.end(); itRDFClasses++)
		{
			int64_t	iMinCard = 0;
			int64_t iMaxCard = 0;
			GetClassPropertyCardinalityRestriction(itRDFClasses->first, iPropertyInstance, &iMinCard, &iMaxCard);

			if ((iMinCard == -1) && (iMaxCard == -1))
			{
				continue;
			}

			itRDFClasses->second->AddPropertyRestriction(new CRDFPropertyRestriction(iPropertyInstance, iMinCard, iMaxCard));
		} // for (; itRDFClasses != ...

		iPropertyInstance = GetPropertiesByIterator(m_iModel, iPropertyInstance);
	} // while (iPropertyInstance != 0)

	#ifndef _LINUX
	LOG_DEBUG("*** END PROPERTIES ***");
	LOG_DEBUG("CRDFModel::LoadRDFModel() END");
	#endif // _LINUX
}

// ------------------------------------------------------------------------------------------------
void CRDFModel::LoadRDFInstances()
{
    #ifndef _LINUX
    LOG_DEBUG("CRDFModel::LoadRDFInstances() BEGIN");
    #endif // _LINUX

	m_fXmin = FLT_MAX;
	m_fXmax = -FLT_MAX;
	m_fYmin = FLT_MAX;
	m_fYmax = -FLT_MAX;
	m_fZmin = FLT_MAX;
	m_fZmax = -FLT_MAX;

	/*
	* Enumerate all instances and calculate X/Y/Z min/max
	*/
	int64_t iInstance = GetInstancesByIterator(m_iModel, 0);
	while (iInstance != 0)
	{
		CRDFInstance * pRDFInstance = new CRDFInstance(m_iID++, m_iModel, iInstance);

		pRDFInstance->CalculateMinMax(m_fXmin, m_fXmax, m_fYmin, m_fYmax, m_fZmin, m_fZmax);

		m_mapRDFInstances[iInstance] = pRDFInstance;

		iInstance = GetInstancesByIterator(m_iModel, iInstance);
	} // while (iInstance != 0)

	if (m_mapRDFInstances.empty())
	{
		m_fXmin = -1.f;
		m_fXmax = 1.f;
		m_fYmin = -1.f;
		m_fYmax = 1.f;
		m_fZmin = -1.f;
		m_fZmax = 1.f;
	}

	m_fBoundingSphereDiameter = m_fXmax - m_fXmin;
	m_fBoundingSphereDiameter = max(m_fBoundingSphereDiameter, m_fYmax - m_fYmin);
	m_fBoundingSphereDiameter = max(m_fBoundingSphereDiameter, m_fZmax - m_fZmin);

#ifndef _LINUX
    LOG_DEBUG("X/Y/Z min: " << m_fXmin << ", " << m_fYmin << ", " << m_fZmin);
	LOG_DEBUG("X/Y/Z max: " << m_fXmax << ", " << m_fYmax << ", " << m_fZmax);
	LOG_DEBUG("World's bounding sphere diameter: " << m_fBoundingSphereDiameter);
#endif // _LINUX

	// DISABLED ***********************************************************************************
	/*
	* Coordinate System
	*/
	//CreateCoordinateSystem();

	/*
	* Coordinate System - Instances
	*/
	iInstance = GetInstancesByIterator(m_iCoordinateSystemModel, 0);
	while (iInstance != 0)
	{
		assert(m_mapRDFInstances.find(iInstance) == m_mapRDFInstances.end());

		CRDFInstance * pRDFInstance = new CRDFInstance(m_iID++, m_iCoordinateSystemModel, iInstance);

		m_mapRDFInstances[iInstance] = pRDFInstance;

		iInstance = GetInstancesByIterator(m_iCoordinateSystemModel, iInstance);
	} // while (iInstance != 0)

	/*
	* Coordinate System - Properties
	*/
	//int64_t iPropertyInstance = GetPropertiesByIterator(m_iCoordinateSystemModel, 0);
	//while (iPropertyInstance != 0)
	//{
	//	int64_t iPropertyType = GetPropertyType(iPropertyInstance);
	//	switch (iPropertyType)
	//	{
	//	case TYPE_OBJECTTYPE:
	//	{
	//		m_mapRDFProperties[iPropertyInstance] = new CObjectRDFProperty(iPropertyInstance);
	//	}
	//	break;

	//	case TYPE_BOOL_DATATYPE:
	//	{
	//		m_mapRDFProperties[iPropertyInstance] = new CBoolRDFProperty(iPropertyInstance);
	//	}
	//	break;

	//	case TYPE_CHAR_DATATYPE:
	//	{
	//		m_mapRDFProperties[iPropertyInstance] = new CStringRDFProperty(iPropertyInstance);
	//	}
	//	break;

	//	case TYPE_INT_DATATYPE:
	//	{
	//		m_mapRDFProperties[iPropertyInstance] = new CIntRDFProperty(iPropertyInstance);
	//	}
	//	break;

	//	case TYPE_DOUBLE_DATATYPE:
	//	{
	//		m_mapRDFProperties[iPropertyInstance] = new CDoubleRDFProperty(iPropertyInstance);
	//	}
	//	break;

	//	default:
	//		//assert(false); Unknown
	//		break;
	//	} // switch (iPropertyType)

	//	iPropertyInstance = GetPropertiesByIterator(m_iCoordinateSystemModel, iPropertyInstance);
	//} // while (iPropertyInstance != 0)
	// ********************************************************************************************

#ifndef _LINUX
	LOG_DEBUG("CRDFModel::LoadRDFInstances() END");
#endif // _LINUX
}

// ------------------------------------------------------------------------------------------------
// UNUSED
/*
unsigned char * CRDFModel::LoadBMP(const char * imagepath, unsigned int& outWidth, unsigned int& outHeight, bool flipY)
{
	printf("Reading image %s\n", imagepath);
	outWidth = 0;
	outHeight = 0;
	// Data read from the header of the BMP file
	unsigned char header[54];
	unsigned int dataPos;
	unsigned int imageSize;
	// Actual RGB data
	unsigned char * data;

	// Open the file
	FILE * file = fopen(imagepath, "rb");
	if (!file)							    { assert(false); printf("Image could not be opened\n"); return NULL; }

	// Read the header, i.e. the 54 first bytes

	// If less than 54 byes are read, problem
	if (fread(header, 1, 54, file) != 54){
		assert(false);
		printf("Not a correct BMP file\n");
		return NULL;
	}
	// A BMP files always begins with "BM"
	if (header[0] != 'B' || header[1] != 'M'){
		assert(false);
		printf("Not a correct BMP file\n");
		return NULL;
	}
	// Make sure this is a 24bpp file
	if (*(int*)&(header[0x1E]) != 0)         { assert(false); printf("Not a correct BMP file\n");    return NULL; }
	if (*(int*)&(header[0x1C]) != 24)         { assert(false); printf("Not a correct BMP file\n");    return NULL; }

	// Read the information about the image
	dataPos = *(int*)&(header[0x0A]);
	imageSize = *(int*)&(header[0x22]);
	outWidth = *(int*)&(header[0x12]);
	outHeight = *(int*)&(header[0x16]);

	// Some BMP files are misformatted, guess missing information
	if (imageSize == 0)    imageSize = outWidth*outHeight * 3; // 3 : one byte for each Red, Green and Blue component
	if (dataPos == 0)      dataPos = 54; // The BMP header is done that way

	// Create a buffer
	data = new unsigned char[imageSize];

	// Read the actual data from the file into the buffer
	fread(data, 1, imageSize, file);

	// Everything is in memory now, the file wan be closed
	fclose(file);

	if (flipY){
		// swap y-axis
		unsigned char * tmpBuffer = new unsigned char[outWidth * 3];
		int size = outWidth * 3;
		for (unsigned int i = 0; i<outHeight / 2; i++){
			// copy row i to tmp
			memcpy_s(tmpBuffer, size, data + outWidth * 3 * i, size);
			// copy row h-i-1 to i
			memcpy_s(data + outWidth * 3 * i, size, data + outWidth * 3 * (outHeight - i - 1), size);
			// copy tmp to row h-i-1
			memcpy_s(data + outWidth * 3 * (outHeight - i - 1), size, tmpBuffer, size);
		}
		delete[] tmpBuffer;
	}

	return data;
}
*/

// ------------------------------------------------------------------------------------------------
void CRDFModel::Clean()
{
	/*
	* Model
	*/
	if (m_iModel != 0)
	{
		CloseModel(m_iModel);
		m_iModel = 0;
	}	

	if (m_iCoordinateSystemModel != 0)
	{
		CloseModel(m_iCoordinateSystemModel);
		m_iCoordinateSystemModel = 0;
	}

	/*
	* RDF Classes
	*/
	map<int64_t, CRDFClass *>::iterator itRDFClasses = m_mapRDFClasses.begin();
	for (; itRDFClasses != m_mapRDFClasses.end(); itRDFClasses++)
	{
		delete itRDFClasses->second;
	}

	m_mapRDFClasses.clear();

	/*
	* RDF Properties
	*/
	map<int64_t, CRDFProperty *>::iterator itRDFProperties = m_mapRDFProperties.begin();
	for (; itRDFProperties != m_mapRDFProperties.end(); itRDFProperties++)
	{
		delete itRDFProperties->second;
	}

	m_mapRDFProperties.clear();

	/*
	* RDF Instances
	*/
	map<int64_t, CRDFInstance *>::iterator itRDFInstances = m_mapRDFInstances.begin();
	for (; itRDFInstances != m_mapRDFInstances.end(); itRDFInstances++)
	{
		delete itRDFInstances->second;
	}

	m_mapRDFInstances.clear();

	m_iID = 1;
}
