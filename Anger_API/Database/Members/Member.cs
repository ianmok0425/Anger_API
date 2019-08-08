using System.ComponentModel.DataAnnotations;
using Anger_API.Database.PreMembers;

namespace Anger_API.Database.Members
{
    public class Member : Table
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        [EnumDataType(typeof(MemberStatus))]
        public MemberStatus Status { get; set; }
        public Member(PreMember preMember)
        {
            Name = preMember.Name;
            Mobile = preMember.Mobile;
            Email = preMember.Email;
            Account = preMember.Account;
            Password = preMember.Password;
            Status = (MemberStatus)preMember.Status;
        }
        public enum MemberStatus
        {
            Active = 1,
            Inactive = 2
        }
    }
}