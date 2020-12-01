using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Movies.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Movies.Queries
{
    public class GetMovieSetQueryHandler : IRequestHandler<GetMovieSetQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public GetMovieSetQueryHandler(
            IMovieRepository movieRepository,
            IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetMovieSetQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetMoviesBasicSetAsync();
            var list = movies.Select(x => _mapper.Map<MovieDO, MovieDto>(x)).ToList();
            return list;
        }
    }
}
