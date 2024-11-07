// stp2bin.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "stp2bin.h"

#include "convert.h"

#include <assert.h>


#ifdef _DEBUG
#define new DEBUG_NEW
#endif



double	qualityFactor;
bool	qualityPrepareBooleanOperations,
		qualityPrepareBoundaryRepresentations;


const	uint_t	flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
const	uint_t	flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
const	uint_t	flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000



wchar_t	* charToWChar(
				char	* input
			)
{
	if (input) {
		int i = 0;
		while (input[i]) { i++; }
		wchar_t	* output = new wchar_t[i + 1];

		i = 0;
		while (input[i]) {
			output[i] = (wchar_t) input[i];
			i++;
		}
		output[i] = 0;

		return	output;
	}

	return	nullptr;
}

void	ConvertSTPfile(
				wchar_t		* stepFileName,
				wchar_t		* rdfFileName
			)
{
	//
	//  http://rdf.bg/downloads/setFilter.pdf
	//
	//	override schema loading from file with internal AP242 schema when embedded in the DLL
	//

	int_t	stepModel = sdaiOpenModelBNUnicode(0, (const wchar_t*) stepFileName, L"");
	if (stepModel) {
		if (qualityFactor > 2) {
			int_t	elements = (int_t) (1000. * qualityFactor);

			setBRepProperties(
					stepModel,
					1 + 2 + 4 + 64,
					0.7,
					0.00001,
					elements	//	100000
				);
		}

//		GetProductDefinitionInstances(stepModel);
//		GetProductDefinitionShapeInstances(stepModel);

//		int64_t	rdfModel = 0;
//		owlGetModel(stepModel, &rdfModel);

		int64_t	collection = BuildProductDefinition(stepModel);

//		int64_t	classCollection = GetClassByName(rdfModel, "Collection"),
//				propertyObjects = GetPropertyByName(rdfModel, "objects");

//		int64_t	instanceCollection = CreateInstance(classCollection, nullptr);

//		SetObjectProperty(instanceCollection, propertyObjects, &collections, 1);

		SaveInstanceTreeW(collection, rdfFileName);

		sdaiCloseModel(stepModel);
	}
	else {
		assert(false);
	}
}

void	setQuality(
				double		factor,
				bool		prepareBooleanOperations,
				bool		prepareBoundaryRepresentations
			)
{
	qualityFactor = factor;
	qualityPrepareBooleanOperations = prepareBooleanOperations;
	qualityPrepareBoundaryRepresentations = prepareBoundaryRepresentations;
}

void	STP2BIN(
				wchar_t		* inputFileName,
				wchar_t		* outputFileName
			)
{
	ConvertSTPfile(
			inputFileName,
			outputFileName
		);
}