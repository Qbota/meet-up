using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Meetings.Commands
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, string>
    {
        public async Task<string> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            return String.Empty;
        }
    }
}
