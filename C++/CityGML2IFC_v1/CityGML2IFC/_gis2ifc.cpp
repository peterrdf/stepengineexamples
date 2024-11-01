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
		logInfo("Exporting...");

		if (IsCityGML(m_iOwlModel))
		{
			_citygml_exporter exporter(this);
			exporter.execute(iRootInstance, strOuputFile);

			logInfo("Done.");
		}
		else if (IsCityJSON(m_iOwlModel))
		{
			_cityjson_exporter exporter(this);
			exporter.execute(iRootInstance, strOuputFile);

			logInfo("Done.");
		}
		else
		{
			logErr("Not supported format.");
		}
	} // if (iRootInstance != 0)
	else
	{
		logErr("Not supported format.");
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
	, m_iTagProperty(0)
	, m_iIfcModel(0)	
	, m_iSiteInstance(0)
	, m_iPersonInstance(0)
	, m_iOrganizationInstance(0)
	, m_iPersonAndOrganizationInstance(0)	
	, m_iApplicationInstance(0)
	, m_iOwnerHistoryInstance(0)
	, m_iDimensionalExponentsInstance(0)
	, m_iConversionBasedUnitInstance(0)
	, m_iUnitAssignmentInstance(0)
	, m_iWorldCoordinateSystemInstance(0)
	, m_iGeometricRepresentationContextInstance(0)
	, m_iProjectInstance(0)
	
{
	assert(m_pSite != nullptr);

	m_iTagProperty = GetPropertyByName(getSite()->getOwlModel(), "tag");
	assert(m_iTagProperty);
}

/*virtual*/ _exporter_base::~_exporter_base()
{
	if (m_iIfcModel != 0)
	{
		sdaiCloseModel(m_iIfcModel);
		m_iIfcModel = 0;
	}
}

SdaiInstance _exporter_base::getPersonInstance()
{
	if (m_iPersonInstance == 0) 
	{
		m_iPersonInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcPerson");
		assert(m_iPersonInstance != 0);

		sdaiPutAttrBN(m_iPersonInstance, "GivenName", sdaiSTRING, "Peter");
		sdaiPutAttrBN(m_iPersonInstance, "FamilyName", sdaiSTRING, "Bonsma");
	}

	return	m_iPersonInstance;
}

SdaiInstance _exporter_base::getOrganizationInstance()
{
	if (m_iOrganizationInstance == 0) 
	{
		m_iOrganizationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcOrganization");
		assert(m_iOrganizationInstance != 0);

		sdaiPutAttrBN(m_iOrganizationInstance, "Name", sdaiSTRING, "RDF");
		sdaiPutAttrBN(m_iOrganizationInstance, "Description", sdaiSTRING, "RDF Ltd.");
	}	

	return	m_iOrganizationInstance;
}

SdaiInstance _exporter_base::getPersonAndOrganizationInstance()
{
	if (m_iPersonAndOrganizationInstance == 0) 
	{
		m_iPersonAndOrganizationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcPersonAndOrganization");
		assert(m_iPersonAndOrganizationInstance != 0);

		sdaiPutAttrBN(m_iPersonAndOrganizationInstance, "ThePerson", sdaiINSTANCE, (void*)getPersonInstance());
		sdaiPutAttrBN(m_iPersonAndOrganizationInstance, "TheOrganization", sdaiINSTANCE, (void*)getOrganizationInstance());
	}

	return	m_iPersonAndOrganizationInstance;
}

SdaiInstance _exporter_base::getApplicationInstance()
{
	if (m_iApplicationInstance == 0)
	{
		m_iApplicationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcApplication");
		assert(m_iApplicationInstance != 0);

		sdaiPutAttrBN(m_iApplicationInstance, "ApplicationDeveloper", sdaiINSTANCE, (void*)getOrganizationInstance());
		sdaiPutAttrBN(m_iApplicationInstance, "Version", sdaiSTRING, "0.10"); //#tbd
		sdaiPutAttrBN(m_iApplicationInstance, "ApplicationFullName", sdaiSTRING, "Test Application"); //#tbd
		sdaiPutAttrBN(m_iApplicationInstance, "ApplicationIdentifier", sdaiSTRING, "TA 1001"); //#tbd
	}

	return	m_iApplicationInstance;
}

SdaiInstance _exporter_base::getOwnerHistoryInstance()
{
	if (m_iOwnerHistoryInstance == 0)
	{
		int64_t iTimeStamp = time(0);

		m_iOwnerHistoryInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcOwnerHistory");
		assert(m_iOwnerHistoryInstance != 0);

		sdaiPutAttrBN(m_iOwnerHistoryInstance, "OwningUser", sdaiINSTANCE, (void*)getPersonAndOrganizationInstance());
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "OwningApplication", sdaiINSTANCE, (void*)getApplicationInstance());
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "ChangeAction", sdaiENUM, "ADDED");
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "CreationDate", sdaiINTEGER, &iTimeStamp);
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "LastModifiedDate", sdaiINTEGER, &iTimeStamp);
	}

	return	m_iOwnerHistoryInstance;
}

SdaiInstance _exporter_base::getDimensionalExponentsInstance()
{
	if (m_iDimensionalExponentsInstance == 0)
	{
		int_t LengthExponent = 0,
			MassExponent = 0,
			TimeExponent = 0,
			ElectricCurrentExponent = 0,
			ThermodynamicTemperatureExponent = 0,
			AmountOfSubstanceExponent = 0,
			LuminousIntensityExponent = 0;

		m_iDimensionalExponentsInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcDimensionalExponents");
		assert(m_iDimensionalExponentsInstance != 0);

		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "LengthExponent", sdaiINTEGER, &LengthExponent);
		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "MassExponent", sdaiINTEGER, &MassExponent);
		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "TimeExponent", sdaiINTEGER, &TimeExponent);
		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "ElectricCurrentExponent", sdaiINTEGER, &ElectricCurrentExponent);
		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "ThermodynamicTemperatureExponent", sdaiINTEGER, &ThermodynamicTemperatureExponent);
		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "AmountOfSubstanceExponent", sdaiINTEGER, &AmountOfSubstanceExponent);
		sdaiPutAttrBN(m_iDimensionalExponentsInstance, "LuminousIntensityExponent", sdaiINTEGER, &LuminousIntensityExponent);
	}

	return	m_iDimensionalExponentsInstance;
}

SdaiInstance _exporter_base::getConversionBasedUnitInstance()
{
	if (m_iConversionBasedUnitInstance == 0)
	{
		m_iConversionBasedUnitInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcConversionBasedUnit");
		assert(m_iConversionBasedUnitInstance != 0);

		sdaiPutAttrBN(m_iConversionBasedUnitInstance, "Dimensions", sdaiINSTANCE, (void*)getDimensionalExponentsInstance());
		sdaiPutAttrBN(m_iConversionBasedUnitInstance, "UnitType", sdaiENUM, "PLANEANGLEUNIT");
		sdaiPutAttrBN(m_iConversionBasedUnitInstance, "Name", sdaiSTRING, "DEGREE");
		sdaiPutAttrBN(m_iConversionBasedUnitInstance, "ConversionFactor", sdaiINSTANCE, (void*)buildMeasureWithUnitInstance());
	}

	return	m_iConversionBasedUnitInstance;
}

SdaiInstance _exporter_base::getUnitAssignmentInstance()
{
	if (m_iUnitAssignmentInstance == 0)
	{
		m_iUnitAssignmentInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcUnitAssignment");
		assert(m_iUnitAssignmentInstance != 0);

		SdaiAggr pUnits = sdaiCreateAggrBN(m_iUnitAssignmentInstance, "Units");
		assert(pUnits != nullptr);

		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("LENGTHUNIT", nullptr, "METRE"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("AREAUNIT", nullptr, "SQUARE_METRE"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("VOLUMEUNIT", nullptr, "CUBIC_METRE"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)getConversionBasedUnitInstance());
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("SOLIDANGLEUNIT", nullptr, "STERADIAN"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("MASSUNIT", nullptr, "GRAM"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("TIMEUNIT", nullptr, "SECOND"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("THERMODYNAMICTEMPERATUREUNIT", nullptr, "DEGREE_CELSIUS"));
		sdaiAppend(pUnits, sdaiINSTANCE, (void*)buildSIUnitInstance("LUMINOUSINTENSITYUNIT", nullptr, "LUMEN"));
	}

	return	m_iUnitAssignmentInstance;
}

SdaiInstance _exporter_base::getWorldCoordinateSystemInstance()
{
	if (m_iWorldCoordinateSystemInstance == 0)
	{
		m_iWorldCoordinateSystemInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcAxis2Placement3D");
		assert(m_iWorldCoordinateSystemInstance != 0);

		sdaiPutAttrBN(m_iWorldCoordinateSystemInstance, "Location", sdaiINSTANCE, (void*)buildCartesianPointInstance(0., 0., 0.));
	}	

	return m_iWorldCoordinateSystemInstance;
}

SdaiInstance _exporter_base::getGeometricRepresentationContextInstance()
{
	if (m_iGeometricRepresentationContextInstance == 0)
	{
		double dPrecision = 0.00001;
		int_t iCoordinateSpaceDimension = 3;

		m_iGeometricRepresentationContextInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcGeometricRepresentationContext");
		assert(m_iGeometricRepresentationContextInstance != 0);

		sdaiPutAttrBN(m_iGeometricRepresentationContextInstance, "ContextType", sdaiSTRING, "Model");
		sdaiPutAttrBN(m_iGeometricRepresentationContextInstance, "CoordinateSpaceDimension", sdaiINTEGER, &iCoordinateSpaceDimension);
		sdaiPutAttrBN(m_iGeometricRepresentationContextInstance, "Precision", sdaiREAL, &dPrecision);
		sdaiPutAttrBN(m_iGeometricRepresentationContextInstance, "WorldCoordinateSystem", sdaiINSTANCE, (void*)getWorldCoordinateSystemInstance());
		sdaiPutAttrBN(m_iGeometricRepresentationContextInstance, "TrueNorth", sdaiINSTANCE, (void*)buildDirectionInstance(0., 1., 0.));
	}

	return  m_iGeometricRepresentationContextInstance;
}

SdaiInstance _exporter_base::getProjectInstance()
{
	if (m_iProjectInstance == 0) 
	{
		m_iProjectInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcProject");
		assert(m_iProjectInstance != 0);

		sdaiPutAttrBN(m_iProjectInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
		sdaiPutAttrBN(m_iProjectInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
		sdaiPutAttrBN(m_iProjectInstance, "Name", sdaiSTRING, "Default Project"); //#tbd
		sdaiPutAttrBN(m_iProjectInstance, "Description", sdaiSTRING, "Description of Default Project"); //#tbd
		sdaiPutAttrBN(m_iProjectInstance, "UnitsInContext", sdaiINSTANCE, (void*)getUnitAssignmentInstance());

		SdaiAggr pRepresentationContexts = sdaiCreateAggrBN(m_iProjectInstance, "RepresentationContexts");
		assert(pRepresentationContexts != nullptr);

		sdaiAppend(pRepresentationContexts, sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());
	}

	return m_iProjectInstance;
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

	//#tbd
	char    description[512], timeStamp[512];
	time_t  t;
	struct tm* tInfo;

	time(&t);
	tInfo = localtime(&t);

	//#tbd
	if (true)//view == COORDINATIONVIEW) {
		//if (m_Quantities.GetCheck()) {
		memcpy(description, "ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]", sizeof("ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]"));
	//}
	/*else {
		memcpy(description, "ViewDefinition [CoordinationView]", sizeof("ViewDefinition [CoordinationView]"));
	}*/
	/*}
	else {
		ASSERT(view == PRESENTATIONVIEW);
		if (m_Quantities.GetCheck()) {
			memcpy(description, "ViewDefinition [PresentationView, QuantityTakeOffAddOnView]", sizeof("ViewDefinition [PresentationView, QuantityTakeOffAddOnView]"));
		}
		else {
			memcpy(description, "ViewDefinition [PresentationView]", sizeof("ViewDefinition [PresentationView]"));
		}
	}*/

	_itoa(1900 + tInfo->tm_year, &timeStamp[0], 10);
	_itoa(100 + 1 + tInfo->tm_mon, &timeStamp[4], 10);
	_itoa(100 + tInfo->tm_mday, &timeStamp[7], 10);
	timeStamp[4] = '-';
	timeStamp[7] = '-';
	_itoa(100 + tInfo->tm_hour, &timeStamp[10], 10);
	_itoa(100 + tInfo->tm_min, &timeStamp[13], 10);
	_itoa(100 + tInfo->tm_sec, &timeStamp[16], 10);
	timeStamp[10] = 'T';
	timeStamp[13] = ':';
	timeStamp[16] = ':';
	timeStamp[19] = 0;

	SetSPFFHeader(
		m_iIfcModel,
		(const char*)description,           //  description //#tbd
		"2;1",                              //  implementationLevel //#tbd
		(const char*)nullptr,	            //  name //#tbd
		(const char*)&timeStamp[0],         //  timeStamp //#tbd
		"Architect",                        //  author //#tbd
		"Building Designer Office",         //  organization //#tbd
		"IFC Engine DLL version 1.03 beta", //  preprocessorVersion //#tbd
		"IFC Engine DLL version 1.03 beta", //  originatingSystem //#tbd
		"The authorising person",           //  authorization //#tbd
		CW2A(szSchemaName)                  //  fileSchema //#tbd
	);
}

void _exporter_base::saveIfcFile(const wchar_t* szFileName)
{
	assert(szFileName != nullptr);
	assert(m_iIfcModel != 0);

	sdaiSaveModelBNUnicode(m_iIfcModel, szFileName);
}

SdaiInstance _exporter_base::buildSIUnitInstance(const char* szUnitType, const char* szPrefix, const char* szName)
{
	assert(szUnitType != nullptr);
	assert(szName != nullptr);

	SdaiInstance iSIUnitInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcSIUnit");
	assert(iSIUnitInstance != 0);

	sdaiPutAttrBN(iSIUnitInstance, "Dimensions", sdaiINTEGER, (void*)nullptr);
	sdaiPutAttrBN(iSIUnitInstance, "UnitType", sdaiENUM, szUnitType);
	if (szPrefix != nullptr) 
	{
		sdaiPutAttrBN(iSIUnitInstance, "Prefix", sdaiENUM, szPrefix);
	}
	sdaiPutAttrBN(iSIUnitInstance, "Name", sdaiENUM, szName);

	return iSIUnitInstance;
}

SdaiInstance _exporter_base::buildMeasureWithUnitInstance()
{
	double	dValueComponent = 0.01745; //#tbd
	SdaiADB pValueComponentADB = sdaiCreateADB(sdaiREAL, &dValueComponent);
	assert(pValueComponentADB != nullptr);

	SdaiInstance iMeasureWithUnitInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcMeasureWithUnit");
	assert(iMeasureWithUnitInstance != 0);	

	sdaiPutADBTypePath(pValueComponentADB, 1, "IFCREAL");
	sdaiPutAttrBN(iMeasureWithUnitInstance, "ValueComponent", sdaiADB, (void*)pValueComponentADB);
	sdaiPutAttrBN(iMeasureWithUnitInstance, "UnitComponent", sdaiINSTANCE, (void*)buildSIUnitInstance("PLANEANGLEUNIT", NULL, "RADIAN"));
	
	return iMeasureWithUnitInstance;
}

SdaiInstance _exporter_base::buildDirectionInstance(double dX, double dY, double dZ)
{
	SdaiInstance iDirectionInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcDirection");
	assert(iDirectionInstance != 0);

	SdaiAggr pDirectionRatios = sdaiCreateAggrBN(iDirectionInstance, "DirectionRatios");
	assert(pDirectionRatios != nullptr);

	sdaiAppend(pDirectionRatios, sdaiREAL, &dX);
	sdaiAppend(pDirectionRatios, sdaiREAL, &dY);
	sdaiAppend(pDirectionRatios, sdaiREAL, &dZ);

	return iDirectionInstance;
}

SdaiInstance _exporter_base::buildCartesianPointInstance(double dX, double dY, double dZ)
{
	SdaiInstance iCartesianPointInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcCartesianPoint");
	assert(iCartesianPointInstance != 0);

	SdaiAggr pCoordinates = sdaiCreateAggrBN(iCartesianPointInstance, "Coordinates");
	assert(pCoordinates != nullptr);

	sdaiAppend(pCoordinates, sdaiREAL, &dX);
	sdaiAppend(pCoordinates, sdaiREAL, &dY);
	sdaiAppend(pCoordinates, sdaiREAL, &dZ);

	return iCartesianPointInstance;
}

SdaiInstance _exporter_base::buildSiteInstance(
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix, 
	SdaiInstance& iSiteInstancePlacement)
{
	assert(pMatrix != nullptr);

	SdaiInstance iSiteInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcSite");
	assert(iSiteInstance != 0);

	sdaiPutAttrBN(iSiteInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iSiteInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iSiteInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iSiteInstance, "Description", sdaiSTRING, szDescription);

	iSiteInstancePlacement = buildLocalPlacementInstance(pMatrix, 0);
	assert(iSiteInstancePlacement != 0);

	sdaiPutAttrBN(iSiteInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iSiteInstancePlacement);
	sdaiPutAttrBN(iSiteInstance, "CompositionType", sdaiENUM, "ELEMENT");

	SdaiAggr pRefLatitude = sdaiCreateAggrBN(iSiteInstance, "RefLatitude");
	assert(pRefLatitude != nullptr);

	int_t refLat_x = 24, refLat_y = 28, refLat_z = 0; //#tbd
	sdaiAppend(pRefLatitude, sdaiINTEGER, &refLat_x);
	sdaiAppend(pRefLatitude, sdaiINTEGER, &refLat_y);
	sdaiAppend(pRefLatitude, sdaiINTEGER, &refLat_z);

	SdaiAggr pRefLongitude = sdaiCreateAggrBN(iSiteInstance, "RefLongitude");
	assert(pRefLongitude != nullptr);

	int_t refLong_x = 54, refLong_y = 25, refLong_z = 0; //#tbd
	sdaiAppend(pRefLongitude, sdaiINTEGER, &refLong_x);
	sdaiAppend(pRefLongitude, sdaiINTEGER, &refLong_y);
	sdaiAppend(pRefLongitude, sdaiINTEGER, &refLong_z);

	double dRefElevation = 10;  //#tbd
	sdaiPutAttrBN(iSiteInstance, "RefElevation", sdaiREAL, &dRefElevation);

	return	iSiteInstance;
}

SdaiInstance _exporter_base::buildLocalPlacementInstance(_matrix* pMatrix, SdaiInstance iPlacementRelativeTo)
{
	SdaiInstance iLocalPlacementInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcLocalPlacement");
	assert(iLocalPlacementInstance != 0);

	if (iPlacementRelativeTo != 0) 
	{
		sdaiPutAttrBN(iLocalPlacementInstance, "PlacementRelTo", sdaiINSTANCE, (void*)iPlacementRelativeTo);
	}
	sdaiPutAttrBN(iLocalPlacementInstance, "RelativePlacement", sdaiINSTANCE, (void*)buildAxis2Placement3DInstance(pMatrix));

	return iLocalPlacementInstance;
}

SdaiInstance _exporter_base::buildAxis2Placement3DInstance(_matrix* pMatrix)
{
	SdaiInstance iAxis2Placement3DInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcAxis2Placement3D");
	assert(iAxis2Placement3DInstance != 0);

	sdaiPutAttrBN(iAxis2Placement3DInstance, "Location", sdaiINSTANCE, (void*)buildCartesianPointInstance(pMatrix->_41, pMatrix->_42, pMatrix->_43));
	sdaiPutAttrBN(iAxis2Placement3DInstance, "Axis", sdaiINSTANCE, (void*)buildDirectionInstance(pMatrix->_31, pMatrix->_32, pMatrix->_33));
	sdaiPutAttrBN(iAxis2Placement3DInstance, "RefDirection", sdaiINSTANCE, (void*)buildDirectionInstance(pMatrix->_11, pMatrix->_12, pMatrix->_13));

	return iAxis2Placement3DInstance;
}

SdaiInstance _exporter_base::buildBuildingInstance(
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingInstancePlacement)
{
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);

	SdaiInstance iBuildingInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcBuilding");
	assert(iBuildingInstance != 0);

	sdaiPutAttrBN(iBuildingInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iBuildingInstance, "Description", sdaiSTRING, szDescription);

	iBuildingInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingInstancePlacement);
	sdaiPutAttrBN(iBuildingInstance, "CompositionType", sdaiENUM, "ELEMENT");
	//sdaiPutAttrBN(iBuildingInstance, "BuildingAddress", sdaiINSTANCE, (void*)buildPostalAddress()); //#tbd

	return iBuildingInstance;
}

SdaiInstance _exporter_base::buildFeatureInstance(
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingInstancePlacement,
	const vector<SdaiInstance>& vecRepresentations)
{
	assert(szName != nullptr);
	assert(szDescription != nullptr);
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);
	assert(!vecRepresentations.empty());

	SdaiInstance iBuildingElementInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcBuildingElementProxy");
	assert(iBuildingElementInstance != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingElementInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingElementInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iBuildingElementInstance, "Description", sdaiSTRING, szDescription);

	iBuildingInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingInstancePlacement);
	sdaiPutAttrBN(iBuildingElementInstance, "Representation", sdaiINSTANCE, (void*)buildProductDefinitionShapeInstance(vecRepresentations));

	return iBuildingElementInstance;
}

SdaiInstance _exporter_base::buildTransportElementInstance(
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingInstancePlacement,
	const vector<SdaiInstance>& vecRepresentations)
{
	assert(szName != nullptr);
	assert(szDescription != nullptr);
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);
	assert(!vecRepresentations.empty());

	SdaiInstance iBuildingElementInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcTransportElement");
	assert(iBuildingElementInstance != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingElementInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingElementInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iBuildingElementInstance, "Description", sdaiSTRING, szDescription);

	iBuildingInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingInstancePlacement);
	sdaiPutAttrBN(iBuildingElementInstance, "Representation", sdaiINSTANCE, (void*)buildProductDefinitionShapeInstance(vecRepresentations));

	return iBuildingElementInstance;
}

SdaiInstance _exporter_base::buildFurnitureObjectInstance(
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingInstancePlacement,
	const vector<SdaiInstance>& vecRepresentations)
{
	assert(szName != nullptr);
	assert(szDescription != nullptr);
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);
	assert(!vecRepresentations.empty());

	SdaiInstance iBuildingElementInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcFurnishingElement");
	assert(iBuildingElementInstance != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingElementInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingElementInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iBuildingElementInstance, "Description", sdaiSTRING, szDescription);

	iBuildingInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingInstancePlacement);
	sdaiPutAttrBN(iBuildingElementInstance, "Representation", sdaiINSTANCE, (void*)buildProductDefinitionShapeInstance(vecRepresentations));

	return iBuildingElementInstance;
}

SdaiInstance _exporter_base::buildGeographicElementInstance(
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingInstancePlacement,
	const vector<SdaiInstance>& vecRepresentations)
{
	assert(szName != nullptr);
	assert(szDescription != nullptr);
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);
	assert(!vecRepresentations.empty());

	SdaiInstance iBuildingElementInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcGeographicElement");
	assert(iBuildingElementInstance != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingElementInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingElementInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iBuildingElementInstance, "Description", sdaiSTRING, szDescription);

	iBuildingInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingInstancePlacement);
	sdaiPutAttrBN(iBuildingElementInstance, "Representation", sdaiINSTANCE, (void*)buildProductDefinitionShapeInstance(vecRepresentations));

	return iBuildingElementInstance;
}

SdaiInstance _exporter_base::buildBuildingStoreyInstance(_matrix* pMatrix, SdaiInstance iPlacementRelativeTo, SdaiInstance& iBuildingStoreyInstancePlacement)
{
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);

	SdaiInstance iBuildingStoreyInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcBuildingStorey");
	assert(iBuildingStoreyInstance != 0);

	sdaiPutAttrBN(iBuildingStoreyInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingStoreyInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingStoreyInstance, "Name", sdaiSTRING, "Default Building Storey"); //#tbd
	sdaiPutAttrBN(iBuildingStoreyInstance, "Description", sdaiSTRING, "Description of Default Building Storey"); //#tbd

	iBuildingStoreyInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingStoreyInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingStoreyInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingStoreyInstancePlacement);
	sdaiPutAttrBN(iBuildingStoreyInstance, "CompositionType", sdaiENUM, "ELEMENT");

	double dElevation = 0;
	sdaiPutAttrBN(iBuildingStoreyInstance, "Elevation", sdaiREAL, &dElevation);

	return iBuildingStoreyInstance;
}

SdaiInstance _exporter_base::buildProductDefinitionShapeInstance(const vector<SdaiInstance>& vecRepresentations)
{
	assert(!vecRepresentations.empty());

	SdaiInstance iProductDefinitionShapeInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcProductDefinitionShape");
	assert(iProductDefinitionShapeInstance != 0);

	SdaiAggr pRepresentations = sdaiCreateAggrBN(iProductDefinitionShapeInstance, "Representations");
	assert(pRepresentations != nullptr);

	for (auto iRepresentation : vecRepresentations)
	{
		sdaiAppend(pRepresentations, sdaiINSTANCE, (void*)iRepresentation);
	}

	return iProductDefinitionShapeInstance;
}

SdaiInstance _exporter_base::buildRelAggregatesInstance(
	const char* szName, 
	const char* szDescription, 
	SdaiInstance iRelatingObjectInstance, 
	const vector<SdaiInstance>& vecRelatedObjects)
{
	assert(iRelatingObjectInstance != 0);
	assert(!vecRelatedObjects.empty());

	SdaiInstance iRelAggregatesInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcRelAggregates");
	assert(iRelAggregatesInstance != 0);

	sdaiPutAttrBN(iRelAggregatesInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iRelAggregatesInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iRelAggregatesInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iRelAggregatesInstance, "Description", sdaiSTRING, szDescription);
	sdaiPutAttrBN(iRelAggregatesInstance, "RelatingObject", sdaiINSTANCE, (void*)iRelatingObjectInstance);

	SdaiAggr pRelatedObjects = sdaiCreateAggrBN(iRelAggregatesInstance, "RelatedObjects");
	assert(pRelatedObjects != nullptr);

	for (auto iRelatedObject : vecRelatedObjects)
	{
		sdaiAppend(pRelatedObjects, sdaiINSTANCE, (void*)iRelatedObject);
	}

	return iRelAggregatesInstance;
}

SdaiInstance _exporter_base::buildRelContainedInSpatialStructureInstance(
	const char* szName,
	const char* szDescription,
	SdaiInstance iRelatingStructureInstance,
	const vector<SdaiInstance>& vecRelatedElements)
{
	assert(iRelatingStructureInstance != 0);
	assert(!vecRelatedElements.empty());

	SdaiInstance iRelContainedInSpatialStructureInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcRelContainedInSpatialStructure");
	assert(iRelContainedInSpatialStructureInstance != 0);

	sdaiPutAttrBN(iRelContainedInSpatialStructureInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iRelContainedInSpatialStructureInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iRelContainedInSpatialStructureInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iRelContainedInSpatialStructureInstance, "Description", sdaiSTRING, szDescription);
	sdaiPutAttrBN(iRelContainedInSpatialStructureInstance, "RelatingStructure", sdaiINSTANCE, (void*)iRelatingStructureInstance);

	SdaiAggr pRelatedElements = sdaiCreateAggrBN(iRelContainedInSpatialStructureInstance, "RelatedElements");
	assert(pRelatedElements != nullptr);

	for (auto iRelatedElement : vecRelatedElements)
	{
		sdaiAppend(pRelatedElements, sdaiINSTANCE, (void*)iRelatedElement);
	}

	return iRelContainedInSpatialStructureInstance;
}

SdaiInstance _exporter_base::buildBuildingElementInstance(
	const char* szEntity,
	const char* szName,
	const char* szDescription,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingElementInstancePlacement,
	const vector<SdaiInstance>& vecRepresentations)
{
	assert(szEntity != nullptr);
	assert(szName != nullptr);
	assert(szDescription != nullptr);
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);
	assert(!vecRepresentations.empty());

	SdaiInstance iBuildingElementInstance = sdaiCreateInstanceBN(m_iIfcModel, szEntity);
	assert(iBuildingElementInstance != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingElementInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingElementInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iBuildingElementInstance, "Description", sdaiSTRING, szDescription);

	iBuildingElementInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingElementInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingElementInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingElementInstancePlacement);
	sdaiPutAttrBN(iBuildingElementInstance, "Representation", sdaiINSTANCE, (void*)buildProductDefinitionShapeInstance(vecRepresentations));

	return iBuildingElementInstance;
}

SdaiInstance _exporter_base::buildRepresentationMap(_matrix* pMatrix, const vector<SdaiInstance>& vecMappedRepresentations)
{
	SdaiInstance iRepresentationMapInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcRepresentationMap");
	assert(iRepresentationMapInstance != 0);

	sdaiPutAttrBN(iRepresentationMapInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iRepresentationMapInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iRepresentationMapInstance, "MappingOrigin", sdaiINSTANCE, (void*)buildAxis2Placement3DInstance(pMatrix));

	SdaiInstance iShapeRepresentationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcShapeRepresentation");
	assert(iShapeRepresentationInstance != 0);

	sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, "Body");
	sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "Brep");
	sdaiPutAttrBN(iShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());

	SdaiAggr pItems = sdaiCreateAggrBN(iShapeRepresentationInstance, "Items");
	assert(pItems != 0);

	for (auto iMappedRepresentation : vecMappedRepresentations)
	{
		sdaiAppend(pItems, sdaiINSTANCE, (void*)iMappedRepresentation);
	}

	sdaiPutAttrBN(iRepresentationMapInstance, "MappedRepresentation", sdaiINSTANCE, (void*)iShapeRepresentationInstance);	

	return iRepresentationMapInstance;
}

SdaiInstance _exporter_base::buildMappedItem(
	const vector<SdaiInstance>& vecRepresentations,
	OwlInstance iReferencePointMatrixInstance,
	OwlInstance iTransformationMatrixInstance)
{
	assert(!vecRepresentations.empty());
	assert(iReferencePointMatrixInstance != 0);
	assert(iTransformationMatrixInstance != 0);

	SdaiInstance iMappedItemInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcMappedItem");
	assert(iMappedItemInstance != 0);

	sdaiPutAttrBN(iMappedItemInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iMappedItemInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	// Reference Point (Anchor)
	double dReferencePointX = 0.;
	double dReferencePointY = 0.;
	double dReferencePointZ = 0.;
	{
		int64_t iValuesCount = 0;
		double* pdValues = nullptr;
		GetDatatypeProperty(
			iReferencePointMatrixInstance,
			GetPropertyByName(getSite()->getOwlModel(), "coordinates"),
			(void**)&pdValues,
			&iValuesCount);
		assert(iValuesCount == 12);

		dReferencePointX = pdValues[9];
		dReferencePointY = pdValues[10];
		dReferencePointZ = pdValues[11];
	}

	_matrix mtxReferencePoint;
	mtxReferencePoint._41 = dReferencePointX;
	mtxReferencePoint._42 = dReferencePointY;
	mtxReferencePoint._43 = dReferencePointZ;
	sdaiPutAttrBN(iMappedItemInstance, "MappingSource", sdaiINSTANCE, (void*)buildRepresentationMap(&mtxReferencePoint, vecRepresentations));

	SdaiInstance iCartesianTransformationOperator3DInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcCartesianTransformationOperator3D");
	assert(iCartesianTransformationOperator3DInstance != 0);	

	// Transformation Matrix
	{
		int64_t iValuesCount = 0;
		double* pdValues = nullptr;
		GetDatatypeProperty(
			iTransformationMatrixInstance,
			GetPropertyByName(getSite()->getOwlModel(), "coordinates"),
			(void**)&pdValues,
			&iValuesCount);
		assert(iValuesCount == 12);

		sdaiPutAttrBN(iCartesianTransformationOperator3DInstance, "Axis1", sdaiINSTANCE, (void*)buildDirectionInstance(pdValues[0], pdValues[1], pdValues[2]));
		sdaiPutAttrBN(iCartesianTransformationOperator3DInstance, "Axis2", sdaiINSTANCE, (void*)buildDirectionInstance(pdValues[3], pdValues[4], pdValues[5]));
		sdaiPutAttrBN(iCartesianTransformationOperator3DInstance, "Axis3", sdaiINSTANCE, (void*)buildDirectionInstance(pdValues[6], pdValues[7], pdValues[8]));

		SdaiInstance iLocalOriginInstance = buildCartesianPointInstance(pdValues[9], pdValues[10], pdValues[11]);
		assert(iLocalOriginInstance != 0);

		sdaiPutAttrBN(iCartesianTransformationOperator3DInstance, "LocalOrigin", sdaiINSTANCE, (void*)iLocalOriginInstance);
	}	

	sdaiPutAttrBN(iMappedItemInstance, "MappingTarget", sdaiINSTANCE, (void*)iCartesianTransformationOperator3DInstance);

	SdaiInstance iShapeRepresentationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcShapeRepresentation");
	assert(iShapeRepresentationInstance != 0);

	sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, "Body");
	sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "MappedRepresentation");
	sdaiPutAttrBN(iShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());

	SdaiAggr pItems = sdaiCreateAggrBN(iShapeRepresentationInstance, "Items");
	assert(pItems != 0);

	sdaiAppend(pItems, sdaiINSTANCE, (void*)iMappedItemInstance);

	return iShapeRepresentationInstance;
}

void _exporter_base::createStyledItemInstance(OwlInstance iOwlInstance, SdaiInstance iSdaiInstance)
{
	assert(iOwlInstance != 0);
	assert(iSdaiInstance != 0);

	// material
	OwlInstance* piMaterials = nullptr;
	int64_t iMaterialsCount = 0;
	GetObjectProperty(
		iOwlInstance,
		GetPropertyByName(getSite()->getOwlModel(), "material"),
		&piMaterials,
		&iMaterialsCount);

	assert(iMaterialsCount == 1);

	OwlInstance iMaterialInstance = piMaterials[0];

	if (hasObjectProperty(iMaterialInstance, "textures"))
	{
		m_pSite->logWarn("Textures are not supported.");

		createDefaultStyledItemInstance(iSdaiInstance);

		return;
	}

	string strTag = getTag(iMaterialInstance);
	if (strTag == "Default Material")
	{
		createDefaultStyledItemInstance(iSdaiInstance);

		return;
	}

	// color
	OwlInstance* piInstances = nullptr;
	int64_t iInstancesCount = 0;
	GetObjectProperty(
		iMaterialInstance,
		GetPropertyByName(getSite()->getOwlModel(), "color"),
		&piInstances,
		&iInstancesCount);

	assert(iInstancesCount == 1);

	OwlInstance iColorInstance = piInstances[0];

	// transparency
	double* pdValues = nullptr;
	int64_t iValuesCount = 0;
	GetDatatypeProperty(
		iColorInstance,
		GetPropertyByName(getSite()->getOwlModel(), "transparency"),
		(void**)&pdValues,
		&iValuesCount);

	double dTransparency = 0.;
	if (iValuesCount == 1)
	{
		dTransparency = 1. - pdValues[0];
	}

	// diffuse
	piInstances = nullptr;
	iInstancesCount = 0;
	GetObjectProperty(
		iColorInstance,
		GetPropertyByName(getSite()->getOwlModel(), "diffuse"),
		&piInstances,
		&iInstancesCount);

	assert(iInstancesCount == 1);

	OwlInstance iDiffuseColorComponentInstance = piInstances[0];

	// R
	double* pdRValue = nullptr;
	iValuesCount = 0;
	GetDatatypeProperty(
		iDiffuseColorComponentInstance,
		GetPropertyByName(getSite()->getOwlModel(), "R"),
		(void**)&pdRValue,
		&iValuesCount);

	assert(iValuesCount == 1);

	// G
	double* pdGValue = nullptr;
	iValuesCount = 0;
	GetDatatypeProperty(
		iDiffuseColorComponentInstance,
		GetPropertyByName(getSite()->getOwlModel(), "G"),
		(void**)&pdGValue,
		&iValuesCount);

	assert(iValuesCount == 1);

	// B
	double* pdBValue = nullptr;
	iValuesCount = 0;
	GetDatatypeProperty(
		iDiffuseColorComponentInstance,
		GetPropertyByName(getSite()->getOwlModel(), "B"),
		(void**)&pdBValue,
		&iValuesCount);

	assert(iValuesCount == 1);

	createStyledItemInstance(iSdaiInstance, pdRValue[0], pdGValue[0], pdBValue[0], dTransparency);
}

void _exporter_base::createStyledItemInstance(SdaiInstance iSdaiInstance, double dR, double dG, double dB, double dTransparency)
{
	assert(iSdaiInstance != 0);

	SdaiInstance iStyledItemInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcStyledItem");
	assert(iStyledItemInstance != 0);

	sdaiPutAttrBN(iStyledItemInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iStyledItemInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	SdaiAggr pStyles = sdaiCreateAggrBN(iStyledItemInstance, "Styles");
	assert(pStyles != nullptr);

	SdaiInstance iPresentationStyleAssignmentInstance = buildPresentationStyleAssignmentInstance();
	sdaiAppend(pStyles, sdaiINSTANCE, (void*)iPresentationStyleAssignmentInstance);

	pStyles = sdaiCreateAggrBN(iPresentationStyleAssignmentInstance, "Styles");
	assert(pStyles != nullptr);

	SdaiInstance iSurfaceStyleInstance = buildSurfaceStyleInstance();
	sdaiPutAttrBN(iSurfaceStyleInstance, "Side", sdaiENUM, "BOTH");
	sdaiAppend(pStyles, sdaiINSTANCE, (void*)iSurfaceStyleInstance);

	pStyles = sdaiCreateAggrBN(iSurfaceStyleInstance, "Styles");
	assert(pStyles != nullptr);

	SdaiInstance iSurfaceStyleRenderingInstance = buildSurfaceStyleRenderingInstance();
	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "ReflectanceMethod", sdaiENUM, "NOTDEFINED");
	sdaiAppend(pStyles, sdaiINSTANCE, (void*)iSurfaceStyleRenderingInstance);

	SdaiInstance iColorRgbInstance = buildColorRgbInstance(dR, dG, dB);
	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "SurfaceColour", sdaiINSTANCE, (void*)iColorRgbInstance);
	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "Transparency", sdaiREAL, &dTransparency);

	sdaiPutAttrBN(iStyledItemInstance, "Item", sdaiINSTANCE, (void*)iSdaiInstance);
}

void _exporter_base::createStyledItemInstance(SdaiInstance iSdaiInstance, SdaiInstance iColorRgbInstance, double dTransparency)
{
	assert(iSdaiInstance != 0);
	assert(iColorRgbInstance != 0);

	SdaiInstance iStyledItemInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcStyledItem");
	assert(iStyledItemInstance != 0);

	sdaiPutAttrBN(iStyledItemInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iStyledItemInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	SdaiAggr pStyles = sdaiCreateAggrBN(iStyledItemInstance, "Styles");
	assert(pStyles != nullptr);

	SdaiInstance iPresentationStyleAssignmentInstance = buildPresentationStyleAssignmentInstance();
	sdaiAppend(pStyles, sdaiINSTANCE, (void*)iPresentationStyleAssignmentInstance);

	pStyles = sdaiCreateAggrBN(iPresentationStyleAssignmentInstance, "Styles");
	assert(pStyles != nullptr);

	SdaiInstance iSurfaceStyleInstance = buildSurfaceStyleInstance();
	sdaiPutAttrBN(iSurfaceStyleInstance, "Side", sdaiENUM, "BOTH");
	sdaiAppend(pStyles, sdaiINSTANCE, (void*)iSurfaceStyleInstance);

	pStyles = sdaiCreateAggrBN(iSurfaceStyleInstance, "Styles");
	assert(pStyles != nullptr);

	SdaiInstance iSurfaceStyleRenderingInstance = buildSurfaceStyleRenderingInstance();
	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "ReflectanceMethod", sdaiENUM, "NOTDEFINED");
	sdaiAppend(pStyles, sdaiINSTANCE, (void*)iSurfaceStyleRenderingInstance);

	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "SurfaceColour", sdaiINSTANCE, (void*)iColorRgbInstance);
	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "Transparency", sdaiREAL, &dTransparency);

	sdaiPutAttrBN(iStyledItemInstance, "Item", sdaiINSTANCE, (void*)iSdaiInstance);
}

SdaiInstance _exporter_base::buildPresentationStyleAssignmentInstance()
{
	SdaiInstance iPresentationStyleAssignmentInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcPresentationStyleAssignment");
	assert(iPresentationStyleAssignmentInstance != 0);

	sdaiPutAttrBN(iPresentationStyleAssignmentInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iPresentationStyleAssignmentInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	return iPresentationStyleAssignmentInstance;
}

SdaiInstance _exporter_base::buildSurfaceStyleInstance()
{
	SdaiInstance iSurfaceStyleInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcSurfaceStyle");
	assert(iSurfaceStyleInstance != 0);

	sdaiPutAttrBN(iSurfaceStyleInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iSurfaceStyleInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	return iSurfaceStyleInstance;
}

SdaiInstance _exporter_base::buildSurfaceStyleRenderingInstance()
{
	SdaiInstance iSurfaceStyleRenderingInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcSurfaceStyleRendering");
	assert(iSurfaceStyleRenderingInstance != 0);

	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iSurfaceStyleRenderingInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	return iSurfaceStyleRenderingInstance;
}

SdaiInstance _exporter_base::buildColorRgbInstance(double dR, double dG, double dB)
{
	SdaiInstance iColorRgbInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcColourRgb");
	assert(iColorRgbInstance != 0);

	sdaiPutAttrBN(iColorRgbInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iColorRgbInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iColorRgbInstance, "Name", sdaiSTRING, "Color");
	sdaiPutAttrBN(iColorRgbInstance, "Red", sdaiREAL, &dR);
	sdaiPutAttrBN(iColorRgbInstance, "Green", sdaiREAL, &dG);
	sdaiPutAttrBN(iColorRgbInstance, "Blue", sdaiREAL, &dB);

	return iColorRgbInstance;
}

SdaiInstance _exporter_base::buildPropertySet(char* szName, SdaiAggr& pHasProperties)
{
	SdaiInstance iPropertySetInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcPropertySet");
	assert(iPropertySetInstance != 0);

	sdaiPutAttrBN(iPropertySetInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iPropertySetInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iPropertySetInstance, "Name", sdaiSTRING, szName);

	pHasProperties = sdaiCreateAggrBN(iPropertySetInstance, "HasProperties");
	assert(pHasProperties != nullptr);

	return iPropertySetInstance;
}

SdaiInstance _exporter_base::buildRelDefinesByProperties(SdaiInstance iRelatedObject, SdaiInstance iRelatingPropertyDefinition)
{
	assert(iRelatedObject != 0);
	assert(iRelatingPropertyDefinition != 0);

	SdaiInstance iRelDefinesByPropertiesInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcRelDefinesByProperties");
	assert(iRelDefinesByPropertiesInstance != 0);

	sdaiPutAttrBN(iRelDefinesByPropertiesInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iRelDefinesByPropertiesInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	SdaiAggr pRelatedObjects = sdaiCreateAggrBN(iRelDefinesByPropertiesInstance, "RelatedObjects");
	assert(pRelatedObjects != 0);

	sdaiAppend(pRelatedObjects, sdaiINSTANCE, (void*)iRelatedObject);
	sdaiPutAttrBN(iRelDefinesByPropertiesInstance, "RelatingPropertyDefinition", sdaiINSTANCE, (void*)iRelatingPropertyDefinition);

	return iRelDefinesByPropertiesInstance;
}

SdaiInstance _exporter_base::buildPropertySingleValueText(
	const char* szName,
	const char* szDescription,
	const char* szNominalValue,
	const char* szTypePath)
{
	SdaiInstance iPropertySingleValueInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcPropertySingleValue");
	assert(iPropertySingleValueInstance != 0);

	sdaiPutAttrBN(iPropertySingleValueInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iPropertySingleValueInstance, "Description", sdaiSTRING, szDescription);

	SdaiADB pNominalValueADB = sdaiCreateADB(sdaiSTRING, szNominalValue);
	assert(pNominalValueADB != nullptr);

	sdaiPutADBTypePath(pNominalValueADB, 1, szTypePath);
	sdaiPutAttrBN(iPropertySingleValueInstance, "NominalValue", sdaiADB, (void*)pNominalValueADB);

	return iPropertySingleValueInstance;
}

SdaiInstance _exporter_base::buildPropertySingleValueReal(
	const char* szName,
	const char* szDescription,
	double dNominalValue,
	const char* szTypePath)
{
	SdaiInstance iPropertySingleValueInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcPropertySingleValue");
	assert(iPropertySingleValueInstance != 0);

	sdaiPutAttrBN(iPropertySingleValueInstance, "Name", sdaiSTRING, szName);
	sdaiPutAttrBN(iPropertySingleValueInstance, "Description", sdaiSTRING, szDescription);

	SdaiADB pNominalValueADB = sdaiCreateADB(sdaiREAL, (void*)&dNominalValue);
	assert(pNominalValueADB != nullptr);

	sdaiPutADBTypePath(pNominalValueADB, 1, szTypePath);
	sdaiPutAttrBN(iPropertySingleValueInstance, "NominalValue", sdaiADB, (void*)pNominalValueADB);

	return iPropertySingleValueInstance;
}

SdaiInstance _exporter_base::buildMaterial()
{
	SdaiInstance iMaterialInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcMaterial");
	assert(iMaterialInstance != 0);

	sdaiPutAttrBN(iMaterialInstance, "Name", sdaiSTRING, (void*)"Material");

	return  iMaterialInstance;
}

SdaiInstance _exporter_base::buildMaterialLayer(double dThickness)
{
	SdaiInstance iMaterialLayerInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcMaterialLayer");
	assert(iMaterialLayerInstance != 0);

	sdaiPutAttrBN(iMaterialLayerInstance, "Material", sdaiINSTANCE, (void*)buildMaterial());
	sdaiPutAttrBN(iMaterialLayerInstance, "LayerThickness", sdaiREAL, &dThickness);

	return iMaterialLayerInstance;
}

SdaiInstance _exporter_base::buildMaterialLayerSet(double dThickness)
{
	SdaiInstance iMaterialLayerSetInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcMaterialLayerSet");
	assert(iMaterialLayerSetInstance != 0);

	SdaiAggr pMaterialLayers = sdaiCreateAggrBN(iMaterialLayerSetInstance, "MaterialLayers");
	assert(pMaterialLayers != nullptr);

	sdaiAppend(pMaterialLayers, sdaiINSTANCE, (void*)buildMaterialLayer(dThickness));

	return iMaterialLayerSetInstance;
}

SdaiInstance _exporter_base::buildMaterialLayerSetUsage(double dThickness)
{
	double dOffsetFromReferenceLine = -dThickness / 2.;

	SdaiInstance iMaterialLayerSetUsageInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcMaterialLayerSetUsage");
	assert(iMaterialLayerSetUsageInstance != 0);

	sdaiPutAttrBN(iMaterialLayerSetUsageInstance, "ForLayerSet", sdaiINSTANCE, (void*)buildMaterialLayerSet(dThickness));
	sdaiPutAttrBN(iMaterialLayerSetUsageInstance, "LayerSetDirection", sdaiENUM, "AXIS2");
	sdaiPutAttrBN(iMaterialLayerSetUsageInstance, "DirectionSense", sdaiENUM, "POSITIVE");
	sdaiPutAttrBN(iMaterialLayerSetUsageInstance, "OffsetFromReferenceLine", sdaiREAL, &dOffsetFromReferenceLine);

	return iMaterialLayerSetUsageInstance;
}

SdaiInstance _exporter_base::buildRelAssociatesMaterial(SdaiInstance iBuildingElementInstance, double dThickness)
{
	assert(iBuildingElementInstance != 0);

	SdaiInstance iRelAssociatesMaterialInstance = sdaiCreateInstanceBN(m_iIfcModel, "IfcRelAssociatesMaterial");
	assert(iRelAssociatesMaterialInstance != 0);

	sdaiPutAttrBN(iRelAssociatesMaterialInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iRelAssociatesMaterialInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());

	SdaiAggr pRelatedObjects = sdaiCreateAggrBN(iRelAssociatesMaterialInstance, "RelatedObjects");
	assert(pRelatedObjects != nullptr);

	sdaiAppend(pRelatedObjects, sdaiINSTANCE, (void*)iBuildingElementInstance);
	sdaiPutAttrBN(iRelAssociatesMaterialInstance, "RelatingMaterial", sdaiINSTANCE, (void*)buildMaterialLayerSetUsage(dThickness));

	return iRelAssociatesMaterialInstance;
}

string _exporter_base::getTag(OwlInstance iInstance) const
{
	assert(iInstance != 0);

	SetCharacterSerialization(getSite()->getOwlModel(), 0, 0, false);

	wchar_t** szValue = nullptr;
	int64_t iValuesCount = 0;
	GetDatatypeProperty(iInstance, m_iTagProperty, (void**)&szValue, &iValuesCount);

	assert(iValuesCount == 1);

	SetCharacterSerialization(getSite()->getOwlModel(), 0, 0, true);

	return (LPCSTR)CW2A(szValue[0]);
}

OwlInstance* _exporter_base::getObjectProperty(OwlInstance iInstance, const string& strPropertyName, int64_t& iInstancesCount) const
{
	assert(iInstance != 0);
	assert(!strPropertyName.empty());

	iInstancesCount = 0;

	RdfProperty iProperty = GetPropertyByName(getSite()->getOwlModel(), strPropertyName.c_str());
	if (iProperty == 0)
	{
		return nullptr;
	}

	OwlInstance* piInstances = nullptr;
	GetObjectProperty(
		iInstance,
		iProperty,
		&piInstances,
		&iInstancesCount);

	if (iInstancesCount == 0)
	{
		return nullptr;
	}

	return piInstances;
}

bool _exporter_base::hasObjectProperty(OwlInstance iInstance, const string& strPropertyName)
{
	int64_t iInstancesCount = 0;

	return getObjectProperty(iInstance, strPropertyName, iInstancesCount) != nullptr;
}

// ************************************************************************************************
_citygml_exporter::_citygml_exporter(_gis2ifc* pSite)
	: _exporter_base(pSite)
	, m_iCollectionClass(0)
	, m_iTransformationClass(0)
	, m_mapMappedItems()
	, m_iCityObjectGroupMemberClass(0)
	, m_iGeometryMemberClass(0)
	, m_iBuildingClass(0)
	, m_iWallSurfaceClass(0)
	, m_iRoofSurfaceClass(0)
	, m_iDoorClass(0)
	, m_iWindowClass(0)
	, m_mapBuildings()
	, m_mapBuildingElements()
	, m_iVegetationObjectClass(0)
	, m_iWaterObjectClass(0)
	, m_iBridgeObjectClass(0)
	, m_iTunnelObjectClass(0)
	, m_iTransportationObjectClass(0)
	, m_iFurnitureObjectClass(0)
	, m_iReliefObjectClass(0)
	, m_mapFeatures()
	, m_mapFeatureElements()
	, m_iCurrentOwlBuildingElementInstance(0)
	, m_iDefaultWallSurfaceColorRgbInstance(0)
	, m_iDefaultRoofSurfaceColorRgbInstance(0)
	, m_iDefaultDoorColorRgbInstance(0)
	, m_iDefaultWindowColorRgbInstance(0)
	, m_iDefaultColorRgbInstance(0)
{
	// Geometry Kernel
	m_iCollectionClass = GetClassByName(getSite()->getOwlModel(), "Collection");
	m_iTransformationClass = GetClassByName(getSite()->getOwlModel(), "Transformation");

	// CityObjectGroup
	m_iCityObjectGroupMemberClass = GetClassByName(getSite()->getOwlModel(), "class:CityObjectGroupMemberType");

	// relativeGMLGeometry
	m_iGeometryMemberClass = GetClassByName(getSite()->getOwlModel(), "class:geometryMember");

	// Building
	m_iBuildingClass = GetClassByName(getSite()->getOwlModel(), "class:Building");
	m_iWallSurfaceClass = GetClassByName(getSite()->getOwlModel(), "class:WallSurface");
	m_iRoofSurfaceClass = GetClassByName(getSite()->getOwlModel(), "class:RoofSurface");
	m_iDoorClass = GetClassByName(getSite()->getOwlModel(), "class:Door");
	m_iWindowClass = GetClassByName(getSite()->getOwlModel(), "class:Window");

	// Feature
	m_iVegetationObjectClass = GetClassByName(getSite()->getOwlModel(), "class:_VegetationObject");
	m_iWaterObjectClass = GetClassByName(getSite()->getOwlModel(), "class:_WaterObject");
	m_iBridgeObjectClass = GetClassByName(getSite()->getOwlModel(), "class:_AbstractBridge");
	m_iTunnelObjectClass = GetClassByName(getSite()->getOwlModel(), "class:_AbstractTunnel");
	m_iTransportationObjectClass = GetClassByName(getSite()->getOwlModel(), "class:_TransportationObject");
	m_iFurnitureObjectClass = GetClassByName(getSite()->getOwlModel(), "class:CityFurniture");
	m_iReliefObjectClass = GetClassByName(getSite()->getOwlModel(), "class:_ReliefComponent");
}

/*virtual*/ _citygml_exporter::~_citygml_exporter()
{}

/*virtual*/ void _citygml_exporter::execute(OwlInstance iRootInstance, const wstring& strOuputFile)
{
	assert(iRootInstance != 0);
	assert(!strOuputFile.empty());

	m_mapBuildings.clear();
	m_mapBuildingElements.clear();

	if (m_iBuildingClass == 0)
	{
		getSite()->logWarn("There are no Buildings.");

		return;
	}

	createIfcModel(L"IFC4");

	string strTag = getTag(iRootInstance);

	OwlClass iInstanceClass = GetInstanceClass(iRootInstance);
	assert(iInstanceClass != 0);

	char* szClassName = nullptr;
	GetNameOfClass(iInstanceClass, &szClassName);
	assert(szClassName != nullptr);

	_matrix mtxIdentity;
	SdaiInstance iSiteInstancePlacement = 0;
	SdaiInstance iSiteInstance = buildSiteInstance(
		strTag.c_str(),
		szClassName,
		&mtxIdentity, 
		iSiteInstancePlacement);
	assert(iSiteInstancePlacement != 0);

	createProperties(iRootInstance, iSiteInstance);

	buildRelAggregatesInstance(
		"ProjectContainer", 
		"ProjectContainer for Sites", 
		getProjectInstance(), 
		vector<SdaiInstance>{iSiteInstance});

	createBuildings(iSiteInstance, iSiteInstancePlacement);
	createFeatures(iSiteInstance, iSiteInstancePlacement);

	saveIfcFile(strOuputFile.c_str());
}

/*virtual*/ void _citygml_exporter::createDefaultStyledItemInstance(SdaiInstance iSdaiInstance) /*override*/
{
	assert(m_iCurrentOwlBuildingElementInstance != 0);
	assert(iSdaiInstance != 0);

	OwlClass iInstanceClass = GetInstanceClass(m_iCurrentOwlBuildingElementInstance);
	assert(iInstanceClass != 0);

	if (isWallSurfaceClass(iInstanceClass))
	{
		if (m_iDefaultWallSurfaceColorRgbInstance == 0)
		{
			m_iDefaultWallSurfaceColorRgbInstance = buildColorRgbInstance(128. / 255., 128. / 255., 128. / 255.);
		}

		createStyledItemInstance(iSdaiInstance, m_iDefaultWallSurfaceColorRgbInstance, 0.);
	}
	else if (isRoofSurfaceClass(iInstanceClass))
	{
		if (m_iDefaultRoofSurfaceColorRgbInstance == 0)
		{
			m_iDefaultRoofSurfaceColorRgbInstance = buildColorRgbInstance(139. / 255., 69. / 255., 19. / 255.);
		}

		createStyledItemInstance(iSdaiInstance, m_iDefaultRoofSurfaceColorRgbInstance, 0.);
	}
	else if (isDoorClass(iInstanceClass))
	{
		if (m_iDefaultDoorColorRgbInstance == 0)
		{
			m_iDefaultDoorColorRgbInstance = buildColorRgbInstance(139. / 255., 139. / 255., 139. / 255.);
		}

		createStyledItemInstance(iSdaiInstance, m_iDefaultDoorColorRgbInstance, 0.);
	}
	else if (isWindowClass(iInstanceClass))
	{
		if (m_iDefaultWindowColorRgbInstance == 0)
		{
			m_iDefaultWindowColorRgbInstance = buildColorRgbInstance(25. / 255., 25. / 255., 25. / 255.);
		}

		createStyledItemInstance(iSdaiInstance, m_iDefaultWindowColorRgbInstance, 0.95);
	}
	else
	{
		if (m_iDefaultColorRgbInstance == 0)
		{
			m_iDefaultColorRgbInstance = buildColorRgbInstance(0., 0., 1.);
		}

		createStyledItemInstance(iSdaiInstance, m_iDefaultColorRgbInstance, 0.75);
	}	
}

void _citygml_exporter::createBuildings(SdaiInstance iSiteInstance, SdaiInstance iSiteInstancePlacement)
{
	assert(iSiteInstance != 0);
	assert(iSiteInstancePlacement != 0);

	OwlClass iSchemasClass = GetClassByName(getSite()->getOwlModel(), "class:Schemas");
	assert(iSchemasClass != 0);

	OwlInstance iInstance = GetInstancesByIterator(getSite()->getOwlModel(), 0);
	while (iInstance != 0)
	{
		if (GetInstanceInverseReferencesByIterator(iInstance, 0) == 0)
		{
			OwlClass iInstanceClass = GetInstanceClass(iInstance);
			assert(iInstanceClass != 0);

			if (iInstanceClass != iSchemasClass)
			{
				if (isBuildingClass(iInstanceClass))
				{
					if (m_mapBuildings.find(iInstance) == m_mapBuildings.end())
					{
						m_mapBuildings[iInstance] = vector<OwlInstance>();

						searchForBuildingElements(iInstance, iInstance);
					}
					else
					{
						assert(false); // Internal error!
					}
				}
				else
				{
					createBuildingsRecursively(iInstance);
				}
			}
		} // if (GetInstanceInverseReferencesByIterator(iInstance, 0) == 0)

		iInstance = GetInstancesByIterator(getSite()->getOwlModel(), iInstance);
	} // while (iInstance != 0)
		
	if (m_mapBuildings.empty())
	{
		return;
	}
		
	_matrix mtxIdentity;
	vector<SdaiInstance> vecBuildingInstances;
	for (auto& itBuilding : m_mapBuildings)
	{
		string strTag = getTag(itBuilding.first);

		OwlClass iInstanceClass = GetInstanceClass(itBuilding.first);
		assert(iInstanceClass != 0);

		char* szClassName = nullptr;
		GetNameOfClass(iInstanceClass, &szClassName);
		assert(szClassName != nullptr);

		SdaiInstance iBuildingInstancePlacement = 0;
		SdaiInstance iBuildingInstance = buildBuildingInstance(
			strTag.c_str(),
			szClassName,
			&mtxIdentity, 
			iSiteInstancePlacement, 
			iBuildingInstancePlacement);
		assert(iBuildingInstance != 0);

		createProperties(itBuilding.first, iBuildingInstance);

		vecBuildingInstances.push_back(iBuildingInstance);

		// Proxy/Unknown Building Elements
		searchForProxyBuildingElements(itBuilding.first, itBuilding.first);

		if (itBuilding.second.empty())
		{
			continue;
		}
		
		vector<SdaiInstance> vecBuildingElementInstances;
		for (auto iOwlBuildingElementInstance : itBuilding.second)
		{
			m_iCurrentOwlBuildingElementInstance = iOwlBuildingElementInstance;

			auto itBuildingElement = m_mapBuildingElements.find(iOwlBuildingElementInstance);
			assert(itBuildingElement != m_mapBuildingElements.end());
			assert(!itBuildingElement->second.empty());

			vector<SdaiInstance> vecSdaiBuildingElementGeometryInstances;
			for (auto iOwlBuildingElementGeometryInstance : itBuildingElement->second)
			{
				createGeometry(iOwlBuildingElementGeometryInstance, vecSdaiBuildingElementGeometryInstances, true);
			}

			if (vecSdaiBuildingElementGeometryInstances.empty())
			{
				// Not supported
				continue;
			}
			
			SdaiInstance iBuildingElementInstancePlacement = 0;
			SdaiInstance iSdaiBuildingElementInstance = buildBuildingElementInstance(
				itBuildingElement->first,
				&mtxIdentity,
				iBuildingInstancePlacement,
				iBuildingElementInstancePlacement,
				vecSdaiBuildingElementGeometryInstances);
			assert(iSdaiBuildingElementInstance != 0);

			createProperties(iOwlBuildingElementInstance, iSdaiBuildingElementInstance);

			vecBuildingElementInstances.push_back(iSdaiBuildingElementInstance);

			m_iCurrentOwlBuildingElementInstance = 0;
		} // for (auto iOwlBuildingElementInstance : ...

		SdaiInstance iBuildingStoreyInstancePlacement = 0;
		SdaiInstance iBuildingStoreyInstance = buildBuildingStoreyInstance(&mtxIdentity, iBuildingInstancePlacement, iBuildingStoreyInstancePlacement);
		assert(iBuildingStoreyInstance != 0);

		buildRelAggregatesInstance(
			"BuildingContainer", 
			"BuildingContainer for BuildigStories", 
			iBuildingInstance, 
			vector<SdaiInstance>{ iBuildingStoreyInstance });

		if (vecBuildingElementInstances.empty())
		{
			// Not supported
			continue;
		}		

		buildRelContainedInSpatialStructureInstance(
			"BuildingStoreyContainer", 
			"BuildingStoreyContainer for Building Elements", 
			iBuildingStoreyInstance, 
			vecBuildingElementInstances);
	} // for (auto& itBuilding : ...

	buildRelAggregatesInstance(
		"SiteContainer", 
		"SiteContainer For Buildings", 
		iSiteInstance, 
		vecBuildingInstances);
}

void _citygml_exporter::createBuildingsRecursively(OwlInstance iInstance)
{
	assert(iInstance != 0);

	RdfProperty iProperty = GetInstancePropertyByIterator(iInstance, 0);
	while (iProperty != 0)
	{
		if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)
		{
			int64_t iValuesCount = 0;
			OwlInstance* piValues = nullptr;
			GetObjectProperty(iInstance, iProperty, &piValues, &iValuesCount);

			for (int64_t iValue = 0; iValue < iValuesCount; iValue++)
			{
				if (piValues[iValue] == 0)
				{
					continue;
				}
					
				OwlClass iInstanceClass = GetInstanceClass(piValues[iValue]);
				assert(iInstanceClass != 0);

				if ((iInstanceClass == m_iCityObjectGroupMemberClass) || IsClassAncestor(iInstanceClass, m_iCityObjectGroupMemberClass))
				{
					continue; // Ignore
				}

				if (isBuildingClass(iInstanceClass))
				{
					if (m_mapBuildings.find(piValues[iValue]) == m_mapBuildings.end())
					{
						m_mapBuildings[piValues[iValue]] = vector<OwlInstance>();

						searchForBuildingElements(piValues[iValue], piValues[iValue]);
					}
					else
					{
						assert(false); // Internal error!
					}
				}
				else
				{
					createBuildingsRecursively(piValues[iValue]);
				}
			} // for (int64_t iValue = ...
		} // if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)

		iProperty = GetInstancePropertyByIterator(iInstance, iProperty);
	} // while (iProperty != 0)
}

void _citygml_exporter::searchForBuildingElements(OwlInstance iBuildingInstance, OwlInstance iInstance)
{
	assert(iBuildingInstance != 0);
	assert(iInstance != 0);

	RdfProperty iProperty = GetInstancePropertyByIterator(iInstance, 0);
	while (iProperty != 0)
	{
		if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)
		{
			int64_t iValuesCount = 0;
			OwlInstance* piValues = nullptr;
			GetObjectProperty(iInstance, iProperty, &piValues, &iValuesCount);

			for (int64_t iValue = 0; iValue < iValuesCount; iValue++)
			{
				if (piValues[iValue] == 0)
				{
					continue;
				}

				if (isBuildingElement(piValues[iValue]))
				{
					auto itBuilding = m_mapBuildings.find(iBuildingInstance);
					if (itBuilding != m_mapBuildings.end())
					{
						itBuilding->second.push_back(piValues[iValue]);
					}
					else
					{
						m_mapBuildings[iBuildingInstance] = vector<OwlInstance>{ piValues[iValue] };
					}

					searchForBuildingElementGeometry(piValues[iValue], piValues[iValue]);
				}

				searchForBuildingElements(iBuildingInstance, piValues[iValue]);
			} // for (int64_t iValue = ...
		} // if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)

		iProperty = GetInstancePropertyByIterator(iInstance, iProperty);
	} // while (iProperty != 0)
}

void _citygml_exporter::searchForProxyBuildingElements(OwlInstance iBuildingInstance, OwlInstance iInstance)
{
	assert(iBuildingInstance != 0);
	assert(iInstance != 0);

	RdfProperty iProperty = GetInstancePropertyByIterator(iInstance, 0);
	while (iProperty != 0)
	{
		if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)
		{
			int64_t iValuesCount = 0;
			OwlInstance* piValues = nullptr;
			GetObjectProperty(iInstance, iProperty, &piValues, &iValuesCount);

			for (int64_t iValue = 0; iValue < iValuesCount; iValue++)
			{
				if (piValues[iValue] == 0)
				{
					continue;
				}

				if (isBuildingElement(piValues[iValue]))
				{
					continue;
				}

				if (GetInstanceGeometryClass(piValues[iValue]) &&
					GetBoundingBox(piValues[iValue], nullptr, nullptr))
				{
					auto itBuilding = m_mapBuildings.find(iBuildingInstance);
					if (itBuilding != m_mapBuildings.end())
					{
						itBuilding->second.push_back(piValues[iValue]);
					}
					else
					{
						assert(false); // Internal error!
					}

					auto itBuildingElement = m_mapBuildingElements.find(piValues[iValue]);
					if (itBuildingElement == m_mapBuildingElements.end())
					{
						m_mapBuildingElements[piValues[iValue]] = vector<OwlInstance>{ piValues[iValue] };
					}
					else
					{
						OwlClass iChildInstanceClass = GetInstanceClass(piValues[iValue]);
						assert(iChildInstanceClass != 0);

						wchar_t* szClassName = nullptr;
						GetNameOfClassW(iChildInstanceClass, &szClassName);

						string strEvent = "Duplicated Geometry: '";
						strEvent += CW2A(szClassName);
						strEvent += "'";
						getSite()->logErr(strEvent);
					}
				}
				else
				{
					searchForProxyBuildingElements(iBuildingInstance, piValues[iValue]);
				}
			} // for (int64_t iValue = ...
		} // if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)

		iProperty = GetInstancePropertyByIterator(iInstance, iProperty);
	} // while (iProperty != 0)
}

void _citygml_exporter::searchForBuildingElementGeometry(OwlInstance iBuildingElementInstance, OwlInstance iInstance)
{
	assert(iBuildingElementInstance != 0);
	assert(iInstance != 0);

	RdfProperty iProperty = GetInstancePropertyByIterator(iInstance, 0);
	while (iProperty != 0)
	{
		if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)
		{
			int64_t iValuesCount = 0;
			OwlInstance* piValues = nullptr;
			GetObjectProperty(iInstance, iProperty, &piValues, &iValuesCount);

			for (int64_t iValue = 0; iValue < iValuesCount; iValue++)
			{
				if (piValues[iValue] == 0)
				{
					continue;
				}

				if (isBuildingElement(piValues[iValue]))
				{
					continue;
				}

				if (GetInstanceGeometryClass(piValues[iValue]) &&
					GetBoundingBox(piValues[iValue], nullptr, nullptr))
				{
					auto itBuildingElement = m_mapBuildingElements.find(iBuildingElementInstance);
					if (itBuildingElement != m_mapBuildingElements.end())
					{
						itBuildingElement->second.push_back(piValues[iValue]);
					}
					else
					{
						m_mapBuildingElements[iBuildingElementInstance] = vector<OwlInstance>{ piValues[iValue] };
					}
				}
				else
				{
					searchForBuildingElementGeometry(iBuildingElementInstance, piValues[iValue]);
				}
			} // for (int64_t iValue = ...
		} // if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)

		iProperty = GetInstancePropertyByIterator(iInstance, iProperty);
	} // while (iProperty != 0)
}

void _citygml_exporter::createFeatures(SdaiInstance iSiteInstance, SdaiInstance iSiteInstancePlacement)
{
	assert(iSiteInstance != 0);
	assert(iSiteInstancePlacement != 0);

	OwlClass iSchemasClass = GetClassByName(getSite()->getOwlModel(), "class:Schemas");
	assert(iSchemasClass != 0);

	OwlInstance iInstance = GetInstancesByIterator(getSite()->getOwlModel(), 0);
	while (iInstance != 0)
	{
		if (GetInstanceInverseReferencesByIterator(iInstance, 0) == 0)
		{
			OwlClass iInstanceClass = GetInstanceClass(iInstance);
			assert(iInstanceClass != 0);

			if (iInstanceClass != iSchemasClass)
			{
				if (isFeatureClass(iInstanceClass))
				{
					if (m_mapFeatures.find(iInstance) == m_mapFeatures.end())
					{
						m_mapFeatures[iInstance] = vector<OwlInstance>();

						searchForFeatureElements(iInstance, iInstance);
					}
					else
					{
						assert(false); // Internal error!
					}
				}
				else
				{
					createFeaturesRecursively(iInstance);
				}
			}
		} // if (GetInstanceInverseReferencesByIterator(iInstance, 0) == 0)

		iInstance = GetInstancesByIterator(getSite()->getOwlModel(), iInstance);
	} // while (iInstance != 0)

	if (m_mapFeatures.empty())
	{
		return;
	}

	_matrix mtxIdentity;
	vector<SdaiInstance> vecFeatureInstances;
	for (auto& itFeature : m_mapFeatures)
	{
		if (itFeature.second.empty())
		{
			continue;
		}

		// Geometry
		vector<SdaiInstance> vecSdaiFeatureElementGeometryInstances;
		for (auto iOwlFeatureElementInstance : itFeature.second)
		{
			m_iCurrentOwlBuildingElementInstance = iOwlFeatureElementInstance;

			auto itFeatureElement = m_mapFeatureElements.find(iOwlFeatureElementInstance);
			assert(itFeatureElement != m_mapFeatureElements.end());
			assert(!itFeatureElement->second.empty());

			for (auto iOwlFeatureElementGeometryInstance : itFeatureElement->second)
			{
				createGeometry(iOwlFeatureElementGeometryInstance, vecSdaiFeatureElementGeometryInstances, true);
			}

			m_iCurrentOwlBuildingElementInstance = 0;
		} // for (auto iOwlFeatureElementInstance : ...

		if (vecSdaiFeatureElementGeometryInstances.empty())
		{
			// Not supported
			continue;
		}

		// Feature
		string strTag = getTag(itFeature.first);

		OwlClass iInstanceClass = GetInstanceClass(itFeature.first);
		assert(iInstanceClass != 0);

		char* szClassName = nullptr;
		GetNameOfClass(iInstanceClass, &szClassName);
		assert(szClassName != nullptr);

		SdaiInstance iFeatureInstancePlacement = 0;
		if (isTransportationObjectClass(iInstanceClass) ||
			isBridgeObjectClass(iInstanceClass) ||
			isTunnelObjectClass(iInstanceClass))
		{			
			SdaiInstance iFeatureInstance = buildTransportElementInstance(
				strTag.c_str(),
				szClassName,
				&mtxIdentity,
				iSiteInstancePlacement,
				iFeatureInstancePlacement,
				vecSdaiFeatureElementGeometryInstances);
			assert(iFeatureInstance != 0);

			createProperties(itFeature.first, iFeatureInstance);

			vecFeatureInstances.push_back(iFeatureInstance);
		}
		else if (isReliefObjectClass(iInstanceClass) ||
			isWaterObjectClass(iInstanceClass) ||
			isVegetationObjectClass(iInstanceClass))
		{
			SdaiInstance iFeatureInstance = buildGeographicElementInstance(
				strTag.c_str(),
				szClassName,
				&mtxIdentity,
				iSiteInstancePlacement,
				iFeatureInstancePlacement,
				vecSdaiFeatureElementGeometryInstances);
			assert(iFeatureInstance != 0);

			createProperties(itFeature.first, iFeatureInstance);

			vecFeatureInstances.push_back(iFeatureInstance);
		}
		else if (isFurnitureObjectClass(iInstanceClass))
		{
			SdaiInstance iFeatureInstance = buildFurnitureObjectInstance(
				strTag.c_str(),
				szClassName,
				&mtxIdentity,
				iSiteInstancePlacement,
				iFeatureInstancePlacement,
				vecSdaiFeatureElementGeometryInstances);
			assert(iFeatureInstance != 0);

			createProperties(itFeature.first, iFeatureInstance);

			vecFeatureInstances.push_back(iFeatureInstance);
		}
		else
		{
			SdaiInstance iFeatureInstance = buildFeatureInstance(
				strTag.c_str(),
				szClassName,
				&mtxIdentity,
				iSiteInstancePlacement,
				iFeatureInstancePlacement,
				vecSdaiFeatureElementGeometryInstances);
			assert(iFeatureInstance != 0);

			createProperties(itFeature.first, iFeatureInstance);

			vecFeatureInstances.push_back(iFeatureInstance);
		}		
	} // for (auto& itFeature : ...

	buildRelAggregatesInstance(
		"SiteContainer",
		"SiteContainer For Features",
		iSiteInstance,
		vecFeatureInstances);
}

void _citygml_exporter::createFeaturesRecursively(OwlInstance iInstance)
{
	assert(iInstance != 0);

	RdfProperty iProperty = GetInstancePropertyByIterator(iInstance, 0);
	while (iProperty != 0)
	{
		if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)
		{
			int64_t iValuesCount = 0;
			OwlInstance* piValues = nullptr;
			GetObjectProperty(iInstance, iProperty, &piValues, &iValuesCount);

			for (int64_t iValue = 0; iValue < iValuesCount; iValue++)
			{
				if (piValues[iValue] == 0)
				{
					continue;
				}

				OwlClass iInstanceClass = GetInstanceClass(piValues[iValue]);
				assert(iInstanceClass != 0);

				if ((iInstanceClass == m_iCityObjectGroupMemberClass) || IsClassAncestor(iInstanceClass, m_iCityObjectGroupMemberClass))
				{					
					continue; // Ignore
				}

				if (isFeatureClass(iInstanceClass))
				{
					if (m_mapFeatures.find(piValues[iValue]) == m_mapFeatures.end())
					{
						m_mapFeatures[piValues[iValue]] = vector<OwlInstance>();

						searchForFeatureElements(piValues[iValue], piValues[iValue]);
					}
					else
					{
						assert(false); // Internal error!
					}
				}
				else
				{
					createFeaturesRecursively(piValues[iValue]);
				}
			} // for (int64_t iValue = ...
		} // if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)

		iProperty = GetInstancePropertyByIterator(iInstance, iProperty);
	} // while (iProperty != 0)
}

void _citygml_exporter::searchForFeatureElements(OwlInstance iFeatureInstance, OwlInstance iInstance)
{
	assert(iFeatureInstance != 0);
	assert(iInstance != 0);

	RdfProperty iProperty = GetInstancePropertyByIterator(iInstance, 0);
	while (iProperty != 0)
	{
		if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)
		{
			int64_t iValuesCount = 0;
			OwlInstance* piValues = nullptr;
			GetObjectProperty(iInstance, iProperty, &piValues, &iValuesCount);

			for (int64_t iValue = 0; iValue < iValuesCount; iValue++)
			{
				if (piValues[iValue] == 0)
				{
					continue;
				}

				if (GetInstanceGeometryClass(piValues[iValue]) &&
					GetBoundingBox(piValues[iValue], nullptr, nullptr))
				{
					auto itFeature = m_mapFeatures.find(iFeatureInstance);
					if (itFeature != m_mapFeatures.end())
					{
						itFeature->second.push_back(piValues[iValue]);
					}
					else
					{
						assert(false); // Internal error!
					}

					auto itFeatureElement = m_mapFeatureElements.find(piValues[iValue]);
					if (itFeatureElement == m_mapFeatureElements.end())
					{
						m_mapFeatureElements[piValues[iValue]] = vector<OwlInstance>{ piValues[iValue] };
					}
					else
					{
						OwlClass iChildInstanceClass = GetInstanceClass(piValues[iValue]);
						assert(iChildInstanceClass != 0);

						wchar_t* szClassName = nullptr;
						GetNameOfClassW(iChildInstanceClass, &szClassName);

						string strEvent = "Duplicated Geometry: '";
						strEvent += CW2A(szClassName);
						strEvent += "'";
						getSite()->logErr(strEvent);
					}
				}
				else
				{
					searchForFeatureElements(iFeatureInstance, piValues[iValue]);
				}
			} // for (int64_t iValue = ...
		} // if (GetPropertyType(iProperty) == OBJECTPROPERTY_TYPE)

		iProperty = GetInstancePropertyByIterator(iInstance, iProperty);
	} // while (iProperty != 0)
}

void _citygml_exporter::createGeometry(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	OwlClass iInstanceClass = GetInstanceClass(iInstance);
	assert(iInstanceClass != 0);

	if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:MultiSurfaceType"))
	{
		createMultiSurface(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:SolidType"))
	{
		createSolid(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:CompositeSolidType"))
	{
		createCompositeSolid(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "BoundaryRepresentation"))
	{
		createBoundaryRepresentation(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "Point3D"))
	{
		createPoint3D(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "Point3DSet"))
	{
		createPoint3DSet(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (iInstanceClass == GetClassByName(getSite()->getOwlModel(), "PolyLine3D"))
	{
		createPolyLine3D(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
	}
	else if (isCollectionClass(iInstanceClass))
	{
		OwlInstance* piInstances = nullptr;
		int64_t iInstancesCount = 0;
		GetObjectProperty(
			iInstance,
			GetPropertyByName(getSite()->getOwlModel(), "objects"),
			&piInstances,
			&iInstancesCount);

		for (int64_t iInstanceIndex = 0; iInstanceIndex < iInstancesCount; iInstanceIndex++)
		{
			createGeometry(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
	}
	else if (isTransformationClass(iInstanceClass))
	{
		// Reference Point (Anchor) Transformation
		OwlInstance iReferencePointTransformationInstance = iInstance;

		// Reference Point Transformation - matrix
		OwlInstance* piInstances = nullptr;
		int64_t iInstancesCount = 0;
		GetObjectProperty(
			iReferencePointTransformationInstance,
			GetPropertyByName(getSite()->getOwlModel(), "matrix"),
			&piInstances,
			&iInstancesCount);
		assert(iInstancesCount == 1);

		OwlInstance iReferencePointMatrixInstance = piInstances[0];
		assert(iReferencePointMatrixInstance != 0);	

		// Transformation Matrix Transformation
		piInstances = nullptr;
		iInstancesCount = 0;
		GetObjectProperty(
			iReferencePointTransformationInstance,
			GetPropertyByName(getSite()->getOwlModel(), "object"),
			&piInstances,
			&iInstancesCount);
		assert(iInstancesCount == 1);

		OwlInstance iTransformationMatrixTransformationInstance = piInstances[0];
		assert(iTransformationMatrixTransformationInstance != 0);
		assert(isTransformationClass(GetInstanceClass(iTransformationMatrixTransformationInstance)));

		// Transformation Matrix Transformation - matrix
		piInstances = nullptr;
		iInstancesCount = 0;
		GetObjectProperty(
			iTransformationMatrixTransformationInstance,
			GetPropertyByName(getSite()->getOwlModel(), "matrix"),
			&piInstances,
			&iInstancesCount);
		assert(iInstancesCount == 1);

		OwlInstance iTransformationMatrixInstance = piInstances[0];
		assert(iTransformationMatrixInstance != 0);

		// Reference Point Transformation - object
		piInstances = nullptr;
		iInstancesCount = 0;
		GetObjectProperty(
			iTransformationMatrixTransformationInstance,
			GetPropertyByName(getSite()->getOwlModel(), "object"),
			&piInstances,
			&iInstancesCount);
		assert(iInstancesCount == 1);		
		
		OwlInstance iRelativeGMLGeometryInstance = piInstances[0];
		assert(iRelativeGMLGeometryInstance != 0);

		iInstanceClass = GetInstanceClass(iRelativeGMLGeometryInstance);
		assert(isCollectionClass(iInstanceClass));

		piInstances = nullptr;
		iInstancesCount = 0;
		GetObjectProperty(
			iRelativeGMLGeometryInstance,
			GetPropertyByName(getSite()->getOwlModel(), "objects"),
			&piInstances,
			&iInstancesCount);
		assert(iInstancesCount == 1);

		OwlInstance iMappedItemGeometryInstance = piInstances[0];
		assert(iMappedItemGeometryInstance != 0);

		auto itMappedItem = m_mapMappedItems.find(iMappedItemGeometryInstance);
		if (itMappedItem == m_mapMappedItems.end())
		{
			vector<SdaiInstance> vecMappedItemGeometryInstances;
			createGeometry(iMappedItemGeometryInstance, vecMappedItemGeometryInstances, false);

			m_mapMappedItems[iMappedItemGeometryInstance] = vecMappedItemGeometryInstances;			

			vecGeometryInstances.push_back(
				buildMappedItem(
					vecMappedItemGeometryInstances,
					iReferencePointMatrixInstance,
					iTransformationMatrixInstance)
			);
		}
		else
		{
			vecGeometryInstances.push_back(
				buildMappedItem(
					itMappedItem->second,
					iReferencePointMatrixInstance,
					iTransformationMatrixInstance));
		}
	}
	else
	{
		wchar_t* szClassName = nullptr;
		GetNameOfClassW(iInstanceClass, &szClassName);

		string strEvent = "Geometry is not supported: '";
		strEvent += CW2A(szClassName);
		strEvent += "'";
		getSite()->logErr(strEvent);
	}
}

void _citygml_exporter::createSolid(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	OwlInstance* piInstances = nullptr;
	int64_t iInstancesCount = 0;
	GetObjectProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "objects"),
		&piInstances,
		&iInstancesCount);

	for (int64_t iInstanceIndex = 0; iInstanceIndex < iInstancesCount; iInstanceIndex++)
	{
		OwlClass iChildInstanceClass = GetInstanceClass(piInstances[iInstanceIndex]);
		assert(iChildInstanceClass != 0);

		if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:SolidType"))
		{
			createSolid(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:CompositeSurfaceType"))
		{
			createCompositeSurface(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:ShellType"))
		{
			createMultiSurface(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else
		{
			//#todo
			wchar_t* szClassName = nullptr;
			GetNameOfClassW(iChildInstanceClass, &szClassName);

			string strEvent = "Geometry is not supported: '";
			strEvent += CW2A(szClassName);
			strEvent += "'";
			getSite()->logErr(strEvent);
		}
	} // for (int64_t iInstanceIndex = ...
}

void _citygml_exporter::createCompositeSolid(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	createMultiSolid(iInstance, vecGeometryInstances, bCreateIfcShapeRepresentation);
}

void _citygml_exporter::createMultiSolid(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	OwlInstance* piInstances = nullptr;
	int64_t iInstancesCount = 0;
	GetObjectProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "objects"),
		&piInstances,
		&iInstancesCount);

	for (int64_t iInstanceIndex = 0; iInstanceIndex < iInstancesCount; iInstanceIndex++)
	{
		OwlClass iChildInstanceClass = GetInstanceClass(piInstances[iInstanceIndex]);
		assert(iChildInstanceClass != 0);

		if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:SolidType"))
		{
			createSolid(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else
		{
			//#todo
			wchar_t* szClassName = nullptr;
			GetNameOfClassW(iChildInstanceClass, &szClassName);

			string strEvent = "Geometry is not supported: '";
			strEvent += CW2A(szClassName);
			strEvent += "'";
			getSite()->logErr(strEvent);
		}
	} // for (int64_t iInstanceIndex = ...
}

void _citygml_exporter::createMultiSurface(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	OwlInstance* piInstances = nullptr;
	int64_t iInstancesCount = 0;
	GetObjectProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "objects"),
		&piInstances,
		&iInstancesCount);

	for (int64_t iInstanceIndex = 0; iInstanceIndex < iInstancesCount; iInstanceIndex++)
	{
		OwlClass iChildInstanceClass = GetInstanceClass(piInstances[iInstanceIndex]);
		assert(iChildInstanceClass != 0);

		if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:CompositeSurfaceType"))
		{
			createCompositeSurface(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:SurfacePropertyType"))
		{
			createSurfaceMember(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "BoundaryRepresentation"))
		{
			createBoundaryRepresentation(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);			
		}
		else 
		{
			//#todo
			wchar_t* szClassName = nullptr;
			GetNameOfClassW(iChildInstanceClass, &szClassName);

			string strEvent = "Geometry is not supported: '";
			strEvent += CW2A(szClassName);
			strEvent += "'";
			getSite()->logErr(strEvent);
		}
	} // for (int64_t iInstanceIndex = ...
}

void _citygml_exporter::createCompositeSurface(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	OwlInstance* piInstances = nullptr;
	int64_t iInstancesCount = 0;
	GetObjectProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "objects"),
		&piInstances,
		&iInstancesCount);

	for (int64_t iInstanceIndex = 0; iInstanceIndex < iInstancesCount; iInstanceIndex++)
	{
		OwlClass iChildInstanceClass = GetInstanceClass(piInstances[iInstanceIndex]);
		assert(iChildInstanceClass != 0);

		if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:CompositeSurfaceType"))
		{
			createCompositeSurface(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:OrientableSurfaceType"))
		{
			//#todo
			getSite()->logErr("Geometry is not supported: 'class:OrientableSurfaceType'");
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:SurfacePropertyType"))
		{
			createSurfaceMember(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "BoundaryRepresentation"))
		{
			createBoundaryRepresentation(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else
		{
			//#todo
			wchar_t* szClassName = nullptr;
			GetNameOfClassW(iChildInstanceClass, &szClassName);

			string strEvent = "Geometry is not supported: '";
			strEvent += CW2A(szClassName);
			strEvent += "'";
			getSite()->logErr(strEvent);
		}
	} // for (int64_t iInstanceIndex = ...
}

void _citygml_exporter::createSurfaceMember(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	OwlInstance* piInstances = nullptr;
	int64_t iInstancesCount = 0;
	GetObjectProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "objects"),
		&piInstances,
		&iInstancesCount);

	for (int64_t iInstanceIndex = 0; iInstanceIndex < iInstancesCount; iInstanceIndex++)
	{
		OwlClass iChildInstanceClass = GetInstanceClass(piInstances[iInstanceIndex]);
		assert(iChildInstanceClass != 0);

		if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "class:CompositeSurfaceType"))
		{
			createCompositeSurface(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else if (iChildInstanceClass == GetClassByName(getSite()->getOwlModel(), "BoundaryRepresentation"))
		{
			createBoundaryRepresentation(piInstances[iInstanceIndex], vecGeometryInstances, bCreateIfcShapeRepresentation);
		}
		else
		{
			//#todo
			wchar_t* szClassName = nullptr;
			GetNameOfClassW(iChildInstanceClass, &szClassName);

			string strEvent = "Geometry is not supported: '";
			strEvent += CW2A(szClassName);
			strEvent += "'";
			getSite()->logErr(strEvent);
		}
	} // for (int64_t iInstanceIndex = ...
}

void _citygml_exporter::createBoundaryRepresentation(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	// Indices
	int64_t* piIndices = nullptr;
	int64_t iIndicesCount = 0;
	GetDatatypeProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "indices"),
		(void**)&piIndices,
		&iIndicesCount);

	// Vertices
	double* pdValue = nullptr;
	int64_t iVerticesCount = 0;
	GetDatatypeProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "vertices"),
		(void**)&pdValue,
		&iVerticesCount);

	vector<SdaiInstance> vecOuterPolygons;
	map<SdaiInstance, vector<SdaiInstance>> mapOuter2InnerPolygons;
	vector<int64_t> vecPolygonIndices;
	map<int64_t, SdaiInstance> mapIndex2Instance;
	for (int64_t iIndex = 0; iIndex < iIndicesCount; iIndex++)
	{
		if (piIndices[iIndex] < 0)
		{
			SdaiInstance iPolyLoopInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcPolyLoop");
			assert(iPolyLoopInstance != 0);

			SdaiAggr pPolygon = sdaiCreateAggrBN(iPolyLoopInstance, "Polygon");
			assert(pPolygon != nullptr);

			for (auto iIndex : vecPolygonIndices)
			{
				assert(mapIndex2Instance.find(iIndex) != mapIndex2Instance.end());

				sdaiAppend(pPolygon, sdaiINSTANCE, (void*)mapIndex2Instance.at(iIndex));
			}

			if (piIndices[iIndex] == -1)
			{
				// Outer Polygon
				vecOuterPolygons.push_back(iPolyLoopInstance);
			}
			else
			{
				// Outer Polygon : [Inner Polygons...]
				assert(piIndices[iIndex] == -2);
				assert(!vecOuterPolygons.empty());

				auto itOuter2InnerPolygons = mapOuter2InnerPolygons.find(vecOuterPolygons.back());
				if (itOuter2InnerPolygons != mapOuter2InnerPolygons.end())
				{
					itOuter2InnerPolygons->second.push_back(iPolyLoopInstance);
				}
				else
				{
					mapOuter2InnerPolygons[vecOuterPolygons.back()] = vector<SdaiInstance>{ iPolyLoopInstance };
				}
			}

			vecPolygonIndices.clear();

			continue;
		} // if (piIndices[iIndex] < 0)

		vecPolygonIndices.push_back(piIndices[iIndex]);

		if (mapIndex2Instance.find(piIndices[iIndex]) == mapIndex2Instance.end())
		{
			mapIndex2Instance[piIndices[iIndex]] = buildCartesianPointInstance(
				pdValue[(piIndices[iIndex] * 3) + 0],
				pdValue[(piIndices[iIndex] * 3) + 1],
				pdValue[(piIndices[iIndex] * 3) + 2]);
		}
	} // for (int64_t iIndex = ...

	assert(vecPolygonIndices.empty());
	assert(!vecOuterPolygons.empty());

	SdaiInstance iClosedShellInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcClosedShell");
	assert(iClosedShellInstance != 0);

	SdaiAggr pCfsFaces = sdaiCreateAggrBN(iClosedShellInstance, "CfsFaces");
	assert(pCfsFaces != nullptr);	

	for (auto iOuterPolygon : vecOuterPolygons)
	{
		// Outer Polygon
		SdaiInstance iFaceOuterBoundInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcFaceOuterBound");
		assert(iFaceOuterBoundInstance != 0);

		sdaiPutAttrBN(iFaceOuterBoundInstance, "Bound", sdaiINSTANCE, (void*)iOuterPolygon);
		sdaiPutAttrBN(iFaceOuterBoundInstance, "Orientation", sdaiENUM, "T");

		SdaiInstance iFaceInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcFace");
		assert(iFaceInstance != 0);

		SdaiAggr pBounds = sdaiCreateAggrBN(iFaceInstance, "Bounds");
		sdaiAppend(pCfsFaces, sdaiINSTANCE, (void*)iFaceInstance);

		sdaiAppend(pBounds, sdaiINSTANCE, (void*)iFaceOuterBoundInstance);

		// Inner Polygons
		auto itOuter2InnerPolygons = mapOuter2InnerPolygons.find(iOuterPolygon);
		if (itOuter2InnerPolygons != mapOuter2InnerPolygons.end())
		{
			for (auto iInnerPolygon : itOuter2InnerPolygons->second)
			{
				SdaiInstance iFaceBoundInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcFaceBound");
				assert(iFaceBoundInstance != 0);

				sdaiPutAttrBN(iFaceBoundInstance, "Bound", sdaiINSTANCE, (void*)iInnerPolygon);
				sdaiPutAttrBN(iFaceBoundInstance, "Orientation", sdaiENUM, "T");

				sdaiAppend(pBounds, sdaiINSTANCE, (void*)iFaceBoundInstance);
			}
		}
	} // auto iOuterPolygon : ...

	SdaiInstance iFacetedBrepInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcFacetedBrep");
	assert(iFacetedBrepInstance != 0);

	sdaiPutAttrBN(iFacetedBrepInstance, "Outer", sdaiINSTANCE, (void*)iClosedShellInstance);

	createStyledItemInstance(iInstance, iFacetedBrepInstance);

	if (bCreateIfcShapeRepresentation)
	{
		SdaiInstance iShapeRepresentationInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcShapeRepresentation");
		assert(iShapeRepresentationInstance != 0);

		SdaiAggr pItems = sdaiCreateAggrBN(iShapeRepresentationInstance, "Items");
		assert(pItems != 0);

		sdaiAppend(pItems, sdaiINSTANCE, (void*)iFacetedBrepInstance);

		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, "Body");
		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "Brep");
		sdaiPutAttrBN(iShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());

		vecGeometryInstances.push_back(iShapeRepresentationInstance);
	}
	else
	{
		vecGeometryInstances.push_back(iFacetedBrepInstance);
	}	
}

void _citygml_exporter::createPoint3D(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	int64_t iValuesCount = 0;
	double* pdValue = nullptr;
	GetDatatypeProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "points"),
		(void**)&pdValue,
		&iValuesCount);

	assert(iValuesCount == 3);

	SdaiInstance iCartesianPointInstance = buildCartesianPointInstance(
		pdValue[0],
		pdValue[1],
		pdValue[2]);
	assert(iCartesianPointInstance != 0);

	if (bCreateIfcShapeRepresentation)
	{
		SdaiInstance iShapeRepresentationInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcShapeRepresentation");
		assert(iShapeRepresentationInstance != 0);

		SdaiAggr pItems = sdaiCreateAggrBN(iShapeRepresentationInstance, "Items");
		assert(pItems != 0);

		sdaiAppend(pItems, sdaiINSTANCE, (void*)iCartesianPointInstance);

		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, "Body");
		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "PointCloud");
		sdaiPutAttrBN(iShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());

		vecGeometryInstances.push_back(iShapeRepresentationInstance);
	}
	else
	{
		vecGeometryInstances.push_back(iCartesianPointInstance);
	}	
}

void _citygml_exporter::createPoint3DSet(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	int64_t iValuesCount = 0;
	double* pdValue = nullptr;
	GetDatatypeProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "points"),
		(void**)&pdValue,
		&iValuesCount);

	assert(iValuesCount >= 3);

	if (bCreateIfcShapeRepresentation)
	{
		SdaiInstance iShapeRepresentationInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcShapeRepresentation");
		assert(iShapeRepresentationInstance != 0);

		SdaiAggr pItems = sdaiCreateAggrBN(iShapeRepresentationInstance, "Items");
		assert(pItems != 0);

		for (int64_t iValue = 0; iValue < iValuesCount; iValue += 3)
		{
			SdaiInstance iCartesianPointInstance = buildCartesianPointInstance(
				pdValue[iValue + 0],
				pdValue[iValue + 1],
				pdValue[iValue + 2]);
			assert(iCartesianPointInstance != 0);

			sdaiAppend(pItems, sdaiINSTANCE, (void*)iCartesianPointInstance);
		} // for (int64_t iValue = ...

		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, "Body");
		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "PointCloud");
		sdaiPutAttrBN(iShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());

		vecGeometryInstances.push_back(iShapeRepresentationInstance);
	}
	else
	{
		for (int64_t iValue = 0; iValue < iValuesCount; iValue += 3)
		{
			SdaiInstance iCartesianPointInstance = buildCartesianPointInstance(
				pdValue[iValue + 0],
				pdValue[iValue + 1],
				pdValue[iValue + 2]);
			assert(iCartesianPointInstance != 0);

			vecGeometryInstances.push_back(iCartesianPointInstance);
		} // for (int64_t iValue = ...
	}	
}

void _citygml_exporter::createPolyLine3D(OwlInstance iInstance, vector<SdaiInstance>& vecGeometryInstances, bool bCreateIfcShapeRepresentation)
{
	assert(iInstance != 0);

	int64_t iValuesCount = 0;
	double* pdValue = nullptr;
	GetDatatypeProperty(
		iInstance,
		GetPropertyByName(getSite()->getOwlModel(), "points"),
		(void**)&pdValue,
		&iValuesCount);

	assert(iValuesCount >= 6);

	SdaiInstance iPolyLineInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcPolyline");
	assert(iPolyLineInstance != 0);

	SdaiAggr pPoints = sdaiCreateAggrBN(iPolyLineInstance, "Points");
	assert(pPoints != nullptr);

	for (int64_t iValue = 0; iValue < iValuesCount; iValue += 3)
	{
		SdaiInstance iCartesianPointInstance = buildCartesianPointInstance(
			pdValue[iValue + 0],
			pdValue[iValue + 1],
			pdValue[iValue + 2]);
		assert(iCartesianPointInstance != 0);

		sdaiAppend(pPoints, sdaiINSTANCE, (void*)iCartesianPointInstance);
	} // for (int64_t iValue = ...

	if (bCreateIfcShapeRepresentation)
	{
		SdaiInstance iShapeRepresentationInstance = sdaiCreateInstanceBN(getIfcModel(), "IfcShapeRepresentation");
		assert(iShapeRepresentationInstance != 0);

		SdaiAggr pItems = sdaiCreateAggrBN(iShapeRepresentationInstance, "Items");
		assert(pItems != 0);

		sdaiAppend(pItems, sdaiINSTANCE, (void*)iPolyLineInstance);

		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, "Body");
		sdaiPutAttrBN(iShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "Curve3D");
		sdaiPutAttrBN(iShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*)getGeometricRepresentationContextInstance());

		vecGeometryInstances.push_back(iShapeRepresentationInstance);
	}
	else
	{
		vecGeometryInstances.push_back(iPolyLineInstance);
	}	
}

void _citygml_exporter::createProperties(OwlInstance iOwlInstance, SdaiInstance iSdaiInstance)
{
	assert(iOwlInstance != 0);
	assert(iSdaiInstance != 0);

	map<string, SdaiInstance> mapProperties;

	RdfProperty iPropertyInstance = GetInstancePropertyByIterator(iOwlInstance, 0);
	while (iPropertyInstance != 0)
	{
		char* szPropertyName = nullptr;
		GetNameOfProperty(iPropertyInstance, &szPropertyName);

		string strPropertyName = szPropertyName;
		
		if (strPropertyName.find("prop:") == 0)
		{
			// Properties
			switch (GetPropertyType(iPropertyInstance))
			{
				case DATATYPEPROPERTY_TYPE_DOUBLE:
				{
					double* pdValue = nullptr;
					int64_t iValuesCount = 0;
					GetDatatypeProperty(iOwlInstance, iPropertyInstance, (void**)&pdValue, &iValuesCount);

					assert(iValuesCount == 1);

					mapProperties[szPropertyName] = buildPropertySingleValueReal(
						strPropertyName.c_str(),
						"attribute",
						pdValue[0],
						"IFCREAL");
				}
				break;

				case DATATYPEPROPERTY_TYPE_WCHAR_T_ARRAY:
				{
					wchar_t** szValue = nullptr;
					int64_t iValuesCount = 0;
					GetDatatypeProperty(iOwlInstance, iPropertyInstance, (void**)&szValue, &iValuesCount);

					mapProperties[szPropertyName] = buildPropertySingleValueText(
						strPropertyName.c_str(),
						"attribute",
						CW2A(szValue[0]),
						"IFCTEXT");
				}
				break;

				case OBJECTTYPEPROPERTY_TYPE:
				{
					//#todo
				}
				break;

				default:
				{
					//#todo
				}
				break;
			} // switch (GetPropertyType(iPropertyInstance))
		} // prop:
		else if (strPropertyName.find("attr:") == 0)
		{
			// Attributes
			assert(GetPropertyType(iPropertyInstance) == DATATYPEPROPERTY_TYPE_WCHAR_T_ARRAY);

			wchar_t** szValue = nullptr;
			int64_t iValuesCount = 0;
			GetDatatypeProperty(iOwlInstance, iPropertyInstance, (void**)&szValue, &iValuesCount);

			assert(iValuesCount == 1);

			mapProperties[szPropertyName] = buildPropertySingleValueText(
				strPropertyName.c_str(),
				"attribute",
				CW2A(szValue[0]),
				"IFCTEXT");
		} // attr:

		iPropertyInstance = GetInstancePropertyByIterator(iOwlInstance, iPropertyInstance);
	} // while (iPropertyInstance != 0)

	if (mapProperties.empty())
	{
		return;
	}

	SdaiAggr pHasProperties = nullptr;
	SdaiInstance iPropertySetInstance = buildPropertySet("Attributes & Properties", pHasProperties);

	for (auto itProperty : mapProperties)
	{
		sdaiAppend(pHasProperties, sdaiINSTANCE, (void*)itProperty.second);
	}

	buildRelDefinesByProperties(iSdaiInstance, iPropertySetInstance);
}

SdaiInstance _citygml_exporter::buildBuildingElementInstance(
	OwlInstance iOwlInstance,
	_matrix* pMatrix,
	SdaiInstance iPlacementRelativeTo,
	SdaiInstance& iBuildingElementInstancePlacement,
	const vector<SdaiInstance>& vecRepresentations)
{
	assert(iOwlInstance != 0);
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);
	assert(!vecRepresentations.empty());

	string strTag = getTag(iOwlInstance);

	OwlClass iInstanceClass = GetInstanceClass(iOwlInstance);
	assert(iInstanceClass != 0);

	char* szClassName = nullptr;
	GetNameOfClass(iInstanceClass, &szClassName);
	assert(szClassName != nullptr);

	string strClass = szClassName;

	string strEntity = "IfcBuildingElement";
	if (isWallSurfaceClass(iInstanceClass))
	{
		strEntity = "IfcWall";
	}
	else if (isRoofSurfaceClass(iInstanceClass))
	{
		strEntity = "IfcRoof";
	}
	else if (isDoorClass(iInstanceClass))
	{
		strEntity = "IfcDoor";
	}
	else if (isWindowClass(iInstanceClass))
	{
		strEntity = "IfcWindow";
	}
	else
	{
		strEntity = "IfcBuildingElementProxy"; // Proxy/Unknown Building Element
	}

	return _exporter_base::buildBuildingElementInstance(
		strEntity.c_str(),
		strTag.c_str(),
		szClassName,
		pMatrix,
		iPlacementRelativeTo,
		iBuildingElementInstancePlacement,
		vecRepresentations);
}

bool _citygml_exporter::isCollectionClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iCollectionClass) || IsClassAncestor(iInstanceClass, m_iCollectionClass);
}

bool _citygml_exporter::isTransformationClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iTransformationClass) || IsClassAncestor(iInstanceClass, m_iTransformationClass);
}

bool _citygml_exporter::isBuildingElement(OwlInstance iInstance) const
{
	assert(iInstance != 0);

	OwlClass iInstanceClass = GetInstanceClass(iInstance);
	assert(iInstanceClass != 0);

	if (isWallSurfaceClass(iInstanceClass))
	{
		return true;
	}

	if (isRoofSurfaceClass(iInstanceClass))
	{
		return true;
	}
	
	if (isDoorClass(iInstanceClass))
	{
		return true;
	}
	
	if (isWindowClass(iInstanceClass))
	{
		return true;
	}

	return false;
}

bool _citygml_exporter::isBuildingClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iBuildingClass) || IsClassAncestor(iInstanceClass, m_iBuildingClass);
}

bool _citygml_exporter::isWallSurfaceClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iWallSurfaceClass) || IsClassAncestor(iInstanceClass, m_iWallSurfaceClass);
}

bool _citygml_exporter::isRoofSurfaceClass(OwlInstance iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iRoofSurfaceClass) || IsClassAncestor(iInstanceClass, m_iRoofSurfaceClass);
}

bool _citygml_exporter::isDoorClass(OwlInstance iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iDoorClass) || IsClassAncestor(iInstanceClass, m_iDoorClass);
}

bool _citygml_exporter::isWindowClass(OwlInstance iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iWindowClass) || IsClassAncestor(iInstanceClass, m_iWindowClass);
}

bool _citygml_exporter::isFeatureClass(OwlInstance iInstanceClass) const
{
	assert(iInstanceClass != 0);

	if (isVegetationObjectClass(iInstanceClass) ||
		isWaterObjectClass(iInstanceClass) ||
		isBridgeObjectClass(iInstanceClass) ||
		isTunnelObjectClass(iInstanceClass) ||
		isTransportationObjectClass(iInstanceClass) ||
		isFurnitureObjectClass(iInstanceClass) ||
		isReliefObjectClass(iInstanceClass))
	{
		return true;
	}

	return false;
}

bool _citygml_exporter::isVegetationObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iVegetationObjectClass) || IsClassAncestor(iInstanceClass, m_iVegetationObjectClass);
}

bool _citygml_exporter::isWaterObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iWaterObjectClass) || IsClassAncestor(iInstanceClass, m_iWaterObjectClass);
}

bool _citygml_exporter::isBridgeObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iBridgeObjectClass) || IsClassAncestor(iInstanceClass, m_iBridgeObjectClass);
}

bool _citygml_exporter::isTunnelObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iTunnelObjectClass) || IsClassAncestor(iInstanceClass, m_iTunnelObjectClass);
}

bool _citygml_exporter::isTransportationObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iTransportationObjectClass) || IsClassAncestor(iInstanceClass, m_iTransportationObjectClass);
}

bool _citygml_exporter::isFurnitureObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iFurnitureObjectClass) || IsClassAncestor(iInstanceClass, m_iFurnitureObjectClass);
}

bool _citygml_exporter::isReliefObjectClass(OwlClass iInstanceClass) const
{
	assert(iInstanceClass != 0);

	return (iInstanceClass == m_iReliefObjectClass) || IsClassAncestor(iInstanceClass, m_iReliefObjectClass);
}

// ************************************************************************************************
_cityjson_exporter::_cityjson_exporter(_gis2ifc* pSite)
	: _citygml_exporter(pSite)
{}

/*virtual*/ _cityjson_exporter::~_cityjson_exporter()
{}
