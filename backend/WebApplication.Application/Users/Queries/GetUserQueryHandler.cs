using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Users.Models;

namespace WebApplication.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        public async Task<User> Handle(GetUserQuery getUserQuery, CancellationToken cancellationToken)
        {
            return new User();
        }
    }
}
