using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database
{
    public class Repository : IRepository
    {
        public void Create(Table table)
        {
            try
            {
                // Ignore fields
                List<string> ignoreFields = new List<string>() { "TableName", "ID" };

                // Common properties
                table.CreatedAt = DateTime.UtcNow;

                // Construct query
                string fieldNames = "";
                string paramNames = "";

                Dictionary<string, object> paramList = new Dictionary<string, object>();

                foreach (PropertyInfo propertyInfo in table.GetType().GetProperties())
                {
                    var name = propertyInfo.Name;
                    if (!ignoreFields.Contains(name))
                    {
                        fieldNames += name + ",";
                        paramNames += "@" + name + ",";

                        string paramName = "@" + name;
                        var value = propertyInfo.GetValue(table);
                        paramList.Add(paramName, value);
                    }
                }

                fieldNames = fieldNames.TrimEnd(',');
                paramNames = paramNames.TrimEnd(',');

                string query =
                    $"INSERT INTO {table.TableName}({fieldNames}) " +
                    $"VALUES({paramNames})";

                DBManager.OpenConnection();
                using (var sqlConnection = DBManager.Conn)
                {
                    var cmd = new SqlCommand(query, sqlConnection)
                        {
                            CommandType = CommandType.Text
                        };
                    foreach(var param in paramList)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
                DBManager.CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public T RetrieveByID<T>(Table table, long ID)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            var obj = db.Query(table.TableName)
                .Where(nameof(ID), ID)
                .Get<T>()
                .FirstOrDefault();
            return obj;
        }
    }
}