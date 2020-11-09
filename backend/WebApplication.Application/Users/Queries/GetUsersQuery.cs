using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebApplication.Application.Users.Models;

namespace WebApplication.Application.Users.Queries
{
     public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public string Name { get; set; }
        public IEnumerable<string> GroupIDs { get; set; }
    }
}
