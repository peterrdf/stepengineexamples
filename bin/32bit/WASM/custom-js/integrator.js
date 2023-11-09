// Third-part tools

g_getObjectDescription = function (object) {
  if (object &&
    (object.label !== undefined)
    && (object.label !== null)
    && (object.label !== '')) {
    return object.label;
  }

  return null;
}

if (embeddedMode()) {
  hideUI();

  window.onmessage = function (e) {
    try {
      let event = null;

      try {
        event = JSON.parse(e.data);
      }
      catch {

      }

      if (event) {
        switch (event.type) {
          case 'loadFile': {
            loadFileByUri(event.file);
          }
            break;
        }
      }
    }
    catch (ex) {
      console.error(ex);
    }
  };
}