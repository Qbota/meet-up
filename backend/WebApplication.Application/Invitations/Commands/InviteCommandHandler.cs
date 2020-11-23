using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Invitations.Commands
{
    public class InviteCommandHandler : IRequestHandler<InviteCommand, string>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        public InviteCommandHandler(
            IInvitationRepository invitationRepository,
            IMapper mapper)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(InviteCommand request, CancellationToken cancellationToken)
        {
            var invitation = _mapper.Map<InvitationDO>(request);
            await _invitationRepository.AddInvitationAsync(invitation);
            return invitation.Id;
        }
    }
}
