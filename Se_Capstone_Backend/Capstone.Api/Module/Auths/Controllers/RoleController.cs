using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Application.Module.Auth.Query;
using Capstone.Application.Module.Auth.Response;
using Capstone.Application.Module.Auths.Command;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Auths.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(IMediator mediator, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _mediator = mediator;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "ADD_ROLE")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand request)
        {
            var result = await _mediator.Send(request);

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPut]
        [Authorize(Roles = "ADD_ROLE")]
        public async Task<IActionResult> CreateRole([FromBody] UpdateRoleCommand request)
        {
            var result = await _mediator.Send(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                if(result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
            return ResponseOk(dataResponse: result.Data);
        }

        [HttpPost("add-role-for-user")]
        [SwaggerResponse(204, "Successful")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles = "ADD_ROLE_FOR_USER")]
        public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleForUserCommand request)
        {
            var result = await _mediator.Send(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                if (result.StatusCode == 400)
                    return ResponseBadRequest(messageResponse: result.ErrorMessage);
                return ResponseNotFound(messageResponse: result.ErrorMessage);
            }

            return ResponseOk(dataResponse: result.Data);

        }

        [HttpGet]
        [SwaggerResponse(200, "Successful", typeof(List<RoleDTO>))]
        [Authorize(Roles = "READ_LIST_ROLE")]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetListRoleQuery());
            return ResponseOk(dataResponse: result.Data);
        }
    }
}
