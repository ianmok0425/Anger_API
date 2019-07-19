using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

using Newtonsoft.Json.Linq;
using Anger_API.Database.Logs;
using Newtonsoft.Json;

namespace Anger_API.Library
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            JObject msgObj = new JObject();
            DateTime Now = DateTime.UtcNow;
            var ex = context.Exception;

            msgObj.Add(new JProperty("Log Time : ", Now.ToString()));
            msgObj.Add(new JProperty("Exception Message : ", ex.Message));
            msgObj.Add(new JProperty("Source : ", ex.Source));
            msgObj.Add(new JProperty("StackTrace : ", ex.StackTrace));
            msgObj.Add(new JProperty("TargetSite : ", ex.TargetSite.ToString()));

            if (ex.InnerException != null)
            {
                msgObj.Add(new JProperty("Inner Exception : ", ex.InnerException));
            }
            if (ex.HelpLink != null)
            {
                msgObj.Add(new JProperty("HelpLink : ", ex.HelpLink));
            }

            var requestedURi = (string)context.Request.RequestUri.AbsoluteUri;
            var requestMethod = context.Request.Method.ToString();
            var timeUtc = DateTime.Now;

            SqlErrorLogger sqlErrorLogging = new SqlErrorLogger();
            ErrorLog log = new ErrorLog()
            {
                Message = JsonConvert.SerializeObject(msgObj, Formatting.Indented),
                Uri = requestedURi,
                Method = requestMethod,
                CreatedAt = Now
            };
            sqlErrorLogging.InsertErrorLog(log);
        }
    }
}