using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using IFCViewer;
using SFB;
using GracesGames.SimpleFileBrowser.Scripts;

#if UNITY_64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

public class IfcEngineUnity : MonoBehaviour
{
    /// <summary>
    /// Material - Standard/Transparent
    /// </summary>
    public Material MyMaterial;

    /// <summary>
    /// Material - Standard/Opaque
    /// </summary>
    public Material MyMaterial2;

    /// <summary>
    /// Material - Unlit/Color
    /// </summary>
    public Material MyMaterial3;

    /// <summary>
    /// IFC Objects
    /// </summary>
    public Dropdown dropdownObjects;

    /// <summary>
    /// Simple File Browser => https://gracesgames.github.io/SimpleFileBrowser/ 
    /// </summary>
    public GameObject FileBrowserPrefab;

    /// <summary>
    /// Model
    /// </summary>
	private int_t _modelx86_64;

    /// <summary>
    /// Support for materials
    /// </summary>
	private IFCMaterialsBuilder _materailsBuilder = null;

    /// <summary>
    /// IFCObject-s
    /// </summary>
    private List<IFCObject> _lsIFCObjects;

    /// <summary>
    /// Min
    /// </summary>
    private Vector3 _vecMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    /// <summary>
    /// Max
    /// </summary>
    private Vector3 _vecMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

    /// <summary>
    /// Rotate
    /// </summary>
    private Vector2? _previousTouch1 = null;

    /// <summary>
    /// 0 - NA
    /// 1 - Pan
    /// 2 - Zoom
    /// </summary>
    private int _mode = 0;

    /// <summary>
    /// Pan
    /// </summary>
    private float _panSpeed = 0.005f;

    /// <summary>
    /// Zoom
    /// </summary>
    private float _zoomSpeed = 0.1f;

    /// <summary>
    /// Zoom/Pan
    /// </summary>
    private double _startDistance2Touch = float.MaxValue;

    /// <summary>
    /// Reset support - Camera
    /// </summary>
    private float _filedOfView = 0f;

    /// <summary>
    /// Reset support - Camera
    /// </summary>
    private Vector3 _cameraPos;

    // Log support
    //public delegate void LogCallbackDelegate(string msg);

    // Log support
    /*
	[MonoPInvokeCallback(typeof(LogCallbackDelegate))]
	static void LogCallback( string msg )
	{
		Debug.Log( "LOG: " + msg );
	}

	[DllImport ("ifcengine")]
	private static extern void SetLogCallback(IntPtr fp);
	*/

    /// <summary>
    /// Unity 
    /// </summary>
    void Start()
    {
#if UNITY_ANDROID
        if ((MyMaterial != null) && (MyMaterial2 != null))
        {
            Debug.Log("Start");

            dropdownObjects.ClearOptions();
            LoadModel("20160414office_model_CV2_fordesign.ifc", true);

			// JOE - TEST
			//CreateStoreyTypeList();

            if (dropdownObjects.options.Count > 0)
            {
                dropdownObjects.value = 0;
                dropdownObjects.RefreshShownValue();
            }
        }
#endif // UNITY_ANDROID

        _cameraPos = Camera.main.transform.position;
        _filedOfView = Camera.main.fieldOfView;
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 150, 50), "Open..."))
    //    {
    //        Debug.Log("Open");
    //    }
    //}

    /// <summary>
    /// Event handler
    /// </summary>
    public void OnOpenIFCFile()
    {
        Debug.Log("OnOpenIFCFile");

#if UNITY_ANDROID
        GameObject fileBrowserObject = Instantiate(FileBrowserPrefab, transform);       
        fileBrowserObject.name = "Open";                
        FileBrowser fileBrowserScript = fileBrowserObject.GetComponent<FileBrowser>();
        fileBrowserScript.SetupFileBrowser(Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight ? ViewMode.Landscape : ViewMode.Portrait);

        fileBrowserScript.OpenFilePanel(this, "OpenFileCallback", "ifc");
#endif // UNITY_ANDROID

#if UNITY_STANDALONE_WIN
        var extensions = new[] {
            new ExtensionFilter("IFC Files", "ifc" ),
            new ExtensionFilter("All Files", "*" ),
        };

        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        if (path.Length > 0)
        {
            Debug.Log("Loading: '" + path[0] + "'");

            LoadModel(path[0], false);
        }        
#endif
    }

    /// <summary>
    /// Event handler
    /// </summary>
    /// <param name="path"></param>
    private void OpenFileCallback(string path)
    {
        LoadModel(path, false);
    }

    /// <summary>
    /// Event handler
    /// </summary>
    public void OnResetButton()
    {
        Debug.Log("OnResetButton");

        /*
         * Reset the Camera
         */
        Camera.main.transform.position = _cameraPos;
        Camera.main.fieldOfView = _filedOfView;

        /*
         * Reset the GameObject-s
         */
        GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.name != "ifc")
            {
                continue;
            }

            gameObject.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Event handler; OBSOLETE
    /// </summary>
    /// <param name="dropDown"></param>
    public void OnModeValueChange(Dropdown dropDown)
    {
        //Debug.Log("OnModeValueChange:" + dropDown.value);

        //_mode = dropDown.value;
    }

    /// <summary>
    /// Unity 
    /// </summary>
    void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.touchCount == 0)
        {
            // Reset
            _previousTouch1 = null;
            _mode = 0;
            _startDistance2Touch = float.MaxValue;

            return;
        }

        /*
        * Rotate
        */
        if (Input.touchCount == 1)
        {
            // Reset Pan/Zoom mode data            
            _mode = 0;
            _startDistance2Touch = float.MaxValue;

            // Init
            if (_previousTouch1 == null)
            {
                _previousTouch1 = Input.GetTouch(0).position;

                return;
            }

            // No change
            if (_previousTouch1 == Input.GetTouch(0).position)
            {
                return;
            }

            float fDeltaX = Input.GetTouch(0).position.x - _previousTouch1.Value.x;
            float fDeltaY = Input.GetTouch(0).position.y - _previousTouch1.Value.y;

            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.name != "ifc")
                {
                    continue;
                }

                gameObject.transform.Rotate(fDeltaY, fDeltaX, 0f, Space.Self);
            }

            _previousTouch1 = Input.GetTouch(0).position;

            return;
        } // if (Input.touchCount == 1)

        /*
        * Zoom/Pan
        */
        if (Input.touchCount == 2)
        {
            // Reset Rotate mode data
            _previousTouch1 = null;

            double dDistance2Touch = Math.Sqrt(Mathf.Pow(Input.GetTouch(0).position.x - Input.GetTouch(1).position.x, 2f) + Math.Pow(Input.GetTouch(0).position.y - Input.GetTouch(1).position.y, 2f));

            if (_startDistance2Touch == float.MaxValue)
            {
                _startDistance2Touch = dDistance2Touch;

                return;
            }

            /*
            * Detect/switch the mode
            */
            if (_mode == 0)
            {
                // Init
                _mode = Math.Abs(_startDistance2Touch - dDistance2Touch) > (_startDistance2Touch * 0.05) ? 2/*Zoom*/ : 1/*Pan*/;
            }
            else
            {
                // Switch to Zoom
                if (_mode == 1)
                {
                    _mode = Math.Abs(_startDistance2Touch - dDistance2Touch) > (_startDistance2Touch * 0.05) ? 2/*Zoom*/ : 1/*Pan*/;
                }
            }

            switch (_mode)
            {
                case 1:
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            // Get movement of the finger since last frame
                            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                            // Move object across XY plane
                            Camera.main.transform.Translate(-touchDeltaPosition.x * _panSpeed, -touchDeltaPosition.y * _panSpeed, 0);
                        }
                    }
                    break;

                case 2:
                    {
                        // BEGIN https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom

                        // Store both touches.
                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        // Find the position in the previous frame of each touch.
                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        // Find the magnitude of the vector (the distance) between the touches in each frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        // Find the difference in the distances between each frame.
                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                        // Otherwise change the field of view based on the change in distance between the touches.
                        Camera.main.fieldOfView += deltaMagnitudeDiff * _zoomSpeed;

                        // Clamp the field of view to make sure it's between 0 and 180.
                        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);

                        // END https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
                    }
                    break;

                default:
                    {
                        // unknown
                    }
                    break;
            } // switch (_mode)

            return;
        } // if (Input.touchCount == 2)
    }

    /// <summary>
    /// Loads a file/resource
    /// </summary>
    void LoadModel(string strIfcFile, bool bResource)
    {
		if (_modelx86_64 != 0)
		{
			IfcEngine.x86_64.sdaiCloseModel(_modelx86_64);
			_modelx86_64 = 0;
		}		     

        GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.name != "ifc")
            {
                continue;
            }

            Destroy(gameObject);
        }

        _lsIFCObjects = new List<IFCObject>();

        if (bResource)
        {
            /*
		    * Load resource (embedded schema)
		    */
            var ifcContent = Resources.Load(strIfcFile) as TextAsset;

            var bytes = new System.Text.UTF8Encoding().GetBytes(ifcContent.text);

            /*
             * Embedded schema  
             */
			#if UNITY_ANDROID
			IfcEngine.x86.setFilter(0, IfcEngine.x86_64.flagbit21, IfcEngine.x86_64.flagbit20 + IfcEngine.x86_64.flagbit21); // force loading IFC2x3 TC1

			_modelx86_64 = IfcEngine.x86.engiOpenModelByArray(0, bytes, bytes.Length, "");
			#endif
        }
        else
        {
            /*
             * External schema
             */
            //_modelx86_64 = IfcEngine.x86_64.sdaiOpenModelBN(0, strIfcFile, "/storage/emulated/0/Download/IFC2X3_TC1.exp");

            /*
             * Embedded schema  
             */
			IfcEngine.x86_64.setFilter(0, IfcEngine.x86_64.flagbit21, IfcEngine.x86_64.flagbit20 + IfcEngine.x86_64.flagbit21); // force loading IFC2x3 TC1

			//_modelx86_64 = IfcEngine.x86_64.sdaiOpenModelBNUnicode(0, new System.Text.UTF8Encoding().GetBytes(strIfcFile), new System.Text.UTF8Encoding().GetBytes(""));

			_modelx86_64 = IfcEngine.x86_64.sdaiOpenModelBN(0, strIfcFile, "");

            // Test
            //IfcEngine.x86_64.sdaiSaveModelBN(_modelx86_64, strIfcFile + ".copy.ifc");
        }

		if (_modelx86_64 == 0)
		{
			Debug.LogError("Failed to load: " + strIfcFile);

			return;
		}

		Debug.Log("Model: " + _modelx86_64);

		_materailsBuilder = new IFCMaterialsBuilder(_modelx86_64);        

		ImportByIFCType(_modelx86_64, "IFCFURNISHINGELEMENT");
		ImportByIFCType(_modelx86_64, "IFCWALL");
		ImportByIFCType(_modelx86_64, "IFCWALLSTANDARDCASE");
		ImportByIFCType(_modelx86_64, "IFCCOLUMN");
		ImportByIFCType(_modelx86_64, "IFCWINDOW");
		ImportByIFCType(_modelx86_64, "IFCDOOR");
		ImportByIFCType(_modelx86_64, "IFCROOF");
		ImportByIFCType(_modelx86_64, "IfcBuildingElementProxy");
		ImportByIFCType(_modelx86_64, "IfcRailing");
		ImportByIFCType(_modelx86_64, "IFCBEAM");

        BuildGameObjects();

        Camera.main.transform.Translate(0, 0, -3);
    }

    /// <summary>
    /// Helper
    /// </summary>
    /// <param name="model"></param>
    /// <param name="ifcInstance"></param>
	void GenerateGeometry(int_t model, int_t ifcInstance)
	{
        IFCObject ifcObject = null;

        // -----------------------------------------------------------------------------------------------
        /*
         *  Faces
         */
        int_t setting = 0, mask = 0;
		mask += IfcEngine.x86_64.flagbit2;        //    PRECISION (32/64 bit)
		mask += IfcEngine.x86_64.flagbit3;        //    INDEX ARRAY (32/64 bit)
		mask += IfcEngine.x86_64.flagbit5;        //    NORMALS
		mask += IfcEngine.x86_64.flagbit8;        //    TRIANGLES
		mask += IfcEngine.x86_64.flagbit12;       //    WIREFRAME

		setting += 0;                          //    SINGLE PRECISION (float)
		setting += 0;                          //    32 BIT INDEX ARRAY (Int32)
		setting += IfcEngine.x86_64.flagbit5;     //    NORMALS ON
		setting += IfcEngine.x86_64.flagbit8;     //    TRIANGLES ON
		setting += 0;                          //    WIREFRAME OFF
		IfcEngine.x86_64.setFormat(model, setting, mask);

		Int32 noVertices = 0, noIndices = 0;

        Int64 iVertexBufferSize = 0;
        Int64 iIndexBufferSize = 0;
        Int64 iTransformationBufferSize = 0;
        IfcEngine.x86_64.CalculateInstance(ifcInstance, ref iVertexBufferSize, ref iIndexBufferSize, ref iTransformationBufferSize);

        noVertices = (Int32)iVertexBufferSize;
        noIndices = (Int32)iIndexBufferSize;

        if (noVertices != 0 && noIndices != 0)
		{
            if (noIndices > 65000)
            {
                Debug.Log("Found > 65000 indices: " + noIndices);

                return;
            }

            ifcObject = new IFCObject();

            ifcObject._facesVertices = new float[noVertices * 6];    //  
            IfcEngine.x86_64.UpdateInstanceVertexBuffer(ifcInstance, ifcObject._facesVertices);

            ifcObject._facesIndices = new Int32[noIndices];
            IfcEngine.x86_64.UpdateInstanceIndexBuffer(ifcInstance, ifcObject._facesIndices);

			//Int32 mode = 0,
			//startVertex = 0, startIndex = 0, primitiveCount = 0;
			//IfcEngine.x86_64.getInstanceInModelling(model, ifcInstance, mode, ref startVertex, ref startIndex, ref primitiveCount);

			//
			//  Array's vertexArray and indexArray are filled with data.
			//
			//  startIndex points to the first index in the indexArray of relevance
			//  In total 3 * primitiveCount elements on the indexArray represent all the triangles of the object.
			//
			//  startVertex is a derived value and given as some applications can get performance gain from this information.
			//			

            _lsIFCObjects.Add(ifcObject);

			List<int> lsFacesIndices = new List<int>();
			List<Int32> lsPrimitivesForFaces = new List<Int32>();

			int_t iFacesCount = IfcEngine.x86_64.getConceptualFaceCnt(ifcInstance);

			List<Int32> lsMaxIndex = new List<Int32>();

			for (Int32 iFace = 0; iFace < iFacesCount; iFace++)
			{
				int_t iStartIndexTriangles = 0;
				int_t iIndicesCountTriangles = 0;

				int_t iStartIndexLines = 0;
				int_t iIndicesCountLines = 0;

				int_t iStartIndexPoints = 0;
				int_t iIndicesCountPoints = 0;

				int_t iStartIndexFacesPolygons = 0;
				int_t iIndicesCountFacesPolygons = 0;

				int_t iValue = 0;
				IfcEngine.x86_64.getConceptualFaceEx(ifcInstance,
					iFace,
					ref iStartIndexTriangles,
					ref iIndicesCountTriangles,
					ref iStartIndexLines,
					ref iIndicesCountLines,
					ref iStartIndexPoints,
					ref iIndicesCountPoints,
					ref iStartIndexFacesPolygons,
					ref iIndicesCountFacesPolygons,
					ref iValue,
					ref iValue);

				lsMaxIndex.Add(0);

				if (iFace > 0)
				{
					lsMaxIndex[(int)iFace] = lsMaxIndex[(int)iFace - 1];
				}
				else
				{
					lsMaxIndex[(int)iFace] = 0;
				}

				if ((iIndicesCountTriangles > 0) && (lsMaxIndex[(int)iFace] < iStartIndexTriangles + iIndicesCountTriangles))
				{
					lsMaxIndex[(int)iFace] = (int)(iStartIndexTriangles + iIndicesCountTriangles);
				}

				if ((iIndicesCountLines > 0) && (lsMaxIndex[(int)iFace] < iStartIndexLines + iIndicesCountLines))
				{
					lsMaxIndex[(int)iFace] = (int)(iStartIndexLines + iIndicesCountLines);
				}

				if ((iIndicesCountPoints > 0) && (lsMaxIndex[(int)iFace] < iStartIndexPoints + iIndicesCountPoints))
				{
					lsMaxIndex[(int)iFace] = (int)(iStartIndexPoints + iIndicesCountPoints);
				}

				if ((iIndicesCountFacesPolygons > 0) && (lsMaxIndex[(int)iFace] < iStartIndexFacesPolygons + iIndicesCountFacesPolygons)) 
				{
					lsMaxIndex[(int)iFace] = (int)(iStartIndexFacesPolygons + iIndicesCountFacesPolygons); 
				}

				/*
				 * Faces
				 */
				lsPrimitivesForFaces.Add((int)iIndicesCountTriangles / 3);

				for (int_t iIndexTriangles = iStartIndexTriangles; iIndexTriangles < iStartIndexTriangles + iIndicesCountTriangles; iIndexTriangles++)
				{
					lsFacesIndices.Add(ifcObject._facesIndices[iIndexTriangles]);
				}
			} // for (Int32 iFace ...

            /*
             * Materials
             */
            ifcObject._materials = _materailsBuilder.extractMaterials(ifcInstance);
            ifcObject._materialsCount = 0;

            if (ifcObject._materials != null)
            {
                STRUCT_MATERIALS materials = ifcObject._materials;
                if (materials.next != null)
                {
                    int_t indexBufferSize = 0, indexArrayOffset = 0, j = 0;
                    while (materials != null)
                    {
                        ifcObject._materialsCount++;

                        Debug.Assert(materials.__indexBufferSize >= 0);
                        Debug.Assert(materials.__noPrimitivesForFaces == 0);
                        indexBufferSize += materials.__indexBufferSize;
                        materials.__indexArrayOffset = indexArrayOffset;
                        while ((j < iFacesCount) && (lsMaxIndex[(int)j] <= indexBufferSize))
                        {
                            materials.__noPrimitivesForFaces += lsPrimitivesForFaces[(int)j];
                            indexArrayOffset += 3 * lsPrimitivesForFaces[(int)j];
                            j++;
                        }
                        materials = materials.next;
                    }

                    Debug.Assert(j == iFacesCount && indexBufferSize == lsFacesIndices.Count);
                }
                else
                {
                    ifcObject._materialsCount++;

                    Debug.Assert(materials.__indexBufferSize == -1);
                    materials.__indexArrayOffset = 0;
                    materials.__noPrimitivesForFaces = lsFacesIndices.Count / 3;
                }
            }
        } // if (noVertices != 0 && noIndices != 0)

        // -----------------------------------------------------------------------------------------------
        /*
		* Wireframes
		*/

        noVertices = 0;
        noIndices = 0;

        if (ifcObject != null)
        {
            setting = 0;
            mask = 0;
            mask += IfcEngine.x86_64.flagbit2;        //    PRECISION (32/64 bit)
            mask += IfcEngine.x86_64.flagbit3;        //    INDEX ARRAY (32/64 bit)
            mask += IfcEngine.x86_64.flagbit5;        //    NORMALS
            mask += IfcEngine.x86_64.flagbit8;        //    TRIANGLES
            mask += IfcEngine.x86_64.flagbit12;       //    WIREFRAME

            setting += 0;                          //    SINGLE PRECISION (float)
            setting += 0;                          //    32 BIT INDEX ARRAY (Int32)
            setting += 0;                          //    NORMALS OFF
            setting += IfcEngine.x86_64.flagbit8;     //    TRIANGLES ON
            setting += IfcEngine.x86_64.flagbit12;    //    WIREFRAME ON
            IfcEngine.x86_64.setFormat(model, setting, mask);

            iVertexBufferSize = 0;
            iIndexBufferSize = 0;
            iTransformationBufferSize = 0;
            IfcEngine.x86_64.CalculateInstance(ifcInstance, ref iVertexBufferSize, ref iIndexBufferSize, ref iTransformationBufferSize);

            noVertices = (Int32)iVertexBufferSize;
            noIndices = (Int32)iIndexBufferSize;
        }

        if (noVertices != 0 && noIndices != 0)
        {
            if (noIndices > 65000)
            {
                Debug.Log("Found > 65000 indices for the faces: " + noIndices);

                return;
            }

            ifcObject._vertices = new float[noVertices * 3];
            IfcEngine.x86_64.UpdateInstanceVertexBuffer(ifcInstance, ifcObject._vertices);

            Int32[] indices = new Int32[noIndices];
            IfcEngine.x86_64.UpdateInstanceIndexBuffer(ifcInstance, indices);
            
            List<int> lsFacesPolygonsIndices = new List<int>();
            List<int> lsLinesIndices = new List<int>();
            List<int> lsPointsIndices = new List<int>();

            int_t iFacesCount = IfcEngine.x86_64.getConceptualFaceCnt(ifcInstance);

            for (Int32 iFace = 0; iFace < iFacesCount; iFace++)
            {
				int_t iStartIndexLines = 0;
				int_t iIndicesCountLines = 0;

				int_t iStartIndexPoints = 0;
				int_t iIndicesCountPoints = 0;

				int_t iStartIndexFacesPolygons = 0;
				int_t iIndicesCountFacesPolygons = 0;

				int_t iValue = 0;
                IfcEngine.x86_64.getConceptualFaceEx(ifcInstance,
                    iFace,
                    ref iValue,
                    ref iValue,
                    ref iStartIndexLines,
                    ref iIndicesCountLines,
                    ref iStartIndexPoints,
                    ref iIndicesCountPoints,                                        
                    ref iStartIndexFacesPolygons,
                    ref iIndicesCountFacesPolygons,
                    ref iValue,
                    ref iValue);

                /*
                 * Conceptual faces polygons
                 */
                Int32 iIndexWireframes = 0;
                int iLastIndex = -1;
                while (iIndexWireframes < iIndicesCountFacesPolygons)
                {
                    if ((iLastIndex >= 0) && (indices[iStartIndexFacesPolygons + iIndexWireframes] >= 0))
                    {
                        lsFacesPolygonsIndices.Add(iLastIndex);

                        lsFacesPolygonsIndices.Add(indices[iStartIndexFacesPolygons + iIndexWireframes]);
                    }

                    iLastIndex = indices[iStartIndexFacesPolygons + iIndexWireframes];
                    iIndexWireframes++;
                }

                /*
                 * Lines
                 */
				for (int_t iIndex = iStartIndexLines; iIndex < iStartIndexLines + iIndicesCountLines; iIndex++)
                {
                    if (indices[iIndex] < 0)
                    {
                        continue;
                    }

                    lsLinesIndices.Add(indices[iIndex]);
                }

                /*
                 * Points
                 */
				for (int_t iIndex = iStartIndexLines; iIndex < iStartIndexPoints + iIndicesCountPoints; iIndex++)
                {
                    lsPointsIndices.Add(indices[iIndex]);
                }
            } // for (Int32 iFace ...

            if (lsFacesPolygonsIndices.Count <= 65000)
            {
                ifcObject._facesPolygonsIndices = lsFacesPolygonsIndices.ToArray();
            }
            else
            {
                Debug.Log("Found > 65000 indices for the faces polygons: " + noIndices);
            }

            if (lsLinesIndices.Count <= 65000)
            {
                ifcObject._linesIndices = lsLinesIndices.ToArray();
            }
            else
            {
                Debug.Log("Found > 65000 indices for the lines: " + noIndices);
            }

            if (lsPointsIndices.Count <= 65000)
            {
                ifcObject._pointsIndices = lsPointsIndices.ToArray();
            }
            else
            {
                Debug.Log("Found > 65000 indices for the points: " + noIndices);
            }
        } // if (noVertices != 0 && noIndices != 0)
    }

    /// <summary>
    /// IFCObject => GameObject
    /// </summary>
    void BuildGameObjects()
    {
        if (_lsIFCObjects.Count == 0)
        {
            return;
        }

        /*
         * Calculate min/max
         */
        _vecMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        _vecMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        foreach (var ifcObject in _lsIFCObjects)
        {
            /*
             * Faces
             */
            for (int i = 0; i < ifcObject._facesVertices.Length; i += 6)
            {
                _vecMin.x = Math.Min(_vecMin.x, ifcObject._facesVertices[i]);
                _vecMin.y = Math.Min(_vecMin.y, ifcObject._facesVertices[i + 1]);
                _vecMin.z = Math.Min(_vecMin.z, ifcObject._facesVertices[i + 2]);

                _vecMax.x = Math.Max(_vecMax.x, ifcObject._facesVertices[i]);
                _vecMax.y = Math.Max(_vecMax.y, ifcObject._facesVertices[i + 1]);
                _vecMax.z = Math.Max(_vecMax.z, ifcObject._facesVertices[i + 2]);
            }

            /*
             * Conceptual faces polygons, lines and points
             */
			if (ifcObject._vertices != null) 
			{
				for (int i = 0; i < ifcObject._vertices.Length; i += 3)
				{
					_vecMin.x = Math.Min(_vecMin.x, ifcObject._vertices[i]);
					_vecMin.y = Math.Min(_vecMin.y, ifcObject._vertices[i + 1]);
					_vecMin.z = Math.Min(_vecMin.z, ifcObject._vertices[i + 2]);

					_vecMax.x = Math.Max(_vecMax.x, ifcObject._vertices[i]);
					_vecMax.y = Math.Max(_vecMax.y, ifcObject._vertices[i + 1]);
					_vecMax.z = Math.Max(_vecMax.z, ifcObject._vertices[i + 2]);
				}
			}            
        } // foreach (var ifcObject in ...

        float fMax = _vecMax.x - _vecMin.x;
        fMax = Math.Max(fMax, _vecMax.y - _vecMin.y);
        fMax = Math.Max(fMax, _vecMax.z - _vecMin.z);

        /*
         * Center and scale
         */
        foreach (var ifcObject in _lsIFCObjects)
        {
            for (int i = 0; i < ifcObject._facesVertices.Length; i += 6)
            {
                // Move => [0.0 -> X/Y/Zmin + X/Y/Zmax]
                ifcObject._facesVertices[i] -= _vecMin.x;
                ifcObject._facesVertices[i + 1] -= _vecMin.y;
                ifcObject._facesVertices[i + 2] -= _vecMin.z;

                // Center
                ifcObject._facesVertices[i] -= (_vecMax.x - _vecMin.x) / 2f;
                ifcObject._facesVertices[i + 1] -= (_vecMax.y - _vecMin.y) / 2f;
                ifcObject._facesVertices[i + 2] -= (_vecMax.z - _vecMin.z) / 2f;

                // Scale => [-1.0 -> 1.0]
                ifcObject._facesVertices[i] /= fMax / 2f;
                ifcObject._facesVertices[i + 1] /= fMax / 2f;
                ifcObject._facesVertices[i + 2] /= fMax / 2f;
            } // for (int i = ...

            if (ifcObject._vertices != null)
            {
                for (int i = 0; i < ifcObject._vertices.Length; i += 3)
                {
                    // Move => [0.0 -> X/Y/Zmin + X/Y/Zmax]
                    ifcObject._vertices[i] -= _vecMin.x;
                    ifcObject._vertices[i + 1] -= _vecMin.y;
                    ifcObject._vertices[i + 2] -= _vecMin.z;

                    // Center
                    ifcObject._vertices[i] -= (_vecMax.x - _vecMin.x) / 2f;
                    ifcObject._vertices[i + 1] -= (_vecMax.y - _vecMin.y) / 2f;
                    ifcObject._vertices[i + 2] -= (_vecMax.z - _vecMin.z) / 2f;

                    // Scale => [-1.0 -> 1.0]
                    ifcObject._vertices[i] /= fMax / 2f;
                    ifcObject._vertices[i + 1] /= fMax / 2f;
                    ifcObject._vertices[i + 2] /= fMax / 2f;
                } // for (int i = ...
            } // if ((ifcObject._wireframesVertices != null) && ...
        } // foreach (var ifcObject in ...

        /*
         * Build game objects
         */
        // Edit => Project Settings => Graphics => Always Included Shaders - Legacy Shader
        //var shader = Shader.Find("Transparent/Cutout/Diffuse");
        foreach (var ifcObject in _lsIFCObjects)
        {   /**************************************************************************************
            * Faces
            */

            if (ifcObject._materials != null)
            {
                List<Vector3> lsVertices;
                GetVertices(ifcObject._facesVertices, 6, out lsVertices);

                List<Vector3> listNormals;
                GetNormals(ifcObject._facesVertices, out listNormals);

                Mesh mesh = new Mesh();
                mesh.SetVertices(lsVertices);
                mesh.SetNormals(listNormals);

                mesh.subMeshCount = ifcObject._materialsCount;
                var meshMaterials = new Material[ifcObject._materialsCount];

                int iMesh = 0;
                var materials = ifcObject._materials;
                while (materials != null)
                {
                    Material m = new Material(materials.material.transparency < 1f ? MyMaterial : MyMaterial2);
                    //Material m = new Material(shader); // Transparent/Cutout/Diffuse - Legacy Shader
                    m.color = new Color(materials.material.ambient.R, materials.material.ambient.G, materials.material.ambient.B, (float)materials.material.transparency);
                    meshMaterials[iMesh] = m;
                    mesh.SetIndices(SubArray(ifcObject._facesIndices, 
						(Int32)materials.__indexArrayOffset, 
						(Int32)materials.__noPrimitivesForFaces * 3), 
						MeshTopology.Triangles, 
						iMesh++);

                    materials = materials.next;
                }

                mesh.RecalculateBounds();
                mesh.RecalculateNormals();

                GameObject gameObject = new GameObject();
                gameObject.name = "ifc";
                gameObject.AddComponent<MeshFilter>().mesh = mesh;
                gameObject.AddComponent<MeshRenderer>().materials = meshMaterials;
            } // if (ifcObject._materials != null)
            else
            {
                List<Vector3> lsVertices;
                GetVertices(ifcObject._facesVertices, 6, out lsVertices);

                List<Vector3> listNormals;
                GetNormals(ifcObject._facesVertices, out listNormals);

                Mesh mesh = new Mesh();
                mesh.SetVertices(lsVertices);
                mesh.SetNormals(listNormals);
                mesh.SetIndices(ifcObject._facesIndices, MeshTopology.Triangles, 0);

                mesh.RecalculateBounds();
                mesh.RecalculateNormals();

                GameObject gameObject = new GameObject();
                gameObject.name = "ifc";
                gameObject.AddComponent<MeshFilter>().mesh = mesh;
                gameObject.AddComponent<MeshRenderer>().material = MyMaterial;
            } // else if (ifcObject._materials != null)

            /**************************************************************************************
            * Conceptual faces polygons, lines and points
            */

            if (ifcObject._vertices != null)
            {
                /*
                 * Conceptual faces polygons
                 */
                if ((ifcObject._facesPolygonsIndices != null) && ((ifcObject._facesPolygonsIndices.Length > 0)))
                {
                    List<Vector3> lsVertices;
                    GetVertices(ifcObject._vertices, 3, out lsVertices);

                    Mesh mesh = new Mesh();
                    mesh.SetVertices(lsVertices);
                    mesh.SetIndices(ifcObject._facesPolygonsIndices, MeshTopology.Lines, 0);

                    mesh.RecalculateBounds();

                    GameObject gameObject = new GameObject();
                    gameObject.name = "ifc";
                    gameObject.AddComponent<MeshFilter>().mesh = mesh;
                    gameObject.AddComponent<MeshRenderer>().material = MyMaterial3;
                }

                /*
                 * Lines
                 */
                if ((ifcObject._linesIndices != null) && ((ifcObject._linesIndices.Length > 0)))
                {
                    List<Vector3> lsVertices;
                    GetVertices(ifcObject._vertices, 3, out lsVertices);

                    Mesh mesh = new Mesh();
                    mesh.SetVertices(lsVertices);
                    mesh.SetIndices(ifcObject._linesIndices, MeshTopology.Lines, 0);

                    mesh.RecalculateBounds();
                    mesh.RecalculateNormals();

                    GameObject gameObject = new GameObject();
                    gameObject.name = "ifc";
                    gameObject.AddComponent<MeshFilter>().mesh = mesh;
                    gameObject.AddComponent<MeshRenderer>().material = MyMaterial3;
                }

                /*
                 * Points
                 */
                if ((ifcObject._pointsIndices != null) && ((ifcObject._pointsIndices.Length > 0)))
                {
                    List<Vector3> lsVertices;
                    GetVertices(ifcObject._vertices, 3, out lsVertices);

                    Mesh mesh = new Mesh();
                    mesh.SetVertices(lsVertices);
                    mesh.SetIndices(ifcObject._pointsIndices, MeshTopology.Points, 0);

                    mesh.RecalculateBounds();

                    GameObject gameObject = new GameObject();
                    gameObject.name = "ifc";
                    gameObject.AddComponent<MeshFilter>().mesh = mesh;
                    gameObject.AddComponent<MeshRenderer>().material = MyMaterial3;
                }
            } // if (ifcObject._vertices != null)
        } // foreach (var ifcObject in ...

        _lsIFCObjects.Clear();
    }

    /// <summary>
    /// Helper
    /// </summary>
    /// <param name="arVertices"></param>
    /// <param name="lsVertices"></param>
	void GetVertices (float[] arVertices, int iVertexLength, out List<Vector3> lsVertices)
	{
		lsVertices = new List<Vector3>();

		for (int i = 0; i < arVertices.Length; i += iVertexLength)
		{
			lsVertices.Add(new Vector3(arVertices[i + 2], arVertices[i + 1], arVertices[i]));
		}
	}

    /// <summary>
    /// Helper
    /// </summary>
    /// <param name="arVertices"></param>
    /// <param name="lsNormals"></param>
    void GetNormals(float[] arVertices, out List<Vector3> lsNormals)
    {
        lsNormals = new List<Vector3>();

        for (int i = 0; i < arVertices.Length; i += 6)
        {
            lsNormals.Add(new Vector3(arVertices[i + 5], arVertices[i + 4], arVertices[i + 3]));
        }
    }

    /// <summary>
    /// Helper
    /// </summary>
    /// <param name="modelx86"></param>
    /// <param name="type"></param>
	void ImportByIFCType(int_t modelx86, string type)
	{
		int_t ifcInstances = IfcEngine.x86_64.sdaiGetEntityExtentBN (modelx86, type);	
		int_t noIfcInstances = IfcEngine.x86_64.sdaiGetMemberCount(ifcInstances);

		if (noIfcInstances != 0)
		{
			for (int_t i = 0; i < noIfcInstances; i++)
			{
				int_t ifcInstance = 0;
				IfcEngine.x86_64.engiGetAggrElement(ifcInstances, i, IfcEngine.x86_64.sdaiINSTANCE, out ifcInstance);

				GenerateGeometry(_modelx86_64, ifcInstance);

				string strName;

				#if UNITY_64
				IntPtr name;
				IfcEngine.x86_64.sdaiGetAttrBN(ifcInstance, "Name", IfcEngine.x86_64.sdaiUNICODE, out name);

				strName = Marshal.PtrToStringUni(name);
				#else
				IfcEngine.x86_64.sdaiGetAttrBNUnicode(ifcInstance, "Name", out strName);
				#endif

                dropdownObjects.options.Add(new Dropdown.OptionData(strName));
            }
		}
    }

    /// <summary>
    /// Helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T[] SubArray<T>(T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);

        return result;
    }

	// ****************************************************************************************************************
	// JOE - BEGIN TEST
	private List<Dictionary<string, List<IFCObject>>> storeyByTypesList = new
		List<Dictionary<string, List<IFCObject>>>();

	private void CreateStoreyTypeList()
	{
		int_t iEntityID = IfcEngine.x86_64.sdaiGetEntityExtentBN(_modelx86_64, "IfcProject");
		int_t iEntitiesCount = IfcEngine.x86_64.sdaiGetMemberCount(iEntityID);

		for (int_t iEntity = 0; iEntity < iEntitiesCount; iEntity++)
		{
			int_t iInstance = 0;
			IfcEngine.x86_64.engiGetAggrElement(iEntityID, iEntity, IfcEngine.x86_64.sdaiINSTANCE, out iInstance);

			AddChildrenItems(iInstance, "IfcSite");
			break;
		}
	}

	private void AddChildrenItems(int_t iParentInstance, string strEntityName)
	{
		Debug.Log("XR start of AddChildrenItems, strEntityName: " + strEntityName);

		// check for decomposition
		IntPtr decompositionInstance;
		IfcEngine.x86_64.sdaiGetAttrBN(iParentInstance, "IsDecomposedBy", IfcEngine.x86_64.sdaiAGGR, out decompositionInstance);

		if (decompositionInstance == IntPtr.Zero)
		{
			return;
		}

		int_t iDecompositionsCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)decompositionInstance);
		if (iDecompositionsCount == 0)
		{
			return;
		}

		for (int_t i = 0; i < iDecompositionsCount; i++)
		{
			int_t iDecompositionInstance = 0;
			IfcEngine.x86_64.engiGetAggrElement((int_t)decompositionInstance, i, IfcEngine.x86_64.sdaiINSTANCE, out iDecompositionInstance);

			if (!IsInstanceOf(iDecompositionInstance, "IFCRELAGGREGATES"))
			{
				Debug.Log("XR iDecompositionInstance not instance of IFCRELAGGREGATES");
				continue;
			}

			IntPtr objectInstances;
			IfcEngine.x86_64.sdaiGetAttrBN(iDecompositionInstance, "RelatedObjects", IfcEngine.x86_64.sdaiAGGR, out objectInstances);

			int_t iObjectsCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)objectInstances);
			for (int_t iObject = 0; iObject < iObjectsCount; iObject++)
			{
				int_t iObjectInstance = 0;
				IfcEngine.x86_64.engiGetAggrElement((int_t)objectInstances, iObject, IfcEngine.x86_64.sdaiINSTANCE, out iObjectInstance);

				if (!IsInstanceOf(iObjectInstance, strEntityName))
				{
					Debug.Log("XR iObjectInstance not instance of strEntityName");
					continue;
				}

				switch (strEntityName)
				{
				case "IfcSite":
					{
						Debug.Log("IfcSite");
						AddChildrenItems(iObjectInstance, "IfcBuilding");
					}
					break;

				case "IfcBuilding":
					{
						Debug.Log("IfcBuilding");
						AddChildrenItems(iObjectInstance, "IfcBuildingStorey");
					}
					break;

				case "IfcBuildingStorey":
					{
						Debug.Log("IfcBuilding");
						// Å¸ÀÔº°·Î ¾ÆÀÌÅÛµéÀ» ºÐ·ù, ÀúÀå
						/*
						Dictionary<string, List<IFCObject>> storeyTreeByType =
							new Dictionary<string, List<IFCObject>>();
						AddElementTreeItems2(iObjectInstance, ref storeyTreeByType);

						storeyByTypesList.Add(storeyTreeByType);

						Debug.Log("XR storeyByTypesList.Add storeyByTypesList.Count: " + storeyByTypesList.Count);
						*/
					}
					break;

					case "IfcSpace":
					{
						Debug.Log("IfcSpace");
						//AddElementTreeItems(ifcTreeItem, iObjectInstance,	parentNode);
					}
					break;

				default:
					Debug.Log("IfcSite");
					break;
				} // switch (strEntityName)
			} // for (int_t iObject = ...
		} // for (int_t iDecomposition = ...
	}

	private bool IsInstanceOf(int_t iInstance, string strType)
	{
		if (IfcEngine.x86_64.sdaiGetInstanceType(iInstance) == IfcEngine.x86_64.sdaiGetEntity(_modelx86_64, strType))
		{
			return true;
		}

		return false;
	} 

	// JOE - END TEST
	// ****************************************************************************************************************

    /*
	private String GetAndroidContextExternalFilesDir()
	{
		string path = "";

		if (Application.platform == RuntimePlatform.Android)
		{
			try
			{
				using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						path = ajo.Call<AndroidJavaObject>("getExternalFilesDir", null).Call<string>("getAbsolutePath");
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning("Error fetching native Android external storage dir: " + e.Message);
			}
		}
		return path;
	}
    
	String GetAndroidContextInternalFilesDir()
	{
		string path = "";

		if (Application.platform == RuntimePlatform.Android)
		{
			try
			{
				using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						path = ajo.Call<AndroidJavaObject>("getFilesDir").Call<string>("getAbsolutePath");
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning("Error fetching native Android internal storage dir: " + e.Message);
			}
		}
		return path;
	}
    */
}