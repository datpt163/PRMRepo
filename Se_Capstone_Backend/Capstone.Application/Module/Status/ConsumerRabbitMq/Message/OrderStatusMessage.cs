namespace Capstone.Application.Module.Status.ConsumerRabbitMq.Message
{
    public class OrderStatusMessage
    {
        public Domain.Entities.Status Status { get; set; } = null!;
        public int Position { get; set; }
    }
}
