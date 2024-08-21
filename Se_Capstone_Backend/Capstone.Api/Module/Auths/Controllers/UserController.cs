using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Application.Module.Auth.Command;
using Capstone.Domain.Enums;
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

        [HttpPost("dangtest")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new RegisterCommand(token, request.Email, request.Password, request.UserName, request.FullName, request.Address, request.Gender, request.Dob, request.Phone));
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
        //[SwaggerResponse(200, "Successful", typeof(ResponseSuccess<GetProfileResponse>))]
        //[SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        //[Authorize]
        //[HttpGet("profile")]
        //public async Task<IActionResult> GetProfile()
        //{
        //    string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //    var result = await _mediator.Send(new GetProfileQuery() { Token = token });
        //    if (string.IsNullOrEmpty(result.ErrorMessage))
        //        return ResponseOk(dataResponse: result.Data);
        //    return ResponseBadRequest(messageResponse: result.ErrorMessage);
        //}
    }
}
