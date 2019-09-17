using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;
using Anger_API.Database.Posts;

namespace Anger_API.API.Models.Posts
{
    public class GetPostRequest : APIRequest
    {
        public long ID { get; set; }
    }
    public class GetPostResponse : ResponseBase
    {
        public Post Post { get; set; }
    }
}