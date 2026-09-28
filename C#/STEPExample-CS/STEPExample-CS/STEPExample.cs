using RDF;
using System;
using System.Windows.Forms;
#if _WIN64
using int_t = System.Int64;
#else
    using int_t = System.Int32;
#endif

namespace STEPExample
{
    public partial class STEPExample : Form
    {
        public STEPExample()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxContent.Text = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\data\\STEPExample-CS_as1-oc-214.stp";
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
            Engine.SaveInstanceTreeW(myInstance, System.Text.Encoding.Unicode.GetBytes(filename));
        }

        private void buttonFind3DModel_Click(object sender, EventArgs e)
        {
            int_t stepModel = STEPEngine.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                STEPEngine.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                STEPEngine.owlGetModel(stepModel, out geometryKernelModel);

                int_t productDefinitionInstances = STEPEngine.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION"),
                      noProductDefinitionInstances = STEPEngine.sdaiGetMemberCount(productDefinitionInstances);
                if (noProductDefinitionInstances != 0)
                {
                    for (int_t i = 0; i < noProductDefinitionInstances; i++)
                    {
                        int_t productDefinitionInstance = 0;
                        STEPEngine.engiGetAggrElement(productDefinitionInstances, i, STEPEngine.sdaiINSTANCE, out productDefinitionInstance);

                        Int64 myInstance = 0;
                        STEPEngine.owlBuildInstance(stepModel, productDefinitionInstance, out myInstance);

                        if (myInstance != 0)
                        {
                            //
                            //  Check if the tree contains real geometry
                            //
                            Int64 vertexArraySize = 0, indexArraySize = 0;
                            Engine.CalculateInstance(myInstance, out vertexArraySize, out indexArraySize, (IntPtr)0);

                            if (vertexArraySize != 0 && indexArraySize != 0)
                            {
                                Int64 expressID = STEPEngine.internalGetP21Line(productDefinitionInstance);
                                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\geom-" + expressID + ".bin";
                                ProcessGeometry(geometryKernelModel, myInstance, path);
                            }
                        }
                    }
                }

                STEPEngine.sdaiCloseModel(stepModel);
            }
        }

        private void buttonFind3DParts_Click(object sender, EventArgs e)
        {
            int_t stepModel = STEPEngine.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                STEPEngine.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                STEPEngine.owlGetModel(stepModel, out geometryKernelModel);

                int_t productDefinitionShapeInstances = STEPEngine.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
                      noProductDefinitionShapeInstances = STEPEngine.sdaiGetMemberCount(productDefinitionShapeInstances);
                if (noProductDefinitionShapeInstances != 0)
                {
                    for (int_t i = 0; i < noProductDefinitionShapeInstances; i++)
                    {
                        int_t productDefinitionShapeInstance = 0;
                        STEPEngine.engiGetAggrElement(productDefinitionShapeInstances, i, STEPEngine.sdaiINSTANCE, out productDefinitionShapeInstance);

                        Int64 myInstance = 0;
                        STEPEngine.owlBuildInstance(stepModel, productDefinitionShapeInstance, out myInstance);

                        if (myInstance != 0)
                        {
                            //
                            //  Check if the tree contains real geometry
                            //
                            Int64 vertexArraySize = 0, indexArraySize = 0;
                            Engine.CalculateInstance(myInstance, out vertexArraySize, out indexArraySize, (IntPtr)0);

                            if (vertexArraySize != 0 && indexArraySize != 0)
                            {
                                Int64 expressID = STEPEngine.internalGetP21Line(productDefinitionShapeInstance);
                                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\geom-" + expressID + ".bin";
                                ProcessGeometry(geometryKernelModel, myInstance, path);
                            }
                        }
                    }
                }

                STEPEngine.sdaiCloseModel(stepModel);
            }
        }

        private void buttonFindAssemblies_Click(object sender, EventArgs e)
        {
            int_t stepModel = STEPEngine.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                STEPEngine.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                STEPEngine.owlGetModel(stepModel, out geometryKernelModel);

	            long nextAssemblyUsageOccurrenceEntity = STEPEngine.sdaiGetEntity(stepModel, "NEXT_ASSEMBLY_USAGE_OCCURRENCE");

                int_t productDefinitionShapeInstances = STEPEngine.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
			          noProductDefinitionShapeInstances = STEPEngine.sdaiGetMemberCount(productDefinitionShapeInstances);
	            if (noProductDefinitionShapeInstances != 0)
                {
		            for (int_t i = 0; i < noProductDefinitionShapeInstances; i++)
                    {
			            int_t productDefinitionShapeInstance = 0;
                        STEPEngine.engiGetAggrElement(productDefinitionShapeInstances, i, STEPEngine.sdaiINSTANCE, out productDefinitionShapeInstance);

			            Int64 myGeometryInstance = 0;
                        STEPEngine.owlBuildInstance(stepModel, productDefinitionShapeInstance, out myGeometryInstance);

			            int_t definitionInstance = 0;
                        STEPEngine.sdaiGetAttrBN(productDefinitionShapeInstance, "definition", STEPEngine.sdaiINSTANCE, out definitionInstance);
			            if (STEPEngine.sdaiGetInstanceType(definitionInstance) == nextAssemblyUsageOccurrenceEntity) {
				            int_t relatingProductDefinitionInstance = 0;
                            STEPEngine.sdaiGetAttrBN(definitionInstance, "relating_product_definition", STEPEngine.sdaiINSTANCE, out relatingProductDefinitionInstance);
                            Int64 myRelatingProductInstanceExpressID = STEPEngine.internalGetP21Line(relatingProductDefinitionInstance);

                            int_t relatedProductDefinitionInstance = 0;
                            STEPEngine.sdaiGetAttrBN(definitionInstance, "related_product_definition", STEPEngine.sdaiINSTANCE, out relatedProductDefinitionInstance);
                            Int64 myRelatedProductInstanceExpressID = STEPEngine.internalGetP21Line(relatedProductDefinitionInstance);

                            Int64 propertyParentInstance = Engine.CreateProperty(geometryKernelModel, Engine.OBJECTPROPERTY_TYPE, "parentInstance"),
                                  propertyRelatingProduct = Engine.CreateProperty(geometryKernelModel, Engine.DATATYPEPROPERTY_TYPE_INTEGER, "relatingProduct"),
                                  propertyRelatedProduct = Engine.CreateProperty(geometryKernelModel, Engine.DATATYPEPROPERTY_TYPE_INTEGER, "relatedProduct");

                            Int64 myCollectionInstance = Engine.CreateInstance(Engine.CreateClass(geometryKernelModel, "MyClass"), (string) null);

                            Engine.SetObjectProperty(myCollectionInstance, propertyParentInstance, ref myGeometryInstance, 1);
                            Engine.SetDatatypeProperty(myCollectionInstance, propertyRelatingProduct, ref myRelatingProductInstanceExpressID, 1);
                            Engine.SetDatatypeProperty(myCollectionInstance, propertyRelatedProduct, ref myRelatedProductInstanceExpressID, 1);

                            Int64 expressID = STEPEngine.internalGetP21Line(productDefinitionShapeInstance);
                            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\assembly-" + expressID + ".bin";
                            ProcessGeometry(geometryKernelModel, myCollectionInstance, path);
			            }
		            }
	            }

                STEPEngine.sdaiCloseModel(stepModel);
            }
        }
    }
}
