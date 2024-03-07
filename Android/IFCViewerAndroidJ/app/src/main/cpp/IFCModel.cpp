#include "IFCModel.h"

#include "engdef.h"
#include "engine.h"
#include "ifcengine.h"

#include "common.h"

#include <cwchar>
#include <cfloat>
#include <math.h>

#include <string>

using namespace std;

//#include "ConceptualFace.h"

// ------------------------------------------------------------------------------------------------
/*static*/ int_t CIFCModel::s_iObjectID = 1;

// ------------------------------------------------------------------------------------------------
CIFCModel::CIFCModel(const string & strIFCFile)
	: m_strIFCFile(strIFCFile)
	, m_iIFCModel(0)
	, m_prXMinMax(pair<float, float>(-1.f, 1.f))
	, m_prYMinMax(pair<float, float>(-1.f, 1.f))
	, m_prZMinMax(pair<float, float>(-1.f, 1.f))
	, m_fBoundingSphereDiameter(2.f)
	//, m_vecIFCObjects()
	, m_iIFCObjectCount(0)
	, m_ifcSpaceEntity(0)
	, m_ifcOpeningElementEntity(0)
	, m_ifcDistributionElementEntity(0)
	, m_ifcElectricalElementEntity(0)
	, m_ifcElementAssemblyEntity(0)
	, m_ifcElementComponentEntity(0)
	, m_ifcEquipmentElementEntity(0)
	, m_ifcFeatureElementEntity(0)
	, m_ifcFeatureElementSubtractionEntity(0)
	, m_ifcFurnishingElementEntity(0)
	, m_ifcReinforcingElementEntity(0)
	, m_ifcTransportElementEntity(0)
	, m_ifcVirtualElementEntity(0)
{	
}

// ------------------------------------------------------------------------------------------------
CIFCModel::~CIFCModel()
{
	if (m_iIFCModel != 0)
	{
		sdaiCloseModel(m_iIFCModel);
		m_iIFCModel = 0;
	}	

	/*for (size_t iIFCObject = 0; iIFCObject < m_vecIFCObjects.size(); iIFCObject++)
	{
		delete m_vecIFCObjects[iIFCObject];
	}
	m_vecIFCObjects.clear();*/
}

// ------------------------------------------------------------------------------------------------
bool CIFCModel::Load()
{
    //setStringUnicode(1);

	/*
	* Load
	*/
	m_iIFCModel = sdaiOpenModelBNUnicode(0, (char *)m_strIFCFile.c_str(), NULL);
	if (m_iIFCModel == 0)
    {
		LOGE("Can't load IFC model: '%ls'", m_strIFCFile.c_str());

        return false;
    }

	/*
	* Detect the schema
	*/
	wchar_t * szSchema = NULL;
	GetSPFFHeaderItem(m_iIFCModel, 9, 0, sdaiUNICODE, (char **)&szSchema);

	wstring strSchema = szSchema;
	if (strSchema.find(L"IFC2") != -1)
	{
		setFilter(0, 2097152, 1048576 + 2097152 + 4194304);
	}
	else
	{
		if ((strSchema.find(L"IFC4x") != -1) || (strSchema.find(L"IFC4X") != -1))
		{
			setFilter(0, 1048576 + 2097152, 1048576 + 2097152 + 4194304);
		}
		else
		{
			if ((strSchema.find(L"IFC4") != -1) ||
				(strSchema.find(L"IFC2x4") != -1) ||
				(strSchema.find(L"IFC2X4") != -1))
			{
				setFilter(0, 1048576, 1048576 + 2097152 + 4194304);
			}
			else
			{
                LOGE("Unknown schema.");

				return false;
			}
		}
	}

	/*
	* Apply the schema
	*/
	sdaiCloseModel(m_iIFCModel);

	m_iIFCModel = sdaiOpenModelBNUnicode(0, (void *)m_strIFCFile.c_str(), (void *)"/storage/emulated/0/Download/IFC2X3_TC1.exp");

	if (m_iIFCModel == 0)
	{
        LOGE("Can't load IFC model: '%ls'", m_strIFCFile.c_str());

		return false;
	}

	setBRepProperties(
			m_iIFCModel,
			1 + 2 + 4 + 32,	// + 64,
			0.92,
			0.000001,
			600
		);

	/*
	* Entities
	*/
	int_t ifcObjectEntity = sdaiGetEntity(m_iIFCModel, "IFCOBJECT");
	int_t ifcProductEntity = sdaiGetEntity(m_iIFCModel, "IFCPRODUCT");
	m_ifcProjectEntity = sdaiGetEntity(m_iIFCModel, "IFCPROJECT");
	m_ifcSpaceEntity = sdaiGetEntity(m_iIFCModel, "IFCSPACE");
	m_ifcOpeningElementEntity = sdaiGetEntity(m_iIFCModel, "IFCOPENINGELEMENT");
	m_ifcDistributionElementEntity = sdaiGetEntity(m_iIFCModel, "IFCDISTRIBUTIONELEMENT");
	m_ifcElectricalElementEntity = sdaiGetEntity(m_iIFCModel, "IFCELECTRICALELEMENT");
	m_ifcElementAssemblyEntity = sdaiGetEntity(m_iIFCModel, "IFCELEMENTASSEMBLY");
	m_ifcElementComponentEntity = sdaiGetEntity(m_iIFCModel, "IFCELEMENTCOMPONENT");
	m_ifcEquipmentElementEntity = sdaiGetEntity(m_iIFCModel, "IFCEQUIPMENTELEMENT");
	m_ifcFeatureElementEntity = sdaiGetEntity(m_iIFCModel, "IFCFEATUREELEMENT");
	m_ifcFeatureElementSubtractionEntity = sdaiGetEntity(m_iIFCModel, "IFCFEATUREELEMENTSUBTRACTION");
	m_ifcFurnishingElementEntity = sdaiGetEntity(m_iIFCModel, "IFCFURNISHINGELEMENT");
	m_ifcReinforcingElementEntity = sdaiGetEntity(m_iIFCModel, "IFCREINFORCINGELEMENT");
	m_ifcTransportElementEntity = sdaiGetEntity(m_iIFCModel, "IFCTRANSPORTELEMENT");
	m_ifcVirtualElementEntity = sdaiGetEntity(m_iIFCModel, "IFCVIRTUALELEMENT");

	/*
	* Retrieve the objects recursively
	*/
///	RetrieveObjects__depth(ifcProductEntity, DEFAULT_CIRCLE_SEGMENTS, 0);
	RetrieveObjects__depth(ifcObjectEntity, DEFAULT_CIRCLE_SEGMENTS, 0);
	RetrieveObjects("IFCRELSPACEBOUNDARY", L"IFCRELSPACEBOUNDARY", DEFAULT_CIRCLE_SEGMENTS);

	/*
	* Min/Max
	*/
	float fXmin = FLT_MAX;
	float fXmax = -FLT_MAX;
	float fYmin = FLT_MAX;
	float fYmax = -FLT_MAX;
	float fZmin = FLT_MAX;
	float fZmax = -FLT_MAX;

	/*
	for (size_t iIFCObject = 0; iIFCObject < m_vecIFCObjects.size(); iIFCObject++)
	{
		CIFCObject * pIFCObject = m_vecIFCObjects[iIFCObject];

		if (!pIFCObject->hasGeometry())
		{
			// skip the objects without geometry
			continue;
		}

		pIFCObject->CalculateMinMaxValues(fXmin, fXmax, fYmin, fYmax, fZmin, fZmax);
	}*/

	m_prXMinMax = pair<float, float>(fXmin, fXmax);
	m_prYMinMax = pair<float, float>(fYmin, fYmax);
	m_prZMinMax = pair<float, float>(fZmin, fZmax);

	m_fBoundingSphereDiameter = fXmax - fXmin;
	m_fBoundingSphereDiameter = fmax(m_fBoundingSphereDiameter, fYmax - fYmin);
	m_fBoundingSphereDiameter = fmax(m_fBoundingSphereDiameter, fZmax - fZmin);

    return true;
}

// ------------------------------------------------------------------------------------------------
int_t CIFCModel::getModel() const
{
	return m_iIFCModel;
}

// ------------------------------------------------------------------------------------------------
const char * CIFCModel::getModelName() const
{
	return m_strIFCFile.c_str();
}

// ------------------------------------------------------------------------------------------------
const pair<float, float> & CIFCModel::getXMinMax() const
{
	return m_prXMinMax;
}

// ------------------------------------------------------------------------------------------------
const pair<float, float> & CIFCModel::getYMinMax() const
{
	return m_prYMinMax;
}

// ------------------------------------------------------------------------------------------------
const pair<float, float> & CIFCModel::getZMinMax() const
{
	return m_prZMinMax;
}

// ------------------------------------------------------------------------------------------------
float CIFCModel::getBoundingSphereDiameter() const
{
	return m_fBoundingSphereDiameter;
}

// ------------------------------------------------------------------------------------------------
/*
const vector<CIFCObject *> & CIFCModel::getIFCObjects()
{
	return m_vecIFCObjects;
}
*/

// ------------------------------------------------------------------------------------------------
void CIFCModel::RetrieveObjects(const char * szEntityName, const wchar_t * szEntityNameW, int_t iCircleSegements)
{
	int_t * iIFCObjectInstances = sdaiGetEntityExtentBN(m_iIFCModel, (char *) szEntityName);

	int_t iIFCObjectInstancesCount = sdaiGetMemberCount(iIFCObjectInstances);
	if (iIFCObjectInstancesCount == 0)
    {
        return;
    }	

	for (int_t i = 0; i < iIFCObjectInstancesCount; ++i)
	{
		int_t iInstance = 0;
		engiGetAggrElement(iIFCObjectInstances, i, sdaiINSTANCE, &iInstance);

		wchar_t	* szInstanceGUIDW = nullptr;
		sdaiGetAttrBN(iInstance, "GlobalId", sdaiUNICODE, &szInstanceGUIDW);

		/*
		CIFCObject * pIFCObject = RetrieveGeometry(szInstanceGUIDW, szEntityNameW, iInstance, iCircleSegements);
		pIFCObject->ID() = s_iObjectID++;
		
		m_vecIFCObjects.push_back(pIFCObject);
		 */
		// TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!
        m_iIFCObjectCount++;
	}
}

// ------------------------------------------------------------------------------------------------
void CIFCModel::RetrieveObjects__depth(int_t iParentEntity, int_t iCircleSegments, int_t depth)
{
	if ((iParentEntity == m_ifcDistributionElementEntity) ||
		(iParentEntity == m_ifcElectricalElementEntity) ||
		(iParentEntity == m_ifcElementAssemblyEntity) ||
		(iParentEntity == m_ifcElementComponentEntity) ||
		(iParentEntity == m_ifcEquipmentElementEntity) ||
		(iParentEntity == m_ifcFeatureElementEntity) ||
		(iParentEntity == m_ifcFurnishingElementEntity) ||
		(iParentEntity == m_ifcTransportElementEntity) ||
		(iParentEntity == m_ifcVirtualElementEntity))
	{
		iCircleSegments = 12;
	}

	if (iParentEntity == m_ifcReinforcingElementEntity)
	{
		iCircleSegments = 6;
	}

	int_t* ifcObjectInstances = sdaiGetEntityExtent(m_iIFCModel, iParentEntity);
	int_t noIfcObjectIntances = sdaiGetMemberCount(ifcObjectInstances);

	if (noIfcObjectIntances != 0)
	{
		char	* szParenEntityName = NULL;
		engiGetEntityName(iParentEntity, sdaiSTRING, &szParenEntityName);

		wchar_t	* szParentEntityNameW = NULL;
		engiGetEntityName(iParentEntity, sdaiUNICODE, (char **)&szParentEntityNameW);

		RetrieveObjects(szParenEntityName, szParentEntityNameW, iCircleSegments);

		if (iParentEntity == m_ifcProjectEntity) {
			for (int_t i = 0; i < noIfcObjectIntances; i++) {
				int_t ifcObjectInstance = 0;
				engiGetAggrElement(ifcObjectInstances, i, sdaiINSTANCE, &ifcObjectInstance);

				wchar_t	* szInstanceGUIDW = nullptr;
				sdaiGetAttrBN(ifcObjectInstance, "GlobalId", sdaiUNICODE, &szInstanceGUIDW);

				//CIFCObject * pIFCObject = RetrieveGeometry(szInstanceGUIDW, szParentEntityNameW, ifcObjectInstance, iCircleSegments);
				//pIFCObject->ID() = s_iObjectID++;
			}
		}
	} // if (noIfcObjectIntances != 0)

	noIfcObjectIntances = engiGetEntityCount(m_iIFCModel);
	for (int_t i = 0; i < noIfcObjectIntances; i++)
	{
		int_t ifcEntity = engiGetEntityElement(m_iIFCModel, i);
		if (engiGetEntityParent(ifcEntity) == iParentEntity)
		{
			RetrieveObjects__depth(ifcEntity, iCircleSegments, depth + 1);
		}
	}
}

// ------------------------------------------------------------------------------------------------
/*
CIFCObject * CIFCModel::RetrieveGeometry(const wchar_t * szInstanceGUIDW, const wchar_t * szEntityNameW, int_t iInstance, int_t iCircleSegments)
{
	CIFCObject * pIFCObject = new CIFCObject(iInstance, szInstanceGUIDW, szEntityNameW);

	/
	* Set up format
	/
	int_t setting = 0, mask = 0;
	mask += flagbit2;        // PRECISION (32/64 bit)
	mask += flagbit3;        //	INDEX ARRAY (32/64 bit)
	mask += flagbit5;        // NORMALS
	mask += flagbit6;        // TEXTURE
	mask += flagbit8;        // TRIANGLES
	mask += flagbit9;        // LINES
	mask += flagbit10;       // POINTS
	mask += flagbit12;       // WIREFRAME
	mask += flagbit17;       // OPENGL
	mask += flagbit24;		 //	AMBIENT
	mask += flagbit25;		 //	DIFFUSE
	mask += flagbit26;		 //	EMISSIVE
	mask += flagbit27;		 //	SPECULAR

	setting += 0;		     // SINGLE PRECISION (float)
	setting += 0;            // 32 BIT INDEX ARRAY (Int32)
	setting += flagbit5;     // NORMALS ON
	setting += 0;			 // TEXTURE OFF
	setting += flagbit8;     // TRIANGLES ON
	setting += 9;            // LINES ON
	setting += 10;           // POINTS ON
	setting += flagbit12;    // WIREFRAME ON
	setting += flagbit17;    // OPENGL
	setting += flagbit24;	 //	AMBIENT
	setting += flagbit25;	 //	DIFFUSE
	setting += flagbit26;	 //	EMISSIVE
	setting += flagbit27;	 //	SPECULAR
	setFormat(m_iIFCModel, setting, mask);
	setFilter(m_iIFCModel, flagbit1, flagbit1);

	/
	* Set up circleSegments()
	/
	if (iCircleSegments != DEFAULT_CIRCLE_SEGMENTS)
	{
		circleSegments(iCircleSegments, 5);
	}

	int64_t iVerticesCount = 0;
	int64_t iIndicesCount = 0;
	CalculateInstance(iInstance, &iVerticesCount, &iIndicesCount, 0);
	if ((iVerticesCount > 0) && (iIndicesCount > 0))
	{
		int64_t iOWLModel = 0;
		owlGetModel(m_iIFCModel, &iOWLModel);

		int64_t iOWLInstance = 0;
		owlGetInstance(m_iIFCModel, iInstance, &iOWLInstance);		

		pIFCObject->verticesCount() = (int_t)iVerticesCount;


		// TODO
		// ((R * 255 + G) * 255 + B) * 255 + A
		/
		uint32_t R = 10,
			G = 200,
			B = 10,
			A = 255;
		uint32_t iDefaultColor = 256 * 256 * 256 * R +
			256 * 256 * G +
			256 * B +
			A;		
		SetDefaultColor(m_iIFCModel, iDefaultColor, iDefaultColor, iDefaultColor, iDefaultColor);
		/

		// X, Y, Z, Nx, Ny, Nz, Ambient, Diffuse, Emissive, Specular		
		const int_t VERTEX_LENGTH = 10;
		
		float * pVertices = new float[(int_t) iVerticesCount * VERTEX_LENGTH];
		UpdateInstanceVertexBuffer(iOWLInstance, pVertices);

		int32_t * pIndices = new int32_t[(int_t) iIndicesCount];
		UpdateInstanceIndexBuffer(iOWLInstance, pIndices);

		/
		* Volume
		/
		double dVolume = GetVolume(iOWLInstance, pVertices, pIndices);
		pIFCObject->volume() = abs(dVolume);
		
		// MATERIAL : FACE INDEX, START INDEX, INIDCES COUNT, etc.
		map<CIFCMaterial, vector<CConceptualFace>, CIFCMaterialComparator> mapMaterial2ConceptualFaces;
		vector<pair<int_t, int_t>> vecWireframesIndices;
		vector<pair<int_t, int_t>> vecLinesIndices;
		vector<pair<int_t, int_t>> vecPointsIndices;		

		/
		* Extract the conceptual face - triangles and polygons
		/
		int_t iFacesCount = getConceptualFaceCnt(iInstance);

		pIFCObject->conceptualFacesCount() = iFacesCount;

		for (int_t iFace = 0; iFace < iFacesCount; iFace++)
		{
			int_t iStartIndexTriangles = 0;
			int_t iIndicesCountTriangles = 0;

			int_t iStartIndexLines = 0;
			int_t iIndicesCountLines = 0;

			int_t iStartIndexPoints = 0;
			int_t iIndicesCountPoints = 0;

			int_t iStartIndexFacesPolygons = 0;
			int_t iIndicesCountFacesPolygons = 0;

			int64_t	conceptualFace = getConceptualFaceEx(
				iInstance, iFace,
				&iStartIndexTriangles, &iIndicesCountTriangles,
				&iStartIndexLines, &iIndicesCountLines,
				&iStartIndexPoints, &iIndicesCountPoints,
				&iStartIndexFacesPolygons, &iIndicesCountFacesPolygons,
				0, 0);		

			/
			* Area
			/
			double dArea = GetConceptualFaceArea(conceptualFace, pVertices, pIndices);
			pIFCObject->conceptualFacesArea().push_back(dArea);			

			if (iIndicesCountTriangles > 0)
			{
				/
				* Material
				/
				CIFCMaterial * pMaterial = NULL;

				int32_t iIndexValue = *(pIndices + iStartIndexTriangles);
				iIndexValue *= VERTEX_LENGTH;

				float fColor = *(pVertices + iIndexValue + 6);
				unsigned int iAmbientColor = *(reinterpret_cast<unsigned int *>(&fColor));
				float fTransparency = (float)(iAmbientColor & (255)) / (float)255;

				fColor = *(pVertices + iIndexValue + 7);
				unsigned int iDiffuseColor = *(reinterpret_cast<unsigned int *>(&fColor));

				fColor = *(pVertices + iIndexValue + 8);
				unsigned int iEmissiveColor = *(reinterpret_cast<unsigned int *>(&fColor));

				fColor = *(pVertices + iIndexValue + 9);
				unsigned int iSpecularColor = *(reinterpret_cast<unsigned int *>(&fColor));

				pMaterial = new CIFCMaterial(iAmbientColor, iDiffuseColor, iEmissiveColor, iSpecularColor, fTransparency);

				map<CIFCMaterial, vector<CConceptualFace>, CIFCMaterialComparator>::iterator itMaterial2ConceptualFaces = mapMaterial2ConceptualFaces.find(*pMaterial);
				if (itMaterial2ConceptualFaces == mapMaterial2ConceptualFaces.end())
				{
					vector<CConceptualFace> vecConceptualFaces;

					CConceptualFace conceptualFace;
					conceptualFace.index() = iFace;
					conceptualFace.trianglesStartIndex() = iStartIndexTriangles;
					conceptualFace.trianglesIndicesCount() = iIndicesCountTriangles;

					vecConceptualFaces.push_back(conceptualFace);

					mapMaterial2ConceptualFaces[*pMaterial] = vecConceptualFaces;
				}
				else
				{
					CConceptualFace conceptualFace;
					conceptualFace.index() = iFace;
					conceptualFace.trianglesStartIndex() = iStartIndexTriangles;
					conceptualFace.trianglesIndicesCount() = iIndicesCountTriangles;

					itMaterial2ConceptualFaces->second.push_back(conceptualFace);
				}

				delete pMaterial;
			} // if (iIndicesCountTriangles > 0)

			if (iIndicesCountLines > 0)
			{
				vecLinesIndices.push_back(pair<int_t, int_t>(iStartIndexLines, iIndicesCountLines));
			}

			if (iIndicesCountPoints > 0)
			{
				vecPointsIndices.push_back(pair<int_t, int_t>(iStartIndexPoints, iIndicesCountPoints));
			}

			if (iIndicesCountFacesPolygons > 0)
			{
				vecWireframesIndices.push_back(pair<int_t, int_t>(iStartIndexFacesPolygons, iIndicesCountFacesPolygons));
			}
		} // for (int_t iFace = ...

		/
		* Group the triangles
		/
		map<CIFCMaterial, vector<CConceptualFace>, CIFCMaterialComparator>::iterator itMaterial2ConceptualFaces = mapMaterial2ConceptualFaces.begin();
		for (; itMaterial2ConceptualFaces != mapMaterial2ConceptualFaces.end(); itMaterial2ConceptualFaces++)
		{
			CIFCMaterial * pMaterial = NULL;

			for (size_t iConceptualFace = 0; iConceptualFace < itMaterial2ConceptualFaces->second.size(); iConceptualFace++)
			{
				CConceptualFace & conceptualFace = itMaterial2ConceptualFaces->second[iConceptualFace];

				int_t iStartIndexTriangles = conceptualFace.trianglesStartIndex();
				int_t iIndicesCountTriangles = conceptualFace.trianglesIndicesCount();

				/
				* Split the conceptual face - isolated case
				/
				if (iIndicesCountTriangles > MAX_INDICES_COUNT)
				{
					while (iIndicesCountTriangles > MAX_INDICES_COUNT)
					{
						// INDICES
						CIFCMaterial * pNewMaterial = new CIFCMaterial(itMaterial2ConceptualFaces->first);
						for (int_t iIndex = iStartIndexTriangles; iIndex < iStartIndexTriangles + MAX_INDICES_COUNT; iIndex++)
						{							
							pNewMaterial->indices().push_back(pIndices[iIndex]);
						}
						
						pIFCObject->getIFCMaterials().push_back(pNewMaterial);

						/
						* Update Conceptual face start index
						/
						conceptualFace.trianglesStartIndex() = 0;

						// Conceptual faces
						pNewMaterial->conceptualFaces().push_back(conceptualFace);

						iIndicesCountTriangles -= MAX_INDICES_COUNT;
						iStartIndexTriangles += MAX_INDICES_COUNT;
					}

					if (iIndicesCountTriangles > 0)
					{
						// INDICES
						CIFCMaterial * pNewMaterial = new CIFCMaterial(itMaterial2ConceptualFaces->first);
						for (int_t iIndex = iStartIndexTriangles; iIndex < iStartIndexTriangles + iIndicesCountTriangles; iIndex++)
						{
							pNewMaterial->indices().push_back(pIndices[iIndex]);
						}

						pIFCObject->getIFCMaterials().push_back(pNewMaterial);

						/
						* Update Conceptual face start index
						/
						conceptualFace.trianglesStartIndex() = 0;

						// Conceptual faces
						pNewMaterial->conceptualFaces().push_back(conceptualFace);
					}					

					continue;
				} // if (iIndicesCountTriangles > MAX_INDICES_COUNT)	

				/
				* Create material
				/
				if (pMaterial == NULL)
				{
					// C++
					pMaterial = new CIFCMaterial(itMaterial2ConceptualFaces->first);

					pIFCObject->getIFCMaterials().push_back(pMaterial);
				}
				
				/
				* Check the limit
				/
				if (pMaterial->indices().size() + iIndicesCountTriangles > MAX_INDICES_COUNT)
				{
					// C++
					pMaterial = new CIFCMaterial(itMaterial2ConceptualFaces->first);				

					pIFCObject->getIFCMaterials().push_back(pMaterial);
				}

				/
				* Update Conceptual face start index
				/
				conceptualFace.trianglesStartIndex() = pMaterial->indices().size();

				/
				* Add the indices
				/
				for (int_t iIndex = iStartIndexTriangles; iIndex < iStartIndexTriangles + iIndicesCountTriangles; iIndex++)
				{
					pMaterial->indices().push_back(pIndices[iIndex]);
				}

				// Conceptual faces
				pMaterial->conceptualFaces().push_back(conceptualFace);
			} // for (size_t iConceptualFace = ...				
		} // for (; itMaterial2ConceptualFaces != ...

		/
		* Group the lines
		/
		if (!vecLinesIndices.empty())
		{
			CLinesCohort * pLinesCohort = NULL;
			for (size_t iFace = 0; iFace < vecLinesIndices.size(); iFace++)
			{
				int_t iStartIndexLines = vecLinesIndices[iFace].first;
				int_t iIndicesLinesCount = vecLinesIndices[iFace].second;

				/
				* Create the cohort
				/
				if (pLinesCohort == NULL)
				{
					// C++
					pLinesCohort = new CLinesCohort();
					pIFCObject->getLinesCohorts().push_back(pLinesCohort);
				}

				/
				* Check the limit
				/
				if (pLinesCohort->indices().size() + iIndicesLinesCount > MAX_INDICES_COUNT)
				{
					// C++
					pLinesCohort = new CLinesCohort();
					pIFCObject->getLinesCohorts().push_back(pLinesCohort);
				}

				int_t iPreviousIndex = -1;
				for (int_t iIndex = iStartIndexLines; iIndex < iStartIndexLines + iIndicesLinesCount; iIndex++)
				{
					if (pIndices[iIndex] < 0)
					{
						iPreviousIndex = -1;

						continue;
					}

					if (iPreviousIndex != -1)
					{
						pLinesCohort->indices().push_back(pIndices[iPreviousIndex]);
						pLinesCohort->indices().push_back(pIndices[iIndex]);
					} // if (iPreviousIndex != -1)

					iPreviousIndex = iIndex;
				} // for (int_t iIndex = ...
			} // for (size_t iFace = ...
		} // if (!vecLines.empty())

		/
		* Group the points
		/
		// TODO

		/
		* Group the polygons
		/
		if (!vecWireframesIndices.empty())
		{
			CWireframesCohort * pWireframesCohort = NULL;
			for (size_t iFace = 0; iFace < vecWireframesIndices.size(); iFace++)
			{
				int_t iStartIndexFacesPolygons = vecWireframesIndices[iFace].first;
				int_t iIndicesFacesPolygonsCount = vecWireframesIndices[iFace].second;

				/
				* Create the cohort
				/
				if (pWireframesCohort == NULL)
				{
					// C++
					pWireframesCohort = new CWireframesCohort();
					pIFCObject->getWireframesCohorts().push_back(pWireframesCohort);
				}

				/
				* Check the limit
				/
				if (pWireframesCohort->indices().size() + iIndicesFacesPolygonsCount > MAX_INDICES_COUNT)
				{
					// C++
					pWireframesCohort = new CWireframesCohort();
					pIFCObject->getWireframesCohorts().push_back(pWireframesCohort);
				}

				int_t iPreviousIndex = -1;
				for (int_t iIndex = iStartIndexFacesPolygons; iIndex < iStartIndexFacesPolygons + iIndicesFacesPolygonsCount; iIndex++)
				{
					if (pIndices[iIndex] < 0)
					{
						iPreviousIndex = -1;

						continue;
					}

					if (iPreviousIndex != -1)
					{
						pWireframesCohort->indices().push_back(pIndices[iPreviousIndex]);
						pWireframesCohort->indices().push_back(pIndices[iIndex]);
					} // if (iPreviousIndex != -1)

					iPreviousIndex = iIndex;
				} // for (int_t iIndex = ...
			} // for (size_t iFace = ...
		} // if (!vecWireframes.empty())

		/
		* Copy the vertices - <X, Y, Z, Nx, Ny, Nz>
		/
		pIFCObject->vertices() = new float[(int_t) iVerticesCount * BUFFER_VERTEX_LENGTH];
		for (int_t iVertex = 0; iVertex < iVerticesCount; iVertex++)
		{
			pIFCObject->vertices()[(iVertex * BUFFER_VERTEX_LENGTH) + 0] = pVertices[(iVertex * VERTEX_LENGTH) + 0];
			pIFCObject->vertices()[(iVertex * BUFFER_VERTEX_LENGTH) + 1] = pVertices[(iVertex * VERTEX_LENGTH) + 1];
			pIFCObject->vertices()[(iVertex * BUFFER_VERTEX_LENGTH) + 2] = pVertices[(iVertex * VERTEX_LENGTH) + 2];
			pIFCObject->vertices()[(iVertex * BUFFER_VERTEX_LENGTH) + 3] = pVertices[(iVertex * VERTEX_LENGTH) + 3];
			pIFCObject->vertices()[(iVertex * BUFFER_VERTEX_LENGTH) + 4] = pVertices[(iVertex * VERTEX_LENGTH) + 4];
			pIFCObject->vertices()[(iVertex * BUFFER_VERTEX_LENGTH) + 5] = pVertices[(iVertex * VERTEX_LENGTH) + 5];
		} // for (int_t iVertex = ...

		delete[] pVertices;
		delete[] pIndices;

		//
		//	get bounding box
		//
		double	transformationMatrix[12], startVector[3], endVector[3];
		GetBoundingBox(iInstance, transformationMatrix, startVector, endVector);

		MATRIX	* matrix = (MATRIX*) transformationMatrix;
		VECTOR3	originVec;

		Vec3Transform(&originVec, (VECTOR3*)startVector, matrix);
		pIFCObject->setOrigin(&originVec);

		VECTOR3	myVec;

		VECTOR3	xVec;
		xVec.x = endVector[0];
		xVec.y = startVector[1];
		xVec.z = startVector[2];
		Vec3Transform(&myVec, &xVec, matrix);
		Vec3Subtract(&myVec, &myVec, &originVec);
		pIFCObject->setXVec(&myVec);

		VECTOR3	yVec;
		yVec.x = startVector[0];
		yVec.y = endVector[1];
		yVec.z = startVector[2];
		Vec3Transform(&myVec, &yVec, matrix);
		Vec3Subtract(&myVec, &myVec, &originVec);
		pIFCObject->setYVec(&myVec);

		VECTOR3	zVec;
		zVec.x = startVector[0];
		zVec.y = startVector[1];
		zVec.z = endVector[2];
		Vec3Transform(&myVec, &zVec, matrix);
		Vec3Subtract(&myVec, &myVec, &originVec);
		pIFCObject->setZVec(&myVec);
	} // if ((iVerticesCount > 0) && ...	

	/
	* Restore circleSegments()

	if (iCircleSegments != DEFAULT_CIRCLE_SEGMENTS)
	{
		circleSegments(DEFAULT_CIRCLE_SEGMENTS, 5);
	}

	cleanMemory(m_iIFCModel, 0);

	return pIFCObject;
}
*/

