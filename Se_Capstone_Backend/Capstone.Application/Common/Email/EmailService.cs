using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.Email
{
    public class EmailService : IEmailService
    {
        public async Task<(bool, string)> SendEmailAsync(string body, string email)
        {
            try
            {
                // Send OTP via email
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential("huongdl40@gmail.com", "gepcdegcpjjzceke"),
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("huongdl40@gmail.com"),
                    Subject = "Forgot password",
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);
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
