using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Users.Queries
{
    public class GetUsersNamesQueryHandler : IRequestHandler<GetUsersNamesQuery, IEnumerable<UserNameDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUsersNamesQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserNameDto>> Handle(GetUsersNamesQuery getUsersQuery, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();
            var list = users.Select(x => _mapper.Map<UserDO, UserNameDto>(x)).ToList();
            return list;
        }
        
    }
}
