using System;
using System.Threading.Tasks;
using Anger_API.Library.MailService;

namespace Anger_API.Database.PreMembers
{
    public class PreMemberRepository : Repository, IPreMemberRepository
    {
        public override string TableName => "Anger_PreMember";
        public IMailService MailService { get; }
        public PreMemberRepository(IMailService mailService)
        {
            MailService = mailService ?? throw new ArgumentNullException(nameof(MailService));
        }
        public async Task CreateAndSendVerifyCode(PreMember preMember)
        {
            Random random = new Random();
            string verifyCode = random.Next(100000, 999999).ToString();
            preMember.VerifyCode = verifyCode;
            preMember.Verified = false;
            MailService.SendVerifyEmail(verifyCode, preMember.Name, preMember.Email);
            await base.Create(preMember);
        }
    }
}