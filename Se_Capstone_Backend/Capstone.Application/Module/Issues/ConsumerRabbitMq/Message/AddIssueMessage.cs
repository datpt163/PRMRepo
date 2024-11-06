using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.ConsumerRabbitMq.Message
{
    public class AddIssueMessage
    {
        public Domain.Entities.Issue? issue { get; set; }
    }
}
