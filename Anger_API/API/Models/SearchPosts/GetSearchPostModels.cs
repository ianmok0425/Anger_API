using System.Collections.Generic;

using Anger_Library;
using Anger_API.Database.Views.SearchPost;

namespace Anger_API.API.Models.SearchPosts
{
    public class GetSearchPostRequest : APIRequest
    {
        public string SearchText { get; set; }
        public int StartRowNo { get; set; }
        public int EndRowNo { get; set; }
    }
    public class GetSearchPostResponse : ResponseBase
    {
        public List<SearchPost> SearchPosts { get; set; }
    }
}