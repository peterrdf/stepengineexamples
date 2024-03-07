package com.android.gl2jni;

public class ConceptualFace {
    private long mStartIndexTriangles = 0;
    private long mIndicesCountTriangles = 0;

    private long mStartIndexLines = 0;
    private long mIndicesCountLines = 0;

    private long mStartIndexPoints = 0;
    private long mIndicesCountPoints = 0;

    private long mStartIndexFacesPolygons = 0;
    private long mIndicesCountFacesPolygons = 0;

    private long mStartIndexConceptualFacesPolygons = 0;
    private long mIndicesCountConceptualFacesPolygons = 0;

    /**
     * ctor
     */
    public ConceptualFace() {
    }

    /**
     * Initialize
     * @param startIndexTriangles
     * @param noIndicesTriangles
     * @param startIndexLines
     * @param noIndicesLines
     * @param startIndexPoints
     * @param noIndicesPoints
     * @param startIndexFacesPolygons
     * @param noIndicesFacesPolygons
     * @param startIndexConceptualFacePolygons
     * @param noIndicesConceptualFacePolygons
     */
    public void init(long startIndexTriangles,
                      long noIndicesTriangles,
                      long startIndexLines,
                      long noIndicesLines,
                      long startIndexPoints,
                      long noIndicesPoints,
                      long startIndexFacesPolygons,
                      long noIndicesFacesPolygons,
                      long startIndexConceptualFacePolygons,
                      long noIndicesConceptualFacePolygons) {
        mStartIndexTriangles = startIndexTriangles;
        mIndicesCountTriangles = noIndicesTriangles;

        mStartIndexLines = startIndexLines;
        mIndicesCountLines = noIndicesLines;

        mStartIndexPoints = startIndexPoints;
        mIndicesCountPoints = noIndicesPoints;

        mStartIndexFacesPolygons = startIndexFacesPolygons;
        mIndicesCountFacesPolygons = noIndicesFacesPolygons;

        mStartIndexConceptualFacesPolygons = startIndexConceptualFacePolygons;
        mIndicesCountConceptualFacesPolygons = noIndicesConceptualFacePolygons;
    }

    /**
     * Getter
     * @return
     */
    public long getTrianglesStartIndex() {
        return mStartIndexTriangles;
    }

    /**
     * Getter
     * @return
     */
    public long getTrianglesIndicesCount() {
        return mIndicesCountTriangles;
    }

    /**
     * Getter
     * @return
     */
    public long getFacesPolygonsStartIndex() {
        return mStartIndexFacesPolygons;
    }

    public long getFacesPolygonsIndicesCount() {
        return mIndicesCountFacesPolygons;
    }

    // TODO: getters for all fields
}
