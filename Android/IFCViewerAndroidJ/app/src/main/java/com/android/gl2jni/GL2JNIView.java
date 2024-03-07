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

package com.android.gl2jni;
/*
 * Copyright (C) 2008 The Android Open Source Project
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


import android.content.Context;
import android.graphics.PixelFormat;
import android.graphics.Point;
import android.opengl.GLSurfaceView;
import android.os.Build;
import android.util.AttributeSet;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;

import javax.microedition.khronos.egl.EGL10;
import javax.microedition.khronos.egl.EGLConfig;
import javax.microedition.khronos.egl.EGLContext;
import javax.microedition.khronos.egl.EGLDisplay;
import javax.microedition.khronos.opengles.GL10;
import javax.microedition.khronos.opengles.GL11;

/**
 * A simple GLSurfaceView sub-class that demonstrate how to perform
 * OpenGL ES 2.0 rendering into a GL Surface. Note the following important
 * details:
 *
 * - The class must use a custom context factory to enable 2.0 rendering.
 *   See ContextFactory class definition below.
 *
 * - The class must use a custom EGLConfigChooser to be able to select
 *   an EGLConfig that supports 2.0. This is done by providing a config
 *   specification to eglChooseConfig() that has the attribute
 *   EGL10.ELG_RENDERABLE_TYPE containing the EGL_OPENGL_ES2_BIT flag
 *   set. See ConfigChooser class definition below.
 *
 * - The class must select the surface's format, then choose an EGLConfig
 *   that matches it exactly (with regards to red/green/blue/alpha channels
 *   bit depths). Failure to do so would result in an EGL_BAD_MATCH error.
 */
class GL2JNIView extends GLSurfaceView implements GLSurfaceView.Renderer {
    private static String TAG = "IFCViewerAndroidJ";

    private IFCRenderer mIFCRenderer = null;

    private float mXAngle = 30.f;
    private float mYAngle = 30.f;
    private float mZTranslate = -4.f;

    private Point mPreviousTouch = new Point(-1, -1);
    
    private float mPreviousTouchDistance = -1.f;

    /**
     * ctor
     * @param context
     * @param ifcRenderer
     */
    public GL2JNIView(Context context, IFCRenderer ifcRenderer) {
        super(context);

        mIFCRenderer = ifcRenderer;

        if (!isEmulator()) {
            setEGLConfigChooser(new IFCConfigChooser());
        }

        setRenderer(this);
        setRenderMode(GLSurfaceView.RENDERMODE_WHEN_DIRTY);
    }

    /**
     * Render
     * @param gl
     */
    public void onDrawFrame(GL10 gl) {
        gl.glClear(GL11.GL_COLOR_BUFFER_BIT | GL11.GL_DEPTH_BUFFER_BIT);

        gl.glMatrixMode(GL11.GL_MODELVIEW);
        gl.glLoadIdentity();

        gl.glTranslatef(0.0f, 0.0f, mZTranslate);

        gl.glRotatef(mXAngle, 1.0f, 0.0f, 0.0f);
        gl.glRotatef(mYAngle, 0.0f, 1.0f, 0.0f);

        if (mIFCRenderer != null) {
            mIFCRenderer.render(gl);
        }
    }

    /**
     * Open GL Scene
     * @param gl
     * @param width
     * @param height
     */
    public void onSurfaceChanged(GL10 gl, int width, int height) {
        Log.v(TAG, "onSurfaceChanged()");

        gl.glViewport(0, 0, width, height);
        float ratio;
        float zNear = .01f;
        float zFar = 1000f;
        float fieldOfView = (float) Math.toRadians(30);
        float size;

        gl.glEnable(GL11.GL_NORMALIZE);

        ratio = (float) width / (float) height;

        gl.glMatrixMode(GL11.GL_PROJECTION);

        size = zNear * (float) (Math.tan((double) (fieldOfView / 2.0f)));

        gl.glFrustumf(-size, size, -size / ratio, size / ratio, zNear, zFar);

        gl.glMatrixMode(GL11.GL_MODELVIEW);
    }

    /**
     * Open GL Scene
     * @param gl
     * @param config
     */
    public void onSurfaceCreated(GL10 gl, EGLConfig config) {
        Log.v(TAG, "onSurfaceCreated()");

        gl.glClearColor(1, 1, 1, 1);

        gl.glFrontFace(GL11.GL_CCW);

        gl.glEnable(GL11.GL_DEPTH_TEST);
        gl.glDepthFunc(GL11.GL_LEQUAL);

        gl.glHint(GL11.GL_PERSPECTIVE_CORRECTION_HINT, GL11.GL_FASTEST);

        gl.glEnable(GL11.GL_MULTISAMPLE);

        gl.glEnable(GL11.GL_COLOR_MATERIAL);

        gl.glShadeModel(GL11.GL_SMOOTH);
        gl.glDisable(GL11.GL_DITHER);

        gl.glEnable(GL11.GL_LIGHTING);
        gl.glEnable(GL11.GL_LIGHT0);

        float[] ambientLight = { 0.15f, 0.15f, 0.15f, 1.0f };
        gl.glLightfv(GL11.GL_LIGHT0, GL11.GL_AMBIENT, BufferUtils.toBuffer(ambientLight));

        float[] diffuseLight = { 0.15f, 0.15f, 0.15f, 1.0f };
        gl.glLightfv(GL11.GL_LIGHT0, GL11.GL_DIFFUSE, BufferUtils.toBuffer(diffuseLight));

        float[] specularLight = { 0.1f, 0.1f, 0.1f, 0.5f };
        gl.glLightfv(GL11.GL_LIGHT0, GL11.GL_SPECULAR, BufferUtils.toBuffer(specularLight));

        float[] lightPosition = { -2.0f, -2.0f, -2.0f, 0.0f };
        gl.glLightfv(GL11.GL_LIGHT0, GL11.GL_POSITION, BufferUtils.toBuffer(lightPosition));

        float[] model_ambient = {0.3f, 0.3f, 0.3f, 1.0f};
        gl.glLightModelfv(gl.GL_LIGHT_MODEL_AMBIENT, BufferUtils.toBuffer(model_ambient));
    }

    /**
     * Open GL
     * @param prompt
     * @param egl
     */
    private static void checkEglError(String prompt, EGL10 egl) {
        int error;
        while ((error = egl.eglGetError()) != EGL10.EGL_SUCCESS) {
            Log.e(TAG, String.format("%s: EGL error: 0x%x", prompt, error));
        }
    }

    @Override
    public boolean onTouchEvent(MotionEvent e) {
        if (e.getPointerCount() == 2) {
            mPreviousTouch.set(-1, -1);

            float x1 = e.getX(0);
            float y1 = e.getY(0);

            float x2 = e.getX(1);
            float y2 = e.getY(1);

            float touchDistance = (float)Math.sqrt(Math.pow(x2 - x1, 2.) + Math.pow(y2 - y1, 2.));

            if (mPreviousTouchDistance == -1.f) {
                mPreviousTouchDistance = touchDistance;
                return true;
            }

            if (mPreviousTouchDistance == touchDistance) {
                return true;
            }
            
            if (mPreviousTouchDistance > touchDistance) {
                mZTranslate -= 0.0125;
            }
            else {
                mZTranslate += 0.0125;
            }

            requestRender();

            mPreviousTouchDistance = touchDistance;

            return true;
        } // if (e.getPointerCount() == 2)

        mPreviousTouchDistance = -1.f;

        int x = (int)e.getX();
        int y = (int)e.getY();

        if (e.getAction() == MotionEvent.ACTION_DOWN) {
            mPreviousTouch.set(x, y);
        }
        else {
            if (mPreviousTouch.x == -1) {
                return true;
            }

            if (e.getAction() == MotionEvent.ACTION_MOVE) {

                float XAngle = (e.getY() - (float)mPreviousTouch.y);
                float YAngle = (e.getX() - (float)mPreviousTouch.x);

                float ROTATION_SPEED = 1.f / 250.f;
                float ROTATION_SENSITIVITY = 1.1f;

                if (Math.abs(XAngle) >= Math.abs(YAngle) * ROTATION_SENSITIVITY)
                {
                    YAngle = 0.f;
                }
                else
                {
                    if (Math.abs(YAngle) >= Math.abs(XAngle) * ROTATION_SENSITIVITY)
                    {
                        XAngle = 0.f;
                    }
                }

                mPreviousTouch.set(x, y);

                rotate(XAngle * ROTATION_SPEED, YAngle * ROTATION_SPEED);
            }
        }

        return true;
    }

    private void rotate(float dXSpin, float dYSpin) {
        mXAngle += dXSpin * (180. / 3.14f);
        if (mXAngle > 360.0)
        {
            mXAngle = mXAngle - 360.0f;
        }
        else
        {
            if (mXAngle < 0.0)
            {
                mXAngle = mXAngle + 360.0f;
            }
        }

        mYAngle += dYSpin * (180. / 3.14f);
        if (mYAngle > 360.0)
        {
            mYAngle = mYAngle - 360.0f;
        }
        else
        {
            if (mYAngle < 0.0)
            {
                mYAngle = mYAngle + 360.0f;
            }
        }

        requestRender();
    }

    private static boolean isEmulator() {
        return Build.FINGERPRINT.startsWith("generic")
                || Build.FINGERPRINT.startsWith("unknown")
                || Build.MODEL.contains("google_sdk")
                || Build.MODEL.contains("Emulator")
                || Build.MODEL.contains("Android SDK built for x86")
                || Build.MANUFACTURER.contains("Genymotion")
                || (Build.BRAND.startsWith("generic") && Build.DEVICE.startsWith("generic"))
                || "google_sdk".equals(Build.PRODUCT);
    }

    class IFCConfigChooser implements GLSurfaceView.EGLConfigChooser {
        @Override
        public EGLConfig chooseConfig(EGL10 egl, EGLDisplay display) {
            int attribs[] = {
                    EGL10.EGL_LEVEL, 0,
                    EGL10.EGL_RENDERABLE_TYPE, 4,  // EGL_OPENGL_ES2_BIT
                    EGL10.EGL_COLOR_BUFFER_TYPE, EGL10.EGL_RGB_BUFFER,
                    EGL10.EGL_RED_SIZE, 8,
                    EGL10.EGL_GREEN_SIZE, 8,
                    EGL10.EGL_BLUE_SIZE, 8,
                    EGL10.EGL_DEPTH_SIZE, 16,
                    EGL10.EGL_SAMPLE_BUFFERS, 1,
                    EGL10.EGL_SAMPLES, 4,  // This is for 4x MSAA.
                    EGL10.EGL_NONE
            };

            EGLConfig[] configs = new EGLConfig[1];
            int[] configCounts = new int[1];
            egl.eglChooseConfig(display, attribs, configs, 1, configCounts);

            if (configCounts[0] == 0) {
                // Failed! Error handling.
                return null;
            } else {
                return configs[0];
            }
        }
    }
}
