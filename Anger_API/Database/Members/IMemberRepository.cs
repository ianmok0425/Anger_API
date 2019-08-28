using System;
using System.Threading.Tasks;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Members;
using Anger_API.Database.PreMembers;

namespace Anger_API.Database.Members
{
    public interface IMemberRepository : IRepository
    {
        APIReturnCode VerifyNewMember(PreMember preMember);
        Task<Member> RetrieveMemberByAcPw(string account, string password);
        Task<Member> RetrieveByAC(string account);
    }
}