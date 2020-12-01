using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Users.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserQueryHandler(
            IUserRepository userRepository, 
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserDto> Handle(GetUserQuery getUserQuery, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext,getUserQuery.Id);
            var user = await _userRepository.GetUserByIdAsync(getUserQuery.Id);
            var mappedUser = _mapper.Map<UserDO, UserDto>(user);
            return mappedUser;
        }
    }
}
