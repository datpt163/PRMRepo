using Capstone.Application.Module.Applicants.Command;
using Capstone.Application.Module.Applicants.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Applicants.CommandHandler
{
    public class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand, ApplicantDto?>
    {
        private readonly IRepository<Applicant> _applicantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteApplicantCommandHandler(IRepository<Applicant> applicantRepository, IUnitOfWork unitOfWork)
        {
            _applicantRepository = applicantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicantDto?> Handle(DeleteApplicantCommand command, CancellationToken cancellationToken)
        {
            var applicant = await _applicantRepository.GetQuery()
                .Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (applicant == null)
            {
                return null;
            }

            var applicantDto = new ApplicantDto
            {
                Id = applicant.Id,
                Name = applicant.Name,
                Email = applicant.Email,
                StartDate = applicant.StartDate,
                PhoneNumber = applicant.PhoneNumber,
                CvLink = applicant.CvLink,
                CreatedAt = applicant.CreatedAt,
                CreatedBy = applicant.CreatedBy,
                UpdatedAt = applicant.UpdatedAt,
                UpdatedBy = applicant.UpdatedBy,
                IsDeleted = true,
            };

            applicant.IsDeleted = true;
            _applicantRepository.Update(applicant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return applicantDto;
        }
    }
}
