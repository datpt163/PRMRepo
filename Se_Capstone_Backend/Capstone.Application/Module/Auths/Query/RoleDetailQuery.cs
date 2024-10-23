using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Query
{
    public  class RoleDetailQuery : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
    }
}
