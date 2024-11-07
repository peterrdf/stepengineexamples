// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the BIN2JS_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// BIN2JS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef BIN2JS_EXPORTS
#define BIN2JS_API __declspec(dllexport)
#else
#define BIN2JS_API __declspec(dllimport)
#endif

BIN2JS_API void generateJS(const wchar_t* szBINFile, const wchar_t* szJSFile, bool bMainJS = true, const wchar_t* szTag = NULL, const wchar_t* szExtraJS = NULL);