using System;
using System.Data;
using System.Data.SqlClient;

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
                    $"INSERT INTO {log.TableName}(Host, Headers, StatusCode, RequestBody, ResponseBody, Method, UserHostAddress, UserAgent, AbsoluteUri, CreatedAt) " +
                    "VALUES(@Host, @Headers, @StatusCode, @RequestBody, @ResponseBody, @Method, @UserHostAddress, @UserAgent, @AbsoluteUri, @CreatedAt)";
                    var cmd =
                        new SqlCommand(query, connection: sqlConnection)
                        {
                            CommandType = CommandType.Text
                        };
                    cmd.Parameters.AddWithValue("@Host", log.Host);
                    cmd.Parameters.AddWithValue("@Headers", log.Headers);
                    cmd.Parameters.AddWithValue("@StatusCode", log.StatusCode);
                    cmd.Parameters.AddWithValue("@RequestBody", log.RequestBody);
                    cmd.Parameters.AddWithValue("@ResponseBody", log.ResponseBody);
                    cmd.Parameters.AddWithValue("@Method", log.Method);
                    cmd.Parameters.AddWithValue("@UserHostAddress", log.UserHostAddress);
                    cmd.Parameters.AddWithValue("@Useragent", log.UserAgent);
                    cmd.Parameters.AddWithValue("@AbsoluteUri", log.AbsoluteUri);
                    cmd.Parameters.AddWithValue("@CreatedAt", log.CreatedAt);
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