using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Meetings.Models;

namespace WebApplication.Application.Meetings.Queries
{
    public class GetMeetingQueryHandler : IRequestHandler<GetMeetingQuery, MeetingDto>
    {
        public async Task<MeetingDto> Handle(GetMeetingQuery getMeetingQuery, CancellationToken cancellationToken)
        {
            return new MeetingDto();
        }
    }
}
