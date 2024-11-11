#include "stdafx.h"

#include "Step4.h"
#include ".\stepengine\include\engine.h"

#include "flagbits.h"

#include <assert.h>



int64_t	GetFirstBoundaryRepresentationInstance(
				int64_t	myModel
			)
{
	int64_t	classBoundaryRepresentation = GetClassByName(myModel, "BoundaryRepresentation");
	int64_t	myInstance = GetInstancesByIterator(myModel, 0);
	while (myInstance) {
		if (GetInstanceClass(myInstance) == classBoundaryRepresentation) {
			return	myInstance;
		}
		myInstance = GetInstancesByIterator(myModel, myInstance);
	}
	return	0;
}

void	CreateTextVertex(
				wchar_t	* text,
				wchar_t	* lineStart,
				int		vertexIndex,
				double	* vertexBuffer
			)
{
	memcpy(text, lineStart, wcslen(lineStart) * sizeof(wchar_t));
	text = &text[wcslen(lineStart)];

	text[0] = '[';
	text = &text[1];
	_itow_s(vertexIndex, text, 10, 10);
	while (text[0]) { text = &text[1]; }
	text[0] = ']';
	text[1] = ':';
	text[2] = ' ';
	text = &text[3];

	_itow_s((int) vertexBuffer[0], text, 10, 10);

	while (text[0]) { text = &text[1]; }
	text[0] = '.';
	text[1] = ',';
	text[2] = ' ';
	text = &text[3];

	_itow_s((int) vertexBuffer[1], text, 10, 10);

	while (text[0]) { text = &text[1]; }
	text[0] = '.';
	text[1] = ',';
	text[2] = ' ';
	text = &text[3];

	_itow_s((int) vertexBuffer[2], text, 10, 10);

	while (text[0]) { text = &text[1]; }
	text[0] = '.';
	text[1] = 0;
}

void	CreateTextPolygon(
				wchar_t	* text,
				wchar_t	* lineStart,
				int		* indexMapping,
				int64_t	* indexBuffer,
				int64_t	noElements
			)
{
	memcpy(text, lineStart, wcslen(lineStart) * sizeof(wchar_t));
	text = &text[wcslen(lineStart)];

	int i = 0;
	while (i < noElements) {
		assert(indexMapping[(int_t) indexBuffer[i]] >= 0);
		_itow_s(indexMapping[(int_t) indexBuffer[i]], text, 10, 10);

		i++;
		if (i < noElements) {
			while (text[0]) { text = &text[1]; }
			text[0] = ',';
			text[1] = ' ';
			text = &text[2];
		}
	}
}

void	Step4(
				wchar_t		* inputFile,
				CListBox	* listBoxVertices,
				CListBox	* listBoxPolygons
			)
{
	int64_t	myModel = OpenModelW(inputFile);

	if (myModel) {
		//
		//	Find Boolean Operation Instance
		//
		int64_t	myInstance = GetFirstBoundaryRepresentationInstance(myModel);

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
		setting += flagbit12;    //    WIREFRAME ON (ALL FACES)
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

			int	* vertexElementUsed = new int[(int_t) vertexBufferSize];
			for (int i = 0; i < vertexBufferSize; i++) {
				vertexElementUsed[i] = 0;
			}

			int64_t	* indexBuffer = new int64_t[(int_t) indexBufferSize];
			UpdateInstanceIndexBuffer(myInstance, indexBuffer);

			{
				int64_t	conceptualFaceCnt = GetConceptualFaceCnt(myInstance);
				for (int64_t index = 0; index < conceptualFaceCnt; index++) {
					int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
					GetConceptualFaceEx(
							myInstance, index,
							0, 0,
							0, 0,
							0, 0,
							&startIndexFacePolygons, &noIndicesFacePolygons,
							0, 0
						);

					for (int j = 0; j < noIndicesFacePolygons; j++) {
						if (indexBuffer[startIndexFacePolygons + j] >= 0) {
							vertexElementUsed[indexBuffer[startIndexFacePolygons + j]]++;
						}
					}
				}
			}

			//
			//	Create index mapping
			//
			int currentIndex = 0;
			for (int k = 0; k < vertexBufferSize; k++) {
				if (vertexElementUsed[k]) {
					vertexElementUsed[k] = currentIndex;
					currentIndex++;
				}
				else {
					vertexElementUsed[k] = -1;
				}
			}

			{
				int64_t	conceptualFaceCnt = GetConceptualFaceCnt(myInstance);
				for (int64_t index = 0; index < conceptualFaceCnt; index++) {
					int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
					GetConceptualFaceEx(
							myInstance, index,
							0, 0,
							0, 0,
							0, 0,
							&startIndexFacePolygons, &noIndicesFacePolygons,
							0, 0
						);

					int k = 0, startIndicesPolygon = 0;
					while (k < noIndicesFacePolygons) {
						if (indexBuffer[startIndexFacePolygons + k] < 0) {
							wchar_t	* text = new wchar_t[512];
							if (indexBuffer[startIndexFacePolygons + k] == -1) {
								//
								//	found an outer polygon
								//
								CreateTextPolygon(text, L"Outer Pol: ", vertexElementUsed, &indexBuffer[startIndexFacePolygons + startIndicesPolygon], k - startIndicesPolygon);
							}
							else {
								assert(indexBuffer[startIndexFacePolygons + k] == -2);
								//
								//	found an inner polygon
								//
								CreateTextPolygon(text, L"  Inner Pol: ", vertexElementUsed, &indexBuffer[startIndexFacePolygons + startIndicesPolygon], k - startIndicesPolygon);
							}
							listBoxPolygons->AddString(text);
							startIndicesPolygon = k + 1;
						}
						k++;
					}
				}
			}

			//
			//	Now add all (now unqiue) vertices to the list
			//
			for (int i = 0; i < vertexBufferSize; i++) {
				if (vertexElementUsed[i] >= 0) {
					wchar_t	* text = new wchar_t[512];
					CreateTextVertex(text, L"Vertex: ", i, &vertexBuffer[3 * i]);
					listBoxVertices->AddString(text);
				}
			}

			delete[] vertexBuffer;
			delete[] indexBuffer;
		}

		CloseModel(myModel);
	}
}
