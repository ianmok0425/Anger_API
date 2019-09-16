using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.Posts
{
    public class PostRepository : Repository, IPostRepository
    {
        public override string TableName => "Anger_Post";
    }
}