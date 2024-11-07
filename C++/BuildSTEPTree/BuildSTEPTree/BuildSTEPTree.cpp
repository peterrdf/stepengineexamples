// BuildSTEPTree.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "./stepengine/include/stepengine.h"
#include "./stepengine/include/engine.h"

#include <iostream>
#include <cstring>
#include <assert.h>


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

void	GetProductDefinitionInstances(
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

MyStructProductDefinition	* FindProductDefinition(
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

void	GetProductDefinitionShapeInstances(
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

int64_t	BuildParts(
				int64_t		rdfModel
			)
{
	MyStructProductDefinition	* productDefinition = firstProductDefinition;

	int64_t	partCnt = 0;
	while (productDefinition) {
		if (productDefinition->children == 0 && productDefinition->geometryInstance) {
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
			if (productDefinition->children == 0 && productDefinition->geometryInstance) {
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

		int64_t	instanceCollection = CreateInstance(classCollection);

		SetObjectProperty(instanceCollection, propertyObjects, parts, partCnt);

		delete[] parts;

		return	instanceCollection;
	}

	return	0;
}

int64_t	BuildAssemblyGetElementCnt(
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

int64_t	BuildAssemblyGetElement(
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

				int64_t	instanceTransformation = CreateInstance(classTransformation);

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

int64_t	BuildAssembly(
				int64_t						rdfModel,
				MyStructProductDefinition	* productDefinition
			)
{
	int64_t	elementCnt = BuildAssemblyGetElementCnt(productDefinition);

	int64_t	* elements,
			instanceCollection;

	if (elementCnt) {
		elements = new int64_t[elementCnt];

		int u = 0;
		int64_t	classCollection = GetClassByName(rdfModel, "Collection"),
				propertyObjects = GetPropertyByName(rdfModel, "objects");

		instanceCollection = CreateInstance(classCollection);

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

int64_t	BuildAssemblies(
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

		int64_t	instanceCollection = CreateInstance(classCollection);

		SetObjectProperty(instanceCollection, propertyObjects, assemblies, assemblyCnt);

		delete[] assemblies;

		return	instanceCollection;
	}

	return	0;
}

int _tmain(int argc, _TCHAR* argv[])
{
	if (argc != 3)
	{
		std::cout << " Usage: BuildSTEPTree myInputFile.stp geometry.bin" << std::endl;
		return -1;
	}

	uint64_t	flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
	uint64_t	flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
	uint64_t	flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000

	//
	//  http://rdf.bg/downloads/setFilter.pdf	//
	//	override schema loading from file with internal AP242 schema when embedded in the DLL
	//
	setFilter(0, flagbit22, flagbit20 + flagbit21 + flagbit22);

	int_t	stepModel = sdaiOpenModelBNUnicode(0, (const wchar_t*) argv[1], (const wchar_t*) 1);

	if (stepModel) {
		GetProductDefinitionInstances(stepModel);
		GetProductDefinitionShapeInstances(stepModel);

		int64_t	rdfModel = 0;
		owlGetModel(stepModel, &rdfModel);

		int64_t	collections[2];
		collections[0] = BuildParts(rdfModel);
		collections[1] = BuildAssemblies(rdfModel);

		int64_t	classCollection = GetClassByName(rdfModel, "Collection"),
				propertyObjects = GetPropertyByName(rdfModel, "objects");

		int64_t	instanceCollection = CreateInstance(classCollection);

		SetObjectProperty(instanceCollection, propertyObjects, collections, 2);

		SaveInstanceTreeW(instanceCollection, (wchar_t*) argv[2]);

		sdaiCloseModel(stepModel);

		stepModel = 0;
	}

	return 0;
}

