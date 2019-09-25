using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anger_API.Database.Views.SearchPost
{
    public interface ISearchPostRepository : IRepository
    {
        Task<List<SearchPost>> RetrieveSearchPostList(string searchText, int startRowNo);
    }
}