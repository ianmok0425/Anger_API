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
        public enum MemberStatus
        {
            Active = 1,
            Inactive = 2
        }
    }
}