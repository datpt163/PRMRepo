using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Application.Module.Auth.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Auths.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
    }
}
