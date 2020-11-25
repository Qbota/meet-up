using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Application.Invitations.Commands;
using WebApplication.Application.Invitations.Models;
using WebApplication.Application.Invitations.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    //[Authorize]
    [Route("api/meet-up/invitation")]
    public class InvitationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvitationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<InvitationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserInvitationsAsync([FromRoute] string userId )
        {
            return Ok(await _mediator.Send(new GetUserInvitationsQuery() { UserId = userId}));
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InviteAsync([FromBody] InviteCommand inviteCommand)
        {
            var id = await _mediator.Send(inviteCommand);
            return Ok(id);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RespondToInvitationAsync([FromBody] RespondToInvitationCommand respondToInvitationCommand)
        {
            return Ok(await _mediator.Send(respondToInvitationCommand));
        }
    }
}