using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.PreMembers
{
    public class ResendVerifyCodeRquest : APIRequest
    {
        public string PreMemberID { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(PreMemberID, nameof(PreMemberID));
        }
    }
    public class ResendVerifyCodeResponse: ResponseBase
    {

    }
}