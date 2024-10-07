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
        public UserStatus Status { get; set; } = UserStatus.Active;
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
        public Guid? PositionId { get; set; }
        public string? RefreshToken { get; set; }
        public User() { }
        public Position? Position { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Project> LeadProjects { get; set; } = new List<Project>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<LeaveLog> LeaveLogs { get; set; } = new List<LeaveLog>();

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
