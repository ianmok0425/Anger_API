using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.RunningTexts
{
    public class RunningTextRepository : Repository, IRunningTextRepository
    {
        public override string TableName => "Anger_RunningText";
        public async Task<List<RunningText>> RetrieveAll(DateTime? createdAt)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var query = db.Query(TableName);
            if (createdAt != null) query = query.WhereDate(nameof(RunningText.CreatedAt), createdAt.Value.ToString("yyyy-MM-dd"));

            var objs = await query.GetAsync<RunningText>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
        public async Task<List<RunningText>> RetrieveTodayList()
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);

            var objs = await db.Query(TableName)
                .Where(nameof(RunningText.Approved), true)
                .Where(nameof(RunningText.EmailNotice), true)
                .WhereBetween(nameof(RunningText.PostAt), today, yesterday)
                .GetAsync<RunningText>();
            DBManager.CloseConnection();
            return objs.ToList();
        }

        public async Task<List<RunningText>> RetrieveApprovedList(DateTime? createdAt)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var query = db.Query(TableName).Where(nameof(RunningText.Approved), true);
            if (createdAt != null) query = query.WhereDate(nameof(RunningText.CreatedAt), createdAt.Value.ToString("yyyy-MM-dd"));

            var objs = await query.GetAsync<RunningText>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
        public async Task<List<RunningText>> RetrieveNotApprovedList(DateTime? createdAt)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var query = db.Query(TableName)
                            .Where(nameof(RunningText.Approved), false)
                            .OrWhere(nameof(RunningText.Approved), null);
            if (createdAt != null) query = query.WhereDate(nameof(RunningText.CreatedAt), createdAt.Value.ToString("yyyy-MM-dd"));

            var objs = await query.GetAsync<RunningText>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}