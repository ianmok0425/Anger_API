using System.Threading.Tasks;

namespace Anger_API.Database.PreMembers
{
    public interface IPreMemberRepository : IRepository
    {
        Task<string> CreateAndSendVerifyCode(PreMember preMember);
        Task ResendVerifyCode(long preMemberID, PreMember preMember);
        Task Verified(long preMemberID, PreMember preMember);
    }
}