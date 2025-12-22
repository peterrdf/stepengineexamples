/**
* Parse URL parameter by name
*/
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

/**
* Support for gzip decoding - base64
*/
function gZipDecode(compressedText) {
    try {
        // Decode base64 (convert ASCII to binary)
        var strBinary = atob(compressedText);

        // Convert binary string to character-number array
        var charData = strBinary.split('').map(function (x) { return x.charCodeAt(0); });

        // Turn number array into byte-array
        var byteData = new Uint8Array(charData);

        // Decode
        var inflate = new Zlib.Gunzip(byteData);
        var data = inflate.decompress();

        // Convert gunzipped byte-array back to ASCII string
        return String.fromCharCode.apply(null, new Uint16Array(data));
    }
    catch (ex) {
        console.error(ex);
    }
}	