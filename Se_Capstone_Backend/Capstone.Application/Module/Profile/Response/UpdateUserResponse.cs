using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Profile.Response
{
    public class UpdateUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public DateOnly CreateDate { get; set; }

        public UpdateUserResponse() { }
        public UpdateUserResponse(Guid id, string email, string phone, string fullName, string avatar, DateOnly createDate)
        {
            Id = id;
            Email = email;
            Phone = phone;
            FullName = fullName;
            Avatar = avatar;
            CreateDate = createDate;
        }
    }
}
