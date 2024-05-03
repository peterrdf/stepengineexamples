using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCViewerSGL
{
    public interface IIFCView
    {
        void SetController(IIFCController ifcController);

        void OnModelLoaded();

        void OnItemSelected(object sender, IFCItem ifcItem);
    }
}
