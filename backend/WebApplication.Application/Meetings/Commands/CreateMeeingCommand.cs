using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommand : IRequest<string>
    {
    }
}
