using System;
using System.Data;
using System.Data.SqlClient;

using Anger_API.Database.Logs;
using static Anger_API.Database.AngerDB;

namespace Anger_API.Library
{
    public class SqlErrorLogger
    {
        public void InsertErrorLog(ErrorLog log)
        {
            try
            {
                DBManager.OpenConnection();
                string query =
                    $"INSERT INTO {log.TableName}(CreatedAt,Uri,Message, Method, RequestBody) " +
                    "VALUES(@CreatedAt,@Uri,@Message, @Method, @RequestBody)";
                using (var sqlConnection = DBManager.Conn)
                {
                    var cmd =
                        new SqlCommand(query, sqlConnection)
                        {
                            CommandType = CommandType.Text
                        };
                    cmd.Parameters.AddWithValue("@CreatedAt", log.CreatedAt);
                    cmd.Parameters.AddWithValue("@Uri", log.Uri);
                    cmd.Parameters.AddWithValue("@Message", log.Message);
                    cmd.Parameters.AddWithValue("@RequestBody", log.RequestBody);
                    cmd.Parameters.AddWithValue("@Method", log.Method);
                    cmd.ExecuteNonQuery();
                }
                DBManager.CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}