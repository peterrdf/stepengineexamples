#include "stdafx.h"

#include "Step2.h"
#include ".\stepengine\include\engine.h"

#include "flagbits.h"

#include <assert.h>


int64_t	GetFirstBooleanOperationInstance(
				int64_t	myModel
			)
{
	int64_t	classBooleanOperation = GetClassByName(myModel, "BooleanOperation");
	int64_t	myInstance = GetInstancesByIterator(myModel, 0);
	while (myInstance) {
		if (GetInstanceClass(myInstance) == classBooleanOperation) {
			return	myInstance;
		}
		myInstance = GetInstancesByIterator(myModel, myInstance);
	}
	return	0;
}

SIMPLE_TRIANGLE	** AddTriangle(
						SIMPLE_TRIANGLE	** pMyCurrentSimpleTriangle,
						double			* firstVertex,
						double			* secondVertex,
						double			* thirdVertex
					)
{
	(*pMyCurrentSimpleTriangle) = new SIMPLE_TRIANGLE;

	(*pMyCurrentSimpleTriangle)->pntI.x = firstVertex[0];
	(*pMyCurrentSimpleTriangle)->pntI.y = firstVertex[1];
	(*pMyCurrentSimpleTriangle)->pntI.z = firstVertex[2];

	(*pMyCurrentSimpleTriangle)->pntII.x = secondVertex[0];
	(*pMyCurrentSimpleTriangle)->pntII.y = secondVertex[1];
	(*pMyCurrentSimpleTriangle)->pntII.z = secondVertex[2];

	(*pMyCurrentSimpleTriangle)->pntIII.x = thirdVertex[0];
	(*pMyCurrentSimpleTriangle)->pntIII.y = thirdVertex[1];
	(*pMyCurrentSimpleTriangle)->pntIII.z = thirdVertex[2];

	(*pMyCurrentSimpleTriangle)->next = nullptr;

	return	&(*pMyCurrentSimpleTriangle)->next;
}

SIMPLE_TRIANGLE	* Step2(
						wchar_t	* inputFile
					)
{
	SIMPLE_TRIANGLE	* myFirstSimpleTriangle = nullptr,
					** pMyCurrentSimpleTriangle = &myFirstSimpleTriangle;

	int64_t	myModel = OpenModelW(inputFile);

	if (myModel) {
		//
		//	Find Boolean Operation Instance
		//
		int64_t	myInstance = GetFirstBooleanOperationInstance(myModel);

		//
		//	Define the output of the Vertex and Index arrays
		//
		int64_t	setting = 0, mask = 0;
		mask += flagbit2;        //    PRECISION (32/64 bit)
		mask += flagbit3;        //	   INDEX ARRAY (32/64 bit)
		mask += flagbit5;        //    NORMALS
		mask += flagbit8;        //    TRIANGLES
		mask += flagbit9;        //    LINES
		mask += flagbit10;       //    POINTS
		mask += flagbit12;       //    WIREFRAME (ALL FACES)
		mask += flagbit13;       //    WIREFRAME (CONCEPTUAL FACES)
//		mask += flagbit24;		 //	   AMBIENT
//		mask += flagbit25;		 //	   DIFFUSE
//		mask += flagbit26;		 //	   EMISSIVE
//		mask += flagbit27;		 //	   SPECULAR

		setting += flagbit2;	 //    DOUBLE PRECISION (double)
		setting += flagbit3;     //    64 BIT INDEX ARRAY (Int64)
		setting += 0;		     //    NORMALS OFF
		setting += flagbit8;     //    TRIANGLES ON
		setting += 0;		     //    LINES OFF
		setting += 0;		     //    POINTS OFF
		setting += 0;		     //    WIREFRAME OFF (ALL FACES)
		setting += 0;		     //    WIREFRAME OFF (CONCEPTUAL FACES)
//		setting += 0;			 //	   AMBIENT OFF
//		setting += 0;			 //	   DIFFUSE OFF
//		setting += 0;			 //	   EMISSIVE OFF
//		setting += 0;			 //	   SPECULAR OFF
		int64_t	vertexElementLength = SetFormat(myModel, setting, mask);

		//
		//	Get Triangles
		//
		int64_t	vertexBufferSize = 0, indexBufferSize = 0;
		CalculateInstance(myInstance, &vertexBufferSize, &indexBufferSize, 0);
		if (vertexBufferSize && indexBufferSize) {
			assert(vertexElementLength == 3 * sizeof(double));
			double	* vertexBuffer = new double[3 * (int_t) vertexBufferSize];
			UpdateInstanceVertexBuffer(myInstance, vertexBuffer);

			int64_t	* indexBuffer = new int64_t[(int_t) indexBufferSize];
			UpdateInstanceIndexBuffer(myInstance, indexBuffer);

			int64_t	conceptualFaceCnt = GetConceptualFaceCnt(myInstance);
			for (int64_t index = 0; index < conceptualFaceCnt; index++) {
				int64_t	startIndexTriangles = 0, noIndicesTriangles = 0;
				GetConceptualFaceEx(
						myInstance, index,
						&startIndexTriangles, &noIndicesTriangles,
						0, 0,
						0, 0,
						0, 0,
						0, 0
					);

				int i;
				for (i = 0; i < noIndicesTriangles; i += 3) {
					pMyCurrentSimpleTriangle = AddTriangle(
														pMyCurrentSimpleTriangle,
														&vertexBuffer[3 * indexBuffer[startIndexTriangles + i + 0]],
														&vertexBuffer[3 * indexBuffer[startIndexTriangles + i + 1]],
														&vertexBuffer[3 * indexBuffer[startIndexTriangles + i + 2]]
													);
				}
				assert(i == noIndicesTriangles);
			}

			delete[] vertexBuffer;
			delete[] indexBuffer;
		}

		CloseModel(myModel);
	}

	return	myFirstSimpleTriangle;
}
