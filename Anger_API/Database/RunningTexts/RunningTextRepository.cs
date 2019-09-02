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
        public async Task<List<RunningText>> RetrieveAll(DateTime? createdOn)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var query = db.Query(TableName);
            if (createdOn != null) query = query.WhereDate(nameof(RunningText.CreatedAt), createdOn.Value.ToString("yyyy-MM-dd"));

            var objs = await query.GetAsync<RunningText>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}