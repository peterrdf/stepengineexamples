// stp2bin.h : main header file for the stp2bin DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


void	setQuality(
				double		factor,
				bool		prepareBooleanOperations,
				bool		prepareBoundaryRepresentations
			);

void	STP2BIN(
				wchar_t		* inputFileName,
				wchar_t		* outputFileName
			);
