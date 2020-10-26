using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Users.Models;

namespace WebApplication.Application.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(GetUsersQuery getUsersQuery, CancellationToken cancellationToken)
        {
            return new List<User>();
        }
    }
}
