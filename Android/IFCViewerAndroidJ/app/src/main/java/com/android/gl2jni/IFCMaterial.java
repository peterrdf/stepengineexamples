package com.android.gl2jni;

import java.nio.IntBuffer;
import java.nio.ShortBuffer;
import java.util.ArrayList;
import java.util.List;

public class IFCMaterial {
    private IFCColor mAmbient = new IFCColor();
    private IFCColor mDiffuse = new IFCColor();
    private IFCColor mEmissive = new IFCColor();
    private IFCColor mSpecular = new IFCColor();
    private float mA = 1.f;

    private List<Integer> mIndices = new ArrayList<>();

    ShortBuffer mIBO = null;

    /**
     * ctor
     */
    public IFCMaterial(long ambientColor, long diffuseColor, long emissiveColor, long specularColor, float transparency) {
        if ((ambientColor == 0) && (diffuseColor == 0) && (emissiveColor == 0) && (specularColor == 0) && (transparency == 0.f))
        {
            /*
             * There is no material - use non-transparent black
             */
            mAmbient.init(0, 1.f);
            mDiffuse.init(0, 1.f);
            mEmissive.init(0, 1.f);
            mSpecular.init(0, 1.f);

            mA = 1.f;
        }
        else
        {
            mAmbient.init(ambientColor, transparency);
            mDiffuse.init(diffuseColor == 0 ? ambientColor : diffuseColor, transparency);
            mEmissive.init(emissiveColor, transparency);
            mSpecular.init(specularColor, transparency);

            mA = transparency;
        }
    }

    /**
     * ctor
     */
    public IFCMaterial(IFCMaterial ifcMaterial) {
        mAmbient.init(ifcMaterial.mAmbient.getColor(), ifcMaterial.getA());
        mDiffuse.init(ifcMaterial.mDiffuse.getColor(), ifcMaterial.getA());
        mEmissive.init(ifcMaterial.mEmissive.getColor(), ifcMaterial.getA());
        mSpecular.init(ifcMaterial.mSpecular.getColor(), ifcMaterial.getA());

        mA = ifcMaterial.mA;
    }

    /**
     * Getter
     * @return
     */
    public IFCColor getAmbientColor() {
        return mAmbient;
    }

    /**
     * Getter
     * @return
     */
    public IFCColor getDiffuseColor() {
        return mDiffuse;
    }

    /**
     * Getter
     * @return
     */
    public IFCColor getSpecularColor() {
        return mSpecular;
    }

    /**
     * Getter
     * @return
     */
    public IFCColor getEmissiveColor() {
        return mEmissive;
    }

    public float getA() {
        return mA;
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

    @Override
    public int hashCode() {
        return (new Long(mAmbient.getColor()).toString() +
               new Long(mDiffuse.getColor()).toString() +
               new Long(mEmissive.getColor()).toString() +
               new Long(mSpecular.getColor()).toString()).hashCode();
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (getClass() != obj.getClass())
            return false;
        IFCMaterial other = (IFCMaterial)obj;
        if (mAmbient.equals(other.mAmbient) && mDiffuse.equals(other.mDiffuse) && mEmissive.equals(other.mEmissive) && mSpecular.equals(other.mSpecular) && (mA == other.mA))
            return true;
        return false;
    }
}
