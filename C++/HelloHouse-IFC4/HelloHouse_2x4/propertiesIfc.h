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


#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


#include "IFCEngine.h"


//
//
//		PropertySet, PropertySingleValue
//
//


int		buildPropertySet(char * name, int ** aggrHasProperties);
int		buildPropertySingleValue(char * name, char * description, bool nominalValue);
int		buildPropertySingleValue(char * name, char * description, bool nominalValue, char * typePath);
int		buildPropertySingleValue(char * name, char * description, double nominalValue);
int		buildPropertySingleValue(char * name, char * description, double nominalValue, char * typePath);
int		buildPropertySingleValue(char * name, char * description, char * nominalValue);
int		buildPropertySingleValue(char * name, char * description, char * nominalValue, char * typePath);


//
//
//		ElementQuantity, QuantityLength, QuantityArea, QuantityVolume
//
//


int		buildElementQuantity(char * name, int ** aggrQuantities);
int		buildQuantityLength(char * name, char * description, double length);
int		buildQuantityArea(char * name, char * description, double area);
int		buildQuantityVolume(char * name, char * description, double volume);


//
//
//		Pset_WallCommon, BaseQuantities_Wall, BaseQuantities_WallStandardCase, BaseQuantities_Opening, Pset_WindowCommon, BaseQuantities_Window
//
//


int		buildPset_WallCommon();
int		buildBaseQuantities_Wall(double width, double length, double height, double openingArea, double linearConversionFactor);
int		buildBaseQuantities_WallStandardCase(double width, double length, double height, double openingArea, double linearConversionFactor);
int		buildBaseQuantities_Opening(double depth, double height, double width);
int		buildPset_DoorCommon();
int		buildPset_WindowCommon();
int		buildBaseQuantities_Window(double height, double width);


//
//
//      RelDefinesByProperties
//
//


int		buildRelDefinesByProperties(int relatedObject, int relatingPropertyDefinition);
