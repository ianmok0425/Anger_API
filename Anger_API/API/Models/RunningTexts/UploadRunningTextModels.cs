using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_API.Database.Tests;
using Anger_Library;

namespace Anger_API.API.Models.RunningTexts
{
    public class UploadRunningTextRequest : APIRequest
    {
        public string Content { get; set; }
        public string MemberID { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(Content, nameof(Content));
            APIException.ExRequired(MemberID, nameof(MemberID));
        }
    }
    public class UploadRunningTextResponse : ResponseBase
    {

    }
}