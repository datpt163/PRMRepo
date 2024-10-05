using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using Capstone.Application.Common.Email;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace Capstone.Application.Common.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private readonly IEmailService _emailService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly ILogger<RabbitMQConsumer> _logger;

        public RabbitMQConsumer(IEmailService emailService, string hostname, string queueName, ILogger<RabbitMQConsumer> logger)
        {
            _emailService = emailService;
            _hostname = hostname;
            _queueName = queueName;
            _logger = logger;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var emailMessage = JsonConvert.DeserializeObject<EmailMessage>(message);

                    var (success, errorMessage) = await _emailService.SendEmailAsync(emailMessage?.ToEmail ?? string.Empty, emailMessage?.Subject ?? string.Empty, emailMessage?.Body ?? string.Empty);

                    if (success)
                    {
                        _logger.LogInformation($"Email sent to {emailMessage?.ToEmail}");
                    }
                    else
                    {
                        _logger.LogError($"Failed to send email to {emailMessage?.ToEmail}: {errorMessage}");
                    }
                };

                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            }
        }
    }
}
