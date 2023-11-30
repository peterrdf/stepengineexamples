
// ifceditorDoc.cpp : implementation of the CifceditorDoc class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "ifceditor.h"
#endif

#include "ifceditorDoc.h"
#include "ifcengine\include\ifcengine.h"


#include <propkey.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif



extern string_t schemaName_STRUCTURAL_FRAME_SCHEMA;
extern string_t ifcSchemaName_IFC2x3;
extern string_t ifcSchemaName_IFC4;
extern string_t ifcSchemaName_IFC4x1;


bool	isIFC2x3 = false;

extern	int_t	model;
wchar_t			fileName[512];


// CifceditorDoc

IMPLEMENT_DYNCREATE(CifceditorDoc, CDocument)

BEGIN_MESSAGE_MAP(CifceditorDoc, CDocument)
END_MESSAGE_MAP()


// CifceditorDoc construction/destruction

CifceditorDoc::CifceditorDoc()
{
	// TODO: add one-time construction code here

}

CifceditorDoc::~CifceditorDoc()
{
}

BOOL CifceditorDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



bool	contains(const char * txtI, const char * txtII)
{
	size_t	i = 0;
	while (txtI[i]  &&  txtII[i]) {
		if (txtI[i] != txtII[i])
			return	false;
		i++;
	}
	if (txtII[i])
		return	false;
	else
		return	true;
}

// CifceditorDoc serialization

void CifceditorDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		CString	csFileName = ar.m_strFileName;
		wchar_t	* ws = csFileName.GetBuffer(0);

		sdaiSaveModelBNUnicode(model, ws);
	}
	else {
		memcpy(fileName, ar.m_strFileName, ar.m_strFileName.GetLength() * sizeof(wchar_t));
		fileName[ar.m_strFileName.GetLength()] = 0;

		if (model) {
			sdaiCloseModel(model);
		}

		setStringUnicode(true);
		model = sdaiOpenModelBNUnicode(0, (const wchar_t*) fileName, L"");

		isIFC2x3 = false;

		char	* fileSchema = 0;
		GetSPFFHeaderItem(model, 9, 0, sdaiSTRING, (char**) &fileSchema);
		if (fileSchema == 0 || contains((const char*) fileSchema, "IFC2x3") || contains((const char*) fileSchema, "IFC2X3") || contains((const char*) fileSchema, "IFC2x2") || contains((const char*) fileSchema, "IFC2X2") || contains((const char*) fileSchema, "IFC2x_") || contains((const char*) fileSchema, "IFC2X_") || contains((const char*) fileSchema, "IFC20")) {
			isIFC2x3 = true;
		}
	}
}

#ifdef SHARED_HANDLERS

// Support for thumbnails
void CifceditorDoc::OnDrawThumbnail(CDC& dc, LPRECT lprcBounds)
{
	// Modify this code to draw the document's data
	dc.FillSolidRect(lprcBounds, RGB(255, 255, 255));

	CString strText = _T("TODO: implement thumbnail drawing here");
	LOGFONT lf;

	CFont* pDefaultGUIFont = CFont::FromHandle((HFONT) GetStockObject(DEFAULT_GUI_FONT));
	pDefaultGUIFont->GetLogFont(&lf);
	lf.lfHeight = 36;

	CFont fontDraw;
	fontDraw.CreateFontIndirect(&lf);

	CFont* pOldFont = dc.SelectObject(&fontDraw);
	dc.DrawText(strText, lprcBounds, DT_CENTER | DT_WORDBREAK);
	dc.SelectObject(pOldFont);
}

// Support for Search Handlers
void CifceditorDoc::InitializeSearchContent()
{
	CString strSearchContent;
	// Set search contents from document's data. 
	// The content parts should be separated by ";"

	// For example:  strSearchContent = _T("point;rectangle;circle;ole object;");
	SetSearchContent(strSearchContent);
}

void CifceditorDoc::SetSearchContent(const CString& value)
{
	if (value.IsEmpty())
	{
		RemoveChunk(PKEY_Search_Contents.fmtid, PKEY_Search_Contents.pid);
	}
	else
	{
		CMFCFilterChunkValueImpl *pChunk = NULL;
		ATLTRY(pChunk = new CMFCFilterChunkValueImpl);
		if (pChunk != NULL)
		{
			pChunk->SetTextValue(PKEY_Search_Contents, value, CHUNK_TEXT);
			SetChunkValue(pChunk);
		}
	}
}

#endif // SHARED_HANDLERS

// CifceditorDoc diagnostics

#ifdef _DEBUG
void CifceditorDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CifceditorDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CifceditorDoc commands


BOOL CifceditorDoc::OnSaveDocument(LPCTSTR lpszPathName)
{
	CString	csFileName = lpszPathName;
	wchar_t	* ws = csFileName.GetBuffer(0);
	int		len = (int) wcslen(ws);
	if (model) {
		if (((len >= 4) && (ws[len - 4] == '.') && (ws[len - 3] == 'i') && (ws[len - 2] == 'f') && (ws[len - 1] == 'x')) ||
			((len >= 3) && (ws[len - 3] == 'x') && (ws[len - 2] == 'm') && (ws[len - 1] == 'l'))						 ||
			((len >= 3) && (ws[len - 3] == 'X') && (ws[len - 2] == 'M') && (ws[len - 1] == 'L'))) {
			if (isIFC2x3) {
				sdaiSaveModelAsXmlBNUnicode(model, ws);
			}
			else {
				sdaiSaveModelAsSimpleXmlBNUnicode(model, ws);
			}
		}
		else {
			sdaiSaveModelBNUnicode(model, ws);
		}
	}

	return	true;
}
