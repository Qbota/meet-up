using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Invitations.Commands
{
    public class RespondToInvitationCommandHandler : IRequestHandler<RespondToInvitationCommand,string>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RespondToInvitationCommandHandler(IGroupRepository groupRepository,
            IInvitationRepository invitationRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _groupRepository = groupRepository;
            _invitationRepository = invitationRepository;
        }
        public async Task<string> Handle(RespondToInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetInvitationByIdAsync(request.InvitationId);
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext, invitation.UserId);
            if (request.Decision)
            {
                var group = await _groupRepository.GetGroupByIdAsync(invitation.GroupId);
                if (group != null && group.MemberIDs != null)
                {
                    group.MemberIDs.ToList().Add(invitation.UserId);
                    await _groupRepository.UpdateGroupAsync(group);
                }
            }
            await _invitationRepository.DeleteInvitationAsync(invitation.Id);
            return invitation.Id;
        }
    }
}
