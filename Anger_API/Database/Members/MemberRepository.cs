using System.Linq;

using SqlKata.Execution;
using SqlKata.Compilers;

using Anger_API.Database.PreMembers;
using Anger_API.API.Models.Abstract;
using static Anger_API.Database.AngerDB;

namespace Anger_API.Database.Members
{
    public class MemberRepository : Repository, IMemberRepository
    {
        public override string TableName => "Anger_Member";
        public APIReturnCode VerifyNewMember(PreMember preMember)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);

            bool emailExist = db.Query(TableName)
                .Where(nameof(preMember.Email), preMember.Email)
                .Get()
                .Count() > 0;
            if (emailExist) return APIReturnCode.EmailExist;

            bool mobileExist = db.Query(TableName)
                .Where(nameof(preMember.Mobile), preMember.Mobile)
                .Get()
                .Count() > 0;
            if (mobileExist) return APIReturnCode.MobileExist;

            bool accountExist = db.Query(TableName)
                .Where(nameof(preMember.Account), preMember.Account)
                .Get()
                .Count() > 0;
            if (accountExist) return APIReturnCode.AccountExist;

            return APIReturnCode.Success;
        }
    }
}