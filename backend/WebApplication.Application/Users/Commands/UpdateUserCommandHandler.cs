using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrowAsync(_httpContextAccessor.HttpContext, request.Id);
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            var changes = _mapper.Map<UserDO>(request);
            var updated = _mapper.Map(user, changes);
            await _userRepository.UpdateUserAsync(updated);
            return request.Id;
        }
    }
}
