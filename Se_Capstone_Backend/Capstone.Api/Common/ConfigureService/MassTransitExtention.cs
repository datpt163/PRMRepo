using Capstone.Application.Module.Status.ConsumerRabbitMq;
using MassTransit;

namespace Capstone.Api.Common.ConfigureService
{
    public static class MassTransitExtention
    {
        public static void AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
        {
            var RabbitMqSetting = configuration.GetSection("MessageBroker");
            string host = RabbitMqSetting["host"] ?? string.Empty;
            string userName = RabbitMqSetting["userName"] ?? string.Empty;
            string password = RabbitMqSetting["password"] ?? string.Empty;

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();


                busConfiguration.AddConsumer<OrderStatusConsumer>();

                busConfiguration.UsingRabbitMq((context, configuration) =>
                {
                    configuration.Host(new Uri(host), h =>
                    {
                        h.Username(userName);
                        h.Password(password);
                    });
                    configuration.ConfigureEndpoints(context);
                });
            });
        }
    }
}
