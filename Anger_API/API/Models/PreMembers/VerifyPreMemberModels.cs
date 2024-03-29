﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_API.Database.Members;
using Anger_Library;

namespace Anger_API.API.Models.PreMembers
{
    public class VerifyPreMemberRequest : APIRequest
    {
        public string PreMemberID { get; set; }
        public string Code { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(PreMemberID, nameof(PreMemberID));
            APIException.ExRequired(Code, nameof(Code));
        }
    }
    public class VerifyPreMemberReponse : ResponseBase
    {
        public string MemberID { get; set; }
        public Member Member { get; set; }
    }
}