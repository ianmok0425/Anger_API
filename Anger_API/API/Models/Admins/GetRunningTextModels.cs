using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.Admins
{
    public class GetRunningTextRequest : APIRequest
    {
        public int Action { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string CreatedOn { get; set; }
        public ActionType ActionType;
        public override void Validate()
        {
            base.Validate();
            APIException.ExRequired(Account, nameof(Account));
            APIException.ExRequired(Password, nameof(Password));
            if (Action < 1 || Action > (int)Enum.GetValues(typeof(ActionType)).Cast<ActionType>().Max())
                APIException.ExInvalidParams(nameof(Action));
            ActionType = (ActionType)Action;
        }
    }
    public enum ActionType
    {
        All = 1,
        Approved,
        NotApproved,
        Rejected
    }
}