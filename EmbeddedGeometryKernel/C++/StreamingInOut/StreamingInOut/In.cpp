
#include "stdafx.h"
#include "In.h"

#include <assert.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


const int_t	BLOCK_LENGTH_READ = 20000; // MAX: 65535

FILE		* myFileRead = nullptr;


int_t	__stdcall	ReadCallBackFunction(unsigned char * content)
{
	if (myFileRead == nullptr || feof(myFileRead)) {
		return	-1;
	}

	int_t	size = fread(content, 1, BLOCK_LENGTH_READ, myFileRead);

	return	size;
}

int64_t	OpenModelByStream(wchar_t * fileName)
{
	assert(myFileRead == nullptr);

	_wfopen_s(&myFileRead, fileName, L"rb");
	if (&myFileRead) {
		int64_t	model = OpenModelS(&ReadCallBackFunction);

		fclose(myFileRead);
		myFileRead = nullptr;

		return	model;
	}

	assert(false);
	return	0;
}
