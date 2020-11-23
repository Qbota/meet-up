using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Invitations.Models;

namespace WebApplication.Application.Invitations.Queries
{
    public class GetUserInvitationsQuery : IRequest<IEnumerable<InvitationDto>>
    {
        public string UserId { get; set; }
    }
}
