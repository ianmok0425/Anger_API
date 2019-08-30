using System;
using System.ComponentModel.DataAnnotations;

namespace Anger_API.Database.RunningTexts
{
    public class RunningText : Table
    {
        public string Content { get; set; }
        public long MemberID { get; set; }
        public DateTime? PostAt { get; set; }
        public bool? Approved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool? Rejected { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string RejectReason { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}