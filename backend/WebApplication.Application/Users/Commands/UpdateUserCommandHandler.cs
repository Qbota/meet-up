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

namespace WebApplication.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IGroupRepository groupRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _groupRepository = groupRepository;
        }
        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext, request.Id);
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            var changes = _mapper.Map<UserDO>(request);
            var updated = new UserDO
            {
                ID = user.ID,
                Login = user.Login,
                Salt = user.Salt,
                Password = user.Password,
                Name = String.IsNullOrEmpty(changes.Name) ? user.Name : changes.Name,
                AvailableDates = changes.AvailableDates ?? user.AvailableDates,
                GroupIDs = changes.GroupIDs ?? user.GroupIDs,
                MealPreference = changes.MealPreference != null ? changes.MealPreference : user.MealPreference,
                MoviePreference = changes.MoviePreference != null ? changes.MoviePreference : user.MoviePreference
            };
            await _userRepository.UpdateUserAsync(updated);
            await UpdateGroupsAsync(request.Id, request.GroupIDs);
            return request.Id;
        }
        private async Task UpdateGroupsAsync(string userId, IEnumerable<string> updatedGroupsIds)
        {
            var existingGroups = await _groupRepository.GetGroupByUserIdAsync(userId);
            var newGroups = updatedGroupsIds.Where(x => !existingGroups.Select(x => x.ID).Contains(x));
            await RemoveFromGroupsAsync(existingGroups, updatedGroupsIds, userId);
            await AddUserToNewGroupsAsync(newGroups,userId);
        }

        private async Task AddUserToNewGroupsAsync(IEnumerable<string> newGroups, string userId)
        {
            foreach (var groupId in newGroups)
            {
                var group = await _groupRepository.GetGroupByIdAsync(groupId);
                group.MemberIDs.Add(userId);
                await _groupRepository.UpdateGroupAsync(group);
            }
        }

        private async Task RemoveFromGroupsAsync(List<GroupDO> existingGroups, IEnumerable<string> updatedGroupsIds, string userId)
        {
            foreach (var group in existingGroups)
            {
                if (!updatedGroupsIds.Contains(group.ID))
                {
                    group.MemberIDs.Remove(userId);
                    await _groupRepository.UpdateGroupAsync(group);
                }
            }
        }
    }
}
