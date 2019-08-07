using System.Threading.Tasks;

namespace Anger_API.Library.MailService
{
    public interface IMailService
    {
        Task SendVerifyEmail(string verifyCode, string saluation ,string toAddress);
    }
}