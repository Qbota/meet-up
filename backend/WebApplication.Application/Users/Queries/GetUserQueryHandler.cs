using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Users.Models;

namespace WebApplication.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserQuery getUserQuery, CancellationToken cancellationToken)
        {
            return new UserDto();
        }
    }
}
