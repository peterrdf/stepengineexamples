#include "stdafx.h"

#include "Step1.h"
#include ".\stepengine\include\engine.h"


void	Step1(
				wchar_t	* outputFile
			)
{
	int64_t	myModel = CreateModel();

	if (myModel) {
		//
		//	Get relevant Class handles
		//
		int64_t	classBooleanOperation = GetClassByName(myModel, "BooleanOperation"),
				classBox = GetClassByName(myModel, "Box"),
				classMatrix = GetClassByName(myModel, "Matrix"),
				classTransformation = GetClassByName(myModel, "Transformation");

		//
		//	Get relevant Datatype Property handles
		//
		int64_t	property_41 = GetPropertyByName(myModel, "_41"),
				property_42 = GetPropertyByName(myModel, "_42"),
				propertyType = GetPropertyByName(myModel, "type"),
				propertyLength = GetPropertyByName(myModel, "length"),
				propertyHeight = GetPropertyByName(myModel, "height"),
				propertyWidth = GetPropertyByName(myModel, "width");

		//
		//	Get relevant Relation handles
		//
		int64_t	propertyFirstObject = GetPropertyByName(myModel, "firstObject"),
				propertySecondObject = GetPropertyByName(myModel, "secondObject"),
				propertyMatrix = GetPropertyByName(myModel, "matrix"),
				propertyObject = GetPropertyByName(myModel, "object");

		//
		//	Create Instances
		//
		int64_t	myBooleanOperation = CreateInstance(classBooleanOperation),
				myOuterBox = CreateInstance(classBox),
				myTransformation = CreateInstance(classTransformation),
				myMatrix = CreateInstance(classMatrix),
				myInnerBox = CreateInstance(classBox);

		//
		//	Set Inter-Relations
		//
		SetObjectProperty(myBooleanOperation, propertyFirstObject, &myOuterBox, 1);
		SetObjectProperty(myBooleanOperation, propertySecondObject, &myTransformation, 1);
		SetObjectProperty(myTransformation, propertyObject, &myInnerBox, 1);
		SetObjectProperty(myTransformation, propertyMatrix, &myMatrix, 1);

		//
		//	Set Values
		//
		int64_t	type = 1;	//	difference (A - B)
		double	offset = 1,
				lengthInner = 3,
				lengthOuter = 5,
				widthInner = 2,
				widthOuter = 4,
				height = 1;
		SetDatatypeProperty(myBooleanOperation, propertyType, &type, 1);
		SetDatatypeProperty(myMatrix, property_41, &offset, 1);
		SetDatatypeProperty(myMatrix, property_42, &offset, 1);
		SetDatatypeProperty(myOuterBox, propertyLength, &lengthOuter, 1);
		SetDatatypeProperty(myOuterBox, propertyWidth, &widthOuter, 1);
		SetDatatypeProperty(myOuterBox, propertyHeight, &height, 1);
		SetDatatypeProperty(myInnerBox, propertyLength, &lengthInner, 1);
		SetDatatypeProperty(myInnerBox, propertyWidth, &widthInner, 1);
		SetDatatypeProperty(myInnerBox, propertyHeight, &height, 1);

		//
		//	Save and close the model
		//
		SaveModelW(myModel, outputFile);
		CloseModel(myModel);
	}
}
