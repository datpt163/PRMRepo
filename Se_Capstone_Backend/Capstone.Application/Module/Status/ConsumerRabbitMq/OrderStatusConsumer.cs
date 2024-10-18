using Capstone.Application.Module.Status.ConsumerRabbitMq.Message;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.ConsumerRabbitMq
{
    public class OrderStatusConsumer : IConsumer<OrderStatusMessage>
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderStatusConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<OrderStatusMessage> context)
        {
            var position = context.Message.Position;
            var status = _unitOfWork.Statuses.Find(x => x.Id == context.Message.Status.Id).Include(c => c.Project).ThenInclude(c => c.Statuses).FirstOrDefault();
            if (status != null)
            {
                if (position > status.Position)
                {
                    foreach (var s in status.Project.Statuses)
                        if (s.Position > status.Position && s.Position <= position)
                            s.Position -= 1;
                }
                else
                {
                    foreach (var s in status.Project.Statuses)
                        if (s.Position < status.Position && s.Position >= position)
                            s.Position += 1;
                }
                status.Position = position;
                _unitOfWork.Statuses.Update(status);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
