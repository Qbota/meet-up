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

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateMeetingCommandHandler(
        IMeetingRepository meetingRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, request.GroupID);
            var meeting = _mapper.Map<MeetingDO>(request);
            var user = _httpContextAccessor.HttpContext.Items["Account"] as UserDO;
            meeting.OrganiserID = user.ID;
            await UpdateUserAsync(user, request.Dates);
            await _meetingRepository.AddMeetingAsync(meeting);
            return meeting.ID;
        }

        private async Task UpdateUserAsync(UserDO user, List<DateTime> dates)
        {
            user.AvailableDates.AddRange(dates);
            user.AvailableDates = user.AvailableDates.Distinct().ToList();
            await _userRepository.UpdateUserAsync(user);
        }
    }
}

