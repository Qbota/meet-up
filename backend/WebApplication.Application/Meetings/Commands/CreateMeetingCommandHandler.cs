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

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateMeetingCommandHandler(
        IMeetingRepository meetingRepository,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrowAsync(_httpContextAccessor.HttpContext, request.GroupID);
            var meeting = _mapper.Map<MeetingDO>(request);
            await _meetingRepository.AddMeetingAsync(meeting);
            return meeting.ID;
        }
    }
}

