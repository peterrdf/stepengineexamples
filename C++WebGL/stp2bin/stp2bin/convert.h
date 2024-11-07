// stp2bin.h : main header file for the stp2bin DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "stepengine/include/engine.h"
#include "stepengine/include/stepengine.h"



/*void		GetProductDefinitionInstances(
					int_t	stepModel
				);

void		GetProductDefinitionShapeInstances(
					int_t	stepModel
				);

int64_t		BuildParts(
					int64_t		rdfModel
				);

int64_t		BuildAssemblies(
					int64_t		rdfModel
				);	*/

int64_t		BuildProductDefinition(
					int_t		stepModel
				);
