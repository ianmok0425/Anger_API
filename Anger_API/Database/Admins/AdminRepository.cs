using System;
using System.Linq;

using SqlKata.Execution;
using SqlKata.Compilers;

using Anger_API.Database.PreMembers;
using Anger_API.API.Models.Abstract;
using static Anger_API.Database.AngerDB;
using System.Threading.Tasks;

using Anger_API.API.Models.Members;

namespace Anger_API.Database.Admins
{
    public class AdminRepository : Repository, IAdminRepository
    {
        public override string TableName => "Anger_Admin";
        public async Task<Admin> GetAdminByAcAndPw(string account, string password)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            var objs = await db.Query(TableName)
                .Where(nameof(Admin.Account), account)
                .Where(nameof(Admin.Password), password)
                .GetAsync<Admin>();
            DBManager.CloseConnection();
            return objs.FirstOrDefault();
        }
    }
}