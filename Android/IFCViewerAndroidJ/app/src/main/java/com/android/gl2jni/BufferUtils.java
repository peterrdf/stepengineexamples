package com.android.gl2jni;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;
import java.nio.ShortBuffer;
import java.util.List;

public class BufferUtils {
    public static final int FLOAT_SIZE_BYTES = 4;
    public static final int INT_SIZE_BYTES = 4;

    public static FloatBuffer toBuffer(float[] inBuffer) {
        FloatBuffer outBuffer = ByteBuffer.allocateDirect(
                inBuffer.length * FLOAT_SIZE_BYTES)
                .order(ByteOrder.nativeOrder()).asFloatBuffer();
        outBuffer.put(inBuffer).position(0);

        return outBuffer;
    }

    public static ShortBuffer toBuffer(List<Integer> inBuffer) {
        int[] indices = inBuffer.stream().mapToInt(Integer::intValue).toArray();

        ShortBuffer outBuffer = ByteBuffer.allocateDirect(
                indices.length * INT_SIZE_BYTES)
                .order(ByteOrder.nativeOrder()).asShortBuffer();

        for (int index = 0; index < indices.length; index++) {
            outBuffer.put(index, (short)indices[index]);
        }

        outBuffer.position(0);

        return outBuffer;
    }
}
