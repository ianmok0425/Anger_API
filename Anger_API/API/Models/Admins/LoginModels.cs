using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_API.Database.Admins;
using Anger_Library;

namespace Anger_API.API.Models.Admins
{
    public class LoginRequest : APIRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(Account, nameof(Account));
            APIException.ExRequired(Password, nameof(Password));
        }
    }
    public class LoginResponse : ResponseBase
    {
        public Admin Admin { get; set; }
    }
}