using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.Logs
{
    public class RequestLog : Log
    {
        public override string TableName => "Anger_Logs_ApiRequest";
        public string Host { get; set; }
        public string Headers { get; set; }
        public string StatusCode { get; set; }
        public string Body { get; set; }
        public string Method { get; set; }
        public string UserHostAddress { get; set; }
        public string UserAgent { get; set; }
        public string AbsoluteUri { get; set; }
        public string Type { get; set; }
    }
}