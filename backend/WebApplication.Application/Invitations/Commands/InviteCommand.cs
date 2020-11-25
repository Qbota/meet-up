using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Invitations.Commands
{
    public class InviteCommand : IRequest<string>
    {
        public string SenderName { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public List<string> UsersId { get; set; }
    }
}
