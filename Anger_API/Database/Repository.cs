using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database
{
    public class Repository : IRepository
    {
        public virtual string TableName { get; set; }
        public async Task Create(Table table)
        {
            try
            {
                // Ignore fields
                List<string> ignoreFields = new List<string>() { "ID" };

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
                    $"INSERT INTO {TableName}({fieldNames}) " +
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
                    await cmd.ExecuteNonQueryAsync();
                }
                DBManager.CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public async Task<T> RetrieveByID<T>(long ID)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            var objs = await db.Query(TableName)
                .Where(nameof(ID), ID)
                .GetAsync<T>();
            return objs.FirstOrDefault();
        }
    }
}