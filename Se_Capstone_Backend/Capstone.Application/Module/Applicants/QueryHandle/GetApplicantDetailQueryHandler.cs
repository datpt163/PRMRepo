using Capstone.Application.Module.Applicants.Query;
using Capstone.Application.Module.Applicants.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Applicants.QueryHandler
{
    public class GetApplicantDetailQueryHandler : IRequestHandler<GetApplicantDetailQuery, ApplicantDto?>
    {
        private readonly IRepository<Applicant> _applicantRepository;

        public GetApplicantDetailQueryHandler(IRepository<Applicant> applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<ApplicantDto?> Handle(GetApplicantDetailQuery query, CancellationToken cancellationToken)
        {
            var applicant = await _applicantRepository.GetQueryNoTracking()
                .FirstOrDefaultAsync(a => a.Id == query.Id, cancellationToken);

            if (applicant == null) return null;

            return new ApplicantDto
            {
                Id = applicant.Id,
                Name = applicant.Name,
                Email = applicant.Email,
                StartDate = applicant.StartDate,
                PhoneNumber = applicant.PhoneNumber,
                CvLink = applicant.CvLink
            };
        }
    }
}
