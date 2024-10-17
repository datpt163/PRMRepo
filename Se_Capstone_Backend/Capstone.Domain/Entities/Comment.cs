using Capstone.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    [Table("comments")]
    public class Comment : BaseEntity

    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IssueId {  get; set; }
        public string Content { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public Issue Issue { get; set; } = null!;

    }
}
