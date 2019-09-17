using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Views.HomePost
{
    public class HomePostRepository : Repository, IHomePostRepository
    {
        public override string TableName => "VE_HomePost";
        public async Task<List<HomePost>> RetrieveHomePostList(int startRowNo, int endRowNo)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var objs = await db.Query(TableName)
                .WhereDate(nameof(HomePost.PostAt), today)
                .WhereBetween(nameof(HomePost.RowNo), startRowNo, endRowNo)
                .GetAsync<HomePost>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}