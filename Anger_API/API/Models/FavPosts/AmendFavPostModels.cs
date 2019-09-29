using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.FavPosts
{
    public class AmendFavPostRequest : APIRequest
    {
        public string Action { get; set; }
        public string ID { get; set; }
        public string PostID { get; set; }
        public string MemberID { get; set; }
        public int ActionVal;
        public long IDVal;
        public long PostIDVal;
        public long MemberIDVal;
        public override void Validate()
        {
            APIException.ExRequiredInt(Action, nameof(Action), ref ActionVal);
            if (ActionVal < 1 || ActionVal > 2) APIException.ExInvalidParams(nameof(Action));

            if (ActionVal == 1)
            {
                APIException.ExRequiredLong(PostID, nameof(PostID), ref PostIDVal);
                APIException.ExRequiredLong(MemberID, nameof(MemberID), ref MemberIDVal);
            }
            else if(ActionVal == 2)
            {
                APIException.ExRequiredLong(ID, nameof(ID), ref IDVal);
            }
        }
    }
    
    public class AmendFavPostResponse : ResponseBase
    {
        public string FavPostID { get; set; }
    }
}