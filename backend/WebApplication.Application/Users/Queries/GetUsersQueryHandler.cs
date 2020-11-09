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
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUsersQueryHandler(IUserRepository userRepository,  IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery getUsersQuery, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();
            SearchByParams(ref users, getUsersQuery);
            var list = users.Select(x => _mapper.Map<UserDO,UserDto>(x)).ToList();
            return list;
        }

        private void SearchByParams(ref IEnumerable<UserDO> users, GetUsersQuery getUsersQuery)
        {
            if (!String.IsNullOrEmpty(getUsersQuery.Name))
            {
                users = users.Where(x => x.Name.ToLower().Contains(getUsersQuery.Name.ToLower()));
            }
            if (getUsersQuery.GroupIDs != null && getUsersQuery.GroupIDs.Any())
            {
                users = users.Where(x => x.GroupIDs.Select(x => getUsersQuery.GroupIDs.Contains(x)).Any());
            }
        }

    }
}
