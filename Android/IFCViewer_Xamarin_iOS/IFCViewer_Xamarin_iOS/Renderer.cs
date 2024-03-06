using System;
using Metal;
using OpenTK;
using System.Threading;
using Foundation;
using System.Runtime.InteropServices;
using CoreAnimation;
using System.Collections.Generic;
using System.Diagnostics;
using ModelIO;

namespace IFCViewer_Xamarin_iOS
{
	public class Renderer : IGameViewController, IGameView
	{
		const int DEFAULT_MATERIAL_INDEX = 0;
		const int LINES_MATERIAL_INDEX = 1;

		const int MAX_INFLIGHT_BUFFERS = 3;
		const int MAX_BYTES_PER_FRAME = 1024 * 1024;

		float FOVY = 65.0f;
		Vector3 _eye = new Vector3 (0.0f, 0.0f, 0.0f);
		Vector3 _center = new Vector3 (0.0f, 0.0f, 1.0f);
		Vector3 _up = new Vector3 (0.0f, 1.0f, 0.0f);

		float _rotation;		

		MTLPixelFormat _depthPixelFormat;
		MTLPixelFormat _stencilPixelFormat;
		nuint _sampleCount;

		IMTLDevice _device;
		IMTLCommandQueue _commandQueue;
		IMTLLibrary _defaultLibrary;

		Semaphore _inflightSemaphore;

		int _frameUniformsBufferIndex;
		IMTLBuffer[] _frameUniformsBuffer;
		IMTLBuffer[] _materialUniformsBuffers;

		// render stage
		IMTLRenderPipelineState _pipelineState;
		
		IMTLDepthStencilState _depthState;

		// global transform data
		Matrix4 _projectionMatrix;
		Matrix4 _viewMatrix;

		// Model
		IfcModel _ifcModel;
		
		public Renderer (IfcModel ifcModel)
		{
			_ifcModel = ifcModel;

			_sampleCount = 4;
			_depthPixelFormat = MTLPixelFormat.Depth32Float;
			_stencilPixelFormat = MTLPixelFormat.Invalid;

			// find a usable Device
			_device = MTLDevice.SystemDefault;

			// create a new command queue
			_commandQueue = _device.CreateCommandQueue ();

			NSError error;
			_defaultLibrary = _device.CreateLibrary ("default.metallib", out error);

			// if the shader library isn't loading, nothing good will happen
			if (_defaultLibrary == null)
				throw new Exception ("ERROR: Couldn't create a default shader library");

			_frameUniformsBufferIndex = 0;
			_inflightSemaphore = new Semaphore (MAX_INFLIGHT_BUFFERS, MAX_INFLIGHT_BUFFERS);
		}

		public void RenderViewControllerUpdate (GameViewController gameViewController)
		{
		}

		public void RenderViewController(GameViewController gameViewController, bool value)
		{
			// timer is suspended/resumed
			// Can do any non-rendering related background work here when suspended
		}

		public void Reshape (GameView view)
		{
			var aspect = (float)(view.Bounds.Size.Width / view.Bounds.Size.Height);
			_projectionMatrix = CreateMatrixFromPerspective (FOVY, aspect, 0.1f, 100.0f);
			_viewMatrix = LookAt (_eye, _center, _up);
		}

		public void Render (GameView view)
		{
			_inflightSemaphore.WaitOne ();

			PrepareToDraw ();

			IMTLCommandBuffer commandBuffer = _commandQueue.CommandBuffer ();
			ICAMetalDrawable drawable = view.GetNextDrawable ();
			MTLRenderPassDescriptor renderPassDescriptor = view.GetRenderPassDescriptor (drawable);

			if (renderPassDescriptor == null)
				Console.WriteLine ("ERROR: Failed to get render pass descriptor from view!");

			IMTLRenderCommandEncoder renderEncoder = commandBuffer.CreateRenderCommandEncoder (renderPassDescriptor);
			renderEncoder.SetDepthStencilState (_depthState);
			

			foreach (var ifcItem in _ifcModel.getIFCtems())
			{
				if (ifcItem.Value._metalVerticesBuffer == null)
				{
					continue;
				}

				RenderFaces(renderEncoder, 0, ifcItem.Value);
				RenderWireframes(renderEncoder, 0, ifcItem.Value);
			}

			renderEncoder.EndEncoding ();

			commandBuffer.AddCompletedHandler ((IMTLCommandBuffer buffer) => {
				drawable.Dispose ();
				_inflightSemaphore.Release ();
			});

			commandBuffer.PresentDrawable (drawable);
			commandBuffer.Commit ();

			_frameUniformsBufferIndex = (_frameUniformsBufferIndex + 1) % MAX_INFLIGHT_BUFFERS;
		}

		public void Configure (GameView view)
		{
			view.DepthPixelFormat = _depthPixelFormat;
			view.StencilPixelFormat = _stencilPixelFormat;
			view.SampleCount = _sampleCount;

			_frameUniformsBuffer = new IMTLBuffer[MAX_INFLIGHT_BUFFERS];

			// allocate one region of memory for the constant buffer
			for (int i = 0; i < MAX_INFLIGHT_BUFFERS; i++)
			{
				_frameUniformsBuffer[i] = _device.CreateBuffer(MAX_BYTES_PER_FRAME, MTLResourceOptions.CpuCacheModeDefault);
				_frameUniformsBuffer[i].Label = string.Format("ConstantBuffer{0}", i);
			}

			// load the fragment program into the library
			IMTLFunction fragmentProgram = _defaultLibrary.CreateFunction ("lighting_fragment");
			if (fragmentProgram == null)
				Console.WriteLine ("ERROR: Couldn't load fragment function from default library");

			// load the vertex program into the library
			IMTLFunction vertexProgram = _defaultLibrary.CreateFunction ("lighting_vertex");
			if (vertexProgram == null)
				Console.WriteLine ("ERROR: Couldn't load vertex function from default library");

			/*
			 * Build the buffers
			 */
			List<IMTLBuffer> materialUniformsBuffers = new List<IMTLBuffer>();

			/*
			 * Default material
			 */
			var buffer = _device.CreateBuffer((nuint)Marshal.SizeOf<MaterialUniforms>(), MTLResourceOptions.CpuCacheModeDefault);

			var materialUniforms = Marshal.PtrToStructure<MaterialUniforms>(buffer.Contents);

			materialUniforms.ambientColor = new Vector4(
				0,
				1,
				0,
				1);

			materialUniforms.diffuseColor = new Vector4(
				0,
				1,
				0,
				1);

			Marshal.StructureToPtr(materialUniforms, buffer.Contents, true);

			materialUniformsBuffers.Add(buffer);

			/*
			 * Wireframes material
			 */
			buffer = _device.CreateBuffer((nuint)Marshal.SizeOf<MaterialUniforms>(), MTLResourceOptions.CpuCacheModeDefault);

			materialUniforms = Marshal.PtrToStructure<MaterialUniforms>(buffer.Contents);

			materialUniforms.ambientColor = new Vector4(
				0,
				0,
				0,
				1);

			materialUniforms.diffuseColor = new Vector4(
				0,
				0,
				0,
				1);

			Marshal.StructureToPtr(materialUniforms, buffer.Contents, true);

			materialUniformsBuffers.Add(buffer);

			foreach (var ifcItem in _ifcModel.getIFCtems())
			{
				/*
			    * Check for geometry
			    */
				if ((ifcItem.Value._vertices == null) || (ifcItem.Value._vertices.Length == 0))
				{
					continue;
				}

				/*
				 * Metal Vertex Buffer 
				 */ 
				ifcItem.Value._metalVerticesBuffer = CreateMetalVertexBuffer(ifcItem.Value._vertices, 10);
				ifcItem.Value._metalVerticesBuffer.Label = string.Format("Vertices {0}", ifcItem.Value._instance);

				/*
			    * Faces
			    */
				if ((ifcItem.Value._facesIndices != null) && (ifcItem.Value._facesIndices.Length > 0))
				{
					/*
					 * Metal Index Buffer 
					 */
					ifcItem.Value._metalFacesIndicesBuffer = CreateMetalIndexBuffer(ifcItem.Value._facesIndices);
					ifcItem.Value._metalFacesIndicesBuffer.Label = string.Format("Indices {0}", ifcItem.Value._instance);

					/*
					 * Materials
					 */  
					if (ifcItem.Value._materials != null)
					{
						foreach (var materialInfo in ifcItem.Value._materials)
						{
							/*
							 * Material
							 */
							var material = materialInfo.Key;

							material.MetalBufferID = materialUniformsBuffers.Count;

							buffer = _device.CreateBuffer((nuint)Marshal.SizeOf<MaterialUniforms>(), MTLResourceOptions.CpuCacheModeDefault);

							materialUniforms = Marshal.PtrToStructure<MaterialUniforms>(buffer.Contents);

							materialUniforms.ambientColor = new Vector4(
								material.Ambient.R,
								material.Ambient.G,
								material.Ambient.B,
								material.A);

							materialUniforms.diffuseColor = new Vector4(
								material.Diffuse.R,
								material.Diffuse.G,
								material.Diffuse.B,
								material.A);

							Marshal.StructureToPtr(materialUniforms, buffer.Contents, true);

							materialUniformsBuffers.Add(buffer);
						} // foreach (var materialInfo in ...
					} // if (ifcItem.Value._materials != null)
				} // if ((ifcItem.Value._facesIndices != null) && ...

				/*
			     * Conceptual faces polygons
			     */
				if ((ifcItem.Value._facesPolygonsIndices != null) && ((ifcItem.Value._facesPolygonsIndices.Length > 0)))
				{
					/*
					 * Metal Index Buffer 
					 */
					ifcItem.Value._metalFaceslPolygonsBuffer = CreateMetalIndexBuffer(ifcItem.Value._facesPolygonsIndices);
					ifcItem.Value._metalFaceslPolygonsBuffer.Label = string.Format("Indices {0}", ifcItem.Value._instance);
				}

				/*
				 * Lines
				 */
				//if ((ifcItem.Value._linesIndices != null) && ((ifcItem.Value._linesIndices.Length > 0)))
				//{
				//}

				/*
				 * Points
				 */
				//if ((ifcItem.Value._pointsIndices != null) && ((ifcItem.Value._pointsIndices.Length > 0)))
				//{
				//}
			} // foreach (var ifcItem in ...

			_materialUniformsBuffers = materialUniformsBuffers.ToArray();

			var mtlVertexDescriptor = new MTLVertexDescriptor();
			mtlVertexDescriptor.Attributes[0].Format = MTLVertexFormat.Float3;
			mtlVertexDescriptor.Attributes[0].BufferIndex = (nuint)(int)BufferIndex.VertexBuffer;
			mtlVertexDescriptor.Attributes[0].Offset = 0;
			mtlVertexDescriptor.Attributes[1].Format = MTLVertexFormat.Float3;
			mtlVertexDescriptor.Attributes[1].BufferIndex = (nuint)(int)BufferIndex.VertexBuffer;
			mtlVertexDescriptor.Attributes[1].Offset = 12;
			mtlVertexDescriptor.Layouts[0].Stride = 24;
			mtlVertexDescriptor.Layouts[0].StepRate = 1;
			mtlVertexDescriptor.Layouts[0].StepFunction = MTLVertexStepFunction.PerVertex;

			//  create a reusable pipeline state
			var pipelineStateDescriptor = new MTLRenderPipelineDescriptor {
				Label = "MyPipeline",
				SampleCount = _sampleCount,
				VertexFunction = vertexProgram,
				FragmentFunction = fragmentProgram,
				DepthAttachmentPixelFormat = _depthPixelFormat,
				VertexDescriptor = mtlVertexDescriptor
			};

			var coloAttachment = pipelineStateDescriptor.ColorAttachments[0];
			coloAttachment.PixelFormat = MTLPixelFormat.BGRA8Unorm;
			coloAttachment.BlendingEnabled = true;
			coloAttachment.RgbBlendOperation = MTLBlendOperation.Add;
			coloAttachment.AlphaBlendOperation = MTLBlendOperation.Add;
			coloAttachment.SourceRgbBlendFactor = MTLBlendFactor.SourceAlpha;
			coloAttachment.SourceAlphaBlendFactor = MTLBlendFactor.SourceAlpha;
			coloAttachment.DestinationRgbBlendFactor = MTLBlendFactor.OneMinusSourceAlpha;
			coloAttachment.DestinationAlphaBlendFactor = MTLBlendFactor.OneMinusSourceAlpha;

			NSError error;
			_pipelineState = _device.CreateRenderPipelineState (pipelineStateDescriptor, out error);

			var depthStateDesc = new MTLDepthStencilDescriptor {
				DepthCompareFunction = MTLCompareFunction.Less,
				DepthWriteEnabled = true
			};

			_depthState = _device.CreateDepthStencilState (depthStateDesc);
		}

		void PrepareToDraw()
		{
			Matrix4 baseModel = CreateMatrixFromTranslation(0.0f, 0.0f, 2.0f) * CreateMatrixFromRotation(_rotation, 0.0f, 1.0f, 0.0f);
			Matrix4 baseMv = _viewMatrix * baseModel;
			Matrix4 modelViewMatrix = CreateMatrixFromTranslation(0.0f, 0.0f, 2.0f) * CreateMatrixFromRotation(_rotation, 1.0f, 1.0f, 1.0f);
			modelViewMatrix = baseMv * modelViewMatrix;

			Matrix4 normalMatrix = Matrix4.Invert(Matrix4.Transpose(modelViewMatrix));
			Matrix4 modelviewProjectionMatrix = Matrix4.Transpose(_projectionMatrix * modelViewMatrix);

			var frameUniforms = Marshal.PtrToStructure<FrameUniforms>(_frameUniformsBuffer[_frameUniformsBufferIndex].Contents);

			frameUniforms.modelViewProjectionMatrix = modelviewProjectionMatrix;
			frameUniforms.normalMatrix = normalMatrix;

			Marshal.StructureToPtr(frameUniforms, _frameUniformsBuffer[_frameUniformsBufferIndex].Contents, true);

			_rotation += .015f;
		}

		void RenderFaces(IMTLRenderCommandEncoder renderEncoder, nuint offset, IFCItem ifcItem)
		{
			/*
			* Check for geometry
			*/
			if ((ifcItem._metalVerticesBuffer == null) || (ifcItem._metalFacesIndicesBuffer == null))
			{
				return;
			}

			/*
			 * Debug
			 */ 
			renderEncoder.PushDebugGroup(string.Format("Faces {0}", ifcItem._instance));

			/* 
			* Context
			*/
			renderEncoder.SetRenderPipelineState(_pipelineState);
			renderEncoder.SetVertexBuffer(ifcItem._metalVerticesBuffer, 0, (nuint)(int)BufferIndex.VertexBuffer);
			renderEncoder.SetVertexBuffer(_frameUniformsBuffer[_frameUniformsBufferIndex], 0, (nuint)(int)BufferIndex.FrameUniformsBuffer);

			/*
			* Faces
			*/
			if (ifcItem._materials != null)
			{
				foreach (var materialInfo in ifcItem._materials)
				{
					/*
					 * Material
					 */
					var material = materialInfo.Key;

					renderEncoder.SetVertexBuffer(_materialUniformsBuffers[material.MetalBufferID], 0, (nuint)(int)BufferIndex.MaterialUnformsBuffer);

					/*
					 * Render
					 */
					renderEncoder.DrawIndexedPrimitives(
						MTLPrimitiveType.Triangle,
						(nuint)material.IndicesCount,
						MTLIndexType.UInt16,
						ifcItem._metalFacesIndicesBuffer,
						(nuint)material.IndicesOffset);
				} // foreach (var materialInfo in ...
			} // if (ifcItem._materials != null)
			else
			{
				renderEncoder.SetVertexBuffer(_materialUniformsBuffers[DEFAULT_MATERIAL_INDEX], 0, (nuint)(int)BufferIndex.MaterialUnformsBuffer);

				/*
				 * Render
				 */
				renderEncoder.DrawIndexedPrimitives(
					MTLPrimitiveType.Triangle,
					(nuint)ifcItem._facesIndices.Length,
					MTLIndexType.UInt16,
					ifcItem._metalFacesIndicesBuffer,
					0);
			} // else if (ifcItem._materials != null)

			/*
			 * Debug
			 */
			renderEncoder.PopDebugGroup();			
		}

		void RenderWireframes(IMTLRenderCommandEncoder renderEncoder, nuint offset, IFCItem ifcItem)
		{
			/*
			* Check for geometry
			*/
			if ((ifcItem._metalVerticesBuffer == null) || (ifcItem._metalFaceslPolygonsBuffer == null))
			{
				return;
			}

			/*
			 * Debug
			 */
			renderEncoder.PushDebugGroup(string.Format("Indices {0}", ifcItem._instance));

			/* 
			* Context
			*/
			renderEncoder.SetRenderPipelineState(_pipelineState);
			renderEncoder.SetVertexBuffer(ifcItem._metalVerticesBuffer, 0, (nuint)(int)BufferIndex.VertexBuffer);
			renderEncoder.SetVertexBuffer(_frameUniformsBuffer[_frameUniformsBufferIndex], 0, (nuint)(int)BufferIndex.FrameUniformsBuffer);
			renderEncoder.SetVertexBuffer(_materialUniformsBuffers[LINES_MATERIAL_INDEX], 0, (nuint)(int)BufferIndex.MaterialUnformsBuffer);

			/*
			 * Render
			 */
			renderEncoder.DrawIndexedPrimitives(
				MTLPrimitiveType.Line,
				(nuint)ifcItem._facesPolygonsIndices.Length,
				MTLIndexType.UInt16,
				ifcItem._metalFaceslPolygonsBuffer,
				0);
		}

		Matrix4 CreateMatrixFromPerspective (float fovY, float aspect, float near, float far)
		{
			float yscale = 1.0f / (float)Math.Tan (fovY * 0.5f);
			float xscale = yscale / aspect;
			float zScale = far / (far - near);

			var m = new Matrix4 {
				Row0 = new Vector4 (xscale, 0.0f, 0.0f, 0.0f),
				Row1 = new Vector4 (0.0f, yscale, 0.0f, 0.0f),
				Row2 = new Vector4 (0.0f, 0.0f, zScale, -near * zScale),
				Row3 = new Vector4 (0.0f, 0.0f, 1.0f, 0.0f)
			};

			return m;
		}

		Matrix4 CreateMatrixFromTranslation (float x, float y, float z)
		{
			var m = Matrix4.Identity;
			m.Row0.W = x;
			m.Row1.W = y;
			m.Row2.W = z;
			m.Row3.W = 1.0f;
			return m;
		}

		Matrix4 CreateMatrixFromRotation (float radians, float x, float y, float z)
		{
			Vector3 v = Vector3.Normalize (new Vector3 (x, y, z));
			var cos = (float)Math.Cos (radians);
			var sin = (float)Math.Sin (radians);
			float cosp = 1.0f - cos;

			var m = new Matrix4 {
				Row0 = new Vector4 (cos + cosp * v.X * v.X, cosp * v.X * v.Y - v.Z * sin, cosp * v.X * v.Z + v.Y * sin, 0.0f),
				Row1 = new Vector4 (cosp * v.X * v.Y + v.Z * sin, cos + cosp * v.Y * v.Y, cosp * v.Y * v.Z - v.X * sin, 0.0f),
				Row2 = new Vector4 (cosp * v.X * v.Z - v.Y * sin, cosp * v.Y * v.Z + v.X * sin, cos + cosp * v.Z * v.Z, 0.0f),
				Row3 = new Vector4 (0.0f, 0.0f, 0.0f, 1.0f)
			};

			return m;
		}

		Matrix4 LookAt (Vector3 eye, Vector3 center, Vector3 up)
		{
			Vector3 zAxis = Vector3.Normalize (center - eye);
			Vector3 xAxis = Vector3.Normalize (Vector3.Cross (up, zAxis));
			Vector3 yAxis = Vector3.Cross (zAxis, xAxis);

			var P = new Vector4 (xAxis.X, yAxis.X, zAxis.X, 0.0f);
			var Q = new Vector4 (xAxis.Y, yAxis.Y, zAxis.Y, 0.0f);
			var R = new Vector4 (xAxis.Z, yAxis.Z, zAxis.Z, 0.0f);
			var S = new Vector4 (Vector3.Dot (xAxis, eye), Vector3.Dot (yAxis, eye), Vector3.Dot (zAxis, eye), 1.0f);

			var result = new Matrix4 (P, Q, R, S);
			return Matrix4.Transpose (result);
		}

		private IMTLBuffer CreateMetalVertexBuffer(float[] arVertices, int iVertexLength)
		{
			List<float> lsVertices = new List<float>();

			for (int i = 0; i < arVertices.Length; i += iVertexLength)
			{
				lsVertices.Add(arVertices[i + 2]);
				lsVertices.Add(arVertices[i + 1]);
				lsVertices.Add(arVertices[i + 0]);

				lsVertices.Add(arVertices[i + 5]);
				lsVertices.Add(arVertices[i + 4]);
				lsVertices.Add(arVertices[i + 3]);
			}

			return _device.CreateBuffer(lsVertices.ToArray(), MTLResourceOptions.CpuCacheModeDefault);
		}

		private IMTLBuffer CreateMetalIndexBuffer(int[] indices)
		{
			List<short> lsIndices = new List<short>();

			for (int index = 0; index < indices.Length; index++)
			{
				lsIndices.Add((short)indices[index]);
			}

			return _device.CreateBuffer(lsIndices.ToArray(), MTLResourceOptions.CpuCacheModeDefault);
		}
	}
}

