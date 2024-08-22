using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone.Domain.Enums;
namespace Capstone.Domain.Entities
{
    public partial class User : IdentityUser<Guid>
    {
        [MaxLength(100)]
        public string? Avatar { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
        public StatusUser Status { get; set; } = StatusUser.Active;
        [MaxLength(30)]
        public Gender Gender { get; set; } 
        public DateTime? Dob { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? BankAccount { get; set; }
        public string? BankAccountName { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Staff? Staff { get; set; }
        public User() { }
        public User(string email, string address, Gender gender, DateTime dob, string phone, string userName, string fullName)
        {
            Email = email;  
            Address = address;
            Gender = gender;
            Dob = dob;
            UserName = userName;
            PhoneNumber = phone;
            FullName = fullName;
            CreateDate = DateTime.Now;
        }
    }
}
