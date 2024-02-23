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
            Console.Out.WriteLine("TODO");
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
