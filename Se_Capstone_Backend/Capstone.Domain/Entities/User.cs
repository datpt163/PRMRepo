using System;
using System.Collections.Generic;

namespace Capstone.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public DateOnly Createdate { get; set; }
}
