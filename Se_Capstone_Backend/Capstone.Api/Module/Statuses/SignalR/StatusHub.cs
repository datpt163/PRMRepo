using Capstone.Application.Common.Jwt;
using Capstone.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace Capstone.Api.Module.Statuses.SignalR
{
    [Authorize]
    public class StatusHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public StatusHub(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task JoinGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task StatusOrderRequest(string groupId, Guid statusId, int position)
        {
            try
            {
                var httpContext = Context.GetHttpContext();
                var token = httpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
                var user = await _jwtService.VerifyTokenAsync(token ?? "");
                if (user == null)
                    throw new Exception("Not found user");

                var status = _unitOfWork.Statuses.Find(x => x.Id == statusId).Include(c => c.Project).ThenInclude(c => c.Statuses).FirstOrDefault();
                if (status == null)
                    throw new Exception("Status not found.");
                if(position > status.Project.Statuses.Count())
                    throw new Exception("Some thing wrong with position");
                if(position == status.Position)
                    throw new Exception("Old position same new position");

                if(position > status.Position)
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

                await Clients.Group(groupId).SendAsync("StatusOrderResponse", user.Id);

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }

        }
    }

}
