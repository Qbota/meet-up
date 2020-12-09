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

namespace WebApplication.Application.Meetings.Commands
{
    public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGroupRepository _groupRepository;
        public DeleteMeetingCommandHandler(
            IMeetingRepository meetingRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IGroupRepository groupRepository)
        {
            _meetingRepository = meetingRepository;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _groupRepository = groupRepository;
        }
        public async Task<string> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.Id);
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, meeting.GroupID);
            await _meetingRepository.DeleteMeetingAsync(request.Id);
            await UpdateGroupAsync(meeting);
            return request.Id;
        }
        private async Task UpdateGroupAsync(MeetingDO meeting)
        {
            var group = await _groupRepository.GetGroupByIdAsync(meeting.GroupID);
            group.MeetingIDs.Remove(meeting.ID);
            await _groupRepository.UpdateGroupAsync(group);
        }
    }
}
