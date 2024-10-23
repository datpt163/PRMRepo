using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Query
{
    public class RefreshTokenQuery : IRequest<ResponseMediator>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
