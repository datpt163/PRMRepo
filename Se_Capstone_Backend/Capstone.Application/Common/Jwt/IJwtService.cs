using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Domain.Entities;
namespace Capstone.Application.Common.Jwt
{
    public interface IJwtService
    {
        Task<string> generatejwttokentw(User account);
        Task<User> VerifyToken(string token);
    }
}
