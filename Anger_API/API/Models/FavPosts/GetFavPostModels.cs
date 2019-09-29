using System.Collections.Generic;

using Anger_Library;
using Anger_API.Database.Views.FavPost;

namespace Anger_API.API.Models.FavPosts
{
    public class GetFavPostRequest : APIRequest
    {
        public long MemberID { get; set; }
        public int StartRowNo { get; set; }
    }
    public class GetFavPostResponse : ResponseBase
    {
        public List<FavPost> FavPosts { get; set; }
    }
}