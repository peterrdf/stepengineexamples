package com.android.gl2jni;

import java.nio.FloatBuffer;

public class IFCColor {
    private long mColor = 0;
    private float mR = 0;
    private float mG = 0;
    private float mB = 0;

    private FloatBuffer mBuffer = null;

    /**
     * ctor
     */
    public IFCColor() {
    }

    /**
     * Initialize
     * @param color
     */
    void init(long color, float fA)
    {
        mColor = color;

        mR = IFCEngineLib.retrieveColorR(color);
        mG = IFCEngineLib.retrieveColorG(color);
        mB = IFCEngineLib.retrieveColorB(color);

        float[] material = {R(), G(), B(), fA};
        mBuffer = BufferUtils.toBuffer(material);
    }

    /**
     * Getter
     * @return
     */
    long getColor()
    {
        return mColor;
    }

    /**
     * Getter
     * @return
     */
    float R()
    {
        return mR;
    }

    /**
     * Getter
     * @return
     */
    float G()
    {
        return mG;
    }

    /**
     * Getter
     * @return
     */
    float B()
    {
        return mB;
    }

    public FloatBuffer asBuffer() {
        return mBuffer;
    }

    @Override
    public String toString() {
        return "RGB: (" + mR + ", " + mG + ", " + mB + ")";
    }

    @Override
    public int hashCode() {
        return new Long(mColor).toString().hashCode();
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (getClass() != obj.getClass())
            return false;
        IFCColor other = (IFCColor)obj;
        if (mColor != other.mColor)
            return false;
        return true;
    }
}
