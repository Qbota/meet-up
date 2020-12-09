using AutoMapper;
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

namespace WebApplication.Application.Groups.Commands
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateGroupCommandHandler(
            IGroupRepository groupRepository, 
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<string> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, request.ID);
            var group = await _groupRepository.GetGroupByIdAsync(request.ID);
            var changes = _mapper.Map<GroupDO>(request);
            var updated = _mapper.Map(changes, group);
            await _groupRepository.UpdateGroupAsync(updated);
            await UpdateUsersAsync(request.ID, request.MemberIDs, group.MemberIDs);
            return request.ID;
        }
        private async Task UpdateUsersAsync(string groupId, IEnumerable<string> updatedUsersIds, IEnumerable<string> previousMembersIDs)
        {
            var newUsers = updatedUsersIds.Where(x => !previousMembersIDs.Contains(x));
            await RemoveUsersFromGroupAsync(previousMembersIDs, updatedUsersIds, groupId);
            await AddUserToNewGroupsAsync(newUsers, groupId);
        }

        private async Task AddUserToNewGroupsAsync(IEnumerable<string> newUsers, string groupId)
        {
            foreach (var userId in newUsers)
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                user.GroupIDs.Add(groupId);
                await _userRepository.UpdateUserAsync(user);
            }
        }

        private async Task RemoveUsersFromGroupAsync(IEnumerable<string> previousMembersIDs, IEnumerable<string> updatedUsersIds, string groupId)
        {
            foreach (var userId in previousMembersIDs)
            {
                if (!updatedUsersIds.Contains(userId))
                {
                    var user = await _userRepository.GetUserByIdAsync(userId);
                    user.GroupIDs.Remove(groupId);
                    await _userRepository.UpdateUserAsync(user);
                }
            }
        }
    }
}
