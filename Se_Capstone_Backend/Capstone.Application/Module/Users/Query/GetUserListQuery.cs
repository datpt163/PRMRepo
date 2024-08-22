using Capstone.Application.Module.Users.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.Query
{
    public class GetUserListQuery : IRequest<List<UserDto>>
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = null;
        public int? Gender { get; set; } = null; 
        public DateTime? DobFrom { get; set; } = null; 
        public DateTime? DobTo { get; set; } = null; 
    }

}
