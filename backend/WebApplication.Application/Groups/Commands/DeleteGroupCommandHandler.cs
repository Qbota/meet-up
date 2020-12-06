using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public DeleteGroupCommandHandler(
            IGroupRepository groupRepository, 
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IMeetingRepository meetingRepository,
            IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _meetingRepository = meetingRepository;
        }
        public async Task<string> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, request.Id);
            var group = await _groupRepository.GetGroupByIdAsync(request.Id);
            await DeleteMeetingsAsync(group.MeetingIDs);
            await _groupRepository.DeleteGroupAsync(request.Id);
            await UpdateUsersAsync(request.Id);
            return request.Id;
        }
        private async Task UpdateUsersAsync(string groupId)
        {
            var users = await _userRepository.GetUsersByGroupIdAsync(groupId);
            foreach (var user in users)
            {
                user.GroupIDs.Remove(groupId);
                await _userRepository.UpdateUserAsync(user);
            }
        }
        private async Task DeleteMeetingsAsync(List<string> meetingIds)
        {
            foreach (var id in meetingIds)
            {
                await _meetingRepository.DeleteMeetingAsync(id);
            }
        }
    }
}
