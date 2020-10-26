using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
