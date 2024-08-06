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
        Task<string> GenerateJwtToken(User account, int expireTime = 30);
        Task<User> VerifyToken(string token);
    }
}
