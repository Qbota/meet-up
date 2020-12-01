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
using WebApplication.Application.Movies.Commands;
using WebApplication.Application.Movies.Models;
using WebApplication.Application.Movies.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    //[Authorize]
    [Route("api/meet-up/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMovieSetAsync([FromQuery] GetMovieSetQuery getMoviesQuery)
        {
            return Ok(await _mediator.Send(getMoviesQuery));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{movieId}")]
        [ProducesResponseType(typeof(MovieDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMovieByIdAsync([FromRoute]string movieId)
        {
            return Ok(await _mediator.Send(new GetMovieQuery { Id = movieId }));
        }
        

        [HttpPost]
        [Produces("application/json")]
        [Route("/rate")]
        [ProducesResponseType(typeof(MovieDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RateMovieAsync([FromBody]RateMovieCommand rateMovieCommand)
        {
            return Ok(await _mediator.Send(rateMovieCommand));
        }
    }
}