////////////////////////////////////////////////////////////////////////
//	Author:		Peter Bonsma
//	Date:		20 February 2019
//	Project:	IFC Engine Series (example using DLL)
//
//	This code may be used and edited freely,
//	also for commercial projects in open and closed source software
//
//	In case of use of the DLL:
//	be aware of license fee for use of this DLL when used commercially
//	more info for commercial use:	contact@rdf.bg
//
//	more info for using the IFC Engine DLL in other languages
//	and creation of specific code examples:
//									peter.bonsma@rdf.bg
////////////////////////////////////////////////////////////////////////


#include	"stdafx.h"
#include	"baseIfc.h"

#include	<stdio.h>
#include	<stdlib.h>
#include	<string.h>
#include	<assert.h>

#include	<ctime>
#include	<iostream>


int_t	model = 0,
		timeStamp,
		* aggrRelatedElements,
		* aggr_ifcRelDeclaresInstance_RelatedDefinitions_ProjectLibrary,
		ifcApplicationInstance,
		ifcBuildingInstance,
		ifcBuildingInstancePlacement,
		ifcBuildingStoreyInstance,
		ifcBuildingStoreyInstancePlacement,
		ifcBuildOwnerHistoryInstance,
		ifcConversionBasedUnitInstance,
		ifcDimensionalExponentsInstance,
		ifcGeometricRepresentationContextInstance,
		ifcOrganizationInstance,
		ifcPersonAndOrganizationInstance,
		ifcPersonInstance,
		ifcPostalAddressInstance,
		ifcProjectInstance,
		ifcRelDeclaresInstance,
		ifcSiteInstance,
		ifcSiteInstancePlacement,
		ifcUnitAssignmentInstance;


int_t	createEmptyIfcFile(wchar_t * ifcSchemaName, bool objectsWillBeAdded, char * lengthUnitConversion)
{
	model = sdaiCreateModelBNUnicode(1, 0, ifcSchemaName);

	if (model) {
		MATRIX	matrix;
		identityMatrix(&matrix);

		timeStamp = 0;
		aggrRelatedElements = nullptr;
		aggr_ifcRelDeclaresInstance_RelatedDefinitions_ProjectLibrary = nullptr;
		ifcApplicationInstance = 0;
		ifcBuildingInstance = 0;
		ifcBuildingInstancePlacement = 0;
		ifcBuildingStoreyInstance = 0;
		ifcBuildingStoreyInstancePlacement = 0;
		ifcBuildOwnerHistoryInstance = 0;
		ifcConversionBasedUnitInstance = 0;
		ifcDimensionalExponentsInstance = 0;
		ifcGeometricRepresentationContextInstance = 0;
		ifcOrganizationInstance = 0;
		ifcPersonAndOrganizationInstance = 0;
		ifcPersonInstance = 0;
		ifcPostalAddressInstance = 0;
		ifcProjectInstance = 0;
		ifcRelDeclaresInstance = 0;
		ifcSiteInstance = 0;
		ifcSiteInstancePlacement = 0;
		ifcUnitAssignmentInstance = 0;

		//
		//	Build standard IFC structures
		//

		//
		//		Build standard IFC objects
		//
		ifcProjectInstance = getProjectInstance(lengthUnitConversion);
		ifcSiteInstance = buildSiteInstance(&matrix, 0, &ifcSiteInstancePlacement);
		ifcBuildingInstance = buildBuildingInstance(&matrix, ifcSiteInstancePlacement, &ifcBuildingInstancePlacement);
		ifcBuildingStoreyInstance = buildBuildingStoreyInstance(&matrix, ifcBuildingInstancePlacement, &ifcBuildingStoreyInstancePlacement);

		//
		//		Build relations
		//
		buildRelAggregatesInstance( "BuildingContainer", "BuildingContainer for BuildigStories", ifcBuildingInstance, ifcBuildingStoreyInstance);
		buildRelAggregatesInstance( "SiteContainer", "SiteContainer For Buildings", ifcSiteInstance, ifcBuildingInstance);
		buildRelAggregatesInstance( "ProjectContainer", "ProjectContainer for Sites", ifcProjectInstance, ifcSiteInstance);

		if (objectsWillBeAdded) {
			buildRelContainedInSpatialStructureInstance( "BuildingStoreyContainer", "BuildingStoreyContainer for Building Elements", ifcBuildingStoreyInstance, &aggrRelatedElements);
		}

		return	model;
	}
	else {
		return	0;
	}
}

bool	saveIfcFile(wchar_t * ifcFileName)
{
	if (model) {
		//
		//	Save the created configuration
		//
		sdaiSaveModelBNUnicode(model, ifcFileName);

		return	true;
	}
	else {
		return	false;
	}
}

bool	saveIfcFileAsXml(wchar_t * ifcFileName)
{
	if (model) {
		//
		//  Save the created configuration
		//
		sdaiSaveModelAsXmlBNUnicode(model, ifcFileName);

		return	true;
	}
	else {
		return	false;
	}
}

void	identityMatrix(MATRIX * matrix)
{
	matrix->_11 = 1;
	matrix->_12 = 0;
	matrix->_13 = 0;
	matrix->_21 = 0;
	matrix->_22 = 1;
	matrix->_23 = 0;
	matrix->_31 = 0;
	matrix->_32 = 0;
	matrix->_33 = 1;
	matrix->_41 = 0;
	matrix->_42 = 0;
	matrix->_43 = 0;
}

void	identityPoint(POINT3D * point)
{
	point->x = 0;
	point->y = 0;
	point->z = 0;
}

int_t	* getTimeStamp()
{
	timeStamp = (int_t) time(0);

	return	&timeStamp;
}


//
//
//		Application, Organization, Person (OwnerHistory, PersonAndOrganization)
//
//


int_t	getApplicationInstance()
{
	if (!ifcApplicationInstance) {
		ifcApplicationInstance = sdaiCreateInstanceBN(model, "IFCAPPLICATION");

		sdaiPutAttrBN(ifcApplicationInstance, "ApplicationDeveloper", sdaiINSTANCE, (void*) getOrganizationInstance());
		sdaiPutAttrBN(ifcApplicationInstance, "Version", sdaiSTRING, "0.10");
		sdaiPutAttrBN(ifcApplicationInstance, "ApplicationFullName", sdaiSTRING, "Test Application");
		sdaiPutAttrBN(ifcApplicationInstance, "ApplicationIdentifier", sdaiSTRING, "TA 1001");
	}

	assert(ifcApplicationInstance);

	return	ifcApplicationInstance;
}

int_t	getOrganizationInstance()
{
	if (!ifcOrganizationInstance) {
		ifcOrganizationInstance = sdaiCreateInstanceBN(model, "IFCORGANIZATION");

		sdaiPutAttrBN(ifcOrganizationInstance, "Name", sdaiSTRING, "RDF");
		sdaiPutAttrBN(ifcOrganizationInstance, "Description", sdaiSTRING, "RDF Ltd.");
	}

	assert(ifcOrganizationInstance);

	return	ifcOrganizationInstance;
}

int_t	getOwnerHistoryInstance()
{
	if (!ifcBuildOwnerHistoryInstance) {
		ifcBuildOwnerHistoryInstance = sdaiCreateInstanceBN(model, "IFCOWNERHISTORY");

		sdaiPutAttrBN(ifcBuildOwnerHistoryInstance, "OwningUser", sdaiINSTANCE, (void*) getPersonAndOrganizationInstance());
		sdaiPutAttrBN(ifcBuildOwnerHistoryInstance, "OwningApplication", sdaiINSTANCE, (void*) getApplicationInstance());
		sdaiPutAttrBN(ifcBuildOwnerHistoryInstance, "ChangeAction", sdaiENUM, "ADDED");
		sdaiPutAttrBN(ifcBuildOwnerHistoryInstance, "CreationDate", sdaiINTEGER, (void*) getTimeStamp());
	}

	assert(ifcBuildOwnerHistoryInstance);

	return	ifcBuildOwnerHistoryInstance;
}

int_t	getPersonAndOrganizationInstance()
{
	if (!ifcPersonAndOrganizationInstance) {
		ifcPersonAndOrganizationInstance = sdaiCreateInstanceBN(model, "IFCPERSONANDORGANIZATION");

		sdaiPutAttrBN(ifcPersonAndOrganizationInstance, "ThePerson", sdaiINSTANCE, (void*) getPersonInstance());
		sdaiPutAttrBN(ifcPersonAndOrganizationInstance, "TheOrganization", sdaiINSTANCE, (void*) getOrganizationInstance());
	}

	assert(ifcPersonAndOrganizationInstance);

	return	ifcPersonAndOrganizationInstance;
}

int_t	getPersonInstance()
{
	if (!ifcPersonInstance) {
		ifcPersonInstance = sdaiCreateInstanceBN(model, "IFCPERSON");

		sdaiPutAttrBN(ifcPersonInstance, "GivenName", sdaiSTRING, "Peter");
		sdaiPutAttrBN(ifcPersonInstance, "FamilyName", sdaiSTRING, "Bonsma");
	}

	assert(ifcPersonInstance);

	return	ifcPersonInstance;
}


//
//
//		CartesianPoint, Direction, LocalPlacement (Axis2Placement)
//
//


int_t	buildAxis2Placement3DInstance(MATRIX * matrix)
{
	int_t	ifcAxis2Placement3DInstance;

	ifcAxis2Placement3DInstance = sdaiCreateInstanceBN(model, "IFCAXIS2PLACEMENT3D");

	sdaiPutAttrBN(ifcAxis2Placement3DInstance, "Location", sdaiINSTANCE, (void*) buildCartesianPointInstance((POINT3D*) &matrix->_41));
	sdaiPutAttrBN(ifcAxis2Placement3DInstance, "Axis", sdaiINSTANCE, (void*) buildDirectionInstance((POINT3D*) &matrix->_31));
	sdaiPutAttrBN(ifcAxis2Placement3DInstance, "RefDirection", sdaiINSTANCE, (void*) buildDirectionInstance((POINT3D*) &matrix->_11));

	assert(ifcAxis2Placement3DInstance);

	return	ifcAxis2Placement3DInstance;
}

int_t	buildCartesianPointInstance(POINT3D * point)
{
	int_t	ifcCartesianPointInstance, * aggrCoordinates;

	ifcCartesianPointInstance = sdaiCreateInstanceBN(model, "IFCCARTESIANPOINT");

	aggrCoordinates = sdaiCreateAggrBN(ifcCartesianPointInstance, "Coordinates");

	sdaiAppend(aggrCoordinates, sdaiREAL, &point->x);
	sdaiAppend(aggrCoordinates, sdaiREAL, &point->y);
	sdaiAppend(aggrCoordinates, sdaiREAL, &point->z);

	assert(ifcCartesianPointInstance);

	return	ifcCartesianPointInstance;
}

int_t	buildDirectionInstance(POINT3D * point)
{
	int_t	ifcDirectionInstance, * aggrDirectionRatios;

	ifcDirectionInstance = sdaiCreateInstanceBN(model, "IFCDIRECTION");

	aggrDirectionRatios = sdaiCreateAggrBN(ifcDirectionInstance, "DirectionRatios");

	sdaiAppend(aggrDirectionRatios, sdaiREAL, &point->x);
	sdaiAppend(aggrDirectionRatios, sdaiREAL, &point->y);
	sdaiAppend(aggrDirectionRatios, sdaiREAL, &point->z);

	assert(ifcDirectionInstance);

	return	ifcDirectionInstance;
}

int_t	buildLocalPlacementInstance(MATRIX * matrix, int_t ifcPlacementRelativeTo)
{
	int_t	ifcLocalPlacementInstance;

	ifcLocalPlacementInstance = sdaiCreateInstanceBN(model, "IFCLOCALPLACEMENT");

	if (ifcPlacementRelativeTo) {
		sdaiPutAttrBN(ifcLocalPlacementInstance, "PlacementRelTo", sdaiINSTANCE, (void*) ifcPlacementRelativeTo);
	}
	sdaiPutAttrBN(ifcLocalPlacementInstance, "RelativePlacement", sdaiINSTANCE, (void*) buildAxis2Placement3DInstance(matrix));

	assert(ifcLocalPlacementInstance);

	return	ifcLocalPlacementInstance;
}


//
//
//		ConversionBasedUnit, DimensionalExponents, MeasureWithUnit, SIUnit, UnitAssignment
//
//


int_t	getConversionBasedUnitInstance()
{
	if (!ifcConversionBasedUnitInstance) {
		ifcConversionBasedUnitInstance = sdaiCreateInstanceBN(model, "IFCCONVERSIONBASEDUNIT");

		sdaiPutAttrBN(ifcConversionBasedUnitInstance, "Dimensions", sdaiINSTANCE, (void*) getDimensionalExponentsInstance());
		sdaiPutAttrBN(ifcConversionBasedUnitInstance, "UnitType", sdaiENUM, "PLANEANGLEUNIT");
		sdaiPutAttrBN(ifcConversionBasedUnitInstance, "Name", sdaiSTRING, "DEGREE");
		sdaiPutAttrBN(ifcConversionBasedUnitInstance, "ConversionFactor", sdaiINSTANCE, (void*) buildMeasureWithUnitInstance());
	}

	assert(ifcConversionBasedUnitInstance);

	return	ifcConversionBasedUnitInstance;
}

int_t	getDimensionalExponentsInstance()
{
	if (!ifcDimensionalExponentsInstance) {
		int_t	LengthExponent = 0,
				MassExponent = 0,
				TimeExponent = 0,
				ElectricCurrentExponent = 0,
				ThermodynamicTemperatureExponent = 0,
				AmountOfSubstanceExponent = 0,
				LuminousIntensityExponent = 0;

		ifcDimensionalExponentsInstance = sdaiCreateInstanceBN(model, "IFCDIMENSIONALEXPONENTS");

		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "LengthExponent", sdaiINTEGER, &LengthExponent);
		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "MassExponent", sdaiINTEGER, &MassExponent);
		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "TimeExponent", sdaiINTEGER, &TimeExponent);
		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "ElectricCurrentExponent", sdaiINTEGER, &ElectricCurrentExponent);
		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "ThermodynamicTemperatureExponent", sdaiINTEGER, &ThermodynamicTemperatureExponent);
		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "AmountOfSubstanceExponent", sdaiINTEGER, &AmountOfSubstanceExponent);
		sdaiPutAttrBN(ifcDimensionalExponentsInstance, "LuminousIntensityExponent", sdaiINTEGER, &LuminousIntensityExponent);
	}

	assert(ifcDimensionalExponentsInstance);

	return	ifcDimensionalExponentsInstance;
}

int_t	buildMeasureWithUnitInstance()
{
	int_t	ifcMeasureWithUnitInstance;
	void	* valueComponentADB;
	double	valueComponent= 0.01745;

	ifcMeasureWithUnitInstance = sdaiCreateInstanceBN(model, "IFCMEASUREWITHUNIT");

	valueComponentADB = sdaiCreateADB(sdaiREAL, &valueComponent);
	sdaiPutADBTypePath(valueComponentADB, 1, "IFCPLANEANGLEMEASURE"); 
	sdaiPutAttrBN(ifcMeasureWithUnitInstance, "ValueComponent", sdaiADB, (void*) valueComponentADB);

	sdaiPutAttrBN(ifcMeasureWithUnitInstance, "UnitComponent", sdaiINSTANCE, (void*) buildSIUnitInstance( "PLANEANGLEUNIT", 0, "RADIAN"));

	assert(ifcMeasureWithUnitInstance);

	return	ifcMeasureWithUnitInstance;
}

int_t	buildSIUnitInstance(char * UnitType, char * Prefix, char * Name)
{
	int_t	ifcSIUnitInstance;

	ifcSIUnitInstance = sdaiCreateInstanceBN(model, "IFCSIUNIT");

	sdaiPutAttrBN(ifcSIUnitInstance, "Dimensions", sdaiINTEGER, (int_t) 0);
	sdaiPutAttrBN(ifcSIUnitInstance, "UnitType", sdaiENUM, UnitType);
	if (Prefix) {
		sdaiPutAttrBN(ifcSIUnitInstance, "Prefix", sdaiENUM, Prefix);
	}
	sdaiPutAttrBN(ifcSIUnitInstance, "Name", sdaiENUM, Name);

	assert(ifcSIUnitInstance);

	return	ifcSIUnitInstance;
}

int_t	getUnitAssignmentInstance(char * lengthUnitConversion)
{
	int_t	* aggrUnits;

	if (!ifcUnitAssignmentInstance) {
		ifcUnitAssignmentInstance = sdaiCreateInstanceBN(model, "IFCUNITASSIGNMENT");

		aggrUnits = sdaiCreateAggrBN(ifcUnitAssignmentInstance, "Units");
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("LENGTHUNIT", lengthUnitConversion, "METRE"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("AREAUNIT", 0, "SQUARE_METRE"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("VOLUMEUNIT", 0, "CUBIC_METRE"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) getConversionBasedUnitInstance());
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("SOLIDANGLEUNIT", 0, "STERADIAN"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("MASSUNIT", 0, "GRAM"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("TIMEUNIT", 0, "SECOND"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("THERMODYNAMICTEMPERATUREUNIT", 0, "DEGREE_CELSIUS"));
		sdaiAppend(aggrUnits, sdaiINSTANCE, (void*) buildSIUnitInstance("LUMINOUSINTENSITYUNIT", 0, "LUMEN"));
	}

	assert(ifcUnitAssignmentInstance);

	return	ifcUnitAssignmentInstance;
}


//
//
//		RelAggregates, RelContainedInSpatialStructure
//
//


int_t	buildRelAggregatesInstance(char * name, char * description, int_t ifcRelatingObjectInstance, int_t ifcRelatedObjectInstance)
{
	assert(ifcRelatingObjectInstance  &&  ifcRelatedObjectInstance);

	int_t	ifcRelAggregatesInstance, * aggrRelatedObjects;

	ifcRelAggregatesInstance = sdaiCreateInstanceBN(model, "IFCRELAGGREGATES");

	sdaiPutAttrBN(ifcRelAggregatesInstance, "GlobalId", engiGLOBALID, (void*) 0);
	sdaiPutAttrBN(ifcRelAggregatesInstance, "OwnerHistory", sdaiINSTANCE, (void*) getOwnerHistoryInstance());
	sdaiPutAttrBN(ifcRelAggregatesInstance, "Name", sdaiSTRING, name);
	sdaiPutAttrBN(ifcRelAggregatesInstance, "Description", sdaiSTRING, description);
	sdaiPutAttrBN(ifcRelAggregatesInstance, "RelatingObject", sdaiINSTANCE, (void*) ifcRelatingObjectInstance);
	aggrRelatedObjects = sdaiCreateAggrBN(ifcRelAggregatesInstance, "RelatedObjects");
	sdaiAppend(aggrRelatedObjects, sdaiINSTANCE, (void*) ifcRelatedObjectInstance);

	assert(ifcRelAggregatesInstance);

	return	ifcRelAggregatesInstance;
}

int_t	buildRelContainedInSpatialStructureInstance(char * name, char * description, int_t ifcRelatingStructureInstance, int_t ** aggrRelatedElements)
{
	assert(ifcRelatingStructureInstance);

	int_t	ifcRelContainedInSpatialStructureInstance;

	ifcRelContainedInSpatialStructureInstance = sdaiCreateInstanceBN(model, "IFCRELCONTAINEDINSPATIALSTRUCTURE");

	sdaiPutAttrBN(ifcRelContainedInSpatialStructureInstance, "GlobalId", engiGLOBALID, (void*) 0);
	sdaiPutAttrBN(ifcRelContainedInSpatialStructureInstance, "OwnerHistory", sdaiINSTANCE, (void*) getOwnerHistoryInstance());
	sdaiPutAttrBN(ifcRelContainedInSpatialStructureInstance, "Name", sdaiSTRING, "Default Building");
	sdaiPutAttrBN(ifcRelContainedInSpatialStructureInstance, "Description", sdaiSTRING, "Contents of Building Storey");
	(* aggrRelatedElements) = sdaiCreateAggrBN(ifcRelContainedInSpatialStructureInstance, "RelatedElements");
	sdaiPutAttrBN(ifcRelContainedInSpatialStructureInstance, "RelatingStructure", sdaiINSTANCE, (void*) ifcRelatingStructureInstance);

	assert(ifcRelContainedInSpatialStructureInstance);

	return	ifcRelContainedInSpatialStructureInstance;
}


//
//
//		Building, BuildingStorey, Project, Site
//
//


int_t	buildBuildingInstance(MATRIX * matrix, int_t ifcPlacementRelativeTo, int_t * ifcBuildingInstancePlacement)
{
	assert(ifcPlacementRelativeTo);

	int_t	ifcBuildingInstance;

	ifcBuildingInstance = sdaiCreateInstanceBN(model, "IFCBUILDING");

	sdaiPutAttrBN(ifcBuildingInstance, "GlobalId", engiGLOBALID, (void*) 0);
	sdaiPutAttrBN(ifcBuildingInstance, "OwnerHistory", sdaiINSTANCE, (void*) getOwnerHistoryInstance());
	sdaiPutAttrBN(ifcBuildingInstance, "Name", sdaiSTRING, "Default Building");
	sdaiPutAttrBN(ifcBuildingInstance, "Description", sdaiSTRING, "Description of Default Building");

	(*ifcBuildingInstancePlacement) = buildLocalPlacementInstance(matrix, ifcPlacementRelativeTo);
	sdaiPutAttrBN(ifcBuildingInstance, "ObjectPlacement", sdaiINSTANCE, (void*) (*ifcBuildingInstancePlacement));
	sdaiPutAttrBN(ifcBuildingInstance, "CompositionType", sdaiENUM, "ELEMENT");

	sdaiPutAttrBN(ifcBuildingInstance, "BuildingAddress", sdaiINSTANCE, (void*) buildPostalAddress());

	assert(ifcBuildingInstance);

	return	ifcBuildingInstance;
}

int_t	buildBuildingStoreyInstance(MATRIX * matrix, int_t ifcPlacementRelativeTo, int_t * ifcBuildingStoreyInstancePlacement)
{
	assert(ifcPlacementRelativeTo);

	int_t	ifcBuildingStoreyInstance;
	double	elevation = 0;

	ifcBuildingStoreyInstance = sdaiCreateInstanceBN(model, "IFCBUILDINGSTOREY");

	sdaiPutAttrBN(ifcBuildingStoreyInstance, "GlobalId", engiGLOBALID, (void*) 0);
	sdaiPutAttrBN(ifcBuildingStoreyInstance, "OwnerHistory", sdaiINSTANCE, (void*) getOwnerHistoryInstance());
	sdaiPutAttrBN(ifcBuildingStoreyInstance, "Name", sdaiSTRING, "Default Building Storey");
	sdaiPutAttrBN(ifcBuildingStoreyInstance, "Description", sdaiSTRING, "Description of Default Building Storey");

	(*ifcBuildingStoreyInstancePlacement) = buildLocalPlacementInstance(matrix, ifcPlacementRelativeTo);
	sdaiPutAttrBN(ifcBuildingStoreyInstance, "ObjectPlacement", sdaiINSTANCE, (void*) (*ifcBuildingStoreyInstancePlacement));
	sdaiPutAttrBN(ifcBuildingStoreyInstance, "CompositionType", sdaiENUM, "ELEMENT");
	sdaiPutAttrBN(ifcBuildingStoreyInstance, "Elevation", sdaiREAL, &elevation);

	assert(ifcBuildingStoreyInstance);

	return	ifcBuildingStoreyInstance;
}

int_t	getProjectInstance(char * lengthUnitConversion)
{
	int_t	* aggrRepresentationContexts = 0;

	if (!ifcProjectInstance) {
		ifcProjectInstance = sdaiCreateInstanceBN(model, "IFCPROJECT");

		sdaiPutAttrBN(ifcProjectInstance, "GlobalId", engiGLOBALID, (void*) 0);
		sdaiPutAttrBN(ifcProjectInstance, "OwnerHistory", sdaiINSTANCE, (void*) getOwnerHistoryInstance());
		sdaiPutAttrBN(ifcProjectInstance, "Name", sdaiSTRING, "Default Project");
		sdaiPutAttrBN(ifcProjectInstance, "Description", sdaiSTRING, "Description of Default Project");
		sdaiPutAttrBN(ifcProjectInstance, "UnitsInContext", sdaiINSTANCE, (void*) getUnitAssignmentInstance(lengthUnitConversion));
		aggrRepresentationContexts = sdaiCreateAggrBN(ifcProjectInstance, "RepresentationContexts");
		sdaiAppend(aggrRepresentationContexts, sdaiINSTANCE, (void*) getGeometricRepresentationContextInstance());
	}

	assert(ifcProjectInstance);

	return	ifcProjectInstance;
}

int_t	buildSiteInstance(MATRIX * matrix, int_t ifcPlacementRelativeTo, int_t * ifcSiteInstancePlacement)
{
	assert(ifcPlacementRelativeTo == 0);

	int_t	ifcSiteInstance, * aggrRefLatitude, * aggrRefLongitude,
			refLat_x = 24, refLat_y = 28, refLat_z = 0, refLong_x = 54, refLong_y = 25, refLong_z = 0;

	ifcSiteInstance = sdaiCreateInstanceBN(model, "IFCSITE");

	sdaiPutAttrBN(ifcSiteInstance, "GlobalId", engiGLOBALID, (void*) 0);
	sdaiPutAttrBN(ifcSiteInstance, "OwnerHistory", sdaiINSTANCE, (void*) getOwnerHistoryInstance());
	sdaiPutAttrBN(ifcSiteInstance, "Name", sdaiSTRING, "Default Site");
	sdaiPutAttrBN(ifcSiteInstance, "Description", sdaiSTRING, "Description of Default Site");

	(*ifcSiteInstancePlacement) = buildLocalPlacementInstance(matrix, ifcPlacementRelativeTo);
	sdaiPutAttrBN(ifcSiteInstance, "ObjectPlacement", sdaiINSTANCE, (void*) (*ifcSiteInstancePlacement));
	sdaiPutAttrBN(ifcSiteInstance, "CompositionType", sdaiENUM, "ELEMENT");

	aggrRefLatitude = sdaiCreateAggrBN(ifcSiteInstance, "RefLatitude");
	sdaiAppend(aggrRefLatitude, sdaiINTEGER, &refLat_x);
	sdaiAppend(aggrRefLatitude, sdaiINTEGER, &refLat_y);
	sdaiAppend(aggrRefLatitude, sdaiINTEGER, &refLat_z);

	aggrRefLongitude = sdaiCreateAggrBN(ifcSiteInstance, "RefLongitude");
	sdaiAppend(aggrRefLongitude, sdaiINTEGER, &refLong_x);
	sdaiAppend(aggrRefLongitude, sdaiINTEGER, &refLong_y);
	sdaiAppend(aggrRefLongitude, sdaiINTEGER, &refLong_z);

	double	refElevation = 10;
	sdaiPutAttrBN(ifcSiteInstance, "RefElevation", sdaiREAL, &refElevation);

	assert(ifcSiteInstance);

	return	ifcSiteInstance;
}


//
//
//		WorldCoordinateSystem, GeometricRepresentationContext
//
//


int_t	getWorldCoordinateSystemInstance()
{
	POINT3D	point;
	identityPoint(&point);

	int_t	ifcWorldCoordinateSystemInstance;

	ifcWorldCoordinateSystemInstance = sdaiCreateInstanceBN(model, "IFCAXIS2PLACEMENT3D");
	
	sdaiPutAttrBN(ifcWorldCoordinateSystemInstance, "Location", sdaiINSTANCE, (void*) buildCartesianPointInstance(&point));

	assert(ifcWorldCoordinateSystemInstance);

	return	ifcWorldCoordinateSystemInstance;
}

int_t	getGeometricRepresentationContextInstance()
{
	if (!ifcGeometricRepresentationContextInstance) {
		double	precision = 0.00001;
		int_t	coordinateSpaceDimension = 3;

		ifcGeometricRepresentationContextInstance = sdaiCreateInstanceBN(model, "IFCGEOMETRICREPRESENTATIONCONTEXT");

		sdaiPutAttrBN(ifcGeometricRepresentationContextInstance, "ContextType", sdaiSTRING, "Model");
		sdaiPutAttrBN(ifcGeometricRepresentationContextInstance, "CoordinateSpaceDimension", sdaiINTEGER, &coordinateSpaceDimension);
		sdaiPutAttrBN(ifcGeometricRepresentationContextInstance, "Precision", sdaiREAL, &precision);
		sdaiPutAttrBN(ifcGeometricRepresentationContextInstance, "WorldCoordinateSystem", sdaiINSTANCE, (void*) getWorldCoordinateSystemInstance());

		POINT3D	point;
		point.ifcCartesianPointInstance = 0;
		point.x = 0;
		point.y = 1;
		point.z = 0;

		sdaiPutAttrBN(ifcGeometricRepresentationContextInstance, "TrueNorth", sdaiINSTANCE, (void*) buildDirectionInstance(&point));
	}

	assert(ifcGeometricRepresentationContextInstance);

	return	ifcGeometricRepresentationContextInstance;
}


//
//
//		PostalAddress
//
//


int_t	buildPostalAddress()
{
	if (!ifcPostalAddressInstance) {
		int_t	* addressLines;

		ifcPostalAddressInstance = sdaiCreateInstanceBN(model, "IFCPOSTALADDRESS");

		addressLines = sdaiCreateAggrBN(ifcPostalAddressInstance, "AddressLines");
		sdaiAppend(addressLines, sdaiSTRING, "RDF Ltd.");
		sdaiAppend(addressLines, sdaiSTRING, "Main Office");

		sdaiPutAttrBN(ifcPostalAddressInstance, "PostalBox", sdaiSTRING, "32");
		sdaiPutAttrBN(ifcPostalAddressInstance, "Town", sdaiSTRING, "Bankya");
		sdaiPutAttrBN(ifcPostalAddressInstance, "Region", sdaiSTRING, "Sofia");
		sdaiPutAttrBN(ifcPostalAddressInstance, "PostalCode", sdaiSTRING, "1320");
		sdaiPutAttrBN(ifcPostalAddressInstance, "Country", sdaiSTRING, "Bulgaria");
	}

	assert(ifcPostalAddressInstance);

	return	ifcPostalAddressInstance;
}
