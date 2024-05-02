#pragma once

#include <stdint.h>
#include "./include/engine.h"

#include <assert.h>

#include <string>
#include <bitset>
#include <vector>
#include <map>

using namespace std;

#include "./include/ft2build.h"
#include FT_FREETYPE_H
#include FT_OUTLINE_H

// ------------------------------------------------------------------------------------------------
// Collection <= Line3D, BezierCurve
#define LINES_AND_CURVES 0

// ------------------------------------------------------------------------------------------------
// Face2DSet <= Polygon3D <= Line3D, BezierCurve
#define FACE2D_SET 1

// ------------------------------------------------------------------------------------------------
// ExtrusionAreaSolidSet <= Polygon3D <= Line3D, BezierCurve
#define EXTRSUSION_AREA_SOLID_SET 2

// ------------------------------------------------------------------------------------------------
#define DOUBLE_TO_16_16(d) ((FT_Fixed)((d) * 65536.0))
#define DOUBLE_FROM_26_6(t) ((double)(t) / 64.0)

// ------------------------------------------------------------------------------------------------
class CText2BIN
{

private: // Classes

	// --------------------------------------------------------------------------------------------
	// Describes a contour
	class CGlyphContour
	{

	public: // Members

		// ----------------------------------------------------------------------------------------
		// Offset - X
		int m_iOffsetX;

		// ----------------------------------------------------------------------------------------
		// Offset - Y
		int m_iOffsetY;

		// ----------------------------------------------------------------------------------------
		// Line3D and/or BezierCurve
		vector<int64_t> m_vecInstances;

		// ----------------------------------------------------------------------------------------
		// Collection, ExtrusionAreaSolidSet
		int64_t m_iGeometryInstance;

	public: // Methods

		// ----------------------------------------------------------------------------------------
		// ctor
		CGlyphContour()
			: m_iOffsetX(0)
			, m_iOffsetY(0)
			, m_vecInstances()
			, m_iGeometryInstance(0)
		{
		}
	};

private: // Members

	// --------------------------------------------------------------------------------------------
	// Model
	int64_t m_iModel;

	// --------------------------------------------------------------------------------------------
	// Line3D
	int64_t m_iLine3DClass;

	// --------------------------------------------------------------------------------------------
	// Point3D
	int64_t m_iPoint3DClass;

	// --------------------------------------------------------------------------------------------
	// BezierCurve
	int64_t m_iBezierCurveClass;

	// --------------------------------------------------------------------------------------------
	// Collection
	int64_t m_iCollectionClass;

	// --------------------------------------------------------------------------------------------
	// Polygon3D
	int64_t m_iPolygon3DClass;

	// --------------------------------------------------------------------------------------------
	// Face2DSet
	int64_t m_iFace2DSetClass;

	// --------------------------------------------------------------------------------------------
	// ExtrusionAreaSolidSet
	int64_t m_iExtrusionAreaSolidSetClass;

	// --------------------------------------------------------------------------------------------
	// Transformation
	int64_t m_iTransformationClass;

	// --------------------------------------------------------------------------------------------
	// Matrix
	int64_t m_iMatrixClass;

	// --------------------------------------------------------------------------------------------
	// One entry per contour; temp var
	vector<CGlyphContour *> m_vecContours;

	// --------------------------------------------------------------------------------------------
	// Letter : Contours
	map<wchar_t, vector<CGlyphContour *>> m_mapLetters;

	// --------------------------------------------------------------------------------------------
	// Temp var
	CGlyphContour * m_pCurrentContour;

	// --------------------------------------------------------------------------------------------
	// X; temp var
	int m_iX;

	// --------------------------------------------------------------------------------------------
	// Y; temp var
	int m_iY;

	// --------------------------------------------------------------------------------------------
	// Offset - X
	int m_iOffsetX;

	// --------------------------------------------------------------------------------------------
	// Offset - Y
	int m_iOffsetY;

	// --------------------------------------------------------------------------------------------
	// Text
	CString m_strText;

	// --------------------------------------------------------------------------------------------
	// Font
	CString m_strTTFFile;

	// --------------------------------------------------------------------------------------------
	// Output
	CString m_strBINFile;

	// --------------------------------------------------------------------------------------------
	// Geometry
	int m_iGeometry;

	// Transformation
	bool m_bCenter;

public: // Methods

	// --------------------------------------------------------------------------------------------
	// ctor
	CText2BIN(const CString & strText, const CString & strTTFFile, const CString & strBINFile, int iGeometry, bool bCenter = true);

	// --------------------------------------------------------------------------------------------
	// dtor
	virtual ~CText2BIN();

	OwlInstance Translate(OwlInstance iInstance,
		double dX, double dY, double dZ);

	// --------------------------------------------------------------------------------------------
	// Convert
	void Run();

private: // Methods

	// --------------------------------------------------------------------------------------------
	// Format
	void SetFormatSettings(__int64 iModel);

	// --------------------------------------------------------------------------------------------
	// Callback
	static int move_to(const FT_Vector *to, void *user);

	// --------------------------------------------------------------------------------------------
	// Callback
	static int line_to(const FT_Vector *to, void *user);

	// --------------------------------------------------------------------------------------------
	// Callback
	static int conic_to(const FT_Vector *control, const FT_Vector *to, void *user);

	// --------------------------------------------------------------------------------------------
	// Callback
	static int cubic_to(const FT_Vector *control1, const FT_Vector *control2, const FT_Vector *to, void *user);
};

