using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Library
{
    public static class Utility
    {
        public static DateTime ToHKTime(this DateTime dt)
        {
            return dt.AddHours(8);
        }
    }
}