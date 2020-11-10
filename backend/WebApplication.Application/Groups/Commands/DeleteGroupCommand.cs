using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Groups.Commands
{
    public class DeleteGroupCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
