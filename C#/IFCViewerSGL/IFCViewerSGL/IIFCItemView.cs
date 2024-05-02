using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFCViewerSGL
{

    /// <summary>
    /// Visual presentation of IFCItem
    /// </summary>
    public interface IIFCItemView
    {
        /// <summary>
        /// Accessor
        /// </summary>
        IFCItem Item
        {
            get;
            set;
        }
    }
}
