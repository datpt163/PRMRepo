﻿using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.Query
{
    public class RegisterQuery : IRequest<ResponseMediator>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}
