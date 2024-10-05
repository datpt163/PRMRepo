using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Application.Module.Applicants.Command;
using Capstone.Application.Module.Applicants.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Capstone.Api.Module.Applicants.Controllers
{
    [ApiController]
    [Route("api/applicants")] // Updated route
    public class ApplicantsController : BaseController
    {
        private readonly IMediator _mediator;

        public ApplicantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/applicants
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplicants([FromQuery] GetApplicantListQuery query) // Changed FromBody to FromQuery for GET
        {
            var response = await _mediator.Send(query);
            return ResponseOk(response.Data, response.Paging);
        }

        // GET api/applicants/{id}
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplicantDetail(Guid id)
        {
            var query = new GetApplicantDetailQuery(id);
            var applicant = await _mediator.Send(query);
            if (applicant == null)
            {
                return ResponseNotFound("Applicant not found");
            }
            return ResponseOk(dataResponse: applicant);
        }

        // DELETE api/applicants/{id}
        [HttpDelete("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteApplicant(Guid id)
        {
            var command = new DeleteApplicantCommand(id);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return ResponseNotFound("Applicant not found or deletion failed.");
            }

            return ResponseOk(dataResponse: result, "Applicant deleted successfully.");
        }

        // POST api/applicants/add
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddApplicant([FromForm] AddApplicantCommand command)
        {
            var applicantDto = await _mediator.Send(command);
            return ResponseCreated(applicantDto);
        }

    }
}
