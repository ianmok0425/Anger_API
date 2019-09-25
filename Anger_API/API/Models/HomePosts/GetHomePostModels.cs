using System.Collections.Generic;

using Anger_Library;
using Anger_API.Database.Views.HomePost;

namespace Anger_API.API.Models.HomePosts
{
    public class GetHomePostRequest : APIRequest
    {
        public int StartRowNo { get; set; }
    }
    public class GetHomePostResponse : ResponseBase
    {
        public List<HomePost> HomePosts { get; set; }
    }
}