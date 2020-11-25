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
            foreach (var id in request.UsersId)
            {
                await _invitationRepository.AddInvitationAsync(new InvitationDO
                {
                    GroupId = request.GroupId,
                    GroupName = request.GroupName,
                    SenderName = request.SenderName,
                    UserId = id
                });
            }
            return "";
        }
    }
}
