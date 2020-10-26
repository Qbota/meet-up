using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Meetings.Commands
{
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, string>
    {
        public async Task<string> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            return String.Empty;
        }
    }
}
