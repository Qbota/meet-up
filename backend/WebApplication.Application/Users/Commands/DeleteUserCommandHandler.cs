using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IGroupRepository groupRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _groupRepository = groupRepository;
        }
        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext, request.Id);
            await _userRepository.DeleteUserAsync(request.Id);
            await UpdateGroupsAsync(request.Id);
            return request.Id;
        }

        private async Task UpdateGroupsAsync(string userId)
        {
            var groups = await _groupRepository.GetGroupByUserIdAsync(userId);
            foreach (var group in groups)
            {
                group.MemberIDs.ToList().Remove(userId);
                await _groupRepository.UpdateGroupAsync(group);
            }
        }
    }
}
