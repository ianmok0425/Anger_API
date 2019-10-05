using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Notices
{
    public class NoticeRepository : Repository, INoticeRepository
    {
        public override string TableName => "Anger_Notice";

        public async Task<List<Notice>> Retrieve()
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var now = DateTime.Now;

            var objs = await db.Query(TableName)
                .Where(nameof(Notice.StartAt), Operator.LessEqual, now)
                .Where(nameof(Notice.EndAt), Operator.GreaterEqual, now)
                .GetAsync<Notice>();

            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}