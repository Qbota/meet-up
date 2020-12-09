using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Movies.Models;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;


namespace WebApplication.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, JWTAuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IHashService _hashService;
        private readonly IJWTService _jWTService;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IMovieRepository movieRepository,
            IHashService hashService,
            IJWTService jWTService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _hashService = hashService;
            _jWTService = jWTService;
            _mapper = mapper;
        }
        public async Task<JWTAuthResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserDO
            {
                Login = request.Login,
                Name = request.Name,
                Password = _hashService.GetPasswordHash(request.Password, out byte[] salt),
                MealPreference = new MealPreferenceDO {
                    Allergens = request.Allergens ?? new List<string>(),
                    Cousines = new List<string>() },
                MoviePreference = new MoviePreferenceDO {
                    Ratings = request.Movies ?? new Dictionary<string, double>(),
                    MovieGenres = new List<string>() },
                AvailableDates = new List<DateTime>(),
                GroupIDs = new List<string>(),
                Salt = salt,
             };
            await _userRepository.AddUserAsync(user);
            var authResult = _jWTService.GenerateTokens(user);
            authResult.User = _mapper.Map<UserDO, UserDto>(user);
            authResult.User.MoviePreference.Movies = await GetMovies(authResult.User.MoviePreference.Ratings);
            return authResult;
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
