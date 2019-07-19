using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_API.Database.Logs;

namespace Anger_API.Library
{
    public class MessageLogging
    {
        public void IncomingMessageAsync(RequestLog log)
        {
            log.Type = "Request";
            var sqlRequestLogger = new SqlRequestLogger();
            sqlRequestLogger.InsertLog(log);
        }

        public void OutgoingMessageAsync(RequestLog log)
        {
            log.Type = "Response";
            var sqlRequestLogger = new SqlRequestLogger();
            sqlRequestLogger.InsertLog(log);
        }
    }
}