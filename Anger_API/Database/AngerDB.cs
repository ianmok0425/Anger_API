using System.Data.SqlClient;
using static Anger_API.Library.Anger;

namespace Anger_API.Database
{
    public static class AngerDB
    {
        public static class DBManager
        {
            public static SqlConnection Conn;
            public static void OpenConnection()
            {
                Config cfg = GetConfig();
                string connectionString = cfg.ConnectionString;

                Conn = new SqlConnection(connectionString);
                Conn.Open();
            }

            public static void CloseConnection()
            {
                Conn?.Close();
                Conn = null;
            }
        }
    }

    public static class Operator
    {
        public static string Greater => ">";
        public static string GreaterEqual => ">=";
        public static string Equal => "=";
        public static string Less => "<";
        public static string LessEqual => "<=";
    }
}