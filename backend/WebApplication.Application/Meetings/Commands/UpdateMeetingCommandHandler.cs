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
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        public UpdateMeetingCommandHandler(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.ID);
            var changes = _mapper.Map<MeetingDO>(request);
            var updated = _mapper.Map(meeting, changes);
            await _meetingRepository.UpdateMeetingAsync(updated);
            return request.ID;
        }
    }
}
