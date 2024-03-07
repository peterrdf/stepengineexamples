package com.android.gl2jni;

public class IFCEngineLib {
    public static final long sdaiADB = 				1;
    public static final long sdaiAGGR =				sdaiADB + 1;
    public static final long sdaiBINARY =			sdaiAGGR + 1;
    public static final long sdaiBOOLEAN =			sdaiBINARY + 1;
    public static final long sdaiENUM =				sdaiBOOLEAN + 1;
    public static final long sdaiINSTANCE =			sdaiENUM + 1;
    public static final long sdaiINTEGER =			sdaiINSTANCE + 1;
    public static final long sdaiLOGICAL =			sdaiINTEGER + 1;
    public static final long sdaiREAL =				sdaiLOGICAL + 1;
    public static final long sdaiSTRING =			sdaiREAL + 1;
    public static final long sdaiUNICODE =			sdaiSTRING + 1;
    public static final long sdaiEXPRESSSTRING =	sdaiUNICODE + 1;
    public static final long engiGLOBALID =			sdaiEXPRESSSTRING + 1;

    static {
        System.loadLibrary("gl2jni");
    }

    public static native long sdaiOpenModelBN(String ifcFile, String schemaFile);
    public static native void sdaiCloseModel(long model);
    public static native long sdaiGetEntity(long model, String entityName);
    public static native long sdaiGetInstanceType(long instance);
    public static native long sdaiGetEntityExtentBN(long model, String entityName);
    public static native long sdaiGetEntityExtent(long model, long entity);
    public static native long sdaiGetMemberCount(long aggregate);
    public static native long engiGetAggrElement(long aggregate, long elementIndex, long valueType);
    public static native String sdaiGetAttrBNStr(long instance, String attributeName);
    public static native String sdaiGetADBTypePath(String ADB, long typeNameNumber);
    public static native long sdaiGetAttrBNLong(long instance, String attributeName, long valueType);
    public static native String engiGetEntityName(long entity);
    public static native long engiGetEntityCount(long model);
    public static native long engiGetEntityElement(long model, long index);
    public static native long engiGetEntityParent(long entity);
    public static native void CalculateInstance(long instance, IFCObject ifcObject);
    public static native void setFormat(long model, long settings, long mask);
    public static native void setFilter(long model, long settings, long mask);
    public static native void circleSegments(long circles, long smallCircles);
    public static native void cleanMemory(long model, long mode);
    public static native long owlGetInstance(long model, long instance);
    public static native float[] UpdateInstanceVertexBuffer(long owlInstance, long size);
    public static native int[] UpdateInstanceIndexBuffer(long owlInstance, long size);
    public static native String GetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex);
    public static native long getConceptualFaceCnt(long instance);
    public static native long getConceptualFaceEx(long instance, long index, ConceptualFace conceptualFace);
    public static native long owlGetModel(long model);
    public static native long retrieveColor(float color);
    public static native float retrieveColorR(long color);
    public static native float retrieveColorG(long color);
    public static native float retrieveColorB(long color);
    public static native float retrieveColorA(long color);
}
