
#include "stdafx.h"
#include "Out.h"

#include <assert.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


void		SaveModelByArray(int64_t model, wchar_t * fileName)
{
	FILE		* myFileWrite = nullptr;

	_wfopen_s(&myFileWrite, fileName, L"wb");
	if (&myFileWrite) {
		int64_t	length = 0;

		SaveModelA(model, 0, &length);

		unsigned char	* content = new unsigned char[(int_t) length];
		SaveModelA(model, content, &length);

		fwrite(content, 1, (size_t) length, myFileWrite);
		delete[] content;

		fclose(myFileWrite);
	}
	else {
		assert(false);
	}
}
