using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Module.Users.Models;
using Capstone.Application.Module.Users.Command;
using Capstone.Application.Module.Users.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone.Api.Module.Users.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers([FromBody] GetUserListQuery query)
        {
            var response = await _mediator.Send(query);
            
            return ResponseOk(response.Data, response.Paging);

        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            var query = new GetUserDetailQuery(id);
            var user = await _mediator.Send(query);
            if (user == null)
            {
                return ResponseNotFound("User not found");
            }
            return ResponseOk(dataResponse: user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request)
        {
            var command = new UpdateUserCommand
            {
                Id = request.Id,
                AvatarFile = request.Avatar,
                FullName = request.FullName,
                Phone = request.Phone,
                Address = request.Address,
                Gender = request.Gender,
                Dob = request.Dob,
                BankAccount = request.BankAccount,
                BankAccountName = request.BankAccountName,
                Status = request.Status,
                RoleId = request.RoleId,
            };

            var updatedUser = await _mediator.Send(command);

            if (updatedUser == null)
            {
                return ResponseNotFound("User not found or update failed.");
            }

            return ResponseOk(dataResponse: updatedUser);
        }
    }
}