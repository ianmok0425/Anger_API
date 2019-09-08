using System;

namespace Anger_API.Database.Posts
{
    public class Post  : Table
    {
        public long MemberID { get; set; }
        public string Subject { get; set; }
        public string CoverUrl { get; set; }
        public string Content { get; set; }
        public int? ViewCount { get; set; }
        public DateTime? PostAt { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool? EmailNotice { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}