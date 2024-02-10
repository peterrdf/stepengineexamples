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

private: // Members

	double m_dX;
	double m_dY;
	double m_dZ;

	SdaiInstance m_iCartesianPointInstance;

public: // Methods

	_point3d()
		: m_dX(0.)
		, m_dY(0.)
		, m_dZ(0.)
		, m_iCartesianPointInstance(0)
	{}

	double& X() { return m_dX; }
	double& Y() { return m_dY; }
	double& Z() { return m_dZ; }

	SdaiInstance& cartesianPointInstance() { return m_iCartesianPointInstance; }
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

	int_t getIfcModel() const { return m_iIfcModel; }

	int_t getPersonInstance();
	int_t getOrganizationInstance();
	int_t getPersonAndOrganizationInstance();
	int_t getApplicationInstance();
	int_t getOwnerHistoryInstance();
	int_t getDimensionalExponentsInstance();
	int_t getConversionBasedUnitInstance();
	int_t getUnitAssignmentInstance();
	int_t getWorldCoordinateSystemInstance();
	int_t getGeometricRepresentationContextInstance();
	int_t getProjectInstance();	

protected: // Methods

	void createIfcModel(const wchar_t* szSchemaName);

	int_t buildSIUnitInstance(const char* szUnitType, const char* szPrefix, const char* szName);
	int_t buildMeasureWithUnitInstance();
	int_t buildDirectionInstance(double dX, double dY, double dZ);
	int_t buildCartesianPointInstance(double dX, double dY, double dZ);
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