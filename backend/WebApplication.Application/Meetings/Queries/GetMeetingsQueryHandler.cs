using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Meetings.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Queries
{
    public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, IEnumerable<MeetingDto>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetMeetingsQueryHandler(
            IMeetingRepository meetingRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsQuery getMeetingsQuery, CancellationToken cancellationToken)
        {
            var meetings = await _meetingRepository.GetMeetingsAsync();
            SearchByParams(ref meetings, getMeetingsQuery);
            var list = meetings.Select(x => _mapper.Map<MeetingDO, MeetingDto>(x)).ToList();
            _authorizationService.FilterResultByUserRightsAsync(_httpContextAccessor.HttpContext, ref list);
            return list;
        }

        private void SearchByParams(ref IEnumerable<MeetingDO> meetings, GetMeetingsQuery getMeetingsQuery)
        {
            if (!String.IsNullOrEmpty(getMeetingsQuery.Title))
            {
                meetings = meetings.Where(x => x.Title.ToLower().Contains(getMeetingsQuery.Title.ToLower()));
            }
            if (!String.IsNullOrEmpty(getMeetingsQuery.GroupID))
            {
                meetings = meetings.Where(x => x.GroupID.Contains(getMeetingsQuery.GroupID.ToLower()));
            }
        }
    }
}
