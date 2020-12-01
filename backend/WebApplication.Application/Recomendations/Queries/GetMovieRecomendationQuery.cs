using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMovieRecomendationQuery : IRequest<IEnumerable<MovieDto>>
    {
        public string MeetingID;
    }
}
