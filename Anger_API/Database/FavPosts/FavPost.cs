using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.FavPosts
{
    public class FavPost : Table
    {
        public long PostID { get; set; }
        public long MemberID { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}