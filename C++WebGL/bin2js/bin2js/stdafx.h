// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>



// TODO: reference additional headers your program requires here

#include <atlbase.h>
#include <atlconv.h>
#include <assert.h>
#include <iostream>

using namespace std;

#ifdef _DEBUG
#define LOG_DEBUG(msg) cout << "\n" << msg
#else
#define LOG_DEBUG(msg)
#endif 
