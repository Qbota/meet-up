using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Application;
using WebApplication.Application.Authorization;
using WebApplication.Application.Refresh;

namespace WebApplication.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [Route("api/meet-up/login")]
        [ProducesResponseType(typeof(JWTAuthResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody]LoginCommand loginCommand)
        {
            return Ok(await _mediator.Send(loginCommand));
        }
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [Route("api/meet-up/refresh")]
        [ProducesResponseType(typeof(JWTAuthResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody]RefreshCommand refreshCommand)
        {
            return Ok(await _mediator.Send(refreshCommand));
        }

    }
}