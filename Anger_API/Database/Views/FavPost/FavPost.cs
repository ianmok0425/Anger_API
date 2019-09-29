using System;

namespace Anger_API.Database.Views.FavPost
{
    public class FavPost : View
    {
        public long PostID { get; set; }
        public long MemberID { get; set; }
        public string Subject { get; set; }
        public string CoverUrl { get; set; }
        public DateTime PostAt { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}