using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using Anger_API.Database.Logs;
using static Anger_API.Database.AngerDB;

namespace Anger_API.Library
{
    public class SqlLogger
    {
        public void InsertErrorLog(Log log)
        {
            try
            {
                DBManager.OpenConnection();
                string query = 
                    "INSERT INTO Anger_Log(CreatedAt,Uri,Message, Method) " +
                    "VALUES(@CreatedAt,@Uri,@Message, @Method)";
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
                    cmd.Parameters.AddWithValue("@Method", log.Method);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}