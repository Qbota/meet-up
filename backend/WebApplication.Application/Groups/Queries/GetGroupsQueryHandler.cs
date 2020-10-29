using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Groups.Models;

namespace WebApplication.Application.Groups.Queries
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupDto>>
    {
        public async Task<IEnumerable<GroupDto>> Handle(GetGroupsQuery getGroupsQuery, CancellationToken cancellationToken)
        {
            return new List<GroupDto>();
        }
    }
}
