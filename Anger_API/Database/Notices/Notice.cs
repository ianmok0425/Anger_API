using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.Notices
{
    public class Notice : Table
    {
        public string Content { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public long CreatedBy { get; set; }
    }
}