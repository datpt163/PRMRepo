using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Application.Module.Users.Query;
using MediatR;
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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUserListQuery();
            var users = await _mediator.Send(query);
            return ResponseOk(dataResponse: users);
        }

        [HttpGet("detail/{id:guid}")]
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
    }
}
