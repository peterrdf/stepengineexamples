/*
 * Copyright (C) 2009 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// OpenGL ES 2.0 code

#include "common.h"

#include <GLES2/gl2.h>
#include <GLES2/gl2ext.h>

#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string>

#include "engdef.h"
#include "engine.h"
#include "ifcengine.h"
#include "IFCModel.h"

/**
 * jstring to STL string
 * @param env
 * @param jString
 * @return
 */
std::string j2STLString(JNIEnv * env, jstring jString)
{
    const jsize length = env->GetStringUTFLength(jString);
    const char* szSTLString = env->GetStringUTFChars(jString, (jboolean *)0);

    std::string strSTLString = std::string(szSTLString, length);

    env->ReleaseStringUTFChars(jString, szSTLString);

    return strSTLString;
}

extern "C" {
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiOpenModelBN(JNIEnv * env, jobject obj, jstring ifcFile, jstring schemaFile);
    JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiCloseModel(JNIEnv * env, jobject obj, int64_t model);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetEntity(JNIEnv * env, jobject obj, int64_t model, jstring entityName);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetInstanceType(JNIEnv * env, jobject obj, int64_t instance);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetEntityExtentBN(JNIEnv * env, jobject obj, int64_t model, jstring entityName);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetEntityExtent(JNIEnv * env, jobject obj, int64_t model, int64_t entity);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetMemberCount(JNIEnv * env, jobject obj, int64_t aggregate);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetAggrElement(JNIEnv * env, jobject obj, int64_t aggregate, int64_t elementIndex, int64_t valueType);
    JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetAttrBNStr(JNIEnv * env, jobject obj, int64_t instance, jstring attributeName);
    JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetADBTypePath(JNIEnv * env, jobject obj, jstring ADB, int64_t typeNameNumber);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetAttrBNLong(JNIEnv * env, jobject obj, int64_t instance, jstring attributeName, int64_t valueType);
    JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityName(JNIEnv * env, jobject obj, int64_t entity);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityCount(JNIEnv * env, jobject obj, int64_t model);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityElement(JNIEnv * env, jobject obj, int64_t model, int64_t index);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityParent(JNIEnv * env, jobject obj, int64_t entity);
    JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_CalculateInstance(JNIEnv * env, jobject obj, int64_t instance, jobject ifcObject);
    JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_setFormat(JNIEnv * env, jobject obj, int64_t model, int64_t settings, int64_t mask);
    JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_setFilter(JNIEnv * env, jobject obj, int64_t model, int64_t settings, int64_t mask);
    JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_circleSegments(JNIEnv * env, jobject obj, int64_t circles, int64_t smallCircle);
    JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_cleanMemory(JNIEnv * env, jobject obj, int64_t model, int64_t mode);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_owlGetInstance(JNIEnv * env, jobject obj, int64_t model, int64_t instance);
    JNIEXPORT jfloatArray JNICALL Java_com_android_gl2jni_IFCEngineLib_UpdateInstanceVertexBuffer(JNIEnv * env, jobject obj, int64_t owlInstance, int64_t size);
    JNIEXPORT jintArray JNICALL Java_com_android_gl2jni_IFCEngineLib_UpdateInstanceIndexBuffer(JNIEnv * env, jobject obj, int64_t owlInstance, int64_t size);
    JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_GetSPFFHeaderItem(JNIEnv * env, jobject obj, int64_t model, int64_t itemIndex, int64_t itemSubIndex);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_getConceptualFaceCnt(JNIEnv * env, jobject obj, int64_t instance);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_getConceptualFaceEx(JNIEnv * env, jobject obj, int64_t instance, int64_t index, jobject conceptualFace);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_owlGetModel(JNIEnv * env, jobject obj, int64_t model);
    JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColor(JNIEnv * env, jobject obj, float color);
    JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorR(JNIEnv * env, jobject obj, int64_t color);
    JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorG(JNIEnv * env, jobject obj, int64_t color);
    JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorB(JNIEnv * env, jobject obj, int64_t color);
    JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorA(JNIEnv * env, jobject obj, int64_t color);
};

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiOpenModelBN(JNIEnv * env, jobject obj, jstring ifcFile, jstring schemaFile)
{
    std::string strIFCFile = j2STLString(env, ifcFile);
    std::string strSchemaFile = schemaFile != NULL ? j2STLString(env, schemaFile) : "";

    int64_t iIFCModel = sdaiOpenModelBN(0, (char *)strIFCFile.c_str(), (char *)(schemaFile != NULL ? strSchemaFile.c_str() : NULL));
    if (iIFCModel == 0)
    {
        LOGE("Can't load IFC model: '%ls'", strIFCFile.c_str());

        return 0;
    }

    LOGI("IFCViewer: IFC model loaded: '%s'.", strIFCFile.c_str());

    return iIFCModel;
}

JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiCloseModel(JNIEnv * env, jobject obj, int64_t model)
{
   if (model != 0)
    {
        sdaiCloseModel((int_t)model);

        LOGI("IFCViewer: IFC model closed.");
    }
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetEntity(JNIEnv * env, jobject obj, int64_t model, jstring entityName)
{
    std::string strEntiryName = j2STLString(env, entityName);

    return sdaiGetEntity(model, (char *)strEntiryName.c_str());
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetInstanceType(JNIEnv * env, jobject obj, int64_t instance)
{
    return sdaiGetInstanceType((int_t)instance);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetEntityExtentBN(JNIEnv * env, jobject obj, int64_t model, jstring entityName)
{
    std::string strEntiryName = j2STLString(env, entityName);

    return (int64_t)sdaiGetEntityExtentBN(model, (char *)strEntiryName.c_str());
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetEntityExtent(JNIEnv * env, jobject obj, int64_t model, int64_t entity)
{
    return (int64_t)sdaiGetEntityExtent(model, entity);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetMemberCount(JNIEnv * env, jobject obj, int64_t aggregate)
{
    return sdaiGetMemberCount((int_t *)aggregate);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetAggrElement(JNIEnv * env, jobject obj, int64_t aggregate, int64_t elementIndex, int64_t valueType)
{
    int_t instance = 0;
    engiGetAggrElement((int_t *)aggregate, (int_t)elementIndex, (int_t)valueType, &instance);

    return instance;
}

JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetAttrBNStr(JNIEnv * env, jobject obj, int64_t instance, jstring attributeName)
{
    std::string strAttributeName = j2STLString(env, attributeName);

    wchar_t * szAttribute = nullptr;
    sdaiGetAttrBN((int_t)instance, (char *)strAttributeName.c_str(), sdaiUNICODE, &szAttribute);

    if (szAttribute != nullptr) {
        char szBuffer[1000];
        wcstombs (szBuffer, szAttribute, sizeof(szBuffer));

        return env->NewStringUTF(szBuffer);
    }

    return nullptr;
}

JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetADBTypePath(JNIEnv * env, jobject obj, jstring ADB, int64_t typeNameNumber)
{
    std::string strADB = j2STLString(env, ADB);

    char * typePath = sdaiGetADBTypePath((void *)strADB.c_str(), (int)typeNameNumber);

    if (typePath != nullptr) {
        return env->NewStringUTF(typePath);
    }

    return nullptr;
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_sdaiGetAttrBNLong(JNIEnv * env, jobject obj, int64_t instance, jstring attributeName, int64_t valueType)
{
    std::string strAttributeName = j2STLString(env, attributeName);

    int_t * piValue = 0;
    sdaiGetAttrBN((int_t)instance, (char *)strAttributeName.c_str(), (int_t)valueType, &piValue);

    return (long)piValue;
}

JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityName(JNIEnv * env, jobject obj, int64_t entity)
{
    wchar_t * szName = nullptr;
    engiGetEntityName((int_t)entity, sdaiUNICODE, (char **)&szName);

    if (szName != nullptr) {
        char szBuffer[1000];
        wcstombs (szBuffer, szName, sizeof(szBuffer));

        return env->NewStringUTF(szBuffer);
    }

    return nullptr;
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityCount(JNIEnv * env, jobject obj, int64_t model)
{
     return engiGetEntityCount((int_t)model);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityElement(JNIEnv * env, jobject obj, int64_t model, int64_t index)
{
    return engiGetEntityElement((int_t)model, (int_t)index);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_engiGetEntityParent(JNIEnv * env, jobject obj, int64_t entity)
{
    return engiGetEntityParent((int_t)entity);
}

JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_CalculateInstance(JNIEnv * env, jobject obj, int64_t instance, jobject ifcObject)
{
    int64_t iVerticesCount = 0;
    int64_t iIndicesCount = 0;
    CalculateInstance(instance, &iVerticesCount, &iIndicesCount, 0);

    jclass ifcObjectClass = env->GetObjectClass(ifcObject);

    jmethodID setVerticesCountMethodId = env->GetMethodID(ifcObjectClass, "setVerticesCount", "(J)V");
    env->CallVoidMethod(ifcObject, setVerticesCountMethodId, iVerticesCount);

    jmethodID setIndicesCountMethodId = env->GetMethodID(ifcObjectClass, "setIndicesCount", "(J)V");
    env->CallVoidMethod(ifcObject, setIndicesCountMethodId, iIndicesCount);
}

JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_setFormat(JNIEnv * env, jobject obj, int64_t model, int64_t settings, int64_t mask)
{
    SetFormat(model, settings, mask);
}

JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_setFilter(JNIEnv * env, jobject obj, int64_t model, int64_t settings, int64_t mask)
{
    setFilter((int_t)model, (int_t)settings, (int_t)mask);
}

JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_circleSegments(JNIEnv * env, jobject obj, int64_t circles, int64_t smallCircles)
{
    circleSegments((int_t)circles, (int_t)smallCircles);
}

JNIEXPORT void JNICALL Java_com_android_gl2jni_IFCEngineLib_cleanMemory(JNIEnv * env, jobject obj, int64_t model, int64_t mode)
{
    cleanMemory((int_t)model, (int_t)mode);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_owlGetInstance(JNIEnv * env, jobject obj, int64_t model, int64_t instance)
{
    int64_t iOWLInstance = 0;
    owlGetInstance((int_t)model, (int_t)instance, &iOWLInstance);

    return iOWLInstance;
}

JNIEXPORT jfloatArray JNICALL Java_com_android_gl2jni_IFCEngineLib_UpdateInstanceVertexBuffer(JNIEnv * env, jobject obj, int64_t owlInstance, int64_t size)
{
    float * pVertices = new float[size];
    UpdateInstanceVertexBuffer(owlInstance, pVertices);

    jfloatArray jVertices  = env->NewFloatArray(size);
    env->SetFloatArrayRegion(jVertices, 0, size, pVertices);
    delete[] pVertices;

    return jVertices;
}

JNIEXPORT jintArray JNICALL Java_com_android_gl2jni_IFCEngineLib_UpdateInstanceIndexBuffer(JNIEnv * env, jobject obj, int64_t owlInstance, int64_t size)
{
    int32_t * pIndices = new int32_t[size];
    UpdateInstanceIndexBuffer(owlInstance, pIndices);

    jintArray jIndices  = env->NewIntArray(size);
    env->SetIntArrayRegion(jIndices, 0, size, pIndices);
    delete[] pIndices;

    return jIndices;
}

JNIEXPORT jstring JNICALL Java_com_android_gl2jni_IFCEngineLib_GetSPFFHeaderItem(JNIEnv * env, jobject obj, int64_t model, int64_t itemIndex, int64_t itemSubIndex)
{
    wchar_t * szHeaderItem = NULL;
    GetSPFFHeaderItem(model, itemIndex, itemSubIndex, sdaiUNICODE, (char **)&szHeaderItem);

    if (szHeaderItem != nullptr) {
        char szBuffer[1000];
        wcstombs (szBuffer, szHeaderItem, sizeof(szBuffer));

        return env->NewStringUTF(szBuffer);
    }

    return nullptr;
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_getConceptualFaceCnt(JNIEnv * env, jobject obj, int64_t instance)
{
    return GetConceptualFaceCnt(instance);
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_getConceptualFaceEx(JNIEnv * env, jobject obj, int64_t instance, int64_t index, jobject conceptualFace)
{
    int64_t iStartIndexTriangles = 0;
    int64_t iIndicesCountTriangles = 0;

    int64_t iStartIndexLines = 0;
    int64_t iIndicesCountLines = 0;

    int64_t iStartIndexPoints = 0;
    int64_t iIndicesCountPoints = 0;

    int64_t iStartIndexFacesPolygons = 0;
    int64_t iIndicesCountFacesPolygons = 0;

    int64_t iStartIndexCocneptualFacesPolygons = 0;
    int64_t iIndicesCountConceptualFacesPolygons = 0;

    int64_t conceptualFaceInstance = GetConceptualFaceEx(
            instance, index,
            &iStartIndexTriangles, &iIndicesCountTriangles,
            &iStartIndexLines, &iIndicesCountLines,
            &iStartIndexPoints, &iIndicesCountPoints,
            &iStartIndexFacesPolygons, &iIndicesCountFacesPolygons,
            &iStartIndexCocneptualFacesPolygons, &iIndicesCountConceptualFacesPolygons);

    jclass conceptualFaceClass = env->GetObjectClass(conceptualFace);

    jmethodID initMethodId = env->GetMethodID(conceptualFaceClass, "init", "(JJJJJJJJJJ)V");
    env->CallVoidMethod(conceptualFace, initMethodId,
                        iStartIndexTriangles, iIndicesCountTriangles,
                        iStartIndexLines, iIndicesCountLines,
                        iStartIndexPoints, iIndicesCountPoints,
                        iStartIndexFacesPolygons, iIndicesCountFacesPolygons,
                        iStartIndexCocneptualFacesPolygons, iIndicesCountConceptualFacesPolygons);

    return conceptualFaceInstance;
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_owlGetModel(JNIEnv * env, jobject obj, int64_t model)
{
    int64_t iOWLModel = 0;
    owlGetModel((int_t)model, &iOWLModel);

    return iOWLModel;
}

JNIEXPORT int64_t JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColor(JNIEnv * env, jobject obj, float color)
{
    unsigned int iColor = *(reinterpret_cast<unsigned int *>(&color));

    return iColor;
}

JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorR(JNIEnv * env, jobject obj, int64_t color)
{
    float fR = (float)((unsigned int)color & ((unsigned int)255 * 256 * 256 * 256)) / (256 * 256 * 256);
    fR /= 255.f;

    return fR;
}

JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorG(JNIEnv * env, jobject obj, int64_t color)
{
    float fG = (float)((unsigned int)color & (255 * 256 * 256)) / (256 * 256);
    fG /= 255.f;

    return fG;
}

JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorB(JNIEnv * env, jobject obj, int64_t color)
{
    float fB = (float)((unsigned int)color & (255 * 256)) / 256;
    fB /= 255.f;

    return fB;
}

JNIEXPORT float JNICALL Java_com_android_gl2jni_IFCEngineLib_retrieveColorA(JNIEnv * env, jobject obj, int64_t color)
{
    float fA = (float)((unsigned int)color & (255)) / (float)255;

    return fA;
}