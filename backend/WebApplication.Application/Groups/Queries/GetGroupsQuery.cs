using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Groups.Models;

namespace WebApplication.Application.Groups.Queries
{
    public class GetGroupsQuery : IRequest<IEnumerable<GroupDto>>
    {
        public string Name { get; set; }
        public IEnumerable<string> MemberIDs { get; set; }
    }
}
