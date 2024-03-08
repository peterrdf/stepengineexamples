
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
#include "OpenGLIFCView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// ------------------------------------------------------------------------------------------------
CController* CIFCEditorView::GetController()
{
	auto pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	return pDoc;
}

// ------------------------------------------------------------------------------------------------
/*virtual*/ void CIFCEditorView::OnModelChanged()
{
	delete m_pOpenGLView;
	m_pOpenGLView = nullptr;

	auto pController = GetController();
	if (pController == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	if (pController->GetModel() == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	auto pModel = pController->GetModel();
	if (pModel == nullptr)
	{
		ASSERT(FALSE);

		return;
	}

	switch (pModel->GetType())
	{
		case enumModelType::IFC:
		{
			m_pOpenGLView = new COpenGLIFCView(this);
			m_pOpenGLView->SetController(pController);
			m_pOpenGLView->Load();
		}
		break;


		default:
		{
			ASSERT(FALSE); // Unknown
		}
		break;
	}
}


// CIFCEditorView

IMPLEMENT_DYNCREATE(CIFCEditorView, CView)

BEGIN_MESSAGE_MAP(CIFCEditorView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CIFCEditorView::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
	ON_WM_CREATE()
	ON_WM_DESTROY()
	ON_WM_ERASEBKGND()
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_MBUTTONDOWN()
	ON_WM_MBUTTONUP()
	ON_WM_RBUTTONDOWN()
	ON_WM_MOUSEMOVE()
	ON_WM_MOUSEWHEEL()
	ON_WM_DROPFILES()
	ON_WM_KEYUP()
END_MESSAGE_MAP()

// CIFCEditorView construction/destruction

CIFCEditorView::CIFCEditorView() noexcept
	: m_pOpenGLView(nullptr)
{
}

CIFCEditorView::~CIFCEditorView()
{
}

BOOL CIFCEditorView::PreCreateWindow(CREATESTRUCT& cs)
{
	cs.style &= ~CS_PARENTDC;
	cs.style |= CS_OWNDC;
	cs.style |= (WS_CLIPCHILDREN | WS_CLIPSIBLINGS);

	return CView::PreCreateWindow(cs);
}

// CIFCEditorView drawing

void CIFCEditorView::OnDraw(CDC* pDC)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->Draw(pDC);
	}
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

int CIFCEditorView::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CView::OnCreate(lpCreateStruct) == -1)
	{
		return -1;
	}		

	DragAcceptFiles(TRUE);

	auto pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	if (!pDoc)
	{
		return -1;
	}	

	pDoc->RegisterView(this);

	return 0;
}

void CIFCEditorView::OnDestroy()
{
	auto pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	if (!pDoc)
	{
		return;
	}

	pDoc->UnRegisterView(this);

	delete m_pOpenGLView;
	m_pOpenGLView = nullptr;

	CView::OnDestroy();
}

BOOL CIFCEditorView::OnEraseBkgnd(CDC* pDC)
{
	return TRUE;
}

void CIFCEditorView::OnLButtonDown(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::LBtnDown, nFlags, point);
	}

	CView::OnLButtonDown(nFlags, point);
}

void CIFCEditorView::OnLButtonUp(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::LBtnUp, nFlags, point);
	}

	CView::OnLButtonUp(nFlags, point);
}

void CIFCEditorView::OnMButtonDown(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::MBtnDown, nFlags, point);
	}

	CView::OnMButtonDown(nFlags, point);
}

void CIFCEditorView::OnMButtonUp(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::MBtnUp, nFlags, point);
	}

	CView::OnMButtonUp(nFlags, point);
}

void CIFCEditorView::OnRButtonDown(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::RBtnDown, nFlags, point);
	}

	CView::OnRButtonDown(nFlags, point);
}

void CIFCEditorView::OnRButtonUp(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::RBtnUp, nFlags, point);
	}

	//ClientToScreen(&point);
	//OnContextMenu(this, point);
}

void CIFCEditorView::OnMouseMove(UINT nFlags, CPoint point)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseEvent(enumMouseEvent::Move, nFlags, point);
	}

	CView::OnMouseMove(nFlags, point);
}

BOOL CIFCEditorView::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnMouseWheel(nFlags, zDelta, pt);
	}

	return CView::OnMouseWheel(nFlags, zDelta, pt);
}

void CIFCEditorView::OnDropFiles(HDROP hDropInfo)
{
	// Get the number of files dropped 
	int iFilesDropped = DragQueryFile(hDropInfo, 0xFFFFFFFF, nullptr, 0);
	if (iFilesDropped != 1)
	{
		return;
	}

	// Get the buffer size of the file.
	DWORD dwBuffer = DragQueryFile(hDropInfo, 0, nullptr, 0);

	// Get path and name of the file 
	CString strFile;
	DragQueryFile(hDropInfo, 0, strFile.GetBuffer(dwBuffer + 1), dwBuffer + 1);

	// Open
	auto pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	pDoc->OnOpenDocument(strFile);

	strFile.ReleaseBuffer();

	// Free the memory block containing the dropped-file information 
	DragFinish(hDropInfo);
}

void CIFCEditorView::OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	if (m_pOpenGLView != nullptr)
	{
		m_pOpenGLView->OnKeyUp(nChar, nRepCnt, nFlags);
	}
}
