using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Comments.Command
{
    public class AddCommentCommand : IRequest<ResponseMediator>
    {
        public string Token { get; set; } = string.Empty;
        public Guid IssueId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
