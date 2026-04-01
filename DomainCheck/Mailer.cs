using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace DomainCheck
{
    internal class Mailer
    {
        SmtpClient Client;
        MailMessage Message;
        public Mailer()
        {
            Client = ConfigureSMTP();
            Message = ConfigureMessage();
        }
        public void SendMail(string bodyText)
        {
            Message.Body = bodyText;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
            Client.Send(Message);
        }
        private SmtpClient ConfigureSMTP()
        {
            SmtpClient smtp = new SmtpClient("smtp.office365.com");
            smtp.TargetName = "STARTTLS/smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(Configurator.MailAccount, Configurator.MailSecret);
            return smtp;
        }
        private MailMessage ConfigureMessage()
        {
            MailAddress from = new MailAddress("auto@zenger.com");
            MailAddress to = new MailAddress(Configurator.MailTo);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Monthly Domain Check.";
            message.IsBodyHtml = true;
            return message;
        }
    }
}
