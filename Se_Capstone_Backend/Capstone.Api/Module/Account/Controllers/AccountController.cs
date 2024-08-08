using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Api.Module.Account.Request;
using Capstone.Application.Module.Account.Command;
using Capstone.Application.Module.Account.Query;
using Capstone.Application.Module.Account.Response;
using Capstone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerResponse(200, "Successful", typeof(ResponseSuccess<LoginResponse>))]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody]LoginQuery loginQuery)
        {
            var result = await _mediator.Send(loginQuery);

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("change-password")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new ChangePasswordCommand() { Token = token, NewPassword = request.NewPassword, OldPassword = request.OldPassword});
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("forgot-password")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [SwaggerResponse(204, "Success")]
        public async Task<IActionResult> SendEmailForgotPass([FromBody] ForgotPasswordQuery forgotPasswordQuery)
        {
            var result = await _mediator.Send(forgotPasswordQuery);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("reset-password")]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
    }
}
