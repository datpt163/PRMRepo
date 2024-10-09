using Capstone.Application.Module.Jobs.Response;
using MediatR;

namespace Capstone.Application.Module.Jobs.Command
{
    public class UpdateJobCommand : IRequest<JobDto?>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool? Isdeleted { get; set; } 
    }
}
