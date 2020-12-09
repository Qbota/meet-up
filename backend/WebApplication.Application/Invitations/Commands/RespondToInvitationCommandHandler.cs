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
using WebApplication.Application.Groups.Commands;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Invitations.Commands
{
    public class RespondToInvitationCommandHandler : IRequestHandler<RespondToInvitationCommand,string>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RespondToInvitationCommandHandler(IGroupRepository groupRepository,
            IInvitationRepository invitationRepository,
            IAuthorizationService authorizationService,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _groupRepository = groupRepository;
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
        }
        public async Task<string> Handle(RespondToInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetInvitationByIdAsync(request.InvitationId);
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext, invitation.UserId);
            if (request.Decision)
            {
                await UpdateGroupAsync(invitation);
                await UpdateUserAsync(invitation);
            }
            await _invitationRepository.DeleteInvitationAsync(invitation.Id);
            return invitation.Id;
        }

        private async Task UpdateUserAsync(InvitationDO invitation)
        {
            var user = await _userRepository.GetUserByIdAsync(invitation.UserId);
            if (user != null)
            {
                if (user.GroupIDs is null)
                {
                    user.GroupIDs = new List<string>();
                }
                user.GroupIDs.Add(invitation.GroupId);
                await _userRepository.UpdateUserAsync(user);
            }
        }

        private async Task UpdateGroupAsync(Mongo.Models.InvitationDO invitation)
        {
            var group = await _groupRepository.GetGroupByIdAsync(invitation.GroupId);
            if (group != null)
            {
                if (group.MemberIDs is null)
                {
                    group.MemberIDs = new List<string>();
                }
                group.MemberIDs.Add(invitation.UserId);
                await _groupRepository.UpdateGroupAsync(group);
            }
        }
    }
}
