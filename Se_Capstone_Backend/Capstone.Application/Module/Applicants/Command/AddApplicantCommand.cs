using Capstone.Application.Module.Applicants.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Capstone.Application.Module.Applicants.Command
{
    public class AddApplicantCommand : IRequest<ApplicantDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? StartDate { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile CvFile { get; set; }

        public AddApplicantCommand(string name, string email, DateTime? startDate, string phoneNumber, IFormFile cvFile)
        {
            Name = name;
            Email = email;
            StartDate = startDate;
            PhoneNumber = phoneNumber;
            CvFile = cvFile;
        }
    }
}
