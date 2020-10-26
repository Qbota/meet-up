using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Meetings.Commands
{
    public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, string>
    {
        public async Task<string> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            return String.Empty;
        }
    }
}
