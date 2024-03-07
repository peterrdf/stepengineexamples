/*
 * Copyright (C) 2014 The Android Open Source Project
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

package com.android.gl2jni;

import android.opengl.GLES20;
import android.util.Log;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;
import java.nio.IntBuffer;
import java.nio.ShortBuffer;
import java.util.List;

import javax.microedition.khronos.opengles.GL10;
import javax.microedition.khronos.opengles.GL11;

public class IFCRenderer {

    private static final String TAG = "IFCViewerAndroidJ";

    private IFCModel mIFCModel = null;

    /**
     * ctor
     */
    public IFCRenderer() {
    }

    /**
     * Setter
     * @param ifcModel
     */
    public void setModel(IFCModel ifcModel) {
        mIFCModel = ifcModel;

        if (mIFCModel == null) {
            return;
        }

        // Min/Max
        float Xmin = Float.MAX_VALUE;
        float Xmax = -Float.MAX_VALUE;
        float Ymin = Float.MAX_VALUE;
        float Ymax = -Float.MAX_VALUE;
        float Zmin = Float.MAX_VALUE;
        float Zmax = -Float.MAX_VALUE;

        List<IFCObject> ifcObjects = mIFCModel.getIFCObjects();
        for (IFCObject ifcObject : ifcObjects) {
            if (!ifcObject.hasGeometry()) {
                continue;
            }

            float[] minMax = ifcObject.CalculateMinMaxValues(Xmin, Xmax, Ymin, Ymax, Zmin, Zmax);

            Xmin = minMax[0];
            Xmax = minMax[1];
            Ymin = minMax[2];
            Ymax = minMax[3];
            Zmin = minMax[4];
            Zmax = minMax[5];
        }

        Log.v(TAG, "Xmin/max: " + Xmin + "/" + Xmax);
        Log.v(TAG, "Ymin/max: " + Ymin + "/" + Ymax);
        Log.v(TAG, "Zmin/max: " + Zmin + "/" + Zmax);

        Float boundingSphereDiameter = new Float(Xmax - Xmin);
        boundingSphereDiameter = Math.max(boundingSphereDiameter, Ymax - Ymin);
        boundingSphereDiameter = Math.max(boundingSphereDiameter, Zmax - Zmin);
        Log.v(TAG, "Bounding sphere diameter: " + boundingSphereDiameter);

        // Scale
        for (IFCObject ifcObject : ifcObjects) {
            if (!ifcObject.hasGeometry()) {
                continue;
            }

            ifcObject.Scale(Xmin, Xmax, Ymin, Ymax, Zmin, Zmax, boundingSphereDiameter);
        }

        // Vertex/Index buffers
        for (IFCObject ifcObject : ifcObjects)
        {
            if (!ifcObject.hasGeometry()) {
                continue;
            }

            // Vertex buffer
            FloatBuffer vertexBuffer = BufferUtils.toBuffer(ifcObject.getVertices());
            ifcObject.setVBO(vertexBuffer);

            // Materials - Index buffer
            List<IFCMaterial> materials = ifcObject.materials();
            for (IFCMaterial ifcMaterial : materials) {
                ShortBuffer indexBuffer = BufferUtils.toBuffer(ifcMaterial.indices());
                ifcMaterial.setIBO(indexBuffer);
            }

            // Wireframes - Index buffer
            List<WireframesCohort> wireframesCohorts = ifcObject.getWireframesCohorts();
            for (WireframesCohort wireframesCohort : wireframesCohorts) {
                ShortBuffer indexBuffer = BufferUtils.toBuffer(wireframesCohort.indices());
                wireframesCohort.setIBO(indexBuffer);
            }
        } // for (IFCObject ifcObject ...
    }

    /**
     * All
     * @param gl
     */
    public void render(GL10 gl) {
        if (mIFCModel == null) {
            return;
        }

        // Faces
        drawFaces(gl, false);
        drawFaces(gl, true);

        // Wireframes
        drawWireframes(gl);
    }

    /**
     * Cocneptual faces
     * @param gl
     * @param transparent
     */
    private void drawFaces(GL10 gl, boolean transparent) {
        if (transparent) {
            gl.glEnable(GL11.GL_BLEND);
            gl.glBlendFunc(GL11.GL_SRC_ALPHA, GL11.GL_ONE_MINUS_SRC_ALPHA);

            gl.glDepthMask(false);
        }

        gl.glEnableClientState(GL11.GL_VERTEX_ARRAY);
        gl.glEnableClientState(GL11.GL_NORMAL_ARRAY);

        List<IFCObject> ifcObjects = mIFCModel.getIFCObjects();
        for (IFCObject ifcObject : ifcObjects)
        {
            if (!ifcObject.hasGeometry() || !ifcObject.getVisible()) {
                continue;
            }

            // Vertex buffer
            gl.glVertexPointer(3, GL11.GL_FLOAT, (int)IFCModel.VERTEX_LENGTH * BufferUtils.INT_SIZE_BYTES, ifcObject.getVBO().position(0));
            gl.glNormalPointer(GL11.GL_FLOAT, (int)IFCModel.VERTEX_LENGTH * BufferUtils.INT_SIZE_BYTES, ifcObject.getVBO().position(3));

            List<IFCMaterial> materials = ifcObject.materials();
            for (IFCMaterial ifcMaterial : materials)
            {
                if (transparent) {
                    if (ifcMaterial.getA() == 1.0f) {
                        continue;
                    }
                }
                else {
                    if (ifcMaterial.getA() < 1.0f) {
                        continue;
                    }
                }

                // Material
                gl.glMaterialfv(GL11.GL_FRONT_AND_BACK, GL11.GL_AMBIENT, ifcMaterial.getAmbientColor().asBuffer());
                gl.glMaterialfv(GL11.GL_FRONT_AND_BACK, GL11.GL_DIFFUSE, ifcMaterial.getDiffuseColor().asBuffer());
                gl.glMaterialfv(GL11.GL_FRONT_AND_BACK, GL11.GL_SPECULAR, ifcMaterial.getSpecularColor().asBuffer());
                gl.glMaterialfv(GL11.GL_FRONT_AND_BACK, GL11.GL_EMISSION, ifcMaterial.getEmissiveColor().asBuffer());

                /*
                Log.v(TAG, "Drawing: " + ifcObject.getEntity());
                Log.v(TAG, "Ambient: " + ifcMaterial.getAmbientColor().toString());
                Log.v(TAG, "Diffuse: " + ifcMaterial.getDiffuseColor().toString());
                Log.v(TAG, "Specular: " + ifcMaterial.getSpecularColor().toString());
                Log.v(TAG, "Emissive: " + ifcMaterial.getEmissiveColor().toString());
                Log.v(TAG, "A: " + ifcMaterial.getA());
                */

                // Index buffer
                gl.glDrawElements(GL11.GL_TRIANGLES, ifcMaterial.indices().size(), GL11.GL_UNSIGNED_SHORT, ifcMaterial.getIBO());
            } // for (IFCMaterial ifcMaterial ...
        } // for (IFCObject ifcObject ...

        gl.glDisableClientState(GL11.GL_VERTEX_ARRAY);
        gl.glDisableClientState(GL11.GL_NORMAL_ARRAY);

        if (transparent)
        {
            gl.glDisable(GL11.GL_BLEND);

            gl.glDepthMask(true);
        }
    }

    /**
     * Polygons
     */
    private void drawWireframes(GL10 gl) {
        gl.glDisable(GL11.GL_LIGHT0);

        gl.glLineWidth(1.f);
        gl.glColor4f(0.f, 0.f, 0.f, 1.f);

        gl.glEnableClientState(GL11.GL_VERTEX_ARRAY);

        List<IFCObject> ifcObjects = mIFCModel.getIFCObjects();
        for (IFCObject ifcObject : ifcObjects)
        {
            if (!ifcObject.hasGeometry() || !ifcObject.getVisible()) {
                continue;
            }

            // Vertex buffer
            gl.glVertexPointer(3, GL11.GL_FLOAT, (int)IFCModel.VERTEX_LENGTH * BufferUtils.INT_SIZE_BYTES, ifcObject.getVBO().position(0));

            List<WireframesCohort> wireframesCohorts = ifcObject.getWireframesCohorts();
            for (WireframesCohort wireframesCohort : wireframesCohorts) {
                // Index buffer
                gl.glDrawElements(GL11.GL_LINES, wireframesCohort.indices().size(), GL11.GL_UNSIGNED_SHORT, wireframesCohort.getIBO());
            }
        } // for (IFCObject ifcObject ...

        gl.glDisableClientState(GL11.GL_VERTEX_ARRAY);

        gl.glColor4f(1.f, 1.f, 1.f, 1.f);
        gl.glEnable(GL11.GL_LIGHT0);
    }
}
