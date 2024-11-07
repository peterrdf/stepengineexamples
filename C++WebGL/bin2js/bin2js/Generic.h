#pragma once

#include "stdafx.h"

#include <stdint.h>
#include "engine.h"

#include <vector>
#include <map>
#include <limits>

using namespace std;

#ifdef _LINUX
#include <wx/wx.h>

typedef int BOOL;
typedef wxPoint CPoint;
typedef wxRect CRect;
typedef unsigned int UINT;
#endif // _LINUX


// ----------------------------------------------------------------------------
/*
// X, Y, Z, Nx, Ny, Nz, Tx, Ty, Ambient, Diffuse, Emissive, Specular
*/
#define VERTEX_LENGTH 12

#define FACES_VERTEX_LENGTH  6
#define WIREFRAMES_VERTEX_LENGTH 3
#define DEFAULT_CIRCLE_SEGMENTS 36

// ------------------------------------------------------------------------------------------------
#define MAX_INDICES_COUNT 60000
