﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class User
    {
        public Guid Id { get;  set; }
        public string? Email { get;  set; }
        public string? Password { get;  set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Guid RoleId { get; private set; }

    }
}
