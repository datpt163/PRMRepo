using Microsoft.AspNetCore.Identity;
using System;

namespace Capstone.Domain.Entities
{
    public partial class User : IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public DateOnly CreateDate { get; set; }

        public User() : base()
        {
            CreateDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public User(string email, string password, string phone, string fullName, string avatar) : base()
        {
            Email = email;
            PasswordHash = new PasswordHasher<User>().HashPassword(this, password);
            PhoneNumber = phone;
            UserName = email;
            FullName = fullName;
            Avatar = avatar;
            CreateDate = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
