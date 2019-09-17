using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;
using Anger_API.Database.Views.HomePost;

namespace Anger_API.API.Models.Posts
{
    public class GetHomePostRequest : APIRequest
    {
        public int StartRowNo { get; set; }
        public int EndRowNo { get; set; }
    }
    public class GetHomePostResponse : ResponseBase
    {
        public List<HomePost> HomePosts { get; set; }
    }
}