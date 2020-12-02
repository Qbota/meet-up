using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Movies.Models;
using WebApplication.Application.Movies.Queries;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserQueryHandler(
            IUserRepository userRepository,
            IMovieRepository movieRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _movieRepository = movieRepository;
        }
        public async Task<UserDto> Handle(GetUserQuery getUserQuery, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext,getUserQuery.Id);
            var user = await _userRepository.GetUserByIdAsync(getUserQuery.Id);
            var mappedUser = _mapper.Map<UserDO, UserDto>(user);
            mappedUser.MoviePreference.Movies = await GetMovies(mappedUser.MoviePreference.Ratings);
            return mappedUser;
        }

        private async Task<List<MovieDto>> GetMovies(Dictionary<string, double> ratings)
        {
            var list = new List<MovieDO>();
            foreach (var movieId in ratings.Keys)
            {
                list.Add(await _movieRepository.GetMovieByIdAsync(movieId));
            }    
            return list.Select(x => _mapper.Map<MovieDto>(x)).ToList();
        }
    }
}
