using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Application.Module.Permissions.Command;
using Capstone.Application.Module.Permissions.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Permissions.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupPermission()
        {
            var result = await _mediator.Send(new GetPermissionQuery());

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupPermission([FromBody] CreatePermissionCommand request)
        {
            var result = await _mediator.Send(request);

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("add-permissions-for-role")]
        public async Task<IActionResult> AddPermissionForRole([FromBody] AddPermissionForRoleCommand request)
        {
            var result = await _mediator.Send(request);

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
    }
}
