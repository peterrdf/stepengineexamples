using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class IFCObject
{
    /// <summary>
    /// Faces
    /// </summary>
    public Int32[] _facesIndices;

    /// <summary>
    /// Faces
    /// </summary>
    public float[] _facesVertices;

    /// <summary>
    /// Faces - materials
    /// </summary>
    public STRUCT_MATERIALS _materials;

    /// <summary>
    /// Faces - materials
    /// </summary>
    public int _materialsCount;

    /// <summary>
    /// Conceptual faces polygons, lines and points
    /// </summary>
    public float[] _vertices;

    /// <summary>
    /// Conceptual faces polygons
    /// </summary>
    public Int32[] _facesPolygonsIndices;

    /// <summary>
    /// Lines
    /// </summary>
    public Int32[] _linesIndices;

    /// <summary>
    /// Points
    /// </summary>
    public Int32[] _pointsIndices;
}
