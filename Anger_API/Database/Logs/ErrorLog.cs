using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Anger_API.Database.Logs
{
    public class ErrorLog : Log
    {
        public override string TableName => "Anger_Logs_ApiError";
        public DateTime CreatedAt { get; set; }
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Message { get; set; }
    }
}