using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Anger_API.Database.Views.HomePost
{
    public interface IHomePostRepository : IRepository
    {
        Task<List<HomePost>> RetrieveHomePostList(int startRowNo, int endRowNo);
    }
}