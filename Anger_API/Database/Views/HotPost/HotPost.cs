﻿using System;

namespace Anger_API.Database.Views.HotPost
{
    public class HotPost : View
    {
        public int RowNo { get; set; }
        public long MemberID { get; set; }
        public string Subject { get; set; }
        public string CoverUrl { get; set; }
        public DateTime PostAt { get; set; }
        public int ViewCount { get; set; }
    }
}