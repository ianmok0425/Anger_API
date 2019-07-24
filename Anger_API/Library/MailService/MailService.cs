using System;
using System.Net.Mail;

namespace Anger_API.Library.MailService
{
    public class MailService : IMailService
    {
        public void SendVerifyEmail(string verifyCode, string saluation, string toAddress)
        {
            var mailInfo = Anger.GetConfig().Mail;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(mailInfo.SmtpAddress);

            mail.From = new MailAddress(mailInfo.Address);
            mail.To.Add(toAddress);
            mail.Subject = "Anger 註冊驗證碼";
            mail.Body = 
                $"{saluation},你好!" 
                + Environment.NewLine + Environment.NewLine + 
                $"{verifyCode} 是你的驗證碼。" 
                + Environment.NewLine + Environment.NewLine + 
                "感謝你的註冊，謝謝!";

            SmtpServer.Port = mailInfo.Port.Value;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailInfo.Account, mailInfo.Password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}