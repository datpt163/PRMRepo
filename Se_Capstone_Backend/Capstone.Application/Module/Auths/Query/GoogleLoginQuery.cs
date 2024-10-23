using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Query
{
    public class GoogleLoginQuery : IRequest<ResponseMediator>
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
