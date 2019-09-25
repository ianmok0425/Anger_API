using System.Collections.Generic;

using Anger_Library;
using Anger_API.Database.Views.HotPost;

namespace Anger_API.API.Models.HotPosts
{
    public class GetHotPostRquest : APIRequest
    {
        public int StartRowNo { get; set; }
    }
    public class GetHotPostResponse : ResponseBase
    {
        public List<HotPost> HotPosts { get; set; }
    }
}