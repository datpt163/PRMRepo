
namespace Capstone.Application.Common.Email
{
    public interface IEmailService
    {
        Task<(bool, string)> SendEmailAsync(string ToEmail, string subject, string body);
    }
}
