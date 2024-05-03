using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if _WIN64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

namespace IFCViewerSGL
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
        /// Unique 1-based index
        /// </summary>
        public int_t _ID;

        /// <summary>
        /// Faces, Conceptual faces polygons, lines and points
        /// </summary>
        public float[] _vertices;

        /// <summary>
        /// Conceptual faces polygons
        /// </summary>
        public uint[] _facesPolygonsIndices;

        /// <summary>
        /// Lines
        /// </summary>
        public uint[] _linesIndices;

        /// <summary>
        /// Points
        /// </summary>
        public uint[] _pointsIndices;

        /// <summary>
        /// Faces
        /// </summary>
        public int[] _facesIndices;

        /// <summary>
        /// Materials
        /// </summary>
        public int_t _noPrimitivesForFaces;

        /// <summary>
        /// Materials
        /// </summary>
        public STRUCT_MATERIALS _materials = null;

        /// <summary>
        /// IFC instance
        /// </summary>
        public int_t _instance = 0;

        /// <summary>
        /// IFC type
        /// </summary>
        public string _ifcType; 

        /// <summary>
        /// The first argument for circleSegments()
        /// </summary>
        public int_t circleSegments = DEFAULT_CIRCLE_SEGMENTS;

        /// <summary>
        /// Selection
        /// </summary>
        public STRUCT_COLOR _selectionColor = null;

        /// <summary>
        /// Show/hide
        /// </summary>
        public bool _visible = false;

        public int_t offsetFaceLines = 0;

        

        /// <summary>
        /// The visual presentation of an IFC item
        /// </summary>
        public IIFCItemView _view = null;

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
