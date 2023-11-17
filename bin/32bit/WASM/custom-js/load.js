var Module = {
  onRuntimeInitialized: function () {
    console.log('onRuntimeInitialized')
    
    loadSceneInstances()
    loadNavigatorInstances()

    printRuleSets()
  },
}

function embeddedMode() {
  try {
    return window.self !== window.top;
  } catch (e) {
    return true;
  }
}

function getFileExtension(file) {
  if (file && file.length > 4) {
    return file.substr(file.length - 3, 3).toLowerCase();
  }

  return null;
}

function loadContent(fileName, fileExtension, fileContent) {
  g_instances = [];
  g_geometries = [];

  Module.unload();
  Module['FS_createDataFile']('/data/', 'input.ifc', fileContent, true, true);

  if (fileExtension === 'dxf') {
    Module.loadDXF(true, true)
  }
  else if ((fileExtension === 'bin') || (fileExtension == 'rdf')) {
    Module.loadBIN(true, true);
  }
  else if ((fileExtension == 'dae') || (fileExtension == 'zae')) {
    Module.loadDAE(true, true)
  }
  else if (fileExtension == 'obj') {
    Module.loadOBJ(true, true)
  }
  else {
    Module.loadSTEP(true, true)
  }

  FS.unlink('/data/' + 'input.ifc')

  loadInstances();
  updateFileNameField(fileName);
  loadAllIFCTrees();
}

function loadZAE(fileName, data) {
  var jsZip = new JSZip();
  jsZip.loadAsync(data).then(function (zip) {
    let daeFile = getDAEFile(zip);
    if (daeFile) {
      zip.file(daeFile).async('string').then(function (fileContent) {
        loadContent(fileName, 'dae', fileContent);

        var textureCnt = Module.getTextureCnt();
        for (let t = 0; t < textureCnt; t++) {
          var textureName = Module.getTextureInfo(t + 1);
          loadTexture(zip, textureName);
        }
      });
    }
  });
}

function loadFile(file) {
  resetFields()

  var fileReader = new FileReader()
  fileReader.onload = function () {
    var fileContent = new Uint8Array(fileReader.result);

    var fileExtension = getFileExtension(file.name);
    if (fileExtension === 'zae') {
      try {
        loadZAE(file.name, fileContent)
      }
      catch (e) {
        console.error(e);
      }
    }
    else {
      loadContent(file.name, fileExtension, fileContent);
    }
  }

  fileReader.readAsArrayBuffer(file)
}

function loadInstances() {
  try {
    Module.createCache()

    g_viewer._hasTexures = false

    let texturesCount = Module.getTextureCnt()
    console.log('Textures Count: ' + texturesCount)

    var geometryCnt = Module.getGeometryCnt()
    console.log('Geometries Count: ' + geometryCnt)

    for (let g = 0; g < geometryCnt; g++) {
      let geometry = {
        id: Module.getIndexGeometryItem(g),
        vertices: [],
        conceptualFaces: [],
        conceptualFacesPolygons: [],
        vertexSizeInBytes: texturesCount > 0 ? 32 : 24,
      }            

      // Vertices
      let vertices = texturesCount > 0 ?
        Module.getGeometryItemVerticesWithTextureCoordinates(geometry.id) :
        Module.getGeometryItemVertices(geometry.id)
      let vertexCnt = vertices.size()     
      for (let v = 0; v < vertexCnt; v++) {
        geometry.vertices.push(vertices.get(v))
      }

      // Faces
      var faceCnt = Module.getFaceCnt(geometry.id)
      for (let group = 0; group < faceCnt; group++) {
        let material = Module.getFaceMaterial(geometry.id, group)
        let textureIndex = Module.getFaceTexture(geometry.id, group)
        let conceptualFace = {
          material: {
            ambient: [material.get(0), material.get(1), material.get(2)],
            diffuse: [material.get(3), material.get(4), material.get(5)],
            specular: [material.get(6), material.get(7), material.get(8)],
            emissive: [material.get(9), material.get(10), material.get(10)],
            transparency: material.get(12),
          },
          indicesTriangles: [],
          indicesLines: [],
          indicesPoints: [],
        }

        if (textureIndex > 0) {
          conceptualFace.material.texture = {}
          conceptualFace.material.texture.name = Module.getTextureInfo(textureIndex)
        }

        let indices = Module.getFaceTriangleIndices(geometry.id, group)
        let indicesSize = indices.size()
        for (let i = 0; i < indicesSize; i++) {
          conceptualFace.indicesTriangles.push(indices.get(i))
        }

        indices = Module.getFaceEdgeIndices(geometry.id, group)
        indicesSize = indices.size()
        for (let i = 0; i < indicesSize; i++) {
          conceptualFace.indicesLines.push(indices.get(i))
        }

        indices = Module.getFacePointIndices(geometry.id, group)
        indicesSize = indices.size()
        for (let i = 0; i < indicesSize; i++) {
          conceptualFace.indicesPoints.push(indices.get(i))
        }

        geometry.conceptualFaces.push(conceptualFace)
      } // for (let group = ...

      // Wireframes
      var wireframeCnt = Module.getWireframeCnt(geometry.id)
      for (let group = 0; group < wireframeCnt; group++) {
        let wireframes = {
          indices: [],
        }

        let indicesWF = Module.getWireframeIndices(geometry.id, group)
        let mySizeWF = indicesWF.size()
        for (var i = 0; i < mySizeWF; i++) {
          wireframes.indices.push(indicesWF.get(i))
        }

        geometry.conceptualFacesPolygons.push(wireframes)
      } // for (let group = ...

      g_geometries.push(geometry)
    } // for (let g = ...

    var instanceCnt = Module.getInstanceCnt()
    console.log('Instances Count: ' + instanceCnt)

    for (let i = 0; i < instanceCnt; i++) {
      var bbox = Module.getInstanceBBox(i)

      var instance = {
        uri: Module.getInstanceUri(i),
        guid: Module.getInstanceGuid(i),
        label: Module.getInstanceLabel(i),
        visible: true,
        Xmin: bbox.get(0),
        Ymin: bbox.get(1),
        Zmin: bbox.get(2),
        Xmax: bbox.get(3),
        Ymax: bbox.get(4),
        Zmax: bbox.get(5),
        geometry: [],
        matrix: [],
      }

      let instanceGeometryCnt = Module.getInstanceGemetryCnt(i)
      for (let r = 0; r < instanceGeometryCnt; r++) {
        let instanceGeometryRef = Module.getInstanceGemetryRef(i, r)        
        instance.geometry.push(instanceGeometryRef)

        let matrix = []
        let instanceGemetryMatrix = Module.getInstanceGemetryMatrix(i, g_geometries[instanceGeometryRef])
        if (!!instanceGemetryMatrix && instanceGemetryMatrix.size() === 16) {
          for (let i = 0; i < instanceGemetryMatrix.size(); i++) {
            matrix.push(instanceGemetryMatrix.get(i))
          }
        }

        instance.matrix.push(matrix)
      } // for (let r = ...

      g_instances.push(instance)
    } // for (let i = ...

    g_viewer._hasTexures = texturesCount > 0
    g_viewer._scaleFactor = Module.getScale()
    g_viewer.loadInstances()
  }
  catch (ex) {
    console.error(ex)
  }
}

function loadSceneInstances() {
  Module.loadCoordinateSystem()

  loadInstances()

  for (let i = 0; i < g_instances.length; i++) {
    g_sceneInstances.push(g_instances[i]);
  }

  for (let g = 0; g < g_geometries.length; g++) {
    g_sceneGeometries.push(g_geometries[g]);
  }

  g_instances = []
  g_geometries = []
}

function loadNavigatorInstances() {
  Module.loadNavigator()

  loadInstances()

  for (let i = 0; i < g_instances.length; i++) {
    g_navigatorInstances.push(g_instances[i]);
  }

  for (let g = 0; g < g_geometries.length; g++) {
    g_navigatorGeometries.push(g_geometries[g]);
  }

  g_instances = []
  g_geometries = []
}

function clearFields() { }

function readFileByUri(file, callback) {
  var rawFile = new XMLHttpRequest();
  rawFile.open('GET', file, true);
  rawFile.setRequestHeader('Content-Type', 'text/xml');
  rawFile.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
  rawFile.setRequestHeader('Access-Control-Allow-Origin', '*');
  rawFile.onreadystatechange = function () {
    if (rawFile.readyState === 4 && rawFile.status === 200) {
      callback(rawFile.responseText);
    }
  }
  rawFile.send(null);
}

function getDAEFile(zip) {
  if (zip) {
    for (let [fileName] of Object.entries(zip.files)) {
      if (getFileExtension(fileName) === 'dae') {
        return fileName;
      }
    }
  }

  return null;
}

function loadTexture(zip, textureName) {
  if (zip) {
    zip.file(textureName).async('blob').then(function (blob) {
      g_viewer._textures[textureName] = g_viewer.createTextureBLOB(blob)
    });
  }
}

function loadFileByUri(file) {
  let fileExtension = getFileExtension(file);

  if (fileExtension === 'zae') {
    try {
      JSZipUtils.getBinaryContent(file, function (err, data) {
        if (err) {
          throw err;
        }
        loadZAE(file, data);
      });
    }
    catch (e) {
      console.error(e);
    }
  }
  else {
    readFileByUri(`${file}`, function (fileContent) {
      try {
        loadContent(file, fileExtension, fileContent);
      }
      catch (e) {
        console.error(e);
      }
    });
  }
}
