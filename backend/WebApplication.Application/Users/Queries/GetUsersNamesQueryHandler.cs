using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetUsersNamesQueryHandler(
            IUserRepository userRepository, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserNameDto>> Handle(GetUsersNamesQuery getUsersQuery, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();
            var sender = _httpContextAccessor.HttpContext.Items["Account"] as UserDO;
            var list = users.Select(x => _mapper.Map<UserDO, UserNameDto>(x)).ToList();
            if (sender != null)
                list = list.Where(x => x.Id != sender.ID).ToList();
            return list;
        }
        
    }
}
