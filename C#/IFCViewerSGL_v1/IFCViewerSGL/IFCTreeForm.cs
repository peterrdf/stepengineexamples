﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

#if _WIN64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

namespace IFCViewerSGL
{
    public partial class IFCTreeForm : Form, IIFCView
    {
        #region Constants

        /// <summary>
        /// Zero-based indices of the images inside the image list.
        /// </summary>
        private const int IMAGE_CHECKED = 0;
        private const int IMAGE_MIDDLE = 1;
        private const int IMAGE_UNCHECKED = 2;
        private const int IMAGE_PROPERTY_SET = 3;
        private const int IMAGE_PROPERTY = 4;
        private const int IMAGE_NOT_REFERENCED = 5;

        /// <summary>
        /// Tree
        /// </summary>
        private const string GEOMETRY = "geometry";
        private const string PROPERTIES = "properties";
        private const string DECOMPOSITION = "decomposition";
        private const string CONTAINS = "contains";        
        private const string PROPERTIES_PENDING_LOAD = "***..........***";

        #endregion // Constants

        #region Fields

        /// <summary>
        /// Model
        /// </summary>
        IFCModel _ifcModel;

        /// <summary>
        /// Controller
        /// </summary>
        IIFCController _ifcController;

        /// <summary>
        /// Units
        /// </summary>
        Dictionary<string, IFCUnit> _dicUnits = new Dictionary<string, IFCUnit>();

        #endregion // Fields

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="model"></param>
        public IFCTreeForm()
        {
            InitializeComponent();
        }

        #region IIFCView

        /// <summary>
        /// IIFCView
        /// </summary>
        /// <param name="ifcController"></param>
        public void SetController(IIFCController ifcController)
        {
            System.Diagnostics.Debug.Assert(ifcController != null);

            _ifcController = ifcController;

            _ifcController.RegisterView(this);

            OnModelLoaded();
        }

        /// <summary>
        /// IIFCView
        /// </summary>
        /// <param name="sender"></param>
        public void OnModelLoaded()
        {
            System.Diagnostics.Debug.Assert(_ifcController.Model != null);

            _ifcModel = _ifcController.Model;

            _treeView.Nodes.Clear();

            if (string.IsNullOrEmpty(_ifcModel.IFCFile))
            {
                return;
            }

            LoadHeader();
            LoadProjects();
            LoadNotReferenced();
        }

        /// <summary>
        /// IIFCView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ifcItem"></param>
        public void OnItemSelected(object sender, IFCItem ifcItem)
        {
            if (sender == this)
            {
                return;
            }

            if ((ifcItem != null) && (ifcItem._view != null))
            {
                IFCTreeNode ifcTreeNode = ifcItem._view as IFCTreeNode;
                System.Diagnostics.Debug.Assert(ifcTreeNode != null);

                _treeView.SelectedNode = ifcTreeNode;
            }
            else
            {
                _treeView.SelectedNode = null;
            }
        }

        #endregion // IIFCView

        /// <summary>
        /// Loads the IFC header
        /// </summary>
        private void LoadHeader()
        {
            // Header info
            TreeNode tnHeaderInfo = _treeView.Nodes.Add("Header Info");
            tnHeaderInfo.ImageIndex = tnHeaderInfo.SelectedImageIndex = IMAGE_PROPERTY_SET;

            // Descriptions
            TreeNode tnDescriptions = tnHeaderInfo.Nodes.Add("Descriptions");
            tnDescriptions.ImageIndex = tnDescriptions.SelectedImageIndex = IMAGE_PROPERTY_SET;

            int i = 0;
            IntPtr description;
            while (IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 0, i++, IfcEngine.x86_64.sdaiUNICODE, out description) == 0)
            {
                TreeNode tnDescription = tnDescriptions.Nodes.Add(Marshal.PtrToStringUni(description));
                tnDescription.ImageIndex = tnDescription.SelectedImageIndex = IMAGE_PROPERTY;
            }

            // ImplementationLevel
            IntPtr implementationLevel;
            IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 1, 0, IfcEngine.x86_64.sdaiUNICODE, out implementationLevel);

            TreeNode tnImplementationLevel = tnHeaderInfo.Nodes.Add("ImplementationLevel = '" + Marshal.PtrToStringUni(implementationLevel) + "'");
            tnImplementationLevel.ImageIndex = tnImplementationLevel.SelectedImageIndex = IMAGE_PROPERTY;

            // Name
            IntPtr name;
            IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 2, 0, IfcEngine.x86_64.sdaiUNICODE, out name);

            TreeNode tnName = tnHeaderInfo.Nodes.Add("Name = '" + Marshal.PtrToStringUni(name) + "'");
            tnName.ImageIndex = tnName.SelectedImageIndex = IMAGE_PROPERTY;

            // TimeStamp
            IntPtr timeStamp;
            IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 3, 0, IfcEngine.x86_64.sdaiUNICODE, out timeStamp);

            TreeNode tnTimeStamp = tnHeaderInfo.Nodes.Add("TimeStamp = '" + Marshal.PtrToStringUni(timeStamp) + "'");
            tnTimeStamp.ImageIndex = tnTimeStamp.SelectedImageIndex = IMAGE_PROPERTY;

            // Authors
            TreeNode tnAuthors = tnHeaderInfo.Nodes.Add("Authors");
            tnAuthors.ImageIndex = tnAuthors.SelectedImageIndex = IMAGE_PROPERTY_SET;

            i = 0;
            IntPtr author;
            while (IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 4, i++, IfcEngine.x86_64.sdaiUNICODE, out author) == 0)
            {
                TreeNode tnAuthor = tnAuthors.Nodes.Add(Marshal.PtrToStringUni(author));
                tnAuthor.ImageIndex = tnAuthor.SelectedImageIndex = IMAGE_PROPERTY;
            }

            // Organizations
            TreeNode tnOrganizations = tnHeaderInfo.Nodes.Add("Organizations");
            tnOrganizations.ImageIndex = tnOrganizations.SelectedImageIndex = IMAGE_PROPERTY_SET;

            i = 0;
            IntPtr organization;
            while (IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 5, i++, IfcEngine.x86_64.sdaiUNICODE, out organization) == 0)
            {
                TreeNode tnOrganization = tnOrganizations.Nodes.Add(Marshal.PtrToStringUni(organization));
                tnOrganization.ImageIndex = tnOrganization.SelectedImageIndex = IMAGE_PROPERTY;
            }

            // PreprocessorVersion
            IntPtr preprocessorVersion;
            IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 6, 0, IfcEngine.x86_64.sdaiUNICODE, out preprocessorVersion);

            TreeNode tnPreprocessorVersion = tnHeaderInfo.Nodes.Add("PreprocessorVersion = '" + Marshal.PtrToStringUni(preprocessorVersion) + "'");
            tnPreprocessorVersion.ImageIndex = tnPreprocessorVersion.SelectedImageIndex = IMAGE_PROPERTY;

            // OriginatingSystem
            IntPtr originatingSystem;
            IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 7, 0, IfcEngine.x86_64.sdaiUNICODE, out originatingSystem);

            TreeNode tnOriginatingSystem = tnHeaderInfo.Nodes.Add("OriginatingSystem = '" + Marshal.PtrToStringUni(originatingSystem) + "'");
            tnOriginatingSystem.ImageIndex = tnOriginatingSystem.SelectedImageIndex = IMAGE_PROPERTY;

            // Authorization
            IntPtr authorization;
            IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 8, 0, IfcEngine.x86_64.sdaiUNICODE, out authorization);

            TreeNode tnAuthorization = tnHeaderInfo.Nodes.Add("Authorization = '" + Marshal.PtrToStringUni(authorization) + "'");
            tnAuthorization.ImageIndex = tnAuthorization.SelectedImageIndex = IMAGE_PROPERTY;

            // FileSchemas
            TreeNode tnFileSchemas = tnHeaderInfo.Nodes.Add("FileSchemas");
            tnFileSchemas.ImageIndex = tnFileSchemas.SelectedImageIndex = IMAGE_PROPERTY_SET;

            i = 0;
            IntPtr fileSchema;
            while (IfcEngine.x86_64.GetSPFFHeaderItem(_ifcModel.Model, 9, i++, IfcEngine.x86_64.sdaiUNICODE, out fileSchema) == 0)
            {
                TreeNode tnFileSchema = tnFileSchemas.Nodes.Add(Marshal.PtrToStringUni(fileSchema));
                tnFileSchema.ImageIndex = tnFileSchema.SelectedImageIndex = IMAGE_PROPERTY;
            }
        }

        /// <summary>
        /// Loads the projects
        /// </summary>
        private void LoadProjects()
        {
            int_t iEntityID = IfcEngine.x86_64.sdaiGetEntityExtentBN(_ifcModel.Model, Encoding.Unicode.GetBytes("IfcProject"));
            int_t iEntitiesCount = IfcEngine.x86_64.sdaiGetMemberCount(iEntityID);

            for (int_t iEntity = 0; iEntity < iEntitiesCount; iEntity++)
            {
                int_t iInstance = 0;
                IfcEngine.x86_64.engiGetAggrElement(iEntityID, iEntity, IfcEngine.x86_64.sdaiINSTANCE, out iInstance);

                IFCTreeNode tnProject = CreateIFCTreeNode(null, iInstance);
                _treeView.Nodes.Add(tnProject);

                /*
		        * decomposition/contains
		        */
                LoadIsDecomposedBy(tnProject, iInstance);
                LoadContainsElements(tnProject, iInstance);

                /*
                 * Update the states
                 */
                UpdateItemState(tnProject);

                /*
                 * Units
                 */
                _dicUnits = IFCUnit.LoadUnits(_ifcModel.Model, iInstance);

                // Support for 1 project only
                break;
            } // for (int_t iEntity = ...
        }

        /// <summary>
        /// Load the children
        /// </summary>
        /// <param name="ifcParent"></param>
        /// <param name="iParentInstance"></param>
        private void LoadIsDecomposedBy(IFCTreeNode ifcParent, int_t iParentInstance)
        {
            // check for decomposition
            IntPtr decompositionInstance;
            IfcEngine.x86_64.sdaiGetAttrBN(iParentInstance, Encoding.Unicode.GetBytes("IsDecomposedBy"), IfcEngine.x86_64.sdaiAGGR, out decompositionInstance);

            if (decompositionInstance == IntPtr.Zero)
            {
                return;
            }

            int_t iDecompositionsCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)decompositionInstance);
            if (iDecompositionsCount == 0)
            {
                return;
            }

            IFCTreeNode ifcDecomposition = new IFCTreeNode(DECOMPOSITION, IFCTreeNodeType.decomposition);
            ifcDecomposition.ImageIndex = ifcDecomposition.SelectedImageIndex = IMAGE_UNCHECKED;
            ifcParent.Nodes.Add(ifcDecomposition);

            for (int_t iDecomposition = 0; iDecomposition < iDecompositionsCount; iDecomposition++)
            {
                int_t iDecompositionInstance = 0;
                IfcEngine.x86_64.engiGetAggrElement((int_t)decompositionInstance, iDecomposition, IfcEngine.x86_64.sdaiINSTANCE, out iDecompositionInstance);

                if (!IsInstanceOf(iDecompositionInstance, "IFCRELAGGREGATES"))
                {
                    continue;
                }

                IntPtr objectInstances;
                IfcEngine.x86_64.sdaiGetAttrBN(iDecompositionInstance, Encoding.Unicode.GetBytes("RelatedObjects"), IfcEngine.x86_64.sdaiAGGR, out objectInstances);

                int_t iObjectsCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)objectInstances);
                for (int_t iObject = 0; iObject < iObjectsCount; iObject++)
                {
                    int_t iObjectInstance = 0;
                    IfcEngine.x86_64.engiGetAggrElement((int_t)objectInstances, iObject, IfcEngine.x86_64.sdaiINSTANCE, out iObjectInstance);

                    IFCTreeNode ifcTreeItem = CreateIFCTreeNode(ifcDecomposition, iObjectInstance);

                    /*
		            * decomposition/contains
		            */
                    LoadIsDecomposedBy(ifcTreeItem, iObjectInstance);
                    LoadContainsElements(ifcTreeItem, iObjectInstance);
                } // for (int_t iObject = ...
            } // for (int_t iDecomposition = ...
        }

        /// <summary>
        /// Load the children
        /// </summary>
        /// <param name="ifcParent"></param>
        /// <param name="iParentInstance"></param>
        private void LoadContainsElements(IFCTreeNode ifcParent, int_t iParentInstance)
        {
            // check for elements
            IntPtr elementsInstance;
            IfcEngine.x86_64.sdaiGetAttrBN(iParentInstance, Encoding.Unicode.GetBytes("ContainsElements"), IfcEngine.x86_64.sdaiAGGR, out elementsInstance);

            if (elementsInstance == IntPtr.Zero)
            {
                return;
            }

            int_t iElementsCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)elementsInstance);
            if (iElementsCount == 0)
            {
                return;
            }

            IFCTreeNode ifcContains = new IFCTreeNode(CONTAINS, IFCTreeNodeType.decomposition);
            ifcContains.ImageIndex = ifcContains.SelectedImageIndex = IMAGE_UNCHECKED;
            ifcParent.Nodes.Add(ifcContains);

            for (int_t iElement = 0; iElement < iElementsCount; iElement++)
            {
                int_t iElementInstance = 0;
                IfcEngine.x86_64.engiGetAggrElement((int_t)elementsInstance, iElement, IfcEngine.x86_64.sdaiINSTANCE, out iElementInstance);

                if (!IsInstanceOf(iElementInstance, "IFCRELCONTAINEDINSPATIALSTRUCTURE"))
                {
                    continue;
                }

                IntPtr objectInstances;
                IfcEngine.x86_64.sdaiGetAttrBN(iElementInstance, Encoding.Unicode.GetBytes("RelatedElements"), IfcEngine.x86_64.sdaiAGGR, out objectInstances);

                int_t iObjectsCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)objectInstances);
                for (int_t iObject = 0; iObject < iObjectsCount; iObject++)
                {
                    int_t iObjectInstance = 0;
                    IfcEngine.x86_64.engiGetAggrElement((int_t)objectInstances, iObject, IfcEngine.x86_64.sdaiINSTANCE, out iObjectInstance);

                    IFCTreeNode ifcTreeItem = CreateIFCTreeNode(ifcContains, iObjectInstance);
                    if (!_ifcModel.Items.Keys.Contains(iObjectInstance))
                    {
                        ifcTreeItem.ImageIndex = ifcTreeItem.SelectedImageIndex = IMAGE_NOT_REFERENCED;
                    }

                    /*
                    * decomposition/contains
                    */
                    LoadIsDecomposedBy(ifcTreeItem, iObjectInstance);
                    LoadContainsElements(ifcTreeItem, iObjectInstance);
                } // for (int_t iObject = ...
            } // for (int_t iDecomposition = ...
        }

        /// <summary>
        /// Support for properties
        /// </summary>
        /// <param name="iInstance"></param>
        /// <returns></returns>
        private bool HasProperties(int_t iInstance)
        {
            IntPtr definedByInstances;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("IsDefinedBy"), IfcEngine.x86_64.sdaiAGGR, out definedByInstances);

            if (definedByInstances == IntPtr.Zero)
            {
                return false;
            }

            int_t iDefinedByCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)definedByInstances);
            for (int_t iDefinedBy = 0; iDefinedBy < iDefinedByCount; iDefinedBy++)
            {
                int_t iDefinedByInstance = 0;
                IfcEngine.x86_64.engiGetAggrElement((int_t)definedByInstances, iDefinedBy, IfcEngine.x86_64.sdaiINSTANCE, out iDefinedByInstance);

                if (IsInstanceOf(iDefinedByInstance, "IFCRELDEFINESBYPROPERTIES") || IsInstanceOf(iDefinedByInstance, "IFCRELDEFINESBYTYPE"))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Load the properties
        /// </summary>
        /// <param name="ifcParent"></param>
        /// <param name="iInstance"></param>
        private void LoadRelDefinesByProperties(IFCTreeNode ifcParent, int_t iInstance)
        {
            IntPtr propertyInstances;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("RelatingPropertyDefinition"), IfcEngine.x86_64.sdaiINSTANCE, out propertyInstances);

            if (IsInstanceOf((int_t)propertyInstances, "IFCELEMENTQUANTITY"))
            {
                IFCTreeNode ifcPropertySetTreeItem = new IFCTreeNode(BuildItemText((int_t)propertyInstances), IFCTreeNodeType.properties);
                ifcPropertySetTreeItem.ImageIndex = ifcPropertySetTreeItem.SelectedImageIndex = IMAGE_PROPERTY_SET;
                ifcPropertySetTreeItem.ForeColor = Color.Gray;
                ifcParent.Nodes.Add(ifcPropertySetTreeItem);

                // check for quantity
                IntPtr quantitiesInstance;
                IfcEngine.x86_64.sdaiGetAttrBN((int_t)propertyInstances, Encoding.Unicode.GetBytes("Quantities"), IfcEngine.x86_64.sdaiAGGR, out quantitiesInstance);

                if (quantitiesInstance == IntPtr.Zero)
                {
                    return;
                }

                int_t iQuantitiesCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)quantitiesInstance);
                for (int_t iQuantity = 0; iQuantity < iQuantitiesCount; iQuantity++)
                {
                    int_t iQuantityInstance = 0;
                    IfcEngine.x86_64.engiGetAggrElement((int_t)quantitiesInstance, iQuantity, IfcEngine.x86_64.sdaiINSTANCE, out iQuantityInstance);

                    string strPropertyText = string.Empty;

                    if (IsInstanceOf(iQuantityInstance, "IFCQUANTITYLENGTH"))
                        strPropertyText = BuildPropertyText((int_t)iQuantityInstance, "IFCQUANTITYLENGTH");
                    else
                        if (IsInstanceOf(iQuantityInstance, "IFCQUANTITYAREA"))
                            strPropertyText = BuildPropertyText((int_t)iQuantityInstance, "IFCQUANTITYAREA");
                        else
                            if (IsInstanceOf(iQuantityInstance, "IFCQUANTITYVOLUME"))
                                strPropertyText = BuildPropertyText((int_t)iQuantityInstance, "IFCQUANTITYVOLUME");
                            else
                                if (IsInstanceOf(iQuantityInstance, "IFCQUANTITYCOUNT"))
                                    strPropertyText = BuildPropertyText((int_t)iQuantityInstance, "IFCQUANTITYCOUNT");
                                else
                                    if (IsInstanceOf(iQuantityInstance, "IFCQUANTITYWEIGTH"))
                                        strPropertyText = BuildPropertyText((int_t)iQuantityInstance, "IFCQUANTITYWEIGTH");
                                    else
                                        if (IsInstanceOf(iQuantityInstance, "IFCQUANTITYTIME"))
                                            strPropertyText = BuildPropertyText((int_t)iQuantityInstance, "IFCQUANTITYTIME");

                    IFCTreeNode ifcQuantityTreeItem = new IFCTreeNode(strPropertyText, IFCTreeNodeType.property);
                    ifcQuantityTreeItem.ImageIndex = ifcQuantityTreeItem.SelectedImageIndex = IMAGE_PROPERTY;
                    ifcQuantityTreeItem.ForeColor = Color.Gray;
                    ifcPropertySetTreeItem.Nodes.Add(ifcQuantityTreeItem);
                } // for (int iQuantity = ...
            }
            else
            {
                if (IsInstanceOf((int_t)propertyInstances, "IFCPROPERTYSET"))
                {
                    IFCTreeNode ifcPropertySetTreeItem = new IFCTreeNode(BuildItemText((int_t)propertyInstances), IFCTreeNodeType.properties);
                    ifcPropertySetTreeItem.ImageIndex = ifcPropertySetTreeItem.SelectedImageIndex = IMAGE_PROPERTY_SET;
                    ifcPropertySetTreeItem.ForeColor = Color.Gray;
                    ifcParent.Nodes.Add(ifcPropertySetTreeItem);

                    // check for quantity
                    IntPtr propertiesInstance;
                    IfcEngine.x86_64.sdaiGetAttrBN((int_t)propertyInstances, Encoding.Unicode.GetBytes("HasProperties"), IfcEngine.x86_64.sdaiAGGR, out propertiesInstance);

                    if (propertiesInstance == IntPtr.Zero)
                    {
                        return;
                    }

                    int_t iPropertiesCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)propertiesInstance);
                    for (int_t iProperty = 0; iProperty < iPropertiesCount; iProperty++)
                    {
                        int_t iPropertyInstance = 0;
                        IfcEngine.x86_64.engiGetAggrElement((int_t)propertiesInstance, iProperty, IfcEngine.x86_64.sdaiINSTANCE, out iPropertyInstance);

                        if (!IsInstanceOf(iPropertyInstance, "IFCPROPERTYSINGLEVALUE"))
                            continue;

                        IFCTreeNode ifcPropertyTreeItem = new IFCTreeNode(BuildPropertyText(iPropertyInstance, "IFCPROPERTYSINGLEVALUE"), IFCTreeNodeType.property);
                        ifcPropertyTreeItem.ImageIndex = ifcPropertyTreeItem.SelectedImageIndex = IMAGE_PROPERTY;
                        ifcPropertyTreeItem.ForeColor = Color.Gray;
                        ifcPropertySetTreeItem.Nodes.Add(ifcPropertyTreeItem);
                    } // for (int iProperty = ...
                }
            }
        }

        /// <summary>
        /// Loads Not referenced IFCItem-s
        /// </summary>
        private void LoadNotReferenced()
        {
            IFCTreeNode tnNotReferenced = new IFCTreeNode("Not referenced", IFCTreeNodeType.unknown);

            int iCheckedItemsCount = 0;
            foreach (var ifcItem in _ifcModel.Geometry)
            {
                if (ifcItem._view != null)
                {
                    continue;
                }

                IFCTreeNode ifcTreeItem = CreateIFCTreeNode(tnNotReferenced, ifcItem._instance);                
                iCheckedItemsCount += ifcItem._visible ? 1 : 0;
            } // foreach (var ifcItem ...

            if (tnNotReferenced.Nodes.Count > 0)
            {
                if (iCheckedItemsCount > 0)
                {
                    tnNotReferenced.ImageIndex = tnNotReferenced.SelectedImageIndex = iCheckedItemsCount == tnNotReferenced.Nodes.Count ? IMAGE_CHECKED : IMAGE_MIDDLE;
                }
                else
                {
                    tnNotReferenced.ImageIndex = tnNotReferenced.SelectedImageIndex = IMAGE_UNCHECKED;
                }

                _treeView.Nodes.Add(tnNotReferenced);
            }
        }

        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="iChildInstance"></param>
        /// <returns></returns>
        private IFCTreeNode CreateIFCTreeNode(IFCTreeNode ifcParent, int_t iChildInstance)
        {
            IFCTreeNode ifcChild = new IFCTreeNode(BuildItemText(iChildInstance), IFCTreeNodeType.item);            

            // check the visual presentation & checked/unchecked state
            if (_ifcModel.Items.Keys.Contains(iChildInstance))
            {                
                IFCItem ifcItem = _ifcModel.Items[iChildInstance];
                ifcChild.Tag = ifcItem;
                ifcChild.Item = ifcItem;
                ifcItem._view = ifcChild;
                ifcChild.ImageIndex = ifcChild.SelectedImageIndex = ifcItem._visible ? IMAGE_CHECKED : IMAGE_UNCHECKED;

                // Geometry
                IFCTreeNode ifcGeometry = new IFCTreeNode(GEOMETRY, IFCTreeNodeType.geometry);

                if (!ifcItem.hasGeometry)
                {
                    ifcChild.ForeColor = Color.Gray;

                    ifcGeometry.ImageIndex = ifcGeometry.SelectedImageIndex = IMAGE_NOT_REFERENCED;                    
                }
                else
                {
                    ifcGeometry.ImageIndex = ifcGeometry.SelectedImageIndex = ifcItem._visible ? IMAGE_CHECKED : IMAGE_UNCHECKED;
                }

                ifcChild.Nodes.Add(ifcGeometry);

                // Properties
                if (HasProperties(iChildInstance))
                {
                    IFCTreeNode ifcProperties = new IFCTreeNode(PROPERTIES, IFCTreeNodeType.properties);
                    ifcProperties.ImageIndex = ifcProperties.SelectedImageIndex = IMAGE_PROPERTY_SET;

                    ifcChild.Nodes.Add(ifcProperties);

                    // Load on demand
                    IFCTreeNode ifcLoadOnDemand = new IFCTreeNode(PROPERTIES_PENDING_LOAD, IFCTreeNodeType.unknown);
                    ifcProperties.Nodes.Add(ifcLoadOnDemand);
                }
            }
            else
            {
                ifcChild.ImageIndex = ifcChild.SelectedImageIndex = IMAGE_UNCHECKED;
                ifcChild.ForeColor = Color.Gray;

                // Geometry
                IFCTreeNode ifcGeometry = new IFCTreeNode(GEOMETRY, IFCTreeNodeType.geometry);
                ifcGeometry.ImageIndex = ifcGeometry.SelectedImageIndex = IMAGE_NOT_REFERENCED;
                ifcChild.Nodes.Add(ifcGeometry);

                // Properties
                if (HasProperties(iChildInstance))
                {
                    IFCTreeNode ifcProperties = new IFCTreeNode(PROPERTIES, IFCTreeNodeType.properties);
                    ifcProperties.ImageIndex = ifcProperties.SelectedImageIndex = IMAGE_PROPERTY_SET;

                    ifcChild.Nodes.Add(ifcProperties);

                    // Load on demand
                    IFCTreeNode ifcLoadOnDemand = new IFCTreeNode(PROPERTIES_PENDING_LOAD, IFCTreeNodeType.unknown);
                    ifcProperties.Nodes.Add(ifcLoadOnDemand);
                }
            }

            if (ifcParent != null)
            {
                ifcParent.Nodes.Add(ifcChild);
            }

            return ifcChild;
        }

        /// <summary>
        /// Updates the visual state
        /// </summary>
        /// <param name="ifcParent"></param>
        private void UpdateItemState(IFCTreeNode ifcParent)
        {
            System.Diagnostics.Debug.Assert(ifcParent != null);

            int iItemsCount = 0;
            int iCheckedItems = 0;
            int iSemiCheckedItems = 0;
            foreach (var treeNode in ifcParent.Nodes)
            {
                IFCTreeNode ifcItem = treeNode as IFCTreeNode;
                System.Diagnostics.Debug.Assert(ifcItem != null);

                if ((ifcItem.Type == IFCTreeNodeType.properties) ||
                    (ifcItem.Type == IFCTreeNodeType.property))
                {
                    continue;
                }

                UpdateItemState(ifcItem);

                iItemsCount += ifcItem.ImageIndex == IMAGE_CHECKED || ifcItem.ImageIndex == IMAGE_MIDDLE || ifcItem.ImageIndex == IMAGE_UNCHECKED ? 1 : 0;
                iCheckedItems += ifcItem.ImageIndex == IMAGE_CHECKED ? 1 : 0;
                iSemiCheckedItems += ifcItem.ImageIndex == IMAGE_MIDDLE ? 1 : 0;
            } // foreach (var treeNode ...

            // Update the state only if the item has children; otherwise keep the original state
            if (iItemsCount > 0)
            {
                if (iSemiCheckedItems > 0)
                {
                    ifcParent.ImageIndex = ifcParent.SelectedImageIndex = IMAGE_MIDDLE;
                }
                else
                {
                    if (iCheckedItems > 0)
                    {
                        ifcParent.ImageIndex = ifcParent.SelectedImageIndex = iCheckedItems == iItemsCount ? IMAGE_CHECKED : IMAGE_MIDDLE;
                    }
                    else
                    {
                        ifcParent.ImageIndex = ifcParent.SelectedImageIndex = IMAGE_UNCHECKED;
                    }
                }                            
            }
        }

        /// <summary>
        /// Updates the visual state and model
        /// </summary>
        /// <param name="ifcParent"></param>
        private void OnParentItemStateChanged(IFCTreeNode ifcParent)
        {
            System.Diagnostics.Debug.Assert(ifcParent != null);

            foreach (var treeNode in ifcParent.Nodes)
            {
                IFCTreeNode ifcItem = treeNode as IFCTreeNode;
                System.Diagnostics.Debug.Assert(ifcItem != null);

                if ((ifcItem.Type == IFCTreeNodeType.properties) ||
                    (ifcItem.Type == IFCTreeNodeType.property))
                {
                    continue;
                }

                switch (ifcParent.ImageIndex)
                {
                    case IMAGE_CHECKED:
                        {
                            ifcItem.ImageIndex = ifcItem.SelectedImageIndex = IMAGE_CHECKED;

                            if (ifcItem.Item != null)
                            {
                                ifcItem.Item._visible = true;
                            }

                        }
                        break;

                    case IMAGE_UNCHECKED:
                        {
                            ifcItem.ImageIndex = ifcItem.SelectedImageIndex = IMAGE_UNCHECKED;

                            if (ifcItem.Item != null)
                            {
                                ifcItem.Item._visible = false;
                            }
                        }
                        break;

                    default:
                        {
                            System.Diagnostics.Debug.Assert(false); // unexpected
                        }
                        break;
                } // switch (ifcParent.ImageIndex)

                OnParentItemStateChanged(ifcItem);
            } // foreach (var treeNode ...
        }

        /// <summary>
        /// Updates the visual state and model
        /// </summary>
        /// <param name="ifcParent"></param>
        private void OnChildItemStateChanged(IFCTreeNode ifcChild)
        {
            System.Diagnostics.Debug.Assert(ifcChild != null);

            if (ifcChild.Parent == null)
            {
                return;
            }

            int iItemsCount = 0;
            int iCheckedItems = 0;
            int iSemiCheckedItems = 0;
            foreach (var treeNode in ifcChild.Parent.Nodes)
            {
                IFCTreeNode ifcItem = treeNode as IFCTreeNode;
                System.Diagnostics.Debug.Assert(ifcItem != null);

                if ((ifcItem.Type == IFCTreeNodeType.properties) ||
                    (ifcItem.Type == IFCTreeNodeType.property))
                {
                    continue;
                }

                iItemsCount += ifcItem.ImageIndex == IMAGE_CHECKED || ifcItem.ImageIndex == IMAGE_MIDDLE || ifcItem.ImageIndex == IMAGE_UNCHECKED ? 1 : 0;
                iCheckedItems += ifcItem.ImageIndex == IMAGE_CHECKED ? 1 : 0;
                iSemiCheckedItems += ifcItem.ImageIndex == IMAGE_MIDDLE ? 1 : 0;
            } // foreach (var treeNode ...

            // Update the state only if the item has children; otherwise keep the original state
            if (iItemsCount > 0)
            {
                if (iSemiCheckedItems > 0)
                {
                    ifcChild.Parent.ImageIndex = ifcChild.Parent.SelectedImageIndex = IMAGE_MIDDLE;
                }
                else
                {
                    if (iCheckedItems > 0)
                    {
                        ifcChild.Parent.ImageIndex = ifcChild.Parent.SelectedImageIndex = iCheckedItems == iItemsCount ? IMAGE_CHECKED : IMAGE_MIDDLE;
                    }
                    else
                    {
                        ifcChild.Parent.ImageIndex = ifcChild.Parent.SelectedImageIndex = IMAGE_UNCHECKED;
                    }
                }
            }

            if (ifcChild.Parent.Parent != null)
            {
                OnChildItemStateChanged(ifcChild.Parent as IFCTreeNode);
            }            
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iInstance"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        private bool IsInstanceOf(int_t iInstance, string strType)
        {
            if (IfcEngine.x86_64.sdaiGetInstanceType(iInstance) == IfcEngine.x86_64.sdaiGetEntity(_ifcModel.Model, Encoding.Unicode.GetBytes(strType)))
            {
                return true;
            }

            return false;
        }  

        /// <summary>
        /// Label for the IFCItem-s
        /// </summary>
        /// <param name="iInstance"></param>
        /// <returns></returns>
        private string BuildItemText(int_t iInstance)
        {
            if (iInstance == 0)
            {
                System.Diagnostics.Debug.Assert(false);

                return string.Empty;
            }

            int_t iType = IfcEngine.x86_64.sdaiGetInstanceType(iInstance);

            IntPtr type = IntPtr.Zero;
            IfcEngine.x86_64.engiGetEntityName(iType, IfcEngine.x86_64.sdaiUNICODE, out type);

            string strIfcType = Marshal.PtrToStringUni(type);

            IntPtr name;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("Name"), IfcEngine.x86_64.sdaiUNICODE, out name);

            string strName = Marshal.PtrToStringUni(name);

            IntPtr globalID;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("GlobalID"), IfcEngine.x86_64.sdaiUNICODE, out globalID);

            string strGlobalID = Marshal.PtrToStringUni(globalID);

            IntPtr description;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("Description"), IfcEngine.x86_64.sdaiUNICODE, out description);

            string strDescription = Marshal.PtrToStringUni(description);

            string strItemText = "'" + (string.IsNullOrEmpty(strName) ? "<name>" : strName) +
                "', '" + (string.IsNullOrEmpty(strDescription) ? "<description>" : strDescription) +
                "' (" + strIfcType + ") GlobalID: " + strGlobalID;

            return strItemText;
        }

        /// <summary>
        /// Label for the properties
        /// </summary>
        /// <param name="iInstance"></param>
        /// <returns></returns>
        private string BuildPropertyText(int_t iInstance, string strProperty)
        {
            if (iInstance == 0)
            {
                System.Diagnostics.Debug.Assert(false);

                return string.Empty;
            }

            int_t entity = IfcEngine.x86_64.sdaiGetInstanceType(iInstance);

            IntPtr entityNamePtr;
            IfcEngine.x86_64.engiGetEntityName(entity, IfcEngine.x86_64.sdaiUNICODE, out entityNamePtr);

            string strIfcType = Marshal.PtrToStringUni(entityNamePtr);

            IntPtr name;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("Name"), IfcEngine.x86_64.sdaiUNICODE, out name);

            string strName = Marshal.PtrToStringUni(name);

            string strUnit = string.Empty;

            IntPtr unit;
            IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("Unit"), IfcEngine.x86_64.sdaiUNICODE, out unit);

            if (unit != IntPtr.Zero)
            {
                strUnit = Marshal.PtrToStringUni(unit);
            }

            string strValue = string.Empty;
            switch (strProperty)
            {
                case "IFCQUANTITYLENGTH":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("LengthValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);

                        if (string.IsNullOrEmpty(strUnit))
                        {
                            if (_dicUnits.Keys.Contains("LENGTHUNIT"))
                            {
                                strUnit = _dicUnits["LENGTHUNIT"].Name;
                            }
                        }
                    }
                    break;

                case "IFCQUANTITYAREA":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("AreaValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);

                        if (string.IsNullOrEmpty(strUnit))
                        {
                            if (_dicUnits.Keys.Contains("AREAUNIT"))
                            {
                                strUnit = _dicUnits["AREAUNIT"].Name;
                            }
                        }
                    }
                    break;

                case "IFCQUANTITYVOLUME":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("VolumeValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);

                        if (string.IsNullOrEmpty(strUnit))
                        {
                            if (_dicUnits.Keys.Contains("VOLUMEUNIT"))
                            {
                                strUnit = _dicUnits["VOLUMEUNIT"].Name;
                            }
                        }
                    }
                    break;

                case "IFCQUANTITYCOUNT":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("CountValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);
                    }
                    break;

                case "IFCQUANTITYWEIGTH":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("WeigthValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);

                        if (string.IsNullOrEmpty(strUnit))
                        {
                            if (_dicUnits.Keys.Contains("MASSUNIT"))
                            {
                                strUnit = _dicUnits["MASSUNIT"].Name;
                            }
                        }
                    }
                    break;

                case "IFCQUANTITYTIME":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("TimeValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);

                        if (string.IsNullOrEmpty(strUnit))
                        {
                            if (_dicUnits.Keys.Contains("TIMEUNIT"))
                            {
                                strUnit = _dicUnits["TIMEUNIT"].Name;
                            }
                        }
                    }
                    break;

                case "IFCPROPERTYSINGLEVALUE":
                    {
                        IntPtr value;
                        IfcEngine.x86_64.sdaiGetAttrBN(iInstance, Encoding.Unicode.GetBytes("NominalValue"), IfcEngine.x86_64.sdaiUNICODE, out value);

                        strValue = Marshal.PtrToStringUni(value);
                    }
                    break;

                default:
                    throw new Exception("Unknown property.");
            } // switch (strProperty)    

            string strItemText = "'" + (string.IsNullOrEmpty(strName) ? "<name>" : strName) +
                "' = '" + (string.IsNullOrEmpty(strValue) ? "<value>" : strValue) +
                (!string.IsNullOrEmpty(strUnit) ? " " + strUnit : "") +
                "' (" + strIfcType + ")";

            return strItemText;
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Rectangle rcIcon = new Rectangle(e.Node.Bounds.Location - new Size(16, 0), new Size(16, 16));
                if (!rcIcon.Contains(e.Location))
                {
                    return;
                }

                IFCTreeNode ifcItem = e.Node as IFCTreeNode;

                System.Diagnostics.Debug.Assert(ifcItem != null);

                switch (e.Node.ImageIndex)
                {
                    case IMAGE_CHECKED:
                    case IMAGE_MIDDLE:
                        {
                            e.Node.ImageIndex = e.Node.SelectedImageIndex = IMAGE_UNCHECKED;

                            // Geometry
                            if (ifcItem.Type == IFCTreeNodeType.geometry)
                            {
                                System.Diagnostics.Debug.Assert(ifcItem.Parent != null);

                                IFCTreeNode ifcParent = ifcItem.Parent as IFCTreeNode;
                                System.Diagnostics.Debug.Assert(ifcParent.Item != null);

                                ifcParent.Item._visible = false;

                                OnChildItemStateChanged(ifcItem);
                            }
                            else
                            {
                                if (ifcItem.Item != null)
                                {
                                    ifcItem.Item._visible = false;
                                }

                                OnParentItemStateChanged(ifcItem);
                                OnChildItemStateChanged(ifcItem);
                            }
                        }
                        break;

                    case IMAGE_UNCHECKED:
                        {
                            e.Node.ImageIndex = e.Node.SelectedImageIndex = IMAGE_CHECKED;

                            if (ifcItem.Type == IFCTreeNodeType.geometry)
                            {
                                System.Diagnostics.Debug.Assert(ifcItem.Parent != null);

                                IFCTreeNode ifcParent = ifcItem.Parent as IFCTreeNode;
                                System.Diagnostics.Debug.Assert(ifcParent.Item != null);

                                ifcParent.Item._visible = true;

                                OnChildItemStateChanged(ifcItem);
                            }
                            else
                            {
                                if (ifcItem.Item != null)
                                {
                                    ifcItem.Item._visible = true;
                                }

                                OnParentItemStateChanged(ifcItem);
                                OnChildItemStateChanged(ifcItem);
                            }                            
                        }
                        break;
                } // switch (e.Node.ImageIndex)
            } // if (e.Button == System.Windows.Forms.MouseButtons.Left)
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    _treeView.SelectedNode = e.Node;
                }
            }
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IFCItem ifcItem = null;

            if (e.Node.Tag != null)
            {
                ifcItem = e.Node.Tag as IFCItem;
                System.Diagnostics.Debug.Assert(ifcItem != null);
            }

            _ifcController.SelectItem(this, ifcItem);
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IFCTreeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ifcController.UnRegisterView(this);
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            IFCTreeNode ifcItem = e.Node as IFCTreeNode;
            if (ifcItem == null)
            {
                return;
            }

            if (ifcItem.Nodes.Count != 1)
            {
                return;
            }

            if (ifcItem.Nodes[0].Text != PROPERTIES_PENDING_LOAD)
            {
                return;
            }

            ifcItem.Nodes.Clear();

            System.Diagnostics.Debug.Assert(ifcItem.Parent != null);

            IFCTreeNode ifcParent = ifcItem.Parent as IFCTreeNode;
            System.Diagnostics.Debug.Assert(ifcParent != null);
            System.Diagnostics.Debug.Assert(ifcParent.Item != null);

            IntPtr definedByInstances;
            IfcEngine.x86_64.sdaiGetAttrBN(ifcParent.Item._instance, Encoding.Unicode.GetBytes("IsDefinedBy"), IfcEngine.x86_64.sdaiAGGR, out definedByInstances);

            System.Diagnostics.Debug.Assert(definedByInstances != IntPtr.Zero);

            int_t iDefinedByCount = IfcEngine.x86_64.sdaiGetMemberCount((int_t)definedByInstances);
            for (int_t iDefinedBy = 0; iDefinedBy < iDefinedByCount; iDefinedBy++)
            {
                int_t iDefinedByInstance = 0;
                IfcEngine.x86_64.engiGetAggrElement((int_t)definedByInstances, iDefinedBy, IfcEngine.x86_64.sdaiINSTANCE, out iDefinedByInstance);

                if (IsInstanceOf(iDefinedByInstance, "IFCRELDEFINESBYPROPERTIES"))
                {
                    LoadRelDefinesByProperties(ifcItem, iDefinedByInstance);
                }
                else
                {
                    if (IsInstanceOf(iDefinedByInstance, "IFCRELDEFINESBYTYPE"))
                    {
                        //LoadRelDefinesByType(iIFCIsDefinedByInstance, hParent);
                    }
                }
            }
        }
    }
}