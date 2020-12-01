using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Movies.Queries
{
    public class GetMovieSetQuery :  IRequest<IEnumerable<MovieDto>>
    {
    }
}
