using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.Response
{
    public class RegisterResponse
    {
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? BankAccount { get; set; }
        public string? BankAccountName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Staff? Staff { get; set; }
        public RegisterResponse() { }
        //public RegisterResponse(Guid id, string email, string userName, string fullName, string address, Gender gender, DateTime dob, string phone,  string fullName)
        //{
           
        //}
    }
}
