using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Commands
{
    public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        public DeleteMeetingCommandHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }
        public async Task<string> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            await _meetingRepository.DeleteMeetingAsync(request.Id);
            return request.Id;
        }
    }
}
