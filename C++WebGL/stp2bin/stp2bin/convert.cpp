// stp2bin.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "convert.h"

#include "stepengine/include/engine.h"
#include "stepengine/include/stepengine.h"

#include <assert.h>


#ifdef _DEBUG
#define new DEBUG_NEW
#endif




struct MATRIX
{
	double	_11, _12, _13,
			_21, _22, _23,
			_31, _32, _33,
			_41, _42, _43;
};

void	MatrixIdentity(
				MATRIX						* pOut
			)
{
	pOut->_12 = pOut->_13 = 
	pOut->_21 = pOut->_23 = 
	pOut->_31 = pOut->_32 = 
	pOut->_41 = pOut->_42 = pOut->_43 = 0.;

	pOut->_11 = pOut->_22 = pOut->_33 = 1.;
}

void	MatrixMultiply(
				MATRIX			* pOut,
				const MATRIX	* pM1,
				const MATRIX	* pM2
			)
{
	assert(pOut && pM1 && pM2);

	MATRIX pTmp;

	if (pOut) {
		pTmp._11 = pM1->_11 * pM2->_11 + pM1->_12 * pM2->_21 + pM1->_13 * pM2->_31;
		pTmp._12 = pM1->_11 * pM2->_12 + pM1->_12 * pM2->_22 + pM1->_13 * pM2->_32;
		pTmp._13 = pM1->_11 * pM2->_13 + pM1->_12 * pM2->_23 + pM1->_13 * pM2->_33;

		pTmp._21 = pM1->_21 * pM2->_11 + pM1->_22 * pM2->_21 + pM1->_23 * pM2->_31;
		pTmp._22 = pM1->_21 * pM2->_12 + pM1->_22 * pM2->_22 + pM1->_23 * pM2->_32;
		pTmp._23 = pM1->_21 * pM2->_13 + pM1->_22 * pM2->_23 + pM1->_23 * pM2->_33;

		pTmp._31 = pM1->_31 * pM2->_11 + pM1->_32 * pM2->_21 + pM1->_33 * pM2->_31;
		pTmp._32 = pM1->_31 * pM2->_12 + pM1->_32 * pM2->_22 + pM1->_33 * pM2->_32;
		pTmp._33 = pM1->_31 * pM2->_13 + pM1->_32 * pM2->_23 + pM1->_33 * pM2->_33;

		pTmp._41 = pM1->_41 * pM2->_11 + pM1->_42 * pM2->_21 + pM1->_43 * pM2->_31 + pM2->_41;
		pTmp._42 = pM1->_41 * pM2->_12 + pM1->_42 * pM2->_22 + pM1->_43 * pM2->_32 + pM2->_42;
		pTmp._43 = pM1->_41 * pM2->_13 + pM1->_42 * pM2->_23 + pM1->_43 * pM2->_33 + pM2->_43;

		pOut->_11 = pTmp._11;
		pOut->_12 = pTmp._12;
		pOut->_13 = pTmp._13;

        pOut->_21 = pTmp._21;
		pOut->_22 = pTmp._22;
		pOut->_23 = pTmp._23;

        pOut->_31 = pTmp._31;
		pOut->_32 = pTmp._32;
		pOut->_33 = pTmp._33;

        pOut->_41 = pTmp._41;
		pOut->_42 = pTmp._42;
		pOut->_43 = pTmp._43;
	}
	else {
		assert(false);
	}
}





/*
struct MyStructProductDefinition;

struct MyStructRelation
{
	MyStructProductDefinition	* productDefinition;

	int64_t						geometryInstance;

	MyStructRelation			* next;
};

struct MyStructProductDefinition
{
	int_t						stepInstance;

	char						* id;
	char						* name;
	char						* description;

	MyStructRelation			* parents;
	MyStructRelation			* children;

	int64_t						geometryInstance;

	MyStructProductDefinition	* next;
};

MyStructProductDefinition	* firstProductDefinition = nullptr;

void	REMOVE_GetProductDefinitionInstances(
				int_t	stepModel
			)
{
	int_t	* productDefinitionInstances = sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION"),
			noProductDefinitionInstances = sdaiGetMemberCount(productDefinitionInstances);
	if (noProductDefinitionInstances) {
		for (int_t i = 0; i < noProductDefinitionInstances; i++) {
			int_t	productDefinitionInstance = 0;
			engiGetAggrElement(productDefinitionInstances, i, sdaiINSTANCE, &productDefinitionInstance);

			MyStructProductDefinition	* myProductDefinition = new MyStructProductDefinition;

			myProductDefinition->stepInstance = productDefinitionInstance;
			myProductDefinition->id = nullptr;
			myProductDefinition->name = nullptr;
			myProductDefinition->description = nullptr;
			myProductDefinition->parents = nullptr;
			myProductDefinition->children = nullptr;
			myProductDefinition->geometryInstance = 0;
			owlBuildInstance(stepModel, productDefinitionInstance, &myProductDefinition->geometryInstance);
			myProductDefinition->next = firstProductDefinition;

			firstProductDefinition = myProductDefinition;

			int_t	productDefinitionFormationInstance = 0;
			sdaiGetAttrBN(productDefinitionInstance, "formation", sdaiINSTANCE, &productDefinitionFormationInstance);

			if (productDefinitionFormationInstance) {
				int_t	productInstance = 0;
				sdaiGetAttrBN(productDefinitionFormationInstance, "of_product", sdaiINSTANCE, &productInstance);
				if (productInstance) {
					sdaiGetAttrBN(productInstance, "id", sdaiSTRING, &myProductDefinition->id);
					sdaiGetAttrBN(productInstance, "name", sdaiSTRING, &myProductDefinition->name);
					sdaiGetAttrBN(productInstance, "description", sdaiSTRING, &myProductDefinition->description);
				}
				else {
					assert(false);
				}
			}
			else {
				assert(false);
			}

//			char    * globalId = 0, *name = 0, *description = 0;
//			sdaiGetAttrBN(ifcColumnInstance, "GlobalId", sdaiSTRING, &globalId);
//			sdaiGetAttrBN(ifcColumnInstance, "Name", sdaiSTRING, &name);
//			sdaiGetAttrBN(ifcColumnInstance, "Description", sdaiSTRING, &description);
		}
	}
}

MyStructProductDefinition	* REMOVE_FindProductDefinition(
									int_t		productDefinitionInstance
								)
{
	MyStructProductDefinition	* productDefinition = firstProductDefinition;

	while (productDefinition) {
		if (productDefinition->stepInstance == productDefinitionInstance) {
			return	productDefinition;
		}
		productDefinition = productDefinition->next;
	}

	return	nullptr;
}

void	REMOVE_GetProductDefinitionShapeInstances(
				int_t	stepModel
			)
{
	int_t	nextAssemblyUsageOccurrenceEntity = sdaiGetEntity(stepModel, "NEXT_ASSEMBLY_USAGE_OCCURRENCE");

	int_t	* productDefinitionShapeInstances = sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
			noProductDefinitionShapeInstances = sdaiGetMemberCount(productDefinitionShapeInstances);
	if (noProductDefinitionShapeInstances) {
		for (int_t i = 0; i < noProductDefinitionShapeInstances; i++) {
			int_t	productDefinitionShapeInstance = 0;
			engiGetAggrElement(productDefinitionShapeInstances, i, sdaiINSTANCE, &productDefinitionShapeInstance);

			int64_t	geometryInstance = 0;
			owlBuildInstance(stepModel, productDefinitionShapeInstance, &geometryInstance);

			int_t	definitionInstance = 0;
			sdaiGetAttrBN(productDefinitionShapeInstance, "definition", sdaiINSTANCE, &definitionInstance);
			if (sdaiGetInstanceType(definitionInstance) == nextAssemblyUsageOccurrenceEntity) {
				int_t	relatingProductDefinitionInstance = 0;
				sdaiGetAttrBN(definitionInstance, "relating_product_definition", sdaiINSTANCE, &relatingProductDefinitionInstance);
				MyStructProductDefinition	* relatingProductDefinition = FindProductDefinition(relatingProductDefinitionInstance);

				int_t	relatedProductDefinitionInstance = 0;
				sdaiGetAttrBN(definitionInstance, "related_product_definition", sdaiINSTANCE, &relatedProductDefinitionInstance);
				MyStructProductDefinition	* relatedProductDefinition = FindProductDefinition(relatedProductDefinitionInstance);

				if (relatingProductDefinition && relatedProductDefinition) {
					//
					//	parent => child
					//
					MyStructRelation	* myChildRelation = new MyStructRelation;
					myChildRelation->productDefinition = relatedProductDefinition;
					myChildRelation->geometryInstance = geometryInstance;
					myChildRelation->next = relatingProductDefinition->children;
					relatingProductDefinition->children = myChildRelation;

					//
					//	child => parent
					//
					MyStructRelation	* myParentRelation = new MyStructRelation;
					myParentRelation->productDefinition = relatingProductDefinition;
					myParentRelation->geometryInstance = geometryInstance;
					myParentRelation->next = relatedProductDefinition->parents;
					relatedProductDefinition->parents = myParentRelation;
				}
				else {
					assert(false);
				}
			}
		}
	}
}

int64_t	REMOVE_BuildParts(
				int64_t		rdfModel
			)
{
	MyStructProductDefinition	* productDefinition = firstProductDefinition;

	int64_t	partCnt = 0;
	while (productDefinition) {
//		if (productDefinition->children == 0 && productDefinition->geometryInstance) {
		if (productDefinition->geometryInstance) {
			partCnt++;
		}
		productDefinition = productDefinition->next;
	}

	if (partCnt) {
		productDefinition = firstProductDefinition;

		int64_t	propertyId = GetPropertyByName(rdfModel, "id"),
				propertyName = GetPropertyByName(rdfModel, "name"),
				propertyDescription = GetPropertyByName(rdfModel, "description");

		SetPropertyType(propertyId, 3);
		SetPropertyType(propertyName, 3);
		SetPropertyType(propertyDescription, 3);

		int64_t	* parts = new int64_t[partCnt], i = 0;
		while (productDefinition) {
//			if (productDefinition->children == 0 && productDefinition->geometryInstance) {
			if (productDefinition->geometryInstance) {
				parts[i] = productDefinition->geometryInstance;

				SetDatatypeProperty(productDefinition->geometryInstance, propertyId, &productDefinition->id, 1);
				SetDatatypeProperty(productDefinition->geometryInstance, propertyName, &productDefinition->name, 1);
				SetDatatypeProperty(productDefinition->geometryInstance, propertyDescription, &productDefinition->description, 1);

				i++;
			}
			productDefinition = productDefinition->next;
		}

		int64_t	classCollection = GetClassByName(rdfModel, "Collection"),
				propertyObjects = GetPropertyByName(rdfModel, "objects");

		int64_t	instanceCollection = CreateInstance(classCollection, nullptr);

		SetObjectProperty(instanceCollection, propertyObjects, parts, partCnt);

		delete[] parts;

		return	instanceCollection;
	}

	return	0;
}

int64_t	REMOVE_BuildAssemblyGetElementCnt(
				MyStructProductDefinition	* productDefinition
			)
{
	int64_t	elementCnt = 0;

	if (productDefinition->children) {
		MyStructRelation	* children = productDefinition->children;
		while (children) {
			elementCnt += BuildAssemblyGetElementCnt(children->productDefinition);
			children = children->next;
		}
	}
	else {
		return	1;
	}

	return	elementCnt;
}

int64_t	REMOVE_BuildAssemblyGetElement(
				int64_t						rdfModel,
				MyStructProductDefinition	* productDefinition,
				int64_t						index
			)
{
	if (productDefinition->children) {
		MyStructRelation	* children = productDefinition->children;
		while (children) {
			int64_t	localCnt = BuildAssemblyGetElementCnt(children->productDefinition);
			if (index < localCnt) {
				int64_t	owlInstance = BuildAssemblyGetElement(rdfModel, children->productDefinition, index);

				int64_t	classTransformation = GetClassByName(rdfModel, "Transformation"),
						propertyMatrix = GetPropertyByName(rdfModel, "matrix"),
						propertyObject = GetPropertyByName(rdfModel, "object");

				int64_t	instanceTransformation = CreateInstance(classTransformation, nullptr);

				SetObjectProperty(instanceTransformation, propertyObject, &owlInstance, 1);

				if (children->geometryInstance) {
#ifdef _DEBUG
					int64_t	classMatrix = GetClassByName(rdfModel, "Matrix"),
							classMatrixMultiplication = GetClassByName(rdfModel, "MatrixMultiplication"),
							classInverseMatrix = GetClassByName(rdfModel, "InverseMatrix");

					int64_t	geometryInstanceClass = GetInstanceClass(children->geometryInstance);
					assert(geometryInstanceClass == classMatrix || geometryInstanceClass == classMatrixMultiplication || geometryInstanceClass == classInverseMatrix);
#endif // _DEBUG
					SetObjectProperty(instanceTransformation, propertyMatrix, &children->geometryInstance, 1);
				}

				return	instanceTransformation;
			}
			else {
				index -= localCnt;
			}
			children = children->next;
		}
	}
	else {
		assert(index == 0);
		return	productDefinition->geometryInstance;
	}
	assert(false);
	return	0;
}

int64_t	REMOVE_BuildAssembly(
				int64_t						rdfModel,
				MyStructProductDefinition	* productDefinition
			)
{
	int64_t	elementCnt = REMOVE_BuildAssemblyGetElementCnt(productDefinition);

	int64_t	* elements,
			instanceCollection;

	if (elementCnt) {
		elements = new int64_t[elementCnt];

		int u = 0;
		int64_t	classCollection = GetClassByName(rdfModel, "Collection"),
				propertyObjects = GetPropertyByName(rdfModel, "objects");

		instanceCollection = CreateInstance(classCollection, nullptr);

		for (int k = 0; k < elementCnt; k++) {
			elements[k] = BuildAssemblyGetElement(rdfModel, productDefinition, k);
		}

		SetObjectProperty(instanceCollection, propertyObjects, elements, elementCnt);

		delete[] elements;
	}
	else {
		assert(false);
	}

	return	instanceCollection;
}

int64_t	REMOVE_BuildAssemblies(
				int64_t		rdfModel
			)
{
	MyStructProductDefinition	* productDefinition = firstProductDefinition;

	int64_t	assemblyCnt = 0;
	while (productDefinition) {
		if (productDefinition->parents == 0 && productDefinition->children) {
			assemblyCnt++;
		}
		productDefinition = productDefinition->next;
	}

	if (assemblyCnt) {
		productDefinition = firstProductDefinition;

		int64_t	propertyId = GetPropertyByName(rdfModel, "id"),
				propertyName = GetPropertyByName(rdfModel, "name"),
				propertyDescription = GetPropertyByName(rdfModel, "description");

		SetPropertyType(propertyId, 3);
		SetPropertyType(propertyName, 3);
		SetPropertyType(propertyDescription, 3);

		int64_t	* assemblies = new int64_t[assemblyCnt], i = 0;
		while (productDefinition) {
			if (productDefinition->parents == 0 && productDefinition->children) {
				assemblies[i] = BuildAssembly(rdfModel, productDefinition);

				SetDatatypeProperty(assemblies[i], propertyId, &productDefinition->id, 1);
				SetDatatypeProperty(assemblies[i], propertyName, &productDefinition->name, 1);
				SetDatatypeProperty(assemblies[i], propertyDescription, &productDefinition->description, 1);

				i++;
			}
			productDefinition = productDefinition->next;
		}

		int64_t	classCollection = GetClassByName(rdfModel, "Collection"),
				propertyObjects = GetPropertyByName(rdfModel, "objects");

		int64_t	instanceCollection = CreateInstance(classCollection, nullptr);

		SetObjectProperty(instanceCollection, propertyObjects, assemblies, assemblyCnt);

		delete[] assemblies;

		return	instanceCollection;
	}

	return	0;
}
*/










struct PRODUCT_DEFINITION
{
	int					expressID;
	int_t				instanceHandle;

	char				* attr_id;
	char				* attr_name;
	char				* attr_description;

	char				* attr_product_id;
	char				* attr_product_name;

	int_t				relating_product_refs;
	int_t				related_product_refs;

	PRODUCT_DEFINITION	* next;
};

struct ASSEMBLY
{
	int					expressID;

	char				* attr_id;
	char				* attr_name;
	char				* attr_description;

	PRODUCT_DEFINITION	* relating_product_definition;
	PRODUCT_DEFINITION	* related_product_definition;

	ASSEMBLY			* firstChild;
	ASSEMBLY			* sibling;

	ASSEMBLY			* next;
};

PRODUCT_DEFINITION	* firstProductDefinition = nullptr;

ASSEMBLY			* firstAssembly = nullptr;

PRODUCT_DEFINITION	* GetProductDefinition(int_t product_definition_instance, bool relating_product, bool related_product)
{
	int expressID = (int) internalGetP21Line(product_definition_instance);

	PRODUCT_DEFINITION	* myProductDefinition = firstProductDefinition;
	while (myProductDefinition) {
		if (myProductDefinition->expressID == expressID) {
			if (relating_product) { myProductDefinition->relating_product_refs++; }
			if (related_product) { myProductDefinition->related_product_refs++; }

			return	myProductDefinition;
		}
		myProductDefinition = myProductDefinition->next;
	}

	myProductDefinition = new PRODUCT_DEFINITION;

	myProductDefinition->expressID = expressID;
	myProductDefinition->instanceHandle = product_definition_instance;

	myProductDefinition->attr_id = nullptr;
	sdaiGetAttrBN(product_definition_instance, "id", sdaiSTRING, &myProductDefinition->attr_id);
	myProductDefinition->attr_name = nullptr;
	sdaiGetAttrBN(product_definition_instance, "name", sdaiSTRING, &myProductDefinition->attr_name);
	myProductDefinition->attr_description = nullptr;
	sdaiGetAttrBN(product_definition_instance, "description", sdaiSTRING, &myProductDefinition->attr_description);

    int_t product_definition_formation_instance = 0;
    sdaiGetAttrBN(product_definition_instance, "formation", sdaiINSTANCE, &product_definition_formation_instance);
 
    int_t product_instance = 0;
    sdaiGetAttrBN(product_definition_formation_instance, "of_product", sdaiINSTANCE, &product_instance);
 
    sdaiGetAttrBN(product_instance, "id", sdaiSTRING, &myProductDefinition->attr_product_id);
    sdaiGetAttrBN(product_instance, "name", sdaiSTRING, &myProductDefinition->attr_product_name);

	myProductDefinition->relating_product_refs = relating_product ? 1 : 0;
	myProductDefinition->related_product_refs = related_product ? 1 : 0;

	myProductDefinition->next = firstProductDefinition;
	firstProductDefinition = myProductDefinition;

	return	myProductDefinition;
}

void	FindProperties(FILE * fp, int_t stepModel, PRODUCT_DEFINITION * myProductDefinition)
{
	//
	//	Find properties
	//
	int_t * propertyDefinitionInstances = sdaiGetEntityExtentBN(stepModel, "PROPERTY_DEFINITION"),
			noPropertyDefinitionInstances = sdaiGetMemberCount(propertyDefinitionInstances);
	for (int_t i = 0; i < noPropertyDefinitionInstances; i++) {
		int_t propertyDefinitionInstance = 0;
		engiGetAggrElement(propertyDefinitionInstances, i, sdaiINSTANCE, &propertyDefinitionInstance);

		int_t definitionInstance = 0;
		sdaiGetAttrBN(propertyDefinitionInstance, "definition", sdaiINSTANCE, &definitionInstance);
		if (definitionInstance == myProductDefinition->instanceHandle) {
			fprintf(fp, "#   property (#%i = PROPERTY_DEFINITION( ... ))\n", (int) internalGetP21Line(propertyDefinitionInstance));

			char	* name = nullptr;
			sdaiGetAttrBN(propertyDefinitionInstance, "name", sdaiSTRING, &name);
			fprintf(fp, "#     name = %s\n", name);

			char	* description = nullptr;
			sdaiGetAttrBN(propertyDefinitionInstance, "description", sdaiSTRING, &description);
			fprintf(fp, "#     description = %s\n", description);

			//
			//	Lookup value (not using inverse relations)
			//
			int_t * propertyDefinitionRepresentationInstances = sdaiGetEntityExtentBN(stepModel, "PROPERTY_DEFINITION_REPRESENTATION"),
					noPropertyDefinitionRepresentationInstances = sdaiGetMemberCount(propertyDefinitionRepresentationInstances);
			for (int_t j = 0; j < noPropertyDefinitionRepresentationInstances; j++) {
				int_t propertyDefinitionRepresentationInstance = 0;
				engiGetAggrElement(propertyDefinitionRepresentationInstances, j, sdaiINSTANCE, &propertyDefinitionRepresentationInstance);
					
				int_t PDR_definitionInstance = 0;
				sdaiGetAttrBN(propertyDefinitionRepresentationInstance, "definition", sdaiINSTANCE, &PDR_definitionInstance);
				if (PDR_definitionInstance == propertyDefinitionInstance) {
					int_t representationInstance = 0;
					sdaiGetAttrBN(propertyDefinitionRepresentationInstance, "used_representation", sdaiINSTANCE, &representationInstance);

					int_t	* aggrItems = nullptr;
					sdaiGetAttrBN(representationInstance, "items", sdaiAGGR, &aggrItems);
					int_t	noAggrItems = sdaiGetMemberCount(aggrItems);
					for (int_t k = 0; k < noAggrItems; k++) {
						int_t representationItemInstance = 0;
						engiGetAggrElement(aggrItems, k, sdaiINSTANCE, &representationItemInstance);

						if (sdaiGetInstanceType(representationItemInstance) == sdaiGetEntity(stepModel, "DESCRIPTIVE_REPRESENTATION_ITEM")) {
							char	* valueDescription = nullptr;
							sdaiGetAttrBN(representationItemInstance, "description", sdaiSTRING, &valueDescription);
							fprintf(fp, "#     value = %s\n", valueDescription);
						}
						else if (sdaiGetInstanceType(representationItemInstance) == sdaiGetEntity(stepModel, "VALUE_REPRESENTATION_ITEM")) {
							int_t	* valueComponentADB = nullptr;
							sdaiGetAttrBN(representationItemInstance, "value_component", sdaiADB, &valueComponentADB);

							const char	* typePath = sdaiGetADBTypePath(valueComponentADB, 0);
							switch (sdaiGetADBType(valueComponentADB)) {
								case  sdaiINTEGER:
									{
										int_t	value = 0;
										sdaiGetADBValue(valueComponentADB, sdaiINTEGER, (void*) &value);
										fprintf(fp, "#     value = %i [%s]\n", (int) value, typePath);
									}
									break;
								case  sdaiREAL:
									{
										double	value = 0;
										sdaiGetADBValue(valueComponentADB, sdaiREAL, (void*) &value);
										fprintf(fp, "#     value = %f [%s]\n", value, typePath);
									}
									break;
								case  sdaiSTRING:
									{
										char	* value = nullptr;
										sdaiGetADBValue(valueComponentADB, sdaiSTRING, (void*) &value);
										fprintf(fp, "#     value = %s [%s]\n", value, typePath);
									}
									break;
								default:
									assert(false);
									break;
							}
						}
						else {
//							int u = internalGetP21Line(representationItemInstance);
							assert(false);
						}
					}
				}
			}
		}
	}
}

int64_t	GetDatatypeProperty__INTEGER(int64_t owlInstance, char * propertyName)
{
	int64_t	* values = nullptr,
			card = 0;
	GetDatatypeProperty(
			owlInstance,
			GetPropertyByName(
					GetModel(
							owlInstance
						),
					propertyName
				),
			(void**) &values,
			&card
		);
	return	(card == 1) ? values[0] : 0;
}

double	GetDatatypeProperty__DOUBLE(int64_t owlInstance, char * propertyName)
{
	double	* values = nullptr;
	int64_t	card = 0;
	GetDatatypeProperty(
			owlInstance,
			GetPropertyByName(
					GetModel(
							owlInstance
						),
					propertyName
				),
			(void**) &values,
			&card
		);
	return	(card == 1) ? values[0] : 0.;
}

void	SetDatatypeProperty__DOUBLE(int64_t owlInstance, char * propertyName, double value)
{
	SetDatatypeProperty(
			owlInstance,
			GetPropertyByName(
					GetModel(
							owlInstance
						),
					propertyName
				),
			&value,
			1
		);
}

int64_t	GetObjectProperty(int64_t owlInstance, char * propertyName)
{
	int64_t	* values = nullptr,
			card = 0;
	GetObjectProperty(
			owlInstance,
			GetPropertyByName(
					GetModel(
							owlInstance
						),
					propertyName
				),
			&values,
			&card
		);
	return	(card == 1) ? values[0] : 0;
}

void	SetObjectProperty(int64_t owlInstance, char * propertyName, int64_t owlInstanceObject)
{
	SetObjectProperty(
			owlInstance,
			GetPropertyByName(
					GetModel(
							owlInstance
						),
					propertyName
				),
			&owlInstanceObject,
			1
		);
}

void	AddObjectProperty(int64_t owlInstance, char * propertyName, int64_t owlInstanceObject)
{
	int64_t	* valuesOut = nullptr,
			cardOut = 0;
	GetObjectProperty(
			owlInstance,
			GetPropertyByName(
					GetModel(
							owlInstance
						),
					propertyName
				),
			&valuesOut,
			&cardOut
		);

	if (cardOut == 0) {
		SetObjectProperty(
				owlInstance,
				propertyName,
				owlInstanceObject
			);
	}
	else {
		int64_t	* valuesIn = new int64_t[cardOut + 1],
				cardIn = cardOut + 1;

		memcpy(valuesIn, valuesOut, cardOut * sizeof(int64_t));
		valuesIn[cardOut] = owlInstanceObject;

		SetObjectProperty(
				owlInstance,
				GetPropertyByName(
						GetModel(
								owlInstance
							),
						propertyName
					),
				valuesIn,
				cardIn
			);
	}
}

void	WalkAssemblyTreeRecursively(
				int_t				stepModel,
				char				* stepName,
				char				* groupName,
				PRODUCT_DEFINITION	* myProductDefinition,
				MATRIX				* parentMatrix,
				int64_t				myCollectionInstance
			)
{
	size_t	lenStepName = strlen(stepName);
	size_t	lenGroupName = strlen(groupName);

	ASSEMBLY	* myAssembly = firstAssembly;
	while (myAssembly) {
		if (myAssembly->relating_product_definition == myProductDefinition) {
			//
			//	Update namings (stepName / groupName)
			//
			stepName[lenStepName + 0] = ' ';
			stepName[lenStepName + 1] = '-';
			stepName[lenStepName + 2] = ' ';
			memcpy(&stepName[lenStepName + 3], myAssembly->attr_id, strlen(myAssembly->attr_id) + 1);
			size_t	len = strlen(stepName);
			stepName[len + 0] = ' ';
			stepName[len + 1] = '-';
			stepName[len + 2] = ' ';
			memcpy(&stepName[len + 3], myAssembly->related_product_definition->attr_product_id, strlen(myAssembly->related_product_definition->attr_product_id) + 1);

			groupName[lenGroupName] = '_';
			_itoa_s(myAssembly->expressID, &groupName[lenGroupName + 1], 512 - lenGroupName, 10);

			int64_t	owlInstanceMatrix = 0;
			owlBuildInstance(stepModel, internalGetInstanceFromP21Line(stepModel, myAssembly->expressID), &owlInstanceMatrix);
			if (owlInstanceMatrix && GetInstanceClass(owlInstanceMatrix) == GetClassByName(GetModel(owlInstanceMatrix), "Transformation")) {
				owlInstanceMatrix = GetObjectProperty(owlInstanceMatrix, "matrix");
			}
			assert(owlInstanceMatrix == 0 || GetInstanceClass(owlInstanceMatrix) == GetClassByName(GetModel(owlInstanceMatrix), "Matrix") || GetInstanceClass(owlInstanceMatrix) == GetClassByName(GetModel(owlInstanceMatrix), "MatrixMultiplication"));

			MATRIX	matrix;
			MatrixIdentity(&matrix);
			if (owlInstanceMatrix) {
				InferenceInstance(owlInstanceMatrix);
				matrix._11 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_11");
				matrix._12 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_12");
				matrix._13 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_13");
				matrix._21 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_21");
				matrix._22 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_22");
				matrix._23 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_23");
				matrix._31 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_31");
				matrix._32 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_32");
				matrix._33 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_33");
				matrix._41 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_41");
				matrix._42 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_42");
				matrix._43 = GetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_43");
			}

			if (parentMatrix) {
				MatrixMultiply(&matrix, &matrix, parentMatrix);
			}

			WalkAssemblyTreeRecursively(stepModel, stepName, groupName, myAssembly->related_product_definition, &matrix, myCollectionInstance);
			stepName[lenStepName] = 0;
			groupName[lenGroupName] = 0;
		}
		myAssembly = myAssembly->next;
	}

	if (myProductDefinition->relating_product_refs == 0) {
		groupName[lenGroupName] = '_';
		_itoa_s(myProductDefinition->expressID, &groupName[lenGroupName + 1], 512 - lenGroupName, 10);

		int_t	myProductDefinitionInstanceHandle = internalGetInstanceFromP21Line(stepModel, myProductDefinition->expressID);

		int64_t	owlInstanceProductDefinition = 0;
		owlBuildInstance(stepModel, myProductDefinitionInstanceHandle, &owlInstanceProductDefinition);

		int64_t	owlInstanceTransformation = CreateInstance(GetClassByName(stepModel, "Transformation")),
				owlInstanceMatrix = CreateInstance(GetClassByName(stepModel, "Matrix"));

		SetObjectProperty(owlInstanceTransformation, "object", owlInstanceProductDefinition);
		SetObjectProperty(owlInstanceTransformation, "matrix", owlInstanceMatrix);

		if (parentMatrix) {
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_11", parentMatrix->_11);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_12", parentMatrix->_12);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_13", parentMatrix->_13);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_21", parentMatrix->_21);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_22", parentMatrix->_22);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_23", parentMatrix->_23);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_31", parentMatrix->_31);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_32", parentMatrix->_32);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_33", parentMatrix->_33);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_41", parentMatrix->_41);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_42", parentMatrix->_42);
			SetDatatypeProperty__DOUBLE(owlInstanceMatrix, "_43", parentMatrix->_43);
		}

		//
		//	Add to the collection
		//
		AddObjectProperty(myCollectionInstance, "objects", owlInstanceTransformation);
	}
}

int64_t	BuildProductDefinition(
				int_t	stepModel
			)
{
	setSegmentation(stepModel, 12, 0.);

    int_t * productDefinitionInstances = sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION"),
          noProductDefinitionInstances = sdaiGetMemberCount(productDefinitionInstances);
	for (int_t i = 0; i < noProductDefinitionInstances; i++) {
		int_t productDefinitionInstance = 0;
		engiGetAggrElement(productDefinitionInstances, i, sdaiINSTANCE, &productDefinitionInstance);

		GetProductDefinition(productDefinitionInstance, false, false);
	}

	//
	//	Find ASSEMBLY structure
	//
    int_t * nextAssemblyUsageOccurrenceInstances = sdaiGetEntityExtentBN(stepModel, "NEXT_ASSEMBLY_USAGE_OCCURRENCE"),
          noNextAssemblyUsageOccurrenceInstances = sdaiGetMemberCount(nextAssemblyUsageOccurrenceInstances);
	for (int_t i = 0; i < noNextAssemblyUsageOccurrenceInstances; i++) {
		int_t nextAssemblyUsageOccurrenceInstance = 0;
		engiGetAggrElement(nextAssemblyUsageOccurrenceInstances, i, sdaiINSTANCE, &nextAssemblyUsageOccurrenceInstance);

		ASSEMBLY	* myAssembly = new ASSEMBLY;

		myAssembly->expressID = (int) internalGetP21Line(nextAssemblyUsageOccurrenceInstance);

		myAssembly->attr_id = nullptr;
		sdaiGetAttrBN(nextAssemblyUsageOccurrenceInstance, "id", sdaiSTRING, &myAssembly->attr_id);
		myAssembly->attr_name = nullptr;
		sdaiGetAttrBN(nextAssemblyUsageOccurrenceInstance, "name", sdaiSTRING, &myAssembly->attr_name);
		myAssembly->attr_description = nullptr;
		sdaiGetAttrBN(nextAssemblyUsageOccurrenceInstance, "description", sdaiSTRING, &myAssembly->attr_description);

		int_t	relating_product_definition = 0;
		sdaiGetAttrBN(nextAssemblyUsageOccurrenceInstance, "relating_product_definition", sdaiINSTANCE, &relating_product_definition);
		myAssembly->relating_product_definition = GetProductDefinition(relating_product_definition, true, false);

		int_t	related_product_definition = 0;
		sdaiGetAttrBN(nextAssemblyUsageOccurrenceInstance, "related_product_definition", sdaiINSTANCE, &related_product_definition);
		myAssembly->related_product_definition = GetProductDefinition(related_product_definition, false, true);

		myAssembly->firstChild = nullptr;
		myAssembly->sibling = nullptr;
		myAssembly->next = firstAssembly;

		firstAssembly = myAssembly;
	}

	int64_t	myCollectionInstance =
				CreateInstance(
						GetClassByName(
								(int64_t) stepModel,
								"Collection"
							)
					);

	{
		PRODUCT_DEFINITION	* myProductDefinition = firstProductDefinition;
		while (myProductDefinition) {
			if (myProductDefinition->related_product_refs == 0) {
				char	stepName[1024];
				memcpy(stepName, myProductDefinition->attr_product_id, strlen(myProductDefinition->attr_product_id) + 1);

				char	groupName[1024];
				_itoa_s(myProductDefinition->expressID, groupName, 512, 10);
				WalkAssemblyTreeRecursively(stepModel, stepName, groupName, myProductDefinition, nullptr, myCollectionInstance);
			}
			myProductDefinition = myProductDefinition->next;
		}
	}

	return	myCollectionInstance;
}