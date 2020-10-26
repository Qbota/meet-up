using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Meetings.Models;

namespace WebApplication.Application.Meetings.Queries
{
    public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, IEnumerable<Meeting>>
    {
        public async Task<IEnumerable<Meeting>> Handle(GetMeetingsQuery getMeetingsQuery, CancellationToken cancellationToken)
        {
            return new List<Meeting>();
        }
    }
}
