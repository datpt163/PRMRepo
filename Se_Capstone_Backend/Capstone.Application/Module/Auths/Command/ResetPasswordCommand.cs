using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Command
{
    public class ResetPasswordCommand : IRequest<ResponseMediator>
    {
        public string? Code { get; set; }
        public string? NewPassword { get; set; }
    }
}
