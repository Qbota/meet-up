using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.AIs;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRecomendationService _recomendationService;

        public CreateMeetingCommandHandler(
        IMeetingRepository meetingRepository,
        IUserRepository userRepository,
        IGroupRepository groupRepository,
        IMapper mapper,
        IRecomendationService recomendationService,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _recomendationService = recomendationService;
        }
        public async Task<string> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, request.GroupID);
            var meeting = _mapper.Map<MeetingDO>(request);
            var user = _httpContextAccessor.HttpContext.Items["Account"] as UserDO;
            meeting.OrganiserID = user.ID;
            await _recomendationService.GetRecomendations(meeting);
            await _meetingRepository.AddMeetingAsync(meeting);
            await UpdateGroupAsync(meeting);
            return meeting.ID;
        }

        private async Task UpdateGroupAsync(MeetingDO meeting)
        {
            var group = await _groupRepository.GetGroupByIdAsync(meeting.GroupID);
            if (group.MeetingIDs is null)
                group.MeetingIDs = new List<string>();
            group.MeetingIDs.Add(meeting.ID);
            await _groupRepository.UpdateGroupAsync(group);
        }

        private async Task UpdateUserAsync(UserDO user, List<DateTime> dates)
        {
            user.AvailableDates.AddRange(dates);
            user.AvailableDates = user.AvailableDates.Distinct().ToList();
            await _userRepository.UpdateUserAsync(user);
        }
    }
}

