using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.FavPosts
{
    public class AddFavPostRequest : APIRequest
    {
        public string PostID { get; set; }
        public string MemberID { get; set; }
        public long PostIDVal;
        public long MemberIDVal;
        public override void Validate()
        {
            APIException.ExRequiredLong(PostID, nameof(PostID), ref PostIDVal);
            APIException.ExRequiredLong(MemberID, nameof(MemberID), ref MemberIDVal);
        }
    }
    
    public class AmendFavPostResponse : ResponseBase
    {
        public string FavPostID { get; set; }
    }
}