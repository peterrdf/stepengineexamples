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
class _exporter_base
{

private: // Members

	_gis2ifc* m_pSite;

	SdaiModel m_iIfcModel;
	SdaiInstance m_iProjectInstance;
	SdaiInstance m_iSiteInstance;
	SdaiInstance m_iOwnerHistoryInstance;

public: // Methods

	_exporter_base(_gis2ifc* pSite);
	virtual ~_exporter_base();

	virtual void execute(const wstring& strOuputFile) = 0;

	int_t getIfcModel() const { return m_iIfcModel; }

protected: // Methods

	void createIfcModel(const wchar_t* szSchemaName);
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