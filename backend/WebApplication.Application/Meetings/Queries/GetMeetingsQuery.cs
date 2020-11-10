using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meetings.Models;

namespace WebApplication.Application.Meetings.Queries
{
    public  class GetMeetingsQuery : IRequest<IEnumerable<MeetingDto>>
    {
        public string Title { get; set; }
        public string GroupID { get; set; }
    }
}
