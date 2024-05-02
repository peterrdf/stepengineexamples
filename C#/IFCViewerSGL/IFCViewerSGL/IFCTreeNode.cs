using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IFCViewerSGL
{
    /// <summary>
    /// Type
    /// </summary>
    enum IFCTreeNodeType
    {
        unknown = -1,
        item = 0,
        geometry = 1,
        decomposition = 2,
        contains = 3,
        properties = 4,
        property = 5,
    }

    /// <summary>
    /// TreeNode-based IFCItem presentation
    /// </summary>
    class IFCTreeNode : TreeNode, IIFCItemView
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="imageIndex"></param>
        /// <param name="selectedImageIndex"></param>
        public IFCTreeNode(string text, IFCTreeNodeType type)
            : base(text)
        {
            Type = type;
        }

        /// <summary>
        /// IIFCItemView
        /// </summary>
        public IFCItem Item
        {
            get;
            set;
        }

        /// <summary>
        /// Getter
        /// </summary>
        public IFCTreeNodeType Type
        {
            get;
            private set;
        }
    }
}
