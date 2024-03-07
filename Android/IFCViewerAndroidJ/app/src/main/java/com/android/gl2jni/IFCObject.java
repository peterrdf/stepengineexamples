package com.android.gl2jni;

import android.util.Log;

import java.nio.FloatBuffer;
import java.util.ArrayList;
import java.util.List;

public class IFCObject {
    private String mEntity = "";

    private float[] mVertices = null;
    private long mVerticesCount = 0;

    private int[] mIndices = null;
    private long mIndicesCount = 0;

    private boolean mVisible = true;

    private List<IFCMaterial> mMaterials = new ArrayList<>();

    private List<WireframesCohort> mWireframesCohorts = new ArrayList<>();

    private FloatBuffer mVBO = null;

    /**
     * ctor
     */
    public IFCObject(String entity) {
        mEntity = entity;
    }

    /**
     * Getter
     * @return
     */
    public String getEntity() {
        return mEntity;
    }

    /**
     * Getter
     * @return
     */
    public boolean hasGeometry() {
        return (mVerticesCount > 0) && (mIndicesCount > 0);
    }

    /**
     * Setter
     * @param verticesCount
     */
    public void setVerticesCount(long verticesCount) {
        mVerticesCount = verticesCount;
    }

    /**
     * Getter
     * @return
     */
    public long getVerticesCount() {
        return mVerticesCount;
    }

    /**
     * Setter
     * @param inidcesCount
     */
    public void setIndicesCount(long inidcesCount) {
        mIndicesCount = inidcesCount;
    }

    /**
     * Getter
     * @return
     */
    public long getIndicesCount() {
        return mIndicesCount;
    }

    /**
     * Setter
     * @param vertices
     * @throws Exception
     */
    public void setVertices(float[] vertices) throws Exception {
        if (vertices.length != (mVerticesCount * IFCModel.VERTEX_LENGTH)) {
            throw new Exception("Invalid vertex buffer.");
        }

        mVertices = vertices;
    }

    /**
     * Getter
     * @return
     */
    public float[] getVertices() {
        return mVertices;
    }

    /**
     * Setter
     * @param indices
     * @throws Exception
     */
    public void setIndices(int[] indices) throws Exception {
        if (indices.length != mIndicesCount) {
            throw new Exception("Invalid index buffer.");
        }

        mIndices = indices;
    }

    /**
     * Getter
     * @return
     */
    public int[] getIndices() {
        return mIndices;
    }

    /**
     * Setter
     * @param visible
     */
    public void setVisible(boolean visible) {
        mVisible = visible;
    }

    /**
     * Getter
     * @return
     */
    public boolean getVisible() {
        return mVisible;
    }

    /**
     * Accessor
     * @return
     */
    public List<IFCMaterial> materials() {
        return mMaterials;
    }

    /**
     * Accessor
     * @return
     */
    public List<WireframesCohort> getWireframesCohorts() {
        return mWireframesCohorts;
    }

    /**
     * Accessor
     * @return
     */
    public void setVBO(FloatBuffer VBO) {
        mVBO = VBO;
    }

    /**
     * Getter
     * @return
     */
    public FloatBuffer getVBO() {
        return mVBO;
    }

    /**
     * Min/Max X/Y/Z
     * @param Xmin
     * @param Xmax
     * @param Ymin
     * @param Ymax
     * @param Zmin
     * @param Zmax
     */
    public float[] CalculateMinMaxValues(float Xmin, float Xmax, float Ymin, float Ymax, float Zmin, float Zmax)
    {
        for (int vertex = 0; vertex < mVerticesCount; vertex++)
        {
            Xmin = Math.min(Xmin, mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)]);
            Xmax = Math.max(Xmax, mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)]);

            Ymin = Math.min(Ymin, mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1]);
            Ymax = Math.max(Ymax, mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1]);

            Zmin = Math.min(Zmin, mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2]);
            Zmax = Math.max(Zmax, mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2]);
        }

        float[] minMax =
                {
                        Xmin,
                        Xmax,
                        Ymin,
                        Ymax,
                        Zmin,
                        Zmax
                };

        return minMax;
    }

    /**
     * [-1, 1]
     * @param Xmin
     * @param Xmax
     * @param Ymin
     * @param Ymax
     * @param Zmin
     * @param Zmax
     * @param resoltuion
     */
    public void Scale(float Xmin, float Xmax, float Ymin, float Ymax, float Zmin, float Zmax, float resoltuion)
    {
        for (int vertex = 0; vertex < mVerticesCount; vertex++)
        {
            // [0.0 -> X/Y/Zmin + X/Y/Zmax]
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)] - Xmin;
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1] - Ymin;
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2] - Zmin;

            // center
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)] - ((Xmax - Xmin) / 2.0f);
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1] - ((Ymax - Ymin) / 2.0f);
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2] - ((Zmax - Zmin) / 2.0f);

            // [-1.0 -> 1.0]
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH)] / (resoltuion / 2.0f);
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 1] / (resoltuion / 2.0f);
            mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2] = mVertices[(vertex * (int)IFCModel.VERTEX_LENGTH) + 2] / (resoltuion / 2.0f);
        }
    }
}
