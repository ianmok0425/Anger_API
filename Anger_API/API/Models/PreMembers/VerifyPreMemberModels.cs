using Anger_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.API.Models.PreMembers
{
    public class VerifyPreMemberRequest : APIRequest
    {
        public string PreMemberID { get; set; }
        public string VerifyCode { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(PreMemberID, nameof(PreMemberID));
            APIException.ExRequired(VerifyCode, nameof(VerifyCode));
        }
    }
    public class VerifyPreMemberReponse : ResponseBase
    {
    }
}