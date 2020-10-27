using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Services;

namespace WebApplication.Application.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> Handle(GetUsersQuery getUsersQuery, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();
            return new List<User>();
        }
    }
}
