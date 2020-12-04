using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Application.Authorization;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;
using WebApplication.Application.Movies.Models;
using System.Linq;

namespace WebApplication.Application.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, JWTAuthResult>
    {
        private readonly IJWTService _jWTService;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        public LoginCommandHandler(
            IJWTService jWTService,
            IUserRepository userRepository, 
            IMovieRepository movieRepository,
            IHashService hashService,
            IMapper mapper)
           
        {
            _jWTService = jWTService;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _hashService = hashService;
            _mapper = mapper;
        }
        public async Task<JWTAuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            UserDO user = await GetUserByCredentials(request);
            if (user != null)
            {
                var result = _jWTService.GenerateTokens(user);
                result.User = _mapper.Map<UserDO, UserDto>(user);
                result.User.MoviePreference.Movies = await GetMovies(result.User.MoviePreference.Ratings);
                return result;
            }
            throw new AuthenticationException();
        }

        private async Task<UserDO> GetUserByCredentials(LoginCommand request)
        {
            var user = await _userRepository.GetUserByLoginAsync(request.Login);
            if (user != null && _hashService.ComparePasswords(request.Password, user.Password,user.Salt))
            {
                return user;
            }
            return null;
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
