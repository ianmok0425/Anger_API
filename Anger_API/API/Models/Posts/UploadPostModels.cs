using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.Posts
{
    public class UploadPostRequest : APIRequest
    {
        public long AdminID { get; set; }
        public string Subject { get; set; }
        public long MemberID { get; set; }
        public bool Approve { get; set; }
        public DateTime PostAt { get; set; }
        public string RejectReason { get; set; }
        public string CoverBase64 { get; set; }
        public string Content { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(AdminID.ToString(), nameof(AdminID));
            APIException.ExRequired(Subject, nameof(Subject));
            APIException.ExRequired(MemberID.ToString(), nameof(MemberID));
            APIException.ExRequired(CoverBase64, nameof(CoverBase64));
            CoverBase64 = CoverBase64.Split(',')[1].Trim();
        }
    }
    public class UploadPostResponse : ResponseBase
    {
    }
}