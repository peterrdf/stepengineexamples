using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using static Android.Content.ClipData;


namespace IfcViewer_Xamarin_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            Console.Out.WriteLine("*** FabOnClick");

            var strDocumentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Console.Out.WriteLine("*** " + strDocumentsPath);

            var ifcContent = LoadResource("myFile.ifc");

            string strIfcFileFullPath = Path.Combine(strDocumentsPath, "myFile.ifc");
            Console.Out.WriteLine("*** " + strIfcFileFullPath);

            File.WriteAllText(strIfcFileFullPath, ifcContent);

            long model = IfcEngine.x86_64.sdaiOpenModelBN(0, strIfcFileFullPath, (string)null);
            Console.Out.WriteLine("sdaiOpenModelBN(): " + model);

            IfcEngine.x86_64.GetSPFFHeaderItem(model, 9, 0, IfcEngine.x86_64.sdaiSTRING, out IntPtr schema);

            string strSchema = Marshal.PtrToStringAuto(schema);
            Console.Out.WriteLine("Schema: " + strSchema);

            IfcEngine.x86_64.sdaiCloseModel(model);

            /*
             * Embedded Schema
             */
            if (strSchema.IndexOf("IFC2") != -1)
            {
                IfcEngine.x86_64.setFilter(0, 2097152, 1048576 + 2097152 + 4194304);
            }
            else
            {
                if ((strSchema.IndexOf("IFC4x") != -1) || (strSchema.IndexOf("IFC4X") != -1))
                {
                    IfcEngine.x86_64.setFilter(0, 1048576 + 2097152, 1048576 + 2097152 + 4194304);
                }
                else
                {
                    if ((strSchema.IndexOf("IFC4") != -1) ||
                        (strSchema.IndexOf("IFC2x4") != -1) ||
                        (strSchema.IndexOf("IFC2X4") != -1))
                    {
                        IfcEngine.x86_64.setFilter(0, 1048576, 1048576 + 2097152 + 4194304);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            model = IfcEngine.x86_64.sdaiOpenModelBN(0, strIfcFileFullPath, "");
            Console.Out.WriteLine("sdaiOpenModelBN(): " + model);

            LoadModel(model);

            IfcEngine.x86_64.sdaiCloseModel(model);
        }

        private void LoadModel(long model)
        {
            long lVertexElementSizeInBytes = SetFormat(model);
            Console.Out.WriteLine("SetFormat(): " + lVertexElementSizeInBytes);
        }

        private long SetFormat(long lModel)
        {
            long setting = 0, mask = 0;

            mask += IfcEngine.x86_64.flagbit2;           //    PRECISION (32/64 bit)
            mask += IfcEngine.x86_64.flagbit3;           //     INDEX ARRAY (32/64 bit)
            mask += IfcEngine.x86_64.flagbit5;           //    NORMALS
            mask += IfcEngine.x86_64.flagbit8;           //    TRIANGLES
            mask += IfcEngine.x86_64.flagbit9;           //    LINES
            mask += IfcEngine.x86_64.flagbit10;          //    POINTS
            mask += IfcEngine.x86_64.flagbit12;          //    WIREFRAME
            mask += IfcEngine.x86_64.flagbit24;          // AMBIENT
            mask += IfcEngine.x86_64.flagbit25;          // DIFFUSE
            mask += IfcEngine.x86_64.flagbit26;          // EMISSIVE
            mask += IfcEngine.x86_64.flagbit27;          //     SPECULAR*/

            setting += 0 * IfcEngine.x86_64.flagbit2;    //    SINGLE PRECISION (float)
            setting += 0;                                //    32 BIT INDEX ARRAY (Int32)
            setting += 1 * IfcEngine.x86_64.flagbit5;    //    NORMALS
            setting += IfcEngine.x86_64.flagbit8;        //    TRIANGLES ON
            setting += 1 * IfcEngine.x86_64.flagbit9;    //    LINES ON
            setting += 0 * IfcEngine.x86_64.flagbit10;   //    POINTS ON
            setting += 1 * IfcEngine.x86_64.flagbit12;   //    WIREFRAME ON
            setting += 1 * IfcEngine.x86_64.flagbit24;   //  AMBIENT
            setting += 0 * IfcEngine.x86_64.flagbit25;   //  DIFFUSE
            setting += 0 * IfcEngine.x86_64.flagbit26;   //  EMISSIVE
            setting += 0 * IfcEngine.x86_64.flagbit27;   //  SPECULAR

            return IfcEngine.x86_64.SetFormat(lModel, setting, mask);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private string LoadResource(string name)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(name, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var content = reader.ReadToEnd();

                    return content;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.GetBaseException().Message);

                throw ex;
            }
        }
    }
}
