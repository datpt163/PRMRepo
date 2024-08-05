using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Capstone.Application.Common.Email
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<(bool, string)> SendEmailAsync(string ToEmail, string subject, string body)
        {

            try
            {
                var smtpClient = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(ToEmail);
                await smtpClient.SendMailAsync(mailMessage);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false,ex.Message);
            }
        }
    }
}
