using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Command
{
    public class AddMembersToProject : IRequest<ResponseMediator>
    {
        public Guid ProjectId { get; set; }
        public List<Guid> MemberIds { get; set; } = new List<Guid>();
    }
}
