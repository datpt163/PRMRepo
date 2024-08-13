using System;
using System.Collections.Generic;

namespace Capstone.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public DateOnly CreateDate { get; set; }

    public User() { }
    public User(string email, string password, string phone, string fullname, string avatar)
    {
        Email = email;
        Password = password;
        Phone = phone;
        FullName = fullname;
        Avatar = avatar;
        CreateDate = DateOnly.FromDateTime(DateTime.Now);
    }
}
