using IfcEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IFCViewerSGL
{
    public class CreateModelAnyCPU
    {
        private string _strFileName = "test.ifc";
        private long _model = 0;

        public CreateModelAnyCPU()
        {            
        }

        public void Run()
        {
            CreateModel();
        }

        private void CreateHeader()
        {
            string strDescription = "ViewDefinition ";
            strDescription += "[";
            strDescription += "CoordinationView";
            strDescription += "QuantityTakeOffAddOnView";
            strDescription += "]";

            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 0, 0, IfcEngineAnyCPU.sdaiUNICODE, strDescription);
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 0, 1, IfcEngineAnyCPU.sdaiSTRING, string.Empty);
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 1, 0, IfcEngineAnyCPU.sdaiSTRING, "2;1");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 2, 0, IfcEngineAnyCPU.sdaiUNICODE, _strFileName);
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 3, 0, IfcEngineAnyCPU.sdaiUNICODE, IfcEngineAnyCPU.getTimeStamp(_model).ToString());
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 4, 0, IfcEngineAnyCPU.sdaiSTRING, "Architect");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 4, 1, IfcEngineAnyCPU.sdaiSTRING, string.Empty);
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 5, 0, IfcEngineAnyCPU.sdaiSTRING, "Building Designer Office");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 6, 0, IfcEngineAnyCPU.sdaiSTRING, "IFC Engine DLL 2015");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 7, 0, IfcEngineAnyCPU.sdaiSTRING, "Hello Wall example");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 8, 0, IfcEngineAnyCPU.sdaiSTRING, "The authorising person");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 9, 0, IfcEngineAnyCPU.sdaiSTRING, "IFC2X3_TC1");
            IfcEngineAnyCPU.SetSPFFHeaderItem(_model, 9, 1, IfcEngineAnyCPU.sdaiSTRING, string.Empty);
        }

        private void CreateModel()
        {
            string ROOT_PATH = Assembly.GetExecutingAssembly().Location.ToUpper().Replace("IFCVIEWERSGL.EXE", "");

            string strExpFile = ROOT_PATH;
            strExpFile += "IFC2X3_TC1.exp";

            /*
             * Model
             */
            _model = IfcEngineAnyCPU.sdaiCreateModelBNUnicode(0, null, strExpFile);
            if (_model == 0)
            {
                Debug.Assert(false);

                return;
            }

            /*
             * HEADER
             */
            CreateHeader();

            /*
             * IFCPERSON
             */
            long ifcPersonInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCPERSON");
            if (ifcPersonInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcPersonInstance, "GivenName", IfcEngineAnyCPU.sdaiUNICODE, "Peter");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcPersonInstance, "FamilyName", IfcEngineAnyCPU.sdaiUNICODE, "Bonsma");

            /*
             * IFCORGANIZATION
             */
            long ifcOrganizationInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCORGANIZATION");
            if (ifcOrganizationInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcOrganizationInstance, "Name", IfcEngineAnyCPU.sdaiUNICODE, "RDF LTD");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcOrganizationInstance, "Description", IfcEngineAnyCPU.sdaiUNICODE, "RDF LTD");

            /*
             * IFCPERSONANDORGANIZATION
             */
            long ifcPersonAndOrganizationInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCPERSONANDORGANIZATION");
            if (ifcPersonAndOrganizationInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcPersonAndOrganizationInstance, "ThePerson", IfcEngineAnyCPU.sdaiINSTANCE, ifcPersonInstance);
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcPersonAndOrganizationInstance, "TheOrganization", IfcEngineAnyCPU.sdaiINSTANCE, ifcOrganizationInstance);

            /*
             * IFCOWNERHISTORY
             */
            long ifcOwnerHistoryInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCOWNERHISTORY");
            if (ifcOwnerHistoryInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcOwnerHistoryInstance, "OwningUser", IfcEngineAnyCPU.sdaiINSTANCE, ifcPersonAndOrganizationInstance);

            long timeStamp = IfcEngineAnyCPU.getTimeStamp(_model);
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcOwnerHistoryInstance, "CreationDate", IfcEngineAnyCPU.sdaiINTEGER, ref timeStamp);

            /*
             * IFCPROJECT
             */
            long ifcProjectInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCPROJECT");
            if (ifcProjectInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcProjectInstance, "GlobalId", IfcEngineAnyCPU.sdaiUNICODE, "123456");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcProjectInstance, "Name", IfcEngineAnyCPU.sdaiUNICODE, "Default Project");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcProjectInstance, "Description", IfcEngineAnyCPU.sdaiUNICODE, "Description of Default Project");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcProjectInstance, "OwnerHistory", IfcEngineAnyCPU.sdaiINSTANCE, ifcOwnerHistoryInstance);

            /*
             * IFCSIUNIT
             */
            long ifcSIUnitInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCSIUNIT");
            if (ifcSIUnitInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcSIUnitInstance, "UnitType", IfcEngineAnyCPU.sdaiENUM, "LENGTHUNIT");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcSIUnitInstance, "Prefix", IfcEngineAnyCPU.sdaiENUM, "MILLI");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcSIUnitInstance, "Name", IfcEngineAnyCPU.sdaiENUM, "METRE");

            /*
             * IFCWINDOW
             */
            long ifcWindowInstance = IfcEngineAnyCPU.sdaiCreateInstanceBN(_model, "IFCWINDOW");
            if (ifcWindowInstance == 0)
            {
                Debug.Assert(false);

                return;
            }

            IfcEngineAnyCPU.sdaiPutAttrBN(ifcWindowInstance, "PredefinedType", IfcEngineAnyCPU.sdaiENUM, "WINDOW");
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcWindowInstance, "PartitioningType", IfcEngineAnyCPU.sdaiENUM, "SINGLE_PANEL");

            double dOverallHeight = 12345;
            double dOverallWidth = 2233;
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcWindowInstance, "OverallHeight", IfcEngineAnyCPU.sdaiREAL, ref dOverallHeight);
            IfcEngineAnyCPU.sdaiPutAttrBN(ifcWindowInstance, "OverallWidth", IfcEngineAnyCPU.sdaiREAL, ref dOverallWidth);

            /*
             * IFC
             */
            string strIfcFile = ROOT_PATH;
            strIfcFile += _strFileName;
            IfcEngineAnyCPU.sdaiSaveModelBNUnicode(_model, strIfcFile);

            /*
             * XML
             */
            string strXmlFile = ROOT_PATH;
            strXmlFile += "test.xml";
            IfcEngineAnyCPU.sdaiSaveModelAsXmlBNUnicode(_model, strXmlFile);
        }
    }
}
