using System;
using System.Threading.Tasks;

using SqlKata.Compilers;
using SqlKata.Execution;

using Anger_API.API.Models.Abstract;
using Anger_API.Library.MailService;
using static Anger_API.Database.AngerDB;

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
        public async Task<string> CreateAndSendVerifyCode(PreMember preMember)
        {
            Random random = new Random();
            string verifyCode = random.Next(100000, 999999).ToString();
            preMember.VerifyCode = verifyCode;
            preMember.Verified = false;

            string preMemberID = await base.CreateAsync(preMember);
            await MailService.SendVerifyEmail(verifyCode, preMember.Name, preMember.Email);
            return preMemberID;
        }
        public async Task ResendVerifyCode(long preMemberID, PreMember preMember)
        {
            Random random = new Random();
            string verifyCode = random.Next(100000, 999999).ToString();
            preMember.VerifyCode = verifyCode;
            await MailService.SendVerifyEmail(verifyCode, preMember.Name, preMember.Email);
            await base.Update(preMemberID, preMember);
        }
        public async Task Verified(long preMemberID, PreMember preMember)
        {
            preMember.Verified = true;
            await base.Update(preMemberID, preMember);
        }
    }
}