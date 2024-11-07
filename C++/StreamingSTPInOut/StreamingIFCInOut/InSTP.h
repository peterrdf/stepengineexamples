#pragma once


#include "./stepengine/include/stepengine.h"


const	uint_t	flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
const	uint_t	flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
const	uint_t	flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000


int_t	OpenModelByStream(wchar_t * fileName);
