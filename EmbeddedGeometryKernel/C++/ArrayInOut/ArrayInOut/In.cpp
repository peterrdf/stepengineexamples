
#include "stdafx.h"
#include "In.h"

#include <assert.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


int64_t	OpenModelByArray(wchar_t * fileName)
{
	FILE	* myFileRead = nullptr;

	_wfopen_s(&myFileRead, fileName, L"rb");
	if (&myFileRead) {
		fseek(myFileRead, 0L, SEEK_END);
		int_t	size = ftell(myFileRead);
		rewind(myFileRead);

		int64_t	model;
		if (size) {
			unsigned char	* content = new unsigned char[size];
			size = fread(content, 1, size, myFileRead);

			model = OpenModelA(content, (int64_t) size);
			delete[] content;
		}
		else {
			assert(false);

			model = OpenModelA(0, 0);
		}

		fclose(myFileRead);
		myFileRead = nullptr;

		return	model;
	}

	assert(false);
	return	0;
}
