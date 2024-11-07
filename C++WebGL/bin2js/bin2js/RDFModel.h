#pragma once

#include "RDFClass.h"
#include "ObjectRDFProperty.h"
#include "BoolRDFProperty.h"
#include "StringRDFProperty.h"
#include "IntRDFProperty.h"
#include "DoubleRDFProperty.h"
#include "RDFInstance.h"

#include <map>

using namespace std;

// ------------------------------------------------------------------------------------------------
static int flagbit0 = 1;			// 2^^0							 0000.0000..0000.0001
static int flagbit1 = 2;			// 2^^1							 0000.0000..0000.0010
static int flagbit2 = 4;			// 2^^2							 0000.0000..0000.0100
static int flagbit3 = 8;			// 2^^3							 0000.0000..0000.1000

static int flagbit4 = 16;			// 2^^4							 0000.0000..0001.0000
static int flagbit5 = 32;			// 2^^5							 0000.0000..0010.0000
static int flagbit6 = 64;			// 2^^6							 0000.0000..0100.0000
static int flagbit7 = 128;			// 2^^7							 0000.0000..1000.0000

static int flagbit8 = 256;			// 2^^8							 0000.0001..0000.0000
static int flagbit9 = 512;			// 2^^9							 0000.0010..0000.0000
static int flagbit10 = 1024;		// 2^^10						 0000.0100..0000.0000
static int flagbit11 = 2048;		// 2^^11						 0000.1000..0000.0000

static int flagbit12 = 4096;		// 2^^12						 0001.0000..0000.0000
static int flagbit13 = 8192;		// 2^^13						 0010.0000..0000.0000
static int flagbit14 = 16384;		// 2^^14						 0100.0000..0000.0000
static int flagbit15 = 32768;		// 2^^15						 1000.0000..0000.0000

static int flagbit16 = 65536;		// 2^^16   0000.0000..0000.0001  0000.0000..0000.0000
static int flagbit17 = 131072;		// 2^^17   0000.0000..0000.0010  0000.0000..0000.0000
static int flagbit18 = 262144;		// 2^^18   0000.0000..0000.0100  0000.0000..0000.0000
static int flagbit19 = 524288;		// 2^^19   0000.0000..0000.1000  0000.0000..0000.0000

static int flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
static int flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
static int flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000
static int flagbit23 = 8388608;		// 2^^23   0000.0000..1000.0000  0000.0000..0000.0000

static int flagbit24 = 16777216;	// 2^^24   0000.0001..0000.0000  0000.0000..0000.0000
static int flagbit25 = 33554432;	// 2^^25   0000.0010..0000.0000  0000.0000..0000.0000
static int flagbit26 = 67108864;	// 2^^26   0000.0100..0000.0000  0000.0000..0000.0000
static int flagbit27 = 134217728;	// 2^^27   0000.1000..0000.0000  0000.0000..0000.0000

static int flagbit28 = 268435456;	// 2^^28   0001.0000..0000.0000  0000.0000..0000.0000
static int flagbit29 = 536870912;	// 2^^29   0010.0000..0000.0000  0000.0000..0000.0000
static int flagbit30 = 1073741824;	// 2^^30   0100.0000..0000.0000  0000.0000..0000.0000
static int flagbit31 = 2147483648;	// 2^^31   1000.0000..0000.0000  0000.0000..0000.0000

#define EMPTY_INSTANCE L"---<EMPTY>---"

// ------------------------------------------------------------------------------------------------
// Describes an RDF model
class CRDFModel
{

private: // Members

	// --------------------------------------------------------------------------------------------
	// Model
	int64_t m_iModel;

	// --------------------------------------------------------------------------------------------
	// Coordinate system and XY grid
	int64_t m_iCoordinateSystemModel;

	// --------------------------------------------------------------------------------------------
	// Instance : Class
	map<int64_t, CRDFClass *> m_mapRDFClasses;

	// --------------------------------------------------------------------------------------------
	// Instance : Property
	map<int64_t, CRDFProperty *> m_mapRDFProperties;

	// --------------------------------------------------------------------------------------------
	// Instance : Object
	map<int64_t, CRDFInstance *> m_mapRDFInstances;

	// --------------------------------------------------------------------------------------------
	// ID (1-based index)
	int64_t m_iID;

	// --------------------------------------------------------------------------------------------
	// World's dimensions
	float m_fXmin;
	float m_fXmax;
	float m_fYmin;
	float m_fYmax;
	float m_fZmin;
	float m_fZmax;

	// --------------------------------------------------------------------------------------------
	// World's bounding sphere diameter
	float m_fBoundingSphereDiameter;

public: // Methods

	// --------------------------------------------------------------------------------------------
	// ctor
	CRDFModel();

	// --------------------------------------------------------------------------------------------
	// dtor
	virtual ~CRDFModel();

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t GetModel() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	int64_t GetCoordinateSystemModel() const;

	// --------------------------------------------------------------------------------------------
	// Default model
	void CreateDefaultModel();

	// --------------------------------------------------------------------------------------------
	// Getter
	const map<int64_t, CRDFClass *> & GetRDFClasses() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	void GetClassAncestors(int64_t iClassInstance, vector<int64_t> & vecAncestors) const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const map<int64_t, CRDFProperty *> & GetRDFProperties();

	// --------------------------------------------------------------------------------------------
	// Getter
	const map<int64_t, CRDFInstance *> & GetRDFInstances() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	CRDFInstance * GetRDFInstanceByID(int64_t iID);

	// --------------------------------------------------------------------------------------------
	// Factory
	CRDFInstance * CreateNewInstance(int64_t iClassInstance);

	// --------------------------------------------------------------------------------------------
	// Removes an instance
	bool DeleteInstance(CRDFInstance * pInstance);

	// --------------------------------------------------------------------------------------------
	// Support for editing of object properties
	void GetCompatibleInstances(CRDFInstance * pRDFInstance, CObjectRDFProperty * pObjectRDFProperty, vector<int64_t> & vecCompatibleInstances) const;

	// --------------------------------------------------------------------------------------------
	// Getter
	void GetWorldDimensions(float & fXmin, float & fXmax, float & fYmin, float & fYmax, float & fZmin, float & fZmax) const;

	// --------------------------------------------------------------------------------------------
	// Getter
	float GetBoundingSphereDiameter() const;

	// --------------------------------------------------------------------------------------------
	// Zoom to an instance
	void ZoomToInstance(int64_t iInstance);

	// --------------------------------------------------------------------------------------------
	// Edit properties support
	void OnInstancePropertyEdited(CRDFInstance * pInstance, CRDFProperty * pProperty);

	// --------------------------------------------------------------------------------------------
	// Stores model
	void Save(const wchar_t * szPath);

	// --------------------------------------------------------------------------------------------
	// Loads a model
	void Load(const wchar_t * szPath);

private: // Methods

	// --------------------------------------------------------------------------------------------
	void SetFormatSettings(int64_t iModel);

	// --------------------------------------------------------------------------------------------
	// Coordinate system & grid
	void CreateCoordinateSystem();

	// --------------------------------------------------------------------------------------------
	// Loads RDF hierarchy
	void LoadRDFModel();

	// --------------------------------------------------------------------------------------------
	// Loads RDF instances
	void LoadRDFInstances();

	// --------------------------------------------------------------------------------------------
	// Support for textures; https://github.com/mortennobel/OpenGL_3_2_Utils/blob/master/src/TextureLoader.cpp
	// UNUSED
	//unsigned char * LoadBMP(const char * imagepath, unsigned int& outWidth, unsigned int& outHeight, bool flipY);

	// --------------------------------------------------------------------------------------------
	// Clean up
	void Clean();
};

