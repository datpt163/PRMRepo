
using Capstone.Domain.Entities;
namespace Capstone.Application.Common.Jwt
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(User account, DateTime expireTime, string secretKeyReserve = "");
        Task<User?> VerifyTokenAsync(string token, string secretKeyReserve = "");
    }
}
