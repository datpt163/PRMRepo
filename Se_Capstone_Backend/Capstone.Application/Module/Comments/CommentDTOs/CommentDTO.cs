using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Comments.CommentDTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IssueId { get; set; }
        public string Content { get; set; } = string.Empty;
        public UserForProjectDetailDTO? User { get; set; } = null!;
    }
}
