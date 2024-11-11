
// BooleanOperationDlg.cpp : implementation file
//

#include "stdafx.h"
#include "BooleanOperation.h"
#include "BooleanOperationDlg.h"
#include "afxdialogex.h"

#include <assert.h>

#include "stepengine\include\engine.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif




//
//	Declaration First Object
//
double	* vertexBufferFirstObject = nullptr;
int64_t	vertexBufferSizeFirstObject = 0,
		* indexBufferFirstObject = nullptr,
		indexBufferSizeFirstObject = 0;


//
//	Declaration Second Object
//
double	* vertexBufferSecondObject = nullptr;
int64_t	vertexBufferSizeSecondObject = 0,
		* indexBufferSecondObject = nullptr,
		indexBufferSizeSecondObject = 0;


//
//	Declaration Boolean Operation Result
//
double	* vertexBufferBooleanOperationResult = nullptr;
int64_t	vertexBufferSizeBooleanOperationResult = 0,
		* indexBufferBooleanOperationResult = nullptr,
		indexBufferSizeBooleanOperationResult = 0;



// CBooleanOperationDlg dialog



CBooleanOperationDlg::CBooleanOperationDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CBooleanOperationDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBooleanOperationDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_BUTTON_BOOLEAN_OPERATION, m_BooleanOperation);
	DDX_Control(pDX, IDC_BUTTON_FIRST_OBJECT, m_FirstObject);
	DDX_Control(pDX, IDC_BUTTON_SECOND_OBJECT, m_SecondObject);
}

BEGIN_MESSAGE_MAP(CBooleanOperationDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_FIRST_OBJECT, &CBooleanOperationDlg::OnBnClickedButtonFirstObject)
	ON_BN_CLICKED(IDC_BUTTON_SECOND_OBJECT, &CBooleanOperationDlg::OnBnClickedButtonSecondObject)
	ON_BN_CLICKED(IDC_BUTTON_BOOLEAN_OPERATION, &CBooleanOperationDlg::OnBnClickedButtonBooleanOperation)
END_MESSAGE_MAP()


// CBooleanOperationDlg message handlers

BOOL CBooleanOperationDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CBooleanOperationDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CBooleanOperationDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void	ExpectationsForBuffers(int64_t model)
{
	int64_t	setting = 0, mask = 0;

	mask += flagbit2;        //    PRECISION (32/64 bit)
	mask += flagbit3;        //	   INDEX ARRAY (32/64 bit)
	mask += flagbit5;        //    NORMALS
	mask += flagbit8;        //    TRIANGLES
	mask += flagbit9;        //    LINES
	mask += flagbit10;       //    POINTS
	mask += flagbit12;       //    WIREFRAME

	setting += flagbit2;     //    DOUBLE PRECISION (double)
	setting += flagbit3;     //    64 BIT INDEX ARRAY (int64_t)
	setting += 0;		     //    NORMALS OFF
	setting += flagbit8;     //    TRIANGLES ON
	setting += flagbit9;     //    LINES OFF
	setting += 0;			 //    POINTS OFF
	setting += flagbit12;    //    WIREFRAME ON

	SetFormat(model, setting, mask);
}



void CBooleanOperationDlg::OnBnClickedButtonFirstObject()
{
	//
	//	Here we will create a Box within the Geometry Kernel and fill the buffers
	//
	double	boxHeight = 3,
			boxWidth = 2,
			boxLength = 4;


	int64_t	model = CreateModel();
	if (model) {
		//
		//	Get the handles to the classes and properties within the just opened model
		//
		int64_t	classBox = GetClassByName(model, "Box"),
			propertyHeight = GetPropertyByName(model, "height"),
			propertyLength = GetPropertyByName(model, "length"),
			propertyWidth = GetPropertyByName(model, "width");

		//
		//	Create the instance as a minimal design tree
		//
		int64_t instanceBox = CreateInstance(classBox);

		//
		//	Set the properties
		//
		SetDatatypeProperty(instanceBox, propertyHeight, &boxHeight, 1);
		SetDatatypeProperty(instanceBox, propertyLength, &boxLength, 1);
		SetDatatypeProperty(instanceBox, propertyWidth, &boxWidth, 1);

		//
		//	Arrays should contain polygon representation, only export X, Y, Z values and everything in 64 bit precision
		//
		ExpectationsForBuffers(model);

		//
		//	Create internal Geometry for this minimal design tree
		//
		int64_t	vertexBufferSize = 0, indexBufferSize = 0;
		CalculateInstance(instanceBox, &vertexBufferSize, &indexBufferSize, nullptr);

		if (vertexBufferSize && indexBufferSize) {
			m_FirstObject.EnableWindow(false);

			//
			//	With SetFormat(..) we requested double precision for vertices and 3 components (X, Y, Z) per vertex element 
			//
			double	* vertexBuffer = new double[3 * (int)vertexBufferSize];
			UpdateInstanceVertexBuffer(instanceBox, vertexBuffer);

			//
			//	With SetFormat(..) we requested 64 bit integer values 
			//
			int64_t	* indexBuffer = new int64_t[(int)indexBufferSize];
			UpdateInstanceIndexBuffer(instanceBox, indexBuffer);

			//
			//	Now the buffers are ready, we are only interested in the polygons, not in the triangles
			//
			int64_t	conceptualFaceCnt = GetConceptualFaceCnt(instanceBox), cnt = 0;
			for (int64_t i = 0; i < conceptualFaceCnt; i++) {
				int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
				GetConceptualFace(
						instanceBox, i,
						nullptr, nullptr,
						nullptr, nullptr,
						nullptr, nullptr,
						&startIndexFacePolygons, &noIndicesFacePolygons,
						nullptr, nullptr
					);

				//
				//	In this case it is a box, so each conceptual face has exactly 4 lines, i.e. 5 points + ending -1
				//
				assert(noIndicesFacePolygons == 6);
				assert(indexBuffer[startIndexFacePolygons + noIndicesFacePolygons - 1] == -1);

				cnt += noIndicesFacePolygons;
			}
			//
			//	A Box always has exactly 6 conceptual faces (when length, width, height are non-zero)
			//
			assert(conceptualFaceCnt == 6);



			assert(vertexBufferFirstObject == nullptr);
			assert(vertexBufferSizeFirstObject == 0);
			assert(indexBufferFirstObject == nullptr);
			assert(indexBufferSizeFirstObject == 0);


			//
			//	Now assign the results to the buffers
			//
			vertexBufferFirstObject = vertexBuffer;
			vertexBufferSizeFirstObject = vertexBufferSize;
			indexBufferFirstObject = new int64_t[(int)cnt];

			conceptualFaceCnt = GetConceptualFaceCnt(instanceBox);
			for (int64_t i = 0; i < conceptualFaceCnt; i++) {
				int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
				GetConceptualFace(
						instanceBox, i,
						nullptr, nullptr,
						nullptr, nullptr,
						nullptr, nullptr,
						&startIndexFacePolygons, &noIndicesFacePolygons,
						nullptr, nullptr
					);

				memcpy(&indexBufferFirstObject[(int)indexBufferSizeFirstObject], &indexBuffer[(int)startIndexFacePolygons], (int)noIndicesFacePolygons * sizeof(int64_t));
				indexBufferSizeFirstObject += noIndicesFacePolygons;
			}

			assert(indexBufferSizeFirstObject == cnt);

			delete[]  indexBuffer;

			//
			//	We created the basic information of a Box, i.e. 8 vertex elements individual (X, Y, Z) values and 6 polygons (each 5 points ending with -1, i.e. 36 elements)
			//
			assert(vertexBufferSizeFirstObject == 8);
			assert(indexBufferSizeFirstObject == 6 * (4 + 1 + 1));
		}
		else {
			assert(false);
		}

		CloseModel(model);
		model = 0;
	}
	else {
		assert(false);
	}

	if (vertexBufferSizeFirstObject && indexBufferSizeFirstObject && vertexBufferSizeSecondObject && indexBufferSizeSecondObject) {
		m_BooleanOperation.EnableWindow(true);
	}
}


void CBooleanOperationDlg::OnBnClickedButtonSecondObject()
{
	//
	//	Here we will create a Box within the Geometry Kernel and fill the buffers
	//
	double	cylinderHeight = 1.5,
			cylinderRadius = 1.5;
	int64_t	cylinderSegmentationParts = 36;


	int64_t	model = CreateModel();
	if (model) {
		//
		//	Get the handles to the classes and properties within the just opened model
		//
		int64_t	classCylinder = GetClassByName(model, "Cylinder"),
			propertyHeight = GetPropertyByName(model, "height"),
			propertyRadius = GetPropertyByName(model, "radius"),
			propertySegmentationParts = GetPropertyByName(model, "segmentationParts");

		//
		//	Create the instance as a minimal design tree
		//
		int64_t instanceCylinder = CreateInstance(classCylinder);

		//
		//	Set the properties
		//
		SetDatatypeProperty(instanceCylinder, propertyHeight, &cylinderHeight, 1);
		SetDatatypeProperty(instanceCylinder, propertyRadius, &cylinderRadius, 1);
		SetDatatypeProperty(instanceCylinder, propertySegmentationParts, &cylinderSegmentationParts, 1);

		//
		//	Arrays should contain polygon representation, only export X, Y, Z values and everything in 64 bit precision
		//
		ExpectationsForBuffers(model);

		//
		//	Create internal Geometry for this minimal design tree
		//
		int64_t	vertexBufferSize = 0, indexBufferSize = 0;
		CalculateInstance(instanceCylinder, &vertexBufferSize, &indexBufferSize, nullptr);

		if (vertexBufferSize && indexBufferSize) {
			m_SecondObject.EnableWindow(false);

			//
			//	With SetFormat(..) we requested double precision for vertices and 3 components (X, Y, Z) per vertex element 
			//
			double	* vertexBuffer = new double[3 * (int)vertexBufferSize];
			UpdateInstanceVertexBuffer(instanceCylinder, vertexBuffer);

			//
			//	With SetFormat(..) we requested 64 bit integer values 
			//
			int64_t	* indexBuffer = new int64_t[(int)indexBufferSize];
			UpdateInstanceIndexBuffer(instanceCylinder, indexBuffer);

			//
			//	Now the buffers are ready, we are only interested in the polygons, not in the triangles
			//
			int64_t	conceptualFaceCnt = GetConceptualFaceCnt(instanceCylinder), cnt = 0;
			for (int64_t i = 0; i < conceptualFaceCnt; i++) {
				int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
				GetConceptualFace(
						instanceCylinder, i,
						nullptr, nullptr,
						nullptr, nullptr,
						nullptr, nullptr,
						&startIndexFacePolygons, &noIndicesFacePolygons,
						nullptr, nullptr
					);

				cnt += noIndicesFacePolygons;
			}
			//
			//	A Cylnder always has exactly 3 conceptual faces (when radius and height are non-zero)
			//
			assert(conceptualFaceCnt == 3);



			assert(vertexBufferSecondObject == nullptr);
			assert(vertexBufferSizeSecondObject == 0);
			assert(indexBufferSecondObject == nullptr);
			assert(indexBufferSizeSecondObject == 0);


			//
			//	Now assign the results to the buffers
			//
			vertexBufferSecondObject = vertexBuffer;
			vertexBufferSizeSecondObject = vertexBufferSize;
			indexBufferSecondObject = new int64_t[(int)cnt];

			conceptualFaceCnt = GetConceptualFaceCnt(instanceCylinder);
			for (int64_t i = 0; i < conceptualFaceCnt; i++) {
				int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
				GetConceptualFace(
						instanceCylinder, i,
						nullptr, nullptr,
						nullptr, nullptr,
						nullptr, nullptr,
						&startIndexFacePolygons, &noIndicesFacePolygons,
						nullptr, nullptr
					);

				memcpy(&indexBufferSecondObject[(int)indexBufferSizeSecondObject], &indexBuffer[(int)startIndexFacePolygons], (int)noIndicesFacePolygons * sizeof(int64_t));
				indexBufferSizeSecondObject += noIndicesFacePolygons;
			}

			assert(indexBufferSizeSecondObject == cnt);

			delete[]  indexBuffer;

			//
			//	We created the basic information of a Cylinder, i.e. 2 * segmentationParts vertex elements individual (X, Y, Z) values and segmentationparts + 2 polygons
			//
			//			assert(vertexBufferSizeSecondObject == 2 * cylinderSegmentationParts);
			assert(indexBufferSizeSecondObject == 2 * (cylinderSegmentationParts + 1 + 1) + cylinderSegmentationParts * (4 + 1 + 1));
		}
		else {
			assert(false);
		}

		CloseModel(model);
		model = 0;
	}
	else {
		assert(false);
	}

	if (vertexBufferSizeFirstObject && indexBufferSizeFirstObject && vertexBufferSizeSecondObject && indexBufferSizeSecondObject) {
		m_BooleanOperation.EnableWindow(true);
	}
}


void CBooleanOperationDlg::OnBnClickedButtonBooleanOperation()
{
	//
	//	Here we will create a Box within the Geometry Kernel and fill the buffers
	//
	//		Boolean Operation (2D and 3D) supports the following types:
	//			 0 - Union
	//			 1 - Difference (first object - second object)
	//			 2 - Difference (first object - second object)
	//			 3 - Intersection
	//
	//			 4 - like 0, but only with geometry inherited from second object
	//			 5 - like 1, but only with geometry inherited from second object
	//			 6 - like 2, but only with geometry inherited from second object
	//			 7 - like 3, but only with geometry inherited from second object
	//
	//			 8 - like 0, but only with geometry inherited from first object
	//			 9 - like 1, but only with geometry inherited from first object
	//			10 - like 2, but only with geometry inherited from first object
	//			11 - like 3, but only with geometry inherited from first object
	//
	int64_t	booleanOperationType = 1;

	int64_t	model = CreateModel();
	if (model) {
		//
		//	Get the handles to the classes and properties within the just opened model
		//
		int64_t	classBooleanOperation = GetClassByName(model, "BooleanOperation"),
			classBoundaryRepresentation = GetClassByName(model, "BoundaryRepresentation"),
			propertyIndices = GetPropertyByName(model, "indices"),
			propertyFirstObject = GetPropertyByName(model, "firstObject"),
			propertySecondObject = GetPropertyByName(model, "secondObject"),
			propertyType = GetPropertyByName(model, "type"),
			propertyVertices = GetPropertyByName(model, "vertices");

		//
		//	Create the instance as a very small design tree
		//
		int64_t instanceBooleanOperation = CreateInstance(classBooleanOperation),
			instanceBoundaryRepresentationI = CreateInstance(classBoundaryRepresentation),
			instanceBoundaryRepresentationII = CreateInstance(classBoundaryRepresentation);

		//
		//	Set the properties for Boolean Operation
		//
		SetObjectProperty(instanceBooleanOperation, propertyFirstObject, &instanceBoundaryRepresentationI, 1);
		SetObjectProperty(instanceBooleanOperation, propertySecondObject, &instanceBoundaryRepresentationII, 1);
		SetDatatypeProperty(instanceBooleanOperation, propertyType, &booleanOperationType, 1);

		//
		//	Set the properties for Boundary Representation I
		//
		SetDatatypeProperty(instanceBoundaryRepresentationI, propertyVertices, &vertexBufferFirstObject[0], vertexBufferSizeFirstObject * 3);
		SetDatatypeProperty(instanceBoundaryRepresentationI, propertyIndices, &indexBufferFirstObject[0], indexBufferSizeFirstObject);

		//
		//	Set the properties for Boundary Representation II
		//
		SetDatatypeProperty(instanceBoundaryRepresentationII, propertyVertices, &vertexBufferSecondObject[0], vertexBufferSizeSecondObject * 3);
		SetDatatypeProperty(instanceBoundaryRepresentationII, propertyIndices, &indexBufferSecondObject[0], indexBufferSizeSecondObject);

		//
		//	Arrays should contain polygon representation, only export X, Y, Z values and everything in 64 bit precision
		//
		ExpectationsForBuffers(model);

		//
		//	Of course in this case the boolean operation result is what ineterests us, however using one of the other instances should result in 
		//		being the result exactly eaqual to the input.
		//
		int64_t	selectedInstance = instanceBooleanOperation;
		//		selectedInstance = instanceBoundaryRepresentationI;
		//		selectedInstance = instanceBoundaryRepresentationII;

		//
		//	Create internal Geometry for this minimal design tree
		//
		int64_t	vertexBufferSize = 0, indexBufferSize = 0;
		CalculateInstance(selectedInstance, &vertexBufferSize, &indexBufferSize, nullptr);

		if (vertexBufferSize && indexBufferSize) {
			m_BooleanOperation.EnableWindow(false);

			//
			//	With SetFormat(..) we requested double precision for vertices and 3 components (X, Y, Z) per vertex element 
			//
			double	* vertexBuffer = new double[3 * (int)vertexBufferSize];
			UpdateInstanceVertexBuffer(selectedInstance, vertexBuffer);

			//
			//	With SetFormat(..) we requested 64 bit integer values 
			//
			int64_t	* indexBuffer = new int64_t[(int)indexBufferSize];
			UpdateInstanceIndexBuffer(selectedInstance, indexBuffer);

			//
			//	Now the buffers are ready, we are only interested in the polygons, not in the triangles
			//
			int64_t	conceptualFaceCnt = GetConceptualFaceCnt(selectedInstance), cnt = 0;
			for (int64_t i = 0; i < conceptualFaceCnt; i++) {
				int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
				GetConceptualFace(
						selectedInstance, i,
						nullptr, nullptr,
						nullptr, nullptr,
						nullptr, nullptr,
						&startIndexFacePolygons, &noIndicesFacePolygons,
						nullptr, nullptr
					);

				cnt += noIndicesFacePolygons;
			}


			assert(vertexBufferBooleanOperationResult == nullptr);
			assert(vertexBufferSizeBooleanOperationResult == 0);
			assert(indexBufferBooleanOperationResult == nullptr);
			assert(indexBufferSizeBooleanOperationResult == 0);


			//
			//	Now assign the results to the buffers
			//
			vertexBufferBooleanOperationResult = vertexBuffer;
			vertexBufferSizeBooleanOperationResult = vertexBufferSize;
			indexBufferBooleanOperationResult = new int64_t[(int)cnt];

			conceptualFaceCnt = GetConceptualFaceCnt(selectedInstance);
			for (int64_t i = 0; i < conceptualFaceCnt; i++) {
				int64_t	startIndexFacePolygons = 0, noIndicesFacePolygons = 0;
				GetConceptualFace(
						selectedInstance, i,
						nullptr, nullptr,
						nullptr, nullptr,
						nullptr, nullptr,
						&startIndexFacePolygons, &noIndicesFacePolygons,
						nullptr, nullptr
					);

				memcpy(&indexBufferBooleanOperationResult[(int)indexBufferSizeBooleanOperationResult], &indexBuffer[(int)startIndexFacePolygons], (int)noIndicesFacePolygons * sizeof(int64_t));
				indexBufferSizeBooleanOperationResult += noIndicesFacePolygons;
			}

			assert(indexBufferSizeBooleanOperationResult == cnt);

			delete[]  indexBuffer;

			if (selectedInstance == instanceBoundaryRepresentationI) {
				//
				//	To show input is equal to output
				//
				assert(vertexBufferSizeBooleanOperationResult == vertexBufferSizeFirstObject);
				assert(indexBufferSizeBooleanOperationResult == indexBufferSizeFirstObject);
				for (int64_t i = 0; i < vertexBufferSizeFirstObject * 3; i++) {
					assert(vertexBufferBooleanOperationResult[i] == vertexBufferFirstObject[i]);
				}
				for (int64_t i = 0; i < indexBufferSizeFirstObject; i++) {
					assert(indexBufferBooleanOperationResult[i] == indexBufferFirstObject[i]);
				}
			}

			if (selectedInstance == instanceBoundaryRepresentationII) {
				//
				//	To show input is equal to output
				//
				assert(vertexBufferSizeBooleanOperationResult == vertexBufferSizeSecondObject);
				assert(indexBufferSizeBooleanOperationResult == indexBufferSizeSecondObject);
				for (int64_t i = 0; i < vertexBufferSizeSecondObject * 3; i++) {
					assert(vertexBufferBooleanOperationResult[i] == vertexBufferSecondObject[i]);
				}
				for (int64_t i = 0; i < indexBufferSizeSecondObject; i++) {
					assert(indexBufferBooleanOperationResult[i] == indexBufferSecondObject[i]);
				}
			}
		}
		else {
			vertexBufferBooleanOperationResult = nullptr;
			vertexBufferSizeBooleanOperationResult = 0;
			indexBufferBooleanOperationResult = nullptr;
			indexBufferSizeBooleanOperationResult = 0;
		}

		CloseModel(model);
		model = 0;
	}
}
