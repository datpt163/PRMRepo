using Capstone.Application.Common.Cloudinaries;
using Capstone.Application.Module.Applicants.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand, ApplicantDto?>
{
    private readonly IRepository<Applicant> _applicantRepository;
    private readonly CloudinaryService _cloudinaryService;
    private readonly IRepository<Job> _jobRepository;

    public UpdateApplicantCommandHandler(IRepository<Applicant> applicantRepository, IRepository<Job> jobRepository, CloudinaryService cloudinaryService)
    {
        _applicantRepository = applicantRepository;
        _jobRepository = jobRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ApplicantDto?> Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
    {
        var applicant = await _applicantRepository.FindOneAsync(a => a.Id == request.Id);

        if (applicant == null)
        {
            return null;
        }

        applicant.Name = request.Name;
        applicant.Email = request.Email;
        applicant.StartDate = request.StartDate;
        applicant.PhoneNumber = request.PhoneNumber;

        if (request.CvFile != null)
        {
            if (!string.IsNullOrEmpty(applicant.CvLink))
            {
                await _cloudinaryService.DeletePdfByUrlAsync(applicant.CvLink);
            }

            using var pdfStream = new MemoryStream();
            await request.CvFile.CopyToAsync(pdfStream);
            pdfStream.Position = 0;
            applicant.CvLink = await _cloudinaryService.UploadPdfAsync(pdfStream, request.CvFile.FileName);
        }

        applicant.MainJobId = request.MainJobId;
        applicant.MainJob = await _jobRepository.FindOneAsync(x => x.Id == request.MainJobId);
        applicant.IsOnBoard = request.IsOnBoard;

        if (request.JobIds != null && request.JobIds.Any())
        {
            applicant.Jobs.Clear(); 
            foreach (var jobId in request.JobIds)
            {
                var job = await _jobRepository.FindOneAsync(x => x.Id == jobId);
                if (job != null)
                {
                    applicant.Jobs.Add(job); 
                }
            }
        }
        applicant.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        await _applicantRepository.UpdateAsync(applicant);

        var applicantDto = new ApplicantDto
        {
            Id = applicant.Id,
            Name = applicant.Name,
            Email = applicant.Email,
            StartDate = applicant.StartDate,
            PhoneNumber = applicant.PhoneNumber,
            CvLink = applicant.CvLink
        };

        return applicantDto;
    }
}
