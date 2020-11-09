using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, JWTAuthResult>
    {
        private readonly IJWTService _jWTService;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        public LoginCommandHandler(
            IJWTService jWTService,
            IUserRepository userRepository, 
            IHashService hashService)
        {
            _jWTService = jWTService;
            _userRepository = userRepository;
            _hashService = hashService;
        }
        public async Task<JWTAuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            UserDO user = await GetUserByCredentials(request);
            if (user != null)
            {
                return _jWTService.GenerateTokens(user);
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
    }
}
