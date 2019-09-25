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
        public async Task<List<SearchPost>> RetrieveSearchPostList(string searchText, int startRowNo)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var objs = await db.Query(TableName)
                .WhereContains(nameof(SearchPost.Subject), searchText)
                .OrderByDesc(nameof(SearchPost.PostAt))
                .Limit(10)
                .Offset(startRowNo)
                .GetAsync<SearchPost>();

            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}