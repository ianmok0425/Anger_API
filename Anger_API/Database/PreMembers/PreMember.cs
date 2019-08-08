using System.ComponentModel.DataAnnotations;

namespace Anger_API.Database.PreMembers
{
    public class PreMember : Table
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string VerifyCode { get; set; }
        public bool Verified { get; set; }
        [EnumDataType(typeof(PreMemberStatus))]
        public PreMemberStatus Status { get; set; }
    }
    public enum PreMemberStatus
    {
        Active = 0,
        Inactive = 1
    }
}