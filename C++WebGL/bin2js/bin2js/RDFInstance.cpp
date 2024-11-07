#include "stdafx.h"
#include "RDFInstance.h"
#include "Generic.h"

// ------------------------------------------------------------------------------------------------
CRDFInstance::CRDFInstance(int64_t iID, int64_t iModel, int64_t iInstance)
	: m_iID(iID)
	, m_iModel(iModel)
	, m_iInstance(iInstance)
	, m_strName(L"NA")
	, m_strUniqueName(L"")
	, m_strGUID(L"")
	, m_strGroup(L"")
	, m_strPropertyName(L"")
	, m_strPropertyDescription(L"")
	, m_strPropertyExpressID(L"")
	, m_iIndicesCount(0)
	, m_pIndices(NULL)
	, m_iVerticesCount(0)
	, m_pVertices(NULL)
	, m_iConceptualFacesCount(0)
	, m_vecTriangles()
	, m_vecLines()
	, m_vecPoints()
	, m_vecFacesPolygons()
	, m_vecConceptualFacesPolygons()
	, m_vecConceptualFacesMaterials()
	, m_vecBoundingBoxMin(NULL)
	, m_vecBoundingBoxMax(NULL)
	, m_mtxBoundingBoxTransformation(NULL)
	, m_bEnable(true)
	, m_bNeedsRefresh(false)
{
	assert(m_iModel != 0);
	assert(m_iInstance != 0);

	char * szClassName = NULL;
	GetNameOfClass(getClassInstance(), &szClassName);

	char * szName = NULL;
	GetNameOfInstance(m_iInstance, &szName);

	if (szName == NULL)
	{
		szName = szClassName;
	}

	{
		char ** arGUID = NULL;
		int64_t iGUIDCard = 0;
		GetDatatypeProperty(m_iInstance, GetPropertyByName(m_iModel, "name"), (void **)&arGUID, &iGUIDCard);

		if (iGUIDCard == 1)	{
			m_strGUID = CA2W(arGUID[0]);
		}
	}

	{
		char ** arGroup = NULL;
		int64_t iCard = 0;
		GetDatatypeProperty(m_iInstance, GetPropertyByName(m_iModel, "group"), (void **)&arGroup, &iCard);

		if (iCard == 1)	{
			m_strGroup = CA2W(arGroup[0]);
		}
	}

	{
		int64_t	* valuesObject = NULL,
				card = 0;
		GetObjectProperty(m_iInstance, GetPropertyByName(m_iModel, "object"), &valuesObject, &card);

		if (card == 1) {
			int64_t	* valuesRelationWithProperties = NULL,
					card = 0;
			GetObjectProperty(valuesObject[0], GetPropertyByName(m_iModel, "relationWithProperties"), &valuesRelationWithProperties, &card);

			if (card == 1) {
				{
					char	** valuesMyPropName = NULL;
					int64_t	card = 0;
					GetDatatypeProperty(valuesRelationWithProperties[0], GetPropertyByName(m_iModel, "myPropName"), (void**)&valuesMyPropName, &card);

					if (card == 1) {
						m_strPropertyName = CA2W(valuesMyPropName[0]);
					}
				}

				{
					char	** valuesMyPropDescription = NULL;
					int64_t	card = 0;
					GetDatatypeProperty(valuesRelationWithProperties[0], GetPropertyByName(m_iModel, "myPropDescription"), (void**)&valuesMyPropDescription, &card);

					if (card == 1) {
						m_strPropertyDescription = CA2W(valuesMyPropDescription[0]);
					}
				}

				{
					int64_t	* valuesMyExpressID = NULL;
							card = 0;
					GetDatatypeProperty(valuesRelationWithProperties[0], GetPropertyByName(m_iModel, "myExpressID"), (void**) &valuesMyExpressID, &card);

					if (card == 1) {
						wchar_t	* buff = new wchar_t[64];
						_i64tow_s(valuesMyExpressID[0], buff, 64, 10);

						m_strPropertyExpressID = buff;
					}
				}
			}
		}
	}

#ifndef _LINUX
    m_strName = CA2W(szName);

	LOG_DEBUG("*** INSTANCE " << m_strName.c_str());

	wchar_t szUniqueName[200];
	swprintf(szUniqueName, 200, L"%lld (%s)", m_iInstance, (LPWSTR)CA2W(szClassName));

	m_strUniqueName = szUniqueName;
#else
    m_strName = wxString(szName).wchar_str();

    wchar_t szUniqueName[200];
	swprintf(szUniqueName, 200, L"%lld (%ls)", m_iInstance, wxString(szClassName).wc_str());

	m_strUniqueName = szUniqueName;
#endif // _LINUX

	Calculate();
}

// ------------------------------------------------------------------------------------------------
CRDFInstance::~CRDFInstance()
{
	Clean();

	delete m_mtxBoundingBoxTransformation;
	m_mtxBoundingBoxTransformation = NULL;
	delete m_vecBoundingBoxMin;
	m_vecBoundingBoxMin = NULL;
	delete m_vecBoundingBoxMax;
	m_vecBoundingBoxMax = NULL;
}

// ------------------------------------------------------------------------------------------------
void CRDFInstance::Recalculate()
{	
	if (!m_bEnable)
	{
		// Reloading on demand
		m_bNeedsRefresh = true;

		return;
	}

	Clean();

	Calculate();
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::getID() const
{
	return m_iID;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::GetModel() const
{
	return m_iModel;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::getInstance() const
{
	return m_iInstance;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::getClassInstance() const
{
	int64_t iClassInstance = GetInstanceClass(getInstance());
	assert(iClassInstance != 0);

	return iClassInstance;
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getName() const
{
	return m_strName.c_str();
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getUniqueName() const
{
	return m_strUniqueName.c_str();
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getGUID() const
{
	return m_strGUID.c_str();
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getGroup() const
{
	return m_strGroup.c_str();
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getPropertyName() const
{
	return m_strPropertyName.c_str();
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getPropertyDescription() const
{
	return m_strPropertyDescription.c_str();
}

// ------------------------------------------------------------------------------------------------
const wchar_t * CRDFInstance::getPropertyExpressID() const
{
	return m_strPropertyExpressID.c_str();
}

// ------------------------------------------------------------------------------------------------
bool CRDFInstance::isReferenced() const
{
	return GetInstanceInverseReferencesByIterator(m_iInstance, 0) > 0;
}

// ------------------------------------------------------------------------------------------------
bool CRDFInstance::hasGeometry() const
{
	return (m_iVerticesCount > 0) && (m_iIndicesCount > 0);
}

// ------------------------------------------------------------------------------------------------
int32_t * CRDFInstance::getIndices() const
{
	return m_pIndices;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::getIndicesCount() const
{
	return m_iIndicesCount;
}

// ------------------------------------------------------------------------------------------------
float * CRDFInstance::getVertices() const
{
	return m_pVertices;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::getVerticesCount() const
{
	return m_iVerticesCount;
}

// ------------------------------------------------------------------------------------------------
int64_t CRDFInstance::getConceptualFacesCount() const
{
	return m_iConceptualFacesCount;
}

// ------------------------------------------------------------------------------------------------
const vector<pair<int64_t, int64_t> > & CRDFInstance::getTriangles() const
{
	return m_vecTriangles;
}

// ------------------------------------------------------------------------------------------------
const vector<pair<int64_t, int64_t> > & CRDFInstance::getLines() const
{
	return m_vecLines;
}

// ------------------------------------------------------------------------------------------------
const vector<pair<int64_t, int64_t> > & CRDFInstance::getPoints() const
{
	return m_vecPoints;
}

// ------------------------------------------------------------------------------------------------
const vector<pair<int64_t, int64_t> > & CRDFInstance::getFacesPolygons() const
{
	return m_vecFacesPolygons;
}

// ------------------------------------------------------------------------------------------------
const vector<pair<int64_t, int64_t> > & CRDFInstance::getConceptualFacesPolygons() const
{
	return m_vecConceptualFacesPolygons;
}

// ------------------------------------------------------------------------------------------------
const vector<CRDFMaterial *> & CRDFInstance::getConceptualFacesMaterials() const
{
	return m_vecConceptualFacesMaterials;
}

// ------------------------------------------------------------------------------------------------
bool CRDFInstance::hasTexture() const
{
	for (size_t iMaterial = 0; iMaterial < m_vecConceptualFacesMaterials.size(); iMaterial++)
	{
		if (m_vecConceptualFacesMaterials[iMaterial]->hasTexture())
		{
			return true;
		}
	}

	return false;
}

// ------------------------------------------------------------------------------------------------
VECTOR3 * CRDFInstance::getBoundingBoxMin() const
{
	return m_vecBoundingBoxMin;
}

// ------------------------------------------------------------------------------------------------
VECTOR3 * CRDFInstance::getBoundingBoxMax() const
{
	return m_vecBoundingBoxMax;
}

// ------------------------------------------------------------------------------------------------
MATRIX * CRDFInstance::getBoundingBoxTransformation() const
{
	return m_mtxBoundingBoxTransformation;
}

// ------------------------------------------------------------------------------------------------
void CRDFInstance::setEnable(bool bEnable)
{
	m_bEnable = bEnable;

	if (m_bEnable && m_bNeedsRefresh)
	{
		m_bNeedsRefresh = false;

		Recalculate();
	}
}

// ------------------------------------------------------------------------------------------------
bool CRDFInstance::getEnable() const
{
	return m_bEnable;
}

// ------------------------------------------------------------------------------------------------
void CRDFInstance::CalculateMinMax(float & fXmin, float & fXmax, float & fYmin, float & fYmax, float & fZmin, float & fZmax)
{
	if (m_iVerticesCount == 0)
	{
		return;
	}

	/*
	* Triangles
	*/
	if (!m_vecTriangles.empty())
	{
		for (size_t iTriangle = 0; iTriangle < m_vecTriangles.size(); iTriangle++)
		{
			for (int64_t iIndex = m_vecTriangles[iTriangle].first; iIndex < m_vecTriangles[iTriangle].first + m_vecTriangles[iTriangle].second; iIndex++)
			{
				fXmin = min(fXmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fXmax = max(fXmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fYmin = min(fYmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fYmax = max(fYmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fZmin = min(fZmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
				fZmax = max(fZmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
			} // for (size_t iIndex = ...
		} // for (size_t iTriangle = ...
	} // if (!m_vecTriangles.empty())

	// DISABLED
	/*
	* Faces polygons
	*/
	//if (!m_vecFacesPolygons.empty())
	//{
	//	for (size_t iPolygon = 0; iPolygon < m_vecFacesPolygons.size(); iPolygon++)
	//	{
	//		for (int64_t iIndex = m_vecFacesPolygons[iPolygon].first; iIndex < m_vecFacesPolygons[iPolygon].first + m_vecFacesPolygons[iPolygon].second; iIndex++)
	//		{
	//			if ((getIndices()[iIndex] == -1) || (getIndices()[iIndex] == -2))
	//			{
	//				continue;
	//			}

	//			fXmin = min(fXmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
	//			fXmax = max(fXmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
	//			fYmin = min(fYmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
	//			fYmax = max(fYmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
	//			fZmin = min(fZmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
	//			fZmax = max(fZmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
	//		} // for (size_t iIndex = ...
	//	} // for (size_t iPolygon = ...
	//} // if (!m_vecFacesPolygons.empty())

	/*
	* Conceptual faces polygons
	*/
	if (!m_vecConceptualFacesPolygons.empty())
	{
		for (size_t iPolygon = 0; iPolygon < m_vecConceptualFacesPolygons.size(); iPolygon++)
		{
			for (int64_t iIndex = m_vecConceptualFacesPolygons[iPolygon].first; iIndex < m_vecConceptualFacesPolygons[iPolygon].first + m_vecConceptualFacesPolygons[iPolygon].second; iIndex++)
			{
				if ((getIndices()[iIndex] == -1) || (getIndices()[iIndex] == -2))
				{
					continue;
				}

				fXmin = min(fXmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fXmax = max(fXmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fYmin = min(fYmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fYmax = max(fYmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fZmin = min(fZmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
				fZmax = max(fZmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
			} // for (size_t iIndex = ...
		} // for (size_t iPolygon = ...
	} // if (!m_vecConceptualFacesPolygons.empty())

	/*
	* Lines
	*/
	if (!m_vecLines.empty())
	{
		for (size_t iPolygon = 0; iPolygon < m_vecLines.size(); iPolygon++)
		{
			for (int64_t iIndex = m_vecLines[iPolygon].first; iIndex < m_vecLines[iPolygon].first + m_vecLines[iPolygon].second; iIndex++)
			{
				if (getIndices()[iIndex] == -1)
				{
					continue;
				}

				fXmin = min(fXmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fXmax = max(fXmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fYmin = min(fYmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fYmax = max(fYmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fZmin = min(fZmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
				fZmax = max(fZmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
			} // for (size_t iIndex = ...
		} // for (size_t iPolygon = ...
	} // if (!m_vecLines.empty())

	/*
	* Points
	*/
	if (!m_vecPoints.empty())
	{
		for (size_t iPolygon = 0; iPolygon < m_vecPoints.size(); iPolygon++)
		{
			for (int64_t iIndex = m_vecPoints[iPolygon].first; iIndex < m_vecPoints[iPolygon].first + m_vecPoints[iPolygon].second; iIndex++)
			{
				fXmin = min(fXmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fXmax = max(fXmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH)]);
				fYmin = min(fYmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fYmax = max(fYmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 1]);
				fZmin = min(fZmin, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
				fZmax = max(fZmax, getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 2]);
			} // for (size_t iIndex = ...
		} // for (size_t iPolygon = ...
	} // if (!m_vecPoints.empty())
}

// ------------------------------------------------------------------------------------------------
void CRDFInstance::Center(float fXmin, float fXmax, float fYmin, float fYmax, float fZmin, float fZmax)
{
	for (int64_t iVertex = 0; iVertex < m_iVerticesCount; iVertex++)
	{
		m_pVertices[(iVertex * VERTEX_LENGTH)] -= fXmin;
		m_pVertices[(iVertex * VERTEX_LENGTH) + 1] -= fYmin;
		m_pVertices[(iVertex * VERTEX_LENGTH) + 2] -= fZmin;

		m_pVertices[(iVertex * VERTEX_LENGTH)] -= (fXmax - fXmin) / 2.f;
		m_pVertices[(iVertex * VERTEX_LENGTH) + 1] -= (fYmax - fYmin) / 2.f;
		m_pVertices[(iVertex * VERTEX_LENGTH) + 2] -= (fZmax - fZmin) / 2.f;
	} // for (int64_t iVertex = ...
}

// ------------------------------------------------------------------------------------------------
void CRDFInstance::Calculate()
{
	/*
	* Retrieves the size of index/vertex buffers
	*/
	CalculateInstance(m_iInstance, &m_iVerticesCount, &m_iIndicesCount, NULL);

	/*
	* Retrieves the index/vertex buffers
	*/
	if ((m_iVerticesCount != 0) && (m_iIndicesCount != 0))
	{
		/*
		* Retrieves the indices
		*/
		m_pIndices = new int32_t[m_iIndicesCount];
		memset(m_pIndices, 0, m_iIndicesCount * sizeof(int32_t));

		UpdateInstanceIndexBuffer(m_iInstance, m_pIndices);

		/*
		* Retrieves the vertices
		*/
		m_pVertices = new float[m_iVerticesCount * VERTEX_LENGTH];
		memset(m_pVertices, 0, m_iVerticesCount * VERTEX_LENGTH * sizeof(float));

		UpdateInstanceVertexBuffer(m_iInstance, m_pVertices);
		
		m_iConceptualFacesCount = GetConceptualFaceCnt(m_iInstance);
		for (int64_t iConceptualFace = 0; iConceptualFace < m_iConceptualFacesCount; iConceptualFace++)
		{
			int64_t iStartIndexTriangles = 0;
			int64_t iIndicesCountTriangles = 0;
			int64_t iStartIndexLines = 0;
			int64_t iLinesIndicesCount = 0;
			int64_t iStartIndexPoints = 0;
			int64_t iPointsIndicesCount = 0;
			int64_t iStartIndexFacesPolygons = 0;
			int64_t iIndicesCountPolygonFaces = 0;
			int64_t iStartIndexConceptualFacePolygons = 0;
			int64_t iConceptualFacePolygonsIndicesCount = 0;

			GetConceptualFace(m_iInstance, iConceptualFace,
				&iStartIndexTriangles, &iIndicesCountTriangles,
				&iStartIndexLines, &iLinesIndicesCount,
				&iStartIndexPoints, &iPointsIndicesCount,
				&iStartIndexFacesPolygons, &iIndicesCountPolygonFaces,
				&iStartIndexConceptualFacePolygons, &iConceptualFacePolygonsIndicesCount);

			CRDFMaterial * pMaterial = 0;

			if (iIndicesCountTriangles > 0) 
			{
				/*
				* Material
				*/
				int32_t iIndexValue = *(m_pIndices + iStartIndexTriangles);
				iIndexValue *= VERTEX_LENGTH;

				float fColor = *(m_pVertices + iIndexValue + 8);
				unsigned int iAmbientColor = *(reinterpret_cast<unsigned int *>(&fColor));
				float fTransparency = (float)(iAmbientColor & (255)) / (float)255;

				fColor = *(m_pVertices + iIndexValue + 9);
				unsigned int iDiffuseColor = *(reinterpret_cast<unsigned int *>(&fColor));

				fColor = *(m_pVertices + iIndexValue + 10);
				unsigned int iEmissiveColor = *(reinterpret_cast<unsigned int *>(&fColor));

				fColor = *(m_pVertices + iIndexValue + 11);
				unsigned int iSpecularColor = *(reinterpret_cast<unsigned int *>(&fColor));

				pMaterial = new CRDFMaterial(iAmbientColor, iDiffuseColor, iEmissiveColor, iSpecularColor, fTransparency);

				/*
				* Check for a texture
				*/
				bool bHasTexture = false;
				for (int64_t iIndex = iStartIndexTriangles; iIndex < iStartIndexTriangles + iIndicesCountTriangles; iIndex++)
				{
					if ((getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 6] != 0.f) || (getVertices()[(getIndices()[iIndex] * VERTEX_LENGTH) + 7] != 0.f))
					{
						bHasTexture = true;

						break;
					}
				} // for (size_t iIndex = ...

				if (bHasTexture)
				{
					pMaterial->setTexture(L"");

					const int64_t MATERIAL_PROPERTY = GetPropertyByName(m_iModel, "material");
					const int64_t OBJECTS_PROPERTY = GetPropertyByName(m_iModel, "objects");
					const int64_t TEXTURES_PROPERTY = GetPropertyByName(m_iModel, "textures");
					const int64_t NAME_PROPERTY = GetPropertyByName(m_iModel, "name");

					int64_t * piInstances = NULL;
					int64_t iCard = 0;
					// GetObjectTypeProperty(m_iInstance, OBJECTS_PROPERTY, &piInstances, &iCard);
					// if (iCard == 1)
					// {
					// int64_t iObjectPropertyInstance = piInstances[0];
					// GetObjectTypeProperty(iObjectPropertyInstance, MATERIAL_PROPERTY, &piInstances, &iCard);
					GetObjectProperty(m_iInstance, MATERIAL_PROPERTY, &piInstances, &iCard);
					if (iCard == 1)
					{
						int64_t iMaterialPropertyInstance = piInstances[0];
						GetObjectProperty(iMaterialPropertyInstance, TEXTURES_PROPERTY, &piInstances, &iCard);
						if (iCard == 1)
						{
							int64_t iTexturePropertyInstance = piInstances[0];

							char ** arNames = NULL;
							GetDatatypeProperty(iTexturePropertyInstance, NAME_PROPERTY, (void **)&arNames, &iCard);
							if (iCard == 1)
							{
								pMaterial->setTexture(wstring(CA2W(arNames[0])));
							}
						} // TEXTURES_PROPERTY
					} // MATERIAL_PROPERTY
					// } // OBJECTS_PROPERTY				
				} // if (bHasTexture)
			} // if (iIndicesCountTriangles > 0)

			if (iIndicesCountTriangles > 0)
			{
				if (iIndicesCountTriangles > MAX_INDICES_COUNT)
				{
					while (iIndicesCountTriangles > MAX_INDICES_COUNT)
					{
						m_vecTriangles.push_back(pair<int64_t, int64_t>(iStartIndexTriangles, MAX_INDICES_COUNT));
						m_vecConceptualFacesMaterials.push_back(new CRDFMaterial(*pMaterial));

						iIndicesCountTriangles -= MAX_INDICES_COUNT;
						iStartIndexTriangles += MAX_INDICES_COUNT;
					}

					if (iIndicesCountTriangles > 0)
					{
						m_vecTriangles.push_back(pair<int64_t, int64_t>(iStartIndexTriangles, iIndicesCountTriangles));
						m_vecConceptualFacesMaterials.push_back(new CRDFMaterial(*pMaterial));
					}

					delete pMaterial;
				} // if (iIndicesCountTriangles > MAX_INDICES_COUNT)							
				else
				{
					m_vecTriangles.push_back(pair<int64_t, int64_t>(iStartIndexTriangles, iIndicesCountTriangles));
					m_vecConceptualFacesMaterials.push_back(pMaterial);
				}
			} // if (iIndicesCountTriangles > 0)		

			if (iLinesIndicesCount > 0)
			{
				m_vecLines.push_back(pair<int64_t, int64_t>(iStartIndexLines, iLinesIndicesCount));
			}

			if (iPointsIndicesCount > 0)
			{
				m_vecPoints.push_back(pair<int64_t, int64_t>(iStartIndexPoints, iPointsIndicesCount));
			}

			if (iIndicesCountPolygonFaces > 0)
			{
				m_vecFacesPolygons.push_back(pair<int64_t, int64_t>(iStartIndexFacesPolygons, iIndicesCountPolygonFaces));
			}

			if (iConceptualFacePolygonsIndicesCount > 0)
			{
				m_vecConceptualFacesPolygons.push_back(pair<int64_t, int64_t>(iStartIndexConceptualFacePolygons, iConceptualFacePolygonsIndicesCount));
			}
		} // for (int64_t iConceptualFace = ...

		/*
		* Bounding box
		*/
		if (m_mtxBoundingBoxTransformation == NULL)
		{
			m_mtxBoundingBoxTransformation = new MATRIX();
			m_vecBoundingBoxMin = new VECTOR3();
			m_vecBoundingBoxMax = new VECTOR3();

			SetBoundingBoxReference(m_iInstance, (double*)m_mtxBoundingBoxTransformation, (double *)m_vecBoundingBoxMin, (double*)m_vecBoundingBoxMax);
		}
	} // if ((m_iVerticesCount != 0) && ...
}

// ------------------------------------------------------------------------------------------------
void CRDFInstance::Clean()
{
	delete[] m_pIndices;
	m_pIndices = NULL;
	m_iIndicesCount = 0;

	delete[] m_pVertices;
	m_pVertices = NULL;
	m_iVerticesCount = NULL;

	m_vecTriangles.clear();
	m_vecLines.clear();
	m_vecPoints.clear();
	m_vecFacesPolygons.clear();
	m_vecConceptualFacesPolygons.clear();

	for (size_t iMaterial = 0; iMaterial < m_vecConceptualFacesMaterials.size(); iMaterial++)
	{
		delete m_vecConceptualFacesMaterials[iMaterial];
	}

	m_vecConceptualFacesMaterials.clear();
}
