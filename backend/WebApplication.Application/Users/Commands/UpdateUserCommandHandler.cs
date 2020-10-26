using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return String.Empty;
        }
    }
}
