using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Meetings.Models;

namespace WebApplication.Application.Meetings.Queries
{
    public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, IEnumerable<MeetingDto>>
    {
        public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsQuery getMeetingsQuery, CancellationToken cancellationToken)
        {
            return new List<MeetingDto>();
        }
    }
}
