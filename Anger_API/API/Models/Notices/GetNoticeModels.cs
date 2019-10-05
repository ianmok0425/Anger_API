using System.Collections.Generic;
using Anger_API.Database.Notices;
using Anger_Library;

namespace Anger_API.API.Models.Notices
{
    public class GetNoticeRequest : APIRequest
    {
    }
    public class GetNoticeResponse : ResponseBase
    {
        public List<Notice> Notices { get; set; }
    }
}