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
    public class SqlRequestLogger
    {
        public void InsertLog(RequestLog log)
        {
            try
            {
                DBManager.OpenConnection();
                using (var sqlConnection = DBManager.Conn)
                {
                    string query =
                    $"INSERT INTO {log.TableName}(Host, Headers, StatusCode, Body, Method, UserHostAddress, UserAgent, AbsoluteUri, Type) " +
                    "VALUES(@Host, @Headers, @StatusCode, @Body, @Method, @UserHostAddress, @UserAgent, @AbsoluteUri, @Type)";
                    var cmd =
                        new SqlCommand(query, connection: sqlConnection)
                        {
                            CommandType = CommandType.Text
                        };
                    cmd.Parameters.AddWithValue("@Host", log.Host);
                    cmd.Parameters.AddWithValue("@Headers", log.Headers);
                    cmd.Parameters.AddWithValue("@StatusCode", log.StatusCode);
                    cmd.Parameters.AddWithValue("@Body", log.Body);
                    cmd.Parameters.AddWithValue("@Method", log.Method);
                    cmd.Parameters.AddWithValue("@UserHostAddress", log.UserHostAddress);
                    cmd.Parameters.AddWithValue("@Useragent", log.UserAgent);
                    cmd.Parameters.AddWithValue("@AbsoluteUri", log.AbsoluteUri);
                    cmd.Parameters.AddWithValue("@Type", log.Type);
                    cmd.ExecuteNonQuery();
                }
                DBManager.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}