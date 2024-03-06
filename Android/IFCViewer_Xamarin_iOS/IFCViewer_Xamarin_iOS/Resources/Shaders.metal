/*
 <samplecode>
 <abstract>
 lighting shader for Basic Metal 3D
 </abstract>
 </samplecode>
 */

#include <metal_stdlib>

using namespace metal;

struct FrameUniforms {
    float4x4 modelview_projection_matrix;
    float4x4 normal_matrix;
};

struct MaterialUniforms
{
    float4   ambient_color;
    float4   diffuse_color;
};

// variables in constant address space
constant float3 light_position = float3(0.0, 1.0, -1.0);

typedef struct
{
	float3 position [[ attribute(0) ]];
    float3 normal   [[ attribute(1) ]];
} VertexInput;

struct ColorInOut {
    float4 position [[position]];
    half4 color;
};

// vertex shader function
vertex ColorInOut lighting_vertex(
    VertexInput in [[stage_in]],
    constant FrameUniforms& frameUniforms [[ buffer(1) ]],
    constant MaterialUniforms& materialUniforms [[ buffer(2) ]])
{
    ColorInOut out;
    
	float4 in_position = float4(float3(in.position), 1.0);
    out.position = frameUniforms.modelview_projection_matrix * in_position;
    
    float3 normal = in.normal;
    float4 eye_normal = normalize(frameUniforms.normal_matrix * float4(normal, 0.0));
    float n_dot_l = dot(eye_normal.rgb, normalize(light_position));
    n_dot_l = fmax(0.0, n_dot_l);
    
    out.color = half4(materialUniforms.ambient_color + materialUniforms.diffuse_color * n_dot_l);
    
    return out;
}

// fragment shader function
fragment half4 lighting_fragment(ColorInOut in [[stage_in]])
{
    return in.color;
};

