using Capstone.Application.Module.Applicants.Response;
using MediatR;

namespace Capstone.Application.Module.Applicants.Query
{
    public class GetApplicantDetailQuery : IRequest<ApplicantDto?>
    {
        public Guid Id { get; }

        public GetApplicantDetailQuery(Guid id)
        {
            Id = id;
        }
    }
}
