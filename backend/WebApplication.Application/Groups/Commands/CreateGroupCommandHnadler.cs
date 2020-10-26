using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Groups.Commands
{
    public class CreateGroupCommandHnadler : IRequestHandler<CreateGroupCommand, string>
    {
        public async Task<string> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
