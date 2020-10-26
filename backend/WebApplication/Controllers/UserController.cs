using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Application.Users.Commands;
using WebApplication.Application.Users.Models;
using WebApplication.Application.Users.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/meet-up/user")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] GetUsersQuery getUsersQuery)
        {
            return Ok(await _mediator.Send(getUsersQuery));
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("api/meet-up/user/{userId}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute]string userId)
        {
            return Ok(await _mediator.Send(new GetUserQuery { Id = userId}));
        }
        [HttpPost]
        [Route("api/meet-up/user/create-user")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
        {
            var id = await _mediator.Send(createUserCommand);
            return Ok(id);
        }
        [HttpDelete]
        [Route("api/meet-up/user/delete-user/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string userId)
        {
            return Ok(await _mediator.Send(new DeleteUserCommand { Id = userId }));
        }
        [HttpPost]
        [Route("api/meet-up/user/update-user/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UpdateUserCommand updateUserCommand)
        {
            return Ok(await _mediator.Send(updateUserCommand));
        }

    }
}