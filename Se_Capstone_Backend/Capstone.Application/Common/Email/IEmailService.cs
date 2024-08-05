using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.Email
{
    public interface IEmailService
    {
        Task<(bool, string)> SendEmailAsync(string body, string email);
    }
}
