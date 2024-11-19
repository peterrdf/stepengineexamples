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
            engine.SaveInstanceTreeW(myInstance, System.Text.Encoding.Unicode.GetBytes(filename));
        }

        private void buttonFind3DModel_Click(object sender, EventArgs e)
        {
            int_t stepModel = stepengine.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                stepengine.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                stepengine.owlGetModel(stepModel, out geometryKernelModel);

                int_t productDefinitionInstances = stepengine.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION"),
                      noProductDefinitionInstances = stepengine.sdaiGetMemberCount(productDefinitionInstances);
                if (noProductDefinitionInstances != 0)
                {
                    for (int_t i = 0; i < noProductDefinitionInstances; i++)
                    {
                        int_t productDefinitionInstance = 0;
                        stepengine.engiGetAggrElement(productDefinitionInstances, i, stepengine.sdaiINSTANCE, out productDefinitionInstance);

                        Int64 myInstance = 0;
                        stepengine.owlBuildInstance(stepModel, productDefinitionInstance, out myInstance);

                        if (myInstance != 0)
                        {
                            //
                            //  Check if the tree contains real geometry
                            //
                            Int64 vertexArraySize = 0, indexArraySize = 0;
                            engine.CalculateInstance(myInstance, out vertexArraySize, out indexArraySize, (IntPtr)0);

                            if (vertexArraySize != 0 && indexArraySize != 0)
                            {
                                Int64 expressID = stepengine.internalGetP21Line(productDefinitionInstance);
                                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\geom-" + expressID + ".bin";
                                ProcessGeometry(geometryKernelModel, myInstance, path);
                            }
                        }
                    }
                }

                stepengine.sdaiCloseModel(stepModel);
            }
        }

        private void buttonFind3DParts_Click(object sender, EventArgs e)
        {
            int_t stepModel = stepengine.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                stepengine.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                stepengine.owlGetModel(stepModel, out geometryKernelModel);

                int_t productDefinitionShapeInstances = stepengine.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
                      noProductDefinitionShapeInstances = stepengine.sdaiGetMemberCount(productDefinitionShapeInstances);
                if (noProductDefinitionShapeInstances != 0)
                {
                    for (int_t i = 0; i < noProductDefinitionShapeInstances; i++)
                    {
                        int_t productDefinitionShapeInstance = 0;
                        stepengine.engiGetAggrElement(productDefinitionShapeInstances, i, stepengine.sdaiINSTANCE, out productDefinitionShapeInstance);

                        Int64 myInstance = 0;
                        stepengine.owlBuildInstance(stepModel, productDefinitionShapeInstance, out myInstance);

                        if (myInstance != 0)
                        {
                            //
                            //  Check if the tree contains real geometry
                            //
                            Int64 vertexArraySize = 0, indexArraySize = 0;
                            engine.CalculateInstance(myInstance, out vertexArraySize, out indexArraySize, (IntPtr)0);

                            if (vertexArraySize != 0 && indexArraySize != 0)
                            {
                                Int64 expressID = stepengine.internalGetP21Line(productDefinitionShapeInstance);
                                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\geom-" + expressID + ".bin";
                                ProcessGeometry(geometryKernelModel, myInstance, path);
                            }
                        }
                    }
                }

                stepengine.sdaiCloseModel(stepModel);
            }
        }

        private void buttonFindAssemblies_Click(object sender, EventArgs e)
        {
            int_t stepModel = stepengine.sdaiOpenModelBNUnicode(0, System.Text.Encoding.Unicode.GetBytes(textBoxContent.Text), System.Text.Encoding.Unicode.GetBytes(""));
            if (stepModel != 0)
            {
                stepengine.setFilter(stepModel, 268435456, 268435456);

                Int64 geometryKernelModel = 0;  //  => static within one stepModel (in case multi-threading within one stepModel is not used)
                stepengine.owlGetModel(stepModel, out geometryKernelModel);

	            long nextAssemblyUsageOccurrenceEntity = stepengine.sdaiGetEntity(stepModel, "NEXT_ASSEMBLY_USAGE_OCCURRENCE");

                int_t productDefinitionShapeInstances = stepengine.sdaiGetEntityExtentBN(stepModel, "PRODUCT_DEFINITION_SHAPE"),
			          noProductDefinitionShapeInstances = stepengine.sdaiGetMemberCount(productDefinitionShapeInstances);
	            if (noProductDefinitionShapeInstances != 0)
                {
		            for (int_t i = 0; i < noProductDefinitionShapeInstances; i++)
                    {
			            int_t productDefinitionShapeInstance = 0;
                        stepengine.engiGetAggrElement(productDefinitionShapeInstances, i, stepengine.sdaiINSTANCE, out productDefinitionShapeInstance);

			            Int64 myGeometryInstance = 0;
                        stepengine.owlBuildInstance(stepModel, productDefinitionShapeInstance, out myGeometryInstance);

			            int_t definitionInstance = 0;
                        stepengine.sdaiGetAttrBN(productDefinitionShapeInstance, "definition", stepengine.sdaiINSTANCE, out definitionInstance);
			            if (stepengine.sdaiGetInstanceType(definitionInstance) == nextAssemblyUsageOccurrenceEntity) {
				            int_t relatingProductDefinitionInstance = 0;
                            stepengine.sdaiGetAttrBN(definitionInstance, "relating_product_definition", stepengine.sdaiINSTANCE, out relatingProductDefinitionInstance);
                            Int64 myRelatingProductInstanceExpressID = stepengine.internalGetP21Line(relatingProductDefinitionInstance);

                            int_t relatedProductDefinitionInstance = 0;
                            stepengine.sdaiGetAttrBN(definitionInstance, "related_product_definition", stepengine.sdaiINSTANCE, out relatedProductDefinitionInstance);
                            Int64 myRelatedProductInstanceExpressID = stepengine.internalGetP21Line(relatedProductDefinitionInstance);

                            Int64 propertyParentInstance = engine.CreateProperty(geometryKernelModel, engine.OBJECTPROPERTY_TYPE, "parentInstance"),
                                  propertyRelatingProduct = engine.CreateProperty(geometryKernelModel, engine.DATATYPEPROPERTY_TYPE_INTEGER, "relatingProduct"),
                                  propertyRelatedProduct = engine.CreateProperty(geometryKernelModel, engine.DATATYPEPROPERTY_TYPE_INTEGER, "relatedProduct");

                            Int64 myCollectionInstance = engine.CreateInstance(engine.CreateClass(geometryKernelModel, "MyClass"), (string) null);

                            engine.SetObjectProperty(myCollectionInstance, propertyParentInstance, ref myGeometryInstance, 1);
                            engine.SetDatatypeProperty(myCollectionInstance, propertyRelatingProduct, ref myRelatingProductInstanceExpressID, 1);
                            engine.SetDatatypeProperty(myCollectionInstance, propertyRelatedProduct, ref myRelatedProductInstanceExpressID, 1);

                            Int64 expressID = stepengine.internalGetP21Line(productDefinitionShapeInstance);
                            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\assembly-" + expressID + ".bin";
                            ProcessGeometry(geometryKernelModel, myCollectionInstance, path);
			            }
		            }
	            }

                stepengine.sdaiCloseModel(stepModel);
            }
        }
    }
}
