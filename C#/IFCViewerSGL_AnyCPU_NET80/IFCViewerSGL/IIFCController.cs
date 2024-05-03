using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCViewerSGL
{
    public interface IIFCController
    {
        IFCModel Model
        {
            get;
        }

        void RegisterView(IIFCView ifcView);

        void UnRegisterView(IIFCView ifcView);

        void SelectItem(object sender, IFCItem ifcItem);
    }
}
