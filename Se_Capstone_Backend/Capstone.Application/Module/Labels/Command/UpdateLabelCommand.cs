using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Labels.Command
{
    public class UpdateLabelCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
