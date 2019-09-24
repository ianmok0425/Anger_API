using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.Views.HotPost
{
    public class HotPostRepository : Repository, IHotPostRepository
    {
        public override string TableName => "VE_HotPost";
    }
}