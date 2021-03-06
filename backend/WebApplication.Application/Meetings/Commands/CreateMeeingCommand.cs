﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string GroupID { get; set; }
        public List<DateTime> Dates { get; set; }
    }
}
