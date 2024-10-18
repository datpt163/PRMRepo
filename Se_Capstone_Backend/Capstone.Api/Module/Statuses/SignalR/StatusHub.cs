using Capstone.Application.Common.Jwt;
using Capstone.Application.Module.Status.ConsumerRabbitMq.Message;
using Capstone.Infrastructure.Repository;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace Capstone.Api.Module.Statuses.SignalR
{
   
    public class StatusHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IPublishEndpoint _publishEndpoint;

        public StatusHub(IUnitOfWork unitOfWork, IJwtService jwtService, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _publishEndpoint = publishEndpoint;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                Console.WriteLine("Connnect success");
                await base.OnConnectedAsync(); // Gọi lại phương thức gốc để đảm bảo các xử lý mặc định được thực thi
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connnect Fail" + ex.Message);
            }
        }

        public async Task JoinGroup(string groupId)
        {
            Console.WriteLine("Join group success success");
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        public async Task StatusOrderRequest(string groupId, Guid statusId, int position)
        {
            try
            {
                var httpContext = Context.GetHttpContext();

                var status = _unitOfWork.Statuses.Find(x => x.Id == statusId).Include(c => c.Project).ThenInclude(c => c.Statuses).FirstOrDefault();
                if (status == null)
                    throw new Exception("Status not found.");
                if(position > status.Project.Statuses.Count())
                    throw new Exception("Some thing wrong with position");
                if(position == status.Position)
                    throw new Exception("Old position same new position");
                await _publishEndpoint.Publish(new OrderStatusMessage() { Status = status, Position = position });
                await Clients.Group(groupId).SendAsync("StatusOrderResponse", "Success");

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }
        }
    }

}
