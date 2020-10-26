using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Groups.Commands
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, string>
    {
        public Task<string> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
