using Metal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCViewer_Xamarin_iOS
{
    /// <summary>
    /// An IFC item
    /// </summary>
    public class IFCItem
    {
        #region Constants

        /// <summary>
        /// circleSegments()
        /// </summary>
        public const int DEFAULT_CIRCLE_SEGMENTS = 36;

        #endregion // Constants

        #region Fields

        /// <summary>
        /// IFC instance
        /// </summary>
        public Int64 _instance = 0;

        /// <summary>
        /// IFC type
        /// </summary>
        public string _ifcType;

        /// <summary>
        /// Unique 1-based index
        /// </summary>
        public Int64 _ID;

        /// <summary>
        /// Faces, Conceptual faces polygons, lines and points
        /// </summary>
        public float[] _vertices;

        /// <summary>
        /// Metal
        /// </summary>
        public IMTLBuffer _metalVerticesBuffer;

        /// <summary>
        /// Faces
        /// </summary>
        public int[] _facesIndices;

        /// <summary>
        /// Metal
        /// </summary>
        public IMTLBuffer _metalFacesIndicesBuffer;

        /// <summary>
        /// Conceptual faces polygons
        /// </summary>
        public int[] _facesPolygonsIndices;

        /// <summary>
        /// Metal
        /// </summary>
        public IMTLBuffer _metalFaceslPolygonsBuffer;

        /// <summary>
        /// Lines
        /// </summary>
        public int[] _linesIndices;

        /// <summary>
        /// Points
        /// </summary>
        public int[] _pointsIndices;

        /// <summary>
        /// The first argument for circleSegments()
        /// </summary>
        public Int64 circleSegments = DEFAULT_CIRCLE_SEGMENTS;

        /// <summary>
        /// Show/hide
        /// </summary>
        public bool _visible = false;

        /// <summary>
        /// Materials
        /// </summary>
        public Dictionary<IFCMaterial, List<KeyValuePair<Int64, Int64>>> _materials = new Dictionary<IFCMaterial, List<KeyValuePair<Int64, Int64>>>(new IFCMaterialComparer());

        #endregion // Fields

        /// <summary>
        /// Getter
        /// </summary>
        public bool hasGeometry
        {
            get
            {
                return _vertices != null;
            }
        }
    }
}

