using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.Logs
{
    public interface ILogRepository 
    {
        void WriteLog();
    }
}