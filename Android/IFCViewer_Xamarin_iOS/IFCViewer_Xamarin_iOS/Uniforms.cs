using System;
using Metal;
using OpenTK;
using System.Threading;

namespace IFCViewer_Xamarin_iOS
{
	struct FrameUniforms
	{
		public Matrix4 modelViewProjectionMatrix;
		public Matrix4 normalMatrix;
	}

	struct MaterialUniforms
	{
		public Vector4 ambientColor;
		public Vector4 diffuseColor;
	}

	enum BufferIndex
	{
		VertexBuffer = 0,
		FrameUniformsBuffer = 1,
		MaterialUnformsBuffer = 2,
	}
}

