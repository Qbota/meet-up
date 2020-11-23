using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Invitations.Commands
{
    public class RespondToInvitationCommand : IRequest<string>
    {
        public string InvitationId { get; set; }
        public bool Decision { get; set; }
    }
}
