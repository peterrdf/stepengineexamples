package com.android.gl2jni;

import java.nio.ShortBuffer;
import java.util.ArrayList;
import java.util.List;

public class IndicesCohort {

    private List<Integer> mIndices = new ArrayList<>();

    ShortBuffer mIBO = null;

    /**
     * ctor
     */
    public IndicesCohort() {
    }

    /**
     * Accessor
     * @return
     */
    public List<Integer> indices() {
        return mIndices;
    }

    /**
     * Gettter
     * @return
     */
    public ShortBuffer getIBO () {
        return mIBO;
    }

    /**
     * Setter
     * @param IBO
     */
    public void setIBO(ShortBuffer IBO) {
        mIBO = IBO;
    }
}
