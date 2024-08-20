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
        public DateTime Dob { get; set; }
        [MaxLength(100)]
        public string BankAccount { get; set; }
        public DateOnly CreateDate { get; set; }
        public DateOnly UpdateDate { get; set; }
        public DateOnly DeleteDate { get; set; }
        public Staff Staff { get; set; } = new Staff();
        public User() : base()
        {
            CreateDate = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
