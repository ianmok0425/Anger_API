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
        public async Task<List<RunningText>> RetrieveAll()
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            var objs = await db.Query(TableName)
                .GetAsync<RunningText>();
            DBManager.CloseConnection();
            return objs.ToList();
        }
    }
}