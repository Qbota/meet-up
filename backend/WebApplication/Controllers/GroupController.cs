using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Application.Groups.Commands;
using WebApplication.Application.Groups.Models;
using WebApplication.Application.Groups.Queries;

namespace WebApplication.Controllers
{
    [Route("api/meet-up/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("api/meet-up/group")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Group>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGroupsAsync([FromQuery] GetGroupsQuery getGroupsQuery)
        {
            return Ok(await _mediator.Send(getGroupsQuery));
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("api/meet-up/group/{groupId}")]
        [ProducesResponseType(typeof(Group), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGroupByIdAsync([FromRoute]string groupId)
        {
            return Ok(await _mediator.Send(new GetGroupQuery { Id = groupId }));
        }
        [HttpPost]
        [Route("api/meet-up/group/create-group")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateGroupAsync([FromBody] CreateGroupCommand createGroupCommand)
        {
            var id = await _mediator.Send(createGroupCommand);
            return Ok(id);
        }
        [HttpDelete]
        [Route("api/meet-up/group/delete-group/{groupId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteGroupAsync([FromRoute] string groupId)
        {
            return Ok(await _mediator.Send(new DeleteGroupCommand { Id = groupId }));
        }
        [HttpPost]
        [Route("api/meet-up/group/update-group/{groupId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateGroupAsync([FromRoute] string groupId, [FromBody] UpdateGroupCommand updateGroupCommand)
        {
            return Ok(await _mediator.Send(updateGroupCommand));
        }
    }
}