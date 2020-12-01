using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMovieRecomendationQueryHandler : IRequestHandler<GetMovieRecomendationQuery, IEnumerable<MovieDto>>
    {
        public GetMovieRecomendationQueryHandler()
        {
        }
        public Task<IEnumerable<MovieDto>> Handle(GetMovieRecomendationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
