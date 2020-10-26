using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meetings.Models;

namespace WebApplication.Application.Meetings.Queries
{
    public  class GetMeetingsQuery : IRequest<IEnumerable<Meeting>>
    {
    }
}
