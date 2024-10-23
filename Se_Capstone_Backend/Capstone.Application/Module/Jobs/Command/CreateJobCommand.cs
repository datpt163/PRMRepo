using Capstone.Application.Module.Jobs.Response;
using MediatR;



namespace Capstone.Application.Module.Jobs.Command
{
    public class CreateJobCommand : IRequest<JobDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

