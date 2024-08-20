using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    public partial class User : IdentityUser<Guid>
    {
        [MaxLength(100)]
        public string Avatar { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
        public int Status { get; set; }
        [MaxLength(30)]
        public string Gender { get; set; } = string.Empty;
        public DateOnly Dob { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string BankAccount { get; set; } = string.Empty;
        public DateOnly CreateDate { get; set; }
        public DateOnly UpdateDate { get; set; }
        public DateOnly DeleteDate { get; set; }
        public Staff? Staff { get; set; }
        public User() { }
        public User(string email, string address, string gender, DateOnly dob, string phone, string userName, string fullName)
        {
            Email = email;  
            Address = address;
            Gender = gender;
            Dob = dob;
            UserName = userName;
            PhoneNumber = phone;
            FullName = fullName;
            CreateDate = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
