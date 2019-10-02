using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Views.HotPost
{
    public class HotPostRepository : Repository, IHotPostRepository
    {
        public override string TableName => "VE_HotPost";
        public async Task<List<HotPost>> RetrieveHotPostList(int startRowNo)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var today = DateTime.Now;
            var past3Days = today.AddDays(-3);
            var objs = await db.Query(TableName)
                .WhereBetween(nameof(HotPost.PostAt), past3Days, today)
                .Limit(10)
                .Offset(startRowNo)
                .GetAsync<HotPost>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}