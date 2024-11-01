////////////////////////////////////////////////////////////////////////
//  Author:  Peter Bonsma
//  Date:    22 September 2010
//  Project: IFC Engine Series (example using DLL)
//
//  This code may be used and edited freely,
//  also for commercial projects in open and closed source software
//
//  In case of use of the DLL:
//  be aware of license fee for use of this DLL when used commercially
//  more info for commercial use:	peter.bonsma@tno.nl
//
//  more info for using the IFC Engine DLL in other languages
//	and creation of specific code examples:
//									pim.vandenhelm@tno.nl
//								    peter.bonsma@rdf.bg
////////////////////////////////////////////////////////////////////////


#include "stdafx.h"
#include "boundingBoxIfc.h"

extern  int     model;

extern  int     * aggrRelatedElements,
                * aggrRepresentations;

extern  int     ifcBuildingStoreyInstancePlacement;


void    createIfcBoundingBoxShape(double width, double thickness, double height, char * representationIdentifier)
{
	ASSERT(aggrRepresentations);

    sdaiAppend(aggrRepresentations, sdaiINSTANCE, (void*) buildShapeRepresentationInstance(width, thickness, height, representationIdentifier));
}


//
//
//		BoundingBox, ShapeRepresentation
//
//


int		buildBoundingBoxInstance(double width, double thickness, double height)
{
	int		ifcBoundingBoxInstance;

	ifcBoundingBoxInstance = sdaiCreateInstanceBN(model, "IFCBOUNDINGBOX");

	sdaiPutAttrBN(ifcBoundingBoxInstance, "Corner", sdaiINSTANCE, (void*) buildCartesianPointInstance(0, 0, 0));
	sdaiPutAttrBN(ifcBoundingBoxInstance, "XDim", sdaiREAL, &width);
	sdaiPutAttrBN(ifcBoundingBoxInstance, "YDim", sdaiREAL, &thickness);
	sdaiPutAttrBN(ifcBoundingBoxInstance, "ZDim", sdaiREAL, &height);

	ASSERT(ifcBoundingBoxInstance);

	return	ifcBoundingBoxInstance;
}

int		buildShapeRepresentationInstance(double width, double thickness, double height, char * representationIdentifier)
{
	int		ifcShapeRepresentationInstance, * aggrItems;

	ifcShapeRepresentationInstance = sdaiCreateInstanceBN(model, "IFCSHAPEREPRESENTATION");

	sdaiPutAttrBN(ifcShapeRepresentationInstance, "ContextOfItems", sdaiINSTANCE, (void*) getGeometricRepresentationContextInstance());
	aggrItems = sdaiCreateAggrBN(ifcShapeRepresentationInstance, "Items");
	sdaiPutAttrBN(ifcShapeRepresentationInstance, "RepresentationIdentifier", sdaiSTRING, representationIdentifier);
	sdaiPutAttrBN(ifcShapeRepresentationInstance, "RepresentationType", sdaiSTRING, "BoundingBox");
    sdaiAppend(aggrItems, sdaiINSTANCE, (void*) buildBoundingBoxInstance(width, thickness, height));

	ASSERT(ifcShapeRepresentationInstance);

	return	ifcShapeRepresentationInstance;
}


//
//
//      CartesianPoint(3D)
//
//



int		buildCartesianPointInstance(double x, double y, double z)
{
	int		ifcCartesianPointInstance, * aggrCoordinates;

	ifcCartesianPointInstance = sdaiCreateInstanceBN(model, "IFCCARTESIANPOINT");

	aggrCoordinates = sdaiCreateAggrBN(ifcCartesianPointInstance, "Coordinates");
	sdaiAppend(aggrCoordinates, sdaiREAL, &x);
	sdaiAppend(aggrCoordinates, sdaiREAL, &y);
	sdaiAppend(aggrCoordinates, sdaiREAL, &z);

	ASSERT(ifcCartesianPointInstance);

	return	ifcCartesianPointInstance;
}