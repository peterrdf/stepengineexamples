#include "pch.h"
#include "_gis2ifc.h"

// ************************************************************************************************
_gis2ifc::_gis2ifc(const wstring& strRootFolder, _log_callback pLogCallback)
	: m_strRootFolder(strRootFolder)
	, m_pLogCallback(pLogCallback)
	, m_iOwlModel(0)
{
	assert(!m_strRootFolder.empty());
	assert(m_pLogCallback != nullptr);

	SetGISOptionsW(strRootFolder.c_str(), true, m_pLogCallback);
}

/*virtual*/ _gis2ifc::~_gis2ifc()
{
	if (m_iOwlModel != 0)
	{
		CloseModel(m_iOwlModel);
		m_iOwlModel = 0;
	}
}

void _gis2ifc::execute(const wstring& strInputFile, const wstring& strOuputFile)
{
	assert(!strInputFile.empty());
	assert(!strOuputFile.empty());

	/* Import */
	if (m_iOwlModel != 0)
	{
		CloseModel(m_iOwlModel);
		m_iOwlModel = 0;
	}

	m_iOwlModel = CreateModel();
	assert(m_iOwlModel != 0);

	setFormatSettings(m_iOwlModel);

	OwlInstance iRootInstance = ImportGISModelW(m_iOwlModel, strInputFile.c_str());
	if (iRootInstance != 0)
	{
		if (IsCityGML(m_iOwlModel))
		{
			_citygml_exporter exporter(this);
			exporter.execute(strOuputFile);
		}
		else
		{
			logErr("Not supported format.");
		}
	} // if (iRootInstance != 0)
	else
	{
		logErr("Import failed.");
	}
}

/*static*/ string _gis2ifc::dateTimeStamp()
{
	auto timePointNow = chrono::system_clock::now();
	auto timeNow = chrono::system_clock::to_time_t(timePointNow);
	auto timeNowMS = chrono::duration_cast<chrono::milliseconds>(timePointNow.time_since_epoch()) % 1000;

	stringstream ss;
	ss << put_time(localtime(&timeNow), "%Y-%m-%d %H:%M:%S.");
	ss << setfill('0') << setw(3) << timeNowMS.count();

	return ss.str();
}

/*static*/ string _gis2ifc::addDateTimeStamp(const string& strInput)
{
	string strInputCopy = dateTimeStamp();
	strInputCopy += ": ";
	strInputCopy += strInput;

	return strInputCopy;
}

void _gis2ifc::logWrite(enumLogEvent enLogEvent, const string& strEvent)
{
	(*m_pLogCallback)(enLogEvent, addDateTimeStamp(strEvent).c_str());
}

void _gis2ifc::setFormatSettings(OwlModel iOwlModel)
{
	string strSettings = "111111000000001111000001110001";

	bitset<64> bitSettings(strSettings);
	int64_t iSettings = bitSettings.to_ulong();

	string strMask = "11111111111111111111011101110111";
	bitset<64> bitMask(strMask);
	int64_t iMask = bitMask.to_ulong();

	SetFormat(iOwlModel, (int64_t)iSettings, (int64_t)iMask);

	SetBehavior(iOwlModel, 2048 + 4096, 2048 + 4096);
}

// ************************************************************************************************
_exporter_base::_exporter_base(_gis2ifc* pSite)
	: m_pSite(pSite)
	, m_iIfcModel(0)
	, m_iProjectInstance(0)
	, m_iSiteInstance(0)
	, m_iOwnerHistoryInstance(0)
{
	assert(m_pSite != nullptr);
}

/*virtual*/ _exporter_base::~_exporter_base()
{
	if (m_iIfcModel != 0)
	{
		sdaiCloseModel(m_iIfcModel);
		m_iIfcModel = 0;
	}
}

void _exporter_base::createIfcModel(const wchar_t* szSchemaName)
{
	assert(szSchemaName != nullptr);

	if (m_iIfcModel != 0)
	{
		sdaiCloseModel(m_iIfcModel);
		m_iIfcModel = 0;
	}

	m_iIfcModel = sdaiCreateModelBNUnicode(1, NULL, szSchemaName);
	assert(m_iIfcModel != 0);

	/*ifcProjectInstance = getProjectInstance(lengthUnitConversion);
	ifcSiteInstance = buildSiteInstance(&matrix, NULL, &ifcSiteInstancePlacement);
	ifcBuildingInstance = buildBuildingInstance(&matrix, ifcSiteInstancePlacement, &ifcBuildingInstancePlacement);*/
}

// ************************************************************************************************
_citygml_exporter::_citygml_exporter(_gis2ifc* pSite)
	: _exporter_base(pSite)
{}

/*virtual*/ _citygml_exporter::~_citygml_exporter()
{}

/*virtual*/ void _citygml_exporter::execute(const wstring& strOuputFile)
{
	assert(!strOuputFile.empty());

	createIfcModel(L"IFC4");
}

