using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.ConsumerRabbitMq.Message
{
    public class OrderStatusMessage
    {
        public Domain.Entities.Status Status { get; set; } = null!;
        public int Position { get; set; }
    }
}
