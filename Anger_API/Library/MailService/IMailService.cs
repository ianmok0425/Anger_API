namespace Anger_API.Library.MailService
{
    public interface IMailService
    {
        void SendVerifyEmail(string verifyCode, string saluation ,string toAddress);
    }
}