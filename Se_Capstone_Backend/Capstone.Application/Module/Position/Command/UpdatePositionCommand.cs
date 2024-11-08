using Capstone.Application.Module.Positions.Response;
using MediatR;

namespace Capstone.Application.Module.Positions.Command
{
    public class UpdatePositionCommand : IRequest<PositionDto?>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool? Isdeleted { get; set; } 
    }
}
