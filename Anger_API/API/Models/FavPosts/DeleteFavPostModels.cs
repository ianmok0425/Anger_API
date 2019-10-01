using Anger_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.API.Models.FavPosts
{
    public class DeleteFavPostRequest : APIRequest
    {
        public string FavPostID { get; set; }
        public override void Validate()
        {
            APIException.ExRequired(FavPostID, nameof(FavPostID));
        }
    }
    public class DeleteFavPostResponse : ResponseBase
    {
    }
}