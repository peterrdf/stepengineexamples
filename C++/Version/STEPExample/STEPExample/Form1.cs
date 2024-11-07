using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StepEngine;
#if _WIN64
	using int_t = System.Int64;
#else
    using int_t = System.Int32;
#endif

namespace STEPExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxContent.Text = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\as1-oc-214.stp";
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBoxContent.Text;
                openFileDialog.Filter = "txt files (*.stp)|*.stp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxContent.Text = openFileDialog.FileName;
                }
            }
        }

        public void ProcessGeometry(Int64 myModel, Int64 myInstance, string filename)
        {
            StepEngine.x86_64.SaveInstanceTreeW(myInstance, System.Text.Encoding.Unicode.GetBytes(filename));
        }

        private void buttonFind3DModel_Click(object sender, EventArgs e)
        {
            int_t stepModel = StepEngine.x86_64.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                StepEngine.x86_64.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                StepEngine.x86_64.owlGetModel(stepModel, out geometryKernelModel);

                int_t productDefinitionInstances = StepEngine.x86_64.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION"),
                      noProductDefinitionInstances = StepEngine.x86_64.sdaiGetMemberCount(productDefinitionInstances);
                if (noProductDefinitionInstances != 0)
                {
                    for (int_t i = 0; i < noProductDefinitionInstances; i++)
                    {
                        int_t productDefinitionInstance = 0;
                        StepEngine.x86_64.engiGetAggrElement(productDefinitionInstances, i, StepEngine.x86_64.sdaiINSTANCE, out productDefinitionInstance);

                        Int64 myInstance = 0;
                        StepEngine.x86_64.owlBuildInstance(stepModel, productDefinitionInstance, out myInstance);

                        if (myInstance != 0)
                        {
                            //
                            //  Check if the tree contains real geometry
                            //
                            Int64 vertexArraySize = 0, indexArraySize = 0;
                            StepEngine.x86_64.CalculateInstance(myInstance, out vertexArraySize, out indexArraySize, (IntPtr)0);

                            if (vertexArraySize != 0 && indexArraySize != 0)
                            {
                                Int64 expressID = StepEngine.x86_64.internalGetP21Line(productDefinitionInstance);
                                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\geom-" + expressID + ".bin";
                                ProcessGeometry(geometryKernelModel, myInstance, path);
                            }
                        }
                    }
                }

                StepEngine.x86_64.sdaiCloseModel(stepModel);
            }
        }

        private void buttonFind3DParts_Click(object sender, EventArgs e)
        {
            int_t stepModel = StepEngine.x86_64.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                StepEngine.x86_64.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                StepEngine.x86_64.owlGetModel(stepModel, out geometryKernelModel);

                int_t productDefinitionShapeInstances = StepEngine.x86_64.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
                      noProductDefinitionShapeInstances = StepEngine.x86_64.sdaiGetMemberCount(productDefinitionShapeInstances);
                if (noProductDefinitionShapeInstances != 0)
                {
                    for (int_t i = 0; i < noProductDefinitionShapeInstances; i++)
                    {
                        int_t productDefinitionShapeInstance = 0;
                        StepEngine.x86_64.engiGetAggrElement(productDefinitionShapeInstances, i, StepEngine.x86_64.sdaiINSTANCE, out productDefinitionShapeInstance);

                        Int64 myInstance = 0;
                        StepEngine.x86_64.owlBuildInstance(stepModel, productDefinitionShapeInstance, out myInstance);

                        if (myInstance != 0)
                        {
                            //
                            //  Check if the tree contains real geometry
                            //
                            Int64 vertexArraySize = 0, indexArraySize = 0;
                            StepEngine.x86_64.CalculateInstance(myInstance, out vertexArraySize, out indexArraySize, (IntPtr)0);

                            if (vertexArraySize != 0 && indexArraySize != 0)
                            {
                                Int64 expressID = StepEngine.x86_64.internalGetP21Line(productDefinitionShapeInstance);
                                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\geom-" + expressID + ".bin";
                                ProcessGeometry(geometryKernelModel, myInstance, path);
                            }
                        }
                    }
                }

                StepEngine.x86_64.sdaiCloseModel(stepModel);
            }
        }

        private void buttonFindAssemblies_Click(object sender, EventArgs e)
        {
            int_t stepModel = StepEngine.x86_64.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                StepEngine.x86_64.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                StepEngine.x86_64.owlGetModel(stepModel, out geometryKernelModel);

	            int_t nextAssemblyUsageOccurrenceEntity = StepEngine.x86_64.sdaiGetEntity(stepModel, "NEXT_ASSEMBLY_USAGE_OCCURRENCE");

	            int_t productDefinitionShapeInstances = StepEngine.x86_64.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
			          noProductDefinitionShapeInstances = StepEngine.x86_64.sdaiGetMemberCount(productDefinitionShapeInstances);
	            if (noProductDefinitionShapeInstances != 0)
                {
		            for (int_t i = 0; i < noProductDefinitionShapeInstances; i++)
                    {
			            int_t productDefinitionShapeInstance = 0;
                        StepEngine.x86_64.engiGetAggrElement(productDefinitionShapeInstances, i, StepEngine.x86_64.sdaiINSTANCE, out productDefinitionShapeInstance);

			            Int64 myGeometryInstance = 0;
                        StepEngine.x86_64.owlBuildInstance(stepModel, productDefinitionShapeInstance, out myGeometryInstance);

			            int_t definitionInstance = 0;
                        StepEngine.x86_64.sdaiGetAttrBN(productDefinitionShapeInstance, "definition", StepEngine.x86_64.sdaiINSTANCE, out definitionInstance);
			            if (StepEngine.x86_64.sdaiGetInstanceType(definitionInstance) == nextAssemblyUsageOccurrenceEntity) {
				            int_t relatingProductDefinitionInstance = 0;
                            StepEngine.x86_64.sdaiGetAttrBN(definitionInstance, "relating_product_definition", StepEngine.x86_64.sdaiINSTANCE, out relatingProductDefinitionInstance);
                            Int64 myRelatingProductInstanceExpressID = StepEngine.x86_64.internalGetP21Line(relatingProductDefinitionInstance);

                            int_t relatedProductDefinitionInstance = 0;
                            StepEngine.x86_64.sdaiGetAttrBN(definitionInstance, "related_product_definition", StepEngine.x86_64.sdaiINSTANCE, out relatedProductDefinitionInstance);
                            Int64 myRelatedProductInstanceExpressID = StepEngine.x86_64.internalGetP21Line(relatedProductDefinitionInstance);

                            Int64 propertyParentInstance = StepEngine.x86_64.CreateProperty(geometryKernelModel, StepEngine.x86_64.OBJECTPROPERTY_TYPE, "parentInstance"),
                                  propertyRelatingProduct = StepEngine.x86_64.CreateProperty(geometryKernelModel, StepEngine.x86_64.DATATYPEPROPERTY_TYPE_INTEGER, "relatingProduct"),
                                  propertyRelatedProduct = StepEngine.x86_64.CreateProperty(geometryKernelModel, StepEngine.x86_64.DATATYPEPROPERTY_TYPE_INTEGER, "relatedProduct");

                            Int64 myCollectionInstance = StepEngine.x86_64.CreateInstance(StepEngine.x86_64.CreateClass(geometryKernelModel, "MyClass"), (string) null);

                            StepEngine.x86_64.SetObjectProperty(myCollectionInstance, propertyParentInstance, ref myGeometryInstance, 1);
                            StepEngine.x86_64.SetDatatypeProperty(myCollectionInstance, propertyRelatingProduct, ref myRelatingProductInstanceExpressID, 1);
                            StepEngine.x86_64.SetDatatypeProperty(myCollectionInstance, propertyRelatedProduct, ref myRelatedProductInstanceExpressID, 1);

                            Int64 expressID = StepEngine.x86_64.internalGetP21Line(productDefinitionShapeInstance);
                            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\assembly-" + expressID + ".bin";
                            ProcessGeometry(geometryKernelModel, myCollectionInstance, path);
			            }
		            }
	            }

                StepEngine.x86_64.sdaiCloseModel(stepModel);
            }
        }
    }
}
