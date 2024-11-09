using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Api.Module.Comments.Request;
using Capstone.Api.Module.Labels.Requests;
using Capstone.Application.Module.Comments.Command;
using Capstone.Application.Module.Labels.Command;
using Capstone.Application.Module.Labels.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Comments.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] AddCommentRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new AddCommentCommand() { Content = request.Content, IssueId = request.IssueId, Token = token});
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);

            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new DeleteCommentCommand() { Id = id, Token = token });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new UpdateCommentCommand() { Id = id, Content = request.Content, Token = token });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }
    }
}
