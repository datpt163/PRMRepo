using MediatR;
using Capstone.Application.Module.Jobs.Response;

namespace Capstone.Application.Module.Jobs.Query
{
    public class GetJobQuery : IRequest<JobDto>
    {
        public Guid Id { get; set; }
    }
}
