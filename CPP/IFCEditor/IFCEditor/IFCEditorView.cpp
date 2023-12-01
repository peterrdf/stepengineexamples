
// IFCEditorView.cpp : implementation of the CIFCEditorView class
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "IFCEditor.h"
#endif

#include "IFCEditorDoc.h"
#include "IFCEditorView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CIFCEditorView

IMPLEMENT_DYNCREATE(CIFCEditorView, CView)

BEGIN_MESSAGE_MAP(CIFCEditorView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CIFCEditorView::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
END_MESSAGE_MAP()

// CIFCEditorView construction/destruction

CIFCEditorView::CIFCEditorView() noexcept
{
	// TODO: add construction code here

}

CIFCEditorView::~CIFCEditorView()
{
}

BOOL CIFCEditorView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CIFCEditorView drawing

void CIFCEditorView::OnDraw(CDC* /*pDC*/)
{
	CIFCEditorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}


// CIFCEditorView printing


void CIFCEditorView::OnFilePrintPreview()
{
#ifndef SHARED_HANDLERS
	AFXPrintPreview(this);
#endif
}

BOOL CIFCEditorView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CIFCEditorView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CIFCEditorView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void CIFCEditorView::OnRButtonUp(UINT /* nFlags */, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void CIFCEditorView::OnContextMenu(CWnd* /* pWnd */, CPoint point)
{
#ifndef SHARED_HANDLERS
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
#endif
}


// CIFCEditorView diagnostics

#ifdef _DEBUG
void CIFCEditorView::AssertValid() const
{
	CView::AssertValid();
}

void CIFCEditorView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CIFCEditorDoc* CIFCEditorView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CIFCEditorDoc)));
	return (CIFCEditorDoc*)m_pDocument;
}
#endif //_DEBUG


// CIFCEditorView message handlers
