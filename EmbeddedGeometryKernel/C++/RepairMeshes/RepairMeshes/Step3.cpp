#include "stdafx.h"

#include "Step3.h"
#include ".\stepengine\include\engine.h"

#include <assert.h>


void	Step3(
				wchar_t			* outputFile,
				SIMPLE_TRIANGLE	* mySimpleTriangles
			)
{
	int64_t	myModel = CreateModel();

	if (myModel) {
		//
		//	Get relevant Class handle
		//
		int64_t	classBoundaryRepresentation = GetClassByName(myModel, "BoundaryRepresentation");

		//
		//	Get relevant Datatype Property handles
		//
		int64_t	propertyIndices = GetPropertyByName(myModel, "indices"),
				propertyVertices = GetPropertyByName(myModel, "vertices"),
				propertyConsistencyCheck = GetPropertyByName(myModel, "consistencyCheck");

		//
		//	Create Instance
		//
		int64_t	myBoundaryRepresentation = CreateInstance(classBoundaryRepresentation);

		//
		//	Set Values
		//

		//
		//	consistencyCheck
		//		bit0   (1)	merge elements in the vertex array are duplicated (epsilon used as distance)
		//		bit1   (2)	remove elements in the vertex array that are not referenced by elements in the index array (interpreted as SET if flags are defined)
		//		bit2   (4)	merge polygons placed in the same plane and sharing at least one edge
		//		bit3   (8)	merge polygons advanced (check of polygons have the opposite direction and are overlapping, but don't share points)
		//		bit4  (16)	check if faces are wrongly turned opposite from each other
		//		bit5  (32)	check if faces are inside-out
		//		bit6  (64)	check if faces result in solid, if not generate both sided faces
		//		bit7 (128)	invert direction of the faces / normals
		//		bit8 (256)	export all faces as one conceptual face
		//
		int64_t	consistencyCheck = 1 + 2 + 4 + 16 + 32;

		int_t	noTriangles = 0;
		{
			SIMPLE_TRIANGLE	* triangles = mySimpleTriangles;
			while (triangles) {
				noTriangles++;
				triangles = triangles->next;
			}
		}

		SetDatatypeProperty(myBoundaryRepresentation, propertyConsistencyCheck, &consistencyCheck, 1);


		double	* vertices = new double[3 * 3 * noTriangles];
		int64_t	* indices = new int64_t[5 * noTriangles];

		int_t	triangleIndex = 0;
		{
			SIMPLE_TRIANGLE	* triangles = mySimpleTriangles;
			while (triangles) {
				vertices[3 * (3 * triangleIndex + 0) + 0] = triangles->pntI.x;
				vertices[3 * (3 * triangleIndex + 0) + 1] = triangles->pntI.y;
				vertices[3 * (3 * triangleIndex + 0) + 2] = triangles->pntI.z;

				vertices[3 * (3 * triangleIndex + 1) + 0] = triangles->pntII.x;
				vertices[3 * (3 * triangleIndex + 1) + 1] = triangles->pntII.y;
				vertices[3 * (3 * triangleIndex + 1) + 2] = triangles->pntII.z;

				vertices[3 * (3 * triangleIndex + 2) + 0] = triangles->pntIII.x;
				vertices[3 * (3 * triangleIndex + 2) + 1] = triangles->pntIII.y;
				vertices[3 * (3 * triangleIndex + 2) + 2] = triangles->pntIII.z;

				indices[5 * triangleIndex + 0] = 3 * triangleIndex + 0;
				indices[5 * triangleIndex + 1] = 3 * triangleIndex + 1;
				indices[5 * triangleIndex + 2] = 3 * triangleIndex + 2;
				indices[5 * triangleIndex + 3] = 3 * triangleIndex + 0;
				indices[5 * triangleIndex + 4] = -1;		//		-1 means end of polygon, in this case a triangles, -2 means end of inner polygon

				triangleIndex++;
				triangles = triangles->next;
			}
		}
		assert(triangleIndex == noTriangles);

		SetDatatypeProperty(myBoundaryRepresentation, propertyVertices, vertices, 3 * 3 * noTriangles);
		SetDatatypeProperty(myBoundaryRepresentation, propertyIndices, indices, 5 * noTriangles);

		//
		//	Save and close the model
		//
		SaveModelW(myModel, outputFile);
		CloseModel(myModel);
	}
}
