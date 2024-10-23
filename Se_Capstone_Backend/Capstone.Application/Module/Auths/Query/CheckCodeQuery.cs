using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Query
{
    public class CheckCodeQuery : IRequest<ResponseMediator>
    {
        public string Code { get; set; } = string.Empty;
    }
}
