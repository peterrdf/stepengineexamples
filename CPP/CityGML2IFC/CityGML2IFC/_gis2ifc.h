#pragma once

#include "engine.h"
#include "gisengine.h"
#include "ifcengine.h"

#include "_guid.h"

#include <string>
#include <chrono>
#include <ctime>
#include <iomanip>
#include <sstream>
#include <bitset>
#include <algorithm>
#include <iostream>
#include <fstream>
#include <time.h>
#include <map>
using namespace std;

// ************************************************************************************************
class _gis2ifc
{

private: // Members

	wstring m_strRootFolder;
	_log_callback m_pLogCallback;
	OwlModel m_iOwlModel;

public: // Methods

	_gis2ifc(const wstring& strRootFolder, _log_callback pLogCallback);
	virtual ~_gis2ifc();

	void execute(const wstring& strInputFile, const wstring& strOuputFile);

	// Log
	static string dateTimeStamp();
	static string addDateTimeStamp(const string& strInput);
	void logWrite(enumLogEvent enLogEvent, const string& strEvent);
	void logInfo(const string& strEvent) { logWrite(enumLogEvent::info, strEvent); }
	void logWarn(const string& strEvent) { logWrite(enumLogEvent::warning, strEvent); }
	void logErr(const string& strEvent) { logWrite(enumLogEvent::error, strEvent); }

	OwlModel getOwlModel() const { return m_iOwlModel; }

private: // Methods

	void setFormatSettings(OwlModel iOwlModel);
};

// ************************************************************************************************
class _point3d 
{

public: // Members

	double X;
	double Y;
	double Z;

	SdaiInstance CartesianPointInstance;

public: // Methods

	_point3d()
		: X(0.)
		, Y(0.)
		, Z(0.)
		, CartesianPointInstance(0)
	{}
};

// ************************************************************************************************
class _matrix
{

public: // Members

	double _11;
	double _12;
	double _13;
	double _21;
	double _22;
	double _23;
	double _31;
	double _32;
	double _33;
	double _41;
	double _42;
	double _43;

public: // Methods

	_matrix::_matrix()
		: _11(1.)
		, _12(0.)
		, _13(0.)
		, _21(0.)
		, _22(1.)
		, _23(0.)
		, _31(0.)
		, _32(0.)
		, _33(1.)
		, _41(0.)
		, _42(0.)
		, _43(0.)
	{}

	_matrix::~_matrix()
	{}
};

// ************************************************************************************************
class _exporter_base
{

private: // Members

	_gis2ifc* m_pSite;

	SdaiModel m_iIfcModel;	
	SdaiInstance m_iSiteInstance;
	SdaiInstance m_iPersonInstance;
	SdaiInstance m_iOrganizationInstance;
	SdaiInstance m_iPersonAndOrganizationInstance;	
	SdaiInstance m_iApplicationInstance;
	SdaiInstance m_iOwnerHistoryInstance;
	SdaiInstance m_iDimensionalExponentsInstance;
	SdaiInstance m_iConversionBasedUnitInstance;
	SdaiInstance m_iUnitAssignmentInstance;
	SdaiInstance m_iWorldCoordinateSystemInstance;
	SdaiInstance m_iGeometricRepresentationContextInstance;
	SdaiInstance m_iProjectInstance;	

public: // Methods

	_exporter_base(_gis2ifc* pSite);
	virtual ~_exporter_base();

	virtual void execute(const wstring& strOuputFile) = 0;

	SdaiModel getIfcModel() const { return m_iIfcModel; }
	SdaiInstance getPersonInstance();
	SdaiInstance getOrganizationInstance();
	SdaiInstance getPersonAndOrganizationInstance();
	SdaiInstance getApplicationInstance();
	SdaiInstance getOwnerHistoryInstance();
	SdaiInstance getDimensionalExponentsInstance();
	SdaiInstance getConversionBasedUnitInstance();
	SdaiInstance getUnitAssignmentInstance();
	SdaiInstance getWorldCoordinateSystemInstance();
	SdaiInstance getGeometricRepresentationContextInstance();
	SdaiInstance getProjectInstance();

protected: // Methods

	void createIfcModel(const wchar_t* szSchemaName);

	SdaiInstance buildSIUnitInstance(const char* szUnitType, const char* szPrefix, const char* szName);
	SdaiInstance buildMeasureWithUnitInstance();
	SdaiInstance buildDirectionInstance(double dX, double dY, double dZ);
	SdaiInstance buildCartesianPointInstance(double dX, double dY, double dZ);
	SdaiInstance buildSiteInstance(_matrix* pMatrix, SdaiInstance& iSiteInstancePlacement);
	SdaiInstance buildLocalPlacementInstance(_matrix* pMatrix, SdaiInstance iPlacementRelativeTo);
	SdaiInstance buildAxis2Placement3DInstance(_matrix* pMatrix);
	SdaiInstance buildBuildingInstance(_matrix* pMatrix, SdaiInstance iPlacementRelativeTo, SdaiInstance& iBuildingInstancePlacement);
};

// ************************************************************************************************
class _citygml_exporter : public _exporter_base
{

private: // Members

public: // Methods

	_citygml_exporter(_gis2ifc* pSite);
	virtual ~_citygml_exporter();

	virtual void execute(const wstring& strOuputFile) override;

private: // Methods

};