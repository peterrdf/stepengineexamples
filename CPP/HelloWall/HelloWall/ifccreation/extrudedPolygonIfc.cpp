////////////////////////////////////////////////////////////////////////
//	Author:		Peter Bonsma
//	Date:		20 February 2015
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
#include	"extrudedPolygonIfc.h"

#include	<stdio.h>
#include	<stdlib.h>
#include	<string.h>
#include	<assert.h>


extern	int_t	model,
				* aggrRelatedElements,
				* aggrRepresentations;
	

void	createIfcExtrudedPolygonShape(POLYGON2D * polygon, double depth)
{
	assert(aggrRepresentations);

	sdaiAppend(aggrRepresentations, sdaiINSTANCE, (void*) buildShapeRepresentationInstance(BODY_SWEPTSOLID_REPRESENTATION, polygon, depth));
}

void	createIfcPolylineShape(double p0x, double p0y, double p1x, double p1y)
{
	assert(aggrRepresentations);

	sdaiAppend(aggrRepresentations, sdaiINSTANCE, (void*) buildShapeRepresentationInstance(AXIS_CURVE2D_REPRESENTATION, p0x, p0y, p1x, p1y));
}


//
//
//		ShapeRepresentation
//
//


int_t	buildShapeRepresentationInstance(int_t type, POLYGON2D * polygon, double depth)
{
	int_t	ifcShapeRepresentationInstance, * aggrItems;

	ifcShapeRepresentationInstance = sdaiCreateInstanceBN(model,(char*)"IFCSHAPEREPRESENTATION");

	aggrItems = sdaiCreateAggrBN(ifcShapeRepresentationInstance,(char*)"Items");
	switch	(type) {
		case  BODY_SWEPTSOLID_REPRESENTATION:
			sdaiAppend(aggrItems, sdaiINSTANCE, (void*) buildExtrudedAreaSolidInstance(polygon, depth));

			sdaiPutAttrBN(ifcShapeRepresentationInstance,(char*)"RepresentationIdentifier", sdaiSTRING,(char*)"Body");
			sdaiPutAttrBN(ifcShapeRepresentationInstance,(char*)"RepresentationType", sdaiSTRING,(char*)"SweptSolid");
			sdaiPutAttrBN(ifcShapeRepresentationInstance,(char*)"ContextOfItems", sdaiINSTANCE, (void*) getGeometricRepresentationContextInstance());
			break;
		default:
			assert(false);
			break;
	}

	assert(ifcShapeRepresentationInstance);

	return	ifcShapeRepresentationInstance;
}

int_t	buildShapeRepresentationInstance(int_t type, double p0x, double p0y, double p1x, double p1y)
{
	int_t	ifcShapeRepresentationInstance, * aggrItems;

	ifcShapeRepresentationInstance = sdaiCreateInstanceBN(model,(char*)"IFCSHAPEREPRESENTATION");

	aggrItems = sdaiCreateAggrBN(ifcShapeRepresentationInstance,(char*)"Items");
	switch  (type) {
		case  AXIS_CURVE2D_REPRESENTATION:
			sdaiAppend(aggrItems, sdaiINSTANCE, (void*) buildPolylineInstance(p0x, p0y, p1x, p1y));

			sdaiPutAttrBN(ifcShapeRepresentationInstance,(char*)"RepresentationIdentifier", sdaiSTRING,(char*)"Axis");
			sdaiPutAttrBN(ifcShapeRepresentationInstance,(char*)"RepresentationType", sdaiSTRING,(char*)"Curve2D");
			sdaiPutAttrBN(ifcShapeRepresentationInstance,(char*)"ContextOfItems", sdaiINSTANCE, (void*) getGeometricRepresentationContextInstance());
			break;
		default:
			assert(false);
			break;
	}

	assert(ifcShapeRepresentationInstance);

	return	ifcShapeRepresentationInstance;
}


//
//
//		ArbitraryClosedProfileDef, CartesianPoint(2D), ExtrudedAreaSolid, Polyline
//
//


int_t	buildArbitraryClosedProfileDefInstance(POLYGON2D * polygon)
{
	int_t	ifcArbitraryClosedProfileDefInstance;

	ifcArbitraryClosedProfileDefInstance = sdaiCreateInstanceBN(model,(char*)"IFCARBITRARYCLOSEDPROFILEDEF");

	sdaiPutAttrBN(ifcArbitraryClosedProfileDefInstance,(char*)"ProfileType", sdaiENUM,(char*)"AREA");
	sdaiPutAttrBN(ifcArbitraryClosedProfileDefInstance,(char*)"OuterCurve", sdaiINSTANCE, (void*) buildPolylineInstance(polygon));

	assert(ifcArbitraryClosedProfileDefInstance);

	return	ifcArbitraryClosedProfileDefInstance;
}

int_t	buildCartesianPointInstance(double x, double y)
{
	int_t	ifcCartesianPointInstance, * aggrCoordinates;

	ifcCartesianPointInstance = sdaiCreateInstanceBN(model,(char*)"IFCCARTESIANPOINT");

	aggrCoordinates = sdaiCreateAggrBN(ifcCartesianPointInstance,(char*)"Coordinates");
	sdaiAppend(aggrCoordinates, sdaiREAL, &x);
	sdaiAppend(aggrCoordinates, sdaiREAL, &y);

	assert(ifcCartesianPointInstance);

	return	ifcCartesianPointInstance;
}

int_t	buildExtrudedAreaSolidInstance(POLYGON2D * polygon, double depth)
{
	MATRIX	matrix;
	int_t	ifcExtrudedAreaSolidInstance;

	identityMatrix(&matrix);

	ifcExtrudedAreaSolidInstance = sdaiCreateInstanceBN(model,(char*)"IFCEXTRUDEDAREASOLID");

	sdaiPutAttrBN(ifcExtrudedAreaSolidInstance,(char*)"SweptArea", sdaiINSTANCE, (void*) buildArbitraryClosedProfileDefInstance(polygon));
	sdaiPutAttrBN(ifcExtrudedAreaSolidInstance,(char*)"Position", sdaiINSTANCE, (void*) buildAxis2Placement3DInstance(&matrix));
	sdaiPutAttrBN(ifcExtrudedAreaSolidInstance,(char*)"ExtrudedDirection", sdaiINSTANCE, (void*) buildDirectionInstance((POINT3D*) &matrix._31));
	sdaiPutAttrBN(ifcExtrudedAreaSolidInstance,(char*)"Depth", sdaiREAL, (void*) &depth);

	assert(ifcExtrudedAreaSolidInstance);

	return	ifcExtrudedAreaSolidInstance;
}

int_t	buildPolylineInstance(POLYGON2D * polygon)
{
	int_t	ifcPolylineInstance, * aggrPoints;

	ifcPolylineInstance = sdaiCreateInstanceBN(model,(char*)"IFCPOLYLINE");

	aggrPoints = sdaiCreateAggrBN(ifcPolylineInstance,(char*)"Points");
	double	x = polygon->point->x,
			y = polygon->point->y;
	while  (polygon) {
		sdaiAppend(aggrPoints, sdaiINSTANCE, (void*) buildCartesianPointInstance(polygon->point->x, polygon->point->y));
		polygon = polygon->next;
	}
	sdaiAppend(aggrPoints, sdaiINSTANCE, (void*) buildCartesianPointInstance(x, y));

	assert(ifcPolylineInstance);

	return	ifcPolylineInstance;
}

int_t	buildPolylineInstance(double p0x, double p0y, double p1x, double p1y)
{
	int_t	ifcPolylineInstance, * aggrPoints;

	ifcPolylineInstance = sdaiCreateInstanceBN(model,(char*)"IFCPOLYLINE");

	aggrPoints = sdaiCreateAggrBN(ifcPolylineInstance,(char*)"Points");
	sdaiAppend(aggrPoints, sdaiINSTANCE, (void*) buildCartesianPointInstance(p0x, p0y));
	sdaiAppend(aggrPoints, sdaiINSTANCE, (void*) buildCartesianPointInstance(p1x, p1y));

	assert(ifcPolylineInstance);

	return	ifcPolylineInstance;
}
