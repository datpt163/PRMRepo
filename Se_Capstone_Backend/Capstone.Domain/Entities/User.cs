using Microsoft.AspNetCore.Identity;
using System;

namespace Capstone.Domain.Entities
{
    public partial class User : IdentityUser<Guid>
    {
        public string Avatar { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public string BankAccount { get; set; }
        public DateOnly CreateDate { get; set; }
        public DateOnly Updatedate { get; set; }
        public DateOnly DeleteDate { get; set; }
        public Staff Staff { get; set; } = new Staff();

        public User() : base()
        {
            CreateDate = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
