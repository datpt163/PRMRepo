using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.RabbitMQ
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
    }
}
