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
using WebApplication.Application.Users.Commands;
using WebApplication.Application.Users.Models;
using WebApplication.Application.Users.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    //[Authorize]
    [EnableCors("VueCorsPolicy")]
    [Route("api/meet-up/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] GetUsersQuery getUsersQuery)
        {
            return Ok(await _mediator.Send(getUsersQuery));
        }
        [HttpGet]
        [Route("names")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<UserNameDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsersNamesAsync()
        {
            return Ok(await _mediator.Send(new GetUsersNamesQuery()));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{userId}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute]string userId)
        {
            return Ok(await _mediator.Send(new GetUserQuery { Id = userId}));
        }

        [HttpPost]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
        {
            var id = await _mediator.Send(createUserCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            return Ok(await _mediator.Send(new DeleteUserCommand { Id = userId }));
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UpdateUserCommand updateUserCommand)
        {
            return Ok(await _mediator.Send(updateUserCommand));
        }

    }
}