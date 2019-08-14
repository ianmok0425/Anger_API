using System;
using System.Linq;

using SqlKata.Execution;
using SqlKata.Compilers;

using Anger_API.Database.PreMembers;
using Anger_API.API.Models.Abstract;
using static Anger_API.Database.AngerDB;
using System.Threading.Tasks;

using Anger_API.API.Models.Members;

namespace Anger_API.Database.Members
{
    public class MemberRepository : Repository, IMemberRepository
    {
        public override string TableName => "Anger_Member";
        public async Task<Member> RetrieveByAC(string account)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            var objs = await db.Query(TableName)
                .Where(nameof(Member.Account), account)
                .GetAsync<Member>();
            DBManager.CloseConnection();
            return objs.FirstOrDefault();
        }

        public async Task<Tuple<long?, Member>> RetrieveMemberAndIDByAcPw(string account, string password)
        {
            DBManager.OpenConnection();
            var compiler = new SqlServerCompiler();
            var db = new QueryFactory(DBManager.Conn, compiler);
            long? id = await db.Query(TableName)
                .Select("ID")
                .Where(nameof(Member.Account), account)
                .Where(nameof(Member.Password), password)
                .FirstOrDefaultAsync<long?>();

            if(id == null)
            {
                DBManager.CloseConnection();
                return null;
            }
            else
            {
                var objs = await db.Query(TableName)
               .Where(nameof(Member.Account), account)
               .Where(nameof(Member.Password), password)
               .GetAsync<Member>();
                Member member = objs.FirstOrDefault();
                DBManager.CloseConnection(); 
                return new Tuple<long?, Member>(id, member);
            }

        }
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

            DBManager.CloseConnection();
            return APIReturnCode.Success;
        }
    }
}