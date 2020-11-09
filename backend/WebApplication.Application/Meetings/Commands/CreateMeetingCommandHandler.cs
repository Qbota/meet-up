using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        public CreateMeetingCommandHandler(
            IMeetingRepository meetingRepository,
            IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = _mapper.Map<MeetingDO>(request);
            await _meetingRepository.AddMeetingAsync(meeting);
            return meeting.ID;
        }
    }
}
