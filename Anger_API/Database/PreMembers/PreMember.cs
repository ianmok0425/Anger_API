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
    }
}