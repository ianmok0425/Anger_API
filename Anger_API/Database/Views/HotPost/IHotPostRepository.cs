using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anger_API.Database.Views.HotPost
{
    public interface IHotPostRepository : IRepository
    {
        Task<List<HotPost>> RetrieveHotPostList(int startRowNo);
    }
}