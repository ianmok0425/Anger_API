using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_API.Database.Tests;
using Anger_Library;

namespace Anger_API.API.Models.PreMembers
{
    public class RegPreMemberRequest : APIRequest
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(Name, nameof(Name));
            APIException.ExRequired(Name, nameof(Mobile));
            APIException.ExRequired(Name, nameof(Email));
            APIException.ExRequired(Name, nameof(Account));
            APIException.ExRequired(Name, nameof(Password));
        }
    }

    public class RegPrememberResponse : ResponseBase
    {
    }
}