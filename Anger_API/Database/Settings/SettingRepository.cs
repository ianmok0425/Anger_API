using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using SqlKata.Compilers;
using SqlKata.Execution;

using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Settings
{
    public class SettingRepository : Repository, ISettingRepository
    {
        public override string TableName => "Anger_Setting";
        public async Task<Setting> Retrieve()
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            var objs = await db.Query(TableName)
                .GetAsync<Setting>();

            DBManager.CloseConnection();
            return objs.FirstOrDefault();
        }
    }
}