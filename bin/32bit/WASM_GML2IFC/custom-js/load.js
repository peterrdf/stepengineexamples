var Module = {
  onRuntimeInitialized: function () {
    console.log('onRuntimeInitialized')
  },
}

function jsLogCallback(event) {
  document.getElementById("ta_log").value += event;
}

function getFileExtension(file) {
  if (file && file.length > 4) {
    return file.split('.').pop();
  }

  return null
}

function addContent(fileName, fileExtension, fileContent) {
  console.log('addContent BEGIN: ' + fileName)

  Module.unload()
  Module['FS_createDataFile']('/data/', 'input.ifc', fileContent, true, true)

  if ((fileExtension == 'gml') ||
    (fileExtension == 'citygml') ||
    (fileExtension == 'xml') ||
    (fileExtension == 'json')) {
    Module.GIS2IFC(fileName)
    const output = Module.FS.readFile('/data/output.ifc', { encoding: 'utf8' })
    document.getElementById("ta_log").value += output;

    const blob = new Blob([output], { type: 'text/plain' })
    const a = document.createElement('a')
    a.setAttribute('download', fileName + ".ifc")
    a.setAttribute('href', window.URL.createObjectURL(blob))
    a.click();
    a.remove()
  }
  else {
    alert('Not supported.');
  }

  FS.unlink('/data/' + 'input.ifc')  

  console.log('addContent END: ' + fileName)
}

function loadContent(fileName, fileExtension, fileContent) {
  addContent(fileName, fileExtension, fileContent)
}

function loadFile(file) {
  var fileReader = new FileReader()
  fileReader.onload = function () {
    var fileContent = new Uint8Array(fileReader.result)

    var fileExtension = getFileExtension(file.name)
    loadContent(file.name, fileExtension, fileContent)
  }

  fileReader.readAsArrayBuffer(file)
}

// Emscripten/Docker
function readFileFileSystem(file, callback) {
  var rawFile = new XMLHttpRequest();
  rawFile.open("GET", file);
  rawFile.setRequestHeader("Content-Type", "text/xml");
  rawFile.setRequestHeader("X-Requested-With", "XMLHttpRequest");
  rawFile.setRequestHeader("Access-Control-Allow-Origin", "*");
  rawFile.onreadystatechange = function () {
    if (rawFile.readyState === 4 && rawFile.status === 200) {
      callback(rawFile.responseText);
    }
  }
  rawFile.send();
}

// Emscripten/Docker
function loadFileByPath(file) {
  readFileFileSystem(`${file}`, function (fileContent) {
    try {
      var fileExtension = getFileExtension(file)
      loadContent(file.name, fileExtension, fileContent)
    }
    catch (e) {
      console.error(e);
    }
  });
}

function readFileByUri(file, callback) {
  try {
    var rawFile = new XMLHttpRequest() 
    rawFile.open('GET', "http://localhost:8088/fileservice/byUri?fileUri=" + encodeURIComponent(file))
    rawFile.setRequestHeader("Content-type", "application/json; charset=utf-8")
    rawFile.onreadystatechange = function () {
      if (rawFile.readyState === 4 && rawFile.status === 200) {
        callback(rawFile.responseText)
      }
    }
    rawFile.send()
  }
  catch (ex) {
    console.error(ex)
  }
}

function loadFileByUri(file) {
  let fileExtension = getFileExtension(file)
  readFileByUri(`${file}`, function (fileContent) {
    try {
      loadContent(file, fileExtension, fileContent)
    }
    catch (e) {
      console.error(e)
    }
  })
}
