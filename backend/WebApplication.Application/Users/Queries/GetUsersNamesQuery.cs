using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Users.Models;

namespace WebApplication.Application.Users.Queries
{
    public class GetUsersNamesQuery : IRequest<IEnumerable<UserNameDto>>
    {
    }
}
