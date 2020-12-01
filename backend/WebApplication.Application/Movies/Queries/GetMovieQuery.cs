using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Movies.Queries
{
    public class GetMovieQuery : IRequest<MovieDto>
    {
        public string Id { get; set; }
    }
}
