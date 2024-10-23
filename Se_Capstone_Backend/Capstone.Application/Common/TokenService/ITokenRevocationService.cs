

namespace Capstone.Application.Common.TokenService
{
    public interface ITokenRevocationService
    {
        public Task<(bool isSuccess, string errorMessage)> RevocationTokenAsync(Guid userId); 
    }
}
