using Capstone.Application.Module.Applicants.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

public class UpdateApplicantCommand : IRequest<ApplicantDto?>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public IFormFile? CvFile { get; set; }
    public Guid MainJobId { get; set; }
    public List<Guid> JobIds { get; set; } = new List<Guid>();
    public bool? IsOnBoard { get; set; }

    public UpdateApplicantCommand(Guid id, string name, string email, DateTime? startDate,
        string phoneNumber, IFormFile? cvFile, Guid mainJobId, List<Guid> jobIds, bool? isOnBoard)
    {
        Id = id;
        Name = name;
        Email = email;
        StartDate = startDate;
        PhoneNumber = phoneNumber;
        CvFile = cvFile; 
        MainJobId = mainJobId;
        JobIds = jobIds;
        IsOnBoard = isOnBoard;
    }
}
