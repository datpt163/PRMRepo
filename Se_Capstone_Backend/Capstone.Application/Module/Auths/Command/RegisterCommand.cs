using Capstone.Application.Common.ResponseMediator;
using Capstone.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auth.Command
{
    public class RegisterCommand : IRequest<ResponseMediator>
    {

        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Gender Gender { get; set; } 
        public DateTime Dob { get; set; }
        public string Phone { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public Guid PositionId { get; set; }

        public RegisterCommand(Guid positionId, Guid roleId, string token, string email, string password, string userName, string fullName, string address, Gender gender, DateTime dob, string phone)
        {
            RoleId = roleId;
            Token = token;
            Email = email;
            Password = password;
            UserName = userName;
            FullName = fullName;
            Address = address;
            Gender = gender;
            Dob = dob;
            Phone = phone;
            PositionId = positionId;
        }
    }

    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; } = string.Empty;
        public Guid PositionId { get; set; }
        public Guid RoleId { get; set; }
    }
}
