using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.Views.HomePost
{
    public class HomePost : View
    {
        public int RowNo { get; set; }
        public long MemberID { get; set; }
        public string Subject { get; set; }
        public string CoverUrl { get; set; }
        public DateTime PostAt { get; set; }
        public int ViewCount { get; set; }
    }
}