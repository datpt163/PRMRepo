using Capstone.Application.Module.Applicants.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Capstone.Application.Module.Applicants.Command
{
    public class AddApplicantCommand : IRequest<ApplicantDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public IFormFile? CvFile { get; set; }

        //public AddApplicantCommand(string name, string email, DateTime? startDate, string phoneNumber, IFormFile cvFile)
        //{
        //    Name = name;
        //    Email = email;
        //    StartDate = startDate;
        //    PhoneNumber = phoneNumber;
        //    CvFile = cvFile;
        //}
    }
}
