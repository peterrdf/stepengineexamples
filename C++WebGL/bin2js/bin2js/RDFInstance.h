#pragma once

#include "Generic.h"

#include <string>
#include <vector>

using namespace std;

// ------------------------------------------------------------------------------------------------
struct VECTOR3
{
	double x;
	double y;
	double z;
};

// ------------------------------------------------------------------------------------------------
struct MATRIX
{
	double _11, _12, _13;
	double _21, _22, _23;
	double _31, _32, _33;
	double _41, _42, _43;
};

// ------------------------------------------------------------------------------------------------
static void	Transform(const VECTOR3 * pV, const MATRIX *pM, VECTOR3 * pOut)
{
	VECTOR3	pTmp;
	pTmp.x = pV->x * pM->_11 + pV->y * pM->_21 + pV->z * pM->_31 + pM->_41;
	pTmp.y = pV->x * pM->_12 + pV->y * pM->_22 + pV->z * pM->_32 + pM->_42;
	pTmp.z = pV->x * pM->_13 + pV->y * pM->_23 + pV->z * pM->_33 + pM->_43;

	pOut->x = pTmp.x;
	pOut->y = pTmp.y;
	pOut->z = pTmp.z;
}

// ------------------------------------------------------------------------------------------------
class CRDFColor
{

private: // Members

	// ------------------------------------------------------------------------------------------------
	unsigned int m_iColor;

	// ------------------------------------------------------------------------------------------------
	float m_fR;

	// ------------------------------------------------------------------------------------------------
	float m_fG;

	// ------------------------------------------------------------------------------------------------
	float m_fB;

public: // Methods

	// ------------------------------------------------------------------------------------------------
	// ctor
	CRDFColor()
		: m_iColor(0)
		, m_fR(0.f)
		, m_fG(0.f)
		, m_fB(0.f)
	{
	}

	// ------------------------------------------------------------------------------------------------
	// Set up
	void Init(unsigned int iColor)
	{
		m_iColor = iColor;

		m_fR = (float)(iColor & ((unsigned int)255 * 256 * 256 * 256)) / (256 * 256 * 256);
		m_fR /= 255.f;

		m_fG = (float)(iColor & (255 * 256 * 256)) / (256 * 256);
		m_fG /= 255.f;

		m_fB = (float)(iColor & (255 * 256)) / 256;
		m_fB /= 255.f;
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	float R() const
	{
		return m_fR;
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	float G() const
	{
		return m_fG;
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	float B() const
	{
		return m_fB;
	}

	// ------------------------------------------------------------------------------------------------
	// operator
	bool operator == (const CRDFColor& c) const
	{
		return (m_iColor == c.m_iColor);
	}

	// ------------------------------------------------------------------------------------------------
	// operator
	bool operator < (const CRDFColor& c) const
	{
		return (m_iColor < c.m_iColor);
	}

	// ------------------------------------------------------------------------------------------------
	// operator
	bool operator > (const CRDFColor& c) const
	{
		return (m_iColor > c.m_iColor);
	}
};

// ------------------------------------------------------------------------------------------------
class CRDFMaterial
{

private: // Members

	// --------------------------------------------------------------------------------------------
	// Color
	CRDFColor m_clrAmbient;

	// --------------------------------------------------------------------------------------------
	// Color
	CRDFColor m_clrDiffuse;

	// --------------------------------------------------------------------------------------------
	// Color
	CRDFColor m_clrEmissive;

	// --------------------------------------------------------------------------------------------
	// Color
	CRDFColor m_clrSpecular;

	// ------------------------------------------------------------------------------------------------
	float m_fA;

	// ------------------------------------------------------------------------------------------------
	// Texture
	bool m_bHasTexture;

	// ------------------------------------------------------------------------------------------------
	// Texture
	wstring m_strTexture;

public: // Methods

	// ------------------------------------------------------------------------------------------------
	// ctor
	CRDFMaterial(unsigned int iAmbientColor, unsigned int iDiffuseColor, unsigned int iEmissiveColor, unsigned int iSpecularColor, float fTransparency)
		: m_clrAmbient()
		, m_clrDiffuse()
		, m_clrEmissive()
		, m_clrSpecular()
		, m_fA(1.f)
		, m_bHasTexture(false)
		, m_strTexture(L"")
	{
		if ((iAmbientColor == 0) && (iDiffuseColor == 0) && (iEmissiveColor == 0) && (iSpecularColor == 0) && (fTransparency == 0.f))
		{
			/*
			* There is no material - use non-transparent black
			*/
			m_clrAmbient.Init(0);
			m_clrDiffuse.Init(0);
			m_clrEmissive.Init(0);
			m_clrSpecular.Init(0);

			m_fA = 1.f;
		}
		else
		{
			m_clrAmbient.Init(iAmbientColor);
			m_clrDiffuse.Init(iDiffuseColor == 0 ? iAmbientColor : iDiffuseColor);
			m_clrEmissive.Init(iEmissiveColor);
			m_clrSpecular.Init(iSpecularColor);

			m_fA = fTransparency;
		}
	}

	// --------------------------------------------------------------------------------------------
	// Getter
	const CRDFColor & getAmbientColor() const
	{
		return m_clrAmbient;
	}

	// --------------------------------------------------------------------------------------------
	// Getter
	const CRDFColor & getDiffuseColor() const
	{
		return m_clrDiffuse;
	}

	// --------------------------------------------------------------------------------------------
	// Getter
	const CRDFColor & getEmissiveColor() const
	{
		return m_clrEmissive;
	}

	// --------------------------------------------------------------------------------------------
	// Getter
	const CRDFColor & getSpecularColor() const
	{
		return m_clrSpecular;
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	float A() const
	{
		return m_fA;
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	bool hasTexture() const
	{
		return m_bHasTexture;
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getTexture() const
	{
		return m_strTexture.c_str();
	}

	// ------------------------------------------------------------------------------------------------
	// Getter
	void setTexture(const wstring & strTexture)
	{
		m_bHasTexture = true;

		m_strTexture = strTexture;
	}
};

// ------------------------------------------------------------------------------------------------
class CRDFMaterialComparator
{

public: 

	// --------------------------------------------------------------------------------------------
	bool operator()(const CRDFMaterial &left, const CRDFMaterial &right) const
	{
		if (left.hasTexture() && right.hasTexture())
		{
			return wstring(left.getTexture()) < wstring(right.getTexture());
		}

		if (left.getAmbientColor() < right.getAmbientColor())
		{
			return true;
		}

		if (left.getAmbientColor() > right.getAmbientColor())
		{
			return false;
		}

		if (left.getDiffuseColor() < right.getDiffuseColor())
		{
			return true;
		}

		if (left.getDiffuseColor() > right.getDiffuseColor())
		{
			return false;
		}

		if (left.getEmissiveColor() < right.getEmissiveColor())
		{
			return true;
		}

		if (left.getEmissiveColor() > right.getEmissiveColor())
		{
			return false;
		}

		if (left.getSpecularColor() < right.getSpecularColor())
		{
			return true;
		}

		if (left.getSpecularColor() > right.getSpecularColor())
		{
			return false;
		}

		if (left.A() < right.A())
		{
			return true;
		}

		if (left.A() > right.A())
		{
			return false;
		}

		return false;
	}
};

// ------------------------------------------------------------------------------------------------
class CRDFInstance
{

private: // Members

	// --------------------------------------------------------------------------------------------
	// ID (1-based index)
	int64_t m_iID;

	// --------------------------------------------------------------------------------------------
	// Model
	int64_t m_iModel;

	// --------------------------------------------------------------------------------------------
	// RDF Instance
	int64_t m_iInstance;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strName;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strUniqueName;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strGUID;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strGroup;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strPropertyName;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strPropertyDescription;

	// --------------------------------------------------------------------------------------------
	// Name
	wstring m_strPropertyExpressID;

	// --------------------------------------------------------------------------------------------
	// Indices
	int64_t m_iIndicesCount;

	// --------------------------------------------------------------------------------------------
	// Indices
	int32_t * m_pIndices;

	// --------------------------------------------------------------------------------------------
	// Indices
	int64_t m_iVerticesCount;

	// --------------------------------------------------------------------------------------------
	// Vertices
	float * m_pVertices;

	// --------------------------------------------------------------------------------------------
	// Conceptual faces count
	int64_t m_iConceptualFacesCount;

	// --------------------------------------------------------------------------------------------
	// Triangles
	vector<pair<int64_t, int64_t> > m_vecTriangles;

	// --------------------------------------------------------------------------------------------
	// Lines
	vector<pair<int64_t, int64_t> > m_vecLines;

	// --------------------------------------------------------------------------------------------
	// Points
	vector<pair<int64_t, int64_t> > m_vecPoints;

	// --------------------------------------------------------------------------------------------
	// Faces polygons
	vector<pair<int64_t, int64_t> > m_vecFacesPolygons;

	// --------------------------------------------------------------------------------------------
	// Conceptual faces polygons
	vector<pair<int64_t, int64_t> > m_vecConceptualFacesPolygons;

	// --------------------------------------------------------------------------------------------
	// Materials
	vector<CRDFMaterial *> m_vecConceptualFacesMaterials;

	// --------------------------------------------------------------------------------------------
	// Bounding box - X/Y/Z min
	VECTOR3 * m_vecBoundingBoxMin;

	// --------------------------------------------------------------------------------------------
	// Bounding box - X/Y/Z max
	VECTOR3 * m_vecBoundingBoxMax;

	// --------------------------------------------------------------------------------------------
	// Bounding box - Transformation matrix
	MATRIX * m_mtxBoundingBoxTransformation;

	// --------------------------------------------------------------------------------------------
	// Enable/Disable
	bool m_bEnable;

	// --------------------------------------------------------------------------------------------
	// The data is out of date
	bool m_bNeedsRefresh;

public: // Methods

	// --------------------------------------------------------------------------------------------
	// ctor
	CRDFInstance(int64_t iID, int64_t iModel, int64_t iInstance);

	// --------------------------------------------------------------------------------------------
	// dtor
	virtual ~CRDFInstance();

	// --------------------------------------------------------------------------------------------
	// Reloads the data
	void Recalculate();

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t getID() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t GetModel() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t getInstance() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t getClassInstance() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getName() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getUniqueName() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getGUID() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getGroup() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getPropertyName() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getPropertyDescription() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const wchar_t * getPropertyExpressID() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	bool isReferenced() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	bool hasGeometry() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int32_t * getIndices() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t getIndicesCount() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	float * getVertices() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t getVerticesCount() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t getConceptualFacesCount() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const vector<pair<int64_t, int64_t> > & getTriangles() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const vector<pair<int64_t, int64_t> > & getLines() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const vector<pair<int64_t, int64_t> > & getPoints() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const vector<pair<int64_t, int64_t> > & getFacesPolygons() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const vector<pair<int64_t, int64_t> > & getConceptualFacesPolygons() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const vector<CRDFMaterial *> & getConceptualFacesMaterials() const;

	// --------------------------------------------------------------------------------------------
	bool hasTexture() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	VECTOR3 * getBoundingBoxMin() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	VECTOR3 * getBoundingBoxMax() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	MATRIX * getBoundingBoxTransformation() const;

	// --------------------------------------------------------------------------------------------
	// Setter
	void setEnable(bool bEnable);

	// --------------------------------------------------------------------------------------------
	// Getter
	bool getEnable() const;

	// --------------------------------------------------------------------------------------------
	// Helper
	void CalculateMinMax(float & fXmin, float & fXmax, float & fYmin, float & fYmax, float & fZmin, float & fZmax);

	// --------------------------------------------------------------------------------------------
	// Helper
	void Center(float fXmin, float fXmax, float fYmin, float fYmax, float fZmin, float fZmax);

private: // Methods

	// --------------------------------------------------------------------------------------------
	// Load indices and vertices
	void Calculate();

	// --------------------------------------------------------------------------------------------
	// Release the memory
	void Clean();
};

// ------------------------------------------------------------------------------------------------
struct SORT_RDFINSTANCES
{
	bool operator()(const CRDFInstance * a, const CRDFInstance * b) const
	{
		return wcscmp(a->getName(), b->getName()) < 0;
	}
};

