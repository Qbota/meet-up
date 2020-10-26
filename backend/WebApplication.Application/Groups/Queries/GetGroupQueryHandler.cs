using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Groups.Models;

namespace WebApplication.Application.Groups.Queries
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, Group>
    {
        public async Task<Group> Handle(GetGroupQuery getGroupQuery, CancellationToken cancellationToken)
        {
            return new Group();
        }
    }
}
