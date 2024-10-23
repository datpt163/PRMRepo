using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Application.Module.Permissions.Command;
using Capstone.Application.Module.Permissions.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Permissions.Controllers
{
    [Route("api/group-permissions")]
    [ApiController]
    public class GroupPermisionController : BaseController
    {
        private readonly IMediator _mediator;

        public GroupPermisionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupPermission()
        {
            var result = await _mediator.Send(new GetGroupPermissionQuery());

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupPermission([FromBody] CreateGroupPermissionCommand request)
        {
            var result = await _mediator.Send(request);

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
    }
}
