#ifndef IFCFILEPARSER_H
#define IFCFILEPARSER_H

//#include "IFCObject.h"

#include <string>
#include <map>
#include <engdef.h>

using namespace std;


//static const char IFCSchema_IFC2x3_TC1[] = "IFC2X3_TC1.exp";
//static const char IFCSchema_IFC2x3_Settings[] = "IFC2X3-Settings.xml";
//static const char IFCSchema_IFC4_ADD2[] = "IFC4_ADD2.exp";
//static const char IFCSchema_IFC4x1_FINAL[] = "IFC4x1_FINAL.exp";
//static const char IFCSchema_IFC4_Settings[] = "IFC4-Settings.xml";

static const int_t flagbit0 = 1; // 2^^0 0000.0000..0000.0001
static const int_t flagbit1 = 2; // 2^^1 0000.0000..0000.0010
static const int_t flagbit2 = 4; // 2^^2 0000.0000..0000.0100
static const int_t flagbit3 = 8; // 2^^3 0000.0000..0000.1000
static const int_t flagbit4 = 16; // 2^^4 0000.0000..0001.0000
static const int_t flagbit5 = 32; // 2^^5 0000.0000..0010.0000
static const int_t flagbit6 = 64; // 2^^6 0000.0000..0100.0000
static const int_t flagbit7 = 128; // 2^^7 0000.0000..1000.0000
static const int_t flagbit8 = 256; // 2^^8 0000.0001..0000.0000
static const int_t flagbit9 = 512; // 2^^9 0000.0010..0000.0000
static const int_t flagbit10 = 1024; // 2^^10 0000.0100..0000.0000
static const int_t flagbit11 = 2048; // 2^^11 0000.1000..0000.0000
static const int_t flagbit12 = 4096; // 2^^12 0001.0000..0000.0000
static const int_t flagbit13 = 8192; // 2^^13 0010.0000..0000.0000
static const int_t flagbit14 = 16384; // 2^^14 0100.0000..0000.0000
static const int_t flagbit15 = 32768; // 2^^15 1000.0000..0000.0000
static const int_t flagbit17 = 131072; // 2^^15 1000.0000..0000.0000
static const uint64_t	flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
static const uint64_t	flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
static const uint64_t	flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000
static const uint64_t	flagbit23 = 8388608;		// 2^^23   0000.0000..1000.0000  0000.0000..0000.0000
static const int_t flagbit24 = 16777216;
static const int_t flagbit25 = 33554432;
static const int_t flagbit26 = 67108864;
static const int_t flagbit27 = 134217728;


// ------------------------------------------------------------------------------------------------
// Parser for IFC files
class CIFCModel
{

private: // Members

	// --------------------------------------------------------------------------------------------
	// Entities
	int_t m_ifcProjectEntity;
	int_t m_ifcSpaceEntity;
	int_t m_ifcOpeningElementEntity;
	int_t m_ifcDistributionElementEntity;
	int_t m_ifcElectricalElementEntity;
	int_t m_ifcElementAssemblyEntity;
	int_t m_ifcElementComponentEntity;
	int_t m_ifcEquipmentElementEntity;
	int_t m_ifcFeatureElementEntity;
	int_t m_ifcFeatureElementSubtractionEntity;
	int_t m_ifcFurnishingElementEntity;
	int_t m_ifcReinforcingElementEntity;
	int_t m_ifcTransportElementEntity;
	int_t m_ifcVirtualElementEntity;

	// --------------------------------------------------------------------------------------------
	// Input file
	string m_strIFCFile;

	// --------------------------------------------------------------------------------------------
	// Model
	int_t m_iIFCModel;

	// --------------------------------------------------------------------------------------------
	// Min/Max
	pair<float, float> m_prXMinMax;
	pair<float, float> m_prYMinMax;
	pair<float, float> m_prZMinMax;

	// --------------------------------------------------------------------------------------------
	// Bounding sphere diameter
	float m_fBoundingSphereDiameter;

	// --------------------------------------------------------------------------------------------
	// CIFCObject-s
	//vector<CIFCObject *> m_vecIFCObjects;
public: // TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	int_t m_iIFCObjectCount;
private:
	// --------------------------------------------------------------------------------------------
	// Unique index for each object
	static int_t s_iObjectID;

public: // Methods

	// --------------------------------------------------------------------------------------------
	// ctor
	CIFCModel(const string & strIFCFile);

	// --------------------------------------------------------------------------------------------
	// dtor
	virtual ~CIFCModel();

	// --------------------------------------------------------------------------------------------
	// Loads and IFC file
    bool Load();

	// --------------------------------------------------------------------------------------------
	// Getter
	int_t getModel() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const char * getModelName() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const pair<float, float> & getXMinMax() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const pair<float, float> & getYMinMax() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	const pair<float, float> & getZMinMax() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	float getBoundingSphereDiameter() const;

	// --------------------------------------------------------------------------------------------
	// Getter
	//const vector<CIFCObject *> & getIFCObjects();

private: // Methods		

	// --------------------------------------------------------------------------------------------
	// Retrieves IFC objects by Entity
	void RetrieveObjects__depth(int_t iParentEntity, int_t iCircleSegments, int_t depth);

	// --------------------------------------------------------------------------------------------
	// Retrieves IFC objects by Entity
	void RetrieveObjects(const char * szEntityName, const wchar_t * szEntityNameW, int_t iCircleSegements);

	// --------------------------------------------------------------------------------------------
	// Retrieves the geometry for an IFC object
	//CIFCObject * RetrieveGeometry(const wchar_t * szInstanceGUIDW, const wchar_t * szEntityNameW, int_t iInstance, int_t iCircleSegments);
};

#endif // IFCFILEPARSER_H
