
#include "stdafx.h"
#include "Out.h"

#include <assert.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


const int_t	BLOCK_LENGTH_WRITE = 20000;	//	no maximum limit

FILE		* myFileWrite = nullptr;


void	__stdcall	WriteCallBackFunction(unsigned char * content, int64_t size)
{
	fwrite(content, (size_t) size, 1, myFileWrite);
}

void	SaveModelByStream(int64_t model, wchar_t * fileName)
{
	assert(myFileWrite == nullptr);

	_wfopen_s(&myFileWrite, fileName, L"wb");
	if (&myFileWrite) {
		SaveModelS(model, &WriteCallBackFunction, BLOCK_LENGTH_WRITE);

		fclose(myFileWrite);
	}
	else {
		assert(false);
	}
}
