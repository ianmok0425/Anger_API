using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_API.Database.Logs;

namespace Anger_API.Library
{
    public class MessageLogging
    {
        public void OutgoingMessageAsync(RequestLog log)
        {
            var sqlRequestLogger = new SqlRequestLogger();
            sqlRequestLogger.InsertLog(log);
        }
    }
}