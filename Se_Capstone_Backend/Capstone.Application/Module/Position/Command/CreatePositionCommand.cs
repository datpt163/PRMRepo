using Capstone.Application.Module.Skills.Response;
using MediatR;


namespace Capstone.Application.Module.Position.Command
{
    public class CreatePositionCommand : IRequest<SkillDto>
    {
         public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
