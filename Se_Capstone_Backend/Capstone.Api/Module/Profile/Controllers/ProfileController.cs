using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Api.Module.Profile.Request;
using Capstone.Application.Module.Account.Query;
using Capstone.Application.Module.Account.Response;
using Capstone.Application.Module.Profile.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Profile.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerResponse(200, "Successful", typeof(ResponseSuccess<LoginResponse>))]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var updateProfileCommand = new UpdateProfileCommand(token, updateUserRequest.FirstName, updateUserRequest.LastName);
            var result = await _mediator.Send(new LoginQuery());
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
    }
}
