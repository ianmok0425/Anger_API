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
        public async Task<List<HomePost>> RetrieveHomePostList(int startRowNo)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var today = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);
            var objs = await db.Query(TableName)
                .WhereBetween(nameof(HomePost.PostAt), yesterday, today)
                .Limit(10)
                .Offset(startRowNo)
                .GetAsync<HomePost>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}