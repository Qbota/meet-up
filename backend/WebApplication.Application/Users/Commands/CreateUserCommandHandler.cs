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
using WebApplication.Mongo;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;


namespace WebApplication.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, JWTAuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly IJWTService _jWTService;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IHashService hashService,
            IJWTService jWTService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _jWTService = jWTService;
            _mapper = mapper;
        }
        public async Task<JWTAuthResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //add validation
            var user = new UserDO
            {
                Login = request.Login,
                Name = request.Name,
                Password = _hashService.GetPasswordHash(request.Password, out byte[] salt),
                MealPreference = new MealPreferenceDO { Allergens = request.Allergens},
                MoviePreference = new MoviePreferenceDO { Movies = request.Movies},
                Salt = salt,
             };
            await _userRepository.AddUserAsync(user);
            return _jWTService.GenerateTokens(user);
        }
    }
}
