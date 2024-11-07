#ifndef _TEXTURE_H_
#define _TEXTURE_H_

#include "jsbuilder.h"

#include <Windows.h>

/**************************************************************************************************
.png/.bmp/etc. resource => base64 content
Add a prefix, e.g. for .png
data:image/png;base64,
*/
string LoadTextureResourceBase64(const wchar_t* szModuleName, int iResourceID)
{
#ifndef _DISABLE_BOOST
	string strBase64Content;

	HMODULE hModule = GetModuleHandle(szModuleName);

	HRSRC hRes = FindResource(hModule, MAKEINTRESOURCE(iResourceID), L"PNG");
	if (NULL != hRes)
	{
		HGLOBAL hData = LoadResource(hModule, hRes);
		if (NULL != hData)
		{
			DWORD dwDataSize = SizeofResource(hModule, hRes);
			char * data = (char *)LockResource(hData);

			std::string strContent;
			strContent.assign(data, dwDataSize);

			std::stringstream streamBase64;
			std::copy(base64_text(strContent.c_str()), base64_text(strContent.c_str() + strContent.size()), ostream_iterator<char>(streamBase64));

			strBase64Content = streamBase64.str();
		}
	}	

	return strBase64Content;
#else
	// TODO
	assert(false);

	return "";
#endif // _DISABLE_BOOST
}

/**************************************************************************************************
.png/.bmp/etc. file => base64 content
Add a prefix, e.g. for .png
data:image/png;base64,
*/
string LoadTextureFileBase64(const wchar_t* szFileName)
{
#ifndef _DISABLE_BOOST
	ifstream textureStream(szFileName, ios_base::in | ios_base::binary);

	std::stringstream intputStream;
	boost::iostreams::copy(textureStream, intputStream);

	std::string strContent = intputStream.str();

	std::stringstream streamBase64;
	std::copy(base64_text(strContent.c_str()), base64_text(strContent.c_str() + strContent.size()), ostream_iterator<char>(streamBase64));

	string strBase64Content = streamBase64.str();

	return strBase64Content;
#else
	// TODO
	assert(false);

	return "";
#endif // _DISABLE_BOOST
}

#endif // _TEXTURE_H_