using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Labels.Command
{
    public class CreateLabelCommand : IRequest<ResponseMediator>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
    }
}
