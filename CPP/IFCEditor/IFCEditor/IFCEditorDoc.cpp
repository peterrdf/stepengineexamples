
// IFCEditorDoc.cpp : implementation of the CIFCEditorDoc class
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "IFCEditor.h"
#endif

#include "IFCEditorDoc.h"

#include <propkey.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CIFCEditorDoc

IMPLEMENT_DYNCREATE(CIFCEditorDoc, CDocument)

BEGIN_MESSAGE_MAP(CIFCEditorDoc, CDocument)
	ON_COMMAND(ID_FILE_OPEN, &CIFCEditorDoc::OnFileOpen)
END_MESSAGE_MAP()


// CIFCEditorDoc construction/destruction

CIFCEditorDoc::CIFCEditorDoc() noexcept
{
	// TODO: add one-time construction code here

}

CIFCEditorDoc::~CIFCEditorDoc()
{
	delete m_pModel;
}

BOOL CIFCEditorDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	if (m_pModel != nullptr)
	{
		delete m_pModel;
		m_pModel = nullptr;
	}

	m_pModel = new CIFCModel(true);

	SetModel(m_pModel);

	return TRUE;
}




// CIFCEditorDoc serialization

void CIFCEditorDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

#ifdef SHARED_HANDLERS

// Support for thumbnails
void CIFCEditorDoc::OnDrawThumbnail(CDC& dc, LPRECT lprcBounds)
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
void CIFCEditorDoc::InitializeSearchContent()
{
	CString strSearchContent;
	// Set search contents from document's data.
	// The content parts should be separated by ";"

	// For example:  strSearchContent = _T("point;rectangle;circle;ole object;");
	SetSearchContent(strSearchContent);
}

void CIFCEditorDoc::SetSearchContent(const CString& value)
{
	if (value.IsEmpty())
	{
		RemoveChunk(PKEY_Search_Contents.fmtid, PKEY_Search_Contents.pid);
	}
	else
	{
		CMFCFilterChunkValueImpl *pChunk = nullptr;
		ATLTRY(pChunk = new CMFCFilterChunkValueImpl);
		if (pChunk != nullptr)
		{
			pChunk->SetTextValue(PKEY_Search_Contents, value, CHUNK_TEXT);
			SetChunkValue(pChunk);
		}
	}
}

#endif // SHARED_HANDLERS

// CIFCEditorDoc diagnostics

#ifdef _DEBUG
void CIFCEditorDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CIFCEditorDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CIFCEditorDoc commands


BOOL CIFCEditorDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	if (!CDocument::OnOpenDocument(lpszPathName))
		return FALSE;

	if (m_pModel != nullptr)
	{
		delete m_pModel;
		m_pModel = nullptr;
	}

	auto iModel = sdaiOpenModelBNUnicode(0, lpszPathName, L"");
	if (iModel == 0)
	{
		MessageBox(::AfxGetMainWnd()->GetSafeHwnd(), L"Failed to open the model.", L"Error", MB_ICONERROR | MB_OK);

		return FALSE;
	}

	wchar_t* fileSchema = 0;
	GetSPFFHeaderItem(iModel, 9, 0, sdaiUNICODE, (char**)&fileSchema);

	if (fileSchema == nullptr)
	{
		MessageBox(::AfxGetMainWnd()->GetSafeHwnd(), L"Unknown file schema.", L"Error", MB_ICONERROR | MB_OK);

		return FALSE;
	}

	CString strFileSchema = fileSchema;
	strFileSchema.MakeUpper();

	if (strFileSchema.Find(L"IFC") != 0)
	{
		MessageBox(::AfxGetMainWnd()->GetSafeHwnd(), L"Unknown file schema.", L"Error", MB_ICONERROR | MB_OK);

		return FALSE;
	}

	auto pModel = new CIFCModel(true);
	pModel->Load(lpszPathName, iModel);

	m_pModel = pModel;

	SetModel(m_pModel);

	// Title
	CString strTitle = AfxGetAppName();
	strTitle += L" - ";
	strTitle += lpszPathName;

	AfxGetMainWnd()->SetWindowTextW(strTitle);

	// MRU
	AfxGetApp()->AddToRecentFileList(lpszPathName);

	return TRUE;
}

void CIFCEditorDoc::OnFileOpen()
{
	TCHAR szFilters[] = _T("IFC Files (*.ifc)|*.ifc|All Files (*.*)|*.*||");
	CFileDialog dlgFile(TRUE, nullptr, _T(""), OFN_OVERWRITEPROMPT | OFN_HIDEREADONLY, szFilters);

	if (dlgFile.DoModal() != IDOK)
	{
		return;
	}

	OnOpenDocument(dlgFile.GetPathName());
}
