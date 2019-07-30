using System.Threading.Tasks;

namespace Anger_API.Database.PreMembers
{
    public interface IPreMemberRepository : IRepository
    {
        Task CreateAndSendVerifyCode(PreMember preMember);
    }
}