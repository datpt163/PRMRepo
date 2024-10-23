using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Query
{
    public class ForgotPasswordQuery : IRequest<ResponseMediator>
    {
        public string? Email { get; set; }
    }
}
