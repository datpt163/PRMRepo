using Capstone.Application.Module.Jobs.Command;
using Capstone.Application.Module.Jobs.Response;
using Capstone.Api.Common.ResponseApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using Capstone.Application.Module.Jobs.Query;
using Capstone.Api.Module.Jobs.Request;
using static Google.Apis.Requests.BatchRequest;

namespace Capstone.Api.Module.Jobs.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : BaseController
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobCommand command)
        {
            var jobDto = await _mediator.Send(command);
            return ResponseCreated(jobDto, "Job created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateJobCommand command)
        {
            var jobDto = await _mediator.Send(command);
            if (jobDto == null)
            {
                return ResponseNotFound("Job not found or has been deleted");
            }
            return ResponseOk(jobDto, "Job updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteJobCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
            {
                return ResponseNotFound("Job not found or already deleted");
            }
            return ResponseNoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetJobQuery { Id = id };
            var jobDto = await _mediator.Send(query);
            if (jobDto == null)
            {
                return ResponseNotFound("Job not found or has been deleted");
            }
            return ResponseOk(jobDto, "Job retrieved successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetJobsListQuery request)
        {

            var response = await _mediator.Send(request);
            return ResponseOk(response.Data, response.Paging, "Jobs retrieved successfully");
        }


    }
}
