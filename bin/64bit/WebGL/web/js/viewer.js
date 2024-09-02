Array.prototype.clean = function (deleteValue) {
  for (var i = 0; i < this.length; i++) {
    if (this[i] == deleteValue) {
      this.splice(i, 1);
      i--;
    }
  }
  return this;
};

/*
* Static model
*/
var StaticModel = function (model, data) {
  /**************************************************************************
  * Members
  */

  /*
  * Model
  */
  this._model = model;

  /*
  * Data
  */
  this._data = data;
};

/*
* Upload file status
*/
const UPLOAD_EVENT_BEGIN = 0;
const UPLOAD_EVENT_PROGRESS = 1;
const UPLOAD_EVENT_CONVERT = 2;
const UPLOAD_EVENT_END = 3;

/*
* Viewer
*/
var Viewer = function () {
  /**************************************************************************
  * Members
  */

  /*
  * On-line mode
  */
  this._isOnline = false;

  /*
  * Shader
  */
  this._shaderProgram = null;

  /*
  * Matrices
  */
  this._mtxModelView = mat4.create();
  this._mtxProjection = mat4.create();

  /*
  * Scene
  */
  this._clearColor = [0.9, 0.9, 0.9, 1.0];
  this._pointLightPosition = vec3.create([.25, 0.25, 1]);
  this._Shininess = 50.0;
  this._defaultEyeVector = [0, 0, -5];
  this._eyeVector = vec3.create(this._defaultEyeVector);
  this._rotateX = 30;
  this._rotateY = 30;

  /*
  * Selection
  */
  // For both dynamic and static instances
  this._objectsBaseIndex = 0;
  // The encoded colors for the OWL instances: array (one entry per OWL instance)
  this._owlInstancesSelectionColors = [];
  // The encoded colors for the static instances: array (one entry per static instance) of arrays (one entry per transformation)
  this._staticObjectsSelectionColors = [];
  // For the dynamic objects only
  this._dynamicObjectsBaseIndex = 0;
  // Describes the selected dynamic object
  this._selectedDynamicObject = null;
  // Calculate the number of transformations and world's dimensions
  this._dynamicInstancesCalcuationMode = false;
  // Whether to draw OWL content in a frame buffer
  this._dynamicObjectsSelectionMode = false;
  // The encoded colors for the dynamic instances; array (one entry per dynamic instance) of arrays (one entry per transformation)
  this._dynamicObjectsSelectionColors = [];
  // The ID of the picked dynamic or static object
  this._pickedObject = -1;
  // The IDs of the selected dynamic or static objects
  this._selectedObjects = [];

  /*
  * World
  */
  this._staticWorldDimensions = {};
  this._worldDimensions = {};

  /*
  * Clip space
  */
  this._clipSpaceVertexBuffer;
  this._clipSpaceVertexIndexBuffer;
  this._clipSpaceVertexNormalBuffer;

  /*
  * Models
  */
  this._staticModels = [];

  /*
  * Selection support
  */
  this._selectionFramebuffer = null;
  this._selectionTexture;
  this._selectedPixelValues = new Uint8Array(4);

  /*
  * FPS
  */
  this._framesCount = 0;
  this._timeNow = 0;
  this._timeLast = 0;
  this._FPS = 0;

  /*
   * Spinner
   */
  this._spinnerOptions = {
    lines: 13 // The number of lines to draw
    , length: 0 // The length of each line
    , width: 14 // The line thickness
    , radius: 31 // The radius of the inner circle
    , scale: 0.75 // Scales overall size of the spinner
    , corners: 1 // Corner roundness (0..1)
    , color: '#000' // #rgb or #rrggbb or array of colors
    , opacity: 0.25 // Opacity of the lines
    , rotate: 0 // The rotation offset
    , direction: 1 // 1: clockwise, -1: counterclockwise
    , speed: 1 // Rounds per second
    , trail: 29 // Afterglow percentage
    , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
    , zIndex: 2e9 // The z-index (defaults to 2000000000)
    , className: 'spinner' // The CSS class to assign to the spinner
    , top: '48%' // Top position relative to parent
    , left: '50%' // Left position relative to parent
    , shadow: false // Whether to render a shadow
    , hwaccel: false // Whether to use hardware acceleration
    , position: 'fixed' // Element positioning
  };

  /*
   * Spinner
   */
  this._busyIndicator = null;

  /*
  * Texture
  */
  this.canvas = null;

  /**************************************************************************
  * General
  */

  /*
  * Initialize
  */
  Viewer.prototype.initProgram = function () {
    var fgShader = utils.getShader(gl, "shader-fs");
    var vxShader = utils.getShader(gl, "shader-vs");

    this._shaderProgram = gl.createProgram();
    gl.attachShader(this._shaderProgram, vxShader);
    gl.attachShader(this._shaderProgram, fgShader);
    gl.linkProgram(this._shaderProgram);

    if (!gl.getProgramParameter(this._shaderProgram, gl.LINK_STATUS)) {
      alert("Could not initialize shaders.");
      console.error("Could not initialize shaders.");

      return false;
    }

    gl.useProgram(this._shaderProgram);

    /* Vertex Shader */
    this._shaderProgram.VertexPosition = gl.getAttribLocation(
      this._shaderProgram,
      'Position'
    )

    this._shaderProgram.VertexNormal = gl.getAttribLocation(
      this._shaderProgram,
      'Normal'
    )

    this._shaderProgram.UV = gl.getAttribLocation(
      this._shaderProgram,
      'UV'
    )

    this._shaderProgram.ProjectionMatrix = gl.getUniformLocation(
      this._shaderProgram,
      'ProjectionMatrix'
    )
    this._shaderProgram.ModelViewMatrix = gl.getUniformLocation(
      this._shaderProgram,
      'ModelViewMatrix'
    )

    this._shaderProgram.NormalMatrix = gl.getUniformLocation(
      this._shaderProgram,
      'NormalMatrix'
    )

    this._shaderProgram.DiffuseMaterial = gl.getUniformLocation(
      this._shaderProgram,
      'DiffuseMaterial'
    )

    this._shaderProgram.EnableLighting = gl.getUniformLocation(
      this._shaderProgram,
      'EnableLighting'
    )

    this._shaderProgram.EnableTexture = gl.getUniformLocation(
      this._shaderProgram,
      'EnableTexture'
    )

    /* Fragment Shader */
    this._shaderProgram.LightPosition = gl.getUniformLocation(
      this._shaderProgram,
      'LightPosition'
    )

    this._shaderProgram.AmbientMaterial = gl.getUniformLocation(
      this._shaderProgram,
      'AmbientMaterial'
    )

    this._shaderProgram.SpecularMaterial = gl.getUniformLocation(
      this._shaderProgram,
      'SpecularMaterial'
    )

    this._shaderProgram.Transparency = gl.getUniformLocation(
      this._shaderProgram,
      'Transparency'
    )

    // #todo
    //this._shaderProgram.uMaterialEmissiveColor = gl.getUniformLocation(
    //  this._shaderProgram,
    //  'uMaterialEmissiveColor'
    //)

    this._shaderProgram.Shininess = gl.getUniformLocation(
      this._shaderProgram,
      'Shininess'
    )

    this._shaderProgram.AmbientLightWeighting = gl.getUniformLocation(
      this._shaderProgram,
      'AmbientLightWeighting'
    )

    this._shaderProgram.DiffuseLightWeighting = gl.getUniformLocation(
      this._shaderProgram,
      'DiffuseLightWeighting'
    )

    this._shaderProgram.SpecularLightWeighting = gl.getUniformLocation(
      this._shaderProgram,
      'SpecularLightWeighting'
    )

    this._shaderProgram.Sampler = gl.getUniformLocation(
      this._shaderProgram,
      'Sampler'
    )

    return true;
  }

  /*
  * Lights
  */
  Viewer.prototype.setLights = function () {
    gl.uniform3f(
      this._shaderProgram.LightPosition,
      this._pointLightPosition[0],
      this._pointLightPosition[1],
      this._pointLightPosition[2]
    );

    gl.uniform1f(
      this._shaderProgram.Shininess,
      this._Shininess
    );

    gl.uniform3f(
      this._shaderProgram.AmbientLightWeighting,
      0.4, 0.4, 0.4);

    gl.uniform3f(
      this._shaderProgram.DiffuseLightWeighting,
      0.95, 0.95, 0.95);

    gl.uniform3f(
      this._shaderProgram.SpecularLightWeighting,
      0.15, 0.15, 0.15);
  }

  /*
  * Initialize
  */
  Viewer.prototype.init = function (canvasID, width, height) {
    // DISABLED
    //this.checkServerStatus();

    gl = utils.getGLContext(canvasID);
    if (!gl) {
      alert("Could not initialize WebGL.");
      console.error("Could not initialize WebGL.");

      return false;
    }

    if (!this.initProgram()) {
      return false;
    }

    // Fix for WARNING: there is no texture bound to the unit 0
    function createTexture(type, target, count) {

      var data = new Uint8Array(4); // 4 is required to match default unpack alignment of 4.
      var texture = gl.createTexture();

      gl.bindTexture(type, texture);
      gl.texParameteri(type, gl.TEXTURE_MIN_FILTER, gl.NEAREST);
      gl.texParameteri(type, gl.TEXTURE_MAG_FILTER, gl.NEAREST);

      for (var i = 0; i < count; i++) {

        gl.texImage2D(target + i, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, data);

      }

      return texture;
    }

    var emptyTextures = {};
    emptyTextures[gl.TEXTURE_2D] = createTexture(gl.TEXTURE_2D, gl.TEXTURE_2D, 1);
    emptyTextures[gl.TEXTURE_CUBE_MAP] = createTexture(gl.TEXTURE_CUBE_MAP, gl.TEXTURE_CUBE_MAP_POSITIVE_X, 6);

    gl.activeTexture(gl.TEXTURE0);
    gl.bindTexture(gl.TEXTURE_2D, emptyTextures[gl.TEXTURE_2D]);

    gl.activeTexture(gl.TEXTURE1);
    gl.bindTexture(gl.TEXTURE_CUBE_MAP, emptyTextures[gl.TEXTURE_CUBE_MAP]);
    // END WARNING: there is no texture bound to the unit 0

    resizeCanvas(width, height);

    this.initSelectionFramebuffer();

    this.loadOWLInstances();

    //this.loadStaticModels();

    //this.loadDynamicModels();

    this.setLights();

    /*
    * Clip space
    */
    this._clipSpaceVertexBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, this._clipSpaceVertexBuffer);
    var vertices = [
      // Front face
      -1.0, -1.0, -1.0,
      1.0, -1.0, -1.0,
      1.0, 1.0, -1.0,
      -1.0, 1.0, -1.0,

      // Back face
      -1.0, -1.0, 1.0,
      1.0, -1.0, 1.0,
      1.0, 1.0, 1.0,
      -1.0, 1.0, 1.0,

      // Left face
      -1.0, -1.0, -1.0,
      -1.0, 1.0, -1.0,
      -1.0, 1.0, 1.0,
      -1.0, -1.0, 1.0,

      // Right face
      1.0, -1.0, -1.0,
      1.0, 1.0, -1.0,
      1.0, 1.0, 1.0,
      1.0, -1.0, 1.0,
    ];
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
    this._clipSpaceVertexBuffer.itemSize = 3;
    this._clipSpaceVertexBuffer.numItems = 16;

    this._clipSpaceVertexIndexBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, this._clipSpaceVertexIndexBuffer);
    var cubeVertexIndices = [
      0, 1, 2, 3,     // Front face
      4, 5, 6, 7,     // Back face
      8, 9, 10, 11,   // Left face
      12, 13, 14, 15, // Right face
    ];
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
    this._clipSpaceVertexIndexBuffer.itemSize = 1;
    this._clipSpaceVertexIndexBuffer.numItems = 16;

    this._clipSpaceVertexNormalBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, this._clipSpaceVertexNormalBuffer);
    var vertexNormals = [
      // Front face
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,

      // Back face
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,

      // Left face
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,

      // Right face
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
      0.0, 0.0, -1.0,
    ];
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexNormals), gl.STATIC_DRAW);
    this._clipSpaceVertexNormalBuffer.itemSize = 3;
    this._clipSpaceVertexNormalBuffer.numItems = 16;

    renderLoop();

    return true;
  };

  /**
  * Checks status of server
  */
  Viewer.prototype.checkServerStatus = function () {
    try {
      var viewer = this;

      this._isOnline = false;

      var command = '/getStatus';
      console.info('Requesting ' + command);

      var request = new XMLHttpRequest();
      request.open("POST", command);
      request.setRequestHeader("pragma", "no-cache");
      request.setRequestHeader("cache-Control", "no-cache");
      request.responseType = "text";

      request.onreadystatechange = function () {
        if (request.readyState == 4) {
          if ((request.status == 200) && (request.responseText == "OK")) {
            viewer._isOnline = true;

            console.info("The server is available.");
          }
          else {
            viewer._isOnline = false

            console.info("The server is not available.");
          }

          if (typeof (onViewerCheckServerStatus) === typeof (Function)) {
            onViewerCheckServerStatus();
          }
        } // if (request.readyState == 4)
      }

      request.send();
    }
    catch (e) {
      this._isOnline = false;

      console.info("The server is not available: '" + e + "'");
    }
  }

  /**
  * Default Projection matrix
  */
  Viewer.prototype.setDefultProjectionMatrix = function () {
    /*
    * Projection matrix
    */
    mat4.identity(this._mtxProjection);
    mat4.perspective(45, gl.canvas.width / gl.canvas.height, 0.001, 1000000.0, this._mtxProjection);

    gl.uniformMatrix4fv(this._shaderProgram.ProjectionMatrix, false, this._mtxProjection);
  }

  /**
  * Default Model-View, Inverse Model-View and Normal matrices
  */
  Viewer.prototype.setDefultMatrices = function () {
    /*
    * Projection matrix
    */
    this.setDefultProjectionMatrix();

    /*
    * Model-View matrix
    */
    mat4.identity(this._mtxModelView);
    mat4.translate(this._mtxModelView, this._eyeVector);
    mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
    mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);

    /*
    * Fit the image
    */

    // [0.0 -> X/Y/Zmin + X/Y/Zmax]
    mat4.translate(this._mtxModelView,
      [
        -this._worldDimensions.Xmin,
        -this._worldDimensions.Ymin,
        -this._worldDimensions.Zmin
      ]);

    // Center
    mat4.translate(this._mtxModelView,
      [
        -(this._worldDimensions.Xmax - this._worldDimensions.Xmin) / 2,
        -(this._worldDimensions.Ymax - this._worldDimensions.Ymin) / 2,
        -(this._worldDimensions.Zmax - this._worldDimensions.Zmin) / 2
      ]);

    gl.uniformMatrix4fv(this._shaderProgram.ModelViewMatrix, false, this._mtxModelView);

    /*
    * Normals matrix
    */
    gl.uniformMatrix3fv(this._shaderProgram.NormalMatrix, false, mat4.toMat3(this._mtxModelView));
  }

  /**
  * Draws the scene
  */
  Viewer.prototype.drawScene = function () {
    this.setDefultMatrices();

    gl.viewport(0, 0, gl.canvas.width, gl.canvas.height);
    gl.clearColor(this._clearColor[0], this._clearColor[1], this._clearColor[2], this._clearColor[3]);
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

    gl.enable(gl.SAMPLE_COVERAGE);
    gl.sampleCoverage(1.0, false);

    gl.enable(gl.DEPTH_TEST);
    gl.depthFunc(gl.LEQUAL);

    this.setLights();

    this.drawOWLInstances();

    //this.drawStaticModels();

    //this.drawDynamicModels();

    //this.drawClipspace();

    //this.drawSelectionFrameBuffer();

    this.drawOWLInstancesSelectionFrameBuffer();

    this.calculateFps();
  }

  /**
  * Draws the clip space
  */
  Viewer.prototype.drawClipspace = function () {
    this.setDefultMatrices();

    gl.bindBuffer(gl.ARRAY_BUFFER, this._clipSpaceVertexBuffer);
    gl.vertexAttribPointer(this._shaderProgram.VertexPosition, this._clipSpaceVertexBuffer.itemSize, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

    gl.bindBuffer(gl.ARRAY_BUFFER, this._clipSpaceVertexNormalBuffer);
    gl.vertexAttribPointer(this._shaderProgram.VertexNormal, this._clipSpaceVertexNormalBuffer.itemSize, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);

    gl.uniform3f(this._shaderProgram.AmbientMaterial, 0.0, 0.0, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);
    gl.uniform3f(this._shaderProgram.DiffuseMaterial, 0.0, 0.0, 0.0);
    gl.uniform3f(this._shaderProgram.SpecularMaterial, 0.0, 0.0, 0.0);

    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, this._clipSpaceVertexIndexBuffer);
    gl.drawElements(gl.LINE_LOOP, 4, gl.UNSIGNED_SHORT, 0);
    gl.drawElements(gl.LINE_LOOP, 4, gl.UNSIGNED_SHORT, 4 * 2);
    gl.drawElements(gl.LINE_LOOP, 4, gl.UNSIGNED_SHORT, 8 * 2);
    gl.drawElements(gl.LINE_LOOP, 4, gl.UNSIGNED_SHORT, 12 * 2);
  }

  /**
  * Selection support
  */
  Viewer.prototype.initSelectionFramebuffer = function () {
    this._selectionFramebuffer = gl.createFramebuffer();
    gl.bindFramebuffer(gl.FRAMEBUFFER, this._selectionFramebuffer);
    this._selectionFramebuffer.width = 512;
    this._selectionFramebuffer.height = 512;

    this._selectionTexture = gl.createTexture();
    gl.bindTexture(gl.TEXTURE_2D, this._selectionTexture);
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, this._selectionFramebuffer.width, this._selectionFramebuffer.height, 0, gl.RGBA, gl.UNSIGNED_BYTE, null);

    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_NEAREST);
    gl.generateMipmap(gl.TEXTURE_2D);

    var renderbuffer = gl.createRenderbuffer();
    gl.bindRenderbuffer(gl.RENDERBUFFER, renderbuffer);
    gl.renderbufferStorage(gl.RENDERBUFFER, gl.DEPTH_COMPONENT16, this._selectionFramebuffer.width, this._selectionFramebuffer.height);

    gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, this._selectionTexture, 0);
    gl.framebufferRenderbuffer(gl.FRAMEBUFFER, gl.DEPTH_ATTACHMENT, gl.RENDERBUFFER, renderbuffer);

    gl.bindTexture(gl.TEXTURE_2D, null);
    gl.bindRenderbuffer(gl.RENDERBUFFER, null);
    gl.bindFramebuffer(gl.FRAMEBUFFER, null);
  }

  /**
  * Draws the conceptual faces in a frame buffer
  */
  Viewer.prototype.drawSelectionFrameBuffer = function () {
    this.setDefultMatrices();

    gl.bindFramebuffer(gl.FRAMEBUFFER, this._selectionFramebuffer);

    gl.viewport(0, 0, this._selectionFramebuffer.width, this._selectionFramebuffer.height);
    gl.clearColor(0.0, 0.0, 0.0, 0.0);
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

    gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);

    this.drawStaticModelsSelectionFrameBuffer();
    this.drawDynamicModelsSelectionFrameBuffer();

    gl.bindFramebuffer(gl.FRAMEBUFFER, null);
    gl.uniform1f(this._shaderProgram.EnableLighting, 1.0);
  }

  /*
    * Interaction support
    */
  Viewer.prototype.pickObject = function (x, y) {
    if (this._selectionFramebuffer == null) {
      return;
    }

    if (PICKED_OBJECT_COLOR[0] < 0 || PICKED_OBJECT_COLOR[1] < 0 || PICKED_OBJECT_COLOR[2] < 0) {
      return;
    }

    this._pickedObject = -1;

    gl.bindFramebuffer(gl.FRAMEBUFFER, this._selectionFramebuffer);
    gl.readPixels(x * (this._selectionFramebuffer.width / gl.canvas.width),
      (gl.canvas.height - y) * (this._selectionFramebuffer.height / gl.canvas.height),
      1, 1, gl.RGBA, gl.UNSIGNED_BYTE, this._selectedPixelValues);
    gl.bindFramebuffer(gl.FRAMEBUFFER, null);

    if (this._selectedPixelValues[3] != 0.0) {
      // decoding of the selection color
      var objectIndex = (this._selectedPixelValues[0/*R*/] * (255 * 255)) +
        (this._selectedPixelValues[1/*G*/] * 255) +
        this._selectedPixelValues[2/*B*/];

      this._pickedObject = objectIndex;
    } // if (this._selectedPixelValues[3] != ...    
  }

  /*
  * Interaction support
  */
  Viewer.prototype.selectObject = function (x, y) {
    if (this._selectionFramebuffer == null) {
      return;
    }

    if (SELECTED_OBJECT_COLOR[0] < 0 || SELECTED_OBJECT_COLOR[1] < 0 || SELECTED_OBJECT_COLOR[2] < 0) {
      return;
    }

    gl.bindFramebuffer(gl.FRAMEBUFFER, this._selectionFramebuffer);
    gl.readPixels(x * (this._selectionFramebuffer.width / gl.canvas.width),
      (gl.canvas.height - y) * (this._selectionFramebuffer.height / gl.canvas.height),
      1, 1, gl.RGBA, gl.UNSIGNED_BYTE, this._selectedPixelValues);
    gl.bindFramebuffer(gl.FRAMEBUFFER, null);

    if (this._selectedPixelValues[3] != 0.0) {
      // decoding of the selection color
      var objectIndex = (this._selectedPixelValues[0/*R*/] * (255 * 255)) +
        (this._selectedPixelValues[1/*G*/] * 255) +
        this._selectedPixelValues[2/*B*/];

      if (MULTI_SELECTION_MODE) {
        var index = this._selectedObjects.indexOf(objectIndex);
        if (index == -1) {
          // Add the object if it doesn't exist
          this._selectedObjects.push(objectIndex);
        }
        else {
          // Remove it
          this._selectedObjects.splice(index, 1);
        }
      } // if (MULTI_SELECTION_MODE)
      else {
        this._selectedObjects = [];
        this._selectedObjects.push(objectIndex);
      }
    } // if (this._selectedPixelValues[3] != ...   
    else {
      // Reset
      this._selectedObjects = [];
    }
  }

  /*
  * FPS
  */
  Viewer.prototype.calculateFps = function () {
    this._timeNow = new Date().getTime();

    this._framesCount++;

    if (this._timeNow - this._timeLast >= 1000) {
      this._FPS = Number(this._framesCount * 1000.0 / (this._timeNow - this._timeLast)).toPrecision(5);

      this._timeLast = this._timeNow;
      this._framesCount = 0;
    }
  }

  /**
  * Loads a texture
  */
  Viewer.prototype.createTexture = function (textureFile) {
    try {
      var viewer = this;

      var texture = gl.createTexture();

      // Temp texture until the image is loaded
      // https://stackoverflow.com/questions/19722247/webgl-wait-for-texture-to-load/19748905#19748905            
      gl.bindTexture(gl.TEXTURE_2D, texture);
      gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array([0, 0, 0, 255]));

      var image = new Image();
      image.addEventListener('error', function () {
        console.error("Can't load '" + textureFile + "'.");
      });

      image.addEventListener('load', function () {
        // Now that the image has loaded make copy it to the texture.
        gl.bindTexture(gl.TEXTURE_2D, texture);
        gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);

        // Check if the image is a power of 2 in both dimensions.
        if (viewer.isPowerOf2(image.width) && viewer.isPowerOf2(image.height)) {
          // Yes, it's a power of 2. Generate mips.
          gl.generateMipmap(gl.TEXTURE_2D);
          gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR);
        } else {
          // No, it's not a power of 2. Turn of mips and set wrapping to clamp to edge
          gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
          gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
          gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
        }

        gl.bindTexture(gl.TEXTURE_2D, null);
      });

      image.src = textureFile;

      return texture;
    }
    catch (ex) {
      console.error(ex);
    }

    return null;
  }

  /**
  * Loads a texture
  */
  Viewer.prototype.createTextureBase64 = function (base64Content) {
    try {
      var viewer = this;

      var texture = gl.createTexture();

      // Temp texture until the image is loaded
      // https://stackoverflow.com/questions/19722247/webgl-wait-for-texture-to-load/19748905#19748905            
      gl.bindTexture(gl.TEXTURE_2D, texture);
      gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array([0, 0, 0, 255]));

      var image = new Image();
      image.addEventListener('error', function () {
        console.error("Can't load the texture.");
      });

      image.addEventListener('load', function () {
        // Now that the image has loaded make copy it to the texture.
        gl.bindTexture(gl.TEXTURE_2D, texture);

        // Check if the image is a power of 2 in both dimensions.
        if (viewer.isPowerOf2(image.width) && viewer.isPowerOf2(image.height)) {
          gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);

          // Yes, it's a power of 2. Generate mips.
          gl.generateMipmap(gl.TEXTURE_2D);
          gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR);
        } else {
          // No, it's not a power of 2. Resize
          image = viewer.makePowerOfTwo(image);

          gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);

          gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
        }

        gl.bindTexture(gl.TEXTURE_2D, null);
      });

      image.src = base64Content;

      return texture;
    }
    catch (ex) {
      console.error(ex);
    }

    return null;
  }

  /**
  * Texture support
  */
  Viewer.prototype.isPowerOf2 = function (value) {
    return (value & (value - 1)) == 0;
  }

  Viewer.prototype.floorPowerOfTwo = function (value) {
    return Math.pow(2, Math.floor(Math.log(value) / Math.LN2));
  }

  Viewer.prototype.makePowerOfTwo = function (image) {
    if (image instanceof HTMLImageElement || image instanceof HTMLCanvasElement) {

      if (this.canvas === null) this.canvas = document.createElementNS('http://www.w3.org/1999/xhtml', 'canvas');

      this.canvas.width = this.floorPowerOfTwo(image.width);
      this.canvas.height = this.floorPowerOfTwo(image.height);

      var context = this.canvas.getContext('2d');
      context.drawImage(image, 0, 0, this.canvas.width, this.canvas.height);

      return this.canvas;
    }

    return image;
  }

  Viewer.prototype.zoomTo = function (x, y) {
    if (this._selectionFramebuffer == null) {
      return;
    }

    gl.bindFramebuffer(gl.FRAMEBUFFER, this._selectionFramebuffer);
    gl.readPixels(x * (this._selectionFramebuffer.width / gl.canvas.width),
      (gl.canvas.height - y) * (this._selectionFramebuffer.height / gl.canvas.height),
      1, 1, gl.RGBA, gl.UNSIGNED_BYTE, this._selectedPixelValues);
    gl.bindFramebuffer(gl.FRAMEBUFFER, null);

    if (this._selectedPixelValues[3] != 0.0) {
      // decoding of the selection color
      var objectIndex = (this._selectedPixelValues[0/*R*/] * (255 * 255)) +
        (this._selectedPixelValues[1/*G*/] * 255) +
        this._selectedPixelValues[2/*B*/];

      this._worldDimensions.Xmin = g_instances[objectIndex - 1].Xmin;
      this._worldDimensions.Xmax = g_instances[objectIndex - 1].Xmax;
      this._worldDimensions.Ymin = g_instances[objectIndex - 1].Ymin;
      this._worldDimensions.Ymax = g_instances[objectIndex - 1].Ymax;
      this._worldDimensions.Zmin = g_instances[objectIndex - 1].Zmin;
      this._worldDimensions.Zmax = g_instances[objectIndex - 1].Zmax;

      this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
      this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
      this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

      this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
      this._eyeVector = vec3.create(this._defaultEyeVector);
    } // if (this._selectedPixelValues[3] != ...    
  }

  Viewer.prototype.reset = function () {
    try {
      this._clearColor = [0.9, 0.9, 0.9, 1.0];

      this._pointLightPosition = vec3.create([.025, 0.25, 1]);
      this._Shininess = 50.0;

      this._defaultEyeVector = [0, 0, -5];
      this._eyeVector = vec3.create(this._defaultEyeVector);

      /*
      * Calculate world's dimensions
      */
      if (g_instances.length > 0) {
        this._worldDimensions.Xmin = g_instances[0].Xmin;
        this._worldDimensions.Xmax = g_instances[0].Xmax;
        this._worldDimensions.Ymin = g_instances[0].Ymin;
        this._worldDimensions.Ymax = g_instances[0].Ymax;
        this._worldDimensions.Zmin = g_instances[0].Zmin;
        this._worldDimensions.Zmax = g_instances[0].Zmax;

        for (var i = 1; i < g_instances.length; i++) {
          this._worldDimensions.Xmin = Math.min(this._worldDimensions.Xmin, g_instances[i].Xmin);
          this._worldDimensions.Ymin = Math.min(this._worldDimensions.Ymin, g_instances[i].Ymin);
          this._worldDimensions.Zmin = Math.min(this._worldDimensions.Zmin, g_instances[i].Zmin);

          this._worldDimensions.Xmax = Math.max(this._worldDimensions.Xmax, g_instances[i].Xmax);
          this._worldDimensions.Ymax = Math.max(this._worldDimensions.Ymax, g_instances[i].Ymax);
          this._worldDimensions.Zmax = Math.max(this._worldDimensions.Zmax, g_instances[i].Zmax);
        } // for (var i = ...

        this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
        this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
        this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

        this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
        this._eyeVector = vec3.create(this._defaultEyeVector);
      } // if (g_instances.length > 0)

      this._rotateX = 30;
      this._rotateY = 30;

      this._selectedObjects = [];
      this._pickedObject = -1;
    }
    catch (ex) {
      console.error();
    }
  }

  /**************************************************************************
  * OWL instances
  */

  /*
  * OWL - load
  */
  Viewer.prototype.loadOWLInstances = function () {
    console.info("loadOWLInstances - BEGIN");

    this.busyIndicator(true);

    try {
      /*
      * Conceptual faces
      */
      for (var i = 0; i < g_instances.length; i++) {
        if (g_instances[i].conceptualFaces == undefined) {
          console.error("Unknown data model.");
          continue;
        }

        /*
        * VBOs
        */
        if (g_instances[i].vertices != undefined) {
          // Less than 64K vertices
          var vertexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(g_instances[i].vertices), gl.STATIC_DRAW);
          vertexBufferObject.length = g_instances[i].vertices.length;
          g_instances[i].VBO = vertexBufferObject;
        } // if (g_instances[i].vertices != ...
        else {
          // More than 64K vertices
          for (var j = 0; j < g_instances[i].conceptualFaces.length; j++) {
            if (g_instances[i].conceptualFaces[j].vertices != undefined) {
              var vertexBufferObject = gl.createBuffer();
              gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
              gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(g_instances[i].conceptualFaces[j].vertices), gl.STATIC_DRAW);
              vertexBufferObject.length = g_instances[i].conceptualFaces[j].vertices.length;
              g_instances[i].conceptualFaces[j].VBO = vertexBufferObject;
            }
            else {
              console.error("Unknown data model.");
            }
          } // for (var j = ...
        } // else if (g_instances[i].vertices != ...    

        /*
        * IBO-s
        */
        for (var j = 0; j < g_instances[i].conceptualFaces.length; j++) {
          if (g_instances[i].conceptualFaces[j].indices != undefined) {
            // Less than 64 indices
            var indexBufferObject = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(g_instances[i].conceptualFaces[j].indices), gl.STATIC_DRAW);
            indexBufferObject.count = g_instances[i].conceptualFaces[j].indices.length;
            g_instances[i].conceptualFaces[j].IBO = indexBufferObject;
          } // if (g_instances[i].conceptualFaces[j].indices != ...
          else {
            // More than 64K indices
            if (g_instances[i].conceptualFaces[j].cohorts != undefined) {
              for (var c = 0; c < g_instances[i].conceptualFaces[j].cohorts.length; c++) {
                var indexBufferObject = gl.createBuffer();
                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
                gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(g_instances[i].conceptualFaces[j].cohorts[c].indices), gl.STATIC_DRAW);
                indexBufferObject.count = g_instances[i].conceptualFaces[j].cohorts[c].indices.length;
                g_instances[i].conceptualFaces[j].cohorts[c].IBO = indexBufferObject;
              } // for (var c = ...
            } // if (g_instances[i].conceptualFaces[j].cohorts != ...
            else {
              console.error("Unknown data model.");
            }
          }
        } // for (var j = ...
      } // for (var i = ...             

      /*
      * Texture
      */
      var dicTexture2Instance = {};
      for (var i = 0; i < g_instances.length; i++) {
        for (var j = 0; j < g_instances[i].conceptualFaces.length; j++) {
          if (g_instances[i].conceptualFaces[j].material.texture != undefined) {
            if (dicTexture2Instance[g_instances[i].conceptualFaces[j].material.texture.name] == undefined) {
              if (g_instances[i].conceptualFaces[j].material.texture.base64Content != undefined) {
                g_instances[i].conceptualFaces[j].material.texture.instance = this.createTextureBase64(g_instances[i].conceptualFaces[j].material.texture.base64Content);
              }
              else {
                g_instances[i].conceptualFaces[j].material.texture.instance = this.createTexture(g_instances[i].conceptualFaces[j].material.texture.name);
              }

              dicTexture2Instance[g_instances[i].conceptualFaces[j].material.texture.name] = g_instances[i].conceptualFaces[j].material.texture.instance;
            }
            else {
              g_instances[i].conceptualFaces[j].material.texture.instance = dicTexture2Instance[g_instances[i].conceptualFaces[j].material.texture.name];
            }
          }
        } // for (var j = ...             
      } // for (var i = ...

      /*
      * Faces polygons
      */
      // DISABLED
      //for (var i = 0; i < g_instances.length; i++) {
      //    /*
      //    * IBO
      //    */
      //    var indexBufferObject = gl.createBuffer();
      //    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
      //    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(g_instances[i].facesPolygons.indices), gl.STATIC_DRAW);
      //    indexBufferObject.count = g_instances[i].facesPolygons.indices.length;
      //    g_instances[i].facesPolygons.IBO = indexBufferObject;
      //} // for (var i = ...

      /*
      * Conceptual faces polygons
      */
      for (var i = 0; i < g_instances.length; i++) {
        if (g_instances[i].conceptualFacesPolygons == undefined) {
          continue;
        }

        for (var j = 0; j < g_instances[i].conceptualFacesPolygons.length; j++) {
          var conceptualFacesPolygon = g_instances[i].conceptualFacesPolygons[j];

          /*
          * VBO
          */
          if (conceptualFacesPolygon.vertices != undefined) {
            var vertexBufferObject = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(conceptualFacesPolygon.vertices), gl.STATIC_DRAW);
            vertexBufferObject.length = conceptualFacesPolygon.vertices.length;
            conceptualFacesPolygon.VBO = vertexBufferObject;
          }

          /*
          * IBO
          */
          if (conceptualFacesPolygon.indices != undefined) {
            var indexBufferObject = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(conceptualFacesPolygon.indices), gl.STATIC_DRAW);
            indexBufferObject.count = conceptualFacesPolygon.indices.length;
            conceptualFacesPolygon.IBO = indexBufferObject;
          }
          else {
            if (conceptualFacesPolygon.cohorts != undefined) {
              for (var c = 0; c < conceptualFacesPolygon.cohorts.length; c++) {
                var cohort = conceptualFacesPolygon.cohorts[c];

                var indexBufferObject = gl.createBuffer();
                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
                gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cohort.indices), gl.STATIC_DRAW);
                indexBufferObject.count = cohort.indices.length;
                cohort.IBO = indexBufferObject;
              } // for (var c = 
            } // if (conceptualFacesPolygon.cohorts != ...
            else {
              console.error("Unknown data model.");
            }
          }
        } // for (var j = ...
      } // for (var i = ...

      /*
      * Calculate world's dimensions
      */
      if (g_instances.length > 0) {
        this._worldDimensions.Xmin = g_instances[0].Xmin;
        this._worldDimensions.Xmax = g_instances[0].Xmax;
        this._worldDimensions.Ymin = g_instances[0].Ymin;
        this._worldDimensions.Ymax = g_instances[0].Ymax;
        this._worldDimensions.Zmin = g_instances[0].Zmin;
        this._worldDimensions.Zmax = g_instances[0].Zmax;

        for (var i = 1; i < g_instances.length; i++) {
          this._worldDimensions.Xmin = Math.min(this._worldDimensions.Xmin, g_instances[i].Xmin);
          this._worldDimensions.Ymin = Math.min(this._worldDimensions.Ymin, g_instances[i].Ymin);
          this._worldDimensions.Zmin = Math.min(this._worldDimensions.Zmin, g_instances[i].Zmin);

          this._worldDimensions.Xmax = Math.max(this._worldDimensions.Xmax, g_instances[i].Xmax);
          this._worldDimensions.Ymax = Math.max(this._worldDimensions.Ymax, g_instances[i].Ymax);
          this._worldDimensions.Zmax = Math.max(this._worldDimensions.Zmax, g_instances[i].Zmax);
        } // for (var i = ...

        this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
        this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
        this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

        this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
        this._eyeVector = vec3.create(this._defaultEyeVector);
      } // if (g_instances.length > 0)

      /*
      * Removing instances not in the actual phase
      */
      var viewURI = Cookies.get('viewURI');
      if (viewURI && viewURI !== '') {
        console.log("viewURI: " + viewURI);
        for (var i = REMOVED_INSTANCES.length - 1; i >= 0; i--) {
          if (viewURI.split(",").indexOf(REMOVED_INSTANCES[i].uri) != -1) {
            g_instances.push(REMOVED_INSTANCES[i]);
            REMOVED_INSTANCES.splice(i, 1);
          }
        }

        for (var i = g_instances.length - 1; i >= 0; i--) {
          if (viewURI.split(",").indexOf(g_instances[i].uri) == -1) {
            //remove instance
            REMOVED_INSTANCES.push(g_instances[i]);
            g_instances.splice(i, 1);
          }
        }

        Cookies.remove('viewURI');
      }

      /*
      * Default selection from cookie
      */
      var selURI = Cookies.get('selURI');
      this._selectedObjects = [];
      DEFAULT_SELECTED_OBJECTS = [];
      $("#button-run").attr("data-uri", "");
      $('#button-run').off("click");
      $("#button-run").hide();

      if (selURI && selURI !== '') {
        console.log("selURI: " + selURI);
        DEFAULT_SELECTED_OBJECTS = [];
        DEFAULT_SELECTED_OBJECTS = DEFAULT_SELECTED_OBJECTS.concat(selURI.split(","));
        DEFAULT_SELECTED_OBJECTS = DEFAULT_SELECTED_OBJECTS.clean("");
        if (DEFAULT_SELECTED_OBJECTS.length == 1) {
          $('#button-run').attr("data-uri", selURI);
          $('#button-run').find("a").text("Metadata");
          $('#button-run').click(function () {
            parent.showElement($(this).attr("data-uri"));
          });
          $("#button-run").show();
        }

        Cookies.set('selURI', '');
      }

      /*
      * Default selection
      */
      for (var i = 0; i < DEFAULT_SELECTED_OBJECTS.length; i++) {
        var index = g_instances.findIndex(function (v) {
          return v.uri === DEFAULT_SELECTED_OBJECTS[i];
        });

        if (index !== -1) {
          this._selectedObjects.push(index + 1);
        }
      } // for (var i = ...
    }
    catch (e) {
      console.error(e);
    }

    this.busyIndicator(false);

    console.info("loadOWLInstances - END");
  };

  /**
  * OWL - draw
  */
  Viewer.prototype.drawOWLInstances = function () {

    if (this._selectedObjects.length === 0) {
      this.drawOWLConceptualFaces(true);
      this.drawOWLConceptualFaces(false);
      //this.drawOWLFacesPolygons();
      this.drawOWLConceptualFacesPolygons();
      this.drawSelectedOWLInstances();
      this.drawPickedOWLInstance();
    }
    else {
      this.drawSelectedOWLInstances();
      this.drawPickedOWLInstance();
      this.drawOWLNotSelectedConceptualFaces();
    }
  };

  /**
  * OWL - Triangles
  */
  Viewer.prototype.drawOWLConceptualFaces = function (opaqueObjects) {
    if (g_instances.length == 0) {
      return;
    }

    this.setDefultMatrices();

    gl.uniform1f(this._shaderProgram.EnableLighting, 1.0)
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0)

    if (!opaqueObjects) {
      gl.enable(gl.BLEND);
      gl.blendEquation(gl.FUNC_ADD);
      gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
    }

    try {
      for (var i = 0; i < g_instances.length; i++) {
        if (!g_instances[i].visible) {
          continue;
        }

        var vertexSizeInBytes = 0;
        if (g_instances[i].VBO != undefined) {
          vertexSizeInBytes = g_instances[i].vertexSizeInBytes;

          gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].VBO);
          gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

          gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
          gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
        }

        for (var j = 0; j < g_instances[i].conceptualFaces.length; j++) {
          if (opaqueObjects) {
            if (g_instances[i].conceptualFaces[j].material.transparency < 1.0) {
              continue;
            }
          }
          else {
            if (g_instances[i].conceptualFaces[j].material.transparency == 1.0) {
              continue;
            }
          }

          if (g_instances[i].conceptualFaces[j].VBO != undefined) {
            vertexSizeInBytes = g_instances[i].conceptualFaces[j].vertexSizeInBytes;

            gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].conceptualFaces[j].VBO);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

            gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
            gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
          }

          if (g_instances[i].conceptualFaces[j].material.texture != undefined) {
            gl.uniform1f(this._shaderProgram.EnableTexture, 1.0);

            gl.vertexAttribPointer(this._shaderProgram.UV, 2, gl.FLOAT, false, vertexSizeInBytes, 24);
            gl.enableVertexAttribArray(this._shaderProgram.UV);

            gl.activeTexture(gl.TEXTURE0);
            gl.bindTexture(gl.TEXTURE_2D, g_instances[i].conceptualFaces[j].material.texture.instance);
            gl.uniform1i(this._shaderProgram.Sampler, 0);

            gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
          } // if (g_instances[i].conceptualFaces[j].material.texture != undefined)
          else {
            gl.uniform3f(this._shaderProgram.AmbientMaterial,
              g_instances[i].conceptualFaces[j].material.ambient[0],
              g_instances[i].conceptualFaces[j].material.ambient[1],
              g_instances[i].conceptualFaces[j].material.ambient[2]);
            gl.uniform3f(this._shaderProgram.SpecularMaterial,
              g_instances[i].conceptualFaces[j].material.diffuse[0],
              g_instances[i].conceptualFaces[j].material.diffuse[1],
              g_instances[i].conceptualFaces[j].material.diffuse[2]);
            gl.uniform3f(this._shaderProgram.DiffuseMaterial,
              g_instances[i].conceptualFaces[j].material.specular[0],
              g_instances[i].conceptualFaces[j].material.specular[1],
              g_instances[i].conceptualFaces[j].material.specular[2]);
            // #todo
            //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor,
            //  g_instances[i].conceptualFaces[j].material.emissive[0],
            //  g_instances[i].conceptualFaces[j].material.emissive[1],
            //  g_instances[i].conceptualFaces[j].material.emissive[2]);
            gl.uniform1f(this._shaderProgram.Transparency, g_instances[i].conceptualFaces[j].material.transparency);

            gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
            gl.disableVertexAttribArray(this._shaderProgram.UV);
          } // else if (g_instances[i].conceptualFaces[j].material.texture != undefined)

          if (g_instances[i].conceptualFaces[j].cohorts != undefined) {
            for (var c = 0; c < g_instances[i].conceptualFaces[j].cohorts.length; c++) {
              gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].conceptualFaces[j].cohorts[c].IBO);

              gl.drawElements(
                gl.TRIANGLES,
                g_instances[i].conceptualFaces[j].cohorts[c].IBO.count,
                gl.UNSIGNED_SHORT,
                0);
            } // for (var c = ...
          } // if (g_instances[i].conceptualFaces[j].cohorts != ...
          else {
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].conceptualFaces[j].IBO);

            gl.drawElements(
              gl.TRIANGLES,
              g_instances[i].conceptualFaces[j].IBO.count,
              gl.UNSIGNED_SHORT,
              0);
          } // else if (g_instances[i].conceptualFaces[j].cohorts != ...
        } // for (var j = ...
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }

    if (!opaqueObjects) {
      gl.disable(gl.BLEND);
    }
  };

  /**
  * OWL - Triangles
  */
  Viewer.prototype.drawOWLNotSelectedConceptualFaces = function () {
    if (g_instances.length == 0) {
      return;
    }

    this.setDefultMatrices();

    gl.enable(gl.BLEND);
    gl.blendEquation(gl.FUNC_ADD);
    gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);

    try {
      for (var i = 0; i < g_instances.length; i++) {
        if (!g_instances[i].visible) {
          continue;
        }

        if ((this._pickedObject - 1) === i) {
          continue;
        }

        var index = this._selectedObjects.indexOf(i + 1);
        if (index !== -1) {
          continue;
        }

        var vertexSizeInBytes = 0;
        if (g_instances[i].VBO != undefined) {
          vertexSizeInBytes = g_instances[i].vertexSizeInBytes;

          gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].VBO);
          gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

          gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
          gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
        }

        for (var j = 0; j < g_instances[i].conceptualFaces.length; j++) {
          if (g_instances[i].conceptualFaces[j].VBO != undefined) {
            vertexSizeInBytes = g_instances[i].conceptualFaces[j].vertexSizeInBytes;

            gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].conceptualFaces[j].VBO);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

            gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
            gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
          }

          gl.uniform3f(this._shaderProgram.AmbientMaterial,
            NOT_SELECTED_OBJECT_COLOR[0],
            NOT_SELECTED_OBJECT_COLOR[1],
            NOT_SELECTED_OBJECT_COLOR[2]);
          gl.uniform3f(this._shaderProgram.SpecularMaterial,
            NOT_SELECTED_OBJECT_COLOR[0],
            NOT_SELECTED_OBJECT_COLOR[1],
            NOT_SELECTED_OBJECT_COLOR[2]);
          gl.uniform3f(this._shaderProgram.DiffuseMaterial,
            NOT_SELECTED_OBJECT_COLOR[0],
            NOT_SELECTED_OBJECT_COLOR[1],
            NOT_SELECTED_OBJECT_COLOR[2]);
          // #todo
          //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor,
          //  NOT_SELECTED_OBJECT_COLOR[0],
          //  NOT_SELECTED_OBJECT_COLOR[1],
          //  NOT_SELECTED_OBJECT_COLOR[2]);
          gl.uniform1f(this._shaderProgram.Transparency, NOT_SELECTED_OBJECT_COLOR[3]);

          gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
          gl.disableVertexAttribArray(this._shaderProgram.UV);

          if (g_instances[i].conceptualFaces[j].cohorts != undefined) {
            for (var c = 0; c < g_instances[i].conceptualFaces[j].cohorts.length; c++) {
              gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].conceptualFaces[j].cohorts[c].IBO);

              gl.drawElements(
                gl.TRIANGLES,
                g_instances[i].conceptualFaces[j].cohorts[c].IBO.count,
                gl.UNSIGNED_SHORT,
                0);
            } // for (var c = ...
          } // if (g_instances[i].conceptualFaces[j].cohorts != ...
          else {
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].conceptualFaces[j].IBO);

            gl.drawElements(
              gl.TRIANGLES,
              g_instances[i].conceptualFaces[j].IBO.count,
              gl.UNSIGNED_SHORT,
              0);
          } // else if (g_instances[i].conceptualFaces[j].cohorts != ...
        } // for (var j = ...
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }

    gl.disable(gl.BLEND);
  };

  /**
  * OWL - Faces polygons
  */
  // DISABLED
  Viewer.prototype.drawOWLFacesPolygons = function () {
    if (g_instances.length == 0) {
      return;
    }

    this.setDefultMatrices();

    gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);

    gl.uniform3f(this._shaderProgram.AmbientMaterial, 0.0, 0.0, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);
    gl.uniform3f(this._shaderProgram.DiffuseMaterial, 0.0, 0.0, 0.0);
    gl.uniform3f(this._shaderProgram.SpecularMaterial, 0.0, 0.0, 0.0);
    // #todo
    //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, 0.0, 0.0, 0.0);

    try {
      for (var i = 0; i < g_instances.length; i++) {
        gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].VBO);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 32, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].facesPolygons.IBO);
        gl.drawElements(
          gl.LINES,
          g_instances[i].facesPolygons.IBO.count,
          gl.UNSIGNED_SHORT,
          0);
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }
  };

  /**
  * OWL - Conceptual faces polygons
  */
  Viewer.prototype.drawOWLConceptualFacesPolygons = function () {
    if (g_instances.length == 0) {
      return;
    }

    this.setDefultMatrices();

    gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);

    gl.uniform3f(this._shaderProgram.AmbientMaterial, 0.0, 0.0, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);
    gl.uniform3f(this._shaderProgram.DiffuseMaterial, 0.0, 0.0, 0.0);
    gl.uniform3f(this._shaderProgram.SpecularMaterial, 0.0, 0.0, 0.0);
    // #todo
    //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, 0.0, 0.0, 0.0);

    try {
      for (var i = 0; i < g_instances.length; i++) {
        if (!g_instances[i].visible) {
          continue;
        }

        if (g_instances[i].conceptualFacesPolygons == undefined) {
          continue;
        }

        var vertexSizeInBytes = 0;
        if (g_instances[i].VBO != undefined) {
          vertexSizeInBytes = g_instances[i].vertexSizeInBytes;

          gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].VBO);
          gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);
        }

        for (var j = 0; j < g_instances[i].conceptualFacesPolygons.length; j++) {
          var conceptualFacesPolygon = g_instances[i].conceptualFacesPolygons[j];

          if (conceptualFacesPolygon.VBO != undefined) {
            gl.bindBuffer(gl.ARRAY_BUFFER, conceptualFacesPolygon.VBO);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 12, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);
          } // if (conceptualFacesPolygon.VBO != ...		

          if (conceptualFacesPolygon.IBO != undefined) {
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, conceptualFacesPolygon.IBO);
            gl.drawElements(
              gl.LINES,
              conceptualFacesPolygon.IBO.count,
              gl.UNSIGNED_SHORT,
              0);
          } // if (conceptualFacesPolygon.IBO != ...
          else {
            if (conceptualFacesPolygon.cohorts != undefined) {
              for (var c = 0; c < conceptualFacesPolygon.cohorts.length; c++) {
                var cohort = conceptualFacesPolygon.cohorts[c];

                if (cohort.IBO != undefined) {
                  gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cohort.IBO);
                  gl.drawElements(
                    gl.LINES,
                    cohort.IBO.count,
                    gl.UNSIGNED_SHORT,
                    0);
                }
                else {
                  console.error("Unknown data model.");
                }
              } // for (var c = 
            } // if (conceptualFacesPolygon.cohorts != ...
            else {
              console.error("Unknown data model.");
            }
          } // else if (conceptualFacesPolygon.IBO != ...
        } // for (var j = ...
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }
  };

  /**
  * OWL - selection support
  */
  Viewer.prototype.drawOWLInstancesSelectionFrameBuffer = function () {
    if (g_instances.length == 0) {
      return;
    }

    try {
      /*
      * Encoding the selection colors
      */
      if (this._owlInstancesSelectionColors.length == 0) {
        var step = 1.0 / 255.0;

        for (var i = 0; i < g_instances.length; i++) {
          // build selection color
          var R = Math.floor((i + 1) / (255 * 255));
          if (R >= 1.0) {
            R *= step;
          }

          var G = Math.floor((i + 1) / 255);
          if (G >= 1.0) {
            G *= step;
          }

          var B = Math.floor((i + 1) % 255);
          B *= step;

          this._owlInstancesSelectionColors.push([R, G, B]);
        } // for (var i = ...
      } // if (this._owlInstancesSelectionColors.length == 0)

      this.setDefultMatrices();

      gl.bindFramebuffer(gl.FRAMEBUFFER, this._selectionFramebuffer);

      gl.viewport(0, 0, this._selectionFramebuffer.width, this._selectionFramebuffer.height);
      gl.clearColor(0.0, 0.0, 0.0, 0.0);
      gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

      gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
      gl.disableVertexAttribArray(this._shaderProgram.UV);

      gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
      gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
      gl.uniform1f(this._shaderProgram.Transparency, 1.0);

      for (var i = 0; i < g_instances.length; i++) {
        if (!g_instances[i].visible) {
          continue;
        }

        var vertexSizeInBytes = 0;
        if (g_instances[i].VBO != undefined) {
          vertexSizeInBytes = g_instances[i].vertexSizeInBytes;

          gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].VBO);
          gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);
        }

        gl.uniform3f(this._shaderProgram.AmbientMaterial,
          this._owlInstancesSelectionColors[i][0],
          this._owlInstancesSelectionColors[i][1],
          this._owlInstancesSelectionColors[i][2]);

        for (var j = 0; j < g_instances[i].conceptualFaces.length; j++) {
          if (g_instances[i].conceptualFaces[j].VBO != undefined) {
            vertexSizeInBytes = g_instances[i].conceptualFaces[j].vertexSizeInBytes;

            gl.bindBuffer(gl.ARRAY_BUFFER, g_instances[i].conceptualFaces[j].VBO);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);
          }

          if (g_instances[i].conceptualFaces[j].cohorts != undefined) {
            for (var c = 0; c < g_instances[i].conceptualFaces[j].cohorts.length; c++) {
              gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].conceptualFaces[j].cohorts[c].IBO);

              gl.drawElements(
                gl.TRIANGLES,
                g_instances[i].conceptualFaces[j].cohorts[c].IBO.count,
                gl.UNSIGNED_SHORT,
                0);
            }
          } // if (g_instances[i].conceptualFaces[j].cohorts != ...
          else {
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, g_instances[i].conceptualFaces[j].IBO);

            gl.drawElements(
              gl.TRIANGLES,
              g_instances[i].conceptualFaces[j].IBO.count,
              gl.UNSIGNED_SHORT,
              0);
          } // else if (g_instances[i].conceptualFaces[j].cohorts != ...
        } // for (var j = ...
      } // for (var i = ...

      gl.bindFramebuffer(gl.FRAMEBUFFER, null);
      gl.uniform1f(this._shaderProgram.EnableLighting, 1.0);
    }
    catch (ex) {
      console.error(ex);
    }
  };

  /**
  * OWL - selection support
  */
  Viewer.prototype.drawSelectedOWLInstances = function () {
    if (g_instances.length == 0) {
      return;
    }

    if (this._selectedObjects.length == 0) {
      return;
    }

    try {
      this.setDefultMatrices();

      gl.uniform3f(this._shaderProgram.AmbientMaterial,
        SELECTED_OBJECT_COLOR[0],
        SELECTED_OBJECT_COLOR[1],
        SELECTED_OBJECT_COLOR[2]);

      for (var index = 0; index < this._selectedObjects.length; index++) {
        var instance = g_instances[this._selectedObjects[index] - 1];

        var vertexSizeInBytes = 0;
        if (instance.VBO != undefined) {
          vertexSizeInBytes = instance.vertexSizeInBytes;

          gl.bindBuffer(gl.ARRAY_BUFFER, instance.VBO);
          gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

          gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
          gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
        }

        for (var j = 0; j < instance.conceptualFaces.length; j++) {
          if (instance.conceptualFaces[j].VBO != undefined) {
            vertexSizeInBytes = instance.conceptualFaces[j].vertexSizeInBytes;

            gl.bindBuffer(gl.ARRAY_BUFFER, instance.conceptualFaces[j].VBO);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

            gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
            gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
          }

          if (instance.conceptualFaces[j].cohorts != undefined) {
            for (var c = 0; c < instance.conceptualFaces[j].cohorts.length; c++) {
              gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, instance.conceptualFaces[j].cohorts[c].IBO);

              gl.drawElements(
                gl.TRIANGLES,
                instance.conceptualFaces[j].cohorts[c].IBO.count,
                gl.UNSIGNED_SHORT,
                0);
            }  // for (var c = ...
          } // if (instance.conceptualFaces[j].cohorts != ...
          else {
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, instance.conceptualFaces[j].IBO);

            gl.drawElements(
              gl.TRIANGLES,
              instance.conceptualFaces[j].IBO.count,
              gl.UNSIGNED_SHORT,
              0);
          } // else if (g_instances[i].conceptualFaces[j].cohorts != ...
        } // for (var j = ...
      } // for (var index = ...
    }
    catch (ex) {
      console.error(ex);
    }
  };

  /**
    * OWL - selection support
    */
  Viewer.prototype.drawPickedOWLInstance = function () {
    if (g_instances.length == 0) {
      return;
    }

    if (this._pickedObject == -1) {
      return;
    }

    try {
      this.setDefultMatrices();

      gl.uniform3f(this._shaderProgram.AmbientMaterial,
        PICKED_OBJECT_COLOR[0],
        PICKED_OBJECT_COLOR[1],
        PICKED_OBJECT_COLOR[2]);

      var instance = g_instances[this._pickedObject - 1];

      var vertexSizeInBytes = 0;
      if (instance.VBO != undefined) {
        vertexSizeInBytes = instance.vertexSizeInBytes;

        gl.bindBuffer(gl.ARRAY_BUFFER, instance.VBO);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
        gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
      }

      for (var j = 0; j < instance.conceptualFaces.length; j++) {
        if (instance.conceptualFaces[j].VBO != undefined) {
          vertexSizeInBytes = instance.conceptualFaces[j].vertexSizeInBytes;

          gl.bindBuffer(gl.ARRAY_BUFFER, instance.conceptualFaces[j].VBO);
          gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, vertexSizeInBytes, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

          gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, true, vertexSizeInBytes, 12);
          gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);
        }

        if (instance.conceptualFaces[j].cohorts != undefined) {
          for (var c = 0; c < instance.conceptualFaces[j].cohorts.length; c++) {
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, instance.conceptualFaces[j].cohorts[c].IBO);

            gl.drawElements(
              gl.TRIANGLES,
              instance.conceptualFaces[j].cohorts[c].IBO.count,
              gl.UNSIGNED_SHORT,
              0);
          }
        } // if (instance.conceptualFaces[j].cohorts != ...
        else {
          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, instance.conceptualFaces[j].IBO);

          gl.drawElements(
            gl.TRIANGLES,
            instance.conceptualFaces[j].IBO.count,
            gl.UNSIGNED_SHORT,
            0);
        } // else if (g_instances[i].conceptualFaces[j].cohorts != ...
      } // for (var j = ...
    }
    catch (ex) {
      console.error(ex);
    }
  };

  /**************************************************************************
  * Static models
  */

  /*
  * Loads static models
  */
  Viewer.prototype.loadStaticModels = function () {
    console.info("loadStaticModels - BEGIN");

    try {
      /*
      * Create models
      */
      for (var i = 0; i < g_staticInstances.length; i++) {
        var data = $.grep(g_staticInstancesData, function (e) {
          return e.instance == g_staticInstances[i].instance;
        });

        if ((data == null) || (data.length != 1)) {
          console.error("The data for instance '" + g_staticInstances[i].instance + "' is not found.");
        }

        var model = new StaticModel(g_staticInstances[i], data[0]);
        this._staticModels.push(model);
      } // for (var i = ...

      for (var i = 0; i < g_staticInstancesData.length; i++) {
        for (var f = 0; f < g_staticInstancesData[i].facesCohorts.length; f++) {
          var facesCohort = g_staticInstancesData[i].facesCohorts[f];

          var vertexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(facesCohort.vertices), gl.STATIC_DRAW);
          vertexBufferObject.length = facesCohort.vertices.length;
          facesCohort.vertices = vertexBufferObject;

          var indexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
          gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(facesCohort.indices), gl.STATIC_DRAW);
          indexBufferObject.length = facesCohort.indices.length;
          facesCohort.indices = indexBufferObject;

          var normalBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, normalBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(facesCohort.normals), gl.STATIC_DRAW);
          normalBufferObject.length = facesCohort.normals.length;
          facesCohort.normals = normalBufferObject;

          if (facesCohort.texture != undefined) {
            facesCohort.texture = this.createTexture(facesCohort.texture);
          }
          else {
            facesCohort.texture = null;
          }

          if (facesCohort.texture_vertices == undefined) {
            facesCohort.texture_vertices = [];
          }

          var textureBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, textureBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(facesCohort.texture_vertices), gl.STATIC_DRAW);
          textureBufferObject.length = facesCohort.texture_vertices.length;
          facesCohort.textureVertices = textureBufferObject;
        } // for (var f = ...

        for (var w = 0; w < g_staticInstancesData[i].wireframesCohorts.length; w++) {
          var wireframesCohort = g_staticInstancesData[i].wireframesCohorts[w];

          var vertexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(wireframesCohort.vertices), gl.STATIC_DRAW);
          vertexBufferObject.length = wireframesCohort.vertices.length;
          wireframesCohort.vertices = vertexBufferObject;

          var indexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
          gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(wireframesCohort.indices), gl.STATIC_DRAW);
          indexBufferObject.length = wireframesCohort.indices.length;
          wireframesCohort.indices = indexBufferObject;
        } // for (var w = ...
      } // for (var i = ...

      this.calculateStaticInstancesDimensions();
    }
    catch (e) {
      console.error(e);
    }

    console.info("loadStaticModels - END");
  }

  /**
  * Calculates the dimensions for the static instances
  */
  Viewer.prototype.calculateStaticInstancesDimensions = function () {
    if (g_staticInstances.length == 0) {
      return;
    }

    try {
      for (var i = 0; i < g_staticInstances.length; i++) {
        var staticInstance = g_staticInstances[i];

        for (var t = 0; t < staticInstance.transformations.length; t++) {
          var transformation = staticInstance.transformations[t];

          /*
          * Model-View matrix
          */
          this._mtxModelView[0] = transformation._11;
          this._mtxModelView[1] = transformation._12;
          this._mtxModelView[2] = transformation._13;
          this._mtxModelView[3] = 0;
          this._mtxModelView[4] = transformation._21;
          this._mtxModelView[5] = transformation._22;
          this._mtxModelView[6] = transformation._23;
          this._mtxModelView[7] = 0;
          this._mtxModelView[8] = transformation._31;
          this._mtxModelView[9] = transformation._32;
          this._mtxModelView[10] = transformation._33;
          this._mtxModelView[11] = 0;
          this._mtxModelView[12] = 0;
          this._mtxModelView[13] = 0;
          this._mtxModelView[14] = 0;
          this._mtxModelView[15] = 1;

          mat4.translate(this._mtxModelView, [transformation._41, transformation._42, transformation._43]);

          mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
          mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);

          var vertexMin = [staticInstance.Xmin, staticInstance.Ymin, staticInstance.Zmin, 1.0];
          var transformedVertexMin = [0, 0, 0, 0];

          mat4.multiplyVec4(this._mtxModelView, vertexMin, transformedVertexMin);

          if (this._worldDimensions.Xmin != undefined) {
            this._worldDimensions.Xmin = Math.min(this._worldDimensions.Xmin, transformedVertexMin[0]);
          }
          else {
            this._worldDimensions.Xmin = transformedVertexMin[0];
          }

          if (this._worldDimensions.Ymin != undefined) {
            this._worldDimensions.Ymin = Math.min(this._worldDimensions.Ymin, transformedVertexMin[1]);
          }
          else {
            this._worldDimensions.Ymin = transformedVertexMin[1];
          }

          if (this._worldDimensions.Zmin != undefined) {
            this._worldDimensions.Zmin = Math.min(this._worldDimensions.Zmin, transformedVertexMin[2]);
          }
          else {
            this._worldDimensions.Zmin = transformedVertexMin[2];
          }

          if (this._worldDimensions.Xmax != undefined) {
            this._worldDimensions.Xmax = Math.max(this._worldDimensions.Xmax, transformedVertexMin[0]);
          }
          else {
            this._worldDimensions.Xmax = transformedVertexMin[0];
          }

          if (this._worldDimensions.Ymax != undefined) {
            this._worldDimensions.Ymax = Math.max(this._worldDimensions.Ymax, transformedVertexMin[1]);
          }
          else {
            this._worldDimensions.Ymax = transformedVertexMin[1];
          }

          if (this._worldDimensions.Zmax != undefined) {
            this._worldDimensions.Zmax = Math.max(this._worldDimensions.Zmax, transformedVertexMin[2]);
          }
          else {
            this._worldDimensions.Zmax = transformedVertexMin[2];
          }

          var vertexMax = [staticInstance.Xmax, staticInstance.Ymax, staticInstance.Zmax, 1.0];
          var transformedVertexMax = [0, 0, 0, 0];

          mat4.multiplyVec4(this._mtxModelView, vertexMax, transformedVertexMax);

          this._worldDimensions.Xmin = Math.min(this._worldDimensions.Xmin, transformedVertexMax[0]);
          this._worldDimensions.Ymin = Math.min(this._worldDimensions.Ymin, transformedVertexMax[1]);
          this._worldDimensions.Zmin = Math.min(this._worldDimensions.Zmin, transformedVertexMax[2]);

          this._worldDimensions.Xmax = Math.max(this._worldDimensions.Xmax, transformedVertexMax[0]);
          this._worldDimensions.Ymax = Math.max(this._worldDimensions.Ymax, transformedVertexMax[1]);
          this._worldDimensions.Zmax = Math.max(this._worldDimensions.Zmax, transformedVertexMax[2]);
        } // for (var t = ...
      } // for (var i = ...

      this._staticWorldDimensions = $.extend({}, this._worldDimensions);

      this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
      this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
      this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

      this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
      this._eyeVector = vec3.create(this._defaultEyeVector);

      console.info(this._worldDimensions);
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Draws the conceptual faces of the static models
  */
  Viewer.prototype.drawStaticModelsFaces = function (opaqueObjects) {
    if (g_staticInstances.length == 0) {
      return;
    }

    if (!opaqueObjects) {
      gl.enable(gl.BLEND);
      gl.blendEquation(gl.FUNC_ADD);
      gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
    }

    try {
      for (var i = 0; i < g_staticInstances.length; i++) {
        if (!g_staticInstances[i].visible) {
          continue;
        }

        var staticInstanceData = null;
        for (var d = 0; d < g_staticInstancesData.length; d++) {
          if (g_staticInstances[i].instance == g_staticInstancesData[d].instance) {
            staticInstanceData = g_staticInstancesData[d];
            break;
          }
        } // for (var d = ...

        if (staticInstanceData == null) {
          continue;
        }

        for (var t = 0; t < g_staticInstances[i].transformations.length; t++) {
          var transformation = g_staticInstances[i].transformations[t];
          /*
          * Model-View matrix
          */
          this._mtxModelView[0] = transformation._11;
          this._mtxModelView[1] = transformation._12;
          this._mtxModelView[2] = transformation._13;
          this._mtxModelView[3] = 0;
          this._mtxModelView[4] = transformation._21;
          this._mtxModelView[5] = transformation._22;
          this._mtxModelView[6] = transformation._23;
          this._mtxModelView[7] = 0;
          this._mtxModelView[8] = transformation._31;
          this._mtxModelView[9] = transformation._32;
          this._mtxModelView[10] = transformation._33;
          this._mtxModelView[11] = 0;
          this._mtxModelView[12] = 0;
          this._mtxModelView[13] = 0;
          this._mtxModelView[14] = 0;
          this._mtxModelView[15] = 1;
          mat4.translate(this._mtxModelView, this._eyeVector);
          mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
          mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);

          mat4.translate(this._mtxModelView, [transformation._41, transformation._42, transformation._43]);

          /*
          * Fit the image
          */

          // [0.0 -> X/Y/Zmin + X/Y/Zmax]
          mat4.translate(this._mtxModelView,
            [
              -this._worldDimensions.Xmin,
              -this._worldDimensions.Ymin,
              -this._worldDimensions.Zmin
            ]);

          // Center
          mat4.translate(this._mtxModelView,
            [
              -(this._worldDimensions.Xmax - this._worldDimensions.Xmin) / 2,
              -(this._worldDimensions.Ymax - this._worldDimensions.Ymin) / 2,
              -(this._worldDimensions.Zmax - this._worldDimensions.Zmin) / 2
            ]);

          gl.uniformMatrix4fv(this._shaderProgram.ModelViewMatrix, false, this._mtxModelView);

          /*
          * Normals matrix
          */
          gl.uniformMatrix3fv(this._shaderProgram.NormalMatrix, false, mat4.toMat3(this._mtxModelView));

          for (var f = 0; f < staticInstanceData.facesCohorts.length; f++) {
            if (this.areAllStaticObjectsHidden(g_staticInstances[i].objectsCount, transformation)) {
              continue;
            }

            var facesCohort = staticInstanceData.facesCohorts[f];

            if (opaqueObjects) {
              if (facesCohort.transparency < 1.0) {
                continue;
              }
            }
            else {
              if (facesCohort.transparency == 1.0) {
                continue;
              }
            }

            gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.vertices);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

            if ((facesCohort.texture != null) && (facesCohort.textureVertices.length > 0)) {
              gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);

              gl.uniform1f(this._shaderProgram.EnableTexture, 1.0);

              gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.textureVertices);
              gl.vertexAttribPointer(this._shaderProgram.UV, 2, gl.FLOAT, false, 0, 0);
              gl.enableVertexAttribArray(this._shaderProgram.UV);

              gl.activeTexture(gl.TEXTURE0);
              gl.bindTexture(gl.TEXTURE_2D, facesCohort.texture);
              gl.uniform1i(this._shaderProgram.Sampler, 0);
            }
            else {
              gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.normals);
              gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, false, 0, 0);
              gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);

              gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
              gl.disableVertexAttribArray(this._shaderProgram.UV);
            }

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, facesCohort.indices);

            gl.uniform3f(this._shaderProgram.DiffuseMaterial, facesCohort.diffuse[0], facesCohort.diffuse[1], facesCohort.diffuse[2]);            
            gl.uniform3f(this._shaderProgram.SpecularMaterial, facesCohort.specular[0], facesCohort.specular[1], facesCohort.specular[2]);
            // #todo
            //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, facesCohort.emissive[0], facesCohort.emissive[1], facesCohort.emissive[2]);

            if (this.areAllStaticObjectsVisible(g_staticInstances[i].objectsCount, transformation)) {
              gl.uniform3f(this._shaderProgram.AmbientMaterial, facesCohort.ambient[0], facesCohort.ambient[1], facesCohort.ambient[2]);
              gl.uniform1f(this._shaderProgram.Transparency, facesCohort.transparency);

              gl.drawElements(gl.TRIANGLES, facesCohort.indices.length, gl.UNSIGNED_SHORT, 0);

              continue;
            }

            var offset = 0;
            for (var j = 0; j < facesCohort.lengths.length; j++) {
              if (transformation.owlTree[facesCohort.objects[j]].owlState == CHECKED_STATE) {
                gl.uniform3f(this._shaderProgram.AmbientMaterial, facesCohort.ambient[0], facesCohort.ambient[1], facesCohort.ambient[2]);
                gl.uniform1f(this._shaderProgram.Transparency, facesCohort.transparency);

                gl.drawElements(gl.TRIANGLES, facesCohort.lengths[j], gl.UNSIGNED_SHORT, offset * 2);
              } // for (var j = ...

              offset += facesCohort.lengths[j];
            } // for (var j = ...
          } // for (var f = ...
        } // for (var t = ...
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }

    if (!opaqueObjects) {
      gl.disable(gl.BLEND);
    }
  }

  /**
  * Draws the selected conceptual faces of the static object
  */
  Viewer.prototype.drawSelectedStaticObject = function () {
    if (g_staticInstances.length == 0) {
      return;
    }

    var selectedStaticObject = this.getSelectedStaticObject();
    if (selectedStaticObject == null) {
      return;
    }

    try {
      var staticInstance = g_staticInstances[selectedStaticObject.instance];

      var staticInstanceData = null;
      for (var d = 0; d < g_staticInstancesData.length; d++) {
        if (staticInstance.instance == g_staticInstancesData[d].instance) {
          staticInstanceData = g_staticInstancesData[d];
          break;
        }
      } // for (var d = ...

      if (staticInstanceData == null) {
        return;
      }

      var transformation = staticInstance.transformations[selectedStaticObject.transformation];

      /*
      * Model-View matrix
      */
      this._mtxModelView[0] = transformation._11;
      this._mtxModelView[1] = transformation._12;
      this._mtxModelView[2] = transformation._13;
      this._mtxModelView[3] = 0;
      this._mtxModelView[4] = transformation._21;
      this._mtxModelView[5] = transformation._22;
      this._mtxModelView[6] = transformation._23;
      this._mtxModelView[7] = 0;
      this._mtxModelView[8] = transformation._31;
      this._mtxModelView[9] = transformation._32;
      this._mtxModelView[10] = transformation._33;
      this._mtxModelView[11] = 0;
      this._mtxModelView[12] = 0;
      this._mtxModelView[13] = 0;
      this._mtxModelView[14] = 0;
      this._mtxModelView[15] = 1;
      mat4.translate(this._mtxModelView, this._eyeVector);
      mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
      mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);

      mat4.translate(this._mtxModelView, [transformation._41, transformation._42, transformation._43]);

      /*
      * Fit the image
      */

      // [0.0 -> X/Y/Zmin + X/Y/Zmax]
      mat4.translate(this._mtxModelView,
        [
          -this._worldDimensions.Xmin,
          -this._worldDimensions.Ymin,
          -this._worldDimensions.Zmin
        ]);

      // Center
      mat4.translate(this._mtxModelView,
        [
          -(this._worldDimensions.Xmax - this._worldDimensions.Xmin) / 2,
          -(this._worldDimensions.Ymax - this._worldDimensions.Ymin) / 2,
          -(this._worldDimensions.Zmax - this._worldDimensions.Zmin) / 2
        ]);

      gl.uniformMatrix4fv(this._shaderProgram.ModelViewMatrix, false, this._mtxModelView);

      /*
      * Normals matrix
      */gl.uniformMatrix3fv(this._shaderProgram.NormalMatrix, false, mat4.toMat3(this._mtxModelView));

      for (var f = 0; f < staticInstanceData.facesCohorts.length; f++) {
        var facesCohort = staticInstanceData.facesCohorts[f];

        if (this.areAllStaticObjectsHidden(g_staticInstances[selectedStaticObject.instance].objectsCount, transformation)) {
          continue;
        }

        gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.vertices);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.normals);
        gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);

        gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
        gl.disableVertexAttribArray(this._shaderProgram.UV);

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, facesCohort.indices);

        gl.uniform3f(this._shaderProgram.DiffuseMaterial, facesCohort.diffuse[0], facesCohort.diffuse[1], facesCohort.diffuse[2]);
        gl.uniform3f(this._shaderProgram.SpecularMaterial, facesCohort.specular[0], facesCohort.specular[1], facesCohort.specular[2]);
        // #todo
        //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, facesCohort.emissive[0], facesCohort.emissive[1], facesCohort.emissive[2]);
        gl.uniform1f(this._shaderProgram.Transparency, 1.0);

        var offset = 0;
        for (var j = 0; j < facesCohort.lengths.length; j++) {
          var visible = true;
          if ((transformation.owlTree != undefined) && (transformation.owlTree[facesCohort.objects[j]].owlState != CHECKED_STATE)) {
            visible = false;
          }

          if (visible) {
            if (selectedStaticObject.object == facesCohort.objects[j]) {
              gl.uniform3f(this._shaderProgram.AmbientMaterial, 1.0 - facesCohort.ambient[0], 1.0 - facesCohort.ambient[1], 1.0 - facesCohort.ambient[2]);

              gl.drawElements(gl.TRIANGLES, facesCohort.lengths[j], gl.UNSIGNED_SHORT, offset * 2);
            }
          }

          offset += facesCohort.lengths[j];
        } // for (var j = ...
      } // for (var f = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Draws the wireframes of the static models
  */
  Viewer.prototype.drawStaticModelsWireframes = function () {
    if (g_staticInstances.length == 0) {
      return;
    }

    gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);

    gl.uniform3f(this._shaderProgram.AmbientMaterial, 0.0, 0.0, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);
    gl.uniform3f(this._shaderProgram.DiffuseMaterial, 0.0, 0.0, 0.0);
    gl.uniform3f(this._shaderProgram.SpecularMaterial, 0.0, 0.0, 0.0);
    // #todo
    //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, 0.0, 0.0, 0.0);

    try {
      for (var i = 0; i < g_staticInstances.length; i++) {
        if (!g_staticInstances[i].visible) {
          continue;
        }

        var staticInstanceData = null;
        for (var d = 0; d < g_staticInstancesData.length; d++) {
          if (g_staticInstances[i].instance == g_staticInstancesData[d].instance) {
            staticInstanceData = g_staticInstancesData[d];
            break;
          }
        } // for (var d = ...

        if (staticInstanceData == null) {
          continue;
        }

        if (staticInstanceData.wireframesCohorts.length == 0) {
          continue;
        }

        for (var t = 0; t < g_staticInstances[i].transformations.length; t++) {
          var transformation = g_staticInstances[i].transformations[t];
          /*
          * Model-View matrix
          */
          this._mtxModelView[0] = transformation._11;
          this._mtxModelView[1] = transformation._12;
          this._mtxModelView[2] = transformation._13;
          this._mtxModelView[3] = 0;
          this._mtxModelView[4] = transformation._21;
          this._mtxModelView[5] = transformation._22;
          this._mtxModelView[6] = transformation._23;
          this._mtxModelView[7] = 0;
          this._mtxModelView[8] = transformation._31;
          this._mtxModelView[9] = transformation._32;
          this._mtxModelView[10] = transformation._33;
          this._mtxModelView[11] = 0;
          this._mtxModelView[12] = 0;
          this._mtxModelView[13] = 0;
          this._mtxModelView[14] = 0;
          this._mtxModelView[15] = 1;
          mat4.translate(this._mtxModelView, this._eyeVector);
          mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
          mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);
          mat4.translate(this._mtxModelView, [transformation._41, transformation._42, transformation._43]);

          /*
          * Fit the image
          */

          // [0.0 -> X/Y/Zmin + X/Y/Zmax]
          mat4.translate(this._mtxModelView,
            [
              -this._worldDimensions.Xmin,
              -this._worldDimensions.Ymin,
              -this._worldDimensions.Zmin
            ]);

          // Center
          mat4.translate(this._mtxModelView,
            [
              -(this._worldDimensions.Xmax - this._worldDimensions.Xmin) / 2,
              -(this._worldDimensions.Ymax - this._worldDimensions.Ymin) / 2,
              -(this._worldDimensions.Zmax - this._worldDimensions.Zmin) / 2
            ]);

          gl.uniformMatrix4fv(this._shaderProgram.ModelViewMatrix, false, this._mtxModelView);

          /*
          * Normals matrix
          */
          gl.uniformMatrix3fv(this._shaderProgram.NormalMatrix, false, mat4.toMat3(this._mtxModelView));

          for (var w = 0; w < staticInstanceData.wireframesCohorts.length; w++) {
            if (this.areAllStaticObjectsHidden(g_staticInstances[i].objectsCount, transformation)) {
              continue;
            }

            var wirframesCohort = staticInstanceData.wireframesCohorts[w];

            gl.bindBuffer(gl.ARRAY_BUFFER, wirframesCohort.vertices);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, wirframesCohort.indices);

            if (this.areAllStaticObjectsVisible(g_staticInstances[i].objectsCount, transformation)) {
              gl.drawElements(
                gl.LINES,
                wirframesCohort.indices.length,
                gl.UNSIGNED_SHORT,
                0);

              continue;
            }

            var offset = 0;
            for (var j = 0; j < wirframesCohort.lengths.length; j++) {
              if (transformation.owlTree[wirframesCohort.objects[j]].owlState == CHECKED_STATE) {
                gl.drawElements(
                  gl.LINES,
                  wirframesCohort.lengths[j],
                  gl.UNSIGNED_SHORT,
                  offset * 2);
              } // for (var j = ...

              offset += wirframesCohort.lengths[j];
            } // for (var j = ...
          } // for (var w = ...
        } // for (var t = ...
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Draws the conceptual faces of the static instances in a frame buffer
  */
  Viewer.prototype.drawStaticModelsSelectionFrameBuffer = function () {
    if (g_staticInstances.length == 0) {
      return;
    }

    try {
      /*
      * Encoding the selection colors
      */
      if (this._staticObjectsSelectionColors.length == 0) {
        for (var i = 0; i < g_staticInstances.length; i++) {
          var staticInstanceSelectionColors = [];

          for (var t = 0; t < g_staticInstances[i].transformations.length; t++) {
            var transformation = g_staticInstances[i].transformations[t];

            transformation.objectsBaseIndex = this._objectsBaseIndex;
            this._objectsBaseIndex += g_staticInstances[i].objectsCount;

            var transformationSelectionColors = [];

            var step = 1.0 / 255.0;
            for (var iObject = 0; iObject < g_staticInstances[i].objectsCount; iObject++) {
              // build selection color
              var R = Math.floor((transformation.objectsBaseIndex + iObject) / (255 * 255));
              if (R >= 1.0) {
                R *= step;
              }

              var G = Math.floor((transformation.objectsBaseIndex + iObject) / 255);
              if (G >= 1.0) {
                G *= step;
              }

              var B = Math.floor((transformation.objectsBaseIndex + iObject) % 255);
              B *= step;

              transformationSelectionColors.push([R, G, B]);
            } // for (var iObject = ...

            staticInstanceSelectionColors.push(transformationSelectionColors);
          } // for (var t = ...

          this._staticObjectsSelectionColors.push(staticInstanceSelectionColors);
        } // for (var i = ...
      } // if (this._staticObjectsSelectionColors.length == 0)

      for (var i = 0; i < g_staticInstances.length; i++) {
        if (!g_staticInstances[i].visible) {
          continue;
        }

        var staticInstanceData = null;
        for (var d = 0; d < g_staticInstancesData.length; d++) {
          if (g_staticInstances[i].instance == g_staticInstancesData[d].instance) {
            staticInstanceData = g_staticInstancesData[d];
            break;
          }
        } // for (var d = ...

        if (staticInstanceData == null) {
          continue;
        }

        for (var t = 0; t < g_staticInstances[i].transformations.length; t++) {
          var transformation = g_staticInstances[i].transformations[t];
          /*
          * Model-View matrix
          */
          this._mtxModelView[0] = transformation._11;
          this._mtxModelView[1] = transformation._12;
          this._mtxModelView[2] = transformation._13;
          this._mtxModelView[3] = 0;
          this._mtxModelView[4] = transformation._21;
          this._mtxModelView[5] = transformation._22;
          this._mtxModelView[6] = transformation._23;
          this._mtxModelView[7] = 0;
          this._mtxModelView[8] = transformation._31;
          this._mtxModelView[9] = transformation._32;
          this._mtxModelView[10] = transformation._33;
          this._mtxModelView[11] = 0;
          this._mtxModelView[12] = 0;
          this._mtxModelView[13] = 0;
          this._mtxModelView[14] = 0;
          this._mtxModelView[15] = 1;
          mat4.translate(this._mtxModelView, this._eyeVector);
          mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
          mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);
          mat4.translate(this._mtxModelView, [transformation._41, transformation._42, transformation._43]);

          /*
          * Fit the image
          */

          // [0.0 -> X/Y/Zmin + X/Y/Zmax]
          mat4.translate(this._mtxModelView,
            [
              -this._worldDimensions.Xmin,
              -this._worldDimensions.Ymin,
              -this._worldDimensions.Zmin
            ]);

          // Center
          mat4.translate(this._mtxModelView,
            [
              -(this._worldDimensions.Xmax - this._worldDimensions.Xmin) / 2,
              -(this._worldDimensions.Ymax - this._worldDimensions.Ymin) / 2,
              -(this._worldDimensions.Zmax - this._worldDimensions.Zmin) / 2
            ]);

          gl.uniformMatrix4fv(this._shaderProgram.ModelViewMatrix, false, this._mtxModelView);

          /*
          * Normals matrix
          */
          gl.uniformMatrix3fv(this._shaderProgram.NormalMatrix, false, mat4.toMat3(this._mtxModelView));

          for (var f = 0; f < staticInstanceData.facesCohorts.length; f++) {
            if (this.areAllStaticObjectsHidden(g_staticInstances[i].objectsCount, transformation)) {
              continue;
            }

            var facesCohort = staticInstanceData.facesCohorts[f];

            gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.vertices);
            gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
            gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, facesCohort.indices);

            var offset = 0;
            for (var j = 0; j < facesCohort.lengths.length; j++) {
              var visible = true;
              if ((transformation.owlTree != undefined) && (transformation.owlTree[facesCohort.objects[j]].owlState != CHECKED_STATE)) {
                visible = false;
              }

              if (visible) {
                gl.uniform3f(this._shaderProgram.AmbientMaterial,
                  this._staticObjectsSelectionColors[i][t][facesCohort.objects[j]][0],
                  this._staticObjectsSelectionColors[i][t][facesCohort.objects[j]][1],
                  this._staticObjectsSelectionColors[i][t][facesCohort.objects[j]][2]);

                gl.drawElements(gl.TRIANGLES, facesCohort.lengths[j], gl.UNSIGNED_SHORT, offset * 2);
              }

              offset += facesCohort.lengths[j];
            } // for (var j = ...
          } // for (var f = ...
        } // for (var t = ...
      } // for (var i = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Whether all static objects are visible
  */
  Viewer.prototype.areAllStaticObjectsVisible = function (objectsCount, transformation) {
    if (transformation.owlTree == undefined) {
      return true;
    }

    for (var i = 0; i < objectsCount; i++) {
      if (transformation.owlTree[i].owlState != CHECKED_STATE) {
        return false;
      }
    }

    return true;
  }

  /**
  * Whether all static objects are hidden
  */
  Viewer.prototype.areAllStaticObjectsHidden = function (objectsCount, transformation) {
    if (transformation.owlTree == undefined) {
      return false;
    }

    for (var i = 0; i < objectsCount; i++) {
      if (transformation.owlTree[i].owlState == CHECKED_STATE) {
        return false;
      }
    }

    return true;
  }

  /**
  * Retrieves the selected static model, transformation and object
  */
  Viewer.prototype.getSelectedStaticObject = function () {
    if (this._pickedObject == -1) {
      return null;
    }

    for (var i = 0; i < g_staticInstances.length; i++) {
      for (var t = 0; t < g_staticInstances[i].transformations.length; t++) {
        var transformation = g_staticInstances[i].transformations[t];

        if ((this._pickedObject >= transformation.objectsBaseIndex) && (this._pickedObject < (transformation.objectsBaseIndex + g_staticInstances[i].objectsCount))) {
          var selectedStaticObject = {};
          selectedStaticObject.instance = i;
          selectedStaticObject.transformation = t;
          selectedStaticObject.object = this._pickedObject - transformation.objectsBaseIndex;

          return selectedStaticObject;
        }
      } // for (var t = ...
    } // for (var i = ...

    return null;
  }

  /**
  * Draws the static models
  */
  Viewer.prototype.drawStaticModels = function () {
    this.drawStaticModelsFaces(true);
    this.drawStaticModelsFaces(false);
    this.drawSelectedStaticObject();
    this.drawStaticModelsWireframes();
  }

  /**
  * Shows/hides a static instance
  */
  Viewer.prototype.showStaticInstance = function (file, visible) {
    if (g_staticInstances.length == 0) {
      return;
    }

    for (var i = 0; i < g_staticInstances.length; i++) {
      if (g_staticInstances[i].file == file) {
        g_staticInstances[i].visible = visible;

        return;
      }
    }

    console.error("File '" + file + "' not found.");
  }

  /**
  * Finds an object by GUID
  */
  Viewer.prototype.findStaticObject = function (guid) {
    for (var i = 0; i < g_staticInstances.length; i++) {
      var staticInstance = g_staticInstances[i];

      var staticInstanceData = null;
      var staticInstanceDataIndex = -1;
      for (var d = 0; d < g_staticInstancesData.length; d++) {
        if (g_staticInstancesData[d].instance == staticInstance.instance) {
          staticInstanceData = g_staticInstancesData[d];
          staticInstanceDataIndex = d;
          break;
        }
      } // for (var d = ...

      if (staticInstanceData == null) {
        continue;
      }

      var result = $.grep(staticInstanceData.owlTree.items, function (e) {
        return e.name == guid;
      });

      if (result.length == 1) {
        var object = {};
        object.instanceIndex = i;
        object.dataIndex = staticInstanceDataIndex;
        object.owlItem = result[0];

        return object;
      }
    } // for (var i = ...

    return null;
  }

  /**
  * Shows/hides an object
  */
  Viewer.prototype.showStaticObject = function (guid, visible) {
    try {
      var object = findStaticObject(guid);
      if (object == null) {
        console.error("GUID '" + guid + "' doesn't exist.");
      }

      var staticInstance = g_staticInstances[object.instanceIndex];
      var staticInstanceData = g_staticInstancesData[object.dataIndex];
      var transformation = staticInstance.transformations[0];

      if (transformation.owlTree == undefined) {
        var owlTree = staticInstanceData.owlTree;

        transformation.owlTree = [];
        for (var i = 0; i < owlTree.items.length; i++) {
          var owlItem = {};
          owlItem.owlState = CHECKED_STATE; // all objects are checked by default
          owlItem.isLoaded = false;
          owlItem.treeNode = null;

          transformation.owlTree.push(owlItem);
        } // for (var i = ...
      }

      transformation.owlTree[object.owlItem.id].owlState = visible ? CHECKED_STATE : UNCHECKED_STATE;
      updateOWLDescendants(object.dataIndex, transformation, object.owlItem.id, visible ? CHECKED_STATE : UNCHECKED_STATE);
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Shows/hides an object
  */
  Viewer.prototype.showStaticObjectEx = function (model, guid, visible) {
    try {
      var staticInstance = null;
      for (var i = 0; i < g_staticInstances.length; i++) {
        if (g_staticInstances[i].file == model) {
          staticInstance = g_staticInstances[i];
          break;
        }
      }

      if (staticInstance == null) {
        console.error("Static instance '" + model + "' doesn't exist.");
        return;
      }

      var staticInstanceData = null;
      var staticInstanceDataIndex = -1;
      for (var d = 0; d < g_staticInstancesData.length; d++) {
        if (g_staticInstancesData[d].instance == staticInstance.instance) {
          staticInstanceData = g_staticInstancesData[d];
          staticInstanceDataIndex = d;
          break;
        }
      } // for (var d = ...

      if (staticInstanceData == null) {
        console.error("Static instance data'" + model + "' doesn't exist.");
        return;
      }

      var result = $.grep(staticInstanceData.owlTree.items, function (e) {
        return e.name == guid;
      });

      if (result.length == 0) {
        console.error("GUID '" + guid + "' doesn't exist.");
        return;
      }

      console.debug(result[0]);

      var transformation = staticInstance.transformations[0];

      if (transformation.owlTree == undefined) {
        var owlTree = staticInstanceData.owlTree;

        transformation.owlTree = [];
        for (var i = 0; i < owlTree.items.length; i++) {
          var owlItem = {};
          owlItem.owlState = CHECKED_STATE; // all objects are checked by default
          owlItem.isLoaded = false;
          owlItem.treeNode = null;

          transformation.owlTree.push(owlItem);
        } // for (var i = ...
      }

      transformation.owlTree[result[0].id].owlState = visible ? CHECKED_STATE : UNCHECKED_STATE;
      updateOWLDescendants(staticInstanceDataIndex, transformation, result[0].id, visible ? CHECKED_STATE : UNCHECKED_STATE);
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Uploads a static instance
  */
  Viewer.prototype.uploadFile = function (file, callback) {
    var viewer = this;

    if (callback != undefined) {
      callback(UPLOAD_EVENT_BEGIN, file);
    }

    var command = "/createStaticFile";
    console.info('Requesting ' + command);

    var request = new XMLHttpRequest();
    request.open("POST", command);
    request.setRequestHeader("pragma", "no-cache");
    request.setRequestHeader("cache-control", "no-cache");
    request.setRequestHeader('file', file.name);
    request.responseType = "text";

    request.onreadystatechange = function () {
      if (request.readyState == 4) {
        switch (request.status) {
          case 200:
            viewer.uploadFileEx(file, callback);
            break;

          case 404:
            console.error(command + ' does not exist.');
            break;

          case 400:
            var response = JSON.parse(request.responseText);
            console.error(response.error);
            break;

          default:
            console.error("Unknown error.");
            break;
        } // switch (request.status)
      }
    }

    request.onerror = function (e) {
      console.error(e);
    };

    request.send();
  }

  /**
  * Uploads a static instance
  */
  Viewer.prototype.uploadFileEx = function (file, callback) {
    try {
      var viewer = this;

      var command = "/updloadStaticFile";
      console.info('Requesting ' + command);

      var request = new XMLHttpRequest();
      request.open("POST", command);
      request.setRequestHeader("pragma", "no-cache");
      request.setRequestHeader("cache-control", "no-cache");
      request.setRequestHeader('file', file.name);
      request.responseType = "text";

      request.onreadystatechange = function () {
        if (request.readyState == 4) {
          if (callback != undefined) {
            callback(UPLOAD_EVENT_END, file);
          }

          switch (request.status) {
            case 200:
              viewer.getStaticModels();
              break;

            case 404:
              console.error(command + ' does not exist.');
              break;

            default:
              console.error("Unknown error.");
              break;
          } // switch (request.status)                
        }
      }

      request.onload = function (e) {
        console.info(onload);
      };

      request.upload.onprogress = function (e) {
        if (e.lengthComputable) {
          if (callback != undefined) {
            var progress = (e.loaded / e.total) * 100;

            callback(UPLOAD_EVENT_PROGRESS, file, progress);
          }
        }
      };

      request.onerror = function (e) {
        console.error(e);
      };

      request.send(file);
    }
    catch (e) {
      console.error(e);
    }
  }

  /**
  * Retrieves the static models from server
  */
  Viewer.prototype.getStaticModels = function () {
    try {
      var viewer = this;

      var command = "/getStaticInstances";
      console.info('Requesting ' + command);

      var request = new XMLHttpRequest();
      request.open("POST", command);
      request.setRequestHeader("pragma", "no-cache");
      request.setRequestHeader("cache-Control", "no-cache");
      request.responseType = "text";

      request.onreadystatechange = function () {
        if (request.readyState == 4) {
          if (request.status == 404) {
            console.info(command + ' does not exist.');
          }
          else {
            try {
              var response = JSON.parse(request.responseText);

              g_staticInstances = response.staticInstances;
              g_staticInstancesData = [];
              g_owlTreeStaticInstance = -1;
              g_owlTreeTransformation = -1;

              this._objectsBaseIndex = 0;
              this._dynamicObjectsBaseIndex = 0;
              this._dynamicObjectsSelectionColors = [];
              this._staticObjectsSelectionColors = [];

              viewer.calculateStaticInstancesDimensions();

              viewer.getStaticModelsData();
            }
            catch (ex) {
              console.error(ex);
            }
          }
        }
      }

      request.onerror = function (e) {
        console.error(request.statusText);
      };

      request.send();
    }
    catch (e) {
      console.error(e);
    }
  }

  /**
  * Retrieves the static models from server
  */
  Viewer.prototype.getStaticModelsData = function () {
    try {
      g_staticInstancesData = [];
      for (var i = 0; i < g_staticInstances.length; i++) {
        var staticInstanceData = {};
        staticInstanceData.instance = g_staticInstances[i].instance;

        staticInstanceData.facesCohorts = [];
        staticInstanceData.wireframesCohorts = [];
        staticInstanceData.owlTree = {};

        g_staticInstancesData.push(staticInstanceData);

        for (var f = 0; f < g_staticInstances[i].facesCohortsCount; f++) {
          this.getStaticModelFacesCohort(staticInstanceData, f);
        }

        for (var w = 0; w < g_staticInstances[i].wireframesCohortsCount; w++) {
          this.getStaticModelWireframesCohort(staticInstanceData, w);
        }

        this.getStaticModelOWLTree(staticInstanceData);
      } // for (var i = ...
    }
    catch (e) {
      console.error(e);
    }
  }

  /**
  * Retrieves a faces cohort for a static model
  */
  Viewer.prototype.getStaticModelFacesCohort = function (staticInstanceData, index) {
    try {
      var viewer = this;

      var command = "/getStaticInstanceFaces";
      console.info('Requesting ' + command);

      var request = new XMLHttpRequest();
      request.open("GET", command + ((/\?/).test(command) ? "&" : "?") + Math.random().toFixed(20).replace('.', ''));
      request.setRequestHeader("pragma", "no-cache");
      request.setRequestHeader("cache-control", "no-cache");
      request.setRequestHeader('instance', staticInstanceData.instance);
      request.setRequestHeader('cohort', index);
      request.responseType = "text";

      request.onreadystatechange = function () {
        if (request.readyState == 4) {
          if (request.status == 404) {
            console.error(command + ' does not exist.');
          }
          else {
            try {
              viewer.onStaticModelFacesCohortReceieved(staticInstanceData, JSON.parse(request.responseText), index);
            }
            catch (ex) {
              console.error(ex);
            }
          }
        }
      }

      request.onerror = function (e) {
        console.error(request.statusText);
      };

      request.send();
    }
    catch (e) {
      console.error(e);
    }
  }

  /**
  * The definition of a face for a static model has been received
  */
  Viewer.prototype.onStaticModelFacesCohortReceieved = function (staticInstanceData, response, index) {
    try {
      var facesCohort = {};

      facesCohort.ambient = response.ambient;
      facesCohort.transparency = response.transparency;
      facesCohort.specular = response.specular;
      facesCohort.diffuse = response.diffuse;
      facesCohort.emissive = response.emissive;

      var vertexBufferObject = gl.createBuffer();
      gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
      gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(response.vertices), gl.STATIC_DRAW);
      vertexBufferObject.length = response.vertices.length;
      facesCohort.vertices = vertexBufferObject;

      var indexBufferObject = gl.createBuffer();
      gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
      gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(response.indices), gl.STATIC_DRAW);
      indexBufferObject.length = response.indices.length;
      facesCohort.indices = indexBufferObject;

      var normalBufferObject = gl.createBuffer();
      gl.bindBuffer(gl.ARRAY_BUFFER, normalBufferObject);
      gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(response.normals), gl.STATIC_DRAW);
      normalBufferObject.length = response.normals.length;
      facesCohort.normals = normalBufferObject;

      if (response.texture != undefined) {
        facesCohort.texture = this.createTexture(response.texture);
      }
      else {
        facesCohort.texture = null;
      }

      if (response.texture_vertices == undefined) {
        response.texture_vertices = [];
      }

      var textureBufferObject = gl.createBuffer();
      gl.bindBuffer(gl.ARRAY_BUFFER, textureBufferObject);
      gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(response.texture_vertices), gl.STATIC_DRAW);
      textureBufferObject.length = response.texture_vertices.length;
      facesCohort.textureVertices = textureBufferObject;

      facesCohort.lengths = response.lengths;
      facesCohort.objects = response.objects;

      gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, null);
      gl.bindBuffer(gl.ARRAY_BUFFER, null);

      staticInstanceData.facesCohorts.push(facesCohort);
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Retrieves wireframes cohort for a static model
  */
  Viewer.prototype.getStaticModelWireframesCohort = function (staticInstanceData, index) {
    try {
      var viewer = this;

      var command = "/getStaticInstanceWireframes";
      console.info('Requesting ' + command);

      var request = new XMLHttpRequest();
      request.open("POST", command);
      request.setRequestHeader("pragma", "no-cache");
      request.setRequestHeader("cache-control", "no-cache");
      request.setRequestHeader('instance', staticInstanceData.instance);
      request.setRequestHeader('cohort', index);
      request.responseType = "text";

      request.onreadystatechange = function () {
        if (request.readyState == 4) {
          if (request.status == 404) {
            console.error(command + ' does not exist.');
          }
          else {
            try {
              viewer.onStaticModelWireframesCohortReceieved(staticInstanceData, JSON.parse(request.responseText));
            }
            catch (ex) {
              console.error(ex);
            }
          }
        }
      }

      request.onerror = function (e) {
        console.error(request.statusText);
      };

      request.send();
    }
    catch (e) {
      console.error(e);
    }
  }

  /**
  * The definition of a wireframe for a static model has been received
  */
  Viewer.prototype.onStaticModelWireframesCohortReceieved = function (staticInstanceData, response) {
    try {
      var wireframesCohort = {};
      var vertexBufferObject = gl.createBuffer();
      gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
      gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(response.vertices), gl.STATIC_DRAW);
      vertexBufferObject.length = response.vertices.length;
      wireframesCohort.vertices = vertexBufferObject;

      var indexBufferObject = gl.createBuffer();
      gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
      gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(response.indices), gl.STATIC_DRAW);
      indexBufferObject.length = response.indices.length;
      wireframesCohort.indices = indexBufferObject;

      wireframesCohort.lengths = response.lengths;
      wireframesCohort.objects = response.objects;

      staticInstanceData.wireframesCohorts.push(wireframesCohort);
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Retrieves an OWL tree from server
  */
  Viewer.prototype.getStaticModelOWLTree = function (staticInstanceData) {
    try {
      var viewer = this;

      var command = "/getStaticInstanceOWLTree";
      console.info('Requesting ' + command);

      var request = new XMLHttpRequest();
      request.open("POST", command);
      request.setRequestHeader("pragma", "no-cache");
      request.setRequestHeader("cache-control", "no-cache");
      request.setRequestHeader('instance', staticInstanceData.instance);
      request.responseType = "text";

      request.onreadystatechange = function () {
        if (request.readyState == 4) {
          if (request.status == 404) {
            console.error(command + ' does not exist.');
          }
          else {
            try {
              viewer.onStaticModelOWLTreeReceieved(staticInstanceData, JSON.parse(request.responseText));
            }
            catch (ex) {
              console.error(ex);
            }
          }
        }
      }

      request.onerror = function (e) {
        console.error(request.statusText);
      };

      request.send();
    }
    catch (e) {
      console.error(e);
    }
  }

  /**
  * The definition of an OWL tree has been received
  */
  Viewer.prototype.onStaticModelOWLTreeReceieved = function (staticInstanceData, response) {
    try {
      staticInstanceData.owlTree = response;
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**************************************************************************
  * Dynamic models
  */

  /**
  * Loads dynamic models
  */
  Viewer.prototype.loadDynamicModels = function () {
    console.info("loadDynamicModels - BEGIN");

    try {
      /*
      * Faces
      */
      for (var i = 0; i < g_dynamicInstancesData.length; i++) {
        for (var f = 0; f < g_dynamicInstancesData[i].facesCohorts.length; f++) {
          var facesCohort = g_dynamicInstancesData[i].facesCohorts[f];

          var vertexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(facesCohort.vertices), gl.STATIC_DRAW);
          vertexBufferObject.length = facesCohort.vertices.length;
          facesCohort.vertices = vertexBufferObject;

          var indexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
          gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(facesCohort.indices), gl.STATIC_DRAW);
          indexBufferObject.length = facesCohort.indices.length;
          facesCohort.indices = indexBufferObject;

          var normalBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, normalBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(facesCohort.normals), gl.STATIC_DRAW);
          normalBufferObject.length = facesCohort.normals.length;
          facesCohort.normals = normalBufferObject;

          if (facesCohort.texture != undefined) {
            facesCohort.texture = this.createTexture(facesCohort.texture);
          }
          else {
            facesCohort.texture = null;
          }

          if (facesCohort.texture_vertices == undefined) {
            facesCohort.texture_vertices = [];
          }

          var textureBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, textureBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(facesCohort.texture_vertices), gl.STATIC_DRAW);
          textureBufferObject.length = facesCohort.texture_vertices.length;
          facesCohort.textureVertices = textureBufferObject;
        } // for (var f = ...

        /*
        * Wireframes
        */
        for (var w = 0; w < g_dynamicInstancesData[i].wireframesCohorts.length; w++) {
          var wireframesCohort = g_dynamicInstancesData[i].wireframesCohorts[w];

          var vertexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(wireframesCohort.vertices), gl.STATIC_DRAW);
          vertexBufferObject.length = wireframesCohort.vertices.length;
          wireframesCohort.vertices = vertexBufferObject;

          var indexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
          gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(wireframesCohort.indices), gl.STATIC_DRAW);
          indexBufferObject.length = wireframesCohort.indices.length;
          wireframesCohort.indices = indexBufferObject;
        } // for (var w = ...

        /*
        * Points
        */
        for (var p = 0; p < g_dynamicInstancesData[i].pointsCohorts.length; p++) {
          var pointsCohort = g_dynamicInstancesData[i].pointsCohorts[p];

          var vertexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
          gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(pointsCohort.vertices), gl.STATIC_DRAW);
          vertexBufferObject.length = pointsCohort.vertices.length;
          pointsCohort.vertices = vertexBufferObject;

          var indexBufferObject = gl.createBuffer();
          gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBufferObject);
          gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(pointsCohort.indices), gl.STATIC_DRAW);
          indexBufferObject.length = pointsCohort.indices.length;
          pointsCohort.indices = indexBufferObject;
        } // for (var p = ...
      } // for (var i = ...            

      /*
      * Calculate the number of transformations and world's dimensions
      */

      /*
      * Reset the transformation index
      */
      for (var i = 0; i < g_dynamicInstances.length; i++) {
        g_dynamicInstances[i].transformationIndex = 0;
      }

      this._dynamicInstancesCalcuationMode = true;
      eval(g_owlParts[g_owlActivePart]);
      this._dynamicInstancesCalcuationMode = false;

      this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
      this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
      this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

      this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
      this._eyeVector = vec3.create(this._defaultEyeVector);

      console.info(this._worldDimensions);

      if (typeof (onViewerDynamicContentLoaded) === typeof (Function)) {
        onViewerDynamicContentLoaded();
      }
    }
    catch (e) {
      console.error(e);
    }

    console.info("loadDynamicModels - END");
  }

  /**
  * Calculates the selected dynamic instance, transformation and object
  */
  Viewer.prototype.calculateSelectedDynamicObject = function () {
    this._selectedDynamicObject = null;

    if (this._pickedObject == -1) {
      return;
    }

    for (var i = 0; i < g_dynamicInstances.length; i++) {
      var objectsBaseIndex = g_dynamicInstances[i].objectsBaseIndex;

      for (var t = 0; t < g_dynamicInstances[i].transformationsCount; t++) {
        if ((this._pickedObject >= objectsBaseIndex) && (this._pickedObject < (objectsBaseIndex + g_dynamicInstances[i].objectsCount))) {
          this._selectedDynamicObject = {};
          this._selectedDynamicObject.instance = g_dynamicInstances[i].instance;
          this._selectedDynamicObject.transformation = t;
          this._selectedDynamicObject.object = this._pickedObject - objectsBaseIndex;

          return;
        }

        objectsBaseIndex += g_dynamicInstances[i].objectsCount;
      } // for (var t = ...
    } // for (var i = ...
  }

  /**
  * Draws the dynamic models
  */
  Viewer.prototype.drawDynamicModels = function Viewer_drawDynamicModels() {
    this.calculateSelectedDynamicObject();

    /*
    * Reset the transformation index
    */
    for (var i = 0; i < g_dynamicInstances.length; i++) {
      g_dynamicInstances[i].transformationIndex = 0;
    }

    eval(g_owlParts[g_owlActivePart]);
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlTransform = function (transformation) {
    if (this._dynamicInstancesCalcuationMode) {
      mat4.identity(this._mtxModelView);

      mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
      mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);

      mat4.multiply(this._mtxModelView, transformation, this._mtxModelView);

      return;
    }

    this.setDefultProjectionMatrix();

    /*
    * Model-View matrix
    */
    mat4.identity(this._mtxModelView);
    mat4.translate(this._mtxModelView, this._eyeVector);
    mat4.rotate(this._mtxModelView, this._rotateX * Math.PI / 180, [1, 0, 0]);
    mat4.rotate(this._mtxModelView, this._rotateY * Math.PI / 180, [0, 0, 1]);

    /*
    * Fit the image
    */

    // [0.0 -> X/Y/Zmin + X/Y/Zmax]
    mat4.translate(this._mtxModelView,
      [
        -this._worldDimensions.Xmin,
        -this._worldDimensions.Ymin,
        -this._worldDimensions.Zmin
      ]);

    // Center
    mat4.translate(this._mtxModelView,
      [
        -(this._worldDimensions.Xmax - this._worldDimensions.Xmin) / 2,
        -(this._worldDimensions.Ymax - this._worldDimensions.Ymin) / 2,
        -(this._worldDimensions.Zmax - this._worldDimensions.Zmin) / 2
      ]);

    mat4.multiply(this._mtxModelView, transformation, this._mtxModelView);

    gl.uniformMatrix4fv(this._shaderProgram.ModelViewMatrix, false, this._mtxModelView);

    /*
    * Normals matrix
    */
    gl.uniformMatrix3fv(this._shaderProgram.NormalMatrix, false, mat4.toMat3(this._mtxModelView));
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlDrawStaticContent = function (instance, mat) {
    if (this._dynamicObjectsSelectionMode) {
      this.owlDrawFacesSelectionFramebuffer(instance, mat);
      return;
    }

    if (this._dynamicInstancesCalcuationMode) {
      var dynamicInstance = null;
      for (var d = 0; d < g_dynamicInstances.length; d++) {
        if (g_dynamicInstances[d].instance == instance) {
          dynamicInstance = g_dynamicInstances[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstance == null) {
        console.error("Instance '" + instance + "' doesn't exist.");
        return;
      }

      dynamicInstance.transformationsCount++;

      this.owlTransform(mat);

      var vertexMin = [dynamicInstance.Xmin, dynamicInstance.Ymin, dynamicInstance.Zmin, 1.0];
      var transformedVertexMin = [0, 0, 0, 0];

      mat4.multiplyVec4(this._mtxModelView, vertexMin, transformedVertexMin);

      if (this._worldDimensions.Xmin != undefined) {
        this._worldDimensions.Xmin = Math.min(this._worldDimensions.Xmin, transformedVertexMin[0]);
      }
      else {
        this._worldDimensions.Xmin = transformedVertexMin[0];
      }

      if (this._worldDimensions.Ymin != undefined) {
        this._worldDimensions.Ymin = Math.min(this._worldDimensions.Ymin, transformedVertexMin[1]);
      }
      else {
        this._worldDimensions.Ymin = transformedVertexMin[1];
      }

      if (this._worldDimensions.Zmin != undefined) {
        this._worldDimensions.Zmin = Math.min(this._worldDimensions.Zmin, transformedVertexMin[2]);
      }
      else {
        this._worldDimensions.Zmin = transformedVertexMin[2];
      }

      if (this._worldDimensions.Xmax != undefined) {
        this._worldDimensions.Xmax = Math.max(this._worldDimensions.Xmax, transformedVertexMin[0]);
      }
      else {
        this._worldDimensions.Xmax = transformedVertexMin[0];
      }

      if (this._worldDimensions.Ymax != undefined) {
        this._worldDimensions.Ymax = Math.max(this._worldDimensions.Ymax, transformedVertexMin[1]);
      }
      else {
        this._worldDimensions.Ymax = transformedVertexMin[1];
      }

      if (this._worldDimensions.Zmax != undefined) {
        this._worldDimensions.Zmax = Math.max(this._worldDimensions.Zmax, transformedVertexMin[2]);
      }
      else {
        this._worldDimensions.Zmax = transformedVertexMin[2];
      }

      var vertexMax = [dynamicInstance.Xmax, dynamicInstance.Ymax, dynamicInstance.Zmax, 1.0];
      var transformedVertexMax = [0, 0, 0, 0];

      mat4.multiplyVec4(this._mtxModelView, vertexMax, transformedVertexMax);

      this._worldDimensions.Xmin = Math.min(this._worldDimensions.Xmin, transformedVertexMax[0]);
      this._worldDimensions.Ymin = Math.min(this._worldDimensions.Ymin, transformedVertexMax[1]);
      this._worldDimensions.Zmin = Math.min(this._worldDimensions.Zmin, transformedVertexMax[2]);

      this._worldDimensions.Xmax = Math.max(this._worldDimensions.Xmax, transformedVertexMax[0]);
      this._worldDimensions.Ymax = Math.max(this._worldDimensions.Ymax, transformedVertexMax[1]);
      this._worldDimensions.Zmax = Math.max(this._worldDimensions.Zmax, transformedVertexMax[2]);

      return;
    } // if (this._dynamicInstancesCalcuationMode)

    this.owlDrawFaces(instance, mat, true);
    this.owlDrawFaces(instance, mat, false);
    this.owlDrawSelectedObject(instance, mat);
    this.owlDrawWireframes(instance, mat);
    this.owlDrawPoints(instance, mat);
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlDrawFaces = function (instance, mat, opaqueObjects) {
    if (g_dynamicInstancesData.length == 0) {
      return;
    }

    if (!opaqueObjects) {
      gl.enable(gl.BLEND);
      gl.blendEquation(gl.FUNC_ADD);
      gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
    }

    try {
      var dynamicInstance = null;
      for (var d = 0; d < g_dynamicInstances.length; d++) {
        if (g_dynamicInstances[d].instance == instance) {
          dynamicInstance = g_dynamicInstances[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstance == null) {
        return;
      }

      var dynamicInstanceData = null;
      for (var d = 0; d < g_dynamicInstancesData.length; d++) {
        if (g_dynamicInstancesData[d].instance == instance) {
          dynamicInstanceData = g_dynamicInstancesData[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstanceData == null) {
        return;
      }

      this.owlTransform(mat);

      for (var f = 0; f < dynamicInstanceData.facesCohorts.length; f++) {
        //if (areAllStaticObjectsHidden(g_dynamicInstances[i].objectsCount, transformation)) {
        //    continue;
        //}

        var facesCohort = dynamicInstanceData.facesCohorts[f];

        if (opaqueObjects) {
          if (facesCohort.transparency < 1.0) {
            continue;
          }
        }
        else {
          if (facesCohort.transparency == 1.0) {
            continue;
          }
        }

        gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.vertices);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        if ((facesCohort.texture != null) && (facesCohort.textureVertices.length > 0)) {
          gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);

          gl.uniform1f(this._shaderProgram.EnableTexture, 1.0);

          gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.textureVertices);
          gl.vertexAttribPointer(this._shaderProgram.UV, 2, gl.FLOAT, false, 0, 0);
          gl.enableVertexAttribArray(this._shaderProgram.UV);

          gl.activeTexture(gl.TEXTURE0);
          gl.bindTexture(gl.TEXTURE_2D, facesCohort.texture);
          gl.uniform1i(this._shaderProgram.Sampler, 0);
        }
        else {
          gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.normals);
          gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, false, 0, 0);
          gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);

          gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
          gl.disableVertexAttribArray(this._shaderProgram.UV);
        }

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, facesCohort.indices);

        var singleColorValue = false;
        if ((facesCohort.diffuse.length == 3) && (facesCohort.specular.length == 3) && (facesCohort.emissive.length == 3)) {
          singleColorValue = true;

          gl.uniform3f(this._shaderProgram.SpecularMaterial, facesCohort.specular[0], facesCohort.specular[1], facesCohort.specular[2]);
          gl.uniform3f(this._shaderProgram.DiffuseMaterial, facesCohort.diffuse[0], facesCohort.diffuse[1], facesCohort.diffuse[2]);
          // #todo
          //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, facesCohort.emissive[0], facesCohort.emissive[1], facesCohort.emissive[2]);

          if (true/*areAllDynamicObjectsVisible(g_faceObjects[i])*/) {
            gl.uniform3f(this._shaderProgram.AmbientMaterial, facesCohort.ambient[0], facesCohort.ambient[1], facesCohort.ambient[2]);
            gl.uniform1f(this._shaderProgram.Transparency, facesCohort.transparency);

            gl.drawElements(gl.TRIANGLES, facesCohort.indices.length, gl.UNSIGNED_SHORT, 0);

            continue;
          }
        } // if ((facesCohort.diffuse.length == 3) && ...

        var offset = 0;
        for (var j = 0; j < facesCohort.lengths.length; j++) {
          if (!singleColorValue) {
            if (facesCohort.diffuse.length == 3) {
              gl.uniform3f(this._shaderProgram.DiffuseMaterial, facesCohort.diffuse[0], facesCohort.diffuse[1], facesCohort.diffuse[2]);
            }
            else {
              gl.uniform3f(this._shaderProgram.DiffuseMaterial, facesCohort.diffuse[j * 3], facesCohort.diffuse[(j * 3) + 1], facesCohort.diffuse[(j * 3) + 2]);
            }

            if (facesCohort.specular.length == 3) {
              gl.uniform3f(this._shaderProgram.SpecularMaterial, facesCohort.specular[0], facesCohort.specular[1], facesCohort.specular[2]);
            }
            else {
              gl.uniform3f(this._shaderProgram.SpecularMaterial, facesCohort.specular[j * 3], facesCohort.specular[(j * 3) + 1], facesCohort.specular[(j * 3) + 2]);
            }

            // #todo
            //if (facesCohort.emissive.length == 3) {
            //  gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, facesCohort.emissive[0], facesCohort.emissive[1], facesCohort.emissive[2]);
            //}
            //else {
            //  gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, facesCohort.emissive[j * 3], facesCohort.emissive[(j * 3) + 1], facesCohort.emissive[(j * 3) + 2]);
            //}
          } // if (!singleColorValue)

          //if (g_visibleDynamicObjects[facesCohort.objects[j]]) {
          gl.uniform3f(this._shaderProgram.AmbientMaterial, facesCohort.ambient[0], facesCohort.ambient[1], facesCohort.ambient[2]);
          gl.uniform1f(this._shaderProgram.Transparency, facesCohort.transparency);

          gl.drawElements(gl.TRIANGLES, facesCohort.lengths[j], gl.UNSIGNED_SHORT, offset * 2);
          //}

          offset += facesCohort.lengths[j];
        } // for (var j = ...
      } // for (var f = ...
    }
    catch (ex) {
      console.error(ex);
    }

    if (!opaqueObjects) {
      gl.disable(gl.BLEND);
    }
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlDrawWireframes = function (instance, mat) {
    if (g_dynamicInstances.length == 0) {
      return;
    }

    gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);

    gl.uniform3f(this._shaderProgram.AmbientMaterial, 0.0, 0.0, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);
    gl.uniform3f(this._shaderProgram.DiffuseMaterial, 0.0, 0.0, 0.0);    
    gl.uniform3f(this._shaderProgram.SpecularMaterial, 0.0, 0.0, 0.0);    
    // #todo
    //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, 0.0, 0.0, 0.0);

    try {
      var dynamicInstanceData = null;
      for (var d = 0; d < g_dynamicInstancesData.length; d++) {
        if (g_dynamicInstancesData[d].instance == instance) {
          dynamicInstanceData = g_dynamicInstancesData[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstanceData == null) {
        return;
      }

      if (dynamicInstanceData.wireframesCohorts.length == 0) {
        return;
      }

      this.owlTransform(mat);

      for (var w = 0; w < dynamicInstanceData.wireframesCohorts.length; w++) {
        //if (areAllStaticObjectsHidden(g_staticInstances[i].objectsCount, transformation)) {
        //    continue;
        //}

        var wirframesCohort = dynamicInstanceData.wireframesCohorts[w];

        gl.bindBuffer(gl.ARRAY_BUFFER, wirframesCohort.vertices);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, wirframesCohort.indices);

        //if (areAllStaticObjectsVisible(g_staticInstances[i].objectsCount, transformation)) {
        gl.drawElements(
          gl.LINES,
          wirframesCohort.indices.length,
          gl.UNSIGNED_SHORT,
          0);

        continue;
        //}

        var offset = 0;
        for (var j = 0; j < wirframesCohort.lengths.length; j++) {
          // ???
          if (transformation.owlTree[wirframesCohort.objects[j]].owlState == CHECKED_STATE) {
            gl.drawElements(
              gl.LINES,
              wirframesCohort.lengths[j],
              gl.UNSIGNED_SHORT,
              offset * 2);
          } // for (var j = ...

          offset += wirframesCohort.lengths[j];
        } // for (var j = ...
      } // for (var w = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlDrawPoints = function (instance, mat) {
    if (g_dynamicInstances.length == 0) {
      return;
    }

    gl.disableVertexAttribArray(this._shaderProgram.VertexNormal);
    gl.disableVertexAttribArray(this._shaderProgram.UV);

    gl.uniform1f(this._shaderProgram.EnableLighting, 0.0);
    gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);

    gl.uniform3f(this._shaderProgram.AmbientMaterial, 0.0, 0.0, 0.0);
    gl.uniform1f(this._shaderProgram.Transparency, 1.0);
    gl.uniform3f(this._shaderProgram.DiffuseMaterial, 0.0, 0.0, 0.0);
    gl.uniform3f(this._shaderProgram.SpecularMaterial, 0.0, 0.0, 0.0);
    // #todo
    //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, 0.0, 0.0, 0.0);

    try {
      var dynamicInstanceData = null;
      for (var d = 0; d < g_dynamicInstancesData.length; d++) {
        if (g_dynamicInstancesData[d].instance == instance) {
          dynamicInstanceData = g_dynamicInstancesData[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstanceData == null) {
        return;
      }

      if (dynamicInstanceData.pointsCohorts.length == 0) {
        return;
      }

      this.owlTransform(mat);

      for (var w = 0; w < dynamicInstanceData.pointsCohorts.length; w++) {
        //if (areAllStaticObjectsHidden(g_staticInstances[i].objectsCount, transformation)) {
        //    continue;
        //}

        var pointsCohort = dynamicInstanceData.pointsCohorts[w];

        gl.bindBuffer(gl.ARRAY_BUFFER, pointsCohort.vertices);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, pointsCohort.indices);

        //if (areAllStaticObjectsVisible(g_staticInstances[i].objectsCount, transformation)) {
        gl.drawElements(
          gl.POINTS,
          pointsCohort.indices.length,
          gl.UNSIGNED_SHORT,
          0);

        continue;
        //}

        var offset = 0;
        for (var j = 0; j < pointsCohort.lengths.length; j++) {
          // ???
          if (transformation.owlTree[pointsCohort.objects[j]].owlState == CHECKED_STATE) {
            gl.drawElements(
              gl.POINTS,
              pointsCohort.lengths[j],
              gl.UNSIGNED_SHORT,
              offset * 2);
          } // for (var j = ...

          offset += pointsCohort.lengths[j];
        } // for (var j = ...
      } // for (var w = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlDrawSelectedObject = function (instance, mat) {
    if (g_dynamicInstancesData.length == 0) {
      return;
    }

    try {
      var dynamicInstance = null;
      for (var d = 0; d < g_dynamicInstances.length; d++) {
        if (g_dynamicInstances[d].instance == instance) {
          dynamicInstance = g_dynamicInstances[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstance == null) {
        return;
      }

      var dynamicInstanceData = null;
      for (var d = 0; d < g_dynamicInstancesData.length; d++) {
        if (g_dynamicInstancesData[d].instance == instance) {
          dynamicInstanceData = g_dynamicInstancesData[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstanceData == null) {
        return;
      }

      var selectedDynamicObject = null;
      if ((this._selectedDynamicObject != null) && (this._selectedDynamicObject.instance == instance) &&
        (this._selectedDynamicObject.transformation == dynamicInstance.transformationIndex)) {
        selectedDynamicObject = this._selectedDynamicObject;
      }

      dynamicInstance.transformationIndex++;

      if (selectedDynamicObject == null) {
        return;
      }

      this.owlTransform(mat);

      for (var f = 0; f < dynamicInstanceData.facesCohorts.length; f++) {
        //if (areAllStaticObjectsHidden(g_dynamicInstances[i].objectsCount, transformation)) {
        //    continue;
        //}

        var facesCohort = dynamicInstanceData.facesCohorts[f];

        gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.vertices);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.normals);
        gl.vertexAttribPointer(this._shaderProgram.VertexNormal, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexNormal);

        gl.uniform1f(this._shaderProgram.EnableTexture, 0.0);
        gl.disableVertexAttribArray(this._shaderProgram.UV);

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, facesCohort.indices);

        var offset = 0;
        for (var j = 0; j < facesCohort.lengths.length; j++) {
          if (selectedDynamicObject.object == facesCohort.objects[j]) {
            gl.uniform3f(this._shaderProgram.AmbientMaterial, 1.0 - facesCohort.ambient[0], 1.0 - facesCohort.ambient[1], 1.0 - facesCohort.ambient[2]);
            gl.uniform1f(this._shaderProgram.Transparency, 1.0 - facesCohort.transparency);

            gl.uniform3f(this._shaderProgram.SpecularMaterial, facesCohort.specular[0], facesCohort.specular[1], facesCohort.specular[2]);
            gl.uniform3f(this._shaderProgram.DiffuseMaterial, facesCohort.diffuse[0], facesCohort.diffuse[1], facesCohort.diffuse[2]);
            // #todo
            //gl.uniform3f(this._shaderProgram.uMaterialEmissiveColor, facesCohort.emissive[0], facesCohort.emissive[1], facesCohort.emissive[2]);

            gl.drawElements(gl.TRIANGLES, facesCohort.lengths[j], gl.UNSIGNED_SHORT, offset * 2);
          }

          offset += facesCohort.lengths[j];
        } // for (var j = ...
      } // for (var f = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * OWL support
  */
  Viewer.prototype.goToPreviousPart = function () {
    g_owlActivePart--;

    if (g_owlActivePart < 0) {
      g_owlActivePart = g_owlParts.length - 1;
    }

    this._dynamicInstancesCalcuationMode = true;
    eval(g_owlParts[g_owlActivePart]);
    this._dynamicInstancesCalcuationMode = false;

    this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
    this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
    this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

    this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
    this._eyeVector = vec3.create(this._defaultEyeVector);

    console.info(this._worldDimensions);

    this._dynamicObjectsSelectionColors = [];
  }

  /**
  * OWL support
  */
  Viewer.prototype.goToNextPart = function () {
    g_owlActivePart++;

    if (g_owlActivePart >= g_owlParts.length) {
      g_owlActivePart = 0;
    }

    this._dynamicInstancesCalcuationMode = true;
    eval(g_owlParts[g_owlActivePart]);
    this._dynamicInstancesCalcuationMode = false;

    this._worldDimensions.MaxDistance = this._worldDimensions.Xmax - this._worldDimensions.Xmin;
    this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Ymax - this._worldDimensions.Ymin);
    this._worldDimensions.MaxDistance = Math.max(this._worldDimensions.MaxDistance, this._worldDimensions.Zmax - this._worldDimensions.Zmin);

    this._defaultEyeVector[2] = -(2 * this._worldDimensions.MaxDistance);
    this._eyeVector = vec3.create(this._defaultEyeVector);

    console.info(this._worldDimensions);

    this._dynamicObjectsSelectionColors = [];
  }

  /**
  * OWL support
  */
  Viewer.prototype.owlDrawFacesSelectionFramebuffer = function (instance, mat) {
    if (g_dynamicInstancesData.length == 0) {
      return;
    }

    try {
      var dynamicInstanceIndex = -1;
      for (var d = 0; d < g_dynamicInstances.length; d++) {
        if (g_dynamicInstances[d].instance == instance) {
          dynamicInstanceIndex = d;
          break;
        }
      } // for (var d = ...

      if (dynamicInstanceIndex == -1) {
        return;
      }

      var dynamicInstanceData = null;
      for (var d = 0; d < g_dynamicInstancesData.length; d++) {
        if (g_dynamicInstancesData[d].instance == instance) {
          dynamicInstanceData = g_dynamicInstancesData[d];
          break;
        }
      } // for (var d = ...

      if (dynamicInstanceData == null) {
        return;
      }

      this.owlTransform(mat);

      var transformationSelectionColors = this._dynamicObjectsSelectionColors[dynamicInstanceIndex][g_dynamicInstances[dynamicInstanceIndex].transformationIndex];
      g_dynamicInstances[dynamicInstanceIndex].transformationIndex++;

      for (var f = 0; f < dynamicInstanceData.facesCohorts.length; f++) {
        var facesCohort = dynamicInstanceData.facesCohorts[f];

        gl.bindBuffer(gl.ARRAY_BUFFER, facesCohort.vertices);
        gl.vertexAttribPointer(this._shaderProgram.VertexPosition, 3, gl.FLOAT, false, 0, 0);
        gl.enableVertexAttribArray(this._shaderProgram.VertexPosition);

        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, facesCohort.indices);

        var offset = 0;
        for (var j = 0; j < facesCohort.lengths.length; j++) {
          gl.uniform3f(this._shaderProgram.AmbientMaterial,
            transformationSelectionColors[facesCohort.objects[j]][0],
            transformationSelectionColors[facesCohort.objects[j]][1],
            transformationSelectionColors[facesCohort.objects[j]][2]);

          gl.drawElements(gl.TRIANGLES, facesCohort.lengths[j], gl.UNSIGNED_SHORT, offset * 2);

          offset += facesCohort.lengths[j];
        } // for (var j = ...
      } // for (var f = ...
    }
    catch (ex) {
      console.error(ex);
    }
  }

  /**
  * Draws the conceptual faces of the dynamic instances in a frame buffer
  */
  Viewer.prototype.drawDynamicModelsSelectionFrameBuffer = function () {
    if (g_dynamicInstances.length == 0) {
      return;
    }

    try {
      /*
      * Encoding the selection colors
      */
      if (this._dynamicObjectsSelectionColors.length == 0) {
        this._dynamicObjectsBaseIndex = this._objectsBaseIndex;

        for (var i = 0; i < g_dynamicInstances.length; i++) {
          var dynamicInstance = g_dynamicInstances[i];

          dynamicInstance.objectsBaseIndex = this._dynamicObjectsBaseIndex;

          var dynamicInstanceSelectionColors = [];
          for (var t = 0; t < dynamicInstance.transformationsCount; t++) {
            var transformationSelectionColors = [];

            var step = 1.0 / 255.0;
            for (var iObject = 0; iObject < g_dynamicInstances[i].objectsCount; iObject++) {
              // build selection color
              var R = Math.floor((this._dynamicObjectsBaseIndex + iObject) / (255 * 255));
              if (R >= 1.0) {
                R *= step;
              }

              var G = Math.floor((this._dynamicObjectsBaseIndex + iObject) / 255);
              if (G >= 1.0) {
                G *= step;
              }

              var B = Math.floor((this._dynamicObjectsBaseIndex + iObject) % 255);
              B *= step;

              transformationSelectionColors.push([R, G, B]);
            } // for (var iObject = ...

            dynamicInstanceSelectionColors.push(transformationSelectionColors);

            this._dynamicObjectsBaseIndex += g_dynamicInstances[i].objectsCount;
          } // for (var t =

          this._dynamicObjectsSelectionColors.push(dynamicInstanceSelectionColors);
        } // for (var i = ...
      } // if (this._dynamicObjectsSelectionColors.length == ...

      /*
      * Reset the transformation index
      */
      for (var i = 0; i < g_dynamicInstances.length; i++) {
        g_dynamicInstances[i].transformationIndex = 0;
      }

      /*
      * Draw OWL content in selection mode
      */
      this._dynamicObjectsSelectionMode = true;
      eval(g_owlParts[g_owlActivePart]);
      this._dynamicObjectsSelectionMode = false;
    }
    catch (ex) {
      console.error(ex);
    }
  };

  Viewer.prototype.busyIndicator = function (show) {
    if (show) {
      var target = document.getElementById('busy-indicator');

      if (this._busyIndicator === null) {
        this._busyIndicator = new Spinner(this._spinnerOptions).spin(target);
      }
      else {
        this._busyIndicator.spin(target);
      }
    }
    else {
      this._busyIndicator.stop();
    }
  };
};

// TODO
/**
* Whether all dynamic objects are visible
*/
//function areAllDynamicObjectsVisible(objects) {
//    for (var i = 0; i < objects.length; i++) {
//        if (!g_visibleDynamicObjects[objects[i]]) {
//            return false;
//        }
//    }

//    return true;
//}

// TODO
/**
* Whether all dynamic objects are hidden
*/
//function areAllDynamicObjectsHidden(objects) {
//    for (var i = 0; i < objects.length; i++) {
//        if (g_visibleDynamicObjects[objects[i]]) {
//            return false;
//        }
//    }

//    return true;
//}

/**
* Viewer
*/
var g_viewer = new Viewer();

/**
* Render
*/
function renderLoop() {
  utils.requestAnimFrame(renderLoop);

  g_viewer.drawScene();

  if (typeof (onViewerRender) === typeof (Function)) {
    onViewerRender();
  }
}

/**
* Event handler
*/
$(window).resize(function () {
  if (typeof (onWindowResize) === typeof (Function)) {
    onWindowResize();
  }
});

/**
* OWL
*/
function _owlDrawStaticContent(instance, mat) {
  g_viewer.owlDrawStaticContent(instance, mat);
}
