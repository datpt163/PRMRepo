using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Status.Command
{
    public class CreateStatusCommand : IRequest<ResponseMediator>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public Guid ProjectId { get; set; }
    }
}
