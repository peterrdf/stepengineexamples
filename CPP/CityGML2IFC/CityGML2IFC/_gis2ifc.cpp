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
		m_iPersonInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCPERSON");
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
		m_iOrganizationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCORGANIZATION");
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
		m_iPersonAndOrganizationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCPERSONANDORGANIZATION");
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
		m_iApplicationInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCAPPLICATION");
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
		int_t iTimeStamp = time(0);

		m_iOwnerHistoryInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCOWNERHISTORY");
		assert(m_iOwnerHistoryInstance != 0);

		sdaiPutAttrBN(m_iOwnerHistoryInstance, "OwningUser", sdaiINSTANCE, (void*)getPersonAndOrganizationInstance());
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "OwningApplication", sdaiINSTANCE, (void*)getApplicationInstance());
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "ChangeAction", sdaiENUM, "ADDED");
		sdaiPutAttrBN(m_iOwnerHistoryInstance, "CreationDate", sdaiINTEGER, &iTimeStamp);
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

		m_iDimensionalExponentsInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCDIMENSIONALEXPONENTS");
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
		m_iConversionBasedUnitInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCCONVERSIONBASEDUNIT");
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
		m_iUnitAssignmentInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCUNITASSIGNMENT");
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
		m_iWorldCoordinateSystemInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCAXIS2PLACEMENT3D");
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

		m_iGeometricRepresentationContextInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCGEOMETRICREPRESENTATIONCONTEXT");
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
		m_iProjectInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCPROJECT");
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

	_matrix matrix;
	SdaiInstance iSiteInstancePlacement = 0;
	SdaiInstance iSiteInstance = buildSiteInstance(&matrix, iSiteInstancePlacement);
	assert(iSiteInstancePlacement != 0);

	/*ifcBuildingInstance = buildBuildingInstance(&matrix, ifcSiteInstancePlacement, &ifcBuildingInstancePlacement);*/
}

SdaiInstance _exporter_base::buildSIUnitInstance(const char* szUnitType, const char* szPrefix, const char* szName)
{
	assert(szUnitType != nullptr);
	assert(szName != nullptr);

	SdaiInstance iSIUnitInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCSIUNIT");
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

	SdaiInstance iMeasureWithUnitInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCMEASUREWITHUNIT");
	assert(iMeasureWithUnitInstance != 0);	

	sdaiPutADBTypePath(pValueComponentADB, 1, "IFCPLANEANGLEMEASURE");
	sdaiPutAttrBN(iMeasureWithUnitInstance, "ValueComponent", sdaiADB, (void*)pValueComponentADB);
	sdaiPutAttrBN(iMeasureWithUnitInstance, "UnitComponent", sdaiINSTANCE, (void*)buildSIUnitInstance("PLANEANGLEUNIT", NULL, "RADIAN"));
	
	return iMeasureWithUnitInstance;
}

SdaiInstance _exporter_base::buildDirectionInstance(double dX, double dY, double dZ)
{
	SdaiInstance iDirectionInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCDIRECTION");
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
	SdaiInstance iCartesianPointInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCCARTESIANPOINT");
	assert(iCartesianPointInstance != 0);

	SdaiAggr pCoordinates = sdaiCreateAggrBN(iCartesianPointInstance, "Coordinates");
	assert(pCoordinates != nullptr);

	sdaiAppend(pCoordinates, sdaiREAL, &dX);
	sdaiAppend(pCoordinates, sdaiREAL, &dY);
	sdaiAppend(pCoordinates, sdaiREAL, &dZ);

	return iCartesianPointInstance;
}

SdaiInstance _exporter_base::buildSiteInstance(_matrix* pMatrix, SdaiInstance& iSiteInstancePlacement)
{
	assert(pMatrix != nullptr);

	SdaiInstance iSiteInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCSITE");
	assert(iSiteInstance != 0);

	sdaiPutAttrBN(iSiteInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iSiteInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iSiteInstance, "Name", sdaiSTRING, "Default Site"); //#tbd
	sdaiPutAttrBN(iSiteInstance, "Description", sdaiSTRING, "Description of Default Site"); //#tbd

	iSiteInstancePlacement = buildLocalPlacementInstance(pMatrix, 0);
	assert(iSiteInstancePlacement != 0);

	sdaiPutAttrBN(iSiteInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iSiteInstancePlacement);
	sdaiPutAttrBN(iSiteInstance, "CompositionType", sdaiENUM, "ELEMENT");

	SdaiAggr pRefLatitude = sdaiCreateAggrBN(iSiteInstance, "RefLatitude");
	assert(pRefLatitude != 0);

	int_t refLat_x = 24, refLat_y = 28, refLat_z = 0; //#tbd
	sdaiAppend(pRefLatitude, sdaiINTEGER, &refLat_x);
	sdaiAppend(pRefLatitude, sdaiINTEGER, &refLat_y);
	sdaiAppend(pRefLatitude, sdaiINTEGER, &refLat_z);

	SdaiAggr pRefLongitude = sdaiCreateAggrBN(iSiteInstance, "RefLongitude");
	assert(pRefLongitude != 0);

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
	SdaiInstance iLocalPlacementInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCLOCALPLACEMENT");
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
	SdaiInstance iAxis2Placement3DInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCAXIS2PLACEMENT3D");
	assert(iAxis2Placement3DInstance != 0);

	sdaiPutAttrBN(iAxis2Placement3DInstance, "Location", sdaiINSTANCE, (void*)buildCartesianPointInstance(pMatrix->_41, pMatrix->_42, pMatrix->_43));
	sdaiPutAttrBN(iAxis2Placement3DInstance, "Axis", sdaiINSTANCE, (void*)buildDirectionInstance(pMatrix->_31, pMatrix->_32, pMatrix->_33));
	sdaiPutAttrBN(iAxis2Placement3DInstance, "RefDirection", sdaiINSTANCE, (void*)buildDirectionInstance(pMatrix->_11, pMatrix->_12, pMatrix->_13));

	return iAxis2Placement3DInstance;
}

SdaiInstance _exporter_base::buildBuildingInstance(_matrix* pMatrix, SdaiInstance iPlacementRelativeTo, SdaiInstance& iBuildingInstancePlacement)
{
	assert(pMatrix != nullptr);
	assert(iPlacementRelativeTo != 0);

	SdaiInstance iBuildingInstance = sdaiCreateInstanceBN(m_iIfcModel, "IFCBUILDING");
	assert(iBuildingInstance != 0);

	sdaiPutAttrBN(iBuildingInstance, "GlobalId", sdaiSTRING, (void*)_guid::createGlobalId().c_str());
	sdaiPutAttrBN(iBuildingInstance, "OwnerHistory", sdaiINSTANCE, (void*)getOwnerHistoryInstance());
	sdaiPutAttrBN(iBuildingInstance, "Name", sdaiSTRING, "Default Building"); //#tbd
	sdaiPutAttrBN(iBuildingInstance, "Description", sdaiSTRING, "Description of Default Building"); //#tbd

	iBuildingInstancePlacement = buildLocalPlacementInstance(pMatrix, iPlacementRelativeTo);
	assert(iBuildingInstancePlacement != 0);

	sdaiPutAttrBN(iBuildingInstance, "ObjectPlacement", sdaiINSTANCE, (void*)iBuildingInstancePlacement);
	sdaiPutAttrBN(iBuildingInstance, "CompositionType", sdaiENUM, "ELEMENT");
	//sdaiPutAttrBN(iBuildingInstance, "BuildingAddress", sdaiINSTANCE, (void*)buildPostalAddress()); //#tbd

	return iBuildingInstance;
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
