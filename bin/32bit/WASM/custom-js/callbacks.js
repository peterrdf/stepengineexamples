﻿var Module = {
  onRuntimeInitialized: function () {
    console.log('onRuntimeInitialized')
  },
}

var g_fileName = null;

function jsLogCallback(event) {
  document.getElementById("txtLog").value += event;
  document.getElementById("txtLog").value += '\n';
}

function getFileExtension(file) {
  if (file && file.length > 4) {
    return file.split('.').pop();
  }

  return null
}

function addContent(fileName, fileExtension, fileContent) {
  console.log('addContent BEGIN: ' + fileName)

  jsLogCallback('Loading ' + fileName + '...');

  Module.unload()
  Module['FS_createDataFile']('/data/', 'input.ifc', fileContent, true, true)

  if ((fileExtension == 'gml') ||
    (fileExtension == 'citygml') ||
    (fileExtension == 'xml') ||
    (fileExtension == 'json')) {
    let transformationsCount = Module.gml2ifc_retrieveSRSData(fileName)
    if (transformationsCount == 0) {
      // Execute
      Module.gml2ifc(fileName);

      FS.unlink('/data/' + 'input.ifc')

      // Download
      const output = Module.FS.readFile('/data/output.ifc', { encoding: 'utf8' })
      const blob = new Blob([output], { type: 'text/plain' })
      const a = document.createElement('a')
      a.setAttribute('download', fileName + ".ifc")
      a.setAttribute('href', window.URL.createObjectURL(blob))
      a.click();
      a.remove()
    }
  }
  else {
    alert('Not supported.');
  }

  console.log('addContent END: ' + fileName)
}

function loadContent(fileName, fileExtension, fileContent) {
  g_fileName = fileName
  document.getElementById("txtLog").value = ''
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


// ***** Map Tiler API ***** //

maptilerClient.config.apiKey = "IzXwuNELmi1wQXfKZZOQ";

/* 
Example:
  GML_Parcelas.gml

  // Map Tiler Server
  const result = maptilerClient.coordinates.transform(
    [431962.77, 4812381.63, 0.00],
    { sourceCrs: 25830, targetCrs: 4326, operations: "1623" });
  console.log(result);
*/

var g_crsTransformations = {};
var g_pendingCRSTransformations = 0;
function jsGetWGS84Callback(CRS, x, y, z) {
  const key = CRS + '#' + x + '#' + y + '#' + z;
  if (!g_crsTransformations.hasOwnProperty(key)) {
    return stringToNewUTF8(""); 
  }

  let wgs84Data = g_crsTransformations[key];

  let coordinates = "";
  coordinates = wgs84Data.y;
  coordinates += ' ';
  coordinates += wgs84Data.x;
  coordinates += ' ';
  coordinates += wgs84Data.z;

  return stringToNewUTF8(coordinates);

  // Note: does not work in callbacks
  //g_ptr = null;
  //stringToUTF8(coordinates, g_ptr, lengthBytesUTF8(coordinates) + 1);
  //return g_ptr;
}

function jsToWGS84AsyncCallback(CRS, x, y, z) {
  const key = CRS + '#' + x + '#' + y + '#' + z;
  if (g_crsTransformations.hasOwnProperty(key)) {
    return;
  }

  g_pendingCRSTransformations = g_pendingCRSTransformations + 1;

  const promise = maptilerClient.coordinates.transform(
    [x, y, z], {
    sourceCrs: CRS, targetCrs: 4326/*WGS84*/, operations: "1623"
  });

  promise.then(response => {
    g_crsTransformations[key] = { "x": response.results[0].x, "y": response.results[0].y, "z": response.results[0].z };

    g_pendingCRSTransformations = g_pendingCRSTransformations - 1;
    if (g_pendingCRSTransformations == 0) {
      // Execute
      Module.gml2ifc(g_fileName);

      FS.unlink('/data/' + 'input.ifc')

      // Download
      const output = Module.FS.readFile('/data/output.ifc', { encoding: 'utf8' })
      const blob = new Blob([output], { type: 'text/plain' })
      const a = document.createElement('a')
      a.setAttribute('download', g_fileName + ".ifc")
      a.setAttribute('href', window.URL.createObjectURL(blob))
      a.click();
      a.remove()
    }
  }).catch(error => {
    console.error(error);
    alert(error);
  });
}


/* 
Example 2:
  GML_Parcelas.gml

  // Map Tiler Server and JaveScript
  const result = await maptilerClient.coordinates.search('25830');
  console.log(result);

  // result => UTM Time Zone => 30N
  console.log(utmToLatLon(431962.77, 4812381.63, 30, true));
*/
// https://gis.stackexchange.com/questions/147425/formula-to-convert-from-wgs-84-utm-zone-34n-to-wgs-84
// Constants.
// Symbols as used in USGS PP 1395: Map Projections - A Working Manual
const DatumEqRad = [6378137.0,
  6378137.0,
  6378137.0,
  6378135.0,
  6378160.0,
  6378245.0,
  6378206.4,
  6378388.0,
  6378388.0,
  6378249.1,
  6378206.4,
  6377563.4,
  6377397.2,
  6377276.3];
const DatumFlat = [298.2572236,
  298.2572236,
  298.2572215,
  298.2597208,
  298.2497323,
  298.2997381,
  294.9786982,
  296.9993621,
  296.9993621,
  293.4660167,
  294.9786982,
  299.3247788,
  299.1527052,
  300.8021499];

const Item = 0;                    // default
const a = DatumEqRad[Item];     // equatorial radius (meters)
const f = 1 / DatumFlat[Item];  // polar flattening
const drad = Math.PI / 180;        // convert degrees to radians

// Mor constants, extracted from the function:
const k0 = 0.9996;                      // scale on central meridian
const b = a * (1 - f);                   // polar axis
const e = Math.sqrt(1 - (b / a) * (b / a));  // eccentricity
const e0 = e / Math.sqrt(1 - e * e);      // called e' in reference
const esq = (1 - (b / a) * (b / a));         // e² for use in expansions
const e0sq = e * e / (1 - e * e);             // e0² — always even powers

function utmToLatLon(x, y, utmz, north) {

  // First some validation:
  if (x < 160000 || x > 840000) {
    alert("Outside permissible range of easting values.");
    return;
  }
  if (y < 0) {
    alert("Negative values are not allowed for northing.");
    return;
  }
  if (y > 10000000) {
    alert("Northing may not exceed 10,000,000.");
    return;
  }

  // Now the actual calculation:
  const zcm = 3 + 6 * (utmz - 1) - 180;  // central meridian of zone
  const e1 = (1 - Math.sqrt(1 - e * e)) / (1 + Math.sqrt(1 - e * e));  // called e₁ in USGS PP 1395
  const M0 = 0;  // in case origin other than zero lat - not needed for standard UTM

  let M;  // arc length along standard meridian
  if (north) {
    M = M0 + y / k0;
  } else {  // southern hemisphere
    M = M0 + (y - 10000000) / k;
  }
  const mu = M / (a * (1 - esq * (1 / 4 + esq * (3 / 64 + 5 * esq / 256))));
  let phi1 = mu + e1 * (3 / 2 - 27 * e1 * e1 / 32) * Math.sin(2 * mu) + e1 * e1 * (21 / 16 - 55 * e1 * e1 / 32) * Math.sin(4 * mu);  // footprint Latitude
  phi1 = phi1 + e1 * e1 * e1 * (Math.sin(6 * mu) * 151 / 96 + e1 * Math.sin(8 * mu) * 1097 / 512);
  const C1 = e0sq * Math.pow(Math.cos(phi1), 2);
  const T1 = Math.pow(Math.tan(phi1), 2);
  const N1 = a / Math.sqrt(1 - Math.pow(e * Math.sin(phi1), 2));
  const R1 = N1 * (1 - e * e) / (1 - Math.pow(e * Math.sin(phi1), 2));
  const D = (x - 500000) / (N1 * k0);
  let phi = (D * D) * (1 / 2 - D * D * (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * e0sq) / 24);
  phi = phi + Math.pow(D, 6) * (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 252 * e0sq - 3 * C1 * C1) / 720;
  phi = phi1 - (N1 * Math.tan(phi1) / R1) * phi;

  // Output Latitude:
  const outLat = Math.floor(1000000 * phi / drad) / 1000000;

  const lng = D * (1 + D * D * ((-1 - 2 * T1 - C1) / 6 + D * D * (5 - 2 * C1 + 28 * T1 - 3 * C1 * C1 + 8 * e0sq + 24 * T1 * T1) / 120)) / Math.cos(phi1);
  const lngd = zcm + lng / drad;

  // Output Longitude:
  const outLon = Math.floor(1000000 * lngd) / 1000000;

  return [outLat, outLon];
}

// ***** Map Tiler API ***** //