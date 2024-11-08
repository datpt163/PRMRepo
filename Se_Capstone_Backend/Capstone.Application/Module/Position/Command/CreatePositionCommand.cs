using Capstone.Application.Module.Positions.Response;
using MediatR;


namespace Capstone.Application.Module.Positions.Command
{
    public class CreatePositionCommand : IRequest<PositionDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

