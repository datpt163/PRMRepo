using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Query
{
    public class ProfileQuery : IRequest<ResponseMediator>
    {
        public string Token {  get; set; }  = string.Empty;
    }
}
