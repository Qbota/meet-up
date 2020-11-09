using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Meetings.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Queries
{
    public class GetMeetingQueryHandler : IRequestHandler<GetMeetingQuery, MeetingDto>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        public GetMeetingQueryHandler(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }
        public async Task<MeetingDto> Handle(GetMeetingQuery getMeetingQuery, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetMeetingByIdAsync(getMeetingQuery.Id);
            var mappedMeeting = _mapper.Map<MeetingDO, MeetingDto>(meeting);
            return mappedMeeting;
        }
    }
}
