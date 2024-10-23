using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Projects.Command
{
    public class DeleteProjectCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; } 
    }
}
