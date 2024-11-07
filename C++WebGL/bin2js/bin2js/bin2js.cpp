// bin2js.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "resource.h"
#include "bin2js.h"
#include "RDFModel.h"
#include "texture.h"
#include <math.h>
#include <set>

#ifdef _DISABLE_BOOST
static wstring getPathName(const wstring& s) {

	wchar_t sep = L'\\';

	size_t i = s.rfind(sep, s.length());
	if (i != wstring::npos) {
		return(s.substr(0, i));
	}

	return(L"");
}
#endif // _DISABLE_BOOST

using namespace std;

// ------------------------------------------------------------------------------------------------
// Default (embedded) texture
const wchar_t DEFAULT_TEXTURE[] = L"default.png";

// ------------------------------------------------------------------------------------------------
// Root path
wstring g_strInputFolder;

// ------------------------------------------------------------------------------------------------
// Groups
set<wstring> g_setGroups;

// ------------------------------------------------------------------------------------------------
// Texture file : JavaScript variable
map<wstring, string> g_mapTexture2JSVar;

// ------------------------------------------------------------------------------------------------
void generateMaterialJS(const CRDFMaterial& material, ofstream & jsStream)
{
	jsStream << "var material = {};\n";
	jsStream << "conceptualFace.material = material;\n";

	if (material.hasTexture())
	{
		wstring strTexture = material.getTexture();
		if (strTexture.empty())
		{
			/*
			* Use the default texture
			*/
			strTexture = DEFAULT_TEXTURE;

			map<wstring, string>::iterator itTexture = g_mapTexture2JSVar.find(strTexture);
			if (itTexture == g_mapTexture2JSVar.end())
			{
				// Load
				string strTextureBase64 = LoadTextureResourceBase64(L"bin2js.dll", IDB_PNG_DEFAULT_TEXTURE);

				// JavaScript
				jsStream << "var g_defaultTextureBase64 = 'data:image/png;base64,";
				jsStream << strTextureBase64;
				jsStream << "'\n";

				g_mapTexture2JSVar[strTexture] = "g_defaultTextureBase64";
			}
		} // if (strTexture.empty())
		else
		{
			/*
			* Load the texture
			*/
			map<wstring, string>::iterator itTexture = g_mapTexture2JSVar.find(strTexture);
			if (itTexture == g_mapTexture2JSVar.end())
			{
				wstring strTextureFullPath = g_strInputFolder;
				strTextureFullPath += L"\\";
				strTextureFullPath += strTexture;

				// Load
				string strTextureBase64 = LoadTextureFileBase64(strTextureFullPath.c_str());

				// JavaScript
				char szJSVar[100];
				sprintf(szJSVar, "g_textureBase64_%d", (int)g_mapTexture2JSVar.size());

				jsStream << "var ";
				jsStream << szJSVar;
				jsStream << " = 'data:image/png;base64,";
				jsStream << strTextureBase64;
				jsStream << "'\n";

				g_mapTexture2JSVar[strTexture] = szJSVar;
			}
		} // else if (strTexture.empty())

		map<wstring, string>::iterator itTexture = g_mapTexture2JSVar.find(strTexture);
		assert(itTexture != g_mapTexture2JSVar.end());

		jsStream << "var texture = {};\n";
		jsStream << "texture.name = '";
		jsStream << CW2A(strTexture.c_str());
		jsStream << "';\n";
		jsStream << "texture.base64Content = ";
		jsStream << itTexture->second;
		jsStream << ";\n";
		jsStream << "material.texture = texture;\n";
	} // if (material.hasTexture())
	else
	{
		// Ambient
		jsStream << "material.ambient = [";
		jsStream << to_string(material.getAmbientColor().R());
		jsStream << ",";
		jsStream << to_string(material.getAmbientColor().G());
		jsStream << ",";
		jsStream << to_string(material.getAmbientColor().B());
		jsStream << "];\n";

		// Diffuse
		jsStream << "material.diffuse = [";
		jsStream << to_string(material.getDiffuseColor().R());
		jsStream << ",";
		jsStream << to_string(material.getDiffuseColor().G());
		jsStream << ",";
		jsStream << to_string(material.getDiffuseColor().B());
		jsStream << "];\n";

		// Specular
		jsStream << "material.specular = [";
		jsStream << to_string(material.getSpecularColor().R());
		jsStream << ",";
		jsStream << to_string(material.getSpecularColor().G());
		jsStream << ",";
		jsStream << to_string(material.getSpecularColor().B());
		jsStream << "];\n";

		// Emissive
		jsStream << "material.emissive = [";
		jsStream << to_string(material.getEmissiveColor().R());
		jsStream << ",";
		jsStream << to_string(material.getEmissiveColor().G());
		jsStream << ",";
		jsStream << to_string(material.getEmissiveColor().B());
		jsStream << "];\n";

		// Transparency
		jsStream << "material.transparency = ";
		jsStream << to_string(material.A());
		jsStream << ";\n";
	} // else if (material.hasTexture())
}

// ------------------------------------------------------------------------------------------------
void generateConceptualFacesJS(CRDFInstance * pInstance, const vector<size_t>& vecConceptualFaces, int32_t* arVertices, ofstream & jsStream)
{
	char szBuffer[100];

	bool bHasTexture = pInstance->hasTexture();

	const vector<pair<int64_t, int64_t> > & vecTriangles = pInstance->getTriangles();

	jsStream << "var conceptualFace = {};\n";
	jsStream << "instance.conceptualFaces.push(conceptualFace);\n";

	/*
	* Vertices
	*/
	string strVertices;
	for (int64_t iVertex = 0; iVertex < pInstance->getVerticesCount(); iVertex++)
	{
		if (arVertices[iVertex] == 0)
		{
			// Unused
			continue;
		}

		if (!strVertices.empty())
		{
			strVertices += ",";
		}

		// X
		float fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 0];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Y
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 1];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Z
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 2];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Nx
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 3];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Ny
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 4];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Nz
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 5];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		if (bHasTexture)
		{
			// Tx
			fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 6];

			// PATCH: to be fixed in engine.dll
			if ((fVertex > 1E10) || !isfinite(fVertex))
			{
				fVertex = 0.f;
			}

			strVertices += ",";
			sprintf(szBuffer, "%g", fVertex);
			strVertices += szBuffer;

			// Ty
			fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 7];

			// PATCH: to be fixed in engine.dll
			if ((fVertex > 1E10) || !isfinite(fVertex))
			{
				fVertex = 0.f;
			}

			strVertices += ",";
			sprintf(szBuffer, "%g", fVertex);
			strVertices += szBuffer;
		} // if (bHasTexture)

		// Skip Ambient, Diffuse, Emissive, Specular
		// ...
	} // for (int64_t iVertex = ...

	string strJSON = "{ \"vertices\" : [";
	strJSON += strVertices;
	strJSON += "] }";

	jsStream << JSON2GZipBase64JS(strJSON);
	jsStream << "conceptualFace.vertices = JSON.parse(json).vertices;\n";
	jsStream << "conceptualFace.vertexSizeInBytes = " << (bHasTexture ? "32" : "24") << ";\n";

	/*
	* Indices
	*/

	// Calculate the indices according to the new vertex buffer.
	int32_t iIndex = 0;
	for (int64_t iVertex = 0; iVertex < pInstance->getVerticesCount(); iVertex++)
	{
		if (arVertices[iVertex] == 0)
		{
			// Unused
			continue;
		}

		arVertices[iVertex] = iIndex++;
	}

	string strIndices = "";
	for (size_t iConceptualFace = 0; iConceptualFace < vecConceptualFaces.size(); iConceptualFace++)
	{
		for (int64_t iIndex = vecTriangles[vecConceptualFaces[iConceptualFace]].first;
			iIndex < vecTriangles[vecConceptualFaces[iConceptualFace]].first + vecTriangles[vecConceptualFaces[iConceptualFace]].second;
			iIndex++)
		{
			if (!strIndices.empty())
			{
				strIndices += ",";
			}

			int32_t iConvertedIndex = arVertices[pInstance->getIndices()[iIndex]];
			assert(iConvertedIndex <= USHRT_MAX);

			strIndices += to_string(iConvertedIndex);
		} // for (int64_t iIndex = ...
	} // for (int64_t iConceptualFace ...

	strJSON = "{ \"indices\" : [";
	strJSON += strIndices;
	strJSON += "] }";

	jsStream << JSON2GZipBase64JS(strJSON);
	jsStream << "conceptualFace.indices = JSON.parse(json).indices;\n";	
}

// ------------------------------------------------------------------------------------------------
void generateConceptualFacesPolygonsJS(CRDFInstance * pInstance, const vector<size_t>& vecConceptualFacesPolygonsCohort, int32_t* arVertices, ofstream & jsStream)
{
	char szBuffer[100];

	const vector<pair<int64_t, int64_t> > & vecConceptualFacesPolygons = pInstance->getConceptualFacesPolygons();

	jsStream << "var conceptualFacesPolygons = {};\n";
	jsStream << "instance.conceptualFacesPolygons.push(conceptualFacesPolygons);\n";

	/*
	* Vertices
	*/
	string strVertices;
	for (int64_t iVertex = 0; iVertex < pInstance->getVerticesCount(); iVertex++)
	{
		if (arVertices[iVertex] == 0)
		{
			// Unused
			continue;
		}

		// X
		float fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 0];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		if (!strVertices.empty())
		{
			strVertices += ",";
		}

		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Y
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 1];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;

		// Z
		fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 2];

		// PATCH: to be fixed in engine.dll
		if ((fVertex > 1E10) || !isfinite(fVertex))
		{
			fVertex = 0.f;
		}

		strVertices += ",";
		sprintf(szBuffer, "%g", fVertex);
		strVertices += szBuffer;
	} // for (int64_t iVertex = ...

	string strJSON = "{ \"vertices\" : [";
	strJSON += strVertices;
	strJSON += "] }";

	jsStream << JSON2GZipBase64JS(strJSON);
	jsStream << "conceptualFacesPolygons.vertices = JSON.parse(json).vertices;\n";

	/*
	* Indices
	*/

	// Calculate the indices according to the new vertex buffer.
	int32_t iIndex = 0;
	for (int64_t iVertex = 0; iVertex < pInstance->getVerticesCount(); iVertex++)
	{
		if (arVertices[iVertex] == 0)
		{
			// Unused
			continue;
		}

		arVertices[iVertex] = iIndex++;
	}

	string strIndices;
	for (size_t iConceptualFace = 0; iConceptualFace < vecConceptualFacesPolygonsCohort.size(); iConceptualFace++)
	{
		int64_t iPreviousIndex = -1;
		for (int64_t iIndex = vecConceptualFacesPolygons[vecConceptualFacesPolygonsCohort[iConceptualFace]].first;
			iIndex < vecConceptualFacesPolygons[vecConceptualFacesPolygonsCohort[iConceptualFace]].first + vecConceptualFacesPolygons[vecConceptualFacesPolygonsCohort[iConceptualFace]].second;
			iIndex++)
		{
			if (pInstance->getIndices()[iIndex] < 0)
			{				
				iPreviousIndex = -1;

				continue;
			} // if (pInstance->getIndices()[iIndex] < 0)

			if (iPreviousIndex == -1)
			{
				iPreviousIndex = iIndex;

				continue;
			}

			if (!strIndices.empty())
			{
				strIndices += ",";
			}

			int32_t iConvertedIndex = arVertices[pInstance->getIndices()[iPreviousIndex]];
			assert(iConvertedIndex <= USHRT_MAX);

			strIndices += to_string(iConvertedIndex);

			iConvertedIndex = arVertices[pInstance->getIndices()[iIndex]];
			assert(iConvertedIndex <= USHRT_MAX);

			strIndices += ",";
			strIndices += to_string(iConvertedIndex);

			iPreviousIndex = iIndex;
		} // for (size_t iIndex = ...
	} // for (size_t iConceptualFace = ...

	strJSON = "{ \"indices\" : [";
	strJSON += strIndices;
	strJSON += "] }";

	jsStream << JSON2GZipBase64JS(strJSON);
	jsStream << "conceptualFacesPolygons.indices = JSON.parse(json).indices;\n";
}

// ------------------------------------------------------------------------------------------------
void generateInstanceJS(CRDFInstance * pInstance, const wchar_t* szTag, ofstream & jsStream)
{
	char szBuffer[100];

	/*
	* Instance
	*/
	jsStream << "var instance = {};\n";
	jsStream << "instance.name = \"" << CW2A(pInstance->getName()) << "\";\n";
	jsStream << "instance.uri = \"" << CW2A(pInstance->getGUID()) << "\";\n";
	jsStream << "instance.group = \"" << CW2A(pInstance->getGroup()) << "\";\n";
	if (szTag != NULL)
	{
		jsStream << "instance.tag = \"" << CW2A(szTag) << "\";\n";
	}	
	jsStream << "instance.propertyName = \"" << CW2A(pInstance->getPropertyName()) << "\";\n";
	jsStream << "instance.propertyDescription = \"" << CW2A(pInstance->getPropertyDescription()) << "\";\n";
	jsStream << "instance.propertyExpressID = \"" << CW2A(pInstance->getPropertyExpressID()) << "\";\n";
	jsStream << "instance.visible = true;\n";

	/**********************************************************************************************
	* Min/max
	*/
	float fXmin = FLT_MAX;
	float fXmax = -FLT_MAX;
	float fYmin = FLT_MAX;
	float fYmax = -FLT_MAX;
	float fZmin = FLT_MAX;
	float fZmax = -FLT_MAX;
	pInstance->CalculateMinMax(fXmin, fXmax, fYmin, fYmax, fZmin, fZmax);

	sprintf(szBuffer, "%g", fXmin);
	jsStream << "instance.Xmin = " << szBuffer << ";\n";
	sprintf(szBuffer, "%g", fXmax);
	jsStream << "instance.Xmax = " << szBuffer << ";\n";
	sprintf(szBuffer, "%g", fYmin);
	jsStream << "instance.Ymin = " << szBuffer << ";\n";
	sprintf(szBuffer, "%g", fYmax);
	jsStream << "instance.Ymax = " << szBuffer << ";\n";
	sprintf(szBuffer, "%g", fZmin);
	jsStream << "instance.Zmin = " << szBuffer << ";\n";
	sprintf(szBuffer, "%g", fZmax);
	jsStream << "instance.Zmax = " << szBuffer << ";\n";
	// ********************************************************************************************

	/**********************************************************************************************
	* Group the conceptual faces by material
	*/
	jsStream << "instance.conceptualFaces = [];\n";

	const vector<pair<int64_t, int64_t> > & vecTriangles = pInstance->getTriangles();
	map<CRDFMaterial, vector<size_t>, CRDFMaterialComparator> mapMaterial2ConceptualFaces;
	if (!vecTriangles.empty())
	{
		const vector<CRDFMaterial *> & vecConceptualFacesMaterials = pInstance->getConceptualFacesMaterials();
		assert(vecTriangles.size() == vecConceptualFacesMaterials.size());
		
		for (size_t iConceptualFace = 0; iConceptualFace < vecConceptualFacesMaterials.size(); iConceptualFace++)
		{
			map<CRDFMaterial, vector<size_t>, CRDFMaterialComparator>::iterator itMaterial2ConceptualFaces = mapMaterial2ConceptualFaces.find(*vecConceptualFacesMaterials[iConceptualFace]);
			if (itMaterial2ConceptualFaces == mapMaterial2ConceptualFaces.end())
			{
				vector<size_t> vecConceptualFaces;
				vecConceptualFaces.push_back(iConceptualFace);

				mapMaterial2ConceptualFaces[*vecConceptualFacesMaterials[iConceptualFace]] = vecConceptualFaces;
			}
			else
			{
				itMaterial2ConceptualFaces->second.push_back(iConceptualFace);
			}
		}	

		/**********************************************************************************************
		* Vertices
		*/
		if (pInstance->getVerticesCount() > MAX_INDICES_COUNT)
		{
			int32_t* arVertices = new int32_t[pInstance->getVerticesCount()];

			map<CRDFMaterial, vector<size_t>, CRDFMaterialComparator>::iterator itMaterial2ConceptualFaces = mapMaterial2ConceptualFaces.begin();
			for (; itMaterial2ConceptualFaces != mapMaterial2ConceptualFaces.end(); itMaterial2ConceptualFaces++)
			{
				// Unique vertices
				size_t iVerticesCount = 0;
				for (auto iIndex = 0; iIndex < pInstance->getVerticesCount(); iIndex++)
				{
					arVertices[iIndex] = 0;
				}

				// All indices
				size_t iIndicesCount = 0;
	
				vector<size_t> vecConceptualFacesCohort;
				for (size_t iConceptualFace = 0; iConceptualFace < itMaterial2ConceptualFaces->second.size(); iConceptualFace++)
				{
					if ((iVerticesCount + vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second) > MAX_INDICES_COUNT ||
						((iIndicesCount + vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second) > MAX_INDICES_COUNT))
					{
						/*
						* Vertices/Indices
						*/
						generateConceptualFacesJS(pInstance, vecConceptualFacesCohort, arVertices, jsStream);

						/*
						* Material
						*/
						const CRDFMaterial& material = itMaterial2ConceptualFaces->first;
						generateMaterialJS(material, jsStream);

						/*
						* Reset
						*/
						for (auto iIndex = 0; iIndex < pInstance->getVerticesCount(); iIndex++)
						{
							arVertices[iIndex] = 0;
						}
						iVerticesCount = 0;
												
						iIndicesCount = 0;

						vecConceptualFacesCohort.clear();
					} // if ((iVerticesCount + vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second) > MAX_INDICES_COUNT || ...

					vecConceptualFacesCohort.push_back(itMaterial2ConceptualFaces->second[iConceptualFace]);

					for (int64_t iIndex = vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].first;
						iIndex < vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].first + vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second;
						iIndex++)
					{
						if (arVertices[pInstance->getIndices()[iIndex]] == 0)
						{
							iVerticesCount++;
						}

						arVertices[pInstance->getIndices()[iIndex]]++;

						iIndicesCount++;
					} // for (size_t iIndex = ...
				} // for (size_t iConceptualFace = ...

				if (!vecConceptualFacesCohort.empty())
				{
					/*
					* Vertices/Indices
					*/
					generateConceptualFacesJS(pInstance, vecConceptualFacesCohort, arVertices, jsStream);

					/*
					* Material
					*/
					const CRDFMaterial& material = itMaterial2ConceptualFaces->first;
					generateMaterialJS(material, jsStream);

					/*
					* Reset
					*/
					for (auto iIndex = 0; iIndex < pInstance->getVerticesCount(); iIndex++)
					{
						arVertices[iIndex] = 0;
					}
					iVerticesCount = 0;

					iIndicesCount = 0;

					vecConceptualFacesCohort.clear();
				} // if (!vecConceptualFacesCohort.empty())
			} // for (; itMaterial2ConceptualFaces != ...

			delete[] arVertices;
		} // if (pInstance->getVerticesCount() > MAX_INDICES_COUNT)
		else
		{
			bool bHasTexture = pInstance->hasTexture();

			string strJSON = "{ \"vertices\" : [";
			for (int64_t iVertex = 0; iVertex < pInstance->getVerticesCount() ; iVertex++)
			{
				if (iVertex > 0)
				{
					strJSON += ",";
				}

				// X
				float fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 0];

				// PATCH: to be fixed in engine.dll
				if ((fVertex > 1E10) || !isfinite(fVertex))
				{
					fVertex = 0.f;
				}

				sprintf(szBuffer, "%g", fVertex);
				strJSON += szBuffer;

				// Y
				fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 1];

				// PATCH: to be fixed in engine.dll
				if ((fVertex > 1E10) || !isfinite(fVertex))
				{
					fVertex = 0.f;
				}

				strJSON += ",";
				sprintf(szBuffer, "%g", fVertex);
				strJSON += szBuffer;

				// Z
				fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 2];

				// PATCH: to be fixed in engine.dll
				if ((fVertex > 1E10) || !isfinite(fVertex))
				{
					fVertex = 0.f;
				}

				strJSON += ",";
				sprintf(szBuffer, "%g", fVertex);
				strJSON += szBuffer;

				// Nx
				fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 3];

				// PATCH: to be fixed in engine.dll
				if ((fVertex > 1E10) || !isfinite(fVertex))
				{
					fVertex = 0.f;
				}

				strJSON += ",";
				sprintf(szBuffer, "%g", fVertex);
				strJSON += szBuffer;

				// Ny
				fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 4];

				// PATCH: to be fixed in engine.dll
				if ((fVertex > 1E10) || !isfinite(fVertex))
				{
					fVertex = 0.f;
				}

				strJSON += ",";
				sprintf(szBuffer, "%g", fVertex);
				strJSON += szBuffer;

				// Nz
				fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 5];

				// PATCH: to be fixed in engine.dll
				if ((fVertex > 1E10) || !isfinite(fVertex))
				{
					fVertex = 0.f;
				}

				strJSON += ",";
				sprintf(szBuffer, "%g", fVertex);
				strJSON += szBuffer;

				if (bHasTexture)
				{
					// Tx
					fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 6];

					// PATCH: to be fixed in engine.dll
					if ((fVertex > 1E10) || !isfinite(fVertex))
					{
						fVertex = 0.f;
					}

					strJSON += ",";
					sprintf(szBuffer, "%g", fVertex);
					strJSON += szBuffer;

					// Ty
					fVertex = pInstance->getVertices()[(iVertex * VERTEX_LENGTH) + 7];

					// PATCH: to be fixed in engine.dll
					if ((fVertex > 1E10) || !isfinite(fVertex))
					{
						fVertex = 0.f;
					}

					strJSON += ",";
					sprintf(szBuffer, "%g", fVertex);
					strJSON += szBuffer;
				} // if (bHasTexture)

				// Skip Ambient, Diffuse, Emissive, Specular
				// ...
			} // for (int64_t iVertex = ...

			strJSON += "] }";

			jsStream << JSON2GZipBase64JS(strJSON);
			jsStream << "instance.vertexSizeInBytes = " << (bHasTexture ? "32" : "24") << ";\n";
			jsStream << "instance.vertices = JSON.parse(json).vertices;\n";			
			// *********************************************************************************************/
		
			jsStream << "instance.conceptualFaces = [];\n";

			map<CRDFMaterial, vector<size_t>, CRDFMaterialComparator>::iterator itMaterial2ConceptualFaces = mapMaterial2ConceptualFaces.begin();
			for (; itMaterial2ConceptualFaces != mapMaterial2ConceptualFaces.end(); itMaterial2ConceptualFaces++)
			{
				jsStream << "var conceptualFace = {};\n";
				jsStream << "instance.conceptualFaces.push(conceptualFace);\n";

				/*
				* Count the indices
				*/
				size_t iIndicesCount = 0;
				for (size_t iConceptualFace = 0; iConceptualFace < itMaterial2ConceptualFaces->second.size(); iConceptualFace++)
				{
					iIndicesCount += vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second;
				} // for (size_t iConceptualFace = ...

				/*
				* Indices
				*/
				if (iIndicesCount > MAX_INDICES_COUNT)
				{
					jsStream << "conceptualFace.cohorts = [];\n";

					iIndicesCount = 0;
					string strIndices = "";
					for (size_t iConceptualFace = 0; iConceptualFace < itMaterial2ConceptualFaces->second.size(); iConceptualFace++)
					{
						for (int64_t iIndex = vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].first;
							iIndex < vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].first + vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second;
							iIndex += 3)
						{
							if (!strIndices.empty())
							{
								strIndices += ",";
							}

							assert(pInstance->getIndices()[iIndex] <= USHRT_MAX);
							strIndices += to_string((pInstance->getIndices()[iIndex]));

							strIndices += ",";
							assert(pInstance->getIndices()[iIndex + 1] <= USHRT_MAX);
							strIndices += to_string((pInstance->getIndices()[iIndex + 1]));

							strIndices += ",";
							assert(pInstance->getIndices()[iIndex + 2] <= USHRT_MAX);
							strIndices += to_string((pInstance->getIndices()[iIndex + 2]));

							iIndicesCount += 3;

							if (iIndicesCount > MAX_INDICES_COUNT)
							{
								assert(MAX_INDICES_COUNT <= USHRT_MAX);

								jsStream << "var cohort = {};\n";
								jsStream << "conceptualFace.cohorts.push(cohort);\n";

								strJSON = "{ \"indices\" : [";
								strJSON += strIndices;
								strJSON += "] }";

								jsStream << JSON2GZipBase64JS(strJSON);
								jsStream << "cohort.indices = JSON.parse(json).indices;\n";

								iIndicesCount = 0;
								strIndices = "";
							} // if (iIndicesCount > MAX_INDICES_COUNT)
						} // for (size_t iIndex = ...

						if (!strIndices.empty())
						{
							jsStream << "var cohort = {};\n";
							jsStream << "conceptualFace.cohorts.push(cohort);\n";

							strJSON = "{ \"indices\" : [";
							strJSON += strIndices;
							strJSON += "] }";

							jsStream << JSON2GZipBase64JS(strJSON);
							jsStream << "cohort.indices = JSON.parse(json).indices;\n";

							iIndicesCount = 0;
							strIndices = "";
						} // if (!strIndices.empty())
					} // for (size_t iConceptualFace = ...
				} // if (iIndicesCount > MAX_INDICES_COUNT)
				else
				{
					string strIndices = "";
					for (size_t iConceptualFace = 0; iConceptualFace < itMaterial2ConceptualFaces->second.size(); iConceptualFace++)
					{
						for (int64_t iIndex = vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].first;
							iIndex < vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].first + vecTriangles[itMaterial2ConceptualFaces->second[iConceptualFace]].second;
							iIndex++)
						{
							if (!strIndices.empty())
							{
								strIndices += ",";
							}

							strIndices += to_string((pInstance->getIndices()[iIndex]));
						} // for (size_t iIndex = ...
					} // for (size_t iConceptualFace = ...			

					strJSON = "{ \"indices\" : [";
					strJSON += strIndices;
					strJSON += "] }";

					jsStream << JSON2GZipBase64JS(strJSON);
					jsStream << "conceptualFace.indices = JSON.parse(json).indices;\n";
				} // else if (iIndicesCount > MAX_INDICES_COUNT)

				/*
				* Material
				*/
				const CRDFMaterial& material = itMaterial2ConceptualFaces->first;
				generateMaterialJS(material, jsStream);
			} // for (; itMaterial2ConceptualFaces != ...
			// ********************************************************************************************
		} // else if (pInstance->getVerticesCount() > MAX_INDICES_COUNT)
	} // if (!vecTriangles.empty())		

	/**********************************************************************************************
	* Faces polygons
	*/
	// DISABLED
	/*jsStream << "var facesPolygons = {};\n";
	jsStream << "instance.facesPolygons = facesPolygons;\n";*/

	/*
	* Indices
	*/	
	//jsStream << "facesPolygons.indices = [";

	//string strIndices = "";

	//const vector<pair<int64_t, int64_t> > & vecFacesPolygons = pInstance->getFacesPolygons();
	//if (!vecFacesPolygons.empty())
	//{
	//	for (size_t iPolygon = 0; iPolygon < vecFacesPolygons.size(); iPolygon++)
	//	{
	//		int64_t iFirstIndex = -1;
	//		int64_t iPreviousIndex = -1;
	//		for (int64_t iIndex = vecFacesPolygons[iPolygon].first; iIndex < vecFacesPolygons[iPolygon].first + vecFacesPolygons[iPolygon].second; iIndex++)
	//		{
	//			if (pInstance->getIndices()[iIndex] < 0)
	//			{
	//				if ((iFirstIndex != -1) && (iPreviousIndex != -1))
	//				{
	//					if (!strIndices.empty())
	//					{
	//						strIndices += ",";
	//					}

	//					strIndices += to_string((pInstance->getIndices()[iPreviousIndex]));
	//					strIndices += ",";
	//					strIndices += to_string((pInstance->getIndices()[iFirstIndex]));
	//				}

	//				iFirstIndex = -1;
	//				iPreviousIndex = -1;

	//				continue;
	//			} // if (pInstance->getIndices()[iIndex] < 0)

	//			if (iFirstIndex == -1)
	//			{
	//				iFirstIndex = iIndex;
	//				iPreviousIndex = iIndex;

	//				continue;
	//			}

	//			if (!strIndices.empty())
	//			{
	//				strIndices += ",";
	//			}

	//			strIndices += to_string((pInstance->getIndices()[iPreviousIndex]));
	//			strIndices += ",";
	//			strIndices += to_string((pInstance->getIndices()[iIndex]));

	//			iPreviousIndex = iIndex;
	//		} // for (size_t iIndex = ...
	//	} // for (size_t iPolygon = ...

	//	jsStream << strIndices;
	//} // if (!vecFacesPolygons.empty())

	//jsStream << "];\n";
	// ********************************************************************************************

	/**********************************************************************************************
	* Conceptual faces polygons
	*/

	/*
	* Indices
	*/
	const vector<pair<int64_t, int64_t> > & vecConceptualFacesPolygons = pInstance->getConceptualFacesPolygons();
	if (!vecConceptualFacesPolygons.empty())
	{
		jsStream << "instance.conceptualFacesPolygons = [];\n";

		if (pInstance->getVerticesCount() > MAX_INDICES_COUNT)
		{
			int32_t* arVertices = new int32_t[pInstance->getVerticesCount()];

			// Unique vertices
			size_t iVerticesCount = 0;
			for (auto iIndex = 0; iIndex < pInstance->getVerticesCount(); iIndex++)
			{
				arVertices[iIndex] = 0;
			}

			// All indices
			size_t iIndicesCount = 0;

			vector<size_t> vecConceptualFacesPolygonsCohort;
			for (size_t iPolygon = 0; iPolygon < vecConceptualFacesPolygons.size(); iPolygon++)
			{
				if ((iVerticesCount + (vecConceptualFacesPolygons[iPolygon].second * 2)) > MAX_INDICES_COUNT ||
					((iIndicesCount + (vecConceptualFacesPolygons[iPolygon].second * 2)) > MAX_INDICES_COUNT))
				{
					generateConceptualFacesPolygonsJS(pInstance, vecConceptualFacesPolygonsCohort, arVertices, jsStream);
					
					/*
					* Reset
					*/
					for (auto iIndex = 0; iIndex < pInstance->getVerticesCount(); iIndex++)
					{
						arVertices[iIndex] = 0;
					}
					iVerticesCount = 0;

					iIndicesCount = 0;

					vecConceptualFacesPolygonsCohort.clear();
				} // if ((iVerticesCount + (vecConceptualFacesPolygons[iPolygon].second * 2)) > MAX_INDICES_COUNT || ...

				vecConceptualFacesPolygonsCohort.push_back(iPolygon);

				int64_t iPreviousIndex = -1;
				for (int64_t iIndex = vecConceptualFacesPolygons[iPolygon].first; 
					iIndex < vecConceptualFacesPolygons[iPolygon].first + vecConceptualFacesPolygons[iPolygon].second; 
					iIndex++)
				{
					if (pInstance->getIndices()[iIndex] < 0)
					{	
						iPreviousIndex = -1;

						continue;
					} // if (pInstance->getIndices()[iIndex] < 0)

					if (iPreviousIndex == -1)
					{
						iPreviousIndex = iIndex;

						continue;
					}

					if (arVertices[pInstance->getIndices()[iIndex]] == 0)
					{
						iVerticesCount++;
					}

					arVertices[pInstance->getIndices()[iIndex]]++;

					iIndicesCount += 2;

					iPreviousIndex = iIndex;
				} // for (size_t iIndex = ...
			} // for (size_t iPolygon = ...

			if (!vecConceptualFacesPolygonsCohort.empty())
			{
				generateConceptualFacesPolygonsJS(pInstance, vecConceptualFacesPolygonsCohort, arVertices, jsStream);
			}

			delete[] arVertices;
		} // if (pInstance->getVerticesCount() > MAX_INDICES_COUNT)
		else
		{
			// The vertices should be written already
			assert(!vecTriangles.empty());

			jsStream << "var conceptualFacesPolygon = {};\n";
			jsStream << "instance.conceptualFacesPolygons.push(conceptualFacesPolygon);\n";

			// All indices
			size_t iIndicesCount = 0;

			// Count the indices
			for (size_t iPolygon = 0; iPolygon < vecConceptualFacesPolygons.size(); iPolygon++)
			{
				int64_t iPreviousIndex = -1;
				for (int64_t iIndex = vecConceptualFacesPolygons[iPolygon].first; 
					iIndex < vecConceptualFacesPolygons[iPolygon].first + vecConceptualFacesPolygons[iPolygon].second; 
					iIndex++)
				{
					if (pInstance->getIndices()[iIndex] < 0)
					{	
						iPreviousIndex = -1;

						continue;
					} // if (pInstance->getIndices()[iIndex] < 0)

					if (iPreviousIndex == -1)
					{
						iPreviousIndex = iIndex;

						continue;
					}

					iIndicesCount += 2;

					iPreviousIndex = iIndex;
				} // for (size_t iIndex = ...
			} // for (size_t iPolygon = ...

			if (iIndicesCount > MAX_INDICES_COUNT)
			{
				jsStream << "conceptualFacesPolygon.cohorts = [];\n";
				
				string strIndices;
				iIndicesCount = 0;
				for (size_t iPolygon = 0; iPolygon < vecConceptualFacesPolygons.size(); iPolygon++)
				{
					if (iIndicesCount + (vecConceptualFacesPolygons[iPolygon].second * 2) > MAX_INDICES_COUNT)
					{
						assert(iIndicesCount <= USHRT_MAX);

						jsStream << "var cohort = {};\n";
						jsStream << "conceptualFacesPolygon.cohorts.push(cohort);\n";

						string strJSON = "{ \"indices\" : [";
						strJSON += strIndices;
						strJSON += "] }";						

						jsStream << JSON2GZipBase64JS(strJSON);
						jsStream << "cohort.indices = JSON.parse(json).indices;\n";

						/*
						* Reset
						*/
						iIndicesCount = 0;
						strIndices = "";
					} // if (iIndicesCount + (vecConceptualFacesPolygons[iPolygon].second * 2) > ...

					int64_t iPreviousIndex = -1;
					for (int64_t iIndex = vecConceptualFacesPolygons[iPolygon].first; iIndex < vecConceptualFacesPolygons[iPolygon].first + vecConceptualFacesPolygons[iPolygon].second; iIndex++)
					{
						if (pInstance->getIndices()[iIndex] < 0)
						{
							iPreviousIndex = -1;

							continue;
						} // if (pInstance->getIndices()[iIndex] < 0)

						if (iPreviousIndex == -1)
						{
							iPreviousIndex = iIndex;

							continue;
						}

						if (!strIndices.empty())
						{
							strIndices += ",";
						}

						strIndices += to_string((pInstance->getIndices()[iPreviousIndex]));
						strIndices += ",";
						strIndices += to_string((pInstance->getIndices()[iIndex]));

						iIndicesCount += 2;

						iPreviousIndex = iIndex;
					} // for (size_t iIndex = ...
				} // for (size_t iPolygon = ...

				if (!strIndices.empty())
				{
					assert(iIndicesCount <= USHRT_MAX);

					jsStream << "var cohort = {};\n";
					jsStream << "conceptualFacesPolygon.cohorts.push(cohort);\n";

					string strJSON = "{ \"indices\" : [";
					strJSON += strIndices;
					strJSON += "] }";

					jsStream << JSON2GZipBase64JS(strJSON);
					jsStream << "cohort.indices = JSON.parse(json).indices;\n";

					/*
					* Reset
					*/
					iIndicesCount = 0;
					strIndices = "";
				} // if (!strIndices.empty()) ...				
			} // if (iIndicesCount > MAX_INDICES_COUNT)
			else
			{
				string strIndices;
				for (size_t iPolygon = 0; iPolygon < vecConceptualFacesPolygons.size(); iPolygon++)
				{
					int64_t iPreviousIndex = -1;
					for (int64_t iIndex = vecConceptualFacesPolygons[iPolygon].first; iIndex < vecConceptualFacesPolygons[iPolygon].first + vecConceptualFacesPolygons[iPolygon].second; iIndex++)
					{
						if (pInstance->getIndices()[iIndex] < 0)
						{	
							iPreviousIndex = -1;

							continue;
						} // if (pInstance->getIndices()[iIndex] < 0)

						if (iPreviousIndex == -1)
						{
							iPreviousIndex = iIndex;

							continue;
						}

						if (!strIndices.empty())
						{
							strIndices += ",";
						}

						strIndices += to_string((pInstance->getIndices()[iPreviousIndex]));
						strIndices += ",";
						strIndices += to_string((pInstance->getIndices()[iIndex]));

						iIndicesCount += 2;

						iPreviousIndex = iIndex;
					} // for (size_t iIndex = ...
				} // for (size_t iPolygon = ...

				string strJSON = "{ \"indices\" : [";
				strJSON += strIndices;
				strJSON += "] }";

				jsStream << JSON2GZipBase64JS(strJSON);
				jsStream << "conceptualFacesPolygon.indices = JSON.parse(json).indices;\n";
			} // else if (iIndicesCount > MAX_INDICES_COUNT)
		} // else if (pInstance->getVerticesCount() > MAX_INDICES_COUNT)
	} // if (!vecConceptualFacesPolygons.empty())	
	// ********************************************************************************************		

	jsStream << "g_instances.push(instance);\n";

	jsStream.flush();
}

// ------------------------------------------------------------------------------------------------
BIN2JS_API void generateJS(const wchar_t * szBINFile, const wchar_t * szJSFile, bool bMainJS/* = true*/, const wchar_t* szTag/* = NULL*/, const wchar_t* szExtraJS/* = NULL*/)
{
	/*
	* Root path
	*/
#ifndef _DISABLE_BOOST
	fs::path pathRoot(szBINFile);
	g_strInputFolder = pathRoot.parent_path().wstring();
#else
	g_strInputFolder = getPathName(szBINFile);
#endif

	/*
	* Init
	*/
	CRDFModel * pModel = new CRDFModel();
	pModel->Load(szBINFile);

	ofstream jsStream(szJSFile, std::ios::binary);	

	/*
	* Global variables
	*/
	if (bMainJS)
	{
		/*
		* Declare only in the main .js file
		*/
		jsStream << "var g_instances = [];\n";
	}	

	/*
	* Generate .js
	*/
	const map<int64_t, CRDFInstance *> & mapRDFInstances = pModel->GetRDFInstances();

	map<int64_t, CRDFInstance *>::const_iterator itRDFInstances = mapRDFInstances.begin();
	for (; itRDFInstances != mapRDFInstances.end(); itRDFInstances++)
	{
		CRDFInstance * pRDFInstance = itRDFInstances->second;
		if (!pRDFInstance->hasGeometry() || pRDFInstance->isReferenced())
		{
			continue;
		}	

		generateInstanceJS(pRDFInstance, szTag, jsStream);

		if (wcslen(pRDFInstance->getGroup()) > 0)
		{
			g_setGroups.insert(pRDFInstance->getGroup());
		}
	} // for (; itRDFInstances != ...

	/*
	* Groups
	*/
	jsStream << "var g_groups = [];\n";
	for (auto group = g_setGroups.begin(); group != g_setGroups.end(); group++)
	{
		jsStream << "g_groups.push('";
		jsStream << CW2A(group->c_str());
		jsStream << "');\n";
	}		

	/*
	* Extra JS
	*/
	if (szExtraJS != NULL)
	{
		jsStream << CW2A(szExtraJS);
		jsStream << "\n";
	}

	/*
	* Clean up
	*/
	jsStream.close();

	delete pModel;

	g_strInputFolder.clear();
	g_setGroups.clear();
	g_mapTexture2JSVar.clear();
}
