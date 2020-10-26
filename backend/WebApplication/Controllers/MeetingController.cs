using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApplication.Application.Meetings.Commands;
using WebApplication.Application.Meetings.Models;
using WebApplication.Application.Meetings.Queries;

namespace WebApplication.Controllers
{
    [Route("api/meet-up/meeting")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("api/meet-up/meeting")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Meeting>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMeetingsAsync([FromQuery] GetMeetingsQuery getMeetingsQuery)
        {
            return Ok(await _mediator.Send(getMeetingsQuery));
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("api/meet-up/meeting/{meetingId}")]
        [ProducesResponseType(typeof(Meeting), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMeetingByIdAsync([FromRoute]string meetingId)
        {
            return Ok(await _mediator.Send(new GetMeetingQuery { Id = meetingId }));
        }
        [HttpPost]
        [Route("api/meet-up/meeting/create-meeting")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateMeetingAsync([FromBody] CreateMeetingCommand createMeetingCommand)
        {
            var id = await _mediator.Send(createMeetingCommand);
            return Ok(id);
        }
        [HttpDelete]
        [Route("api/meet-up/meeting/delete-meeting/{meetingId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteMeetingAsync([FromRoute] string meetingId)
        {
            return Ok(await _mediator.Send(new DeleteMeetingCommand { Id = meetingId }));
        }
        [HttpPost]
        [Route("api/meet-up/meeting/update-meeting/{meetingId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateMeetingAsync([FromRoute] string MeetingId, [FromBody] UpdateMeetingCommand updateMeetingCommand)
        {
            return Ok(await _mediator.Send(updateMeetingCommand));
        }
    }
}