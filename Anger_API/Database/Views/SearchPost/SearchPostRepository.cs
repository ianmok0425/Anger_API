using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Views.SearchPost
{
    public class SearchPostRepository : Repository, ISearchPostRepository
    {
        public override string TableName => "VE_SearchPost";
        public async Task<List<SearchPost>> RetrieveSearchPostList(string searchText, int startRowNo, int endRowNo)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var objs = await db.Query(TableName)
                .WhereBetween(nameof(SearchPost.RowNo), startRowNo, endRowNo)
                .WhereContains(nameof(SearchPost.Subject), searchText)
                .OrderByDesc(nameof(SearchPost.PostAt))
                .GetAsync<SearchPost>();

            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}