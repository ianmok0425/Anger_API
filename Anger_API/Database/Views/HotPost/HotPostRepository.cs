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
            var past3DaysDateString = today.AddDays(-3).ToString("yyyy-MM-dd");
            var todayDateString = today.ToString("yyyy-MM-dd");
            var objs = await db.Query(TableName)
                .WhereDate(nameof(HotPost.PostAt), Operator.LessEqual, todayDateString)
                .WhereDate(nameof(HotPost.PostAt), Operator.GreaterEqual, past3DaysDateString)
                .Limit(10)
                .Offset(startRowNo)
                .GetAsync<HotPost>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}