using Capstone.Application.Module.Auths.Response;
using MediatR;

namespace Capstone.Api.Module.Auths.Request
{
    public class LogoutQuery : IRequest<LogoutResponse>
    {
        public string Token { get; set; } = string.Empty;

        public LogoutQuery(string token)
        {
            Token = token;
        }

        public LogoutQuery() { }
    }
}
