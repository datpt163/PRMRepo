using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.Response
{
    public class GetProfileResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateOnly CreateDate { get; set; }

        public GetProfileResponse() { }
        public GetProfileResponse(Guid id, string email, string phone, string fullName, string avatar, string userName)
        {
            Id = id;
            Email = email;
            Phone = phone;
            FullName = fullName;
            UserName = userName;
        }
    }
}
