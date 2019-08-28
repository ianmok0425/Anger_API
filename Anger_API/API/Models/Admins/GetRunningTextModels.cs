using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.Admins
{
    public class GetRunningTextRequest : APIRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string CreatedOn { get; set; }
        public override void Validate()
        {
            base.Validate();
            APIException.ExRequired(Account, nameof(Account));
            APIException.ExRequired(Password, nameof(Password));
        }
    }
}