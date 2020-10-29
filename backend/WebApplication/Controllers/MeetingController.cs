using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Meetings.Commands;
using WebApplication.Application.Meetings.Models;
using WebApplication.Application.Meetings.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/meet-up/meeting")]
    public class MeetingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MeetingDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMeetingsAsync([FromQuery] GetMeetingsQuery getMeetingsQuery)
        {
            return Ok(await _mediator.Send(getMeetingsQuery));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{meetingId}")]
        [ProducesResponseType(typeof(MeetingDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMeetingByIdAsync([FromRoute]string meetingId)
        {
            return Ok(await _mediator.Send(new GetMeetingQuery { Id = meetingId }));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateMeetingAsync([FromBody] CreateMeetingCommand createMeetingCommand)
        {
            var id = await _mediator.Send(createMeetingCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteMeetingAsync([FromRoute] string meetingId)
        {
            return Ok(await _mediator.Send(new DeleteMeetingCommand { Id = meetingId }));
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateMeetingAsync([FromRoute] string MeetingId, [FromBody] UpdateMeetingCommand updateMeetingCommand)
        {
            return Ok(await _mediator.Send(updateMeetingCommand));
        }


    }
}