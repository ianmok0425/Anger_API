using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Logs
{
    public class LogRepository : ILogRepository
    {
        public void WriteLog()
        {
            var conn = DBManager.Conn;
        }
    }
}