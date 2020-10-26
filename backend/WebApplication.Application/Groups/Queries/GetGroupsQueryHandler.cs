using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Groups.Models;

namespace WebApplication.Application.Groups.Queries
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<Group>>
    {
        public async Task<IEnumerable<Group>> Handle(GetGroupsQuery getGroupsQuery, CancellationToken cancellationToken)
        {
            return new List<Group>();
        }
    }
}
