using IfcEngine;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IFCViewerSGL
{
    public class PostgreSQL
    {
        #region Members

        private string _strConnectionString;

        #endregion // Members

        #region Methods

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="strConnectionString"></param>
        public PostgreSQL(string strConnectionString)
        {
            _strConnectionString = strConnectionString;

            using (var connection = new NpgsqlConnection(_strConnectionString))
            {
                connection.Open();

                var sql = "SELECT version()";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    var version = command.ExecuteScalar().ToString();

                    Console.WriteLine($"PostgreSQL version: {version}");
                }                
            }
        }

        /// <summary>
        /// Publish
        /// </summary>
        /// <param name="ifcModel"></param>
        public void Publish(IFCModel ifcModel)
        {
            using (var connection = new NpgsqlConnection(_strConnectionString))
            {
                connection.Open();

                var sql = "INSERT INTO models(name) VALUES(@name)";

                using (var command = new NpgsqlCommand(sql, connection))
                {                  
                    /*
                     * Model
                     */ 
                    command.Parameters.AddWithValue("name", NpgsqlDbType.Text, ifcModel.IFCFile);
                    command.Prepare();

                    command.ExecuteNonQuery();

                    /*
                     * Get Model ID
                     */
                    sql = "SELECT currval('models_id_seq');";

                    using (var command2 = new NpgsqlCommand(sql, connection))
                    {
                        var modelID = command2.ExecuteScalar().ToString();

                        Console.WriteLine($"Model ID: {modelID}");

                        /*
                        * Instances
                        */
                        PublishInstances(connection, int.Parse(modelID), ifcModel);
                    }                    
                }
            }            
        }

        private void PublishInstances(NpgsqlConnection connection, int modelID, IFCModel ifcModel)
        {
            foreach (var ifcItem in ifcModel.Items)
            {
                var sql = "INSERT INTO instances(model_id, name, description, global_id, vertices, indices) VALUES(@model_id, @name, @description, @global_id, @vertices, @indices)";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    /*
                     * model_id
                     */
                    command.Parameters.AddWithValue("model_id", NpgsqlDbType.Integer, modelID);

                    /*
                     * name
                     */
                    IntPtr name;
                    IfcEngineAnyCPU.sdaiGetAttrBN(ifcItem.Value._instance, "Name", IfcEngineAnyCPU.sdaiUNICODE, out name);

                    string strName = Marshal.PtrToStringUni(name);
                    command.Parameters.AddWithValue("name", NpgsqlDbType.Text, strName);

                    /*
                     * description
                     */
                    IntPtr description;
                    IfcEngineAnyCPU.sdaiGetAttrBN(ifcItem.Value._instance, "Description", IfcEngineAnyCPU.sdaiUNICODE, out description);

                    string strDescription = Marshal.PtrToStringUni(description);
                    command.Parameters.AddWithValue("description", NpgsqlDbType.Text, !string.IsNullOrEmpty(strDescription) ? strDescription : "" );

                    /*
                     * global_id
                     */
                    IntPtr globalID;
                    IfcEngineAnyCPU.sdaiGetAttrBN(ifcItem.Value._instance, "GlobalID", IfcEngineAnyCPU.sdaiUNICODE, out globalID);

                    string strGlobalID = Marshal.PtrToStringUni(globalID);
                    command.Parameters.AddWithValue("global_id", NpgsqlDbType.Text, strGlobalID);

                    /*
                     * vertices
                     */
                    var vertices = string.Empty;
                    if (ifcItem.Value._vertices != null)
                    {
                        var byteArray = new byte[ifcItem.Value._vertices.Length * sizeof(float)];
                        Buffer.BlockCopy(ifcItem.Value._vertices, 0, byteArray, 0, byteArray.Length);

                        vertices = Convert.ToBase64String(byteArray);
                    }
                    command.Parameters.AddWithValue("vertices", NpgsqlDbType.Text, vertices);

                    /*
                     * indices
                     */
                    var indices = string.Empty;
                    if (ifcItem.Value._facesIndices != null)
                    {
                        var byteArray = new byte[ifcItem.Value._facesIndices.Length * sizeof(int)];
                        Buffer.BlockCopy(ifcItem.Value._facesIndices, 0, byteArray, 0, byteArray.Length);

                        indices = Convert.ToBase64String(byteArray);
                    }
                    command.Parameters.AddWithValue("indices", NpgsqlDbType.Text, indices);

                    command.Prepare();

                    command.ExecuteNonQuery();
                } // foreach (var ifcItem in ...
            }
        }

        #endregion // Methods
    }
}
