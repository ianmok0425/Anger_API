using Anger_API.API.Models.Abstract;
using Anger_API.Database.PreMembers;

namespace Anger_API.Database.Members
{
    public interface IMemberRepository : IRepository
    {
        APIReturnCode VerifyNewMember(PreMember preMember);
    }
}