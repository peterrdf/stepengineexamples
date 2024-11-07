
#include "stdafx.h"
#include "InSTP.h"

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

bool	contains(char * txtI, char * txtII)
{
	int_t	i = 0;

	while (txtI[i] && txtII[i]) {
		if (txtI[i] != txtII[i]) {
			return	false;
		}
		i++;
	}

	if (txtII[i]) {
		return	false;
	}
	else {
		return	true;
	}
}

int_t	OpenModelByStream(wchar_t * fileName)
{
	assert(myFileRead == nullptr);

	//
	//	We start with recognizing the schema to be loaded
	//	As the schema argument is zero only the header will be loaded
	//
	_wfopen_s(&myFileRead, fileName, L"rb");
	if (myFileRead) {
		int_t	ifcModel = engiOpenModelByStream(0, &ReadCallBackFunction, (const char*) nullptr);

		char	* fileSchema = nullptr;
		GetSPFFHeaderItem(ifcModel, 9, 0, sdaiSTRING, &fileSchema);
		if (fileSchema == 0 ||
			contains(fileSchema, "AUTOMOTIVE_DESIGN")) {
			sdaiCloseModel(ifcModel);

			//
			//  http://rdf.bg/downloads/setFilter.pdf			//
			//	override schema loading from file with internal AP242 schema when embedded in the DLL
			//
			setFilter(0, flagbit22, flagbit20 + flagbit21 + flagbit22);
		}
		else {
			sdaiCloseModel(ifcModel);
			fclose(myFileRead);

			return	0;
		}

		fclose(myFileRead);
		myFileRead = nullptr;

		//
		//	Now we know and set the embedded schema we can load the IFC file as a stream
		//
		_wfopen_s(&myFileRead, fileName, L"rb");
		ifcModel = engiOpenModelByStream(0, &ReadCallBackFunction, (char*) 1);

		fclose(myFileRead);
		myFileRead = nullptr;

		return	ifcModel;
	}

	assert(false);
	return	0;
}
