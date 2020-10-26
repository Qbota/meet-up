using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Groups.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, string>
    {
        public Task<string> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
