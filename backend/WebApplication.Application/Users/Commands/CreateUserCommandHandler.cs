using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Mongo;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserDO
            {
                Login = "test",
                Name = "IamTest"
                
            };
            await _userRepository.AddUserAsync(user);
            return user.ID;
        }
    }
}
