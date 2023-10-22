#include "stdafx.h"
#include "Text2RDF.h"

// ------------------------------------------------------------------------------------------------
CText2RDF::CText2RDF(const CString & strText, const CString & strTTFFile, const CString & strRDFFile, int iGeometry)
	: m_iModel(0)
	, m_iLine3DClass(0)
	, m_iPoint3DClass(0)
	, m_iBezierCurveClass(0)
	, m_iCollectionClass(0)
	, m_iPolygon3DClass(0)
	, m_iFace2DSetClass(0)
	, m_iExtrusionAreaSolidSetClass(0)
	, m_iTransformationClass(0)
	, m_iMatrixClass(0)
	, m_vecContours()
	, m_mapLetters()
	, m_pCurrentContour(NULL)
	, m_strText(strText)
	, m_strTTFFile(strTTFFile)
	, m_strRDFFile(strRDFFile)
	, m_iX(0)
	, m_iY(0)
	, m_iOffsetX(0)
	, m_iOffsetY(0)
	, m_iGeometry(iGeometry)
{
}

// ------------------------------------------------------------------------------------------------
CText2RDF::~CText2RDF()
{
}

OwlInstance CText2RDF::Translate(
	OwlInstance iInstance,
	double dX, double dY, double dZ)
{
	ASSERT(iInstance != 0);

	int64_t iMatrixInstance = CreateInstance(m_iMatrixClass);
	ASSERT(iMatrixInstance != 0);

	vector<double> vecTransformationMatrix =
	{
		1., 0., 0.,
		0., 1., 0.,
		0., 0., 1.,
		dX, dY, dZ,
	};

	SetDatatypeProperty(
		iMatrixInstance,
		GetPropertyByName(m_iModel, "coordinates"),
		vecTransformationMatrix.data(),
		vecTransformationMatrix.size());

	int64_t iTransformationInstance = CreateInstance(m_iTransformationClass, "Translate");
	ASSERT(iTransformationInstance != 0);

	SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "matrix"), &iMatrixInstance, 1);
	SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "object"), &iInstance, 1);

	return iTransformationInstance;
}


// ------------------------------------------------------------------------------------------------
void CText2RDF::Run()
{
	assert(!m_strTTFFile.IsEmpty());
	assert(!m_strRDFFile.IsEmpty());

	CString strText = m_strText;
	strText.Trim();

	assert(!strText.IsEmpty());

	m_iOffsetX = 0;
	m_iOffsetY = 0;

	//***************************************************************
	m_iModel = CreateModel();
	assert(m_iModel != 0);

	SetFormatSettings(m_iModel);

	m_iLine3DClass = GetClassByName(m_iModel, "Line3D");
	assert(m_iLine3DClass != 0);

	m_iPoint3DClass = GetClassByName(m_iModel, "Point3D");
	assert(m_iPoint3DClass != 0);

	m_iBezierCurveClass = GetClassByName(m_iModel, "BezierCurve");
	assert(m_iBezierCurveClass != 0);

	m_iCollectionClass = GetClassByName(m_iModel, "Collection");
	assert(m_iCollectionClass != 0);

	m_iPolygon3DClass = GetClassByName(m_iModel, "Polygon3D");
	assert(m_iPolygon3DClass != 0);

	m_iFace2DSetClass = GetClassByName(m_iModel, "Face2DSet");
	assert(m_iFace2DSetClass != 0);

	m_iExtrusionAreaSolidSetClass = GetClassByName(m_iModel, "ExtrusionAreaSolidSet");
	assert(m_iExtrusionAreaSolidSetClass != 0);

	m_iTransformationClass = GetClassByName(m_iModel, "Transformation");
	assert(m_iTransformationClass != 0);

	m_iMatrixClass = GetClassByName(m_iModel, "Matrix");
	assert(m_iMatrixClass != 0);

	// **************************************************************
	FT_Error    error;
	FT_Library  library;
	FT_Face     face;
	FT_UInt     glyph_index;
	FT_Outline *outline;

	error = FT_Init_FreeType(&library);
	assert(error == 0);

	error = FT_New_Face(library, CW2A(m_strTTFFile), 0, &face);
	assert(error == 0);

	FT_Set_Pixel_Sizes(face, 0, 16);

	vector<int64_t> vecFace2DInstances;
	for (size_t iChar = 0; iChar < strText.GetLength(); iChar++)
	{
		FT_ULong charcode = strText[iChar];

		glyph_index = FT_Get_Char_Index(face, charcode);
		assert(glyph_index != 0);

		error = FT_Load_Glyph(face, glyph_index, FT_LOAD_DEFAULT);
		assert(error == 0);

		assert(face->glyph->format == FT_GLYPH_FORMAT_OUTLINE);

		outline = &face->glyph->outline;
		printf("n_contours = %d\n", outline->n_contours);
		printf("n_points   = %d\n", outline->n_points);

		map<wchar_t, vector<CGlyphContour *>>::iterator itLetter = m_mapLetters.find(strText[iChar]);
		if (itLetter == m_mapLetters.end())
		{
			FT_Outline_Funcs outline_funcs = { move_to, line_to, conic_to, cubic_to, 0, 0 };
			FT_Outline_Decompose(outline, &outline_funcs, this);

			if (m_vecContours.empty())
			{
				/*
				* Update offset
				*/
				m_iOffsetX += face->glyph->advance.x;

				continue;
			}
		} // if (itLetter == m_mapLetters.end())	

		switch (m_iGeometry)
		{
		case LINES_AND_CURVES:
		{
			itLetter = m_mapLetters.find(strText[iChar]);
			if (itLetter == m_mapLetters.end())
			{
				/*
				* Create Collection for each letter
				*/
				__int64 iCollectionInstance = CreateInstance(m_iCollectionClass);
				assert(iCollectionInstance != 0);

				m_vecContours[0]->m_iGeometryInstance = iCollectionInstance;
				m_vecContours[0]->m_iOffsetX = m_iOffsetX;

				vector<int64_t> vecLetter;
				for (size_t iContour = 0; iContour < m_vecContours.size(); iContour++)
				{
					for (size_t iInstance = 0; iInstance < m_vecContours[iContour]->m_vecInstances.size(); iInstance++)
					{
						vecLetter.push_back(m_vecContours[iContour]->m_vecInstances[iInstance]);
					}
				} // for (size_t iContour = ...

				SetObjectProperty(iCollectionInstance, GetPropertyByName(m_iModel, "objects"), vecLetter.data(), vecLetter.size());

				m_mapLetters[strText[iChar]] = m_vecContours;
			} // if (itLetter == m_mapLetters.end())
			else
			{
				/*
				* Resuse the Collection
				*/

				// Contour
				CGlyphContour * pContour = itLetter->second[0];
				assert(pContour->m_iGeometryInstance != 0);

				// Transformation
				__int64 iTransformationInstance = CreateInstance(m_iTransformationClass);
				assert(iTransformationInstance != 0);

				// object
				SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "object"), &pContour->m_iGeometryInstance, 1);

				// matrix
				__int64 iMatrixInstance = CreateInstance(m_iMatrixClass);
				assert(iMatrixInstance != 0);

				double _41 = DOUBLE_FROM_26_6(-pContour->m_iOffsetX + m_iOffsetX);
				double _42 = DOUBLE_FROM_26_6(m_iOffsetY);
				SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_41"), &_41, 1);
				SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_42"), &_42, 1);

				SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "matrix"), &iMatrixInstance, 1);
			} // else if (itLetter == m_mapLetters.end())
		} // case LINES_AND_CURVES:
		break;

		case FACE2D_SET:
		{
			itLetter = m_mapLetters.find(strText[iChar]);
			if (itLetter == m_mapLetters.end())
			{
				/*
				* Create Polygon3D and Transformation for each contour
				*/

				// One Transformation per Polygon3D
				vector<int64_t> vecTransformations;
				for (size_t iContour = 0; iContour < m_vecContours.size(); iContour++)
				{
					// Polygon3D
					__int64 iPolygon3DInstance = CreateInstance(m_iPolygon3DClass);
					assert(iPolygon3DInstance != 0);

					m_vecContours[iContour]->m_iGeometryInstance = iPolygon3DInstance;

					SetObjectProperty(iPolygon3DInstance, GetPropertyByName(m_iModel, "lineParts"), m_vecContours[iContour]->m_vecInstances.data(), m_vecContours[iContour]->m_vecInstances.size());

					// Transformation
					__int64 iTransformationInstance = CreateInstance(m_iTransformationClass);
					assert(iTransformationInstance != 0);

					// object
					SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "object"), &iPolygon3DInstance, 1);

					// matrix
					__int64 iMatrixInstance = CreateInstance(m_iMatrixClass);
					assert(iMatrixInstance != 0);

					double _41 = DOUBLE_FROM_26_6(m_vecContours[iContour]->m_iOffsetX + m_iOffsetX);
					double _42 = DOUBLE_FROM_26_6(m_vecContours[iContour]->m_iOffsetY + m_iOffsetY);
					SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_41"), &_41, 1);
					SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_42"), &_42, 1);

					SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "matrix"), &iMatrixInstance, 1);

					vecTransformations.push_back(iTransformationInstance);
				} // for (size_t iContour = ...

				/*
				* Face2DSet
				*/
				__int64 iFace2DSetInstance = CreateInstance(m_iFace2DSetClass);
				assert(iFace2DSetInstance != 0);				

				// polygons
				SetObjectProperty(iFace2DSetInstance, GetPropertyByName(m_iModel, "polygons"), vecTransformations.data(), vecTransformations.size());

				m_vecContours[0]->m_iGeometryInstance = iFace2DSetInstance;
				m_vecContours[0]->m_iOffsetX = m_iOffsetX;

				m_mapLetters[strText[iChar]] = m_vecContours;

				vecFace2DInstances.push_back(iFace2DSetInstance);
			} // if (itLetter == m_mapLetters.end())
			else
			{
				/*
				* Resuse the Face2DSet
				*/

				// Contour
				CGlyphContour * pContour = itLetter->second[0];
				assert(pContour->m_iGeometryInstance != 0);

				// Transformation
				__int64 iTransformationInstance = CreateInstance(m_iTransformationClass);
				assert(iTransformationInstance != 0);

				// object
				SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "object"), &pContour->m_iGeometryInstance, 1);

				// matrix
				__int64 iMatrixInstance = CreateInstance(m_iMatrixClass);
				assert(iMatrixInstance != 0);

				double _41 = DOUBLE_FROM_26_6(-pContour->m_iOffsetX + m_iOffsetX);
				double _42 = DOUBLE_FROM_26_6(m_iOffsetY);
				SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_41"), &_41, 1);
				SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_42"), &_42, 1);

				SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "matrix"), &iMatrixInstance, 1);

				vecFace2DInstances.push_back(iTransformationInstance);
			} // else if (itLetter == m_mapLetters.end())			
		} // case FACE2D_SET:
		break;

		case EXTRSUSION_AREA_SOLID_SET:
		{
			itLetter = m_mapLetters.find(strText[iChar]);
			if (itLetter == m_mapLetters.end())
			{
				/*
				* Create Polygon3D and Transformation for each contour
				*/

				// One Transformation per Polygon3D
				vector<int64_t> vecTransformations;
				for (size_t iContour = 0; iContour < m_vecContours.size(); iContour++)
				{
					// Polygon3D
					__int64 iPolygon3DInstance = CreateInstance(m_iPolygon3DClass);
					assert(iPolygon3DInstance != 0);

					m_vecContours[iContour]->m_iGeometryInstance = iPolygon3DInstance;

					SetObjectProperty(iPolygon3DInstance, GetPropertyByName(m_iModel, "lineParts"), m_vecContours[iContour]->m_vecInstances.data(), m_vecContours[iContour]->m_vecInstances.size());

					// Transformation
					__int64 iTransformationInstance = CreateInstance(m_iTransformationClass);
					assert(iTransformationInstance != 0);

					// object
					SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "object"), &iPolygon3DInstance, 1);

					// matrix
					__int64 iMatrixInstance = CreateInstance(m_iMatrixClass);
					assert(iMatrixInstance != 0);

					double _41 = DOUBLE_FROM_26_6(m_vecContours[iContour]->m_iOffsetX + m_iOffsetX);
					double _42 = DOUBLE_FROM_26_6(m_vecContours[iContour]->m_iOffsetY + m_iOffsetY);
					SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_41"), &_41, 1);
					SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_42"), &_42, 1);

					SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "matrix"), &iMatrixInstance, 1);

					vecTransformations.push_back(iTransformationInstance);
				} // for (size_t iContour = ...

				/*
				* ExtrusionAreaSolidSet
				*/
				__int64 iExtrusionAreaSolidSetInstance = CreateInstance(m_iExtrusionAreaSolidSetClass);
				assert(iExtrusionAreaSolidSetInstance != 0);

				// extrusionAreaSet
				SetObjectProperty(iExtrusionAreaSolidSetInstance, GetPropertyByName(m_iModel, "extrusionAreaSet"), vecTransformations.data(), vecTransformations.size());

				double dExtrusionLength = 1.;
				SetDatatypeProperty(iExtrusionAreaSolidSetInstance, GetPropertyByName(m_iModel, "extrusionLength"), &dExtrusionLength, 1);

				m_vecContours[0]->m_iGeometryInstance = iExtrusionAreaSolidSetInstance;
				m_vecContours[0]->m_iOffsetX = m_iOffsetX;

				m_mapLetters[strText[iChar]] = m_vecContours;
			} // if (itLetter == m_mapLetters.end())
			else
			{
				/*
				* Resuse the ExtrusionAreaSolidSet
				*/

				// Contour
				CGlyphContour * pContour = itLetter->second[0];
				assert(pContour->m_iGeometryInstance != 0);

				// Transformation
				__int64 iTransformationInstance = CreateInstance(m_iTransformationClass);
				assert(iTransformationInstance != 0);

				// object
				SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "object"), &pContour->m_iGeometryInstance, 1);

				// matrix
				__int64 iMatrixInstance = CreateInstance(m_iMatrixClass);
				assert(iMatrixInstance != 0);

				double _41 = DOUBLE_FROM_26_6(-pContour->m_iOffsetX + m_iOffsetX);
				double _42 = DOUBLE_FROM_26_6(m_iOffsetY);
				SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_41"), &_41, 1);
				SetDatatypeProperty(iMatrixInstance, GetPropertyByName(m_iModel, "_42"), &_42, 1);

				SetObjectProperty(iTransformationInstance, GetPropertyByName(m_iModel, "matrix"), &iMatrixInstance, 1);
			} // else if (itLetter == m_mapLetters.end())			
		} // case EXTRSUSION_AREA_SOLID_SET:
		break;

		default:
		{
			assert(false); // unexpected
		}
		break;
		} // switch (m_iGeometry)

		/*
		* Clean up
		*/		
		m_vecContours.clear();

		/*
		* Update offset
		*/
		m_iOffsetX += face->glyph->advance.x;
	} // for (size_t iChar = ...

	/*
	* Clean up
	*/
	FT_Done_Face(face);
	FT_Done_FreeType(library);

	map<wchar_t, vector<CGlyphContour *>>::iterator itLetter = m_mapLetters.begin();
	for (; itLetter != m_mapLetters.end(); itLetter++)
	{
		for (size_t iContour = 0; iContour < itLetter->second.size(); iContour++)
		{
			delete itLetter->second[iContour];
		}
	} // for (; itLetter != ...
	m_mapLetters.clear();

	if (m_iGeometry == FACE2D_SET)
	{
		assert(!vecFace2DInstances.empty());

		__int64 iCollectionInstance = CreateInstance(m_iCollectionClass);
		assert(iCollectionInstance != 0);

		SetObjectProperty(iCollectionInstance, GetPropertyByName(m_iModel, "objects"), vecFace2DInstances.data(), vecFace2DInstances.size());

		double arAABBMin[] = { 0., 0., 0. };
		double arAABBMax[] = { 0., 0., 0. };
		GetBoundingBox(
			iCollectionInstance,
			(double*)&arAABBMin,
			(double*)&arAABBMax);

		Translate(
			iCollectionInstance,
			-(arAABBMin[0] + arAABBMax[0]) / 2., -(arAABBMin[1] + arAABBMax[1]) / 2., -(arAABBMin[2] + arAABBMax[2]) / 2.);
	}	

	/*
	* Save
	*/	
	SetOverrideFileIO(m_iModel, 1 + 16, 7 + 16); // base64
	SaveModelW(m_iModel, (LPCTSTR)m_strRDFFile);

	/*
	* Clean up
	*/
	CloseModel(m_iModel);
	m_iModel = 0;	
}

// ------------------------------------------------------------------------------------------------
void CText2RDF::SetFormatSettings(__int64 iModel)
{
	// X, Y, Z, Nx, Ny, Nz, Tx, Ty, Ambient, Diffuse, Emissive, Specular, Tnx, Tny, Tnz, Bnx, Bny, Bnz
	// (Tx, Ty - bit 6; Normal vectors - bit 5, Diffuse, Emissive, Specular - bit 25, 26 & 27, Tangent vectors - bit 28, Binormal vectors - bit 29)
	string strSettings = "111111000000001011000001110001";

	bitset<64> bitSettings(strSettings);
	int64_t iSettings = bitSettings.to_ulong();

	string strMask = "11111111111111111011011101110111";
	bitset <64> bitMask(strMask);
	int64_t iMask = bitMask.to_ulong();

	int64_t iVertexLength = SetFormat(iModel, (int64_t)iSettings, (int64_t)iMask);
}

// ------------------------------------------------------------------------------------------------
/*static*/ int CText2RDF::move_to(const FT_Vector *to, void *user)
{
	assert(user != NULL);
	CText2RDF * pData = (CText2RDF *)user;

	printf("move_to([%g, %g])\n",
		DOUBLE_FROM_26_6(to->x), DOUBLE_FROM_26_6(to->y));

	pData->m_iX = to->x;
	pData->m_iY = to->y;

	pData->m_pCurrentContour = new CGlyphContour();
	pData->m_pCurrentContour->m_iOffsetX = to->x;
	pData->m_pCurrentContour->m_iOffsetY = to->y;

	pData->m_vecContours.push_back(pData->m_pCurrentContour);

	return 0;
}

// ------------------------------------------------------------------------------------------------
/*static*/ int CText2RDF::line_to(const FT_Vector *to, void *user)
{
	assert(user != NULL);
	CText2RDF * pData = (CText2RDF *)user;

	printf("line_to([%g, %g])\n", DOUBLE_FROM_26_6(to->x), DOUBLE_FROM_26_6(to->y));	

	double dX = DOUBLE_FROM_26_6(to->x + pData->m_iOffsetX);
	double dY = DOUBLE_FROM_26_6(to->y);
	double dZ = 0.;

	__int64 iLine3DInstance = CreateInstance(pData->m_iLine3DClass);
	assert(iLine3DInstance != 0);

	pData->m_pCurrentContour->m_vecInstances.push_back(iLine3DInstance);

	vector<double> vecPoints;
	vecPoints.push_back(DOUBLE_FROM_26_6(pData->m_iX + pData->m_iOffsetX));
	vecPoints.push_back(DOUBLE_FROM_26_6(pData->m_iY));
	vecPoints.push_back(dZ);

	vecPoints.push_back(dX);
	vecPoints.push_back(dY);
	vecPoints.push_back(dZ);

	SetDatatypeProperty(iLine3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), 6);

	pData->m_iX = to->x;
	pData->m_iY = to->y;

	return 0;
}

// ------------------------------------------------------------------------------------------------
/*static*/ int CText2RDF::conic_to(const FT_Vector *control, const FT_Vector *to, void *user)
{
	assert(user != NULL);
	CText2RDF * pData = (CText2RDF *)user;

	printf("conic_to(c = [%g, %g], to = [%g, %g])\n",
		DOUBLE_FROM_26_6(control->x), DOUBLE_FROM_26_6(control->y),
		DOUBLE_FROM_26_6(to->x), DOUBLE_FROM_26_6(to->y));

	double dX1 = DOUBLE_FROM_26_6(control->x + pData->m_iOffsetX);
	double dY1 = DOUBLE_FROM_26_6(control->y);
	double dX2 = DOUBLE_FROM_26_6(to->x + pData->m_iOffsetX);
	double dY2 = DOUBLE_FROM_26_6(to->y);
	double dZ = 0.;

	__int64 iBezierCurveInstance = CreateInstance(pData->m_iBezierCurveClass);
	assert(iBezierCurveInstance != 0);

	pData->m_pCurrentContour->m_vecInstances.push_back(iBezierCurveInstance);

	vector<int64_t> vecInstances;

	/*
	* Point 1
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(DOUBLE_FROM_26_6(pData->m_iX + pData->m_iOffsetX));
		vecPoints.push_back(DOUBLE_FROM_26_6(pData->m_iY));
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	/*
	* Point 2
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(dX1);
		vecPoints.push_back(dY1);
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	/*
	* Point 3
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(dX2);
		vecPoints.push_back(dY2);
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	SetObjectProperty(iBezierCurveInstance, GetPropertyByName(pData->m_iModel, "controlPoints"), vecInstances.data(), vecInstances.size());

	pData->m_iX = to->x;
	pData->m_iY = to->y;

	return 0;
}

// ------------------------------------------------------------------------------------------------
/*static*/ int CText2RDF::cubic_to(const FT_Vector *control1, const FT_Vector *control2,
	const FT_Vector *to, void *user)
{
	assert(user != NULL);
	CText2RDF * pData = (CText2RDF *)user;

	printf("cubic_to(c1 = [%g, %g], c2 = [%g, %g], to = [%g, % g])\n",
		DOUBLE_FROM_26_6(control1->x), DOUBLE_FROM_26_6(control1->y),
		DOUBLE_FROM_26_6(control2->x), DOUBLE_FROM_26_6(control2->y),
		DOUBLE_FROM_26_6(to->x), DOUBLE_FROM_26_6(to->y));

	double dX1 = DOUBLE_FROM_26_6(control1->x + pData->m_iOffsetX);
	double dY1 = DOUBLE_FROM_26_6(control1->y);
	double dX2 = DOUBLE_FROM_26_6(control2->x + pData->m_iOffsetX);
	double dY2 = DOUBLE_FROM_26_6(control2->y);
	double dX3 = DOUBLE_FROM_26_6(to->x + pData->m_iOffsetX);
	double dY3 = DOUBLE_FROM_26_6(to->y);
	double dZ = 0.;

	__int64 iBezierCurveInstance = CreateInstance(pData->m_iBezierCurveClass);
	assert(iBezierCurveInstance != 0);

	pData->m_pCurrentContour->m_vecInstances.push_back(iBezierCurveInstance);

	vector<int64_t> vecInstances;

	/*
	* Point 1
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(DOUBLE_FROM_26_6(pData->m_iX + pData->m_iOffsetX));
		vecPoints.push_back(DOUBLE_FROM_26_6(pData->m_iY));
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	/*
	* Point 2
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(dX1);
		vecPoints.push_back(dY1);
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	/*
	* Point 3
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(dX2);
		vecPoints.push_back(dY2);
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	/*
	* Point 4
	*/
	{
		__int64 iPoint3DInstance = CreateInstance(pData->m_iPoint3DClass);
		assert(iPoint3DInstance != 0);

		vector<double> vecPoints;
		vecPoints.push_back(dX3);
		vecPoints.push_back(dY3);
		vecPoints.push_back(dZ);

		SetDatatypeProperty(iPoint3DInstance, GetPropertyByName(pData->m_iModel, "points"), vecPoints.data(), vecPoints.size());

		vecInstances.push_back(iPoint3DInstance);
	}

	SetObjectProperty(iBezierCurveInstance, GetPropertyByName(pData->m_iModel, "controlPoints"), vecInstances.data(), vecInstances.size());

	pData->m_iX = to->x;
	pData->m_iY = to->y;

	return 0;
}