using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Security;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings smtpSettings;

        public EmailService(SmtpSettings smtpSettings)
        {
            this.smtpSettings = smtpSettings;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            try
            {
                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(from ?? smtpSettings.EmailFrom));
                email.To.Add(MailboxAddress.Parse(to));

                email.Subject = subject;

                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using (var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //smtp.CheckCertificateRevocation = false;
                    smtp.Connect(smtpSettings.SmtpHost, 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(smtpSettings.SmtpUser, smtpSettings.SmtpPass);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }
    }
}
